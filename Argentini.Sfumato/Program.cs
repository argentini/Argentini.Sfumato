namespace Argentini.Sfumato;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Program
{
	private static async Task Main(string[] args)
	{
		var cancellationTokenSource = new CancellationTokenSource();
		var totalTimer = new Stopwatch();

		totalTimer.Start();
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var appState = new AppState();
		var version = await Identify.VersionAsync(System.Reflection.Assembly.GetExecutingAssembly());

        await appState.InitializeAsync(args);
        
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

		/*
		await Console.Out.WriteLineAsync($"Working Path     :  {appState.WorkingPath}");
		await Console.Out.WriteLineAsync($"Transpile        :  {(appState.Minify ? "Minify" : "Expanded")}");
		*/
		await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat(Library.MaxConsoleWidth));

		/*
		foreach (var project in appState.Settings.Projects)
		{
			await Console.Out.WriteLineAsync($"Project          :  {project.ProjectName}");
			await Console.Out.WriteLineAsync($"Theme Mode       :  {(project.DarkMode.Equals("media", StringComparison.OrdinalIgnoreCase) ? "Media Query" : "CSS Classes")}");

			if (project.ProjectPaths.Count > 0)
			{
				var paths = appState.StringBuilderPool.Get();
	        
				foreach (var projectPath in project.ProjectPaths)
				{
					if (paths.Length > 0)
						paths.Append("                 :  ");

					var path  = $".{projectPath.Path.SetNativePathSeparators().TrimStart(appState.WorkingPath).TrimEndingPathSeparators()}{Path.DirectorySeparatorChar}{(projectPath.Recurse ? $"**{Path.DirectorySeparatorChar}" : string.Empty)}*.{(projectPath.ExtensionsList.Count == 1 ? projectPath.Extensions : $"[{projectPath.Extensions}]")}";

					if (string.IsNullOrEmpty(projectPath.IgnoreFolders) == false)
						path += $" (ignore {projectPath.IgnoreFolders.Trim()})";

					path += Environment.NewLine;

					paths.Append(path);
				}
	        
				await Console.Out.WriteLineAsync($"Watch Path(s)    :  {paths.ToString().TrimEnd()}");
			
				appState.StringBuilderPool.Return(paths);
			}        
		}
		*/
		
		await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat(Library.MaxConsoleWidth));

		totalTimer.Restart();

		await Console.Out.WriteLineAsync($"Started build at {DateTime.Now:HH:mm:ss.fff}");


		
		
		
		
		
		
		
		
		
		Environment.Exit(0);
	}
}
