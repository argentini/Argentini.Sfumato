namespace Argentini.Sfumato;

public sealed class SfumatoRunner
{
	#region Properties
	
	public SfumatoAppState AppState { get; } = new();

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
	
    //public List<FileSystemWatcher> ProjectFileSystemWatchers { get; } = new();

	#endregion

	public SfumatoRunner()
	{
		TypeAdapterConfig<ScssNode, ScssNode>.NewConfig()
			.PreserveReference(true)
			.AfterMapping((src, dest) => 
			{
				dest.Classes = src.Classes.Adapt<List<ScssClass>>();
				dest.Nodes = src.Nodes.Adapt<List<ScssNode>>();
			});
		
#if DEBUG
		AppState.DiagnosticMode = true;
#endif		
	}

	#region Entry Points
	
	/// <summary>
	/// Loads settings and app state.
	/// </summary>
	public async Task InitializeAsync(IEnumerable<string>? args = null)
	{
		await AppState.InitializeAsync(args ?? Enumerable.Empty<string>());
	}
	
	/// <summary>
	/// Perform a full build of sfumato.css and project SCSS.
	/// </summary>
	public async Task PerformFullBuildAsync()
	{
		await Task.WhenAll(PerformCoreBuildAsync(), PerformProjectScssBuildAsync());		
	}
	
	/// <summary>
	/// Build sfumato.css based on watched project files.
	/// </summary>
	public async Task PerformCoreBuildAsync()
	{
		var timer = new Stopwatch();
		var totalTimer = new Stopwatch();
		
		#region Generate Global CSS
		
		await AppState.GatherUsedScssCoreClassesAsync();

		timer.Start();
		totalTimer.Start();

		var projectScss = AppState.StringBuilderPool.Get();

		projectScss.Append(AppState.ScssCoreInjectable);
		projectScss.Append(await GenerateScssObjectTreeAsync());

		await File.WriteAllTextAsync(Path.Combine(AppState.Settings.CssOutputPath, "sfumato.scss"), projectScss.ToString());

		if (AppState.DiagnosticMode)
			AppState.DiagnosticOutput.Add($"Generated sfumato.scss ({projectScss.Length.FormatBytes()}) in {timer.FormatTimer()}{Environment.NewLine}");
		
		timer.Restart();
		
		var fileSize = await SfumatoScss.TranspileSingleScss(Path.Combine(AppState.Settings.CssOutputPath, "sfumato.scss"), AppState);
		
		Console.WriteLine($"{Strings.TriangleRight} Generated sfumato.css ({fileSize.FormatBytes()}) in {totalTimer.FormatTimer()}");

		if (AppState.DiagnosticMode)
			AppState.DiagnosticOutput.Add($"Generated sfumato.css ({fileSize.FormatBytes()}) in {timer.FormatTimer()}{Environment.NewLine}");

		#endregion
		
		AppState.StringBuilderPool.Return(projectScss);
	}
	
	/// <summary>
	/// Build the project's CSS and write to storage.
	/// </summary>
	public async Task PerformProjectScssBuildAsync()
	{
		var timer = new Stopwatch();
		
		timer.Start();

		var fileStats = new FileResults();
		var filesList = new ConcurrentBag<string>();
		var tasks = new List<Task>();

		foreach (var projectPath in AppState.Settings.ProjectPaths.Where(p => p.FileSpec.EndsWith(".scss", StringComparison.OrdinalIgnoreCase)))
			tasks.Add(SfumatoAppState.RecurseProjectPathAsync(projectPath.Path, projectPath.FileSpec, projectPath.IsFilePath, filesList, projectPath.Recurse));

		await Task.WhenAll(tasks);

		tasks.Clear();
		
		foreach (var filePath in filesList)
			tasks.Add(TranspileScssFileAsync(AppState, filePath, fileStats));
		
		await Task.WhenAll(tasks);
		
		if (fileStats.FileCount == 0)
			Console.WriteLine($"{Strings.TriangleRight} No project SCSS files found");
		else
			Console.WriteLine($"{Strings.TriangleRight} Generated {fileStats.FileCount:N0} project CSS file{(fileStats.FileCount == 1 ? string.Empty : "s")} ({fileStats.TotalBytes.FormatBytes()}) in {timer.FormatTimer()}");
	}
	
	#endregion
	
	#region Generation Methods
	
	/// <summary>
	/// Generate SCSS class from user class name, including nesting.
	/// Only includes pseudoclasses, not media queries.
	/// </summary>
	/// <param name="scssClass"></param>
	/// <param name="pool"></param>
	/// <param name="stripPrefix"></param>
	/// <returns></returns>
	public static async Task<string> GenerateScssClassMarkupAsync(ScssClass scssClass, ObjectPool<StringBuilder> pool, string stripPrefix = "")
	{
		var scssResult = pool.Get();
		var level = 0;
		var className = scssClass.UserClassName;
		var prefixes = Array.Empty<string>();

		if (string.IsNullOrEmpty(stripPrefix) == false && className.StartsWith(stripPrefix, StringComparison.Ordinal))
			className = className.TrimStart(stripPrefix) ?? className;

		// Bracketed raw CSS style (e.g. tabp:[display:none])
		//if (className.EndsWith(']') && (className.StartsWith('[') || className.Contains(":[")))
		if (scssClass is ArbitraryScssClass)
		{
			if (className.Contains(":["))
				prefixes = className[..className.IndexOf('[')].Split(':', StringSplitOptions.RemoveEmptyEntries);
		}

		// Standard class syntax
		else
		{
			if (className.EndsWith(']') && className.Contains('['))
				className = className[..className.IndexOf('[')];
			
			prefixes = className.Split(':', StringSplitOptions.RemoveEmptyEntries);
			
			if (prefixes.Length == 0)
				return string.Empty;

			prefixes = prefixes[..^1];
		}		
		
		if (prefixes.Length > 0)
		{
			var renderedClassName = false;

			foreach (var prefix in prefixes)
			{
				if (IsPseudoclassPrefix(prefix) == false)
					continue;
				
				if (renderedClassName == false)
				{
					scssResult.Append($"{Indent(level)}.{scssClass.UserClassName.EscapeCssClassName(pool)} {{\n");
					renderedClassName = true;
					level++;

					if (scssClass.ChildSelector != string.Empty)
					{
						scssResult.Append($"{Indent(level)}{scssClass.ChildSelector} {{\n");
						level++;
					}
				}

				var pseudoClass = SfumatoScss.PseudoclassPrefixes.First(p => p.Key.Equals(prefix, StringComparison.Ordinal));
					
				scssResult.Append($"{Indent(level)}{pseudoClass.Value}\n");
				level++;
			}
		}

		else
		{
			scssResult.Append($"{Indent(level)}.{scssClass.UserClassName.EscapeCssClassName(pool)} {{\n");
			level++;
			
			if (scssClass.ChildSelector != string.Empty)
			{
				scssResult.Append($"{Indent(level)}{scssClass.ChildSelector} {{\n");
				level++;
			}
		}
		
		scssResult.Append($"{scssClass.GetStyles().Indent(level * IndentationSpaces)}\n");

		while (level > 0)
		{
			level--;
			scssResult.Append($"{Indent(level)}}}\n");
		}
		
		var result = scssResult.ToString();
		
		pool.Return(scssResult);
		
		return await Task.FromResult(result);
	}
	
	/// <summary>
	/// Iterate used classes and build a tree to consolidate descendants.
	/// Serializing this tree into SCSS markup will prevent
	/// duplication of media queries, etc. to keep the file size down.
	/// </summary>
	/// <param name="config"></param>
	/// <returns></returns>
	public async Task<string> GenerateScssObjectTreeAsync()
	{
		var hierarchy = new ScssNode
		{
			Prefix = string.Empty,
			PrefixPath = string.Empty
		};

		#region Build Hierarchy
		
		foreach (var (_, scssClass) in AppState.UsedClasses.OrderBy(c => c.Value.Depth).ThenBy(c => c.Value.PrefixSortOrder).ThenBy(c => c.Value.SortOrder).ThenBy(c => c.Key))
		{
			// Handle base classes (no prefixes) or prefixes start with pseudoclass (no inheritance)

			if (scssClass.UserClassName.Contains(':') == false || (scssClass.Prefixes.Length > 0 && IsPseudoclassPrefix(scssClass.Prefixes[0])))
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

		if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase))
		{
			// Search first level nodes for "dark" prefix and add additional nodes to support "theme-auto";
			// Light mode will work without any CSS since removing "theme-dark" and "theme-auto" from the
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
		
		var sb = AppState.StringBuilderPool.Get();
		var globalSelector = AppState.StringBuilderPool.Get();
		
		#region Process global class assignments
		
		foreach (var (_, usedClass) in AppState.UsedClasses)
		{
			if (usedClass.GlobalGrouping == "gradients")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedClass.UserClassName.EscapeCssClassName(AppState.StringBuilderPool)}");
		}

		if (globalSelector.Length > 0)
		{
			sb.Append(globalSelector + " {" + Environment.NewLine);
			sb.Append($"{Indent(1)}--sf-gradient-from-position: ; --sf-gradient-via-position: ; --sf-gradient-to-position: ;" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
		}

		globalSelector.Clear();

		foreach (var (_, usedClass) in AppState.UsedClasses)
		{
			if (usedClass.GlobalGrouping == "ring")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedClass.UserClassName.EscapeCssClassName(AppState.StringBuilderPool)}");
		}

		if (globalSelector.Length > 0)
		{
			sb.Append(globalSelector + " {" + Environment.NewLine);
			sb.Append($"{Indent(1)}--sf-ring-inset: ; --sf-ring-offset-width: 0px; --sf-ring-offset-color: #fff; --sf-ring-color: #3b82f680; --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
		}
		
		globalSelector.Clear();

		foreach (var (_, usedClass) in AppState.UsedClasses)
		{
			if (usedClass.GlobalGrouping == "shadow")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedClass.UserClassName.EscapeCssClassName(AppState.StringBuilderPool)}");
		}

		if (globalSelector.Length > 0)
		{
			sb.Append(globalSelector + " {" + Environment.NewLine);
			sb.Append($"{Indent(1)}--sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
		}

		#endregion
		
		await GenerateScssFromObjectTreeAsync(hierarchy, sb);
        
		var scss = sb.ToString();
		
		AppState.StringBuilderPool.Return(globalSelector);
		AppState.StringBuilderPool.Return(sb);
		
		return scss;
	}
	public async Task GenerateScssFromObjectTreeAsync(ScssNode scssNode, StringBuilder sb)
	{
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			var prefix = scssNode.Prefix;

			if (prefix.Equals("auto-dark", StringComparison.Ordinal))
				prefix = "dark";
			
			var mediaQueryPrefix = SfumatoScss.MediaQueryPrefixes.First(p => p.Prefix.Equals(prefix));

			if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.theme-dark {{\n");
			}

			else if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && AppState.Settings.UseAutoTheme && scssNode.Prefix == "auto-dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.theme-auto {{ {mediaQueryPrefix.Statement}\n");
			}
			
			else
			{
				sb.Append($"{Indent(scssNode.Level - 1)}{mediaQueryPrefix.Statement}\n");
			}
		}
			
		if (scssNode.Classes.Count > 0)
		{
			foreach (var scssClass in scssNode.Classes)
			{
				var markup = await GenerateScssClassMarkupAsync(scssClass, AppState.StringBuilderPool, scssNode.PrefixPath);
					
				sb.Append($"{markup.Indent(scssNode.Level * IndentationSpaces).TrimEnd('\n')}\n");
			}
		}
			
		if (scssNode.Nodes.Count > 0)
		{
			foreach (var node in scssNode.Nodes)
			{
				await GenerateScssFromObjectTreeAsync(node, sb);
			}
		}
			
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}

		if (AppState.Settings.UseAutoTheme && scssNode.Prefix == "auto-dark")
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}
	}
	
	/// <summary>
	/// Transpile a SCSS file in-place.
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="filePath"></param>
	/// <param name="fileStats"></param>
	/// <returns></returns>
	public static async Task TranspileScssFileAsync(SfumatoAppState appState, string filePath, FileResults fileStats)
	{
		if (string.IsNullOrEmpty(filePath))
			return;

		if (File.Exists(filePath) == false)
			return;
		
		fileStats.FileCount++;
			
		var length = await SfumatoScss.TranspileSingleScss(filePath, appState);

		if (length < 0)
			return;
		
		fileStats.TotalBytes += length;

		if (appState.DiagnosticMode)
			appState.DiagnosticOutput.Add($"Generated {ShortenPathForOutput(filePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) + ".css", appState)} ({length.FormatBytes()}){Environment.NewLine}");
	}
	
	#endregion
	
	#region Helper Methods

	/// <summary>
	/// Trim the working path from the start of a file path, and prefix with "./" to make it relative.
	/// </summary>
	/// <param name="path"></param>
	/// <param name="appState"></param>
	/// <returns></returns>
	public static string ShortenPathForOutput(string path, SfumatoAppState appState)
	{
		return $".{Path.DirectorySeparatorChar}{path.TrimStart(appState.WorkingPath, StringComparison.OrdinalIgnoreCase)?.TrimStart(Path.DirectorySeparatorChar)}";
	}
	
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
	public static bool IsMediaQueryPrefix(string prefix)
	{
		var mediaQueryPrefix = SfumatoScss.MediaQueryPrefixes.FirstOrDefault(p => p.Prefix.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(mediaQueryPrefix?.Prefix) == false;
	}

	/// <summary>
	/// Determine if a prefix is a pseudoclass prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <returns></returns>
	public static bool IsPseudoclassPrefix(string prefix)
	{
		var pseudoclassPrefix = SfumatoScss.PseudoclassPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(pseudoclassPrefix.Key) == false;
	}
	
	#endregion
}

public class FileResults
{
	public int FileCount { get; set; }
	public decimal TotalBytes { get; set; }
}
