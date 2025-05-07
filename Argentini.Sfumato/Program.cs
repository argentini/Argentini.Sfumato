using System.Threading.Tasks.Dataflow;

namespace Argentini.Sfumato;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Program
{
	private static readonly WeakMessenger Messenger = new ();
	public static readonly ActionBlock<AppRunner> Dispatcher = new (appRunner => Messenger.Send(appRunner), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 }
	);

	// ReSharper disable once UnusedParameter.Local
	private static async Task Main(string[] args)
	{
		var totalTimer = new Stopwatch();

		totalTimer.Start();
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var appState = new AppState();
		var version = await Identify.VersionAsync(System.Reflection.Assembly.GetExecutingAssembly());

		Messenger.Register<AppRunner>(async void (appRunner) =>
		{
			try
			{
				foreach (var message in appRunner.Messages)
					await Console.Out.WriteLineAsync(message);

				appRunner.Messages.Clear();
			}
			catch
			{
				// Ignored
			}
		});

#if DEBUG
		//await appState.InitializeAsync(["watch", "../../../../Argentini.Sfumato.Tests/SampleWebsite/wwwroot/css/source.css", "../../../../Argentini.Sfumato.Tests/SampleCss/sample.css", "../../../../../Coursabi/Coursabi.Apps/Coursabi.Apps.Client/Coursabi.Apps.Client/wwwroot/css/source.css"]);
		//await appState.InitializeAsync(["build", "../../../../../Coursabi/Coursabi.Apps/Coursabi.Apps.Client/Coursabi.Apps.Client/wwwroot/css/source.css"]);
		await appState.InitializeAsync(["build", "../../../../../Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"]);
#else		
        await appState.InitializeAsync(args);
#endif        
		
		if (appState.VersionMode)
		{
			await Console.Out.WriteLineAsync($"Sfumato Version {version}");
			Environment.Exit(0);
		}
		
		await Console.Out.WriteLineAsync();
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));
		await Console.Out.WriteLineAsync("Sfumato: A modern CSS generation tool");
		await Console.Out.WriteLineAsync($"Version {version} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()})");
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));
		
		if (appState.InitMode)
		{
			/*
            var cssReferenceFile = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.EmbeddedCssPath, "example.css"), Library.FileAccessRetryMs, cancellationTokenSource.Token);

			if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
				appState.WorkingPath = appState.WorkingPathOverride;

			await File.WriteAllTextAsync(Path.Combine(appState.WorkingPath, "example.css"), cssReferenceFile, cancellationTokenSource.Token);

			await Console.Out.WriteLineAsync($"Created example.css file at {appState.WorkingPath}");
			await Console.Out.WriteLineAsync();
			*/
			
			Environment.Exit(0);
		}
		
		if (appState.HelpMode)
        {
            await Console.Out.WriteLineAsync();

            const string introText = """
                                     Specify one or more CSS files and Sfumato will scan each for project settings._
                                     It will then watch each project path for instances of valid utility classes as files are saved._
                                     The result is an output CSS file containing only the styles for used utility classes, for each specified source CSS file:
                                     
                                     CSS Settings:
                                     """;
            introText.WriteToConsole(80);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("CSS Settings:".Length));

            var directivesText =
				$$"""
				::sfumato {
				    --paths: ["../Models/", "../Views/"];
				    --output: "output.css";
				    --not-paths: ["../Views/temp/"];

				    --use-reset: true; /* Inject the CSS reset; default is true */
				    --use-forms: true; /* Inject form input styles default is true */
				    --use-minify: false; /* Compress the output CSS by default; default is false */
				}

				{{Strings.TriangleRight}} `@apply [class name] [...];`
				  Embed the styles for a specific utility class within your own classes;
				  used to create custom classes with one or more utility class styles
				  (e.g. `.heading { @apply text-2xl/5 bold; }`)

				{{Strings.TriangleRight}} `@variant [variant name] { .. }`
				  Use @media queries by variant name in your custom CSS code;
				  (e.g. `@variant dark { heading { @apply text-2xl/5 bold; } }`)
				
				Command Line Usage:
				""";
            directivesText.WriteToConsole(80);
            await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("Command Line Usage:".Length));

            const string cliUsageText = """
                                        sfumato [help|version]
                                        sfumato [build|watch] [file] [--minify] [file] [--minify] etc.

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

                                        """;
            commandsText.WriteToConsole(80);

            const string optionsText = """
                                       [file]    : File path to a CSS file that has imported Sfumato
                                       --minify  : Minify CSS output; use with build and watch commands
                                       """;
            optionsText.WriteToConsole(80);

            await Console.Out.WriteLineAsync();

			Environment.Exit(0);
		}

		foreach (var appRunner in appState.AppRunners)
		{
			var options =
				(
					(appRunner.AppRunnerSettings.UseReset ? "CSS Reset, " : string.Empty) +
					(appRunner.AppRunnerSettings.UseForms ? "Forms CSS, " : string.Empty) +
					(appRunner.AppRunnerSettings.UseMinify ? "Compressed Output, " : "Expanded Output, ")
				).Trim(',', ' ');

			var maxWidth = Library.MaxConsoleWidth - 15;
			var relativePath = Path.GetFullPath(appRunner.AppRunnerSettings.CssFilePath).TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
			
			await Console.Out.WriteLineAsync($"CSS Source  :  {relativePath}");
			await Console.Out.WriteLineAsync($"Options     :  {(string.IsNullOrEmpty(options) ? "None" : options)}");

			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (var path in appRunner.AppRunnerSettings.AbsolutePaths)
			{
				var relativePath2 = path.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
				await Console.Out.WriteLineAsync($"Path        :  {relativePath2}");
			}

			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (var path in appRunner.AppRunnerSettings.AbsoluteNotPaths)
			{
				var relativePath2 = path.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
				await Console.Out.WriteLineAsync($"Ignore      :  {relativePath2}");
			}
			
			await Console.Out.WriteLineAsync(appRunner == appState.AppRunners.Last() ? Strings.ThickLine.Repeat(Library.MaxConsoleWidth) : Strings.DotLine.Repeat(Library.MaxConsoleWidth));
		}

		var tasks = new List<Task>();
		
		// ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
		foreach (var appRunner in appState.AppRunners)
		{
			await appRunner.AddCssPathMessageAsync();

			tasks.Add(appRunner.PerformFileScanAsync());
		}

		await Task.WhenAll(tasks);
		tasks.Clear();
		
		// ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
		foreach (var appRunner in appState.AppRunners)
			tasks.Add(appRunner.BuildAndSaveCss());
		
		await Task.WhenAll(tasks);

		totalTimer.Stop();
		
		// await Console.Out.WriteLineAsync($"Elapsed time {totalTimer.FormatTimer()}");
		// await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));

		if (appState.WatchMode)
		{
			do
			{
				await Task.Delay(25);
				
			} while (appState.AppRunners.Any(r => r.Messages.Count != 0));

			await Console.Out.WriteLineAsync("Watching; Press ESC to Exit");

			var cancellationTokenSource = new CancellationTokenSource();

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

			foreach (var appRunner in appState.AppRunners)
				await appRunner.StartWatchingAsync();
			
			while ((Console.IsInputRedirected || Console.KeyAvailable == false) && cancellationTokenSource.IsCancellationRequested == false)
			{
				try
				{
					await Task.Delay(25, cancellationTokenSource.Token);
				}
				catch (TaskCanceledException)
				{
					break;
				}

				if (cancellationTokenSource.IsCancellationRequested)
					break;

				var performedWork = false;
				
				foreach (var appRunner in appState.AppRunners)
				{
					var result = await appRunner.ProcessWatchQueues();

					if (performedWork == false && result)
						performedWork = true;
				}

				if (performedWork)
				{
					do
					{
						await Task.Delay(25, cancellationTokenSource.Token);
				
					} while (appState.AppRunners.Any(r => r.Messages.Count != 0));

					await Console.Out.WriteLineAsync("Watching; Press ESC to Exit");
				}

				// ReSharper disable once InvertIf
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
		}

		await Console.Out.WriteLineAsync();

		if (appState.WatchMode)
		{
			await Console.Out.WriteLineAsync("Shutting down...");

			foreach (var appRunner in appState.AppRunners)
				await appRunner.ShutDownWatchersAsync();
		}
		
		await Console.Out.WriteLineAsync($"Sfumato stopped at {DateTime.Now:HH:mm:ss.fff}");
		await Console.Out.WriteLineAsync();
		
		Environment.Exit(0);
	}
}
