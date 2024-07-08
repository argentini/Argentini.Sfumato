using Mapster;

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
        var sassVersion = await runner.AppState.GetEmbeddedSassVersionAsync();

		await runner.InitializeAsync(args);

		if (runner.AppState.VersionMode)
		{
			await Console.Out.WriteLineAsync($"Sfumato Version {version}");
            await Console.Out.WriteLineAsync($"Dart Sass Version {sassVersion}");
			Environment.Exit(0);
		}
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(SfumatoRunner.MaxConsoleWidth));
		await Console.Out.WriteLineAsync("Sfumato: The lean, modern, utility-based SCSS/CSS framework generation tool");
		await Console.Out.WriteLineAsync($"Version {version} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()}) / Dart Sass {sassVersion} / {runner.AppState.UtilityClassCollection.Count:N0} Utility Classes");
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(SfumatoRunner.MaxConsoleWidth));
		
		if (runner.AppState.InitMode)
		{
            var yaml = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(runner.AppState.YamlPath, "sfumato-complete.yml"), SfumatoAppState.FileAccessRetryMs, cancellationTokenSource.Token);
			
			if (string.IsNullOrEmpty(runner.AppState.WorkingPathOverride) == false)
				runner.AppState.WorkingPath = runner.AppState.WorkingPathOverride;
			
			await File.WriteAllTextAsync(Path.Combine(runner.AppState.WorkingPath, "sfumato.yml"), yaml, cancellationTokenSource.Token);			
			
			await Console.Out.WriteLineAsync($"Created sfumato.yml file at {runner.AppState.WorkingPath}");
			await Console.Out.WriteLineAsync();
			
			Environment.Exit(0);
		}
		
		if (runner.AppState.HelpMode)
        {
            await Console.Out.WriteLineAsync();

            const string introText = """
                                     Sfumato will recursively scan your project directory for SCSS files and transpile them._
                                     It will also inject Sfumato styles into your generated CSS wherever the appropriate Sfumato directive is found:
                                     
                                     Directives:
                                     """;
            introText.WriteToConsole(80);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Directives:".Length));

            var directivesText = $$"""
                                 {{Strings.TriangleRight}} `@sfumato base;`
                                   Embed browser reset and base element styles

                                 {{Strings.TriangleRight}} `@sfumato utilities;`
                                   Embed utility classes based on which ones are being used in your project
                                   files (configurable in a `sfumato.yml` settings file)

                                 {{Strings.TriangleRight}} `@apply [class name] [...];`
                                   Embed the styles for a specific utility class within your own classes;
                                   used to create custom classes with one or more utility class styles
                                   (e.g. `.heading { @apply text-2xl/5 bold }`)

                                 Command Line Usage:
                                 """;
            directivesText.WriteToConsole(80);
            await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Command Line Usage:".Length));

            const string cliUsageText = """
                                        sfumato [help|version]
                                        sfumato [build|watch] [options]

                                        Commands:
                                        """;
            cliUsageText.WriteToConsole(80);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Commands:".Length));

            const string commandsText = """
                                        init      : Create a sample sfumato.yml file (can use --path)
                                        build     : Perform a single complete build
                                        watch     : Perform a complete build and then watch for changes
                                        version   : Show the sfumato version number
                                        help      : Show this help message

                                        * build and watch commands look in the current path for a `sfumato.yml`
                                          settings file unless using the `--path` option; visit https://sfumato.app
                                          for more information on creating a sfumato.yml settings file

                                        Options:
                                        """;
            commandsText.WriteToConsole(80);
            await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Options:".Length));

            const string optionsText = """
                                       --path    : Follow with a relative or absolute path to/for your sfumato.yml
                                                   settings file (e.g. `sfumato watch --path Code/MyProject`)
                                       --minify  : Minify CSS output; use with build and watch commands
                                       """;
            optionsText.WriteToConsole(80);

            await Console.Out.WriteLineAsync();

			Environment.Exit(0);
		}

		await Console.Out.WriteLineAsync($"Theme Mode       :  {(runner.AppState.Settings.DarkMode.Equals("media", StringComparison.OrdinalIgnoreCase) ? "Media Query" : "CSS Classes")}");
		await Console.Out.WriteLineAsync($"Transpile        :  {(runner.AppState.Minify ? "Minify" : "Expanded")}");
		await Console.Out.WriteLineAsync($"Project Path     :  {runner.AppState.WorkingPath}");

		if (runner.AppState.Settings.ProjectPaths.Count > 0)
		{
			var paths = runner.AppState.StringBuilderPool.Get();
	        
			foreach (var path in runner.AppState.Settings.ProjectPaths)
			{
				if (paths.Length > 0)
					paths.Append("                 :  ");

				paths.Append($".{path.Path.SetNativePathSeparators().TrimStart(runner.AppState.WorkingPath).TrimEndingPathSeparators()}{Path.DirectorySeparatorChar}{(path.Recurse ? $"**{Path.DirectorySeparatorChar}" : string.Empty)}*.{(path.ExtensionsList.Count == 1 ? path.Extensions : $"[{path.Extensions}]")}{Environment.NewLine}");
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
				Path = runner.AppState.SettingsFilePath.TrimEnd("sfumato.yml") ?? string.Empty,
				Extensions = "yaml"
				
			}, false));
			
			foreach (var projectPath in runner.AppState.Settings.ProjectPaths)
            {
                if (projectPath.ExtensionsList.Count == 0 || Directory.Exists(projectPath.Path) == false)
                    continue;

				if (projectPath.ExtensionsList.Count != 0 && projectPath.ExtensionsList.Contains("scss"))
                {
                    var newProjectPath = new ProjectPath
                    {
                        Path = projectPath.Path,
                        Extensions = "scss",
                        Recurse = projectPath.Recurse
                    };
                    
					fileWatchers.Add(await CreateFileChangeWatcherAsync(scssTranspileQueue, newProjectPath, projectPath.Recurse));

                    projectPath.Extensions = projectPath.Extensions.TrimStart("scss,")?.TrimEnd(",scss")?.Replace(",scss,", ",") ?? string.Empty;
                }

                if (projectPath.ExtensionsList.Count != 0 && projectPath.ExtensionsList.Contains("scss") == false)
                {
					fileWatchers.Add(await CreateFileChangeWatcherAsync(rebuildProjectQueue, projectPath, projectPath.Recurse));
                }
			}
			
			#endregion

			#region Watch

			var watchTimer = new Stopwatch();

            // Support async stdin read
            if (Console.IsInputRedirected)
            {
                // Read the stream without blocking the main thread.
                // Check if the escape key is sent to the stdin stream.
                // If it is, cancel the token source and exit the loop.
                // This will allow interactive quit when input is redirected.
                _ = Task.Run(async () =>
                {
                    while (cancellationTokenSource.IsCancellationRequested == false)
                    {
                        try
                        {
                            await Task.Delay(25, cancellationTokenSource.Token);
                        }
                        catch (TaskCanceledException)
                        {
                            break;
                        }

                        if (Console.In.Peek() == -1)
                            continue;

                        if ((char)Console.In.Read() != Convert.ToChar(ConsoleKey.Escape))
                            continue;

                        await cancellationTokenSource.CancelAsync();
                        break;
                    }
                }, cancellationTokenSource.Token);
            }
			
			while ((Console.IsInputRedirected || Console.KeyAvailable == false) && cancellationTokenSource.IsCancellationRequested == false)
			{
				var processedFiles = false;

				try
				{
					await Task.Delay(25, cancellationTokenSource.Token);
				}
				catch (TaskCanceledException)
				{
					break;
				}
				
				watchTimer.Restart();

				if (rebuildProjectQueue.IsEmpty == false)
				{
					var triggered = false;

                    var clonedDictionary = rebuildProjectQueue.Adapt<Dictionary<long,FileChangeRequest?>>().ToDictionary();
                    
					foreach (var (_, fileChangeRequest) in clonedDictionary.OrderBy(f => f.Key))
                    {
                        if (fileChangeRequest is null)
                            continue;
                        
						var eventArgs = fileChangeRequest.FileSystemEventArgs;

						if (eventArgs is null)
							continue;
						
						var filePath = SfumatoRunner.ShortenPathForOutput(eventArgs.FullPath, runner.AppState);
						
						if (eventArgs.ChangeType == WatcherChangeTypes.Deleted)
						{
							if (fileChangeRequest.IsMatchingFile(eventArgs.Name) == false)
								continue;
							
							await Console.Out.WriteLineAsync($"Project file changes detected at {DateTime.Now:HH:mm:ss.fff}");
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Deleted {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.DeleteWatchedFileAsync(eventArgs.FullPath);
							triggered = true;
						}

						else if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
						{
							if (fileChangeRequest.IsMatchingFile(eventArgs.Name) == false)
								continue;

							await Console.Out.WriteLineAsync($"Project file changes detected at {DateTime.Now:HH:mm:ss.fff}");
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Updated {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.AddUpdateWatchedFileAsync(eventArgs.FullPath, cancellationTokenSource);
							triggered = true;
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Created)
						{
							if (fileChangeRequest.IsMatchingFile(eventArgs.Name) == false)
								continue;

							await Console.Out.WriteLineAsync($"Project file changes detected at {DateTime.Now:HH:mm:ss.fff}");
							await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Added {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							await runner.AddUpdateWatchedFileAsync(eventArgs.FullPath, cancellationTokenSource);
							triggered = true;
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Renamed)
						{
							var renamedEventArgs = (RenamedEventArgs) eventArgs;

							if (fileChangeRequest.IsMatchingFile(renamedEventArgs.OldName) == false && fileChangeRequest.IsMatchingFile(renamedEventArgs.Name) == false)
								continue;

							await Console.Out.WriteLineAsync($"Project file changes detected at {DateTime.Now:HH:mm:ss.fff}");
							
							if (fileChangeRequest.IsMatchingFile(renamedEventArgs.OldName))
							{
								var oldFilePath = SfumatoRunner.ShortenPathForOutput(renamedEventArgs.OldFullPath, runner.AppState);

								await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Renamed {oldFilePath} => {filePath} at {DateTime.Now:HH:mm:ss.fff}");
								await runner.DeleteWatchedFileAsync(renamedEventArgs.OldFullPath);
							}

							if (fileChangeRequest.IsMatchingFile(renamedEventArgs.Name) == false)
								continue;
								
							await runner.AddUpdateWatchedFileAsync(renamedEventArgs.FullPath, cancellationTokenSource);
							triggered = true;
						}
					}

                    foreach (var item in clonedDictionary)
                        rebuildProjectQueue.TryRemove(item.Key, out _);
					
					if (triggered)
					{
						var timer = new Stopwatch();

						timer.Start();

						await runner.AppState.ExamineWatchedFilesForUsedClassesAsync();
						await runner.PerformCoreBuildAsync(timer, true);

						processedFiles = true;
					}
				}

				else if (scssTranspileQueue.IsEmpty == false)
				{
					var triggered = false;

                    var clonedDictionary = scssTranspileQueue.Adapt<Dictionary<long,FileChangeRequest?>>().ToDictionary();

					foreach (var (_, fileChangeRequest) in clonedDictionary.OrderBy(f => f.Key))
					{
                        if (fileChangeRequest is null)
                            continue;

                        var eventArgs = fileChangeRequest.FileSystemEventArgs;

						if (eventArgs is null)
							continue;

						var filePath = SfumatoRunner.ShortenPathForOutput(eventArgs.FullPath, runner.AppState);

						if (eventArgs.ChangeType == WatcherChangeTypes.Deleted)
						{
							if (fileChangeRequest.IsMatchingFile(eventArgs.Name) == false)
								continue;

							await Console.Out.WriteLineAsync($"Deleted {filePath} at {DateTime.Now:HH:mm:ss.fff}");

							if ((Path.GetFileName(eventArgs.Name)?.StartsWith('_') ?? false) == false)
							{
								await runner.DeleteWatchedScssFileAsync(eventArgs.FullPath);
								await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Deleted {filePath.TrimEnd(".scss")}.css/map");
							}

							if (Path.GetFileName(eventArgs.Name)?.StartsWith('_') ?? false)
							{
								await Console.Out.WriteLineAsync($"{Strings.TriangleRight} SCSS partial deleted, perform full rebuild");

								var timer = new Stopwatch();

								timer.Start();
								await runner.PerformCoreBuildAsync(timer);
							}

							triggered = true;
						}

						else if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
						{
							if (fileChangeRequest.IsMatchingFile(eventArgs.Name) == false)
								continue;

							await Console.Out.WriteLineAsync($"Updated {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							
							if ((Path.GetFileName(eventArgs.Name)?.StartsWith('_') ?? false) == false)
								await runner.AddUpdateWatchedScssFileAsync(eventArgs.FullPath, cancellationTokenSource);
							
							if (Path.GetFileName(eventArgs.Name)?.StartsWith('_') ?? false)
							{
								await Console.Out.WriteLineAsync($"{Strings.TriangleRight} SCSS partial changed, perform full rebuild");

								var timer = new Stopwatch();

								timer.Start();
								await runner.PerformCoreBuildAsync(timer);
							}
							
							triggered = true;
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Created)
						{
							if (fileChangeRequest.IsMatchingFile(eventArgs.Name) == false)
								continue;

							await Console.Out.WriteLineAsync($"Added {filePath} at {DateTime.Now:HH:mm:ss.fff}");

							if ((Path.GetFileName(eventArgs.Name)?.StartsWith('_') ?? false) == false)
								await runner.AddUpdateWatchedScssFileAsync(eventArgs.FullPath, cancellationTokenSource);

							if (Path.GetFileName(eventArgs.Name)?.StartsWith('_') ?? false)
							{
								await Console.Out.WriteLineAsync($"{Strings.TriangleRight} SCSS partial added, perform full rebuild");

								var timer = new Stopwatch();

								timer.Start();
								await runner.PerformCoreBuildAsync(timer);
							}
							
							triggered = true;
						}
						
						else if (eventArgs.ChangeType == WatcherChangeTypes.Renamed)
						{
							var renamedEventArgs = (RenamedEventArgs) eventArgs;
						
							if (fileChangeRequest.IsMatchingFile(renamedEventArgs.OldName) == false && fileChangeRequest.IsMatchingFile(renamedEventArgs.Name) == false)
								continue;

							var oldFilePath = SfumatoRunner.ShortenPathForOutput(renamedEventArgs.OldFullPath, runner.AppState);

							await Console.Out.WriteLineAsync($"Renamed {oldFilePath} => {filePath} at {DateTime.Now:HH:mm:ss.fff}");
							
							if (fileChangeRequest.IsMatchingFile(renamedEventArgs.OldName))
							{
								if ((Path.GetFileName(renamedEventArgs.OldName)?.StartsWith('_') ?? false) == false)
								{
									await runner.DeleteWatchedScssFileAsync(renamedEventArgs.OldFullPath);
									await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Deleted {oldFilePath.TrimEnd(".scss")}.css");
								}
							}

							if (fileChangeRequest.IsMatchingFile(renamedEventArgs.Name))
							{
								if ((Path.GetFileName(renamedEventArgs.Name)?.StartsWith('_') ?? false) == false)
								{
									await runner.AddUpdateWatchedScssFileAsync(renamedEventArgs.FullPath, cancellationTokenSource);
								}
							}

							if ((Path.GetFileName(renamedEventArgs.OldName)?.StartsWith('_') ?? false) || (Path.GetFileName(renamedEventArgs.Name)?.StartsWith('_') ?? false))
							{
								await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Partial changed, perform full rebuild");

								var timer = new Stopwatch();

								timer.Start();
								await runner.PerformCoreBuildAsync(timer);
							}
							
							triggered = true;
						}
					}

                    foreach (var item in clonedDictionary)
                        scssTranspileQueue.TryRemove(item.Key, out _);

					if (triggered)
					{
						processedFiles = true;
					}
				}
				
				else if (restartAppQueue.IsEmpty == false)
				{
					var triggered = false;

                    var clonedDictionary = restartAppQueue.Adapt<Dictionary<long,FileChangeRequest?>>().ToDictionary();

					foreach (var (_, fileChangeRequest) in clonedDictionary)
					{
                        if (fileChangeRequest is null)
                            continue;

                        var eventArgs = fileChangeRequest.FileSystemEventArgs;

						if (eventArgs is null)
							continue;

						if (eventArgs.ChangeType is WatcherChangeTypes.Changed or WatcherChangeTypes.Created)
						{
							if (eventArgs.Name != "sfumato.yml")
								continue;
							
							triggered = true;
							break;
						}

						if (eventArgs.ChangeType != WatcherChangeTypes.Renamed)
							continue;
						
						var renamedEventArgs = (RenamedEventArgs)eventArgs;

						if (renamedEventArgs.OldName == "sfumato.yml")
							continue;
							
						triggered = true;
						break;
					}

					if (triggered)
					{
						await Console.Out.WriteLineAsync($"Modified sfumato.yml at {DateTime.Now:HH:mm:ss.fff}");
						await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Reload settings and rebuild");

						var timer = new Stopwatch();

						timer.Start();

						await runner.InitializeAsync(args);
						await runner.AppState.GatherWatchedFilesAsync();
						await runner.PerformCoreBuildAsync(timer);

						processedFiles = true;
					}

                    foreach (var item in clonedDictionary)
                        restartAppQueue.TryRemove(item.Key, out _);
				}
				
				if (processedFiles)
				{
					await Console.Out.WriteLineAsync();
					await Console.Out.WriteLineAsync("Watching; Press ESC to Exit");
					await Console.Out.WriteLineAsync();
				}

				if (cancellationTokenSource.IsCancellationRequested)
					break;

				// Handle normal console input
				if (Console.IsInputRedirected == false)
				{
					if (Console.KeyAvailable == false)
						continue;
					
					var keyPress = Console.ReadKey(intercept: true);

					if (keyPress.Key != ConsoleKey.Escape)
						continue;
					
					await cancellationTokenSource.CancelAsync();
					
					break;
				}
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
	    var watcher = new FileSystemWatcher(filePath)
        {
            NotifyFilter = NotifyFilters.Attributes
                           | NotifyFilters.CreationTime
                           | NotifyFilters.FileName
                           | NotifyFilters.LastWrite
                           | NotifyFilters.Size
        };

        watcher.Changed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, projectPath.Extensions);
        watcher.Created += async(_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, projectPath.Extensions);
        watcher.Deleted += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, projectPath.Extensions);
        watcher.Renamed += async (_, e) => await AddChangeToQueueAsync(fileChangeQueue, e, projectPath.Extensions);
        
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
    /// <param name="extensions"></param>
    private static async Task AddChangeToQueueAsync(ConcurrentDictionary<long, FileChangeRequest> fileChangeQueue, FileSystemEventArgs e, string extensions)
    {
	    var newKey = DateTimeOffset.UtcNow.UtcTicks;
	    var fcr = new FileChangeRequest
	    {
		    FileSystemEventArgs = e,
		    Extensions = extensions
	    };

	    fileChangeQueue.TryAdd(newKey, fcr);

	    await Task.CompletedTask;
    }
}
