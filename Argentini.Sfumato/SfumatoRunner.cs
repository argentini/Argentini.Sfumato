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
	/// Build CSS based on watched SCSS files.
	/// </summary>
	/// <param name="timer"></param>
	/// <param name="onlyFilesUsingBaseAndUtilities"></param>
	public async Task PerformCoreBuildAsync(Stopwatch timer, bool onlyFilesUsingBaseAndUtilities = false)
	{
		var fileBytes = new ConcurrentBag<decimal>();
		var tasks = new List<Task>();

		foreach (var watchedFile in AppState.WatchedScssFiles.Values)
		{
			var matches = AppState.SfumatoScssRegex.Matches(watchedFile.Scss);

			if (onlyFilesUsingBaseAndUtilities == false || (onlyFilesUsingBaseAndUtilities && matches.Any(m => m.Value.Contains("base") || m.Value.Contains("utilities"))))
			{
				tasks.Add(TranspileAsync(watchedFile, fileBytes));
			}
		}

		await Task.WhenAll(tasks);

		await Console.Out.WriteLineAsync($"Completed build of {fileBytes.Count:N0} CSS file{(fileBytes.Count != 1 ? "s" : string.Empty)} ({fileBytes.Sum().FormatBytes()}) in {timer.FormatTimer()}");
	}

	public async Task TranspileAsync(WatchedScssFile watchedFile, ConcurrentBag<decimal> fileBytes)
	{
        var css = await SfumatoScss.TranspileScssAsync(watchedFile.FilePath, watchedFile.Scss, this);
        
		fileBytes.Add(css.Length);
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
		var prefixes = cssSelector.AllVariants.ToList();

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
	/// Returns generated SCSS markup.
	/// </summary>
	/// <param name="config"></param>
	/// <returns></returns>
	public async Task<string> GenerateUtilityScssAsync()
	{
		var hierarchy = new ScssObject
		{
			Prefix = string.Empty,
			VariantsPath = string.Empty
		};

		#region Build Hierarchy

		foreach (var (_, usedCssSelector) in AppState.UsedClasses.OrderBy(c => c.Value.Depth).ThenBy(c => c.Value.VariantSortOrder).ThenBy(c => c.Value.SelectorSort).ThenBy(c => c.Value.FixedSelector))
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
						var newNode = new ScssObject
						{
							Prefix = prefix,
							VariantsPath = prefixPath
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

		if (AppState.Settings.DarkMode.Equals("class", StringComparison.OrdinalIgnoreCase))
		{
			// Search first level nodes for "dark" prefix and add additional nodes to support "theme-auto";
			// Light mode will work without any CSS since removing "theme-dark" and "theme-auto" from the
			// <html> class list will disabled dark mode altogether.
			foreach (var node in hierarchy.Nodes.ToList())
			{
				if (node.Prefix.Equals("dark", StringComparison.Ordinal) == false)
					continue;
				
				var newNode = new ScssObject
				{
					Prefix = "auto-dark",
					VariantsPath = node.VariantsPath,
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

	private async Task RecurseNodeCloneAsync(ScssObject sourceObject, ScssObject destinationObject)
	{
		foreach (var cssSelector in sourceObject.Classes)
		{
			var newCssSelector = new CssSelector(AppState, cssSelector.Selector, cssSelector.IsArbitraryCss);
			await newCssSelector.ProcessSelectorAsync();

			if (newCssSelector.IsInvalid)
				continue;
			
			newCssSelector.GetStyles();
			destinationObject.Classes.Add(newCssSelector);
		}

		foreach (var childNode in sourceObject.Nodes)
		{
			var newNode = new ScssObject
			{
				Prefix = childNode.Prefix,
				VariantsPath = childNode.VariantsPath,
				Level = childNode.Level
			};
			
			destinationObject.Nodes.Add(newNode);

			await RecurseNodeCloneAsync(childNode, newNode);
		}
	}
	
	private async Task GenerateScssFromObjectTreeAsync(ScssObject scssObject, StringBuilder sb)
	{
		if (string.IsNullOrEmpty(scssObject.Prefix) == false)
		{
			var prefix = scssObject.Prefix;

			if (prefix.Equals("auto-dark", StringComparison.Ordinal))
				prefix = "dark";
			
			var mediaQueryPrefix = AppState.MediaQueryPrefixes.First(p => p.Prefix.Equals(prefix));

			if (AppState.Settings.DarkMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssObject.Prefix == "dark")
			{
				sb.Append($"{Indent(scssObject.Level - 1)}html.theme-dark {{\n");
			}

			else if (AppState.Settings.DarkMode.Equals("class", StringComparison.OrdinalIgnoreCase) && AppState.Settings.UseAutoTheme && scssObject.Prefix == "auto-dark")
			{
				sb.Append($"{Indent(scssObject.Level - 1)}html.theme-auto {{ {mediaQueryPrefix.Statement}\n");
			}
			
			else
			{
				sb.Append($"{Indent(scssObject.Level - 1)}{mediaQueryPrefix.Statement}\n");
			}
		}
			
		if (scssObject.Classes.Count > 0)
		{
			foreach (var scssClass in scssObject.Classes)
			{
				var markup = await GenerateScssClassMarkupAsync(scssClass, AppState.StringBuilderPool, scssObject.VariantsPath);
					
				sb.Append($"{markup.Indent(scssObject.Level * IndentationSpaces).TrimEnd('\n')}\n");
			}
		}
			
		if (scssObject.Nodes.Count > 0)
		{
			foreach (var node in scssObject.Nodes)
			{
				await GenerateScssFromObjectTreeAsync(node, sb);
			}
		}
			
		if (string.IsNullOrEmpty(scssObject.Prefix) == false)
		{
			sb.Append($"{Indent(scssObject.Level - 1)}}}\n");
		}

		if (AppState.Settings.UseAutoTheme && scssObject.Prefix == "auto-dark")
		{
			sb.Append($"{Indent(scssObject.Level - 1)}}}\n");
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
		await AppState.ExamineWatchedFilesForUsedClassesAsync();

		var scss = await File.ReadAllTextAsync(filePath, cancellationTokenSource.Token);
		var css = await SfumatoScss.TranspileScssAsync(filePath, scss, this);

		if (AppState.WatchedScssFiles.TryGetValue(filePath, out var watchedScssFile))
		{
			watchedScssFile.Css = css;
			watchedScssFile.Scss = scss;
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
