using Argentini.Sfumato.Entities.SfumatoSettings;

namespace Argentini.Sfumato;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var cancellationTokenSource = new CancellationTokenSource();
		
		var totalTimer = new Stopwatch();

		totalTimer.Start();
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var runner = new SfumatoRunner();
		var version = await Identify.VersionAsync(System.Reflection.Assembly.GetExecutingAssembly());

		await runner.InitializeAsync(args);

		if (runner.AppState.VersionMode)
		{
			await Console.Out.WriteLineAsync($"Sfumato Version {version}");
			Environment.Exit(0);
		}
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(SfumatoRunner.MaxConsoleWidth));
		await Console.Out.WriteLineAsync("Sfumato: The lean, modern, utility-based SCSS/CSS framework generation tool");
		await Console.Out.WriteLineAsync($"Version {version} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()}) / {runner.AppState.UtilityClassCollection.Count:N0} Utility Classes");
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(SfumatoRunner.MaxConsoleWidth));
		
		if (runner.AppState.HelpMode)
		{
			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync("Usage:");
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Usage:".Length));
			await Console.Out.WriteLineAsync("sfumato [help|version]");
			await Console.Out.WriteLineAsync("sfumato [build|watch] [options]");
			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync("Commands:");
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Commands:".Length));
			await Console.Out.WriteLineAsync("build     : Perform a single complete build");
			await Console.Out.WriteLineAsync("watch     : Perform a complete build and then watch for changes");
			await Console.Out.WriteLineAsync("version   : Show the sfumato version number");
			await Console.Out.WriteLineAsync("help      : Show this help message");
			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync("* build and watch commands look in the current path for a `sfumato.json`");
			await Console.Out.WriteLineAsync("  settings file unless using the `--path` option; visit https://sfumato.app");
			await Console.Out.WriteLineAsync("  for more information on creating a sfumato.json settings file");
			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync("Options:");
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Options:".Length));
			await Console.Out.WriteLineAsync("--path    : Follow with a relative or absolute path to your sfumato.json");
			await Console.Out.WriteLineAsync("            settings file (e.g. `sfumato watch --path Code/MyProject`)");
			await Console.Out.WriteLineAsync("--minify  : Minify CSS output; use with build and watch commands");
			await Console.Out.WriteLineAsync();

			Environment.Exit(0);
		}

		await Console.Out.WriteLineAsync($"Theme Mode       :  {(runner.AppState.Settings.DarkMode.Equals("media", StringComparison.OrdinalIgnoreCase) ? "OS Dark/Light Mode" : "CSS Classes")}");
		await Console.Out.WriteLineAsync($"Transpile        :  {(runner.AppState.Minify ? "Minify" : "Expanded")}");
		await Console.Out.WriteLineAsync($"Project Path     :  {runner.AppState.WorkingPath}");

		if (runner.AppState.Settings.ProjectPaths.Count > 0)
		{
			var paths = runner.AppState.StringBuilderPool.Get();
	        
			foreach (var path in runner.AppState.Settings.ProjectPaths)
			{
				if (paths.Length > 0)
					paths.Append("                 :  ");

				paths.Append($".{path.Path.SetNativePathSeparators().TrimStart(runner.AppState.WorkingPath).TrimEndingPathSeparators()}{Path.DirectorySeparatorChar}{path.FileSpec}{Environment.NewLine}");
			}
	        
			await Console.Out.WriteLineAsync($"Watch Path(s)    :  {paths.ToString().TrimEnd()}");
			
			runner.AppState.StringBuilderPool.Return(paths);
		}        

		await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat(SfumatoRunner.MaxConsoleWidth));

		if (runner.AppState.DiagnosticMode)
			runner.AppState.DiagnosticOutput.TryAdd("init000", $"Initialized app in {totalTimer.FormatTimer()}{Environment.NewLine}");
		
		totalTimer.Restart();

		await Console.Out.WriteLineAsync($"Started build at {DateTime.Now:HH:mm:ss.fff}");

		await runner.AppState.GatherWatchedFilesAsync();
        await runner.PerformCoreBuildAsync(totalTimer);

		#region Watcher Mode

		if (runner.AppState.WatchMode)
		{
			var fileWatchers = new List<FileSystemWatcher>();
			var restartAppQueue = new ConcurrentDictionary<long, FileChangeRequest>();
			var rebuildProjectQueue = new ConcurrentDictionary<long, FileChangeRequest>();
			var scssTranspileQueue = new ConcurrentDictionary<long, FileChangeRequest>();

			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync($"Started watching for changes at {DateTime.Now:HH:mm:ss.fff}");
			await Console.Out.WriteLineAsync();
			
			#region Create Watchers
			
			fileWatchers.Add(await CreateFileChangeWatcherAsync(restartAppQueue, new ProjectPath
			{
				Path = runner.AppState.SettingsFilePath.TrimEnd("sfumato.json") ?? string.Empty,
				FileSpec = "sfumato.json"
				
			}, false));
			
			foreach (var projectPath in runner.AppState.Settings.ProjectPaths)
			{
				if (projectPath.FileSpec.EndsWith(".scss", StringComparison.OrdinalIgnoreCase))
					fileWatchers.Add(await CreateFileChangeWatcherAsync(scssTranspileQueue, projectPath, projectPath.Recurse));
				else
					fileWatchers.Add(await CreateFileChangeWatcherAsync(rebuildProjectQueue, projectPath, projectPath.Recurse));
			}
			
			#endregion

			#region Watch

			var watchTimer = new Stopwatch();
			
			while (Console.KeyAvailable == false && cancellationTokenSource.IsCancellationRequested == false)
			{
				var processedFiles = false;

				await Task.Delay(25, cancellationTokenSource.Token);

				watchTimer.Restart();

				if (rebuildProjectQueue.IsEmpty == false)
				{
					await Console.Out.WriteLineAsync($"Project file changes detected at {DateTime.Now:HH:mm:ss.fff}");

					foreach (var fileChangeRequest in rebuildProjectQueue.OrderBy(f => f.Key))
					{
						if (fileChangeRequest.Value.FileSystemEventArgs is null)
							break;

						var eventArgs = fileChangeRequest.Value.FileSystemEventArgs;
						var filePath = SfumatoRunner.ShortenPathForOutput(eventArgs.FullPath, runner.AppState);
						
						if (eventArgs.ChangeType == WatcherChangeTypes.Deleted)
						{
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Deleted {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.DeleteWatchedFileAsync(eventArgs.FullPath);
						}

						else if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
						{
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Updated {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.AddUpdateWatchedFileAsync(eventArgs.FullPath, cancellationTokenSource);
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Created)
						{
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Added {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.AddUpdateWatchedFileAsync(eventArgs.FullPath, cancellationTokenSource);
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Renamed)
						{
							var renamedEventArgs = (RenamedEventArgs) eventArgs;
							var oldFilePath = SfumatoRunner.ShortenPathForOutput(renamedEventArgs.OldFullPath, runner.AppState);

							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Renamed {oldFilePath} => {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.DeleteWatchedFileAsync(renamedEventArgs.OldFullPath);
							
							if (renamedEventArgs.Name?.EndsWith(fileChangeRequest.Value.FileSpec.TrimStart('*')) ?? false)
								await runner.AddUpdateWatchedFileAsync(renamedEventArgs.FullPath, cancellationTokenSource);
						}
					}					
					
					rebuildProjectQueue.Clear();

					var timer = new Stopwatch();

					timer.Start();
					
					await runner.AppState.ExamineWatchedFilesForUsedClassesAsync();
					await runner.PerformCoreBuildAsync(timer, true);

					processedFiles = true;
				}

				else if (scssTranspileQueue.IsEmpty == false)
				{
					foreach (var fileChangeRequest in scssTranspileQueue.OrderBy(f => f.Key))
					{
						if (fileChangeRequest.Value.FileSystemEventArgs is null)
							break;

						var eventArgs = fileChangeRequest.Value.FileSystemEventArgs;

						if (Path.GetFileName(eventArgs.FullPath).StartsWith('_'))
							continue;
						
						var filePath = SfumatoRunner.ShortenPathForOutput(eventArgs.FullPath, runner.AppState);

						if (eventArgs.ChangeType == WatcherChangeTypes.Deleted)
						{
							await Console.Out.WriteLineAsync($"Deleted {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.DeleteWatchedScssFileAsync(eventArgs.FullPath);
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Deleted {filePath.TrimEnd(".scss")}.css/map");
						}

						else if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
						{
							await Console.Out.WriteLineAsync($"Updated {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.AddUpdateWatchedScssFileAsync(eventArgs.FullPath, cancellationTokenSource);
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Created)
						{
							await Console.Out.WriteLineAsync($"Added {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.AddUpdateWatchedScssFileAsync(eventArgs.FullPath, cancellationTokenSource);
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Renamed)
						{
							var renamedEventArgs = (RenamedEventArgs) eventArgs;
							var oldFilePath = SfumatoRunner.ShortenPathForOutput(renamedEventArgs.OldFullPath, runner.AppState);
							
							await Console.Out.WriteLineAsync($"Renamed {oldFilePath} => {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.DeleteWatchedScssFileAsync(renamedEventArgs.OldFullPath);
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Deleted {oldFilePath.TrimEnd(".scss")}.css");

							if (renamedEventArgs.Name?.EndsWith(".scss") ?? false)
								await runner.AddUpdateWatchedScssFileAsync(renamedEventArgs.FullPath, cancellationTokenSource);
						}
					}

					scssTranspileQueue.Clear();
					
					processedFiles = true;
				}
				
				else if (restartAppQueue.IsEmpty == false)
				{
					await Console.Out.WriteLineAsync($"Modified sfumato.json at {DateTime.Now:HH:mm:ss.fff}");
					await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Reload settings and rebuild");

					var timer = new Stopwatch();

					timer.Start();

					await runner.InitializeAsync(args);
					await runner.AppState.GatherWatchedFilesAsync();
					await runner.PerformCoreBuildAsync(timer);
					
					restartAppQueue.Clear();

					processedFiles = true;
				}
				
				if (processedFiles)
				{
					await Console.Out.WriteLineAsync();
					await Console.Out.WriteLineAsync("Watching; Press ESC to Exit");
					await Console.Out.WriteLineAsync();
				}

				if (cancellationTokenSource.IsCancellationRequested)
					break;

				if (Console.KeyAvailable == false)
					continue;
				
				var keyPress = Console.ReadKey(intercept: true);

				if (keyPress.Key != ConsoleKey.Escape)
					continue;
				
				await cancellationTokenSource.CancelAsync();
				
				break;
			}
			
			#endregion

			#region Cleanup Watchers
			
			foreach (var watcher in fileWatchers)
				watcher.Dispose();

			#endregion
			
			await Console.Out.WriteLineAsync($"Total watch time {TimeSpan.FromMilliseconds(totalTimer.ElapsedMilliseconds).FormatTimer()}");
		}

		#endregion
		
		if (runner.AppState.DiagnosticMode)
		{
			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync("DIAGNOSTICS:");
			await Console.Out.WriteLineAsync(string.Join(string.Empty, runner.AppState.DiagnosticOutput.OrderBy(d => d.Key).Select(v => v.Value)));
		}

		Environment.Exit(0);
	}
	
    /// <summary>
    /// Construct a file changes watcher.
    /// </summary>
    /// <param name="fileChangeQueue">scss or rebuild</param>
    /// <param name="projectPath">Path tree to watch for file changes</param>
    /// <param name="recurse">Also watch subdirectories</param>
    private static async Task<FileSystemWatcher> CreateFileChangeWatcherAsync(ConcurrentDictionary<long, FileChangeRequest> fileChangeQueue, ProjectPath projectPath, bool recurse)
    {
	    if (string.IsNullOrEmpty(projectPath.Path))
	    {
		    await Console.Out.WriteLineAsync("Fatal Error: No watch path specified");
		    Environment.Exit(1);
	    }

	    var filePath = projectPath.Path;
	    var fileSpec = projectPath.FileSpec;
	    
	    if (string.IsNullOrEmpty(fileSpec))
	    {
		    await Console.Out.WriteLineAsync("Fatal Error: No watch filespec specified");
		    Environment.Exit(1);
	    }

	    var watcher = new FileSystemWatcher(filePath)
        {
            NotifyFilter = NotifyFilters.Attributes
                           | NotifyFilters.CreationTime
                           | NotifyFilters.FileName
                           | NotifyFilters.LastWrite
                           | NotifyFilters.Size
        };

        watcher.Changed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, fileSpec);
        watcher.Created += async(_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, fileSpec);
        watcher.Deleted += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, fileSpec);
        watcher.Renamed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, fileSpec);
        
        watcher.Filter = fileSpec;
        watcher.IncludeSubdirectories = recurse;
        watcher.EnableRaisingEvents = true;

        return watcher;
    }

    /// <summary>
    /// Add the change event to a queue for ordered processing.
    /// </summary>
    /// <param name="fileChangeQueue"></param>
    /// <param name="e"></param>
    /// <param name="fileSpec"></param>
    private static async Task AddChangeToQueueAsync(ConcurrentDictionary<long, FileChangeRequest> fileChangeQueue, FileSystemEventArgs e, string fileSpec)
    {
	    if (string.IsNullOrEmpty(fileSpec))
	    {
		    await Console.Out.WriteLineAsync("Fatal Error: No watch filespec specified");
		    Environment.Exit(1);
	    }
	    
	    var newKey = DateTimeOffset.UtcNow.UtcTicks;

	    fileChangeQueue.AddOrUpdate(newKey, new FileChangeRequest
	    {
		    FileSpec = fileSpec,
		    FileSystemEventArgs = e

	    }, (_, _) => new FileChangeRequest
	    {
		    FileSpec = fileSpec,
		    FileSystemEventArgs = e
	    });
    }
}

public class FileChangeRequest
{
    public string FileSpec { get; set; } = string.Empty;
    public FileSystemEventArgs? FileSystemEventArgs { get; set; }
}