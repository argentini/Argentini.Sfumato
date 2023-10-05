using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mapster;

namespace Argentini.Sfumato;

public sealed class SfumatoRunner
{
	#region Properties

	#region Modes

	public bool ReleaseMode { get; set; }
	public bool WatchMode { get; set; }
	public bool VersionMode { get; set; }
	public bool HelpMode { get; set; }
	public bool DiagnosticMode { get; set; }
	
	#endregion

	#region State

	public SfumatoSettings Settings { get; } = new();
	public SfumatoScss Scss { get; } = new();
	public string WorkingPathOverride { get; set; } = string.Empty;
	public List<string> CliArgs { get; } = new();
	public StringBuilder DiagnosticOutput { get; }
	public Dictionary<string, string> ScssFiles { get; } = new();
	public List<string> AllPrefixes { get; } = new();
	public List<ScssClass> Classes { get; } = new();
	public List<ScssClass> UsedClasses { get; } = new();
	public StringBuilder ScssCore { get; }

	#endregion

	#region Constants

	public static int IndentationSpaces => 4;

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
	
    #region FileSystemWatchers

    //public List<FileSystemWatcher> ProjectFileSystemWatchers { get; } = new();

	#endregion

	#endregion

	#region Constructors
	
	public SfumatoRunner(IEnumerable<string>? args)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		DiagnosticOutput = Settings.StringBuilderPool.Get();
		ScssCore = Settings.StringBuilderPool.Get();
		
		TypeAdapterConfig<ScssNode, ScssNode>.NewConfig()
			.PreserveReference(true)
			.AfterMapping((src, dest) => 
			{
				dest.Classes = src.Classes.Adapt<List<ScssClass>>();
				dest.Nodes = src.Nodes.Adapt<List<ScssNode>>();
			});
		
		#region Gather Scss Class Prefixes

		AllPrefixes.Clear();
		AllPrefixes.AddRange(MediaQueryPrefixes.Select(p => p.Key));
		AllPrefixes.AddRange(PseudoclassPrefixes.Select(p => p.Key));
		
		#endregion

		ProcessCliArguments(args);

#if DEBUG
		DiagnosticMode = true;
#endif		

		DiagnosticOutput.Append($"Started environment in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
	}

	/// <summary>
	/// Loads settings, creates SCSS manifest, load core SCSS for injection into project build.
	/// </summary>
	public async Task InitializeAsync()
	{
		var timer = new Stopwatch();

		timer.Start();
		
		await Settings.LoadAsync(WorkingPathOverride);
		
		DiagnosticOutput.Append($"Processed settings in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		timer.Restart();

		await GatherAvailableClassesAsync();

		DiagnosticOutput.Append($"Identified {Classes.Count:N0} available classes in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
		
		timer.Restart();

		ScssCore.Clear();
		ScssCore.Append(await SfumatoScss.GetCoreScssAsync(Settings));
		
		DiagnosticOutput.Append($"Loaded core SCSS libraries in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
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
					
					WorkingPathOverride = path;
				}
		}
	}
	
	/// <summary>
	/// Identify the available class names from the embedded SCSS library.
	/// Also loads all SCSS file content into memory.
	/// </summary>
	/// <param name="config"></param>
	public async Task GatherAvailableClassesAsync()
	{
		var dir = new DirectoryInfo(Settings.ScssPath);

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

		if (Settings.ProjectPaths.Count == 0)
		{
			Console.WriteLine(" no project paths specified");
			return;
		}

		foreach (var projectPath in Settings.ProjectPaths)
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
		{
			Console.WriteLine($"Source directory does not exist or could not be found: {sourcePath}");
			Environment.Exit(1);
		}

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
		var scssResult = Settings.StringBuilderPool.Get();
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
		
		Settings.StringBuilderPool.Return(scssResult);
		
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
		
		var projectScss = Settings.StringBuilderPool.Get();

		projectScss.Append(ScssCore);
		projectScss.Append(await GenerateScssAsync());
		
		//await File.WriteAllTextAsync(Path.Combine(CssOutputPath, "sfumato.scss"), projectScss.ToString());

		DiagnosticOutput.Append($"Generated sfumato.scss ({projectScss.Length.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		timer.Restart();

		var fileSize = await Scss.TranspileScss(projectScss, Settings, ReleaseMode);
		
		Console.WriteLine($" saved sfumato.css ({fileSize.FormatBytes()})");
		DiagnosticOutput.Append($"Transpiled sfumato.css ({fileSize.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		Settings.StringBuilderPool.Return(projectScss);
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

		if (Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase))
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
		
		var sb = Settings.StringBuilderPool.Get();
		
		await GenerateScssRecurseAsync(hierarchy, sb);
        
		var scss = sb.ToString();
		
		Settings.StringBuilderPool.Return(sb);
		
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

			if (Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.dark-theme {{\n");
			}

			else if (Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "auto-dark")
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
