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

		var runner = new SfumatoRunner(args);

		if (runner.VersionMode)
		{
			Console.WriteLine($"Sfumato Version {Identify.Version(Assembly.GetExecutingAssembly())}");
			Console.WriteLine();

			Environment.Exit(0);
		}
		
		Console.OutputEncoding = Encoding.UTF8;

		var startupTitle = $"Sfumato Version {Identify.Version(Assembly.GetExecutingAssembly())}";
		
		Console.WriteLine();
		Console.WriteLine(startupTitle);
		Console.WriteLine("=".Repeat(SfumatoRunner.MaxConsoleWidth));
		
		if (runner.HelpMode)
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
		
		await runner.InitializeAsync();

		Console.WriteLine($"Build Mode       :  {(runner.ReleaseMode ? "Release" : "Development")}");
		Console.WriteLine($"Theme Mode       :  {(runner.ThemeMode.Equals("system", StringComparison.OrdinalIgnoreCase) ? "System" : "CSS Class")}");
		Console.WriteLine($"Project Path     :  {runner.WorkingPath}");
		Console.WriteLine($"CSS Output Path  :  .{runner.CssOutputPath.TrimStart(runner.WorkingPath)}");

		if (runner.ProjectPaths.Count > 0)
		{
			var paths = string.Empty;
	        
			foreach (var path in runner.ProjectPaths)
			{
				if (string.IsNullOrEmpty(paths) == false)
					paths += "                 :  ";

				paths += $".{path.Path.TrimStart(runner.WorkingPath)}/{path.FileSpec}{(path.Recurse ? " (Recurse)" : string.Empty)}{Environment.NewLine}";
			}
	        
			Console.WriteLine($"Include Path(s)  :  {paths.TrimEnd()}");
		}        

		Console.WriteLine("=".Repeat(SfumatoRunner.MaxConsoleWidth));
		Console.WriteLine($"{(runner.WatchMode ? "Watching for Changes..." : "Building...")}");

        await runner.GenerateProjectScssAsync();
		
		#region Watcher Mode

		if (runner.WatchMode)
		{
			// WATCH HERE
		}

		#endregion
		
		Console.WriteLine($"Completed build in {timer.Elapsed.TotalSeconds:N2} seconds at {DateTime.Now:HH:mm:ss.fff}");
		Console.WriteLine();

		if (runner.DiagnosticMode)
		{
			Console.WriteLine("DIAGNOSTICS:");
			Console.WriteLine(runner.DiagnosticOutput.ToString());
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