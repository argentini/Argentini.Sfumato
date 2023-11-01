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
	/// Build sfumato.css based on watched project files.
	/// </summary>
	/// <param name="timer"></param>
	/// <param name="onlyFilesUsingBaseAndUtilities"></param>
	public async Task PerformCoreBuildAsync(Stopwatch timer, bool onlyFilesUsingBaseAndUtilities = false)
	{
		var fileResults = new FileResults();
		var tasks = new List<Task>();

		foreach (var watchedFile in AppState.WatchedScssFiles.Values)
		{
			var matches = AppState.SfumatoScssRegex.Matches(watchedFile.Scss);

			if (onlyFilesUsingBaseAndUtilities == false || (onlyFilesUsingBaseAndUtilities && matches.Any(m => m.Value.Contains("base") || m.Value.Contains("utilities"))))
			{
				tasks.Add(TranspileAsync(watchedFile.FilePath, fileResults));
			}
		}

		await Task.WhenAll(tasks);

		await Console.Out.WriteLineAsync($"Completed build of {fileResults.FileCount:N0} CSS file{(fileResults.FileCount != 1 ? "s" : string.Empty)} ({fileResults.TotalBytes.FormatBytes()}) in {timer.FormatTimer()}");
	}

	public async Task<FileResults> RecurseScssFilesForCoreBuild(string? sourcePath, bool onlyFilesUsingBaseAndUtilities, FileResults fileResults)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty())
			return fileResults;

		FileInfo[] files = null!;
		DirectoryInfo[] dirs = null!;
	
		var dir = new DirectoryInfo(sourcePath);

		if (dir.Exists == false)
		{
			await Console.Out.WriteLineAsync($"Source directory does not exist or could not be found: {sourcePath}");
			Environment.Exit(1);
		}

		dirs = dir.GetDirectories();
		files = dir.GetFiles().Where(f => f.Name.StartsWith('_') == false && f.Name.EndsWith(".scss")).ToArray();

		var tasks = new List<Task>();

		foreach (var projectFile in files)
			tasks.Add(TranspileAsync(projectFile.FullName, fileResults));

		await Task.WhenAll(tasks);

		foreach (var subDir in dirs.OrderBy(d => d.Name))
			await RecurseScssFilesForCoreBuild(subDir.FullName, onlyFilesUsingBaseAndUtilities, fileResults);
		
		return fileResults;
	}

	public async Task TranspileAsync(string filePath, FileResults fileResults)
	{
		fileResults.FileCount++;
		fileResults.TotalBytes += (await SfumatoScss.TranspileScss(filePath, string.Empty, this)).Length;
	}
	
	#endregion
	
	#region Generation Methods
	
	/// <summary>
	/// Generate SCSS class from user class name, including nesting.
	/// Only includes pseudoclasses, not media queries.
	/// </summary>
	/// <param name="cssSelector"></param>
	/// <param name="pool"></param>
	/// <param name="stripPrefix"></param>
	/// <returns></returns>
	public static async Task<string> GenerateScssClassMarkupAsync(CssSelector cssSelector, ObjectPool<StringBuilder> pool, string stripPrefix = "")
	{
		var scssResult = pool.Get();
		var level = 0;
		var stripPrefixes = stripPrefix.Split(':', StringSplitOptions.RemoveEmptyEntries);
		var prefixes = cssSelector.AllVariants;

		foreach (var prefix in stripPrefixes)
			if (prefixes.Count > 0 && prefixes[0] == prefix)
				prefixes.RemoveAt(0);
		
		if (prefixes.Count > 0)
		{
			var renderedClassName = false;

			foreach (var prefix in prefixes)
			{
				if (IsPseudoclassPrefix(prefix, cssSelector.AppState) == false)
					continue;
				
				if (renderedClassName == false)
				{
					scssResult.Append($"{Indent(level)}.{cssSelector.EscapedSelector} {{\n");
					renderedClassName = true;
					level++;
				}

				var pseudoClass = cssSelector.AppState?.PseudoclassPrefixes.First(p => p.Key.Equals(prefix, StringComparison.Ordinal));
					
				scssResult.Append($"{Indent(level)}{pseudoClass?.Value}\n");
				level++;
			}
		}

		else
		{
			scssResult.Append($"{Indent(level)}.{cssSelector.EscapedSelector} {{\n");
			level++;
		}
		
		if (cssSelector is { ScssMarkup: "" })
			cssSelector.GetStyles();

		scssResult.Append($"{cssSelector.ScssMarkup.Indent(level * IndentationSpaces)}\n");
			
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

		foreach (var (_, usedCssSelector) in AppState.UsedClasses.Where(u => u.Value.IsInvalid == false).OrderBy(c => c.Value.Depth).ThenBy(c => c.Value.VariantSortOrder).ThenBy(c => c.Key))
		{
			if (usedCssSelector.IsInvalid)
				continue;
			
			if (usedCssSelector is { IsArbitraryCss: false, ScssMarkup: "" })
			{
				usedCssSelector.IsInvalid = true;
				continue;
			}
			
			// Handle base classes (no prefixes) or prefixes start with pseudoclass (no inheritance)

			if (usedCssSelector.MediaQueryVariants.Count == 0)
			{
				hierarchy.Classes.Add(usedCssSelector);
			}

			else
			{
				var prefixPath = string.Empty;
				var node = hierarchy;

				foreach (var prefix in usedCssSelector.AllVariants)
				{
					if (IsMediaQueryPrefix(prefix, usedCssSelector.AppState) == false)
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
				
				node.Classes.Add(usedCssSelector);
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
					Level = node.Level
				};

				await RecurseNodeCloneAsync(node, newNode);
				
				hierarchy.Nodes.Add(newNode);
			}
		} 
		
		var sb = AppState.StringBuilderPool.Get();
		var globalSelector = AppState.StringBuilderPool.Get();
		
		#region Process global class assignments
		
		foreach (var (_, usedCssSelector) in AppState.UsedClasses)
		{
			if (usedCssSelector.ScssUtilityClassGroup?.Category == "gradients")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
		}

		if (globalSelector.Length > 0)
		{
			sb.Append(globalSelector + " {" + Environment.NewLine);
			sb.Append($"{Indent(1)}--sf-gradient-from-position: ; --sf-gradient-via-position: ; --sf-gradient-to-position: ;" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
		}

		globalSelector.Clear();

		foreach (var (_, usedCssSelector) in AppState.UsedClasses)
		{
			if (usedCssSelector.ScssUtilityClassGroup?.Category == "ring")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
		}

		if (globalSelector.Length > 0)
		{
			sb.Append(globalSelector + " {" + Environment.NewLine);
			sb.Append($"{Indent(1)}--sf-ring-inset: ; --sf-ring-offset-width: 0px; --sf-ring-offset-color: #fff; --sf-ring-color: #3b82f680; --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
			sb.Append("}" + Environment.NewLine);
		}
		
		globalSelector.Clear();

		foreach (var (_, usedCssSelector) in AppState.UsedClasses)
		{
			if (usedCssSelector.ScssUtilityClassGroup?.Category == "shadow")
				globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
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

	private async Task RecurseNodeCloneAsync(ScssNode sourceNode, ScssNode destinationNode)
	{
		foreach (var childClass in sourceNode.Classes)
		{
			var selector = new CssSelector(AppState, childClass.Selector, childClass.IsArbitraryCss);

			await selector.ProcessSelectorAsync();
					
			destinationNode.Classes.Add(selector);
		}

		foreach (var childNode in sourceNode.Nodes)
		{
			var newNode = new ScssNode
			{
				Prefix = childNode.Prefix,
				PrefixPath = childNode.PrefixPath,
				Level = childNode.Level
			};
			
			destinationNode.Nodes.Add(newNode);

			await RecurseNodeCloneAsync(childNode, newNode);
		}
	}
	
	public async Task GenerateScssFromObjectTreeAsync(ScssNode scssNode, StringBuilder sb)
	{
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			var prefix = scssNode.Prefix;

			if (prefix.Equals("auto-dark", StringComparison.Ordinal))
				prefix = "dark";
			
			var mediaQueryPrefix = AppState.MediaQueryPrefixes.First(p => p.Prefix.Equals(prefix));

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
	
	#endregion
	
	#region SCSS Watcher Methods
	
	/// <summary>
	/// Remove a watched SCSS file from the watched SCSS files collection.
	/// Remove any associated CSS file from storage.
	/// </summary>
	/// <param name="filePath"></param>
	public async Task DeleteWatchedScssFileAsync(string filePath)
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
	public async Task AddUpdateWatchedScssFileAsync(string filePath, CancellationTokenSource cancellationTokenSource)
	{
		var scss = await File.ReadAllTextAsync(filePath, cancellationTokenSource.Token);
		var css = await SfumatoScss.TranspileScss(filePath, scss, this);

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
	}
	
	#endregion
	
	#region Watcher Methods
	
	/// <summary>
	/// Remove a watched file from the watched files collection.
	/// </summary>
	/// <param name="filePath"></param>
	public async Task DeleteWatchedFileAsync(string filePath)
	{
		if (filePath.Equals(AppState.SettingsFilePath, StringComparison.OrdinalIgnoreCase))
			return;

		_ = AppState.WatchedFiles.TryRemove(filePath, out _);
								
		await Task.CompletedTask;
	}

	/// <summary>
	/// Add/update a watched file in the watched files collection.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="cancellationTokenSource"></param>
	public async Task AddUpdateWatchedFileAsync(string filePath, CancellationTokenSource cancellationTokenSource)
	{
		if (filePath.Equals(AppState.SettingsFilePath, StringComparison.OrdinalIgnoreCase))
			return;

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

			if (AppState.WatchedFiles.TryAdd(filePath, newWatchedFile))
				await AppState.ProcessFileMatchesAsync(newWatchedFile);
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
	/// <param name="appState"></param>
	/// <returns></returns>
	public static bool IsMediaQueryPrefix(string prefix, SfumatoAppState? appState)
	{
		var mediaQueryPrefix = appState?.MediaQueryPrefixes.FirstOrDefault(p => p.Prefix.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(mediaQueryPrefix?.Prefix) == false;
	}

	/// <summary>
	/// Determine if a prefix is a pseudoclass prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <param name="appState"></param>
	/// <returns></returns>
	public static bool IsPseudoclassPrefix(string prefix, SfumatoAppState? appState)
	{
		var pseudoclassPrefix = appState?.PseudoclassPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		
		return string.IsNullOrEmpty(pseudoclassPrefix?.Key) == false;
	}
	
	#endregion
}

public class FileResults
{
	public int FileCount { get; set; }
	public decimal TotalBytes { get; set; }
}