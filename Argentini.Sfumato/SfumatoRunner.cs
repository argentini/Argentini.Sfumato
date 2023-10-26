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
	
	#endregion

	public SfumatoRunner()
	{
		TypeAdapterConfig<ScssNode, ScssNode>.NewConfig()
			.PreserveReference(true)
			.AfterMapping((src, dest) => 
			{
				dest.Classes = src.Classes.Adapt<List<UsedScssClass>>();
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
		
		timer.Start();
		totalTimer.Start();

		var projectScss = AppState.StringBuilderPool.Get();

		projectScss.Append(AppState.ScssCoreInjectable);
		projectScss.Append(await GenerateScssObjectTreeAsync());

		await File.WriteAllTextAsync(AppState.SfumatoScssOutputPath, projectScss.ToString());

		if (AppState.DiagnosticMode)
			AppState.DiagnosticOutput.TryAdd("init4", $"Generated sfumato.scss ({projectScss.Length.FormatBytes()}) in {timer.FormatTimer()}{Environment.NewLine}");
		
		timer.Restart();

		var css = await SfumatoScss.TranspileScss(AppState.SfumatoScssOutputPath, projectScss.ToString(), AppState);
		
		await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Generated sfumato.css ({css.Length.FormatBytes()}{(AppState.Minify ? ", minified" : string.Empty)}) in {totalTimer.FormatTimer()}");

		if (AppState.DiagnosticMode)
			AppState.DiagnosticOutput.TryAdd("init5", $"Generated sfumato.css ({css.Length.FormatBytes()}) in {timer.FormatTimer()}{Environment.NewLine}");

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
		var tasks = new List<Task>();

		foreach (var watchedFile in AppState.WatchedScssFiles)
			tasks.Add(TranspileScssFileAsync(AppState, watchedFile.Value, fileStats));
		
		await Task.WhenAll(tasks);
		
		if (fileStats.FileCount == 0)
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} No project SCSS files found");
		else
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Generated {fileStats.FileCount:N0} project CSS file{(fileStats.FileCount == 1 ? string.Empty : "s")} ({fileStats.TotalBytes.FormatBytes()}{(AppState.Minify ? ", minified" : string.Empty)}) in {timer.FormatTimer()}");
	}
	
	#endregion
	
	#region Generation Methods
	
	/// <summary>
	/// Generate SCSS class from user class name, including nesting.
	/// Only includes pseudoclasses, not media queries.
	/// </summary>
	/// <param name="usedScssClass"></param>
	/// <param name="pool"></param>
	/// <param name="stripPrefix"></param>
	/// <returns></returns>
	public static async Task<string> GenerateScssClassMarkupAsync(UsedScssClass usedScssClass, ObjectPool<StringBuilder> pool, string stripPrefix = "")
	{
		if (usedScssClass.CssSelector is null)
			return string.Empty;
		
		var scssResult = pool.Get();
		var level = 0;
		var stripPrefixes = stripPrefix.Split(':', StringSplitOptions.RemoveEmptyEntries);
		var prefixes = usedScssClass.CssSelector.AllVariants;

		foreach (var prefix in stripPrefixes)
			if (prefixes.Count > 0 && prefixes[0] == prefix)
				prefixes.RemoveAt(0);
		
		if (prefixes.Count > 0)
		{
			var renderedClassName = false;

			foreach (var prefix in prefixes)
			{
				if (IsPseudoclassPrefix(prefix) == false)
					continue;
				
				if (renderedClassName == false)
				{
					scssResult.Append($"{Indent(level)}.{usedScssClass.CssSelector.EscapedSelector} {{\n");
					renderedClassName = true;
					level++;
				}

				var pseudoClass = SfumatoScss.PseudoclassPrefixes.First(p => p.Key.Equals(prefix, StringComparison.Ordinal));
					
				scssResult.Append($"{Indent(level)}{pseudoClass.Value}\n");
				level++;
			}
		}

		else
		{
			scssResult.Append($"{Indent(level)}.{usedScssClass.CssSelector.EscapedSelector} {{\n");
			level++;
		}
		
		if (usedScssClass.CssSelector.IsArbitraryCss)
			scssResult.Append($"{usedScssClass.CssSelector.ArbitraryValue.Indent(level * IndentationSpaces)}{(usedScssClass.CssSelector.IsImportant ? " !important" : string.Empty)};\n");
		else
			scssResult.Append($"{usedScssClass.CssSelector.ScssUtilityClass?.ScssMarkup.Replace(";", (usedScssClass.CssSelector.IsImportant ? " !important;" : ";")).Indent(level * IndentationSpaces)}\n");
			
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

		foreach (var (_, scssClass) in AppState.UsedClasses.OrderBy(c => c.Value.CssSelector?.Depth).ThenBy(c => c.Value.PrefixSortOrder).ThenBy(c => c.Value.SortOrder).ThenBy(c => c.Key))
		{
			if (scssClass.CssSelector is null)
				continue;
			
			// Handle base classes (no prefixes) or prefixes start with pseudoclass (no inheritance)

			if (scssClass.CssSelector.MediaQueryVariants.Count == 0)
			{
				hierarchy.Classes.Add(scssClass);
			}

			else
			{
				var prefixPath = string.Empty;
				var node = hierarchy;

				foreach (var prefix in scssClass.CssSelector.AllVariants)
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
				
				var newNode = new ScssNode
				{
					Prefix = "auto-dark",
					PrefixPath = node.PrefixPath,
					Level = node.Level,
					Classes = node.Classes,
					Nodes = node.Nodes
				};
				
				hierarchy.Nodes.Add(newNode);
			}
		} 
		
		var sb = AppState.StringBuilderPool.Get();
		var globalSelector = AppState.StringBuilderPool.Get();
		
		#region Process global class assignments
		
		foreach (var (_, usedClass) in AppState.UsedClasses)
		{
			if (usedClass.CssSelector is null)
				continue;
			
			if (usedClass.CssSelector.ScssUtilityClass?.Category == "gradients")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedClass.CssSelector.EscapedSelector}");
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
			if (usedClass.CssSelector is null)
				continue;

			if (usedClass.CssSelector.ScssUtilityClass?.Category == "ring")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedClass.CssSelector.EscapedSelector}");
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
			if (usedClass.CssSelector is null)
				continue;

			if (usedClass.CssSelector.ScssUtilityClass?.Category == "shadow")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedClass.CssSelector.EscapedSelector}");
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
	/// <param name="watchedFile"></param>
	/// <param name="fileStats"></param>
	/// <returns></returns>
	public static async Task TranspileScssFileAsync(SfumatoAppState appState, WatchedScssFile watchedFile, FileResults fileStats)
	{
		if (File.Exists(watchedFile.FilePath) == false)
			return;
		
		fileStats.FileCount++;

		var css = await SfumatoScss.TranspileScss(watchedFile.FilePath, watchedFile.Scss, appState);

		if (string.IsNullOrEmpty(css))
			return;
		
		fileStats.TotalBytes += css.Length;

		if (appState.DiagnosticMode)
			appState.DiagnosticOutput.TryAdd("init6" + Guid.NewGuid(), $"Generated {ShortenPathForOutput(watchedFile.FilePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) + ".css", appState)} ({css.Length.FormatBytes()}){Environment.NewLine}");
	}
	
	#endregion
	
	#region SCSS Watcher Methods
	
	/// <summary>
	/// Remove a watched SCSS file from the watched SCSS files collection.
	/// Remove any associated CSS file from storage.
	/// </summary>
	/// <param name="filePath"></param>
	public async Task DeleteWatchedScssFile(string filePath)
	{
        _ = AppState.WatchedScssFiles.TryRemove(filePath, out _);

        var cssFilePath =
			filePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) +
			".css"; 
							
		if (File.Exists(cssFilePath))
			File.Delete(cssFilePath);

		if (File.Exists(cssFilePath + ".map"))
			File.Delete(cssFilePath + ".map");
		
		await Task.CompletedTask;
	}

	/// <summary>
	/// Add/update a watched SCSS file in the watched SCSS files collection.
	/// Transpile to CSS file on storage and in collection.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="cancellationTokenSource"></param>
	public async Task AddUpdateWatchedScssFile(string filePath, CancellationTokenSource cancellationTokenSource)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var scss = await File.ReadAllTextAsync(filePath, cancellationTokenSource.Token);
		var css = await SfumatoScss.TranspileScss(filePath, scss, AppState);

		if (AppState.WatchedScssFiles.TryGetValue(filePath, out var watchedFile))
		{
			watchedFile.Css = css;
			watchedFile.Scss = scss;
		}

		else
		{
			AppState.WatchedScssFiles.TryAdd(filePath, new WatchedScssFile
			{
				FilePath = filePath,
				Css = css,
				Scss = scss
			});
		}
							
		await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Generated {ShortenPathForOutput(filePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) + ".css", AppState)} ({css.Length.FormatBytes()}) in {timer.FormatTimer()}");
	}
	
	#endregion
	
	#region Watcher Methods
	
	/// <summary>
	/// Remove a watched file from the watched files collection.
	/// </summary>
	/// <param name="filePath"></param>
	public async Task DeleteWatchedFile(string filePath)
	{
		_ = AppState.WatchedFiles.TryRemove(filePath, out _);
								
		await Task.CompletedTask;
	}

	/// <summary>
	/// Add/update a watched file in the watched files collection.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="cancellationTokenSource"></param>
	public async Task AddUpdateWatchedFile(string filePath, CancellationTokenSource cancellationTokenSource)
	{
		var markup = await File.ReadAllTextAsync(filePath, cancellationTokenSource.Token);

		if (AppState.WatchedFiles.TryGetValue(filePath, out var watchedFile))
		{
			watchedFile.Markup = markup;

			await AppState.ProcessFileMatchesAsync(watchedFile);        
		}

		else
		{
			var newWatchedFile = new WatchedFile
			{
				FilePath = filePath,
				Markup = markup
			};

			await AppState.ProcessFileMatchesAsync(newWatchedFile);        
			
			AppState.WatchedFiles.TryAdd(filePath, newWatchedFile);
		}
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
