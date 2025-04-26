// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable InvertIf

using System.Reflection;

namespace Argentini.Sfumato.Entities.Runners;

public class AppRunner
{
	#region Services

	static WeakMessenger Messenger = new ();
	
	#endregion
	
	#region Properties

	public Stopwatch Stopwatch { get; set; } = new();
	public Stopwatch TotalStopwatch { get; set; } = new();
	public Stopwatch WorkingStopwatch { get; set; } = new();
	public string LastCss { get; set; } = string.Empty;
	public List<string> Messages { get; set; } = [];
	
	public AppState AppState { get; }
	public Library.Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; } = new(null);
	public ConcurrentDictionary<string,ScannedFile> ScannedFiles { get; set; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,string> UsedCssCustomProperties { get; set; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,string> UsedCss { get; set; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,CssClass> UtilityClasses { get; set; } = new(StringComparer.Ordinal);

	private readonly string _cssFilePath;
	private readonly bool _useMinify;
	
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
    public void Initialize()
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

	public async Task PerformFullBuild(bool reInitialize = false)
	{
		var tasks = new ConcurrentBag<Task>();

		ScannedFiles.Clear();

		if (reInitialize)
		{
			Initialize();
			await LoadCssFileAsync();
		}

		TotalStopwatch.Restart();
		WorkingStopwatch.Restart();

		foreach (var path in AppRunnerSettings.AbsolutePaths)
			tasks.Add(RecurseProjectPathAsync(path, tasks));

		await Task.WhenAll(tasks);
		
		WorkingStopwatch.Stop();
		Stopwatch.Restart();

		LastCss = BuildCss();

		await File.WriteAllTextAsync(AppRunnerSettings.NativeCssOutputFilePath, LastCss);

		Stopwatch.Stop();
		TotalStopwatch.Stop();

		var relativePath = Path.GetFullPath(AppRunnerSettings.CssFilePath).TruncateCenter((int)Math.Floor(Entities.Library.Library.MaxConsoleWidth / 3d), (int)Math.Floor((Entities.Library.Library.MaxConsoleWidth / 3d) * 2) - 3, Entities.Library.Library.MaxConsoleWidth);
			
		Messages.Add(relativePath);

		Messages.Add($"Found {ScannedFiles.Count:N0} file{(ScannedFiles.Count == 1 ? string.Empty : "s")}, {UtilityClasses.Count:N0} class{(UtilityClasses.Count == 1 ? string.Empty : "es")} in {WorkingStopwatch.FormatTimer()}");
		Messages.Add($"{LastCss.Length.FormatBytes()} written to {AppRunnerSettings.CssOutputFilePath} in {Stopwatch.FormatTimer()}");
		Messages.Add($"Build complete at {DateTime.Now:HH:mm:ss.fff} ({TotalStopwatch.FormatTimer()})");
		Messages.Add(Strings.DotLine.Repeat(Entities.Library.Library.MaxConsoleWidth));
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
	
	public async Task StartWatching()
	{
		// todo: watchers

		await Task.CompletedTask;
	}
	
	#region CSS Generation
    
	/// <summary>
	/// Generate the final CSS output.
	/// </summary>
	/// <returns></returns>
    public string BuildCss()
	{
		var sourceCss = AppState.StringBuilderPool.Get();
		var workingSb = AppState.StringBuilderPool.Get();

		try
		{
			ProcessScannedFileUtilityClassDependencies(this);

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
}
