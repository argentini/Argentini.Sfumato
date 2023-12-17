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

    private static ScssMediaQuery? EnsureMediaQueryPathExists(SfumatoAppState appState, string fullMediaQueryVariantPath, ScssMediaQuery? rootNode, int index = 0, ScssMediaQuery? currentNode = null)
    {
        if (fullMediaQueryVariantPath == string.Empty)
            return rootNode;

        if (fullMediaQueryVariantPath.EndsWith(':') == false)
            fullMediaQueryVariantPath += ':';
        
        if (index == 0 || currentNode is null)
            currentNode = rootNode;
        
        var variantSegments = fullMediaQueryVariantPath.Split(':', StringSplitOptions.RemoveEmptyEntries);

        if (variantSegments.Length == 0 || index > variantSegments.Length - 1)
            return rootNode;
        
        //var variant = variantSegments[index];
        var partialMediaQueryVariantPath = string.Join(':', variantSegments[..(index + 1)]) + ':';
        var existingNode = currentNode?.MediaQueries.FirstOrDefault(n => n.PrefixPath == partialMediaQueryVariantPath);

        if (existingNode is null)
        {
            existingNode = new ScssMediaQuery(appState, partialMediaQueryVariantPath);
            currentNode?.MediaQueries.Add(existingNode);
        }

        if (variantSegments.Length <= index + 1)
            return existingNode;

        index++;

        return EnsureMediaQueryPathExists(appState, fullMediaQueryVariantPath, rootNode, index, existingNode);
    }

    public string GenerateScssGraphMarkup()
    {
        var scss = AppState.StringBuilderPool.Get();
        var scssRootNode = new ScssMediaQuery(AppState, string.Empty, -1);
        
        #region Generate SCSS Tree
        
        var usedCssSelectors = AppState.UsedClasses
            .OrderBy(c => c.Value.Depth)
            .ThenBy(c => c.Value.VariantSortOrder)
            .ThenBy(c => c.Value.SelectorSort)
            .ThenBy(c => c.Value.FixedSelector)
            .ToList();

        foreach (var (_, usedCssSelector) in usedCssSelectors)
        {
            if (usedCssSelector.IsInvalid)
                continue;

            if (string.IsNullOrEmpty(usedCssSelector.ScssMarkup))
                usedCssSelector.GetStyles();
            
            if (usedCssSelector is { IsArbitraryCss: false, ScssMarkup: "" })
            {
                usedCssSelector.IsInvalid = true;
                continue;
            }
            
            var compactScss = usedCssSelector.ScssMarkup.CompactCss();
            var pseudoclassSuffix = string.Empty;
            var parentNode = EnsureMediaQueryPathExists(AppState, string.Join(':', usedCssSelector.MediaQueryVariants), scssRootNode);

            foreach (var pseudoClass in usedCssSelector.PseudoClassVariants)
            {
                if (AppState.PseudoclassPrefixes.TryGetValue(pseudoClass, out var prefix))
                    pseudoclassSuffix += prefix;
            }

            var existingClass = parentNode?.ScssClasses.FirstOrDefault(c => c.CompactScssProperties == compactScss && c.PseudoclassSuffix == pseudoclassSuffix);
            
            if (existingClass is null)
            {
                parentNode?.ScssClasses.Add(new ScssClass
                {
                    Depth = parentNode.Depth + 1,
                    Selectors = { $".{usedCssSelector.EscapedSelector}" },
                    PseudoclassSuffix = pseudoclassSuffix,
                    CompactScssProperties = compactScss,
                    ScssProperties = usedCssSelector.ScssMarkup
                });

                continue;
            }
            
            existingClass.Selectors.Add($".{usedCssSelector.EscapedSelector}");
        }

        #endregion        
        
        #region Process global class assignments

        var globalSelector = AppState.StringBuilderPool.Get();

        foreach (var (_, usedCssSelector) in AppState.UsedClasses)
        {
            if (usedCssSelector.ScssUtilityClassGroup?.Category == "gradients")
                globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
        }

        if (globalSelector.Length > 0)
        {
            scss.Append(globalSelector + " {" + Environment.NewLine);
            scss.Append($"{Indent(1)}--sf-gradient-from-position: ; --sf-gradient-via-position: ; --sf-gradient-to-position: ;" + Environment.NewLine);
            scss.Append("}" + Environment.NewLine);
        }

        globalSelector.Clear();

        foreach (var (_, usedCssSelector) in AppState.UsedClasses)
        {
            if (usedCssSelector.ScssUtilityClassGroup?.Category == "ring")
                globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
        }

        if (globalSelector.Length > 0)
        {
            scss.Append(globalSelector + " {" + Environment.NewLine);
            scss.Append($"{Indent(1)}--sf-ring-inset: ; --sf-ring-offset-width: 0px; --sf-ring-offset-color: #fff; --sf-ring-color: #3b82f680; --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
            scss.Append("}" + Environment.NewLine);
        }

        globalSelector.Clear();

        foreach (var (_, usedCssSelector) in AppState.UsedClasses)
        {
            if (usedCssSelector.ScssUtilityClassGroup?.Category == "shadow")
                globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
        }

        if (globalSelector.Length > 0)
        {
            scss.Append(globalSelector + " {" + Environment.NewLine);
            scss.Append($"{Indent(1)}--sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
            scss.Append("}" + Environment.NewLine);
        }

        AppState.StringBuilderPool.Return(globalSelector);

        #endregion
        
        #region Generate SCSS

        scss.Append(scssRootNode.GetScssMarkup());
        
        #endregion
        
        var result = scss.ToString();
        
        AppState.StringBuilderPool.Return(scss);

        return result;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public static string GenerateSingleClassMarkup(SfumatoAppState appState, IEnumerable<KeyValuePair<string, CssSelector>> usedCssClasses, int rootIndent = 0)
    {
        List<ScssElement> scssNodes = [];
        var scss = appState.StringBuilderPool.Get();
        var suffix = appState.StringBuilderPool.Get();
        var indent = rootIndent;
        var usedCssClassesCopy = usedCssClasses.ToList();

        foreach (var (_, usedCssSelector) in usedCssClassesCopy)
        {
            if (usedCssSelector.IsInvalid)
                continue;

            if (string.IsNullOrEmpty(usedCssSelector.ScssMarkup))
                usedCssSelector.GetStyles();
            
            if (usedCssSelector is { IsArbitraryCss: false, ScssMarkup: "" })
            {
                usedCssSelector.IsInvalid = true;
                continue;
            }
            
            var styles = usedCssSelector.ScssMarkup;
            var compactStyles = styles.CompactCss();
            var existingScssNode = scssNodes.FirstOrDefault(s =>
                s.VariantsPath.InvariantEquals(usedCssSelector.VariantSegment) &&
                s.CompactStyles == compactStyles);

            if (existingScssNode is null)
            {
                scssNodes.Add(new ScssElement
                {
                    Classes = { usedCssSelector },
                    CompactStyles = compactStyles,
                    Styles = styles
                });
            }

            else
            {
                existingScssNode.Classes.Add(usedCssSelector);
            }
        }

        foreach (var node in scssNodes)
        {
            suffix.Clear();
            indent = rootIndent;
            scss.Append($"{" ".Repeat(rootIndent * 4)}{node.Selector} {{{Environment.NewLine}");
            indent++;

            foreach (var variant in node.AllVariants)
            {
                var pseudoClassVariant = appState.PseudoclassPrefixes.FirstOrDefault(p => p.Key == variant);

                if (string.IsNullOrEmpty(pseudoClassVariant.Key))
                    continue;

                scss.Append($"{" ".Repeat(indent * 4)}{pseudoClassVariant.Value}{Environment.NewLine}");
                suffix.Append($"{" ".Repeat(indent * 4)}}}{Environment.NewLine}");
                indent++;
            }
            
            scss.Append($"{node.Styles.Indent(indent * 4)}{Environment.NewLine}");
            
            if (node.AllVariants.Count != 0)
            {
                scss.Append($"{suffix}");
            }

            scss.Append($"{" ".Repeat(rootIndent * 4)}}}{Environment.NewLine}{Environment.NewLine}");
        }
        
        var result = scss.ToString();
        
        appState.StringBuilderPool.Return(suffix);
        appState.StringBuilderPool.Return(scss);

        return result;
    }
    
    /// <summary>
    /// Generate utility class SCSS markup.
    /// </summary>
    /// <returns></returns>
    public string GenerateUtilityScss()
    {
        //todo: REMOVE
        _ = GenerateScssGraphMarkup();

        
        
        
        
        
        
        var scss = AppState.StringBuilderPool.Get();
        var suffix = AppState.StringBuilderPool.Get();
        var globalSelector = AppState.StringBuilderPool.Get();
        
        #region Process global class assignments

        foreach (var (_, usedCssSelector) in AppState.UsedClasses)
        {
            if (usedCssSelector.ScssUtilityClassGroup?.Category == "gradients")
                globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
        }

        if (globalSelector.Length > 0)
        {
            scss.Append(globalSelector + " {" + Environment.NewLine);
            scss.Append($"{Indent(1)}--sf-gradient-from-position: ; --sf-gradient-via-position: ; --sf-gradient-to-position: ;" + Environment.NewLine);
            scss.Append("}" + Environment.NewLine);
        }

        globalSelector.Clear();

        foreach (var (_, usedCssSelector) in AppState.UsedClasses)
        {
            if (usedCssSelector.ScssUtilityClassGroup?.Category == "ring")
                globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
        }

        if (globalSelector.Length > 0)
        {
            scss.Append(globalSelector + " {" + Environment.NewLine);
            scss.Append($"{Indent(1)}--sf-ring-inset: ; --sf-ring-offset-width: 0px; --sf-ring-offset-color: #fff; --sf-ring-color: #3b82f680; --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
            scss.Append("}" + Environment.NewLine);
        }

        globalSelector.Clear();

        foreach (var (_, usedCssSelector) in AppState.UsedClasses)
        {
            if (usedCssSelector.ScssUtilityClassGroup?.Category == "shadow")
                globalSelector.Append((globalSelector.Length > 0 ? "," : string.Empty) + $".{usedCssSelector.EscapedSelector}");
        }

        if (globalSelector.Length > 0)
        {
            scss.Append(globalSelector + " {" + Environment.NewLine);
            scss.Append($"{Indent(1)}--sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
            scss.Append("}" + Environment.NewLine);
        }

        #endregion
        
        #region Process root classes (no media queries)

        var usedRootClasses = AppState.UsedClasses
            .Where(c => c.Value.MediaQueryVariants.Count == 0)
            .OrderBy(c => c.Value.Depth)
            .ThenBy(c => c.Value.VariantSortOrder)
            .ThenBy(c => c.Value.SelectorSort)
            .ThenBy(c => c.Value.FixedSelector).ToList();

        scss.Append(GenerateSingleClassMarkup(AppState, usedRootClasses));
        
        #endregion

        #region Process media queries (no dark theme)
        
        var usedNestedClasses = AppState.UsedClasses
            .Where(c => c.Value.MediaQueryVariants.Count > 0 && c.Value.MediaQueryVariants[0] != "dark")
            .OrderBy(c => c.Value.Depth)
            .ThenBy(c => c.Value.VariantSortOrder)
            .ThenBy(c => c.Value.SelectorSort)
            .ThenBy(c => c.Value.FixedSelector).ToList();

        foreach (var variantsPath in usedNestedClasses.Select(c => string.Join(':', c.Value.MediaQueryVariants)).Distinct())
        {
            var prefixes = variantsPath.Split(':');
            var indent = 0;

            suffix.Clear();
            
            foreach (var prefix in prefixes)
            {
                var variant = AppState.MediaQueryPrefixes.FirstOrDefault(p => p.Prefix == prefix);
                
                if (variant is null)
                    continue;

                scss.Append($"{" ".Repeat(indent * 4)}{variant.Statement}{Environment.NewLine}");
                suffix.Append($"{" ".Repeat(indent * 4)}}}{Environment.NewLine}");
                indent++;
            }
            
            scss.Append(GenerateSingleClassMarkup(AppState, usedNestedClasses.Where(c => string.Join(':', c.Value.MediaQueryVariants) == variantsPath).ToList(), indent));
            scss.Append(suffix);
        }

        #endregion
        
        #region Process dark theme classes
        
        usedNestedClasses = AppState.UsedClasses
            .Where(c => c.Value.MediaQueryVariants.Count > 0 && c.Value.MediaQueryVariants[0] == "dark")
            .OrderBy(c => c.Value.Depth)
            .ThenBy(c => c.Value.VariantSortOrder)
            .ThenBy(c => c.Value.SelectorSort)
            .ThenBy(c => c.Value.FixedSelector).ToList();

        if (usedNestedClasses.Count > 0)
        {
            if (AppState.Settings.DarkMode.InvariantEquals("class"))
            {
                var darkClass = AppState.StringBuilderPool.Get();
                var autoClass = AppState.StringBuilderPool.Get();

                darkClass.Append($"html.theme-dark {{{Environment.NewLine}");
                    
                if (AppState.Settings.UseAutoTheme)
                {
                    autoClass.Append($"html.theme-auto {{{Environment.NewLine}");
                    autoClass.Append($"{" ".Repeat(4)}{AppState.MediaQueryPrefixes.First(p => p.Prefix == "dark").Statement}{Environment.NewLine}");
                }
                
                foreach (var variantsPath in usedNestedClasses.Select(c => string.Join(':', c.Value.MediaQueryVariants)).Distinct())
                {
                    var prefixes = variantsPath.Split(':');
                    var indent = 0;

                    suffix.Clear();

                    indent++;
                    
                    foreach (var prefix in prefixes)
                    {
                        if (prefix == "dark")
                            continue;

                        var variant = AppState.MediaQueryPrefixes.FirstOrDefault(p => p.Prefix == prefix);

                        if (variant is null)
                            continue;

                        darkClass.Append($"{" ".Repeat(indent * 4)}{variant.Statement}{Environment.NewLine}");

                        if (AppState.Settings.UseAutoTheme)
                            autoClass.Append($"{" ".Repeat((indent + 1) * 4)}{variant.Statement}{Environment.NewLine}");
                        
                        suffix.Append($"{" ".Repeat(indent * 4)}}}{Environment.NewLine}");
                        
                        indent++;
                    }
                
                    darkClass.Append(GenerateSingleClassMarkup(AppState, usedNestedClasses.Where(c => string.Join(':', c.Value.MediaQueryVariants) == variantsPath).ToList(), indent));
                    darkClass.Append(suffix);

                    if (AppState.Settings.UseAutoTheme == false)
                        continue;
                    
                    autoClass.Append(GenerateSingleClassMarkup(AppState, usedNestedClasses.Where(c => string.Join(':', c.Value.MediaQueryVariants) == variantsPath).ToList(), indent));
                    autoClass.Append(suffix.ToString().Indent(2 * 4));
                }

                darkClass.Append($"}}{Environment.NewLine}");

                if (AppState.Settings.UseAutoTheme)
                {
                    autoClass.Append($"{" ".Repeat(1 * 4)}}}{Environment.NewLine}");
                    autoClass.Append($"}}{Environment.NewLine}");
                }

                scss.Append(darkClass);

                if (AppState.Settings.UseAutoTheme)
                    scss.Append(autoClass);
                
                AppState.StringBuilderPool.Return(darkClass);
                AppState.StringBuilderPool.Return(autoClass);
            }

            else
            {
                foreach (var variantsPath in usedNestedClasses.Select(c => string.Join(':', c.Value.MediaQueryVariants)).Distinct())
                {
                    var prefixes = variantsPath.Split(':');
                    var indent = 0;

                    suffix.Clear();

                    
                    foreach (var prefix in prefixes)
                    {
                        var variant = AppState.MediaQueryPrefixes.FirstOrDefault(p => p.Prefix == prefix);
                    
                        if (variant is null)
                            continue;

                        scss.Append($"{" ".Repeat(indent * 4)}{variant.Statement}{Environment.NewLine}");
                        suffix.Append($"{" ".Repeat(indent * 4)}}}{Environment.NewLine}");
                        indent++;
                    }
                
                    scss.Append(GenerateSingleClassMarkup(AppState, usedNestedClasses.Where(c => string.Join(':', c.Value.MediaQueryVariants) == variantsPath).ToList(), indent));
                    scss.Append(suffix);
                }
            }
        }

        #endregion
        
        var result = scss.ToString();
        
        AppState.StringBuilderPool.Return(globalSelector);
        AppState.StringBuilderPool.Return(suffix);
        AppState.StringBuilderPool.Return(scss);

        return result;
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
