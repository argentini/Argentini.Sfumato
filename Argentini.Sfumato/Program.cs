namespace Argentini.Sfumato;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var fileWatchers = new List<FileSystemWatcher>();
		var rebuildProjectQueue = new ConcurrentDictionary<long, FileChangeRequest>();
		var scssTranspileQueue = new ConcurrentDictionary<long, FileChangeRequest>();
		var cancellationTokenSource = new CancellationTokenSource();
		
		var totalTimer = new Stopwatch();

		totalTimer.Start();
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var runner = new SfumatoRunner();

		await runner.InitializeAsync(args);

		if (runner.AppState.VersionMode)
		{
			Console.WriteLine($"Sfumato Version {Identify.Version(Assembly.GetExecutingAssembly())}");
			Environment.Exit(0);
		}

		Console.WriteLine(Strings.ThickLine.Repeat(SfumatoRunner.MaxConsoleWidth));
		Console.WriteLine("Sfumato: The lean, modern, utility-based SCSS/CSS framework generation tool");
		Console.WriteLine($"Version {Identify.Version(Assembly.GetExecutingAssembly())} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()}) / {runner.AppState.ScssClassCollection.AllClasses.Count:N0} Core Classes");
		
		Console.WriteLine(Strings.ThickLine.Repeat(SfumatoRunner.MaxConsoleWidth));
		
		if (runner.AppState.HelpMode)
		{
			Console.WriteLine();
			Console.WriteLine("Usage: sfumato [options]");
			Console.WriteLine();
			Console.WriteLine("Options:");
			Console.WriteLine();
			Console.WriteLine("--help      Show this help message");
			Console.WriteLine("--version   Show the sfumato version number");
			Console.WriteLine("--release   Build CSS in release mode");
			Console.WriteLine("--watch     Remain active and rebuild CSS whenever a project file changes");
			Console.WriteLine("--path      Follow with a relative or absolute path to your sfumato.json file");
			Console.WriteLine("            (e.g. Development/MyProject)");
			Console.WriteLine();

			Environment.Exit(0);
		}

		Console.WriteLine($"Build Mode       :  {(runner.AppState.ReleaseMode ? "Release" : "Development")}");
		Console.WriteLine($"Theme Mode       :  {(runner.AppState.Settings.ThemeMode.Equals("system", StringComparison.OrdinalIgnoreCase) ? "System" : "CSS Class")}");
		Console.WriteLine($"Project Path     :  {runner.AppState.WorkingPath}");
		Console.WriteLine($"CSS Output Path  :  .{runner.AppState.Settings.CssOutputPath.TrimStart(runner.AppState.WorkingPath).TrimEndingPathSeparators()}{Path.DirectorySeparatorChar}sfumato.css");

		if (runner.AppState.Settings.ProjectPaths.Count > 0)
		{
			var paths = runner.AppState.StringBuilderPool.Get();
	        
			foreach (var path in runner.AppState.Settings.ProjectPaths)
			{
				if (paths.Length > 0)
					paths.Append("                 :  ");

				if (path.IsFilePath)
				{
					paths.Append($".{path.Path.SetNativePathSeparators().TrimStart(runner.AppState.WorkingPath)}");

					if (path.Path.EndsWith(".scss", StringComparison.OrdinalIgnoreCase))
						paths.Append(" (transpile in-place)");
					else
						paths.Append(" (audit used classes)");
				}

				else
				{
					paths.Append($".{path.Path.SetNativePathSeparators().TrimStart(runner.AppState.WorkingPath)}{Path.DirectorySeparatorChar}{path.FileSpec}");
					
					if (path.FileSpec.EndsWith(".scss", StringComparison.OrdinalIgnoreCase))
						paths.Append(" (transpile in-place)");
					else
						paths.Append(" (audit used classes)");
				}

				paths.Append($"{Environment.NewLine}");
			}
	        
			Console.WriteLine($"Watch Path(s)    :  {paths.ToString().TrimEnd()}");
			
			runner.AppState.StringBuilderPool.Return(paths);
		}        

		Console.WriteLine(Strings.ThinLine.Repeat(SfumatoRunner.MaxConsoleWidth));

		Console.WriteLine($"Started {(runner.AppState.WatchMode ? "initial build" : "build")} at {DateTime.Now:HH:mm:ss.fff}");

		var timer = new Stopwatch();

		timer.Start();
		
        await runner.PerformBuildAsync();

        Console.WriteLine($"Completed {(runner.AppState.WatchMode ? "initial build" : "build")} in {timer.FormatTimer()} at {DateTime.Now:HH:mm:ss.fff}");
        
		#region Watcher Mode

		if (runner.AppState.WatchMode)
		{
			Console.WriteLine();
			Console.WriteLine($"Started Watching at {DateTime.Now:HH:mm:ss.fff}");
			Console.WriteLine();
			
			#region Create Watchers
			
			foreach (var projectPath in runner.AppState.Settings.ProjectPaths)
			{
				if (projectPath.FileSpec.EndsWith(".scss", StringComparison.OrdinalIgnoreCase))
					fileWatchers.Add(CreateFileChangeWatcher(scssTranspileQueue, projectPath));
				else
					fileWatchers.Add(CreateFileChangeWatcher(rebuildProjectQueue, projectPath));
			}
			
			#endregion

			#region Watch

			var watchTimer = new Stopwatch();
			
			while (Console.KeyAvailable == false && cancellationTokenSource.IsCancellationRequested == false)
			{
				var processedFiles = false;

				await Task.Delay(100, cancellationTokenSource.Token);

				watchTimer.Restart();

				if (rebuildProjectQueue.IsEmpty == false)
				{
					Console.WriteLine($"Started rebuild at {DateTime.Now:HH:mm:ss.fff}");

					rebuildProjectQueue.Clear();
					scssTranspileQueue.Clear();

					await runner.PerformBuildAsync();
					processedFiles = true;
				}

				else if (scssTranspileQueue.IsEmpty == false)
				{
					Console.WriteLine($"Started transpile at {DateTime.Now:HH:mm:ss.fff}");

					foreach (var fileChangeRequest in scssTranspileQueue.OrderBy(f => f.Key))
					{
						if (fileChangeRequest.Value.ChangeType.Equals("deleted", StringComparison.OrdinalIgnoreCase))
						{
							var cssFilePath =
								fileChangeRequest.Value.FilePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) +
								".css"; 
							
							if (File.Exists(cssFilePath))
								File.Delete(cssFilePath);
						}

						else
						{
							var length = await SfumatoScss.TranspileSingleScss(fileChangeRequest.Value.FilePath, runner.AppState);
							
							Console.WriteLine($"{Strings.TriangleRight} Generated {SfumatoRunner.ShortenPathForOutput(fileChangeRequest.Value.FilePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) + ".css", runner.AppState)} ({length.FormatBytes()})");
						}
						
						while (scssTranspileQueue.TryRemove(fileChangeRequest.Key, out _) == false)
						{
							await Task.Delay(10, cancellationTokenSource.Token);
						}
					}

					processedFiles = true;
				}
				
				if (processedFiles)
				{
					Console.WriteLine($"Completed build in {watchTimer.FormatTimer()} at {DateTime.Now:HH:mm:ss.fff}");
					Console.WriteLine();
					Console.WriteLine("Watching; Press ESC to Exit");
					Console.WriteLine();
				}

				if (cancellationTokenSource.IsCancellationRequested)
					break;

				if (Console.KeyAvailable == false)
					continue;
				
				var keyPress = Console.ReadKey(intercept: true);

				if (keyPress.Key != ConsoleKey.Escape)
					continue;
				
				cancellationTokenSource.Cancel();
				
				break;
			}
			
			#endregion

			#region Cleanup Watchers
			
			foreach (var watcher in fileWatchers)
				watcher.Dispose();

			#endregion
			
			Console.WriteLine($"Total watch time {TimeSpan.FromMilliseconds(totalTimer.ElapsedMilliseconds).FormatTimer()}");
		}

		#endregion
		
		if (runner.AppState.DiagnosticMode)
		{
			Console.WriteLine();
			Console.WriteLine("DIAGNOSTICS:");
			Console.WriteLine(runner.AppState.DiagnosticOutput.ToString());
		}

		Environment.Exit(0);
	}
	
    /// <summary>
    /// Construct a file changes watcher.
    /// </summary>
    /// <param name="fileChangeQueue">scss or rebuild</param>
    /// <param name="projectPath">Path tree to watch for file changes</param>
    /// <param name="fileSpec">File type to watch (e.g. *.scss)</param>
    private static FileSystemWatcher CreateFileChangeWatcher(ConcurrentDictionary<long, FileChangeRequest> fileChangeQueue, ProjectPath projectPath)
    {
	    if (string.IsNullOrEmpty(projectPath.Path))
	    {
		    Console.WriteLine("Fatal Error: No watch path specified");
		    Environment.Exit(1);
	    }

	    var filePath = projectPath.Path;
	    var fileSpec = projectPath.FileSpec;
	    
	    if (projectPath.IsFilePath)
	    {
		    fileSpec = projectPath.Path[(projectPath.Path.LastIndexOf(Path.DirectorySeparatorChar) + 1)..];
		    filePath = projectPath.Path[..projectPath.Path.LastIndexOf(Path.DirectorySeparatorChar)];
	    }
	    
	    if (string.IsNullOrEmpty(fileSpec))
	    {
		    Console.WriteLine("Fatal Error: No watch filespec specified");
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

        watcher.Changed += (_, e) => AddChangeToQueue(fileChangeQueue, e, fileSpec);
        watcher.Created += (_, e) => AddChangeToQueue(fileChangeQueue, e, fileSpec);
        watcher.Deleted += (_, e) => AddChangeToQueue(fileChangeQueue, e, fileSpec);
        
        watcher.Filter = fileSpec;
        watcher.IncludeSubdirectories = projectPath.IsFilePath == false;
        watcher.EnableRaisingEvents = true;

        return watcher;
    }

    /// <summary>
    /// Add the change event to a queue for ordered processing.
    /// </summary>
    /// <param name="fileChangeQueue"></param>
    /// <param name="e"></param>
    /// <param name="fileSpec"></param>
    private static void AddChangeToQueue(ConcurrentDictionary<long, FileChangeRequest> fileChangeQueue, FileSystemEventArgs e, string fileSpec)
    {
	    if (string.IsNullOrEmpty(fileSpec))
	    {
		    Console.WriteLine("Fatal Error: No watch filespec specified");
		    Environment.Exit(1);
	    }
	    
	    var changeType = e.ChangeType == WatcherChangeTypes.Deleted ? "deleted" : "changed";

	    if (fileChangeQueue.Any(q => q.Value.FilePath == e.FullPath && q.Value.ChangeType == changeType))
		    return;

	    var newKey = DateTimeOffset.UtcNow.UtcTicks;

	    fileChangeQueue.AddOrUpdate(newKey, new FileChangeRequest
	    {
		    FilePath = e.FullPath,
		    ChangeType = changeType,
		    FileSpec = fileSpec

	    }, (_, oldValue) => new FileChangeRequest
	    {
		    FilePath = oldValue.FilePath,
		    ChangeType = oldValue.ChangeType,
		    FileSpec = fileSpec
	    });
    }
}

public class FileChangeRequest
{
    public string ChangeType { get; set; } = string.Empty;
    public string FileSpec { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}