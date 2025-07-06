// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato;

public sealed class AppState
{
    #region Run Mode Properties

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
    
    #region App State Properties

    public static string CliErrorPrefix => "Sfumato => ";
    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public string EmbeddedCssPath => GetEmbeddedCssPath();
    public string WorkingPath => Directory.GetCurrentDirectory();
    
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
			return $"{CliErrorPrefix}Must include one or more CSS files";

		for (var x = 1; x < CliArguments.Count; x++)
		{
			if (string.IsNullOrEmpty(CliArguments[x]) || string.IsNullOrWhiteSpace(CliArguments[x]))
				continue;
				
			var arg = CliArguments[x].SetNativePathSeparators();

			if (arg.EndsWith(".css", StringComparison.OrdinalIgnoreCase) == false)
				return $"{CliErrorPrefix}Invalid CSS file argument: {arg}";

			if (File.Exists(arg))
			{
				var useMinify = CliArguments.Count >= x + 2 && CliArguments[x + 1] == "--minify";

				if (useMinify)
					x++;

				AppRunners.Add(new AppRunner(this, arg, useMinify));
			}
			else
			{
				return $"{CliErrorPrefix}CSS file not found: {arg}";
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

    private static string GetEmbeddedCssPath()
    {
	    var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];

		    if (Directory.Exists(Path.Combine(workingPath, "contentFiles")))
		    {
			    workingPath = Path.Combine(workingPath, "contentFiles", "any", "any", "css");
			    break;
		    }		    
			
		    if (Directory.Exists(Path.Combine(workingPath, "css")))
		    {
			    workingPath = Path.Combine(workingPath, "css");
			    break;
		    }		    
		}

        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
        {
            Console.WriteLine($"{CliErrorPrefix}Embedded CSS resources cannot be found.");
            Environment.Exit(1);
        }
        
		return workingPath;
	}
    
    #endregion
}
