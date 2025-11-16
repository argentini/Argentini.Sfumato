using System.Reflection;
using System.Threading.Tasks.Dataflow;
using Sfumato.Entities.Library;
using Sfumato.Entities.Messenger;
using Sfumato.Entities.Runners;
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable RedundantBoolCompare
// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable MemberCanBePrivate.Global

namespace Sfumato;

public sealed class Service
{
	private static readonly WeakMessenger Messenger = new ();
	private static ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
	private static AppState AppState { get; } = new (StringBuilderPool);

	public static SfumatoConfiguration Configuration { get; } = new();
	public static readonly ActionBlock<AppRunner> Dispatcher = new (appRunner => Messenger.Send(appRunner), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });

	public static void StartWatcher(string relativeCssFilePath, CancellationTokenSource? cancellationTokenSource = null)
	{
		Configuration.Arguments = ["watch", relativeCssFilePath];

		_ = RunAsync(cancellationTokenSource ?? new CancellationTokenSource());
	}

	public static void PerformBuild(string relativeCssFilePath)
	{
		Configuration.Arguments = ["build", relativeCssFilePath];

		_ = RunAsync(new CancellationTokenSource());
	}

	public static async Task<bool> RunAsync(CancellationTokenSource cancellationTokenSource)
	{
		if (Configuration.Arguments?.Length is null)
		{
			"Service.Arguments is null or empty.".WriteToOutput();

			return false;
		}
		
		Console.OutputEncoding = Encoding.UTF8;
		
		var assembly = Assembly.Load("Sfumato");
		var version = await Identify.VersionAsync(assembly);

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
		var argumentErrorMessage = await AppState.InitializeAsync(Configuration.Arguments ?? []);

		if (AppState.VersionMode)
		{
			$"Sfumato Version {version}".WriteToOutput();
			return true;
		}
		
		Strings.ThickLine.Repeat(Library.MaxConsoleWidth).WriteToOutput();
		"Sfumato: The Ultra-Fast CSS Generation Tool".WriteToOutput();
		$"Version {version} for {Identify.GetOsPlatformName()} ({Identify.GetProcessorArchitecture()})".WriteToOutput();
		
		Strings.ThickLine.Repeat(Library.MaxConsoleWidth).WriteToOutput();

		if (string.IsNullOrEmpty(argumentErrorMessage) == false)
		{
			argumentErrorMessage.WriteToOutput();
			return true;
		}

		if (AppState.InitMode)
		{
            var cssReferenceFile = await Storage.ReadAllTextWithRetriesAsync(Path.Combine(Constants.EmbeddedCssPath, "sfumato-example.css"), Library.FileAccessRetryMs);

			await File.WriteAllTextAsync(Path.Combine(Constants.WorkingPath, "sfumato-example.css"), cssReferenceFile);

			"".WriteToOutput();
			$"Created sfumato-example.css file at {Constants.WorkingPath}".WriteToOutput();
			"".WriteToOutput();
			
			return true;
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
			If you need to continue to use the prior version of Sfumato, install sfumato-scss tool. This should be used for older projects relying on that version.

			Install the compatibility tool with the command below: 
			
			dotnet tool install --global argentini.sfumato-scss
			""".WriteToOutput();
			
            "".WriteToOutput();

			return true;
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

				if (Configuration.UsingCli)
				{
					"Watching; press ESC to exit".WriteToOutput();
				}
				else
				{
					"Watching for changes".WriteToOutput();
					"".WriteToOutput();
				}

				foreach (var appRunner in AppState.AppRunners)
					await appRunner.StartWatchingAsync();

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

					if (cancellationTokenSource.IsCancellationRequested)
						break;

					var performedWork = false;

					foreach (var appRunner in AppState.AppRunners)
					{
						var result = await appRunner.ProcessWatchQueues();

						if (performedWork == false && result)
							performedWork = true;
					}

					if (performedWork == false)
						continue;
					
					do
					{
						await Task.Delay(25, cancellationTokenSource.Token);

					} while (AppState.AppRunners.Any(r => r.Messages.Count != 0));

					if (Configuration.UsingCli)
						"Watching; press ESC to exit".WriteToOutput();
					else
						"Watching for changes".WriteToOutput();
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

		return true;
	}
}
