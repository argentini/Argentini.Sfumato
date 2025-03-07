namespace Argentini.Sfumato;

public static class SfumatoScss
{
	#region Core Shared SCSS

	/// <summary>
	/// Get all Sfumato base SCSS include files (e.g. browser reset, static element styles) and return as a single string.
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="diagnosticOutput"></param>
	/// <returns></returns>
	public static async Task<string> GetBaseScssAsync(SfumatoAppState appState, ConcurrentDictionary<string,string> diagnosticOutput)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var sb = appState.StringBuilderPool.Get();
		
        try
        {
		    sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_core.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);
		    sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_browser-reset.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);
		    sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_media-queries.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);
		    sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_initialize.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);
            sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_forms.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);

            ProcessShortCodes(appState, sb);
            
		    diagnosticOutput.TryAdd("init2", $"{Strings.TriangleRight} Prepared SCSS base for output injection in {timer.FormatTimer()}{Environment.NewLine}");

		    return sb.ToString();
        }

        catch
        {
            return string.Empty;
        }
        
        finally
        {
            appState.StringBuilderPool.Return(sb);
        }
	}
	
	/// <summary>
	/// Get all Sfumato core SCSS include files (e.g. mixins) and return as a single string.
	/// Used as a prefix for transpile in-place project SCSS files.
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="diagnosticOutput"></param>
	/// <returns></returns>
	public static async Task<string> GetSharedScssAsync(SfumatoAppState appState, ConcurrentDictionary<string,string> diagnosticOutput)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var sb = appState.StringBuilderPool.Get();
		
        try
        {
		    sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_core.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);
		    sb.Append((await Storage.ReadAllTextWithRetriesAsync(Path.Combine(appState.ScssPath, "_media-queries.scss"), SfumatoAppState.FileAccessRetryMs)).Trim() + Environment.NewLine);

            ProcessShortCodes(appState, sb);

		    diagnosticOutput.TryAdd("init3", $"{Strings.TriangleRight} Prepared shared SCSS for output injection in {timer.FormatTimer()}{Environment.NewLine}");

            return sb.ToString();
        }

        catch
        {
            return string.Empty;
        }
        
        finally
        {
            appState.StringBuilderPool.Return(sb);
        }
	}

    /// <summary>
    /// Find/replace various system short codes in generated SCSS markup
    /// (e.g. media breakpoint values).
    /// </summary>
    /// <param name="appState"></param>
    /// <param name="sb"></param>
    private static void ProcessShortCodes(SfumatoAppState appState, StringBuilder sb)
    {
        var breakpoints = appState.StringBuilderPool.Get();
        
        try
        {
            sb.Replace("#{zero-font-size}", $"{appState.Settings.Theme.FontSizeUnit?.Zero ?? "16px"}");

            sb.Replace("#{sm-bp}", $"{appState.Settings.Theme.MediaBreakpoint?.Sm}");
            sb.Replace("#{md-bp}", $"{appState.Settings.Theme.MediaBreakpoint?.Md}");
            sb.Replace("#{lg-bp}", $"{appState.Settings.Theme.MediaBreakpoint?.Lg}");
            sb.Replace("#{xl-bp}", $"{appState.Settings.Theme.MediaBreakpoint?.Xl}");
            sb.Replace("#{xxl-bp}", $"{appState.Settings.Theme.MediaBreakpoint?.Xxl}");
            
            sb.Replace("$internal-dark-theme: \"\";", $"$internal-dark-theme: \"{(appState.Settings.DarkMode.Equals("media", StringComparison.Ordinal) ? "media" : appState.Settings.UseAutoTheme ? "class+auto" : "class")}\";");

            sb.Replace("$mobi-breakpoint: \"\";", $"$mobi-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "mobi").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            sb.Replace("$phab-breakpoint: \"\";", $"$phab-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "phab").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            sb.Replace("$tabp-breakpoint: \"\";", $"$tabp-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "tabp").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            sb.Replace("$tabl-breakpoint: \"\";", $"$tabl-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "tabl").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            sb.Replace("$desk-breakpoint: \"\";", $"$desk-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "desk").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            sb.Replace("$wide-breakpoint: \"\";", $"$wide-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "wide").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            sb.Replace("$vast-breakpoint: \"\";", $"$vast-breakpoint: \"{appState.MediaQueryPrefixes.First(p => p.Prefix == "vast").Statement.TrimStart("@media ").TrimEnd("{")?.Trim()}\";");
            
            if (appState.Settings.Theme.UseAdaptiveLayout == false)
            {
                sb.Replace("$adaptive-breakpoint-font-sizes;", string.Empty);

                try
                {
                    foreach (var prefix in appState.MediaQueryPrefixes.Where(p => p.PrefixType.Equals("breakpoint", StringComparison.OrdinalIgnoreCase) && p.Prefix.Length < 4).OrderBy(o => o.PrefixOrder))
                    {
                        var fontSize = appState.Settings.Theme.FontSizeUnit?.GetType()
                            .GetProperty(prefix.Prefix.ToSentenceCase())
                            ?.GetValue(appState.Settings.Theme.FontSizeUnit, null)
                            ?.ToString() ?? string.Empty;

                        if (string.IsNullOrEmpty(fontSize))
                            fontSize = "16px";
                        
                        if (prefix.Prefix == "xxl")
                        {
	                        fontSize = fontSize.EndsWith("vw", StringComparison.Ordinal) ? $"calc(#{{$xxl-breakpoint}} * (#{{sf-strip-unit({appState.Settings.Theme.FontSizeUnit?.Xxl})}} / 100))" : $"{appState.Settings.Theme.FontSizeUnit?.Xxl}";
                        }
                        
                        breakpoints.Append($"{prefix.Statement}\n");
                        breakpoints.Append($"    font-size: {fontSize};\n");                
                        breakpoints.Append("}\n");
                    }
                    
                    sb.Replace("$media-breakpoint-font-sizes;", breakpoints.ToString());
                }

                catch
                {
                    sb.Replace("$media-breakpoint-font-sizes;", string.Empty);
                }
            }

            else
            {
                sb.Replace("$media-breakpoint-font-sizes;", string.Empty);

                try
                {
                    foreach (var prefix in appState.MediaQueryPrefixes.Where(p => p.PrefixType.Equals("breakpoint", StringComparison.OrdinalIgnoreCase) && p.Prefix.Length > 3).OrderBy(o => o.PrefixOrder))
                    {
                        var fontSize = appState.Settings.Theme.FontSizeUnit?.GetType()
                            .GetProperty(prefix.Prefix.ToSentenceCase())
                            ?.GetValue(appState.Settings.Theme.FontSizeUnit, null);
                        
                        breakpoints.Append($"{prefix.Statement}\n");
                        breakpoints.Append($"    font-size: {fontSize};\n");                
                        breakpoints.Append("}\n");
                    }

                    sb.Replace("$adaptive-breakpoint-font-sizes;", breakpoints.ToString());
                }

                catch
                {
                    sb.Replace("$adaptive-breakpoint-font-sizes;", string.Empty);
                }
            }
        }        

        finally
        {
            appState.StringBuilderPool.Return(breakpoints);
        }
    }
    
	#endregion
	
	#region SCSS Transpiling
	
	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="rawScss"></param>
	/// <param name="runner"></param>
	/// <param name="showOutput"></param>
	/// <returns>Generated CSS file</returns>
	public static async Task<string> TranspileScssAsync(string filePath, string rawScss, SfumatoRunner runner, bool showOutput = true)
	{
		var sb = runner.AppState.StringBuilderPool.Get();
		var scss = runner.AppState.StringBuilderPool.Get();
        var styles = runner.AppState.StringBuilderPool.Get();
        var details = runner.AppState.StringBuilderPool.Get();
        var first = runner.AppState.StringBuilderPool.Get();
        var second = runner.AppState.StringBuilderPool.Get();

		try
		{
			if (string.IsNullOrEmpty(rawScss))
				rawScss = await Storage.ReadAllTextWithRetriesAsync(filePath, SfumatoAppState.FileAccessRetryMs);

			if (string.IsNullOrEmpty(rawScss))
				return string.Empty;
			
			var arguments = new List<string>();
			var cssOutputPath = filePath.TrimEnd(".scss") + ".css"; 
			var cssMapOutputPath = cssOutputPath + ".map";
			var includesBase = false;
			var includesUtilities = false;
			var includesShared = false;
			var timer = new Stopwatch();

			timer.Start();

			if (File.Exists(cssMapOutputPath))
				File.Delete(cssMapOutputPath);

			if (runner.AppState.Minify == false)
			{
				arguments.Add("--style=expanded");
				arguments.Add("--embed-sources");
			}

			else
			{
				arguments.Add("--style=compressed");
				arguments.Add("--no-source-map");
			}

			arguments.Add(filePath.Contains(Path.DirectorySeparatorChar)
				? $"--load-path={filePath[..filePath.LastIndexOf(Path.DirectorySeparatorChar)]}"
				: $"--load-path={runner.AppState.WorkingPath}");

			arguments.Add("--stdin");
			arguments.Add(cssOutputPath);
			
			#region Process @sfumato directives
			
			var matches = runner.AppState.SfumatoScssRegex.Matches(rawScss);
			var startIndex = 0;

			while (matches.Count > 0)
			{
				var match = matches[0];
				
				if (match.Index + match.Value.Length > startIndex)
					startIndex = match.Index + match.Value.Length;

				var matchValue = match.Value.CompactCss().TrimEnd(';');
				
				if (matchValue.EndsWith(" shared"))
				{
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, runner.AppState.ScssSharedInjectable.ToString());
					includesShared = true;
				}

				else if (matchValue.EndsWith(" base"))
				{
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, runner.AppState.ScssBaseInjectable.ToString());
					includesBase = true;
				}
				
				else if (matchValue.EndsWith(" utilities"))
				{
					var preamble = $"{Environment.NewLine}{Environment.NewLine}/* SFUMATO UTILITY CLASSES */{Environment.NewLine}{Environment.NewLine}";

					var utilitiesScss = runner.GenerateUtilityScss();
					
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, preamble + utilitiesScss);

					includesUtilities = true;
				}
				
				matches = runner.AppState.SfumatoScssRegex.Matches(rawScss);
			}
			
			#endregion
            
			scss.Append(rawScss);
			
			var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap(runner.AppState.SassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

			await cmd.ExecuteAsync();

            sb.Clear();
            sb.Append(await Storage.ReadAllTextWithRetriesAsync(cssOutputPath, SfumatoAppState.FileAccessRetryMs));
            
            sb.Replace("html.theme-dark :root.", "html.theme-dark.");
            sb.Replace("html.theme-auto :root.", "html.theme-auto.");
            sb.Replace("html.theme-dark html.", "html.theme-dark.");
            sb.Replace("html.theme-auto html.", "html.theme-auto.");

            sb.Replace("html.theme-dark :root", "html.theme-dark");
            sb.Replace("html.theme-auto :root", "html.theme-auto");
            sb.Replace("html.theme-dark html", "html.theme-dark");
            sb.Replace("html.theme-auto html", "html.theme-auto");
            
            #region Process @apply directives

            matches = runner.AppState.SfumatoScssApplyRegex.Matches(sb.ToString());
            startIndex = 0;

            while (matches.Count > 0)
            {
                var match = matches[0];
				
                if (match.Index + match.Value.Length > startIndex)
                    startIndex = match.Index + match.Value.Length;

                var matchValue = match.Value.Trim().CompactCss().TrimStart("@apply ");
                var classes = (matchValue?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? []).ToList();

                foreach (var selector in classes.ToList())
                {
                    if (runner.AppState.IsValidCoreClassSelector(selector) == false)
                        classes.Remove(selector);
                }

                if (classes.Count == 0)
                {
                    sb.Remove(match.Index, match.Value.Length);
                }
				
                else
                {
                    styles.Clear();

                    var addGradients = false;
                    var addRing = false;
                    var addShadow = false;
                    var addBackdrop = false;
                    var addFilter = false;
                    var addTransform = false;

                    first.Clear();
                    second.Clear();
                    
                    foreach (var selector in classes)
                    {
                        var newCssSelector = new CssSelector(runner.AppState, selector);

                        await newCssSelector.ProcessSelectorAsync();

                        if (newCssSelector.IsInvalid)
                            continue;
                        
                        #region Process global class assignments

                        if (newCssSelector.ScssUtilityClassGroup?.Category == "gradients")
                        {
                            if (addGradients == false)
                            {
                                first.Append("  --sf-gradient-from-position: ; --sf-gradient-via-position: ; --sf-gradient-to-position: ;" + Environment.NewLine);
                                addGradients = true;
                            }

                            first.Append(newCssSelector.GetStyles() + Environment.NewLine);
                        }
                        else if (newCssSelector.ScssUtilityClassGroup?.Category == "ring")
                        {
                            if (addRing == false)
                            {
                                first.Append("  --sf-ring-inset: ; --sf-ring-offset-width: 0px; --sf-ring-offset-color: #fff; --sf-ring-color: #3b82f680; --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
                                addRing = true;
                            }

                            first.Append(newCssSelector.GetStyles() + Environment.NewLine);
                        }
                        else if (newCssSelector.ScssUtilityClassGroup?.Category == "shadow")
                        {
                            if (addShadow == false)
                            {
                                first.Append("  --sf-ring-offset-shadow: 0 0 #0000; --sf-ring-shadow: 0 0 #0000; --sf-shadow: 0 0 #0000; --sf-shadow-colored: 0 0 #0000;" + Environment.NewLine);
                                addShadow = true;
                            }

                            first.Append(newCssSelector.GetStyles() + Environment.NewLine);
                        }
                        else if (newCssSelector.ScssUtilityClassGroup?.Category == "backdrop")
                        {
                            if (addBackdrop == false)
                            {
                                first.Append("  --sf-backdrop-blur: ; --sf-backdrop-brightness: ; --sf-backdrop-contrast: ; --sf-backdrop-grayscale: ; --sf-backdrop-hue-rotate: ; --sf-backdrop-invert: ; --sf-backdrop-opacity: ; --sf-backdrop-saturate: ; --sf-backdrop-sepia: ;" + Environment.NewLine);
                                addBackdrop = true;
                            }

                            first.Append(newCssSelector.GetStyles() + Environment.NewLine);
                        }
                        else if (newCssSelector.ScssUtilityClassGroup?.Category == "filter")
                        {
                            if (addFilter == false)
                            {
                                first.Append("  --sf-blur: ; --sf-brightness: ; --sf-contrast: ; --sf-drop-shadow: ; --sf-grayscale: ; --sf-hue-rotate: ; --sf-invert: ; --sf-saturate: ; --sf-sepia: ;" + Environment.NewLine);
                                addFilter = true;
                            }

                            first.Append(newCssSelector.GetStyles() + Environment.NewLine);
                        }
                        else if (newCssSelector.ScssUtilityClassGroup?.Category == "transform")
                        {
                            if (addTransform == false)
                            {
                                first.Append("  --sf-translate-x: 0; --sf-translate-y: 0; --sf-rotate: 0; --sf-skew-x: 0; --sf-skew-y: 0; --sf-scale-x: 1; --sf-scale-y: 1;" + Environment.NewLine);
                                addTransform = true;
                            }

                            first.Append(newCssSelector.GetStyles() + Environment.NewLine);
                        }
                        else
                        {
                            second.Append(newCssSelector.GetStyles());
                        }

                        #endregion
                    }

                    styles.Append(first);
                    
                    if (addBackdrop)
                    {
                        styles.Append(
                            "  backdrop-filter: var(--sf-backdrop-blur) var(--sf-backdrop-brightness) var(--sf-backdrop-contrast) var(--sf-backdrop-grayscale) var(--sf-backdrop-hue-rotate) var(--sf-backdrop-invert) var(--sf-backdrop-opacity) var(--sf-backdrop-saturate) var(--sf-backdrop-sepia);" + Environment.NewLine +
                            "  -webkit-backdrop-filter: var(--sf-backdrop-blur) var(--sf-backdrop-brightness) var(--sf-backdrop-contrast) var(--sf-backdrop-grayscale) var(--sf-backdrop-hue-rotate) var(--sf-backdrop-invert) var(--sf-backdrop-opacity) var(--sf-backdrop-saturate) var(--sf-backdrop-sepia);" + Environment.NewLine
                        );
                    }
                    
                    if (addFilter)
                    {
                        styles.Append(
                            "  filter: var(--sf-blur) var(--sf-brightness) var(--sf-contrast) var(--sf-drop-shadow) var(--sf-grayscale) var(--sf-hue-rotate) var(--sf-invert) var(--sf-saturate) var(--sf-sepia);" + Environment.NewLine
                        );
                    }

                    if (addTransform)
                    {
                        styles.Append(
                            "  transform: translate(var(--sf-translate-x), var(--sf-translate-y)) rotate(var(--sf-rotate)) skewX(var(--sf-skew-x)) skewY(var(--sf-skew-y)) scaleX(var(--sf-scale-x)) scaleY(var(--sf-scale-y));" + Environment.NewLine
                        );
                    }

                    styles.Append(second);

                    sb.Remove(match.Index, match.Value.Length);
                    sb.Insert(match.Index, styles.ToString().Trim().TrimEnd(';').Trim().CompactCss());
                }
				
                matches = runner.AppState.SfumatoScssApplyRegex.Matches(sb.ToString());
            }

            #endregion
            
            #region Process single value directives

            matches = runner.AppState.SfumatoScssValueRegex.Matches(sb.ToString());
            startIndex = 0;
            
            while (matches.Count > 0)
            {
	            var match = matches[0];
				
	            if (match.Index + match.Value.Length > startIndex)
		            startIndex = match.Index + match.Value.Length;

	            var selector = match.Value.TrimStart("var(").TrimEnd(")")?.Trim() ?? string.Empty;

	            if (runner.AppState.IsValidCoreClassSelector(selector))
	            {
		            styles.Clear();
		            first.Clear();
		            second.Clear();
                    
		            var newCssSelector = new CssSelector(runner.AppState, selector);

		            await newCssSelector.ProcessSelectorAsync();

		            sb.Remove(match.Index, match.Value.Length);

		            if (newCssSelector.IsInvalid == false)
		            {
			            var value = newCssSelector.GetStyles().Trim();

			            if (value.Contains('\r') == false && value.Contains('\n') == false && value.IndexOf(':') > 0 && value.IndexOf(':') < value.Length - 1)
			            {
				            value = value[(value.IndexOf(':') + 1)..].Trim();
				            sb.Insert(match.Index, value.TrimEnd(';').CompactCss());
			            }
		            }
	            }
	            else
	            {
		            sb.Remove(match.Index, match.Value.Length);
	            }
				
	            matches = runner.AppState.SfumatoScssValueRegex.Matches(sb.ToString());
            }

            #endregion
            
            await File.WriteAllTextAsync(cssOutputPath, sb.ToString());

			if (showOutput == false)
				return sb.ToString();
			
			details.Clear();
				
			if (includesBase)
				details.Append(", +base");
				
			if (includesUtilities)
				details.Append($", +{runner.AppState.UsedClasses.Count(u => u.Value.IsInvalid == false):N0} utilities");

			if (includesShared)
				details.Append(", +shared");

			if (runner.AppState.Minify)
				details.Append(", minified");
				
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Generated {SfumatoRunner.ShortenPathForOutput(filePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) + ".css", runner.AppState)} ({sb.Length.FormatBytes()}{details}) in {timer.FormatTimer()}");
            
			return sb.ToString();
		}

		catch
		{
			var error = sb.ToString();

			if (error.IndexOf($"Command:{Environment.NewLine}", StringComparison.OrdinalIgnoreCase) > -1)
			{
				error = error[..error.IndexOf($"Command:{Environment.NewLine}", StringComparison.OrdinalIgnoreCase)].Trim();
			}
			
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} {SfumatoRunner.ShortenPathForOutput(filePath, runner.AppState)} => {error}");

			return string.Empty;
		}
        
        finally
        {
            runner.AppState.StringBuilderPool.Return(sb);
            runner.AppState.StringBuilderPool.Return(scss);
            runner.AppState.StringBuilderPool.Return(styles);
            runner.AppState.StringBuilderPool.Return(details);
            runner.AppState.StringBuilderPool.Return(first);
            runner.AppState.StringBuilderPool.Return(second);
        }
	}
	
	#endregion
}

public class CssMediaQuery
{
	public int PrefixOrder { get; init; }
	public int Priority { get; init; }
	public string Prefix { get; init; } = string.Empty;
	public string PrefixType { get; init; } = string.Empty;
	public string Statement { get; init; } = string.Empty;
}