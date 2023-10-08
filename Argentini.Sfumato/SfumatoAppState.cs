namespace Argentini.Sfumato;

public sealed class SfumatoAppState
{
    #region Properties

    #region Constants

    public static string CliErrorPrefix => "Sfumato => ";
    
    #endregion

    #region Regex
    
    public Regex ArbitraryCssRegex { get; }
    public Regex CoreClassRegex { get; }
    
    #endregion
    
    #region Run Modes

    public bool ReleaseMode { get; set; }
    public bool WatchMode { get; set; }
    public bool VersionMode { get; set; }
    public bool HelpMode { get; set; }
    public bool DiagnosticMode { get; set; }
	
    #endregion
    
    #region Scss Class Collections

    public ScssClassCollection ScssClassCollection { get; } = new();    
    
    #endregion
    
    #region App State

    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public List<string> CliArguments { get; } = new();
    public SfumatoJsonSettings Settings { get; set; } = new();
    public StringBuilder DiagnosticOutput { get; set; } = new();
    public string WorkingPathOverride { get; set; } = string.Empty;
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public Dictionary<string,ScssClass> UsedClasses { get; } = new();
    public List<string> AllPrefixes { get; } = new();
    public StringBuilder ScssCoreInjectable { get; } = new();
    public StringBuilder ScssSharedInjectable { get; } = new();
    
    #endregion

    #endregion

    public SfumatoAppState()
    {
	    AllPrefixes.Clear();
	    AllPrefixes.AddRange(SfumatoScss.MediaQueryPrefixes.Select(p => p.Key));
	    AllPrefixes.AddRange(SfumatoScss.PseudoclassPrefixes.Select(p => p.Key));
	    
	    ArbitraryCssRegex = new Regex($$"""(?<=[\s"'`])(({{string.Join(":|", AllPrefixes) + ":"}}){0,10}(\[({{string.Join("|", SfumatoScss.CssPropertyNames)}})\:[a-z0-9%/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\]))(?=[\s"'`])""", RegexOptions.Compiled);
	    CoreClassRegex = new Regex($$"""(?<=[\s"'`])(({{string.Join(":|", AllPrefixes) + ":"}}){0,10}((({{string.Join("|", ScssClassCollection.GetClassPrefixesForRegex())}})[a-z0-9\-]{1,100}((/[a-z0-9\.]{1,})|(/\[[a-z0-9%/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\])){0,1})|(({{string.Join("|", ScssClassCollection.GetClassPrefixesForRegex())}})\[[a-z0-9%/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\])))(?=[\s"'`])""", RegexOptions.Compiled);
    }
    
    #region Entry Points
    
    /// <summary>
    /// Initialize the app state. Loads settings JSON file from working path.
    /// Sets up runtime environment for the runner.
    /// </summary>
    /// <param name="args">CLI arguments</param>
    public async Task InitializeAsync(IEnumerable<string> args)
    {
	    var timer = new Stopwatch();

	    timer.Start();

	    ProcessCliArguments(args);

	    if (VersionMode || HelpMode)
		    return;
	    
	    #region Find sfumato.json file

        if (string.IsNullOrEmpty(WorkingPathOverride) == false)
            WorkingPath = WorkingPathOverride;
        
        SettingsFilePath = Path.Combine(WorkingPath, "sfumato.json");

        if (File.Exists(SettingsFilePath) == false)
        {
            Console.WriteLine($"{CliErrorPrefix}Could not find settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        #endregion
        
        try
        {
            #region Load sfumato.json file

            var json = await File.ReadAllTextAsync(SettingsFilePath);
            var jsonSettings = JsonSerializer.Deserialize<SfumatoJsonSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IncludeFields = true
            });
		
            if (jsonSettings is null)
            {
                Console.WriteLine($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
                Environment.Exit(1);
            }
            
            #endregion
            
            #region Import settings
            
            Settings.CssOutputPath = Path.Combine(WorkingPath, jsonSettings.CssOutputPath.SetNativePathSeparators());
        
            if (Directory.Exists(Settings.CssOutputPath) == false)
            {
                Console.WriteLine($"{CliErrorPrefix}Could not find CSS output path: {Settings.CssOutputPath}");
                Environment.Exit(1);
            }
        
            Settings.ThemeMode = jsonSettings.ThemeMode switch
            {
                "system" => "system",
                "class" => "class",
                _ => "system"
            };

            SassCliPath = GetEmbeddedSassPath();
            ScssPath = GetEmbeddedScssPath();

            Settings.ProjectPaths.Clear();
        
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
                projectPath.Path = Path.Combine(WorkingPath, projectPath.Path.SetNativePathSeparators());

                if (string.IsNullOrEmpty(projectPath.FileSpec))
                    return;
            
                var tempFileSpec = projectPath.FileSpec.Replace("*", string.Empty).Replace(".", string.Empty);

                if (string.IsNullOrEmpty(tempFileSpec) == false)
                    projectPath.FileSpec = $"*.{tempFileSpec}";
            
                Settings.ProjectPaths.Add(projectPath);
            }

            jsonSettings.Breakpoints.Adapt(Settings.Breakpoints);
            jsonSettings.FontSizeViewportUnits.Adapt(Settings.FontSizeViewportUnits);
            
            #endregion
        }

        catch
        {
            Console.WriteLine($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        DiagnosticOutput.Append($"Initialized settings in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

        ScssCoreInjectable.Clear();
        ScssCoreInjectable.Append(await SfumatoScss.GetCoreScssAsync(this, DiagnosticOutput));

        ScssSharedInjectable.Clear();
        ScssSharedInjectable.Append(await SfumatoScss.GetSharedScssAsync(this, DiagnosticOutput));

        timer.Restart();
        
        DiagnosticOutput.Append($"Identified {ScssClassCollection.GetClassCount():N0} available classes in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
    }
    
    #endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	public void ProcessCliArguments(IEnumerable<string>? args)
	{
		CliArguments.Clear();
		CliArguments.AddRange(args?.ToList() ?? new List<string>());

		if (CliArguments.Count < 1)
			return;
		
		foreach (var arg in CliArguments)
		{
			if (arg.StartsWith("--release", StringComparison.InvariantCultureIgnoreCase))
				ReleaseMode = true;
					
			else if (arg.StartsWith("--watch", StringComparison.InvariantCultureIgnoreCase))
				WatchMode = true;

			else if (arg.StartsWith("--help", StringComparison.InvariantCultureIgnoreCase))
				HelpMode = true;

			else if (arg.StartsWith("--version", StringComparison.InvariantCultureIgnoreCase))
				VersionMode = true;

			else if (arg.StartsWith("--diagnostics", StringComparison.InvariantCultureIgnoreCase))
				DiagnosticMode = true;
			
			else if (arg.StartsWith("--path", StringComparison.InvariantCultureIgnoreCase))
				if (CliArguments.Count - 1 >= CliArguments.IndexOf(arg) + 1)
				{
					var path = CliArguments[CliArguments.IndexOf(arg) + 1].SetNativePathSeparators();

					if (path.Contains(Path.DirectorySeparatorChar) == false && path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
						continue;
					
					if (path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
						path = path[..path.LastIndexOf(Path.DirectorySeparatorChar)];

					try
					{
						WorkingPathOverride = Path.GetFullPath(path);
					}

					catch
					{
						Console.WriteLine($"{CliErrorPrefix}Invalid project path at {path}");
						Environment.Exit(1);
					}
				}
		}
	}
	
    public static string GetWorkingPath()
    {
        var workingPath = Directory.GetCurrentDirectory();
        
#if DEBUG
        var index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

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

    public string GetEmbeddedSassPath()
    {
        var osPlatform = Identify.GetOsPlatform();
        var processorArchitecture = Identify.GetProcessorArchitecture();
        var sassPath = string.Empty;
		var workingPath = Assembly.GetExecutingAssembly().Location;

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
				if (processorArchitecture is Architecture.X64 or Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-windows-x64", "sass.bat");
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
			Console.WriteLine($"{CliErrorPrefix}Embedded Dart Sass cannot be found.");
			Environment.Exit(1);
		}
		
		var sb = StringBuilderPool.Get();
		var cmd = Cli.Wrap(sassPath)
			.WithArguments(arguments =>
			{
				arguments.Add("--version");
			})
			.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
			.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

		try
		{
			_ = cmd.ExecuteAsync().GetAwaiter().GetResult();
		}

		catch
		{
			Console.WriteLine($"{CliErrorPrefix}Dart Sass is embedded but cannot be found.");
			Environment.Exit(1);
		}

		StringBuilderPool.Return(sb);
		
		return sassPath;
    }

    public static string GetEmbeddedScssPath()
    {
	    var workingPath = Assembly.GetExecutingAssembly().Location;

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
            Console.WriteLine($"{CliErrorPrefix}Embedded SCSS resources cannot be found.");
            Environment.Exit(1);
        }
        
		return workingPath;
	}
    
    #endregion
    
    #region Runner Methods

    /// <summary>
    /// Normalize prefixes to start with theme mode then breakpoint then pseudoclasses.
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    private static string ReOrderPrefixes(string userClassName)
    {
	    var result = userClassName;
	    
	    foreach (var breakpoint in new[] { "zero:", "phab:", "tabp:", "tabl:", "note:", "desk:", "elas:" })
	    {
		    if (result.Contains(breakpoint, StringComparison.Ordinal))
			    result = $"{breakpoint}{result.Replace(breakpoint, string.Empty, StringComparison.Ordinal)}";
	    }
				
	    if (result.Contains("dark:", StringComparison.Ordinal))
		    result = $"dark:{userClassName.Replace("dark:", string.Empty, StringComparison.Ordinal)}";

	    return result;
    }
    
	/// <summary>
	/// Identify the used classes in the project.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GatherUsedScssCoreClassesAsync()
	{
		var timer = new Stopwatch();

		timer.Start();

		Console.Write("=> Identifying used classes...");
		
		UsedClasses.Clear();

		if (Settings.ProjectPaths.Count == 0)
		{
			Console.WriteLine(" no project paths specified");
			return;
		}

		foreach (var projectPath in Settings.ProjectPaths)
			await RecurseProjectPathForUsedScssCoreClassesAsync(projectPath.Path, projectPath.FileSpec, projectPath.Recurse);
		
		if (UsedClasses.Count == 0)
			Console.WriteLine(" no classes used");
		else
			Console.WriteLine($" found {UsedClasses.Count:N0}/{ScssClassCollection.GetClassCount():N0} classes");
		
		DiagnosticOutput.Append($"Identified {UsedClasses.Count:N0} used classes in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
	}
	private async Task RecurseProjectPathForUsedScssCoreClassesAsync(string? sourcePath, string fileSpec, bool recurse = false)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty())
			return;

		var dir = new DirectoryInfo(sourcePath);

		if (dir.Exists == false)
		{
			Console.WriteLine($"Source directory does not exist or could not be found: {sourcePath}");
			Environment.Exit(1);
		}

		var dirs = dir.GetDirectories();
		var files = dir.GetFiles();

		foreach (var projectFile in files.OrderBy(f => f.Name))
		{
			if (projectFile.Name.EndsWith($"{fileSpec.TrimStart('*')}", StringComparison.InvariantCultureIgnoreCase) == false)
				continue;
			
			var markup = await File.ReadAllTextAsync(projectFile.FullName);

			if (string.IsNullOrEmpty(markup))
				continue;

			var matches = new List<Match>();
				
			matches.AddRange(CoreClassRegex.Matches(markup));

			foreach (var match in matches)
			{
				var userClassName = ReOrderPrefixes(match.Value);

				if (UsedClasses.ContainsKey(userClassName))
					continue;

				var scssClasses = ScssClassCollection.GetAllByClassName(userClassName);
				var userClassValueType = userClassName.GetUserClassValueType();
				var userClassValue = userClassName.GetUserClassValue().Replace('_', ' ').Replace("\\ ", "\\_");

				foreach (var scssClass in scssClasses)
				{
					// 1. No arbitrary value type specified (e.g. text-slate-100 or text-[#112233])
					// 2. Arbitrary value type specified, must match source class value type (e.g. text-[color:#112233])
					
					if (string.IsNullOrEmpty(userClassValueType) == false && scssClass.ValueTypes.Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(userClassValueType) == false)
						continue;
					
					var usedScssClass = scssClass.Adapt<ScssClass>();

					usedScssClass.UserClassName = userClassName;

					if (string.IsNullOrEmpty(userClassValue) == false)
						usedScssClass.Value = userClassValue;

					UsedClasses.TryAdd(userClassName, usedScssClass);
				}
			}
			
			matches.AddRange(ArbitraryCssRegex.Matches(markup));

			foreach (var match in matches)
			{
				var userClassName = ReOrderPrefixes(match.Value);
				
				if (UsedClasses.ContainsKey(userClassName))
					continue;

				// 1. Arbitrary CSS style (e.g. tabp:[display:none])

				var usedScssClass = new ScssClass
				{
					ValueTypes = string.Empty,
					UserClassName = userClassName,
					Value = userClassName[(userClassName.IndexOf('[') + 1)..].TrimEnd(']').Replace('_', ' ').Replace("\\ ", "\\_"),
					Template = "{value};"
				};

				UsedClasses.TryAdd(userClassName, usedScssClass);
			}
		}

		if (recurse)
			foreach (var subDir in dirs.OrderBy(d => d.Name))
				await RecurseProjectPathForUsedScssCoreClassesAsync(subDir.FullName, fileSpec);
	}

	#endregion
}
