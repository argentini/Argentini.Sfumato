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
    public Regex SfumatoScssIncludesRegex { get; }
    
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
    public ConcurrentDictionary<string,string> DiagnosticOutput { get; set; } = new();
    public string WorkingPathOverride { get; set; } = string.Empty;
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public ConcurrentDictionary<string,ScssClass> UsedClasses { get; } = new();
    public List<string> AllPrefixes { get; } = new();
    public StringBuilder ScssCoreInjectable { get; } = new();
    public StringBuilder ScssSharedInjectable { get; } = new();
    
    #endregion

    #endregion

    public SfumatoAppState()
    {
	    AllPrefixes.Clear();
	    AllPrefixes.AddRange(SfumatoScss.MediaQueryPrefixes.Select(p => p.Prefix));
	    AllPrefixes.AddRange(SfumatoScss.PseudoclassPrefixes.Select(p => p.Key));
	    
	    var arbitraryCssExpression = $$"""
(?<=[\s"'`])
({{string.Join("\\:|", AllPrefixes) + "\\:"}}){0,10}
(\[({{string.Join("|", SfumatoScss.CssPropertyNames)}})\:[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\])
(?=[\s"'`])
""";
	    
	    ArbitraryCssRegex = new Regex(arbitraryCssExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    var coreClassExpression = $$"""
(?<=[\s"'`])
({{string.Join("\\:|", AllPrefixes) + "\\:"}}){0,10}
(
	([a-z\-][a-z0-9\-\.%]{2,100})
	(
		(/[a-z0-9\-\.]{1,100})|([/]{0,1}\[[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\]){0,1}
	)
)
(?=[\s"'`])
""";
	    
	    CoreClassRegex = new Regex(coreClassExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);

	    const string sfumatoScssIncludesRegexExpression = @"^\s*@sfumato\s+((core)|(base))[\s]{0,};\r?\n";
	    
	    SfumatoScssIncludesRegex = new Regex(sfumatoScssIncludesRegexExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
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

            Settings.UseAutoTheme = jsonSettings.UseAutoTheme;
            
            SassCliPath = GetEmbeddedSassPath();
            ScssPath = GetEmbeddedScssPath();

            Settings.ProjectPaths.Clear();
        
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
	            if (string.IsNullOrEmpty(projectPath.FileSpec))
		            continue;
	            
                projectPath.Path = Path.Combine(WorkingPath, projectPath.Path.SetNativePathSeparators());
                
                if (projectPath.FileSpec.Contains('.') && projectPath.FileSpec.StartsWith("*", StringComparison.Ordinal) == false)
                {
	                projectPath.IsFilePath = true;
	                Settings.ProjectPaths.Add(projectPath);

	                continue;
                }
                
                var tempFileSpec = projectPath.FileSpec.Replace("*", string.Empty).Replace(".", string.Empty);

                if (string.IsNullOrEmpty(tempFileSpec) == false)
                    projectPath.FileSpec = $"*.{tempFileSpec}";
            
                Settings.ProjectPaths.Add(projectPath);
            }

            jsonSettings.Breakpoints.Adapt(Settings.Breakpoints);
            jsonSettings.FontSizeUnits.Adapt(Settings.FontSizeUnits);
            
            #endregion
        }

        catch
        {
            Console.WriteLine($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0", $"Initialized settings in {timer.FormatTimer()}{Environment.NewLine}");

        ScssCoreInjectable.Clear();
        ScssCoreInjectable.Append(await SfumatoScss.GetCoreScssAsync(this, DiagnosticOutput));

        ScssSharedInjectable.Clear();
        ScssSharedInjectable.Append(await SfumatoScss.GetSharedScssAsync(this, DiagnosticOutput));
        
        if (DiagnosticMode)
	        DiagnosticOutput.TryAdd("init1", $"Identified {ScssClassCollection.AllClasses.Count:N0} available classes in {timer.FormatTimer()}{Environment.NewLine}");

        timer.Restart();
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
	/// Identify the used classes in the project.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GatherUsedScssCoreClassesAsync()
	{
		var timer = new Stopwatch();

		timer.Start();

		UsedClasses.Clear();

		if (Settings.ProjectPaths.Count == 0)
		{
			Console.WriteLine($"{Strings.TriangleRight} No project paths specified");
			return;
		}
		
		var filesList = new ConcurrentBag<string>();
		var tasks = new List<Task>();

		foreach (var projectPath in Settings.ProjectPaths.Where(p => p.FileSpec.EndsWith(".scss", StringComparison.OrdinalIgnoreCase) == false))
			tasks.Add(RecurseProjectPathAsync(projectPath.Path, projectPath.FileSpec, projectPath.IsFilePath, filesList, projectPath.Recurse));

		await Task.WhenAll(tasks);

		tasks.Clear();
		
		foreach (var filePath in filesList)
			tasks.Add(ExamineFileForUsedScssCoreClassesAsync(filePath));
		
		await Task.WhenAll(tasks);		
		
		if (filesList.IsEmpty)
			Console.WriteLine($"{Strings.TriangleRight} Identified no used classes");
		else
			Console.WriteLine($"{Strings.TriangleRight} Identified {filesList.Count:N0} file{(filesList.Count == 1 ? string.Empty : "s")} using {UsedClasses.Count:N0} classes in {timer.FormatTimer()}");
	}

	private async Task ExamineFileForUsedScssCoreClassesAsync(string filePath)
	{
		if (string.IsNullOrEmpty(filePath))
			return;

		var markup = await File.ReadAllTextAsync(filePath);

		ExamineMarkupForUsedClasses(markup);
	}

	/// <summary>
	/// Recurse a project path to collect all matching file paths in a list.
	/// </summary>
	/// <param name="sourcePath"></param>
	/// <param name="fileSpec"></param>
	/// <param name="isFilePath"></param>
	/// <param name="filesList"></param>
	/// <param name="recurse"></param>
	public static async Task RecurseProjectPathAsync(string? sourcePath, string fileSpec, bool isFilePath, ConcurrentBag<string> filesList, bool recurse = false)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty())
			return;

		FileInfo[] files = null!;
		DirectoryInfo[] dirs = null!;
		
		if (isFilePath)
		{
			recurse = false;
			files = new [] { new FileInfo(Path.Combine(sourcePath, fileSpec)) };
		}

		else
		{
			var dir = new DirectoryInfo(sourcePath);

			if (dir.Exists == false)
			{
				Console.WriteLine($"Source directory does not exist or could not be found: {sourcePath}");
				Environment.Exit(1);
			}

			dirs = dir.GetDirectories();
			files = dir.GetFiles().Where(f => f.Name.EndsWith(fileSpec.TrimStart('*'))).ToArray();
		}
		
		foreach (var projectFile in files.OrderBy(f => f.Name))
			filesList.Add(projectFile.FullName);

		if (recurse == false)
			return;
		
		foreach (var subDir in dirs.OrderBy(d => d.Name))
			await RecurseProjectPathAsync(subDir.FullName, fileSpec, isFilePath, filesList, recurse);
	}
	
	/// <summary>
	/// Examine markup for used classes and add them to the UsedClasses collection.
	/// </summary>
	/// <param name="markup"></param>
	public void ExamineMarkupForUsedClasses(string markup)
	{
		if (string.IsNullOrEmpty(markup))
			return;

		var matches = new List<Match>();

		matches.AddRange(CoreClassRegex.Matches(markup));
		
		foreach (var match in matches)
		{
			var cssSelector = new CssSelector
			{
				Value = match.Value
			};

			if (cssSelector.IsInvalid)
				continue;
			
			if (UsedClasses.ContainsKey(cssSelector.FixedValue))
				continue;
			
			var scssClasses = ScssClassCollection.GetAllByClassName(cssSelector).ToList();

			if (scssClasses.Count == 0)
				continue;
			
			var userClassValueType = cssSelector.CustomValueSegment.GetUserClassValueType();
			ScssClass? foundScssClass = null;

			// 1. No arbitrary value type specified (e.g. text-slate-100 or text-[#112233])
			// 2. Arbitrary value type specified, must match source class value type (e.g. text-[color:#112233])
			// 3. Utility class (e.g. "container")

			if (string.IsNullOrEmpty(userClassValueType) == false)
				foreach (var scssClass in scssClasses)
				{
					if (scssClass.ValueTypes.Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(userClassValueType) == false)
						continue;

					foundScssClass = scssClass;
					
					break;
				}								
			else
				foreach (var scssClass in scssClasses)
				{
					if (scssClass.CssSelector is null)
						continue;
					
					if (scssClass.ValueTypes != string.Empty)
						continue;

					if (scssClass.CssSelector.FixedValue != cssSelector.RootSegment)
						continue;
					
					foundScssClass = scssClass;
					break;
				}

			if (foundScssClass is null)
				continue;
			
			var usedScssClass = foundScssClass.Adapt<ScssClass>();

			usedScssClass.CssSelector = cssSelector;
			
			if (string.IsNullOrEmpty(userClassValueType) == false)
				usedScssClass.Value = usedScssClass.CssSelector.CustomValue;

			UsedClasses.TryAdd(usedScssClass.CssSelector.FixedValue, usedScssClass);
		}

		matches.Clear();
		matches.AddRange(ArbitraryCssRegex.Matches(markup));

		foreach (var match in matches)
		{
			var cssSelector = new CssSelector
			{
				Value = match.Value
			};
			
			if (cssSelector.IsInvalid)
				continue;
			
			if (UsedClasses.ContainsKey(cssSelector.FixedValue))
				continue;

			// 1. Arbitrary CSS style (e.g. tabp:[display:none])

			var usedScssClass = new ScssClass
			{
				CssSelector = cssSelector,
				ValueTypes = string.Empty,
				Value = cssSelector.CustomValue,				
				Template = "{value};"
			};

			UsedClasses.TryAdd(usedScssClass.CssSelector.FixedValue, usedScssClass);
		}
	}

	#endregion
}
