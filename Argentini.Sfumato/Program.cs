namespace Argentini.Sfumato;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Program
{
	static WeakMessenger Messenger = new ();

	private static async Task Main(string[] args)
	{
		var cancellationTokenSource = new CancellationTokenSource();
		var totalTimer = new Stopwatch();

		totalTimer.Start();
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var appState = new AppState();
		var version = await Identify.VersionAsync(System.Reflection.Assembly.GetExecutingAssembly());

#if DEBUG
		await appState.InitializeAsync(["build", "../../../../Argentini.Sfumato.Tests/SampleWebsite/wwwroot/css/source.css", "../../../../Argentini.Sfumato.Tests/SampleCss/sample.css"]);
#else		
        await appState.InitializeAsync(args);
#endif        
		
		if (appState.VersionMode)
		{
			await Console.Out.WriteLineAsync($"Sfumato Version {version}");
			Environment.Exit(0);
		}
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));
		await Console.Out.WriteLineAsync("Sfumato: A modern CSS generation tool");
		await Console.Out.WriteLineAsync($"Version {version} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()})");
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));
		
		if (appState.InitMode)
		{
            var cssReferenceFile = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.EmbeddedCssPath, "example.css"), Library.FileAccessRetryMs, cancellationTokenSource.Token);
			
			/*
			if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
				appState.WorkingPath = appState.WorkingPathOverride;
			
			await File.WriteAllTextAsync(Path.Combine(appState.WorkingPath, "example.css"), cssReferenceFile, cancellationTokenSource.Token);			
			
			await Console.Out.WriteLineAsync($"Created example.css file at {appState.WorkingPath}");
			*/
			await Console.Out.WriteLineAsync();
			
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
			await Console.Out.WriteLineAsync(appRunner == appState.AppRunners.Last() ? Strings.ThickLine.Repeat(Library.MaxConsoleWidth) : Strings.DotLine.Repeat(Library.MaxConsoleWidth));
		}

		totalTimer.Restart();

		/*
		if (appState.WatchMode)
			await Console.Out.WriteLineAsync($"Started watching at {DateTime.Now:HH:mm:ss.fff}");
		else
			await Console.Out.WriteLineAsync($"Started build at {DateTime.Now:HH:mm:ss.fff}");
			*/

		var tasks = new List<Task>();
		
		foreach (var appRunner in appState.AppRunners)
			tasks.Add(appRunner.PerformFullBuild());

		await Task.WhenAll(tasks);

		foreach (var appRunner in appState.AppRunners)
		{
			var relativePath = Path.GetFullPath(appRunner.AppRunnerSettings.CssFilePath).TruncateCenter((int)Math.Floor(Library.MaxConsoleWidth / 3d), (int)Math.Floor((Library.MaxConsoleWidth / 3d) * 2) - 3, Library.MaxConsoleWidth);
			var utilitiesFound = appRunner.ScannedFiles.Sum(f => f.Value.UtilityClasses.Count);
			
			await Console.Out.WriteLineAsync(relativePath);

			if (appRunner.Messages.Count > 0)
			{
				foreach (var message in appRunner.Messages)
					await Console.Out.WriteLineAsync(message);

				appRunner.Messages.Clear();
			}
			else
			{
				await Console.Out.WriteLineAsync($"Found {appRunner.ScannedFiles.Count:N0} file{(appRunner.ScannedFiles.Count == 1 ? string.Empty : "s")}, {utilitiesFound:N0} class{(utilitiesFound == 1 ? string.Empty : "es")}");
				await Console.Out.WriteLineAsync($"{appRunner.LastCss.Length.FormatBytes()} written to {appRunner.AppRunnerSettings.CssOutputFilePath}");
				await Console.Out.WriteLineAsync($"Build complete at {DateTime.Now:HH:mm:ss.fff} ({appRunner.Stopwatch.FormatTimer()})");
				await Console.Out.WriteLineAsync(Strings.DotLine.Repeat(Library.MaxConsoleWidth));
			}
		}		
		
		await Console.Out.WriteLineAsync($"Elapsed time {totalTimer.FormatTimer()}");
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));

		if (appState.WatchMode)
		{
			foreach (var appRunner in appState.AppRunners)
			{
				// todo: initialize watcher
			}
		}
		
		await Console.Out.WriteLineAsync($"Sfumato stopped at {DateTime.Now:HH:mm:ss.fff}");
		
		Environment.Exit(0);
	}
}
