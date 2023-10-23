using Argentini.Sfumato.ScssUtilityCollections;
using Argentini.Sfumato.ScssUtilityCollections.Entities;

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
    
    #region Collections

    public List<string> CliArguments { get; } = new();
    public ConcurrentDictionary<string,WatchedFile> WatchedFiles { get; } = new();
    public ConcurrentDictionary<string,WatchedScssFile> WatchedScssFiles { get; } = new();
    public ConcurrentDictionary<string,ScssClass> UsedClasses { get; } = new();
    public ScssClassCollection ScssClassCollection { get; } = new();    
    
    

    // todo: New utility class structure
    
    public ConcurrentDictionary<string,ScssUtilityClass> BackgroundsCollection { get; } = new();
    
    
    
    
    #endregion
    
    #region App State

    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public SfumatoJsonSettings Settings { get; set; } = new();
    public ConcurrentDictionary<string,string> DiagnosticOutput { get; set; } = new();
    public string WorkingPathOverride { get; set; } = string.Empty;
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public string SfumatoScssOutputPath { get; set; } = string.Empty;
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
	    
	    #region Regular Expressions
	    
	    var arbitraryCssExpression = $$"""
(?<=[\s"'`])
({{string.Join("\\:|", AllPrefixes) + "\\:"}}){0,10}
([\!]{0,1}\[({{string.Join("|", SfumatoScss.CssPropertyNames)}})\:[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\])
(?=[\s"'`])
""";
	    
	    ArbitraryCssRegex = new Regex(arbitraryCssExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    var coreClassExpression = $$"""
(?<=[\s"'`])
({{string.Join("\\:|", AllPrefixes) + "\\:"}}){0,10}
(
	([\!]{0,1}[a-z\-][a-z0-9\-\.%]{2,100})
	(
		(/[a-z0-9\-\.]{1,100})|([/]{0,1}\[[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\]){0,1}
	)
)
(?=[\s"'`])
""";
	    
	    CoreClassRegex = new Regex(coreClassExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);

	    const string sfumatoScssIncludesRegexExpression = @"^\s*@sfumato\s+((core)|(base))[\s]{0,};\r?\n";
	    
	    SfumatoScssIncludesRegex = new Regex(sfumatoScssIncludesRegexExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    #endregion
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

	    await ProcessCliArgumentsAsync(args);

	    if (VersionMode || HelpMode)
		    return;
	    
	    #region Find sfumato.json file

        if (string.IsNullOrEmpty(WorkingPathOverride) == false)
            WorkingPath = WorkingPathOverride;
        
        SettingsFilePath = Path.Combine(WorkingPath, "sfumato.json");

        if (File.Exists(SettingsFilePath) == false)
        {
            await Console.Out.WriteLineAsync($"Could not find sfumato.json settings file at path {WorkingPath}");
            await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
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
                await Console.Out.WriteLineAsync($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
                Environment.Exit(1);
            }
            
            #endregion
            
            #region Import settings
            
            Settings.CssOutputPath = Path.Combine(WorkingPath, jsonSettings.CssOutputPath.SetNativePathSeparators());
        
            if (Directory.Exists(Settings.CssOutputPath) == false)
            {
                await Console.Out.WriteLineAsync($"{CliErrorPrefix}Could not find CSS output path: {Settings.CssOutputPath}");
                Environment.Exit(1);
            }
        
            Settings.ThemeMode = jsonSettings.ThemeMode switch
            {
                "system" => "system",
                "class" => "class",
                _ => "system"
            };

            Settings.UseAutoTheme = jsonSettings.UseAutoTheme;
            
            SassCliPath = await GetEmbeddedSassPathAsync();
            ScssPath = await GetEmbeddedScssPathAsync();

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
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        SfumatoScssOutputPath = Path.Combine(Settings.CssOutputPath, "sfumato.scss");
        
        if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0", $"Initialized settings in {timer.FormatTimer()}{Environment.NewLine}");

        ScssCoreInjectable.Clear();
        ScssCoreInjectable.Append(await SfumatoScss.GetCoreScssAsync(this, DiagnosticOutput));

        ScssSharedInjectable.Clear();
        ScssSharedInjectable.Append(await SfumatoScss.GetSharedScssAsync(this, DiagnosticOutput));
        
        if (DiagnosticMode)
	        DiagnosticOutput.TryAdd("init1", $"Identified {ScssClassCollection.AllClasses.Count:N0} available classes in {timer.FormatTimer()}{Environment.NewLine}");

        #region Load Utility Classes

        timer.Restart();

        // todo: Add all utility class collections here
        
        var tasks = new List<Task>
        {
	        BackgroundsCollection.AddBackgroundAsync(),
	        BackgroundsCollection.AddFromAsync(),
	        BackgroundsCollection.AddViaAsync(),
	        BackgroundsCollection.AddToAsync()
        };

        await Task.WhenAll(tasks);

        var classCount = 0;

        classCount += BackgroundsCollection.Sum(c => c.Value.Options.Count);
        
        if (DiagnosticMode)
	        DiagnosticOutput.TryAdd("init1a", $"Loaded {classCount:N0} utility classes in {timer.FormatTimer()}{Environment.NewLine}");
        
        #endregion
    }
    
    #endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	public async Task ProcessCliArgumentsAsync(IEnumerable<string>? args)
	{
		CliArguments.Clear();
		CliArguments.AddRange(args?.ToList() ?? new List<string>());

		if (CliArguments.Count < 1)
			return;

		if (CliArguments.Count == 0)
		{
			HelpMode = true;
		}

		else
		{
			if (CliArguments[0] != "help" && CliArguments[0] != "version" && CliArguments[0] != "build" && CliArguments[0] != "watch")
			{
				await Console.Out.WriteLineAsync("Invalid command specified; must be: help, version, build, or watch");
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

			if (CliArguments.Count > 1)
			{
				for (var x = 1; x < CliArguments.Count; x++)
				{
					var arg = CliArguments[x];
					
					if (arg.Equals("--minify", StringComparison.OrdinalIgnoreCase))
						ReleaseMode = true;

					else if (arg.Equals("--path", StringComparison.OrdinalIgnoreCase))
						if (++x < CliArguments.Count)
						{
							var path = CliArguments[x].SetNativePathSeparators();

							if (path.Contains(Path.DirectorySeparatorChar) == false &&
							    path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
								continue;

							if (path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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

    public async Task<string> GetEmbeddedSassPathAsync()
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
			await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded Dart Sass cannot be found.");
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
			await Console.Out.WriteLineAsync($"{CliErrorPrefix}Dart Sass is embedded but cannot be found.");
			Environment.Exit(1);
		}

		StringBuilderPool.Return(sb);
		
		return sassPath;
    }

    public static async Task<string> GetEmbeddedScssPathAsync()
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
    
    #endregion
    
    #region Runner Methods
    
	/// <summary>
	/// Gather all watched files defined in settings.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GatherWatchedFilesAsync()
	{
		var timer = new Stopwatch();
		var totalTimer = new Stopwatch();

		totalTimer.Start();

		if (Settings.ProjectPaths.Count == 0)
		{
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} No project paths specified");
			return;
		}
		
		var tasks = new List<Task>();

		timer.Start();
		
		// Gather files lists
		
		foreach (var projectPath in Settings.ProjectPaths)
			tasks.Add(RecurseProjectPathAsync(projectPath.Path, projectPath.FileSpec, projectPath.IsFilePath, projectPath.Recurse));

		await Task.WhenAll(tasks);
		
		tasks.Clear();

		if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0a", $"{Strings.TriangleRight} RecurseProjectPathAsync => {timer.FormatTimer()}{Environment.NewLine}");

		timer.Restart();
		
		// Add matches to files lists

		foreach (var watchedFile in WatchedFiles)
			tasks.Add(ProcessFileMatchesAsync(watchedFile.Value));
		
		await Task.WhenAll(tasks);

		if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0b", $"{Strings.TriangleRight} ProcessFileMatchesAsync => {timer.FormatTimer()}{Environment.NewLine}");

		timer.Restart();
		
		// Generate used classes list

		await ExamineWatchedFilesForUsedClassesAsync();

		if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0c", $"{Strings.TriangleRight} ExamineWatchedFilesForUsedClassesAsync => {timer.FormatTimer()}{Environment.NewLine}");
		
		if (WatchedFiles.IsEmpty)
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified no used classes");
		else
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified {WatchedFiles.Count:N0} file{(WatchedFiles.Count == 1 ? string.Empty : "s")} using {UsedClasses.Count:N0} classes in {totalTimer.FormatTimer()}");
	}
	
	/// <summary>
	/// Recurse a project path to collect all matching files.
	/// </summary>
	/// <param name="sourcePath"></param>
	/// <param name="fileSpec"></param>
	/// <param name="isFilePath"></param>
	/// <param name="watchedFiles"></param>
	/// <param name="recurse"></param>
	public async Task RecurseProjectPathAsync(string? sourcePath, string fileSpec, bool isFilePath, bool recurse = false)
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
				await Console.Out.WriteLineAsync($"Source directory does not exist or could not be found: {sourcePath}");
				Environment.Exit(1);
			}

			dirs = dir.GetDirectories();
			files = dir.GetFiles().Where(f => f.Name.EndsWith(fileSpec.TrimStart('*'))).ToArray();
		}

		var tasks = new List<Task>();
		
		foreach (var projectFile in files)
		{
			tasks.Add(AddProjectFileToCollection(projectFile, fileSpec));
		}

		await Task.WhenAll(tasks);

		if (recurse == false)
			return;
		
		foreach (var subDir in dirs.OrderBy(d => d.Name))
			await RecurseProjectPathAsync(subDir.FullName, fileSpec, isFilePath, recurse);
	}

	/// <summary>
	/// Read a FileInfo object and add it to the appropriate collection.
	/// </summary>
	/// <param name="projectFile"></param>
	/// <param name="fileSpec"></param>
	public async Task AddProjectFileToCollection(FileInfo projectFile, string fileSpec)
	{
		var markup = await File.ReadAllTextAsync(projectFile.FullName);

		if (fileSpec.EndsWith(".scss"))
		{
			WatchedScssFiles.TryAdd(projectFile.FullName, new WatchedScssFile
			{
				FilePath = projectFile.FullName,
				Scss = markup
			});
		}

		else
		{
			WatchedFiles.TryAdd(projectFile.FullName, new WatchedFile
			{
				FilePath = projectFile.FullName,
				Markup = markup
			});
		}
	}
	
	/// <summary>
	/// Identify class matches in a given watched file.
	/// </summary>
	/// <param name="watchedFile"></param>
	public async Task ProcessFileMatchesAsync(WatchedFile watchedFile)
	{
		watchedFile.CoreClassMatches.Clear();
		watchedFile.ArbitraryCssMatches.Clear();
		
		var tasks = new List<Task>();

		var matches = CoreClassRegex.Matches(watchedFile.Markup);

		if (matches.Count > 0)
		{
			foreach (Match match in matches)
				tasks.Add(AddCssSelectorToCollection(watchedFile.CoreClassMatches, match.Value));

			await Task.WhenAll(tasks);
			tasks.Clear();
		}

		matches = ArbitraryCssRegex.Matches(watchedFile.Markup);

		if (matches.Count > 0)
		{
			foreach (Match match in matches)
				tasks.Add(AddCssSelectorToCollection(watchedFile.ArbitraryCssMatches, match.Value));

			await Task.WhenAll(tasks);
		}
	}

	public static async Task AddCssSelectorToCollection(ConcurrentDictionary<string,CssSelector> collection, string value)
	{
		var cssSelector = new CssSelector(value);
		
		if (cssSelector.IsInvalid == false)
			collection.TryAdd(value,new CssSelector(value));

		await Task.CompletedTask;
	}
	
	/// <summary>
	/// Examine all watched files for used classes.
	/// Generates the UsedClasses collection.
	/// </summary>
	public async Task ExamineWatchedFilesForUsedClassesAsync()
	{
		UsedClasses.Clear();

		var tasks = new List<Task>();
		
		foreach (var watchedFile in WatchedFiles)
			tasks.Add(ExamineMarkupForUsedClassesAsync(watchedFile.Value));
		
		await Task.WhenAll(tasks);
	}
	
	/// <summary>
	/// Examine markup for used classes and add them to the UsedClasses collection.
	/// </summary>
	/// <param name="watchedFile"></param>
	public async Task ExamineMarkupForUsedClassesAsync(WatchedFile watchedFile)
	{
		foreach (var cssSelector in watchedFile.CoreClassMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedValue))
				continue;
			
			var matchingScssClasses = ScssClassCollection.GetAllByClassName(cssSelector).ToList();

			if (matchingScssClasses.Count == 0)
				continue;
			
			var userClassValueType = cssSelector.CustomValueSegment.GetUserClassValueType();
			ScssClass? foundScssClass = null;

			if (string.IsNullOrEmpty(userClassValueType) == false)
				foreach (var scssClass in matchingScssClasses)
				{
					if (scssClass.ValueTypes.Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(userClassValueType) == false)
						continue;

					foundScssClass = scssClass;
					break;
				}								
			else
				foreach (var scssClass in matchingScssClasses)
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
			
			var usedScssClass = new ScssClass
			{
				CssSelector = cssSelector,
				ValueTypes = foundScssClass.ValueTypes,
				Value = string.IsNullOrEmpty(userClassValueType) == false ? cssSelector.CustomValue : foundScssClass.Value,
				Template = foundScssClass.Template,
				ChildSelector = foundScssClass.ChildSelector,
				GlobalGrouping = foundScssClass.GlobalGrouping,
				SortOrder = foundScssClass.SortOrder
			};

			UsedClasses.TryAdd(usedScssClass.CssSelector.FixedValue, usedScssClass);
		}

		foreach (var cssSelector in watchedFile.ArbitraryCssMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedValue))
				continue;

			var usedScssClass = new ScssClass
			{
				CssSelector = cssSelector,
				ValueTypes = string.Empty,
				Value = cssSelector.CustomValue,				
				Template = "{value};"
			};

			UsedClasses.TryAdd(usedScssClass.CssSelector.FixedValue, usedScssClass);
		}

		await Task.CompletedTask;
	}
	
	#endregion
}

public class WatchedFile
{
	public string FilePath { get; set; } = string.Empty;
	public string Markup { get; set; } = string.Empty;
	public ConcurrentDictionary<string,CssSelector> CoreClassMatches { get; set; } = new ();
	public ConcurrentDictionary<string,CssSelector> ArbitraryCssMatches { get; set; } = new ();
}

public class WatchedScssFile
{
	public string FilePath { get; set; } = string.Empty;
	public string Scss { get; set; } = string.Empty;
	public string Css { get; set; } = string.Empty;
}