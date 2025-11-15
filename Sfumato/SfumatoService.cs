using System.Threading.Tasks.Dataflow;
using Sfumato.Entities.Library;
using Sfumato.Entities.Messenger;
using Sfumato.Entities.Runners;
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable RedundantBoolCompare
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Sfumato;

public sealed class SfumatoService
{
	private static readonly WeakMessenger Messenger = new ();
	private static ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
	private static AppState AppState { get; } = new (StringBuilderPool);

	public static SfumatoConfiguration? Configuration { get; } = new();

	public static readonly ActionBlock<AppRunner> Dispatcher = new (appRunner => Messenger.Send(appRunner), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });

	public static async Task InitializeAsync()
	{
		if (Configuration?.Arguments?.Length is null)
		{
			"SfumatoService.Arguments is null or empty.".WriteToOutput();

			Environment.Exit(1);
			return;
		}
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var version = await Identify.VersionAsync(System.Reflection.Assembly.GetExecutingAssembly());

		Messenger.Register<AppRunner>(void (appRunner) =>
		{
			try
			{
				foreach (var message in appRunner.Messages)
					message.WriteToOutput();

				appRunner.Messages.Clear();
			}
			catch
			{
				// Ignored
			}
		});

		// ReSharper disable once RedundantAssignment
		var argumentErrorMessage = string.Empty;

#if DEBUG
		//argumentErrorMessage = await AppState.InitializeAsync(args);
		//argumentErrorMessage = await AppState.InitializeAsync(["watch", @"c:\code\Fynydd-Website-2024\UmbracoCms\wwwroot\stylesheets\source.css"]);
		argumentErrorMessage = await AppState.InitializeAsync(["watch", "/Users/magic/Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"]);
		//argumentErrorMessage = await AppState.InitializeAsync(["watch", "/Users/magic/Developer/Sfumato-Web/UmbracoCms/wwwroot/stylesheets/source.css"]);
		//argumentErrorMessage = await AppState.InitializeAsync(["watch", "/Users/magic/Developer/Tolnedra2/UmbracoCms/wwwroot/stylesheets/source.css"]);
		//argumentErrorMessage = await AppState.InitializeAsync(["watch", "/Users/magic/Developer/Coursabi/Coursabi.Apps/Coursabi.Apps.Client/Coursabi.Apps.Client/wwwroot/css/source.css"]);
		//argumentErrorMessage = await AppState.InitializeAsync(["watch", "/Users/magic/Developer/Woordle/Woordle.Shared/wwwroot/css/source.css"]);
#else		
        argumentErrorMessage = await AppState.InitializeAsync(args);
#endif

		if (AppState.VersionMode)
		{
			$"Sfumato Version {version}".WriteToOutput();
			Environment.Exit(0);
			return;
		}
		
		Strings.ThickLine.Repeat(Library.MaxConsoleWidth).WriteToOutput();
		"Sfumato: The Ultra-Fast CSS Generation Tool".WriteToOutput();
		$"Version {version} for {Identify.GetOsPlatformName()} ({Identify.GetProcessorArchitecture()})".WriteToOutput();
		
		Strings.ThickLine.Repeat(Library.MaxConsoleWidth).WriteToOutput();

		if (string.IsNullOrEmpty(argumentErrorMessage) == false)
		{
			argumentErrorMessage.WriteToOutput();
			Environment.Exit(0);
			return;
		}

		if (AppState.InitMode)
		{
            var cssReferenceFile = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(Constants.EmbeddedCssPath, "sfumato-example.css"), Library.FileAccessRetryMs);

			await File.WriteAllTextAsync(Path.Combine(Constants.WorkingPath, "sfumato-example.css"), cssReferenceFile);

			"".WriteToOutput();
			$"Created sfumato-example.css file at {Constants.WorkingPath}".WriteToOutput();
			"".WriteToOutput();
			
			Environment.Exit(0);
			return;
		}
		
		if (AppState.HelpMode)
        {
			"""
			Sfumato copies one or more specified source CSS files to new output files that have additional styles for all utility class references in project files._
			You then use the output files instead of the source CSS files._
			Sfumato can build once or watch project files for changes and regenerate the CSS as needed.
			
			Additionally, Sfumato can build/watch multiple source CSS files at once!

			Add Sfumato project settings to your source CSS files by adding a `@layer sfumato { :root { } }` block to the top, but below any @import statements._
			Run the `init` command (below) to create an example CSS file in the current path.
			
			USAGE:
			""".WriteToOutput();
			Strings.ThinLine.Repeat("USAGE:".Length).WriteToOutput();

			"""
			sfumato [command] [options]

			COMMANDS:
			""".WriteToOutput();
			Strings.ThinLine.Repeat("COMMANDS:".Length).WriteToOutput();

			"""
			help     : Show this help message
			init     : Create sfumato-example.css file in the current path
			version  : Show the Sfumato version number
			build    : Perform a build of the specified file(s)
			watch    : Perform a build of the specified file(s) and watch for changes

			OPTIONS:
			""".WriteToOutput();
            Strings.ThinLine.Repeat("OPTIONS:".Length).WriteToOutput();

			"""
			[file]   : Path to a CSS file that has Sfumato project settings
			--minify : Minify CSS output; overrides project setting

			Options must be specified as [file] [--minify] and repeat in pairs.
			
			EXAMPLE:
			""".WriteToOutput();
			Strings.ThinLine.Repeat("EXAMPLE:".Length).WriteToOutput();
			
			"""
			sfumato watch client/css/source.css --minify server/css/source.css --minify

			COMPATIBILITY:
			""".WriteToOutput();
			Strings.ThinLine.Repeat("COMPATIBILITY:".Length).WriteToOutput();
			
			"""
			If you need to continue to use the prior version of Sfumato, install sfumato-scss tool. This should be used for older projects relying on that version._

			Install the compatibility tool with the command below: 
			
			dotnet tool install --global argentini.sfumato-scss
			""".WriteToOutput();
			
            "".WriteToOutput();

			Environment.Exit(0);

			return;
		}

		if (AppState.BuildMode || AppState.WatchMode)
		{
			foreach (var appRunner in AppState.AppRunners)
			{
				var options =
				(
					(appRunner.AppRunnerSettings.UseReset ? "CSS Reset, " : string.Empty) +
					(appRunner.AppRunnerSettings.UseForms ? "Forms CSS, " : string.Empty) +
					(appRunner.AppRunnerSettings.UseMinify ? "Compressed Output, " : "Expanded Output, ")
				).Trim(',', ' ');

				var maxWidth = Library.MaxConsoleWidth - 15;
				var relativeSourcePath = Path.GetFullPath(appRunner.AppRunnerSettings.CssFilePath)
					.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
				var relativeOutputPath = Path.GetFullPath(appRunner.AppRunnerSettings.NativeCssOutputFilePath)
					.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);

				$"CSS Source  :  {relativeSourcePath}".WriteToOutput();
				$"CSS Output  :  {relativeOutputPath}".WriteToOutput();
				$"Options     :  {(string.IsNullOrEmpty(options) ? "None" : options)}".WriteToOutput();

				foreach (var path in appRunner.AppRunnerSettings.AbsolutePaths)
				{
					var relativePath2 = path.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
					
					$"Path        :  {relativePath2}".WriteToOutput();
				}

				foreach (var path in appRunner.AppRunnerSettings.AbsoluteNotPaths)
				{
					var relativePath2 = path.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);

					$"Ignore      :  {relativePath2}".WriteToOutput();
				}

				(appRunner == AppState.AppRunners.Last() ? Strings.ThickLine.Repeat(Library.MaxConsoleWidth) : Strings.DotLine.Repeat(Library.MaxConsoleWidth)).WriteToOutput();
			}

			var tasks = new List<Task>();

			foreach (var appRunner in AppState.AppRunners)
			{
				tasks.Add(appRunner.PerformFileScanAsync());
			}

			await Task.WhenAll(tasks);
			tasks.Clear();

			foreach (var appRunner in AppState.AppRunners)
				tasks.Add(appRunner.FullBuildAndSaveCss());

			await Task.WhenAll(tasks);

			if (AppState.WatchMode)
			{
				do
				{
					await Task.Delay(25);

				} while (AppState.AppRunners.Any(r => r.Messages.Count != 0));

				"Watching; press ESC to exit".WriteToOutput();

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

				foreach (var appRunner in AppState.AppRunners)
					await appRunner.StartWatchingAsync();

				while ((Console.IsInputRedirected || Console.KeyAvailable == false) &&
				       cancellationTokenSource.IsCancellationRequested == false)
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

					foreach (var appRunner in AppState.AppRunners)
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

						} while (AppState.AppRunners.Any(r => r.Messages.Count != 0));

						"Watching; press ESC to exit".WriteToOutput();
					}

					if (Console.IsInputRedirected)
						continue;
					
					if (Console.KeyAvailable == false)
						continue;

					var keyPress = Console.ReadKey(intercept: true);

					if (keyPress.Key != ConsoleKey.Escape)
						continue;

					await cancellationTokenSource.CancelAsync();

					break;
				}
			}

			"".WriteToOutput();

			if (AppState.WatchMode)
			{
				"Shutting down...".WriteToOutput();

				foreach (var appRunner in AppState.AppRunners)
					await appRunner.ShutDownWatchersAsync();
			}
		}

		$"Sfumato stopped at {DateTime.Now:HH:mm:ss.fff}".WriteToOutput();
		"".WriteToOutput();
		
		Environment.Exit(0);
	}
}
