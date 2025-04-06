using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato;

public sealed class AppState
{
    #region Run Mode Properties

    public bool WatchMode { get; set; }
    public bool VersionMode { get; set; }
    public bool InitMode { get; set; }
    public bool HelpMode { get; set; }
	
    #endregion
    
    #region Collection Properties

    private List<string> CliArguments { get; } = [];
    private List<AppRunner> AppRunners { get; } = [];
    
    #endregion
    
    #region App State Properties

    public static string CliErrorPrefix => "Sfumato => ";
    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public string EmbeddedCssPath { get; set; } = string.Empty;
    
    #endregion

    #region Entry Points
    
    /// <summary>
    /// Initialize the app state. Loads settings file from working path.
    /// Sets up runtime environment for the runner.
    /// </summary>
    /// <param name="args">CLI arguments</param>
    public async Task InitializeAsync(IEnumerable<string> args)
    {
	    await ProcessCliArgumentsAsync(args);
	    
	    if (VersionMode == false && HelpMode == false && InitMode == false)
		    EmbeddedCssPath = await GetEmbeddedCssPathAsync();
    }

    #endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	private async Task ProcessCliArgumentsAsync(IEnumerable<string>? args)
	{
        CliArguments.Clear();
		CliArguments.AddRange(args?.ToList() ?? []);

		if (CliArguments.Count < 1)
			CliArguments.Add("help");

		if (CliArguments[0] != "help" && CliArguments[0] != "version" && CliArguments[0] != "build" && CliArguments[0] != "watch" && CliArguments[0] != "init")
		{
			await Console.Out.WriteLineAsync("Invalid command specified; must be: help, init, version, build, or watch");
			await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
			Environment.Exit(1);
		}			
		
		switch (CliArguments[0])
		{
			case "help":
				HelpMode = true;
				return;
			case "version":
				VersionMode = true;
				return;
			case "watch":
				WatchMode = true;
				break;
			case "init":
				InitMode = true;
				break;
		}

		if (CliArguments.Count > 1)
		{
			for (var x = 1; x < CliArguments.Count; x++)
			{
				if (string.IsNullOrEmpty(CliArguments[x]) || string.IsNullOrWhiteSpace(CliArguments[x]))
					continue;
				
				var arg = CliArguments[x].SetNativePathSeparators();

				if (arg.EndsWith(".css", StringComparison.OrdinalIgnoreCase) == false)
					continue;
				
				if (File.Exists(arg))
				{
					var useMinify = CliArguments.Count >= x + 2 && CliArguments[x + 1] == "--minify";

					if (useMinify)
						x++;

					AppRunners.Add(new AppRunner(this, arg, useMinify));
				}
				else
				{
					await Console.Out.WriteLineAsync($"{CliErrorPrefix}File not found: {arg}");
					Environment.Exit(1);
				}
			}
		}
	}

    private static async Task<string> GetEmbeddedCssPathAsync()
    {
	    var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];
            
#if DEBUG
		    if (Directory.Exists(Path.Combine(workingPath, "css")) == false)
			    continue;

		    var tempPath = workingPath; 
			
		    workingPath = Path.Combine(tempPath, "css");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "css");
#endif
		    break;
		}

        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
        {
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded CSS resources cannot be found.");
            Environment.Exit(1);
        }
        
		return workingPath;
	}
    
    #endregion
}
