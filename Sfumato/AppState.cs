// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Sfumato.Entities.Runners;
using Sfumato.Helpers;

namespace Sfumato;

public sealed class AppState(ObjectPool<StringBuilder> stringBuilderPool)
{
    #region Run Mode Properties

    public ObjectPool<StringBuilder> StringBuilderPool { get; } = stringBuilderPool;
    public bool BuildMode { get; set; }
    public bool WatchMode { get; set; }
    public bool VersionMode { get; set; }
    public bool InitMode { get; set; }
    public bool HelpMode { get; set; }
	
    #endregion
    
    #region Collection Properties

    private List<string> CliArguments { get; } = [];
    public List<AppRunner> AppRunners { get; } = [];
    
    #endregion

    #region Entry Points
    
    public async Task<string> InitializeAsync(IEnumerable<string> args)
    {
	    return await ProcessCliArgumentsAsync(args);
    }

    #endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	private async Task<string> ProcessCliArgumentsAsync(IEnumerable<string>? args)
	{
        CliArguments.Clear();
		CliArguments.AddRange(args?.ToList() ?? []);

		if (CliArguments.Count < 1)
			CliArguments.Add("help");

		if (CliArguments[0] != "help" && CliArguments[0] != "version" && CliArguments[0] != "build" && CliArguments[0] != "watch" && CliArguments[0] != "init")
		{
			await Console.Out.WriteLineAsync("Invalid command specified; must be: help, init, version, build, or watch");
			await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");

			return string.Empty;
		}			
		
		switch (CliArguments[0])
		{
			case "help":
				HelpMode = true;
				return string.Empty;
			case "version":
				VersionMode = true;
				return string.Empty;
			case "init":
				InitMode = true;
				return string.Empty;
			case "build":
				BuildMode = true;
				break;
			case "watch":
				WatchMode = true;
				break;
		}

		if (CliArguments.Count < 2)
			return $"{Constants.CliErrorPrefix}Must include one or more CSS files";

		for (var x = 1; x < CliArguments.Count; x++)
		{
			if (string.IsNullOrEmpty(CliArguments[x]) || string.IsNullOrWhiteSpace(CliArguments[x]))
				continue;
				
			var arg = CliArguments[x].SetNativePathSeparators();

			if (arg.EndsWith(".css", StringComparison.OrdinalIgnoreCase) == false)
				return $"{Constants.CliErrorPrefix}Invalid CSS file argument: {arg}";

			if (File.Exists(arg))
			{
				var useMinify = CliArguments.Count >= x + 2 && CliArguments[x + 1] == "--minify";

				if (useMinify)
					x++;

				AppRunners.Add(new AppRunner(StringBuilderPool, arg, useMinify));
			}
			else
			{
				return $"{Constants.CliErrorPrefix}CSS file not found: {arg}";
			}
		}

		foreach (var appRunner in AppRunners)
		{
			if (await appRunner.LoadCssFileAsync() == false)
			{
				return $"Could not load settings from file: {appRunner.AppRunnerSettings.CssFilePath}";
			}
		}

		return string.Empty;
	}

    #endregion
}
