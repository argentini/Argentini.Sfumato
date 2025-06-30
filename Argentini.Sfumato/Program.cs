// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ClassNeverInstantiated.Global

using System.Threading.Tasks.Dataflow;

namespace Argentini.Sfumato;

internal class Program
{
	private static readonly WeakMessenger Messenger = new ();

	public static readonly ActionBlock<AppRunner> Dispatcher = new (appRunner => Messenger.Send(appRunner), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });

	private static async Task Main(string[] args)
	{
		var appState = new AppState();

		#region Launch SCSS (prior) version when args indicate

		// args = ["watch", "--path", "/Users/magic/Developer/Fynydd-Website-2024/UmbracoCms/"];

		if (args.Length > 1 && args.Any(a => a.EndsWith(".css")) == false)
		{
			try
			{
				var psi = new ProcessStartInfo("sfumato-scss", string.Join(' ', args))
				{
					RedirectStandardInput  = false, // inherit console → interactive
					RedirectStandardOutput = false, // inherit console → live output
					RedirectStandardError  = false,
					UseShellExecute        = false
				};

				using var proc = Process.Start(psi);

				if (proc is null)
				{
					throw new Exception();
				}

				await proc.WaitForExitAsync();

				Environment.ExitCode = proc.ExitCode;
				return;
			}
			catch
			{
				await Console.Out.WriteLineAsync("CLI arguments used are for legacy sfumato-scss; failed to start.");
				await Console.Out.WriteLineAsync("Install the compatibility tool using:");
				await Console.Out.WriteLineAsync("dotnet tool install --global argentini.sfumato-scss");
				
				Environment.Exit(1);
				return;
			}
		}

		#endregion

		var totalTimer = new Stopwatch();

		totalTimer.Start();
		
		Console.OutputEncoding = Encoding.UTF8;
		
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

		// ReSharper disable once RedundantAssignment
		var argumentErrorMessage = string.Empty;

#if DEBUG
		//argumentErrorMessage = await appState.InitializeAsync(args);
		//argumentErrorMessage = await appState.InitializeAsync(["watch", @"c:\code\Fynydd-Website-2024\UmbracoCms\wwwroot\stylesheets\source.css"]);
		//argumentErrorMessage = await appState.InitializeAsync(["watch", "/Users/magic/Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"]);
		argumentErrorMessage = await appState.InitializeAsync(["watch", "/Users/magic/Developer/Sfumato-Web/UmbracoCms/wwwroot/stylesheets/source.css"]);
		//argumentErrorMessage = await appState.InitializeAsync(["watch", "/Users/magic/Developer/Tolnedra2/UmbracoCms/wwwroot/stylesheets/source.css"]);
		//argumentErrorMessage = await appState.InitializeAsync(["watch", "/Users/magic/Developer/Coursabi/Coursabi.Apps/Coursabi.Apps.Client/Coursabi.Apps.Client/wwwroot/css/source.css"]);
		//argumentErrorMessage = await appState.InitializeAsync(["watch", "/Users/magic/Developer/Woordle/Woordle.Shared/wwwroot/css/source.css"]);
#else		
        argumentErrorMessage = await appState.InitializeAsync(args);
#endif

		if (appState.VersionMode)
		{
			await Console.Out.WriteLineAsync($"Sfumato Version {version}");
			Environment.Exit(0);
			return;
		}
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));
		await Console.Out.WriteLineAsync("Sfumato: The Ultra-Fast CSS Generation Tool");
		await Console.Out.WriteLineAsync($"Version {version} for {Identify.GetOsPlatformName()} (.NET {Identify.GetRuntimeVersion()}/{Identify.GetProcessorArchitecture()})");
		
		await Console.Out.WriteLineAsync(Strings.ThickLine.Repeat(Library.MaxConsoleWidth));

		if (string.IsNullOrEmpty(argumentErrorMessage) == false)
		{
			await Console.Out.WriteLineAsync(argumentErrorMessage);
			Environment.Exit(0);
			return;
		}

		if (appState.InitMode)
		{
            var cssReferenceFile = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.EmbeddedCssPath, "sfumato-example.css"), Library.FileAccessRetryMs);

			await File.WriteAllTextAsync(Path.Combine(appState.WorkingPath, "sfumato-example.css"), cssReferenceFile);

			await Console.Out.WriteLineAsync();
			await Console.Out.WriteLineAsync($"Created sfumato-example.css file at {appState.WorkingPath}");
			await Console.Out.WriteLineAsync();
			
			Environment.Exit(0);
			return;
		}
		
		if (appState.HelpMode)
        {
			"""
			Sfumato copies one or more specified source CSS files to new output files that have additional styles for all utility class references in project files._
			You then use the output files instead of the source CSS files._
			Sfumato can build once or watch project files for changes and regenerate the CSS as needed.
			
			Additionally, Sfumato can build/watch multiple source CSS files at once!

			Add Sfumato project settings to your source CSS files by adding a `@layer sfumato { :root { } }` block to the top, but below any @import statements._
			Run the `init` command (below) to create an example CSS file in the current path.
			
			USAGE:
			""".WriteToConsole(Library.MaxConsoleWidth);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("USAGE:".Length));

			"""
			sfumato [command] [options]

			COMMANDS:
			""".WriteToConsole(Library.MaxConsoleWidth);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("COMMANDS:".Length));

			"""
			help     : Show this help message
			init     : Create sfumato-example.css file in the current path
			version  : Show the Sfumato version number
			build    : Perform a build of the specified file(s)
			watch    : Perform a build of the specified file(s) and watch for changes

			OPTIONS:
			""".WriteToConsole(Library.MaxConsoleWidth);
            await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("OPTIONS:".Length));

			"""
			[file]   : Path to a CSS file that has Sfumato project settings
			--minify : Minify CSS output; overrides project setting

			Options must be specified as [file] [--minify] and repeat in pairs.
			
			EXAMPLE:
			""".WriteToConsole(Library.MaxConsoleWidth);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("EXAMPLE:".Length));
			
			"""
			sfumato watch client/css/source.css --minify server/css/source.css --minify

			COMPATIBILITY:
			""".WriteToConsole(Library.MaxConsoleWidth);
			await Console.Out.WriteLineAsync(Strings.ThinLine.Repeat("COMPATIBILITY:".Length));
			
			"""
			If you need to continue to use the prior version of Sfumato, install the sfumato-scss compatibility tool._
			This allows the `sfumato` command to launch the older version when it sees CLI arguments for that version._
			Install the compatibility tool with the command below: 
			
			dotnet tool install --global argentini.sfumato-scss
			
			This older version can also be run by itself with the command `sfumato-scss`.
			""".WriteToConsole(Library.MaxConsoleWidth);
			
            await Console.Out.WriteLineAsync();

			Environment.Exit(0);

			return;
		}

		if (appState.BuildMode || appState.WatchMode)
		{
			foreach (var appRunner in appState.AppRunners)
			{
				var options =
				(
					(appRunner.AppRunnerSettings.UseReset ? "CSS Reset, " : string.Empty) +
					(appRunner.AppRunnerSettings.UseForms ? "Forms CSS, " : string.Empty) +
					(appRunner.AppRunnerSettings.UseMinify ? "Compressed Output, " : "Expanded Output, ")
				).Trim(',', ' ');

				var maxWidth = Library.MaxConsoleWidth - 15;
				var relativePath = Path.GetFullPath(appRunner.AppRunnerSettings.CssFilePath)
					.TruncateCenter((int)Math.Floor(maxWidth / 3d), (int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);

				await Console.Out.WriteLineAsync($"CSS Source  :  {relativePath}");
				await Console.Out.WriteLineAsync(
					$"Options     :  {(string.IsNullOrEmpty(options) ? "None" : options)}");

				foreach (var path in appRunner.AppRunnerSettings.AbsolutePaths)
				{
					var relativePath2 = path.TruncateCenter((int)Math.Floor(maxWidth / 3d),
						(int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
					await Console.Out.WriteLineAsync($"Path        :  {relativePath2}");
				}

				foreach (var path in appRunner.AppRunnerSettings.AbsoluteNotPaths)
				{
					var relativePath2 = path.TruncateCenter((int)Math.Floor(maxWidth / 3d),
						(int)Math.Floor((maxWidth / 3d) * 2) - 3, maxWidth);
					await Console.Out.WriteLineAsync($"Ignore      :  {relativePath2}");
				}

				await Console.Out.WriteLineAsync(appRunner == appState.AppRunners.Last()
					? Strings.ThickLine.Repeat(Library.MaxConsoleWidth)
					: Strings.DotLine.Repeat(Library.MaxConsoleWidth));
			}

			var tasks = new List<Task>();

			foreach (var appRunner in appState.AppRunners)
			{
				await appRunner.AddCssPathMessageAsync();

				tasks.Add(appRunner.PerformFileScanAsync());
			}

			await Task.WhenAll(tasks);
			tasks.Clear();

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

			await Console.Out.WriteLineAsync();

			if (appState.WatchMode)
			{
				await Console.Out.WriteLineAsync("Shutting down...");

				foreach (var appRunner in appState.AppRunners)
					await appRunner.ShutDownWatchersAsync();
			}
		}

		await Console.Out.WriteLineAsync($"Sfumato stopped at {DateTime.Now:HH:mm:ss.fff}");
		await Console.Out.WriteLineAsync();
		
		Environment.Exit(0);
	}
}
