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
	/// Build the project's CSS and write to storage.
	/// </summary>
	/// <param name="runner"></param>
	public async Task PerformBuildAsync()
	{
		var timer = new Stopwatch();
		
		#region Generate Global CSS
		
		await AppState.GatherUsedScssCoreClassesAsync();

		timer.Start();

		var projectScss = AppState.StringBuilderPool.Get();

		projectScss.Append(AppState.ScssCoreInjectable);
		projectScss.Append(await GenerateScssObjectTreeAsync());
		
		AppState.DiagnosticOutput.Append($"Generated sfumato.scss ({projectScss.Length.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		timer.Restart();

		var fileSize = await SfumatoScss.TranspileScss(projectScss, AppState);
		
		Console.WriteLine($"=> Generated sfumato.css ({fileSize.FormatBytes()})");
		AppState.DiagnosticOutput.Append($"Generated sfumato.css ({fileSize.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		#endregion
		
		#region Compile Project SCSS In-Place
		
		foreach (var projectPath in AppState.Settings.ProjectPaths)
			if (projectPath.FileSpec.EndsWith(".scss", StringComparison.OrdinalIgnoreCase))
				await FindAndBuildProjectScssAsync(AppState, projectPath.Path, projectPath.FileSpec, projectPath.Recurse);
		
		#endregion
		
		AppState.StringBuilderPool.Return(projectScss);
	}
	
	#endregion
	
	#region Generation Methods
	
	/// <summary>
	/// Generate SCSS class from user class name, including nesting.
	/// Only includes pseudoclasses, not media queries.
	/// </summary>
	/// <param name="scssClass"></param>
	/// <param name="stripPrefix"></param>
	/// <returns></returns>
	public async Task<string> GenerateScssClassMarkupAsync(ScssClass scssClass, string stripPrefix = "")
	{
		var scssResult = AppState.StringBuilderPool.Get();
		var level = 0;
		var className = scssClass.UserClassName;
		var segments = Array.Empty<string>();
		
		if (string.IsNullOrEmpty(stripPrefix) == false && className.StartsWith(stripPrefix, StringComparison.Ordinal))
			className = className.TrimStart(stripPrefix) ?? className;

		// Bracketed raw CSS style (e.g. tabp:[display:none])
		if (className.EndsWith(']') && (className.StartsWith('[') || className.Contains(":[")))
		{
			if (className.Contains(":["))
				segments = className[..className.IndexOf('[')].Split(':', StringSplitOptions.RemoveEmptyEntries);
		}

		// Standard class syntax
		else
		{
			if (className.EndsWith(']') && className.Contains('['))
				className = className[..className.IndexOf('[')];
			
			segments = className.Split(':', StringSplitOptions.RemoveEmptyEntries);
			
			if (segments.Length == 0)
				return string.Empty;
		}		
		
		if (segments.Length > 1)
		{
			var prefixes = new string[segments.Length - 1];
			var renderedClassName = false;

			Array.Copy(segments, prefixes, segments.Length - 1);
			
			foreach (var prefix in prefixes)
			{
				if (IsPseudoclassPrefix(prefix) == false)
					continue;
				
				if (renderedClassName == false)
				{
					scssResult.Append($"{Indent(level)}.{scssClass.UserClassName.EscapeCssClassName(AppState.StringBuilderPool)} {{\n");
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
			scssResult.Append($"{Indent(level)}.{scssClass.UserClassName.EscapeCssClassName(AppState.StringBuilderPool)} {{\n");
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
		
		AppState.StringBuilderPool.Return(scssResult);
		
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
		//var usedClasses = AppState.UsedClasses.OrderBy(c => c.Value.UserClassName).ToList().Adapt<List<ScssClass>>();
		var hierarchy = new ScssNode
		{
			Prefix = string.Empty,
			PrefixPath = string.Empty
		};

		#region Build Hierarchy
		
		foreach (var (_, scssClass) in AppState.UsedClasses.OrderBy(c => c.Value.SortOrder))
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
		
		var sb = AppState.StringBuilderPool.Get();
		
		await GenerateScssFromObjectTreeAsync(hierarchy, sb);
        
		var scss = sb.ToString();
		
		AppState.StringBuilderPool.Return(sb);
		
		return scss;
	}
	private async Task GenerateScssFromObjectTreeAsync(ScssNode scssNode, StringBuilder sb)
	{
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			var prefix = scssNode.Prefix;

			if (prefix.Equals("auto-dark", StringComparison.Ordinal))
				prefix = "dark";
			
			var mediaQueryPrefix = SfumatoScss.MediaQueryPrefixes.First(p => p.Key.Equals(prefix));

			if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.dark-theme {{\n");
			}

			else if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "auto-dark")
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
				var markup = await GenerateScssClassMarkupAsync(scssClass, scssNode.PrefixPath);
					
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

		if (scssNode.Prefix == "auto-dark")
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}
	}
	
	/// <summary>
	/// Transpile all configured project SCSS files in-place.
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="sourcePath"></param>
	/// <param name="fileSpec"></param>
	/// <param name="recurse"></param>
	/// <returns></returns>
	private static async Task FindAndBuildProjectScssAsync(SfumatoAppState appState, string? sourcePath, string fileSpec, bool recurse = false)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty())
			return;
		
		var dir = new DirectoryInfo(sourcePath);

		if (dir.Exists == false)
		{
			Console.WriteLine($"Source directory does not exist: {sourcePath}");
			Environment.Exit(1);
		}

		var dirs = dir.GetDirectories();
		var files = dir.GetFiles();
			
		foreach (var cssFile in files.Where(f => f.Name.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase)))
			cssFile.Delete();			

		foreach (var file in files.OrderBy(f => f.Name))
		{
			if (file.Name.ToLower().EndsWith(fileSpec.TrimStart("*") ?? ".scss") == false)
				continue;

			var length = await SfumatoScss.TranspileSingleScss(file.FullName, appState);

			if (length > -1)
				Console.WriteLine($"=> Generated {ShortenPathForOutput(file.FullName, appState)} ({length.FormatBytes()})");
		}

		if (recurse)
			foreach (var subDir in dirs.OrderBy(d => d.Name))
				await FindAndBuildProjectScssAsync(appState, subDir.FullName, fileSpec, recurse);
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
	private bool IsMediaQueryPrefix(string prefix)
	{
		var mediaQueryPrefix = SfumatoScss.MediaQueryPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(mediaQueryPrefix.Key) == false;
	}

	/// <summary>
	/// Determine if a prefix is a pseudoclass prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <returns></returns>
	private bool IsPseudoclassPrefix(string prefix)
	{
		var pseudoclassPrefix = SfumatoScss.PseudoclassPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(pseudoclassPrefix.Key) == false;
	}
	
	#endregion
}
