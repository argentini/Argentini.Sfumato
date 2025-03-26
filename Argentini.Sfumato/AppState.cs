using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Entities.Library;
using Argentini.Sfumato.Entities.Yaml;
using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato;

public sealed class AppState
{
	public Library Library { get; set; } = new();
	
    #region Run Mode Properties

    public bool Minify { get; set; }
    public bool WatchMode { get; set; }
    public bool VersionMode { get; set; }
    public bool InitMode { get; set; }
    public bool HelpMode { get; set; }
    public bool DiagnosticMode { get; set; }
	
    #endregion
    
    #region Collection Properties

    private List<string> CliArguments { get; } = [];
    
    #endregion
    
    #region App State Properties

    public static string CliErrorPrefix => "Sfumato => ";
    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public Settings Settings { get; set; } = new();
    public ConcurrentDictionary<string,string> DiagnosticOutput { get; set; } = new(StringComparer.Ordinal);
    public string WorkingPathOverride { get; private set; } = string.Empty;
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; private set; } = string.Empty;
    public string ScssPath { get; private set; } = string.Empty;
    public string YamlPath { get; private set; } = string.Empty;
    
    #endregion

    // ReSharper disable once EmptyConstructor
    public AppState()
    {
	}
    
    #region Entry Points
    
    /// <summary>
    /// Initialize the app state. Loads settings file from working path.
    /// Sets up runtime environment for the runner.
    /// </summary>
    /// <param name="args">CLI arguments</param>
    public async Task InitializeAsync(IEnumerable<string> args)
    {
	    var timer = new Stopwatch();
	    
	    DiagnosticOutput.Clear();
	    
	    await ProcessCliArgumentsAsync(args);
	    
	    timer.Start();
	    
	    if (VersionMode == false && HelpMode == false && InitMode == false)
		    await Settings.LoadSettingsAsync(this);
	    
	    #region Find Embedded Resources (Sass, SCSS)
	    
	    SassCliPath = await GetEmbeddedSassPathAsync();
	    ScssPath = await GetEmbeddedScssPathAsync();
	    YamlPath = await GetEmbeddedYamlPathAsync();
	    
	    #endregion

	    if (VersionMode || HelpMode || InitMode)
		    return;

        if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init001", $"{Strings.TriangleRight} Processed settings in {timer.FormatTimer()}{Environment.NewLine}");
    }

    #endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	private async Task ProcessCliArgumentsAsync(IEnumerable<string>? args)
	{

// #if DEBUG
//         Minify = true;
// #endif

        CliArguments.Clear();
		CliArguments.AddRange(args?.ToList() ?? new List<string>());

		if (CliArguments.Count < 1)
			return;

		if (CliArguments[0] != "help" && CliArguments[0] != "version" && CliArguments[0] != "build" && CliArguments[0] != "watch" && CliArguments[0] != "init")
		{
			await Console.Out.WriteLineAsync("Invalid command specified; must be: help, init, version, build, or watch");
			await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
			Environment.Exit(1);
		}			
		
		if (CliArguments[0] == "help")
		{
			HelpMode = true;
			return;
		}

		if (CliArguments[0] == "version")
		{
			VersionMode = true;
			return;
		}

		if (CliArguments[0] == "watch")
		{
			WatchMode = true;
		}
	
		if (CliArguments[0] == "init")
		{
			InitMode = true;
		}

		if (CliArguments.Count > 1)
		{
			for (var x = 1; x < CliArguments.Count; x++)
			{
				var arg = CliArguments[x];
				
				if (arg.Equals("--minify", StringComparison.OrdinalIgnoreCase))
					Minify = true;

				else if (arg.Equals("--path", StringComparison.OrdinalIgnoreCase))
					if (++x < CliArguments.Count)
					{
						var path = CliArguments[x].SetNativePathSeparators();

						if (path.Contains(Path.DirectorySeparatorChar) == false &&
							path.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
							continue;

						if (path.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
							path = path[..path.LastIndexOf(Path.DirectorySeparatorChar)];

						try
						{
							WorkingPathOverride = Path.GetFullPath(path);
						}

						catch
						{
							await Console.Out.WriteLineAsync($"{CliErrorPrefix}Invalid project path at {path}");
							Environment.Exit(1);
						}
					}
			}
		}
	}

	private static string GetWorkingPath()
    {
        var workingPath = Directory.GetCurrentDirectory();
        
#if DEBUG
        var index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);
        
        /*
        if (index == -1)
	        index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato.Tests" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);
        index = workingPath[..index].TrimEnd(Path.DirectorySeparatorChar).LastIndexOf(Path.DirectorySeparatorChar);
        return Path.Combine(workingPath[..index], "Coursabi", "Coursabi.Apps", "Coursabi.Apps.Client", "Coursabi.Apps.Client");        
        */
        // return Path.Combine(workingPath[..index], "Fynydd-Website-2024", "UmbracoCms");        

        if (index > -1)
        {
            workingPath = Path.Combine(workingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
        }
        
        else
        {
            index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato.Tests" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);
        
            if (index > -1)
                workingPath = Path.Combine(workingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
        }

#endif
        
        return workingPath;
    }

    public async Task<string> GetEmbeddedSassVersionAsync()
    {
        var sassPath = await GetEmbeddedSassPathAsync();
        var sb = StringBuilderPool.Get();

        try
        {
            var cmd = Cli.Wrap(sassPath)
                .WithArguments(arguments => { arguments.Add("--version"); })
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

            await cmd.ExecuteAsync();

            return sb.ToString().Trim();
        }

        catch
        {
            await Console.Out.WriteLineAsync("Dart Sass is embedded but cannot be found.");
            Environment.Exit(1);
        }

        finally
        {
            StringBuilderPool.Return(sb);
        }

        return string.Empty;
    }

    private async Task<string> GetEmbeddedSassPathAsync()
    {
        var osPlatform = Identify.GetOsPlatform();
        var processorArchitecture = Identify.GetProcessorArchitecture();
        var sassPath = string.Empty;
		var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

		while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
		{
			workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];

#if DEBUG
			if (Directory.Exists(Path.Combine(workingPath, "sass")) == false)
				continue;

			var tempPath = workingPath; 
			
			workingPath = Path.Combine(tempPath, "sass");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "sass");
#endif
			
			if (osPlatform == OSPlatform.Windows)
			{
				if (processorArchitecture is Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-windows-x64", "sass.bat");
                else if (processorArchitecture == Architecture.Arm64)
                    sassPath = Path.Combine(workingPath, "dart-sass-windows-arm64", "sass.bat");
			}
				
			else if (osPlatform == OSPlatform.OSX)
			{
				if (processorArchitecture == Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-macos-x64", "sass");
				else if (processorArchitecture == Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-macos-arm64", "sass");
			}
				
			else if (osPlatform == OSPlatform.Linux)
			{
				if (processorArchitecture == Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-linux-x64", "sass");
				else if (processorArchitecture == Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-linux-arm64", "sass");
			}

			break;
		}
		
		if (string.IsNullOrEmpty(sassPath) || File.Exists(sassPath) == false)
		{
			await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded Dart Sass cannot be found.");
			Environment.Exit(1);
		}
		
		var sb = StringBuilderPool.Get();

        try
        {
		    var cmd = Cli.Wrap(sassPath)
			    .WithArguments(arguments =>
			    {
				    arguments.Add("--version");
			    })
			    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
			    .WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

                await cmd.ExecuteAsync();
        }

        catch
        {
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Dart Sass is embedded but cannot be found.");
            Environment.Exit(1);
        }

        finally
        {
            StringBuilderPool.Return(sb);
        }
        
		return sassPath;
    }

    private static async Task<string> GetEmbeddedScssPathAsync()
    {
	    var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];
            
#if DEBUG
		    if (Directory.Exists(Path.Combine(workingPath, "scss")) == false)
			    continue;

		    var tempPath = workingPath; 
			
		    workingPath = Path.Combine(tempPath, "scss");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "scss");
#endif
		    break;
		}

        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
        {
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded SCSS resources cannot be found.");
            Environment.Exit(1);
        }
        
		return workingPath;
	}

    private static async Task<string> GetEmbeddedYamlPathAsync()
    {
	    var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];
            
#if DEBUG
		    if (Directory.Exists(Path.Combine(workingPath, "yaml")) == false)
			    continue;

		    var tempPath = workingPath; 
			
		    workingPath = Path.Combine(tempPath, "yaml");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "yaml");
#endif
		    break;
	    }

	    // ReSharper disable once InvertIf
	    if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
	    {
		    await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded YAML resources cannot be found.");
		    Environment.Exit(1);
	    }
        
	    return workingPath;
    }
    
    #endregion
}
