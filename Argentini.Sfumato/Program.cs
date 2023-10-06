using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Argentini.Sfumato;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var timer = new Stopwatch();

		timer.Start();

		Console.OutputEncoding = Encoding.UTF8;
		
		var runner = new SfumatoRunner();

		await runner.InitializeAsync(args);

		Console.WriteLine($"Sfumato Version {Identify.Version(Assembly.GetExecutingAssembly())}");
		
		if (runner.AppState.VersionMode)
		{
			Environment.Exit(0);
		}
		
		Console.WriteLine("=".Repeat(SfumatoRunner.MaxConsoleWidth));
		
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
		Console.WriteLine($"CSS Output Path  :  .{runner.AppState.Settings.CssOutputPath.TrimStart(runner.AppState.WorkingPath)}");

		if (runner.AppState.Settings.ProjectPaths.Count > 0)
		{
			var paths = string.Empty;
	        
			foreach (var path in runner.AppState.Settings.ProjectPaths)
			{
				if (string.IsNullOrEmpty(paths) == false)
					paths += "                 :  ";

				paths += $".{path.Path.TrimStart(runner.AppState.WorkingPath)}/{path.FileSpec}{(path.Recurse ? " (Recurse)" : string.Empty)}{Environment.NewLine}";
			}
	        
			Console.WriteLine($"Include Path(s)  :  {paths.TrimEnd()}");
		}        

		Console.WriteLine("=".Repeat(SfumatoRunner.MaxConsoleWidth));
		Console.WriteLine($"{(runner.AppState.WatchMode ? "Watching for Changes..." : "Building...")}");

        await runner.GenerateProjectCssAsync();
		
		#region Watcher Mode

		if (runner.AppState.WatchMode)
		{
			// WATCH HERE
		}

		#endregion
		
		Console.WriteLine($"Completed build in {timer.Elapsed.TotalSeconds:N2} seconds at {DateTime.Now:HH:mm:ss.fff}");
		Console.WriteLine();

		if (runner.AppState.DiagnosticMode)
		{
			Console.WriteLine("DIAGNOSTICS:");
			Console.WriteLine(runner.AppState.DiagnosticOutput.ToString());
		}

		Environment.Exit(0);
	}
}

public class FileChangeRequest
{
    public string ChangeType { get; set; } = string.Empty;
    public string FileSpec { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}