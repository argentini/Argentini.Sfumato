using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CliWrap;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.ObjectPool;

namespace Argentini.Sfumato;

public sealed class SfumatoRunner
{
	#region Properties

	#region State

	public bool DebugMode { get; }
	public bool ReleaseMode { get; set; }
	public bool WatchMode { get; set; }
	public bool VersionMode { get; set; }
	public bool HelpMode { get; set; }
	public bool DiagnosticMode { get; set; }
	public string ThemeMode { get; set; } = "system";
	public string AssemblyPath { get; }
	public Architecture ProcessorArchitecture { get; }
	public OSPlatform OsPlatform { get; }
	public List<string> CliArgs { get; } = new();
	private ObjectPool<StringBuilder> StringBuilderPool { get; }
	public StringBuilder DiagnosticOutput { get; }
	public Breakpoints Breakpoints { get; set; } = new();
	public FontSizeViewportUnits FontSizeViewportUnits { get; set; } = new();
	
	public Dictionary<string, string> ScssFiles { get; } = new();
	public List<string> AllPrefixes { get; } = new();
	public List<ScssClass> Classes { get; } = new();
	public List<ScssClass> UsedClasses { get; } = new();
	public StringBuilder ScssCore { get; }

	#endregion

	#region CLI

	public static int MaxConsoleWidth => GetMaxConsoleWidth();
	
	private static int GetMaxConsoleWidth()
	{
		try
		{
			return Console.WindowWidth - 1;
		}
		catch
		{
			return 78;
		}
	}
	
	public static string CliErrorPrefix => "Sfumato => ";

	#endregion
	
	#region Constants

	public static int IndentationSpaces => 4;
	public Dictionary<string, string> MediaQueryPrefixes { get; } = new ()
	{
		{ "dark", "@media (prefers-color-scheme: dark) {" },
		{ "portrait", "@media (orientation: portrait) {" },
		{ "landscape", "@media (orientation: landscape) {" },
		{ "print", "@media print {" },
		{ "zero", "@include sf-media($from: zero) {" },
		{ "phab", "@include sf-media($from: phab) {" },
		{ "tabp", "@include sf-media($from: tabp) {" },
		{ "tabl", "@include sf-media($from: tabl) {" },
		{ "note", "@include sf-media($from: note) {" },
		{ "desk", "@include sf-media($from: desk) {" },
		{ "elas", "@include sf-media($from: elas) {" }
	};
	public Dictionary<string, string> PseudoclassPrefixes { get; } = new ()
	{
		{ "hover", "&:hover {"},
		{ "focus", "&:focus {" },
		{ "focus-within", "&:focus-within {" },
		{ "focus-visible", "&:focus-visible {" },
		{ "active", "&:active {" },
		{ "visited", "&:visited {" },
		{ "target", "&:target {" },
		{ "first", "&:first-child {" },
		{ "last", "&:last-child {" },
		{ "only", "&:only-child {" },
		{ "odd", "&:nth-child(odd) {" },
		{ "even", "&:nth-child(even) {" },
		{ "first-of-type", "&:first-of-type {" },
		{ "last-of-type", "&:last-of-type {" },
		{ "only-of-type", "&:only-of-type {" },
		{ "empty", "&:empty {" },
		{ "disabled", "&:disabled {" },
		{ "enabled", "&:enabled {" },
		{ "checked", "&:checked {" },
		{ "indeterminate", "&:indeterminate {" },
		{ "default", "&:default {" },
		{ "required", "&:required {" },
		{ "valid", "&:valid {" },
		{ "invalid", "&:invalid {" },
		{ "in-range", "&:in-range {" },
		{ "out-of-range", "&:out-of-range {" },
		{ "placeholder-shown", "&:placeholder-shown {" },
		{ "autofill", "&:autofill {" },
		{ "read-only", "&:read-only {" },
		{ "before", "&::before {" },
		{ "after", "&::after {" },
		{ "first-letter", "&::first-letter {" },
		{ "first-line", "&::first-line {" },
		{ "marker", "&::marker {" },
		{ "selection", "&::selection {" },
		{ "file", "&::file-selector-button {" },
		{ "backdrop", "&::backdrop {" },
		{ "placeholder", "&::placeholder {" }
	};

	#endregion
	
    #region Input Paths

    public string WorkingPath { get; set; }
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public string ConfigFilePath { get; set; }
    public List<ProjectPath> ProjectPaths { get; } = new();
    
    #endregion
    
	#region Output Paths

	public string CssOutputPath { get; set; } = string.Empty;
	
	#endregion

    #region FileSystemWatchers

    //public List<FileSystemWatcher> ProjectFileSystemWatchers { get; } = new();

	#endregion

	#endregion

	#region Constructors
	
	public SfumatoRunner(IEnumerable<string>? args)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		StringBuilderPool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
		DiagnosticOutput = StringBuilderPool.Get();
		ScssCore = StringBuilderPool.Get();
		
		TypeAdapterConfig<ScssNode, ScssNode>.NewConfig()
			.PreserveReference(true)
			.AfterMapping((src, dest) => 
			{
				dest.Classes = src.Classes.Adapt<List<ScssClass>>();
				dest.Nodes = src.Nodes.Adapt<List<ScssNode>>();
			});
		
		#region Identify Runtime

#if DEBUG
		DebugMode = true;
#endif
		
		AssemblyPath = Assembly.GetExecutingAssembly().Location;
		ProcessorArchitecture = Identify.GetProcessorArchitecture();
		OsPlatform = Identify.GetOsPlatform();
		WorkingPath = Directory.GetCurrentDirectory();
		ConfigFilePath = Path.GetFullPath(Path.Combine(WorkingPath, "sfumato.json"));
		
		if (string.IsNullOrEmpty(AssemblyPath))
		{
			Console.WriteLine($"{CliErrorPrefix}Could not identify current runtime location.");
			Environment.Exit(1);
		}

		#endregion
		
		#region Gather Scss Class Prefixes

		AllPrefixes.Clear();
		AllPrefixes.AddRange(MediaQueryPrefixes.Select(p => p.Key));
		AllPrefixes.AddRange(PseudoclassPrefixes.Select(p => p.Key));
		
		#endregion

		ProcessCliArguments(args);
		ProcessEmbeddedResources();

		if (DebugMode)
		{
			DiagnosticMode = true;
		}
		
		DiagnosticOutput.Append($"Loaded environment in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
	}

	/// <summary>
	/// Loads settings, creates SCSS manifest, load core SCSS for injection into project build.
	/// </summary>
	public async Task InitializeAsync()
	{
		var timer = new Stopwatch();

		timer.Start();
		
		await LoadJsonSettingsAsync();
		
		DiagnosticOutput.Append($"Processed settings in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		timer.Restart();

		await GatherAvailableClassesAsync();

		DiagnosticOutput.Append($"Identified {Classes.Count:N0} available classes in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
		
		timer.Restart();
		
		#region Add Includes
		
		ScssCore.Append((await File.ReadAllTextAsync(Path.Combine(ScssPath, "includes", "_core.scss"))).Trim() + '\n');
		ScssCore.Append((await File.ReadAllTextAsync(Path.Combine(ScssPath, "includes", "_browser-reset.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(ScssPath, "includes", "_media-queries.scss"))).Trim() + '\n';

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{Breakpoints.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{Breakpoints.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{Breakpoints.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{Breakpoints.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{Breakpoints.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{Breakpoints.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{Breakpoints.Elas}px");
		
		ScssCore.Append(mediaQueriesScss);

		var initScss = (await File.ReadAllTextAsync(Path.Combine(ScssPath, "includes", "_initialize.scss"))).Trim() + '\n';
		
		initScss = initScss.Replace("#{zero-vw}", $"{FontSizeViewportUnits.Zero}vw");
		initScss = initScss.Replace("#{phab-vw}", $"{FontSizeViewportUnits.Phab}vw");
		initScss = initScss.Replace("#{tabp-vw}", $"{FontSizeViewportUnits.Tabp}vw");
		initScss = initScss.Replace("#{tabl-vw}", $"{FontSizeViewportUnits.Tabl}vw");
		initScss = initScss.Replace("#{note-vw}", $"{FontSizeViewportUnits.Note}vw");
		initScss = initScss.Replace("#{desk-vw}", $"{FontSizeViewportUnits.Desk}vw");
		initScss = initScss.Replace("#{elas-vw}", $"{FontSizeViewportUnits.Elas}vw");
		
		ScssCore.Append(initScss);
		
		DiagnosticOutput.Append($"Loaded core SCSS libraries in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		#endregion
	}
	
	#endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	public void ProcessCliArguments(IEnumerable<string>? args)
	{
		CliArgs.Clear();
		CliArgs.AddRange(args?.ToList() ?? new List<string>());

		if (CliArgs.Count < 1)
			return;
		
		foreach (var arg in CliArgs)
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
				if (CliArgs.Count - 1 >= CliArgs.IndexOf(arg) + 1)
				{
					var path = CliArgs[CliArgs.IndexOf(arg) + 1].TrimEnd("sfumato.json", StringComparison.InvariantCultureIgnoreCase) ?? string.Empty;

					path = Path.GetFullPath(path);
					
					ConfigFilePath = Path.Combine(path, "sfumato.json");
					WorkingPath = path;
				}
		}
	}
	
	/// <summary>
	/// Validate that embedded resources are present and establish paths.
	/// </summary>
	public void ProcessEmbeddedResources()
	{
		#region Validate Embedded Dart Sass
		
		var workingSassPath = AssemblyPath;

		while (workingSassPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
		{
			workingSassPath = workingSassPath[..workingSassPath.LastIndexOf(Path.DirectorySeparatorChar)];

			if (DebugMode)
			{
				if (Directory.Exists(Path.Combine(workingSassPath, "sass")) == false)
					continue;

				var tempPath = workingSassPath; 
				
				workingSassPath = Path.Combine(tempPath, "sass");
			}

			else
			{
				if (Directory.Exists(Path.Combine(workingSassPath, "contentFiles")) == false)
					continue;
			
				var tempPath = workingSassPath; 

				workingSassPath = Path.Combine(tempPath, "contentFiles", "any", "any", "sass");
			}
			
			if (Directory.Exists(workingSassPath) == false)
				continue;
			
			if (OsPlatform == OSPlatform.Windows)
			{
				if (ProcessorArchitecture is Architecture.X64 or Architecture.Arm64)
					SassCliPath = Path.Combine(workingSassPath, "dart-sass-windows-x64", "sass.bat");
			}
				
			else if (OsPlatform == OSPlatform.OSX)
			{
				if (ProcessorArchitecture == Architecture.X64)
					SassCliPath = Path.Combine(workingSassPath, "dart-sass-macos-x64", "sass");
				else if (ProcessorArchitecture == Architecture.Arm64)
					SassCliPath = Path.Combine(workingSassPath, "dart-sass-macos-arm64", "sass");
			}
				
			else if (OsPlatform == OSPlatform.Linux)
			{
				if (ProcessorArchitecture == Architecture.X64)
					SassCliPath = Path.Combine(workingSassPath, "dart-sass-linux-x64", "sass");
				else if (ProcessorArchitecture == Architecture.Arm64)
					SassCliPath = Path.Combine(workingSassPath, "dart-sass-linux-arm64", "sass");
			}

			break;
		}
		
		if (string.IsNullOrEmpty(SassCliPath) || File.Exists(SassCliPath) == false)
		{
			Console.WriteLine($"{CliErrorPrefix}Embedded Dart Sass cannot be found.");
			Environment.Exit(1);
		}
		
		var sb = StringBuilderPool.Get();
		var cmd = Cli.Wrap(SassCliPath)
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
		
		#endregion
		
		#region Validate Embedded SCSS
		
		ScssPath = $"{workingSassPath.TrimEnd("sass")}scss";

		if (string.IsNullOrEmpty(ScssPath) == false && Directory.Exists(ScssPath))
			return;
		
		Console.WriteLine($"{CliErrorPrefix}Embedded SCSS resources cannot be found.");
		Environment.Exit(1);

		#endregion
	}

	/// <summary>
	/// Find and load the sfumato.json configuration file.
	/// </summary>
	public async Task LoadJsonSettingsAsync()
	{
		#region Validate Configuration File
		
		if (DebugMode)
		{
			var index = WorkingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

			if (index > -1)
			{
				WorkingPath = Path.Combine(WorkingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
			}

			else
			{
				index = WorkingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato.Tests" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

				if (index > -1)
					WorkingPath = Path.Combine(WorkingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
			}
		}		
		
		ConfigFilePath = Path.Combine(WorkingPath, "sfumato.json");

		if (File.Exists(ConfigFilePath) == false)
		{
			Console.WriteLine($"{CliErrorPrefix}Could not find settings file at path {ConfigFilePath}");
			Environment.Exit(1);
		}
		
		var builder = new ConfigurationBuilder().AddJsonFile(ConfigFilePath, optional: false);
		var config = builder.Build();
		
		#endregion
		
		#region Settings
		
		ThemeMode = config.GetValue<string?>("ThemeMode") ?? "system";
		
		Breakpoints.Zero = config.GetValue<int?>("breakpoints:zero") ?? Breakpoints.Zero;
		Breakpoints.Phab = config.GetValue<int?>("breakpoints:phab") ?? Breakpoints.Phab;
		Breakpoints.Tabp = config.GetValue<int?>("breakpoints:tabp") ?? Breakpoints.Tabp;
		Breakpoints.Tabl = config.GetValue<int?>("breakpoints:tabl") ?? Breakpoints.Tabl;
		Breakpoints.Note = config.GetValue<int?>("breakpoints:note") ?? Breakpoints.Note;
		Breakpoints.Desk = config.GetValue<int?>("breakpoints:desk") ?? Breakpoints.Desk;
		Breakpoints.Elas = config.GetValue<int?>("breakpoints:elas") ?? Breakpoints.Elas;

		FontSizeViewportUnits.Zero = config.GetValue<double?>("fontSizeViewportUnits:zero") ?? FontSizeViewportUnits.Zero;
		FontSizeViewportUnits.Phab = config.GetValue<double?>("fontSizeViewportUnits:phab") ?? FontSizeViewportUnits.Phab;
		FontSizeViewportUnits.Tabp = config.GetValue<double?>("fontSizeViewportUnits:tabp") ?? FontSizeViewportUnits.Tabp;
		FontSizeViewportUnits.Tabl = config.GetValue<double?>("fontSizeViewportUnits:tabl") ?? FontSizeViewportUnits.Tabl;
		FontSizeViewportUnits.Note = config.GetValue<double?>("fontSizeViewportUnits:note") ?? FontSizeViewportUnits.Note;
		FontSizeViewportUnits.Desk = config.GetValue<double?>("fontSizeViewportUnits:desk") ?? FontSizeViewportUnits.Desk;
		FontSizeViewportUnits.Elas = config.GetValue<double?>("fontSizeViewportUnits:elas") ?? FontSizeViewportUnits.Elas;
		
		#endregion
		
		#region Output Paths
		
		var cssOutputPath = config.GetValue<string>("CssOutputPath") ?? string.Empty;

		cssOutputPath = cssOutputPath.Replace('\\', '/').Replace('/', Path.DirectorySeparatorChar);
		
		CssOutputPath = Path.Combine(WorkingPath, cssOutputPath);

		if (Directory.Exists(CssOutputPath) == false)
		{
			Console.WriteLine($"{CliErrorPrefix}Invalid CSS Output Path (edit the sfumato.json file)");
			Environment.Exit(1);
		}

		#endregion
		
		#region Project Paths
		
		var workingProjectPaths = config.GetSection("ProjectPaths").Get<List<ProjectPath>>() ?? new List<ProjectPath>();
		
		if (workingProjectPaths.Count == 0)
		{
			Console.WriteLine($"{CliErrorPrefix}No Project Paths (edit the sfumato.json file)");
			Environment.Exit(1);
		}

		foreach (var projectPath in workingProjectPaths)
		{
			if (string.IsNullOrEmpty(projectPath.Path))
			{
				Console.WriteLine($"{CliErrorPrefix}Empty Project Path (edit the sfumato.json file)");
				Environment.Exit(1);
			}

			if (string.IsNullOrEmpty(projectPath.FileSpec) == false && string.IsNullOrEmpty(projectPath.FileSpec.Trim('.')) == false)
				continue;
			
			Console.WriteLine($"{CliErrorPrefix}Invalid Project Path FileSpec '{projectPath.FileSpec}' (edit the sfumato.json file)");
			Environment.Exit(1);
		}
		
		foreach (var projectPath in workingProjectPaths)
		{
			projectPath.Path = Path.Combine(WorkingPath, projectPath.Path.Replace('/', Path.DirectorySeparatorChar));
			projectPath.FileSpec = projectPath.FileSpec.Trim('.'); 

			ProjectPaths.Add(projectPath);
		}

		foreach (var projectPath in ProjectPaths)
		{
			if (Directory.Exists(projectPath.Path))
				continue;
			
			Console.WriteLine($"{CliErrorPrefix}Invalid Project Path '{projectPath.Path}' (edit the sfumato.json file)");
			Environment.Exit(1);
		}
		
		#endregion

		await Task.CompletedTask;
	}
	
	/// <summary>
	/// Identify the available class names from the embedded SCSS library.
	/// Also loads all SCSS file content into memory.
	/// </summary>
	/// <param name="config"></param>
	public async Task GatherAvailableClassesAsync()
	{
		var dir = new DirectoryInfo(ScssPath);

		ScssFiles.Clear();
		Classes.Clear();
		
		var files = dir.GetFiles();

		foreach (var file in files.Where(f => f.Extension.Equals(".scss", StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.Name))
		{
			var scss = (await File.ReadAllTextAsync(file.FullName)).NormalizeLinebreaks();

			if (scss.Length < 1)
				continue;

			ScssFiles.Add(file.FullName, scss);
			
			var index = 0;

			while (index > -1)
			{
				if (scss[index] != '.')
				{
					index = scss.IndexOf("\n.", index, StringComparison.Ordinal);

					if (index > -1)
						index++;
				}

				if (index < 0)
					continue;
				
				var end = scss.IndexOf(' ', index);

				if (end < 0)
					continue;
					
				Classes.Add(new ScssClass
				{
					FilePath = file.FullName,
					ClassName = scss.Substring(index + 1, end - index - 1)
				});

				index = end;
			}
		}
	}
	
	#endregion
	
	#region Runtime Methods

	/// <summary>
	/// Create space indentation based on level of depth.
	/// </summary>
	/// <param name="level"></param>
	/// <returns></returns>
	private static string Indent(int level)
	{
		return new string(' ', level * IndentationSpaces);
	}

	/// <summary>
	/// Determine if a prefix is a media query prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <returns></returns>
	private bool IsMediaQueryPrefix(string prefix)
	{
		var mediaQueryPrefix = MediaQueryPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(mediaQueryPrefix.Key) == false;
	}

	/// <summary>
	/// Determine if a prefix is a pseudoclass prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <returns></returns>
	private bool IsPseudoclassPrefix(string prefix)
	{
		var pseudoclassPrefix = PseudoclassPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(pseudoclassPrefix.Key) == false;
	}
	
	/// <summary>
	/// Identify the used classes in the project.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GatherUsedClassesAsync()
	{
		var timer = new Stopwatch();

		timer.Start();

		Console.Write("Identifying used classes...");
		
		UsedClasses.Clear();

		if (ProjectPaths.Count == 0)
		{
			Console.WriteLine(" no project paths specified");
			return;
		}

		foreach (var projectPath in ProjectPaths)
			await RecurseProjectPathForUsedClassesAsync(projectPath.Path, projectPath.FileSpec, projectPath.Recurse);
		
		if (UsedClasses.Count == 0)
			Console.WriteLine(" no classes used");
		else
			Console.WriteLine($" found {UsedClasses.Count:N0}/{Classes.Count:N0} classes");
		
		DiagnosticOutput.Append($"Identified {UsedClasses.Count:N0}/{Classes.Count:N0} used classes in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
	}
	private async Task RecurseProjectPathForUsedClassesAsync(string? sourcePath, string fileSpec, bool recurse)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty())
			return;

		var dir = new DirectoryInfo(sourcePath);

		if (dir.Exists == false)
			throw new DirectoryNotFoundException($"{CliErrorPrefix}Source directory does not exist or could not be found: {sourcePath}");

		var dirs = dir.GetDirectories();
		var files = dir.GetFiles();
		var prefixes = string.Join(":|", AllPrefixes) + ":";

		var regex = new Regex($$"""(?<=[\s"'`])(({{prefixes}}){0,9}[a-z]{1,1}[a-z0-9\-]{1,99})(?=[\s"'`])""", RegexOptions.Compiled);

		foreach (var projectFile in files.OrderBy(f => f.Name))
		{
			if (projectFile.Name.EndsWith($"{fileSpec.TrimStart('*')}", StringComparison.InvariantCultureIgnoreCase) == false)
				continue;
			
			var markup = await File.ReadAllTextAsync(projectFile.FullName);

			if (string.IsNullOrEmpty(markup))
				continue;

			var matches = regex.Matches(markup);

			foreach (Match match in matches)
			{
				var mv = match.Value.ToLower().TrimEnd(':');

				foreach (var breakpoint in new[] { "zero:", "phab:", "tabp:", "tabl:", "note:", "desk:", "elas:" })
				{
					if (mv.Contains(breakpoint, StringComparison.Ordinal))
						mv = $"{breakpoint}{mv.Replace(breakpoint, string.Empty, StringComparison.Ordinal)}";
				}
				
				if (mv.Contains("dark:", StringComparison.Ordinal))
					mv = $"dark:{mv.Replace("dark:", string.Empty, StringComparison.Ordinal)}";
				
				var matchValue = mv.Contains(':') ? mv[(mv.LastIndexOf(':') + 1)..] : mv;
				
				var matchedClass = Classes.FirstOrDefault(c => c.ClassName.Equals(matchValue, StringComparison.Ordinal));
				
				if (matchedClass is null)
					continue;

				if (UsedClasses.FirstOrDefault(c => c.ClassName.Equals(mv, StringComparison.Ordinal)) is not null)
					continue;
				
				UsedClasses.Add(new ScssClass
				{
					FilePath = matchedClass.FilePath,
					ClassName = mv
				});
			}
		}

		if (recurse)
			foreach (var subDir in dirs.OrderBy(d => d.Name))
				await RecurseProjectPathForUsedClassesAsync(subDir.FullName, fileSpec, recurse);
	}
	
	/// <summary>
	/// Get the complete SCSS class from the cached file from storage.  
	/// </summary>
	/// <param name="scssClass"></param>
	/// <param name="excludeDeclaration"></param>
	/// <returns></returns>
	private string GetScssClassMarkup(ScssClass scssClass, bool excludeDeclaration = false)
	{
		var startIndex = ScssFiles[scssClass.FilePath].StartsWith($".{scssClass.RootClassName}", StringComparison.Ordinal) ? 0 : ScssFiles[scssClass.FilePath].IndexOf($"\n.{scssClass.RootClassName}", StringComparison.Ordinal);
		var endIndex = -1;

		if (startIndex < 0)
			return string.Empty;

		endIndex = ScssFiles[scssClass.FilePath].IndexOf("\n.", startIndex + 1, StringComparison.Ordinal);

		if (endIndex < 0)
		{
			// End of file, no more classes; grab last closing brace
			endIndex = ScssFiles[scssClass.FilePath].LastIndexOf("}", startIndex + 1, StringComparison.Ordinal);
		}

		else
		{
			// More classes follow so use the next one as a marker for where the current class ends
			while (endIndex > startIndex)
			{
				if (ScssFiles[scssClass.FilePath][--endIndex] == '}')
					break;
			}
			
			if (ScssFiles[scssClass.FilePath][endIndex] != '}')
				endIndex = -1;
		}

		if (excludeDeclaration)
		{
			// Repoint start index to the opening brace
			startIndex = ScssFiles[scssClass.FilePath].IndexOf("{", startIndex, StringComparison.Ordinal);
			
			if (startIndex < 0)
				return string.Empty;

			startIndex++;
			
			// Repoint end index to the closing brace
			while (ScssFiles[scssClass.FilePath][endIndex] != '}')
			{
				endIndex--;
			}

			endIndex--;

			if (endIndex < 0)
				return string.Empty;
		}

		if (endIndex < 0 || endIndex <= startIndex)
			return string.Empty;

		var result = ScssFiles[scssClass.FilePath].Substring(startIndex, endIndex - startIndex + 1);

		result = result.TrimStart('\n').TrimEnd().TrimEnd('\n');
		
		return result;
	}

	/// <summary>
	/// Generate SCSS class from prefixed class name, including nesting.
	/// </summary>
	/// <param name="scssClass"></param>
	/// <param name="stripPrefix"></param>
	/// <returns></returns>
	public async Task<string> GenerateScssClassAsync(ScssClass scssClass, string stripPrefix = "")
	{
		var scssResult = StringBuilderPool.Get();
		var level = 0;
		var className = scssClass.ClassName;
		var scssBody = string.Empty;

		if (string.IsNullOrEmpty(stripPrefix) == false && className.StartsWith(stripPrefix, StringComparison.Ordinal))
			className = className.TrimStart(stripPrefix) ?? className;
		
		var segments = className.Split(':', StringSplitOptions.RemoveEmptyEntries);

		if (segments.Length == 0)
			return string.Empty;
			
		if (segments.Length > 1)
		{
			var prefixes = new string[segments.Length - 1];
			var renderedClassName = false;

			Array.Copy(segments, prefixes, segments.Length - 1);
			
			scssBody = GetScssClassMarkup(scssClass, true);

			foreach (var prefix in prefixes)
			{
				if (IsPseudoclassPrefix(prefix) == false)
					continue;
				
				if (renderedClassName == false)
				{
					scssResult.Append($"{Indent(level)}.{scssClass.EscapedClassName} {{\n");
					renderedClassName = true;
					level++;
				}

				var pseudoClass = PseudoclassPrefixes.First(p => p.Key.Equals(prefix, StringComparison.Ordinal));
					
				scssResult.Append($"{Indent(level)}{pseudoClass.Value}\n");
				level++;

				// else
				// {
				// 	if (IsMediaQueryPrefix(prefix) == false)
				// 		continue;
				//
				// 	var mediaQuery = MediaQueryPrefixes.First(p => p.Key.Equals(prefix, StringComparison.Ordinal));
				//
				// 	if (ThemeMode.Equals("system", StringComparison.OrdinalIgnoreCase) || prefix != "dark")
				// 	{
				// 		scssResult.Append($"{Indent(level)}{mediaQuery.Value}\n");
				// 		level++;
				// 	}
				// 	
				// 	else if (ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && prefix == "dark")
				// 	{
				// 		scssResult.Append($"{Indent(level)}.dark-theme {{\n");
				// 		level++;
				// 	}
				// }
			}
		}

		else
		{
			scssBody = GetScssClassMarkup(scssClass, true);
			scssResult.Append($"{Indent(level)}.{scssClass.EscapedClassName} {{\n");
			level++;
		}
		
		var existingIndentation = scssBody.FirstNonSpaceCharacter();

		scssResult.Append($"{scssBody.Indent(level * IndentationSpaces - existingIndentation)}\n");

		while (level > 0)
		{
			level--;
			scssResult.Append($"{Indent(level)}}}\n");
		}
		
		var result = scssResult.ToString();
		
		StringBuilderPool.Return(scssResult);
		
		return await Task.FromResult(result);
	}
	
	/// <summary>
	/// After gathering used classes with GatherUsedClassesAsync(), run the SCSS build.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GenerateProjectScssAsync()
	{
		var timer = new Stopwatch();
		
		await GatherUsedClassesAsync();

		timer.Start();

		Console.Write("Generating CSS...");
		
		var projectScss = StringBuilderPool.Get();

		projectScss.Append(ScssCore);
		projectScss.Append(await GenerateScssAsync());
		
		//await File.WriteAllTextAsync(Path.Combine(CssOutputPath, "sfumato.scss"), projectScss.ToString());

		DiagnosticOutput.Append($"Generated sfumato.scss ({projectScss.Length.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
		
		await TranspileScss(projectScss);

		StringBuilderPool.Return(projectScss);
	}
	
	/// <summary>
	/// Iterate used classes and build a tree to consolidate descendants.
	/// Serializing this tree into SCSS markup will prevent
	/// duplication of media queries, etc. to keep the file size down.
	/// </summary>
	/// <param name="config"></param>
	/// <returns></returns>
	public async Task<string> GenerateScssAsync()
	{
		var usedClasses = UsedClasses.OrderBy(c => c.ClassName).ToList().Adapt<List<ScssClass>>();
		var hierarchy = new ScssNode
		{
			Prefix = string.Empty,
			PrefixPath = string.Empty
		};

		#region Build Hierarchy
		
		foreach (var scssClass in usedClasses)
		{
			// Handle base classes (no prefixes) or prefixes start with pseudoclass (no inheritance)

			if (scssClass.ClassName.Contains(':') == false || IsPseudoclassPrefix(scssClass.Prefixes[0]))
			{
				hierarchy.Classes.Add(scssClass);
			}

			else
			{
				var prefixPath = string.Empty;
				var node = hierarchy;

				foreach (var prefix in scssClass.Prefixes)
				{
					if (IsMediaQueryPrefix(prefix) == false)
						break;
					
					prefixPath += $"{prefix}:";
					
					var prefixNode = node.Nodes.FirstOrDefault(n => n.Prefix.Equals(prefix, StringComparison.Ordinal));

					if (prefixNode is null)
					{
						var newNode = new ScssNode
						{
							Prefix = prefix,
							PrefixPath = prefixPath
						};

						node.Nodes.Add(newNode);
						node = newNode;
					}

					else
					{
						node = prefixNode;
					}
				}
				
				node.Classes.Add(scssClass);
			}			
		}
		
		#endregion

		if (ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase))
		{
			// Search first level nodes for "dark" prefixes and add additional nodes to support "auto-theme";
			// Light mode will work without any CSS since removing "dark-theme" and "auto-theme" from the
			// <html> class list will disabled dark mode altogether.
			foreach (var node in hierarchy.Nodes.ToList())
			{
				if (node.Prefix.Equals("dark", StringComparison.Ordinal) == false)
					continue;
				
				var newNode = node.Adapt<ScssNode>();

				newNode.Prefix = "auto-dark";
					
				hierarchy.Nodes.Add(newNode);
			}
		} 
		
		var sb = StringBuilderPool.Get();
		
		await GenerateScssRecurseAsync(hierarchy, sb);
        
		var scss = sb.ToString();
		
		StringBuilderPool.Return(sb);
		
		return scss;
	}
	private async Task GenerateScssRecurseAsync(ScssNode scssNode, StringBuilder sb)
	{
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			var prefix = scssNode.Prefix;

			if (prefix.Equals("auto-dark", StringComparison.Ordinal))
				prefix = "dark";
			
			var mediaQueryPrefix = MediaQueryPrefixes.First(p => p.Key.Equals(prefix));

			if (ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.dark-theme {{\n");
			}

			else if (ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "auto-dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.auto-theme {{ {mediaQueryPrefix.Value}\n");
			}
			
			else
			{
				sb.Append($"{Indent(scssNode.Level - 1)}{mediaQueryPrefix.Value}\n");
			}
		}
			
		if (scssNode.Classes.Count > 0)
		{
			foreach (var scssClass in scssNode.Classes)
			{
				var markup = await GenerateScssClassAsync(scssClass, scssNode.PrefixPath);
					
				sb.Append($"{markup.Indent(scssNode.Level * IndentationSpaces - markup.FirstNonSpaceCharacter()).TrimEnd('\n')}\n");
			}
		}
			
		if (scssNode.Nodes.Count > 0)
		{
			foreach (var node in scssNode.Nodes)
			{
				await GenerateScssRecurseAsync(node, sb);
			}
		}
			
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}

		if (scssNode.Prefix == "auto-dark")
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}
	}
	
	#endregion
	
	#region SCSS
	
	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// Calls "sass" CLI to transpile.
	/// </summary>
	/// <param name="scss">SCSS markup</param>
	public async Task TranspileScss(StringBuilder scss)
	{
		var timer = new Stopwatch();
		var sb = StringBuilderPool.Get();

		timer.Start();
		
		try
		{
			var arguments = new List<string>();

			if (File.Exists(Path.Combine(CssOutputPath, "sfumato.css.map")))
				File.Delete(Path.Combine(CssOutputPath, "sfumato.css.map"));

			if (ReleaseMode == false)
			{
				arguments.Add("--style=expanded");
				arguments.Add("--embed-sources");
			}

			else
			{
				arguments.Add("--style=compressed");
				arguments.Add("--no-source-map");
	            
			}

			arguments.Add("--stdin");
			arguments.Add(Path.Combine(CssOutputPath, "sfumato.css"));
			
			var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap(SassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

			await cmd.ExecuteAsync();

			var fileInfo = new FileInfo(Path.Combine(CssOutputPath, "sfumato.css"));

			Console.WriteLine($" saved sfumato.css ({fileInfo.Length.FormatBytes()})");
			DiagnosticOutput.Append($"Transpiled sfumato.css ({fileInfo.Length.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
		}

		catch (Exception e)
		{
			sb.AppendLine($" ERROR: {e.Message.Trim()}");
			sb.AppendLine(string.Empty);
			sb.AppendLine(e.StackTrace?.Trim());
			sb.AppendLine(string.Empty);

			Console.WriteLine(sb.ToString());
			
			DiagnosticOutput.Append(sb.ToString().Trim());
			
			throw;
		}

		StringBuilderPool.Return(sb);
	}
	
	#endregion
}

public sealed class ProjectPath
{
	public string Path { get; set; } = string.Empty;
	public string FileSpec { get; set; } = string.Empty;
	public bool Recurse { get; set; } = true;
}

public sealed class ScssClass
{
	public string FilePath { get; set; } = string.Empty;

	private string _className = string.Empty;
	public string ClassName
	{
		get => _className;

		set
		{
			_className = value;

			if (_className.LastIndexOf(':') > -1 && _className.LastIndexOf(':') < _className.Length - 1)
			{
				var segments = _className.Split(':', StringSplitOptions.RemoveEmptyEntries);

				if (segments.Length == 0)
					Prefixes = Array.Empty<string>();
			
				if (segments.Length == 1)
				{
					RootClassName = segments[0];
				}

				Prefixes = new string[segments.Length - 1];

				Array.Copy(segments, Prefixes, segments.Length - 1);
				
				RootClassName = segments[^1];
			}
			
			else
			{
				RootClassName = _className;
			}
		}
	}

	public string RootClassName { get; private set; } = string.Empty;
	public string EscapedClassName => ClassName.Replace(":", "\\:");
	public string[] Prefixes { get; private set; } = Array.Empty<string>();

}

public sealed class ScssNode
{
	public string Prefix { get; set; } = string.Empty; // e.g. dark

	public string PrefixPathValue = string.Empty;
	public string PrefixPath // e.g. dark:tabp:
	{
		get => PrefixPathValue;

		set
		{
			PrefixPathValue = value;
			Level = 0;

			if (string.IsNullOrEmpty(PrefixPathValue))
				return;

			var segmentCount = PrefixPathValue.Split(':', StringSplitOptions.RemoveEmptyEntries).Length;

			Level = segmentCount;
		}
	}
	public int Level { get; set; }
	public List<ScssClass> Classes { get; set; } = new();
	public List<ScssNode> Nodes { get; set; } = new();
}

public sealed class Breakpoints
{
	public int Zero { get; set; }
	public int Phab { get; set; } = 400;
	public int Tabp { get; set; } = 540;
	public int Tabl { get; set; } = 800;
	public int Note { get; set; } = 1280;
	public int Desk { get; set; } = 1440;
	public int Elas { get; set; } = 1600;
}

public sealed class FontSizeViewportUnits
{
	public double Zero { get; set; } = 4.35;
	public double Phab { get; set; } = 4;
	public double Tabp { get; set; } = 1.6;
	public double Tabl { get; set; } = 1;
	public double Note { get; set; } = 1;
	public double Desk { get; set; } = 1;
	public double Elas { get; set; } = 1;
}
