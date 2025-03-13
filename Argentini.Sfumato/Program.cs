using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;

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
        var sassVersion = await appState.GetEmbeddedSassVersionAsync();

        await appState.InitializeAsync(args);
        
		if (appState.VersionMode)
		{
			await Console.Out.WriteLineAsync($"Sfumato Version {version}");
            await Console.Out.WriteLineAsync($"Dart Sass Version {sassVersion}");
			Environment.Exit(0);
		}
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(appState.Library.MaxConsoleWidth));
		await Console.Out.WriteLineAsync("Sfumato: A modern CSS generation tool powered by Sass");
		await Console.Out.WriteLineAsync($"Version {version} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()}) / Dart Sass {sassVersion}");
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(appState.Library.MaxConsoleWidth));
		
		if (appState.InitMode)
		{
            var yaml = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.YamlPath, "sfumato-complete.yml"), appState.Library.FileAccessRetryMs, cancellationTokenSource.Token);
			
			if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
				appState.WorkingPath = appState.WorkingPathOverride;
			
			await File.WriteAllTextAsync(Path.Combine(appState.WorkingPath, "sfumato.yml"), yaml, cancellationTokenSource.Token);			
			
			await Console.Out.WriteLineAsync($"Created sfumato.yml file at {appState.WorkingPath}");
			await Console.Out.WriteLineAsync();
			
			Environment.Exit(0);
		}
		
		if (appState.HelpMode)
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

		await Console.Out.WriteLineAsync($"Working Path     :  {appState.WorkingPath}");
		await Console.Out.WriteLineAsync($"Transpile        :  {(appState.Minify ? "Minify" : "Expanded")}");
		await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat(appState.Library.MaxConsoleWidth));

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
		
		await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat(appState.Library.MaxConsoleWidth));

		if (appState.DiagnosticMode)
			appState.DiagnosticOutput.TryAdd("init000", $"Initialized app in {totalTimer.FormatTimer()}{Environment.NewLine}");
		
		totalTimer.Restart();

		await Console.Out.WriteLineAsync($"Started build at {DateTime.Now:HH:mm:ss.fff}");


		
		
		
		
		
		
		
		
		
		if (appState.DiagnosticMode)
		{
			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync("DIAGNOSTICS:");
			await Console.Out.WriteLineAsync(string.Join(string.Empty, appState.DiagnosticOutput.OrderBy(d => d.Key).Select(v => v.Value)));
		}

		Environment.Exit(0);
	}
}
