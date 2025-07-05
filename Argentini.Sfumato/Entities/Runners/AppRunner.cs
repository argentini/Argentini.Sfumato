namespace Argentini.Sfumato.Entities.Runners;

public sealed class AppRunner
{
	#region Properties

	public Stopwatch CssBuildStopwatch { get; } = new();
	public Stopwatch FileScanStopwatch { get; } = new();
	public string LastCss { get; set; } = string.Empty;
	public List<string> Messages { get; } = [];
	public bool MessagesBusy { get; set; }
	public bool ProcessingWatchQueue { get; set; }
	public bool IsFirstRun { get; set; } = true;
	
	public AppState AppState { get; }
	public Library.Library Library { get; set; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; } = new(null);
	public ConcurrentDictionary<string,ScannedFile> ScannedFiles { get; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,string> UsedCssCustomProperties { get; } = new(StringComparer.Ordinal);
	public ConcurrentDictionary<string,string> UsedCss { get; } = new(StringComparer.Ordinal);
	public Dictionary<string,CssClass> UtilityClasses { get; } = new(StringComparer.Ordinal);

	private string _cssFilePath;
	private readonly bool _useMinify;

	private List<FileSystemWatcher> FileWatchers { get; } = [];
	private ConcurrentDictionary<long, FileSystemEventArgs> RestartAppQueue { get; } = [];
	private ConcurrentDictionary<long, FileSystemEventArgs> RebuildProjectQueue { get; } = [];

	#region CSS Generation Data

	public StringBuilder DefaultsCss { get; }
	public StringBuilder BrowserResetCss { get; }
	public StringBuilder FormsCss { get; }

	public StringBuilder ImportsCssSegment { get; } = new();
	public StringBuilder SfumatoSegment { get; } = new();
	public StringBuilder ComponentsCssSegment { get; } = new();
	public StringBuilder CustomCssSegment { get; } = new();
	public StringBuilder UtilitiesCssSegment { get; } = new();
	public StringBuilder ThemeCssSegment { get; } = new();
	public StringBuilder PropertiesCssSegment { get; } = new();
	public StringBuilder PropertyListCssSegment { get; } = new();
	
	#endregion

	#endregion

	#region Construction

	public AppRunner(AppState appState, string cssFilePath = "", bool useMinify = false)
	{
		AppState = appState;

		_cssFilePath = cssFilePath;
		_useMinify = useMinify;

		BrowserResetCss = new StringBuilder(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "browser-reset.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak).Trim());
		FormsCss = new StringBuilder(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "forms.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak).Trim());
		DefaultsCss = new StringBuilder(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "defaults.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak).Trim());

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
		    Library = new Library.Library();
		    
		    AppRunnerSettings = new AppRunnerSettings(this)
		    {
			    CssFilePath = _cssFilePath,
			    UseMinify = _useMinify
		    };

		    DefaultsCss.ExtractSfumatoBlock(this);
		    AppRunnerExtensions.ImportSfumatoBlockSettingsItems(this);
		    AppRunnerExtensions.ProcessSfumatoBlockSettings(this, true);
	    }
	    catch (Exception e)
	    {
		    Messages.Add($"{AppState.CliErrorPrefix}Initialize() - {e.Message}");
	    }
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

	private async Task AddMessageAsync(string message)
	{
		while (MessagesBusy)
			await Task.Delay(25);

		Messages.Add(message);
	}

	private async Task RenderMessagesAsync()
	{
		while (MessagesBusy)
			await Task.Delay(25);

		MessagesBusy = true;

		if (IsFirstRun == false)
			Messages.Insert(0, Strings.DotLine.Repeat(Entities.Library.Library.MaxConsoleWidth));

		Program.Dispatcher.Post(this);

		while (Messages.Count != 0)
			await Task.Delay(25);
			
		MessagesBusy = false;

		if (IsFirstRun)
			IsFirstRun = false;
	}

	#endregion
	
	#region File Scanning
	
	public async Task ReInitializeAsync()
	{
		Initialize();
		await this.LoadCssFileAsync();
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
			if (AppRunnerSettings.AbsoluteNotPaths.Any(s => s.Equals(subDir.FullName + Path.DirectorySeparatorChar, StringComparison.Ordinal)))
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

		LastCss = AppRunnerSettings.CssContent.BuildCss(this);

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

				.InjectUtilityClassesCss(this)
				.ProcessAtVariants(this)

				.ProcessFunctionsAndTrackDependencies(this)
				.InjectRootDependenciesCss(this)
				.MoveComponentsLayer(this);

			if (AppRunnerSettings.UseDarkThemeClasses)
				sourceCss.ProcessDarkThemeClasses(this);
			
			return AppRunnerSettings.UseMinify ? sourceCss.ToString().CompactCss(workingSb) : sourceCss.ReformatCss(workingSb).ToString().NormalizeLinebreaks(AppRunnerSettings.LineBreak);
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
				appRunner.UtilityClasses.TryAdd(utilityClass.Key, utilityClass.Value);
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
		
		FileWatchers.Add(await CreateFileChangeWatcherAsync(RestartAppQueue, AppRunnerSettings.NativeCssFilePathOnly, true, this));
			
		foreach (var projectPath in AppRunnerSettings.AbsolutePaths)
			FileWatchers.Add(await CreateFileChangeWatcherAsync(RebuildProjectQueue, projectPath, true, this));
	}

	/// <summary>
	/// Construct a file changes watcher.
	/// </summary>
	/// <param name="fileChangeQueue">scss or rebuild</param>
	/// <param name="projectPath">Path tree to watch for file changes</param>
	/// <param name="recurse">Also watch subdirectories</param>
	/// <param name="appRunner"></param>
	private static async Task<FileSystemWatcher> CreateFileChangeWatcherAsync(ConcurrentDictionary<long, FileSystemEventArgs> fileChangeQueue, string projectPath, bool recurse, AppRunner appRunner)
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

		watcher.Changed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, appRunner);
		watcher.Created += async(_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, appRunner);
		watcher.Deleted += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, appRunner);
		watcher.Renamed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, appRunner);
        
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
	/// <param name="appRunner"></param>
	private static async Task AddChangeToQueueAsync(ConcurrentDictionary<long, FileSystemEventArgs> fileChangeQueue, FileSystemEventArgs e, AppRunner appRunner)
	{
		while (appRunner.ProcessingWatchQueue)
			await Task.Delay(25);
		
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
		while (ProcessingWatchQueue)
			await Task.Delay(25);

		ProcessingWatchQueue = true;
		
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

			var rebuildProjectQueue = RebuildProjectQueue.ToList();
			
			foreach (var kvp in rebuildProjectQueue.OrderBy(k => k.Key))
			{
				var fcr = kvp.Value;
				var extension = Path.GetExtension(fcr.Name) ?? string.Empty;

				if (string.IsNullOrEmpty(extension))
					continue;

				if (Library.InvalidFileExtensions.Any(e => e == extension))
					continue;

				if (Library.ValidFileExtensions.Any(e => e == extension) == false)
					continue;

				var pathOnly = string.IsNullOrEmpty(extension) ? fcr.FullPath : fcr.FullPath.TrimEnd(Path.GetFileName(fcr.FullPath)) ?? string.Empty;

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

			if (rebuildProjectQueue.Count == RebuildProjectQueue.Count)
				RebuildProjectQueue.Clear();
			else
				foreach (var item in rebuildProjectQueue)
					RebuildProjectQueue.Remove(item.Key, out _);

			if (messages.Count == 0)
			{
				ProcessingWatchQueue = false;

				return performedWork;
			}

			await AddCssPathMessageAsync();

			foreach (var message in messages)
				await AddMessageAsync(message);				
				
			ProcessScannedFileUtilityClassDependencies(this);

			await AddMessageAsync($"Processed changes in {FileScanStopwatch.FormatTimer()}");
			await BuildAndSaveCss();
				
			if (performedWork == false)
				performedWork = true;
		}

		ProcessingWatchQueue = false;

		return performedWork;
	}
	
	#endregion
}
