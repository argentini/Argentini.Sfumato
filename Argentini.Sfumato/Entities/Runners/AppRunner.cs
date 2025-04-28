using System.Reflection;

namespace Argentini.Sfumato.Entities.Runners;

public sealed class AppRunner
{
	#region Properties

	public Stopwatch CssBuildStopwatch { get; } = new();
	public Stopwatch FileScanStopwatch { get; } = new();
	public string LastCss { get; set; } = string.Empty;
	public List<string> Messages { get; } = [];
	public bool MessagesBusy { get; set; }
	public bool IsFirstRun { get; set; } = true;
	
	public AppState AppState { get; }
	public Library.Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; } = new(null);
	public ConcurrentDictionary<string,ScannedFile> ScannedFiles { get; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,string> UsedCssCustomProperties { get; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,string> UsedCss { get; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,CssClass> UtilityClasses { get; } = new(StringComparer.Ordinal);

	private string _cssFilePath;
	private readonly bool _useMinify;

	private List<FileSystemWatcher> FileWatchers { get; } = [];
	private ConcurrentDictionary<long, FileSystemEventArgs> RestartAppQueue { get; } = [];
	private ConcurrentDictionary<long, FileSystemEventArgs> RebuildProjectQueue { get; } = [];
	
    #endregion

    #region Construction
    
    public AppRunner(AppState appState, string cssFilePath = "", bool useMinify = false)
    {
	    AppState = appState;

	    _cssFilePath = cssFilePath;
	    _useMinify = useMinify;

	    Initialize();
    }

    #endregion
    
    #region Process Settings

    /// <summary>
    /// Clears AppRunnerSettings and loads default settings from defaults.css.
    /// </summary>
    private void Initialize()
    {
	    try
	    {
		    AppRunnerSettings = new AppRunnerSettings(this)
		    {
			    CssFilePath = _cssFilePath,
			    UseMinify = _useMinify
		    };

		    AppRunnerSettings.ExtractSfumatoItems(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "defaults.css")));

		    ProcessCssSettings();
	    }
	    catch (Exception e)
	    {
		    Messages.Add($"{AppState.CliErrorPrefix}Initialize() - {e.Message}");
	    }
    }

    /// <summary>
    /// Loads the CSS file, imports partials, extracts the Sfumato settings block, and processes it.
    /// </summary>
    public async Task LoadCssFileAsync()
    {
	    try
	    {
		    AppRunnerSettings.LoadCssAndExtractSfumatoBlock(); // Extract Sfumato settings and CSS content
		    AppRunnerSettings.ExtractSfumatoItems(); // Parse all the Sfumato settings into a Dictionary<string,string>()
		    AppRunnerSettings.ProcessProjectSettings(); // Read project/operation settings
		    AppRunnerSettings.ImportPartials(); // Read in all CSS partial files (@import "...")

		    ProcessCssSettings();
	    }
	    catch (Exception e)
	    {
		    Messages.Add($"{AppState.CliErrorPrefix}LoadCssFileAsync() - {e.Message}");
	    }
	    
	    await Task.CompletedTask;
    }

    /// <summary>
    /// Processes CSS settings for colors, breakpoints, etc., and uses reflection to load all others per utility class file.  
    /// </summary>
    public void ProcessCssSettings()
    {
	    #region Read color definitions
	    
	    foreach (var match in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--color-")))
	    {
		    var key = match.Key.TrimStart("--color-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (Library.ColorsByName.TryAdd(key, match.Value) == false)
			    Library.ColorsByName[key] = match.Value;
	    }
	    
	    #endregion

	    #region Read breakpoints

	    var prefixOrder = 100;

	    foreach (var match in AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (match.Key.StartsWith("--breakpoint-") == false)
			    continue;
		    
		    var key = match.Key.TrimStart("--breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(width >= {match.Value})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[key] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(width >= {match.Value})"
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (Library.MediaQueryPrefixes.TryAdd($"max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(width < {match.Value})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[$"max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(width < {match.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }
	    
	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (breakpoint.Key.StartsWith("--adaptive-breakpoint-") == false)
			    continue;

		    var key = breakpoint.Key.TrimStart("--adaptive-breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (double.TryParse(breakpoint.Value, out var maxValue) == false)
			    continue;

		    if (Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(min-aspect-ratio: {breakpoint.Value})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[key] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(min-aspect-ratio: {breakpoint.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (Library.MediaQueryPrefixes.TryAdd($"max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(max-aspect-ratio: {maxValue - 0.000000000001})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[$"max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(max-aspect-ratio: {maxValue - 0.000000000001})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }

	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
	    {
		    var key = breakpoint.Key.TrimStart("--container-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (Library.ContainerQueryPrefixes.TryAdd($"@{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width >= {breakpoint.Value})"
		        }) == false)
		    {
			    Library.ContainerQueryPrefixes[$"@{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width >= {breakpoint.Value})"
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (Library.ContainerQueryPrefixes.TryAdd($"@max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width < {breakpoint.Value})"
		        }) == false)
		    {
			    Library.ContainerQueryPrefixes[$"@max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width < {breakpoint.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }

	    #endregion
	    
	    #region Read @custom-variant shorthand items

	    foreach (var match in AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (match.Key.StartsWith("@custom-variant") == false)
			    continue;

		    if (match.Value.StartsWith("&:", StringComparison.Ordinal) == false && match.Value.StartsWith('@') == false)
			    continue;

		    var keySegments = match.Key.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			    
		    if (keySegments.Length < 2)
			    continue;

		    var key = keySegments[1];

		    if (match.Value.StartsWith("&:"))
		    {
			    if (Library.PseudoclassPrefixes.TryAdd(key, new VariantMetadata
			        {
				        PrefixType = "pseudoclass",
				        SelectorSuffix = $"{match.Value.TrimStart('&')}"
			        }) == false)
			    {
				    Library.PseudoclassPrefixes[key] = new VariantMetadata
				    {
					    PrefixType = "pseudoclass",
					    SelectorSuffix = $"{match.Value.TrimStart('&')}"
				    };
			    }
		    }
		    else
		    {
			    if (string.IsNullOrEmpty(key))
				    continue;

			    var wrapperSegments = match.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			    
			    if (wrapperSegments.Length < 2)
				    continue;

			    var prefixType = wrapperSegments[0].TrimStart('@').TrimStart('&');

			    if (string.IsNullOrEmpty(prefixType))
				    continue;

			    var statement = $"{match.Value.TrimStart($"@{prefixType}")?.Trim()}";
			    
			    if (prefixType.Equals("media", StringComparison.OrdinalIgnoreCase))
			    {
				    if (Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
				        {
					        PrefixOrder = prefixOrder,
					        PrefixType = prefixType,
					        Statement = statement
				        }) == false)
				    {
					    Library.MediaQueryPrefixes[key] = new VariantMetadata
					    {
						    PrefixOrder = prefixOrder,
						    PrefixType = prefixType,
						    Statement = statement
					    };
				    }

				    if (prefixOrder < int.MaxValue - 100)
					    prefixOrder += 100;
			    }
			    else if (prefixType.Equals("supports", StringComparison.OrdinalIgnoreCase))
			    {
				    if (Library.SupportsQueryPrefixes.TryAdd(key, new VariantMetadata
				        {
					        PrefixOrder = Library.SupportsQueryPrefixes.Count + 1,
					        PrefixType = prefixType,
					        Statement = statement
				        }) == false)
				    {
					    Library.SupportsQueryPrefixes[key] = new VariantMetadata
					    {
						    PrefixOrder = Library.SupportsQueryPrefixes.Count + 1,
						    PrefixType = prefixType,
						    Statement = statement
					    };
				    }
			    }
		    }
	    }
	    
	    #endregion
	    
	    #region Read @utility items

	    foreach (var match in AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (match.Key.StartsWith("@utility") == false)
			    continue;

		    var segments = match.Key.Split(' ', StringSplitOptions.RemoveEmptyEntries);

		    if (segments.Length != 2)
			    continue;
		    
		    if (Library.SimpleClasses.TryAdd(segments[1], new ClassDefinition
		        {
			        IsSimpleUtility = true,
			        Template = match.Value.Trim().TrimStart('{').TrimEnd('}').Trim()
		        }))
			    Library.ScannerClassNamePrefixes.Insert(segments[1]);
	    }

	    #endregion
	    
		#region Read theme settings from ClassDictionary instances (e.g. --text-xs, etc.)
	    
	    var derivedTypes = Assembly.GetExecutingAssembly()
		    .GetTypes()
		    .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

	    foreach (var type in derivedTypes)
	    {
		    if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
			    continue;
		    
		    instance.ProcessThemeSettings(this);
	    }
	    
	    #endregion
    }

	#endregion

	#region Messaging

	public async Task AddCssPathMessageAsync()
	{
		while (MessagesBusy)
			await Task.Delay(25);
		
		var relativePath = Path.GetFullPath(AppRunnerSettings.CssFilePath).TruncateCenter(
			(int)Math.Floor(Entities.Library.Library.MaxConsoleWidth / 3d),
			(int)Math.Floor((Entities.Library.Library.MaxConsoleWidth / 3d) * 2) - 3,
			Entities.Library.Library.MaxConsoleWidth);

		Messages.Add(relativePath);
	}

	public async Task AddMessageAsync(string message)
	{
		while (MessagesBusy)
			await Task.Delay(25);

		Messages.Add(message);
	}

	public async Task RenderMessagesAsync()
	{
		while (MessagesBusy)
			await Task.Delay(25);

		MessagesBusy = true;

		if (IsFirstRun == false)
			Messages.Insert(0, Strings.DotLine.Repeat(Entities.Library.Library.MaxConsoleWidth));

		Program.Dispatcher.Post(Messages.ToList());
		
		Messages.Clear();
		MessagesBusy = false;

		if (IsFirstRun)
			IsFirstRun = false;
	}

	#endregion
	
	#region File Scanning
	
	public async Task ReInitializeAsync()
	{
		Initialize();
		await LoadCssFileAsync();
	}

	public async Task PerformFileScanAsync()
	{
		var tasks = new ConcurrentBag<Task>();

		ScannedFiles.Clear();
		FileScanStopwatch.Restart();

		foreach (var path in AppRunnerSettings.AbsolutePaths)
			tasks.Add(RecurseProjectPathAsync(path, tasks));

		await Task.WhenAll(tasks);
		
		ProcessScannedFileUtilityClassDependencies(this);
		
		FileScanStopwatch.Stop();
		
		await AddMessageAsync($"Found {ScannedFiles.Count:N0} file{(ScannedFiles.Count == 1 ? string.Empty : "s")}, {UtilityClasses.Count:N0} class{(UtilityClasses.Count == 1 ? string.Empty : "es")} in {FileScanStopwatch.FormatTimer()}");
	}

	private async Task RecurseProjectPathAsync(string? sourcePath, ConcurrentBag<Task> tasks)
	{
		if (string.IsNullOrEmpty(sourcePath))
			return;

		string path;

		try
		{
			path = Path.GetFullPath(sourcePath);
		}
		catch
		{
			Messages.Add($"{Strings.TriangleRight} WARNING: Source directory could not be found: {sourcePath}");
			return;
		}
		
		var dir = new DirectoryInfo(path);
		var dirs = dir.GetDirectories();
		var files = dir.GetFiles();

		foreach (var fileInfo in files)
			tasks.Add(AddProjectFile(fileInfo));
		
		// ReSharper disable once LoopCanBeConvertedToQuery
		foreach (var subDir in dirs.OrderBy(d => d.Name))
		{
			if (AppRunnerSettings.AbsoluteNotPaths.Any(s => s.Equals(subDir.FullName, StringComparison.Ordinal)))
				continue;

			if (AppRunnerSettings.NotFolderNames.Any(s => s.Equals(subDir.Name, StringComparison.Ordinal)))
				continue;
			
			await RecurseProjectPathAsync(subDir.FullName, tasks);
		}
		
		await Task.CompletedTask;
	}
	
	private async Task AddProjectFile(FileInfo fileInfo)
	{
		if (Library.InvalidFileExtensions.Any(e => fileInfo.Name.EndsWith(e, StringComparison.Ordinal)))
			return;

		if (Library.ValidFileExtensions.Any(e => fileInfo.Extension.Equals(e, StringComparison.Ordinal)) == false)
			return;
		
		var scannedFile = new ScannedFile(fileInfo.FullName);

		await scannedFile.LoadAndScanFileAsync(this);

		ScannedFiles.TryAdd(fileInfo.FullName, scannedFile);
	}
	
	#endregion
	
	#region CSS Generation

	/// <summary>
	/// Build method used by the CLI
	/// </summary>
	public async Task BuildAndSaveCss()
	{
		CssBuildStopwatch.Restart();

		LastCss = BuildCss();

		await File.WriteAllTextAsync(AppRunnerSettings.NativeCssOutputFilePath, LastCss);

		CssBuildStopwatch.Stop();

		Messages.Add($"{LastCss.Length.FormatBytes()} written to {AppRunnerSettings.CssOutputFilePath} in {CssBuildStopwatch.FormatTimer()}");
		Messages.Add($"Build complete at {DateTime.Now:HH:mm:ss.fff}");
		Messages.Add(Strings.DotLine.Repeat(Entities.Library.Library.MaxConsoleWidth));

		await RenderMessagesAsync();
	}

	/// <summary>
	/// Generate the final CSS output; used internally and for tests
	/// </summary>
	/// <returns></returns>
    public string BuildCss()
	{
		var sourceCss = AppState.StringBuilderPool.Get();
		var workingSb = AppState.StringBuilderPool.Get();

		try
		{
			sourceCss
				.AppendResetCss(this)
				.AppendFormsCss(this)
				.AppendUtilityClassMarker(this)
				
				.AppendProcessedSourceCss(this)

				.ProcessAtApplyStatementsAndTrackDependencies(this)
				.ProcessAtVariantStatements(this)
				.ProcessFunctionsAndTrackDependencies(this)
				
				.InjectRootDependenciesCss(this)
				.InjectUtilityClassesCss(this);
			
			//return AppRunnerSettings.UseMinify ? sourceCss.ToString().CompactCss(workingSb) : sourceCss.ToString().ConsolidateLineBreaks(AppRunnerSettings.LineBreak.Contains('\r'), workingSb);
			return AppRunnerSettings.UseMinify ? sourceCss.ToString().CompactCss(workingSb) : sourceCss.ToString();
		}
		finally
		{
			AppState.StringBuilderPool.Return(sourceCss);
			AppState.StringBuilderPool.Return(workingSb);
		}
	}

    /// <summary>
    /// Gather root dependencies from all scanned file utility classes.
    /// </summary>
    /// <param name="appRunner"></param>
	public void ProcessScannedFileUtilityClassDependencies(AppRunner appRunner)
	{
		try
		{
			appRunner.UtilityClasses.Clear();
			appRunner.UsedCssCustomProperties.Clear();
			appRunner.UsedCss.Clear();

			foreach (var utilityClass in appRunner.ScannedFiles.SelectMany(scannedFile => scannedFile.Value.UtilityClasses))
			{
				appRunner.UtilityClasses.TryAdd(utilityClass.Key, utilityClass.Value);
						
				foreach (var dependency in utilityClass.Value.UsesCssCustomProperties)
				{
					if (dependency.StartsWith("--", StringComparison.Ordinal))
						appRunner.UsedCssCustomProperties.TryAdd(dependency, string.Empty);
					else
						appRunner.UsedCss.TryAdd(dependency, string.Empty);
				}
			}
		}
		catch (Exception e)
		{
			Messages.Add($"{AppState.CliErrorPrefix}ProcessScannedFileUtilityClassDependencies() - {e.Message}");
		}
	}
    
	#endregion
	
	#region Watching

	public async Task ShutDownWatchersAsync()
	{
		if (FileWatchers.Count == 0)
			return;

		while (FileWatchers.Count != 0)
		{
			try
			{
				FileWatchers[0].EnableRaisingEvents = false;
				FileWatchers[0].Dispose();
				FileWatchers.RemoveAt(0);
			}
			catch
			{
				await Task.Delay(25);
			}
		}
	}

	public async Task StartWatchingAsync()
	{
		await ShutDownWatchersAsync();
		
		FileWatchers.Add(await CreateFileChangeWatcherAsync(RestartAppQueue, AppRunnerSettings.NativeCssFilePathOnly, true));
			
		foreach (var projectPath in AppRunnerSettings.AbsolutePaths)
			FileWatchers.Add(await CreateFileChangeWatcherAsync(RebuildProjectQueue, projectPath, true));
	}
	
	/// <summary>
	/// Construct a file changes watcher.
	/// </summary>
	/// <param name="fileChangeQueue">scss or rebuild</param>
	/// <param name="projectPath">Path tree to watch for file changes</param>
	/// <param name="recurse">Also watch subdirectories</param>
	private static async Task<FileSystemWatcher> CreateFileChangeWatcherAsync(ConcurrentDictionary<long, FileSystemEventArgs> fileChangeQueue, string projectPath, bool recurse)
	{
		if (string.IsNullOrEmpty(projectPath))
		{
			await Console.Out.WriteLineAsync("Fatal Error: No watch path specified");
			Environment.Exit(1);
		}
        
		var watcher = new FileSystemWatcher(projectPath)
		{
			NotifyFilter = NotifyFilters.Attributes
			               | NotifyFilters.CreationTime
			               | NotifyFilters.FileName
			               | NotifyFilters.LastWrite
			               | NotifyFilters.Size
		};

		watcher.Changed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e);
		watcher.Created += async(_, e) => await AddChangeToQueueAsync(fileChangeQueue, e);
		watcher.Deleted += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e);
		watcher.Renamed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e);
        
		watcher.Filter = string.Empty;
		watcher.IncludeSubdirectories = recurse;
		watcher.EnableRaisingEvents = true;

		return watcher;
	}

	/// <summary>
	/// Add the change event to a queue for ordered processing.
	/// </summary>
	/// <param name="fileChangeQueue"></param>
	/// <param name="e"></param>
	private static async Task AddChangeToQueueAsync(ConcurrentDictionary<long, FileSystemEventArgs> fileChangeQueue, FileSystemEventArgs e)
	{
		var newKey = DateTimeOffset.UtcNow.UtcTicks;

		fileChangeQueue.TryAdd(newKey, e);

		await Task.CompletedTask;
	}

	private bool ImportChangesPending()
	{
		foreach (var fileInfo in AppRunnerSettings.Imports)
		{
			if (File.Exists(fileInfo.FullName) == false)
				return true;

			var currentFileInfo = new FileInfo(fileInfo.FullName);

			if (fileInfo.Length != currentFileInfo.Length || fileInfo.CreationTimeUtc != currentFileInfo.CreationTimeUtc || fileInfo.LastWriteTimeUtc != currentFileInfo.LastWriteTimeUtc)
				return true;
		}

		return false;
	}
	
	public async Task<bool> ProcessWatchQueues()
	{
		var performRebuild = ImportChangesPending();
		var performedWork = performRebuild;
		
		if (performRebuild || RestartAppQueue.IsEmpty == false)
		{
			var message = performRebuild ? "Import file(s) change, rebuilding..." : "Source CSS file changed, rebuilding...";

			if (RestartAppQueue.Any(kvp => kvp.Value.FullPath == AppRunnerSettings.NativeCssFilePath))
				performRebuild = true;

			if (File.Exists(AppRunnerSettings.NativeCssFilePath) == false && RestartAppQueue.Any(kvp => ((RenamedEventArgs)kvp.Value).OldFullPath == AppRunnerSettings.NativeCssFilePath))
			{
				var fcr = (RenamedEventArgs?)RestartAppQueue.LastOrDefault(kvp => ((RenamedEventArgs)kvp.Value).OldFullPath == AppRunnerSettings.NativeCssFilePath).Value;

				if (fcr is not null)
				{
					if (File.Exists(fcr.FullPath) == false)
					{
						await Console.Out.WriteLineAsync("Renamed source CSS file cannot be found, exiting");
						await Console.Out.WriteLineAsync($"({fcr.FullPath})");
						await Console.Out.WriteLineAsync("");

						await ShutDownWatchersAsync();

						Environment.Exit(1);
					}

					message = $"Renamed: {Path.GetFileName(fcr.OldFullPath)} => {Path.GetFileName(fcr.FullPath)}";
						
					_cssFilePath = _cssFilePath.TrimEnd(AppRunnerSettings.CssFileNameOnly) + Path.GetFileName(fcr.FullPath);
					
					Initialize();
				}
				
				performRebuild = true;
			}

			if (performRebuild)
			{
				if (File.Exists(AppRunnerSettings.NativeCssFilePath) == false)
				{
					await Console.Out.WriteLineAsync("Source CSS file cannot be found, exiting");
					await Console.Out.WriteLineAsync($"({AppRunnerSettings.NativeCssFilePath})");
					await Console.Out.WriteLineAsync("");

					Environment.Exit(1);
				}

				await ShutDownWatchersAsync();

				RebuildProjectQueue.Clear();

				await AddCssPathMessageAsync();
				await AddMessageAsync(message);

				await ReInitializeAsync();
				await PerformFileScanAsync();
				await BuildAndSaveCss();
				await StartWatchingAsync();
				
				if (performedWork == false)
					performedWork = true;
			}
			
			RestartAppQueue.Clear();
		}

		if (RebuildProjectQueue.IsEmpty == false)
		{
			var messages = new List<string>();
			
			foreach (var kvp in RebuildProjectQueue.OrderBy(k => k.Key))
			{
				var fcr = kvp.Value;
				var extension = Path.GetExtension(fcr.Name) ?? string.Empty;

				if (string.IsNullOrEmpty(extension))
					continue;

				if (Library.InvalidFileExtensions.Any(e => e == extension))
					continue;

				if (Library.ValidFileExtensions.Any(e => e == extension) == false)
					continue;

				var pathOnly = fcr.FullPath.TrimEnd(fcr.Name) ?? string.Empty;

				if (string.IsNullOrEmpty(pathOnly))
					continue;

				if (AppRunnerSettings.AbsoluteNotPaths.Any(s => pathOnly.StartsWith(s, StringComparison.Ordinal)))
					continue;

				if (AppRunnerSettings.NotFolderNames.Any(s => pathOnly.Contains($"{Path.DirectorySeparatorChar}{s}{Path.DirectorySeparatorChar}", StringComparison.Ordinal)))
					continue;

				FileScanStopwatch.Restart();

				// ReSharper disable once ConvertIfStatementToSwitchStatement
				if (fcr.ChangeType is WatcherChangeTypes.Changed or WatcherChangeTypes.Created)
				{
					if (ScannedFiles.TryGetValue(fcr.FullPath, out var scannedFile))
					{
						await scannedFile.LoadAndScanFileAsync(this);
					}
					else
					{
						var newScannedFile = new ScannedFile(fcr.FullPath);

						await newScannedFile.LoadAndScanFileAsync(this);

						ScannedFiles.TryAdd(fcr.FullPath, newScannedFile);
					}

					messages.Add(fcr.ChangeType is WatcherChangeTypes.Changed
						? $"Changed : {Path.GetFileName(fcr.FullPath)}"
						: $"Added : {Path.GetFileName(fcr.FullPath)}");
				}
				else if (fcr.ChangeType is WatcherChangeTypes.Deleted)
				{
					ScannedFiles.Remove(fcr.FullPath, out _);
					
					messages.Add($"Deleted : {Path.GetFileName(fcr.FullPath)}");
				}				
				else if (fcr.ChangeType is WatcherChangeTypes.Renamed)
				{
					var renamedEventArgs = (RenamedEventArgs) fcr;
					var scannedFile = new ScannedFile(renamedEventArgs.FullPath);
					
					await scannedFile.LoadAndScanFileAsync(this);
					
					ScannedFiles.TryAdd(renamedEventArgs.FullPath, scannedFile);
					ScannedFiles.Remove(renamedEventArgs.OldFullPath, out _);

					messages.Add($"Renamed : {Path.GetFileName(renamedEventArgs.OldFullPath)} => {Path.GetFileName(renamedEventArgs.FullPath)}");
				}
			}

			RebuildProjectQueue.Clear();

			if (messages.Count == 0)
				return performedWork;
			
			await AddCssPathMessageAsync();

			foreach (var message in messages)
				await AddMessageAsync(message);				
				
			ProcessScannedFileUtilityClassDependencies(this);

			await AddMessageAsync($"Processed changes in {FileScanStopwatch.FormatTimer()}");
			await BuildAndSaveCss();
				
			if (performedWork == false)
				performedWork = true;
		}

		return performedWork;
	}
	
	#endregion
}
