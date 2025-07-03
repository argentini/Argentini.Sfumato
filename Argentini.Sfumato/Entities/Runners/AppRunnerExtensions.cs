namespace Argentini.Sfumato.Entities.Runners;

public static class AppRunnerExtensions
{
	/// <summary>
	/// Appends the CSS reset styles, if enabled.
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder AppendResetCss(this StringBuilder sourceCss, AppRunner appRunner)
	{
		if (appRunner.AppRunnerSettings.UseReset)
		{
			if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
				sourceCss
					.Append("@layer base {")
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);	
			
			sourceCss
				.Append(appRunner.BrowserResetCss)
				.Append(appRunner.AppRunnerSettings.LineBreak)
				.Append(appRunner.AppRunnerSettings.LineBreak);
			
			if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
				sourceCss
					.Append('}')
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		return sourceCss;
	}

	/// <summary>
	/// Appends the forms CSS styles, if enabled.
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder AppendFormsCss(this StringBuilder sourceCss, AppRunner appRunner)
	{
		try
		{
			if (appRunner.AppRunnerSettings.UseForms)
			{
				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
					sourceCss
						.Append("@layer forms {")
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);	

				sourceCss
					.Append(appRunner.FormsCss)
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);
				
				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
					sourceCss
						.Append('}')
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);
			}
		}		
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}AppendFormsCss() - {e.Message}");
			Environment.Exit(1);
		}
		
		return sourceCss;
	}

	/// <summary>
	/// Appends a text marker for later replacement by InjectUtilityClassesCss().
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder AppendUtilityClassMarker(this StringBuilder sourceCss, AppRunner appRunner)
	{
		try
		{
			sourceCss
				.Append("::sfumato{}")
				.Append(appRunner.AppRunnerSettings.LineBreak)
				.Append(appRunner.AppRunnerSettings.LineBreak);
		}		
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}AppendUtilityClassMarker() - {e.Message}");
			Environment.Exit(1);
		}
			
		return sourceCss;
	}

	/// <summary>
	/// Appends the processed CSS from AppRunnerSettings.
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder AppendProcessedSourceCss(this StringBuilder sourceCss, AppRunner appRunner)
	{
		try
		{
			sourceCss
				.Append(appRunner.AppRunnerSettings.ProcessedCssContent.Trim());
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}AppendProcessedSourceCss() - {e.Message}");
			Environment.Exit(1);
		}
		
		return sourceCss;
	}

	/// <summary>
    /// Convert @apply statements in source CSS to utility class property statements.
    /// Also tracks any used CSS custom properties or CSS snippets.
    /// </summary>
    /// <param name="appRunner"></param>
    /// <param name="sourceCss"></param>
	public static StringBuilder ProcessAtApplyStatementsAndTrackDependencies(this StringBuilder sourceCss, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			foreach (var span in sourceCss.ToString().EnumerateAtApplyStatements())
			{
				var utilityClassStrings = span.Arguments.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var utilityClasses = utilityClassStrings
					.Select(utilityClass => new CssClass(appRunner, utilityClass.Replace("\\", string.Empty)))
					.Where(cssClass => cssClass.IsValid)
					.ToList();

				workingSb.Clear();

				if (utilityClasses.Count > 0)
				{
					foreach (var utilityClass in utilityClasses.OrderBy(c => c.SelectorSort))
						workingSb.Append(utilityClass.Styles);
				}

				sourceCss.Replace(span.Full, workingSb.ToString().Trim());
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessAtApplyStatementsAndTrackDependencies() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}

		return sourceCss;
	}
	
    /// <summary>
    /// Convert @variant statements in source CSS to media query statements.
    /// </summary>
    /// <param name="appRunner"></param>
    /// <param name="sourceCss"></param>
	public static StringBuilder ProcessAtVariantStatements(this StringBuilder sourceCss, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			workingSb.Append(sourceCss);
			
			foreach (var span in sourceCss.ToString().EnumerateAtVariantStatements())
			{
				if (span.Name.ToString().TryVariantIsMediaQuery(appRunner, out var variant))
					workingSb.Replace(span.Full, $"@{variant?.PrefixType} {variant?.Statement} {{");
			}

			sourceCss.Clear().Append(workingSb);
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessAtVariantStatements() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}

		return sourceCss;
	}

    /// <summary>
    /// Convert functions in source CSS to CSS statements (e.g. --alpha()).
    /// Also tracks any used CSS custom properties or CSS snippets.
    /// </summary>
    /// <param name="appRunner"></param>
    /// <param name="sourceCss"></param>
	public static StringBuilder ProcessFunctionsAndTrackDependencies(this StringBuilder sourceCss, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			workingSb.Append(sourceCss);

			foreach (var match in sourceCss.EnumerateTokenWithOuterParenthetical("--alpha"))
			{
				if (match.Contains("var(--color-", StringComparison.Ordinal) == false || match.Contains('%') == false)
					continue;
				
				var colorKey = match[..match.IndexOf(')')]
					.TrimStart("--alpha(")?.Trim()
					.TrimStart("var(")?.Trim()
					.TrimStart("--color-");

				if (string.IsNullOrEmpty(colorKey))
					continue;

				if (appRunner.Library.ColorsByName.TryGetValue(colorKey, out var colorValue) == false)
					continue;

				var alphaValue = match[(match.LastIndexOf('/') + 1)..].TrimEnd(')', '%', ' ').Trim();

				if (int.TryParse(alphaValue, out var pct))
					workingSb.Replace(match, colorValue.SetWebColorAlpha(pct));
			}
			
			foreach (var match in sourceCss.EnumerateTokenWithOuterParenthetical("--spacing"))
			{
				if (match.Length <= 11)
					continue;

				var valueString = match.TrimStart("--spacing(")?.Trim().TrimEnd(')', ' ');

				if (string.IsNullOrEmpty(valueString))
					continue;

				if (double.TryParse(valueString, out var value) == false)
					continue;

				workingSb.Replace(match, $"calc(var(--spacing) * {value})");

				appRunner.UsedCssCustomProperties.TryAdd("--spacing", string.Empty);
			}

			sourceCss.Clear().Append(workingSb);
			
			foreach (var span in sourceCss.ToString().EnumerateCssCustomProperties(namesOnly: true))
			{
				if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(span.Property.ToString(), out var value))
					appRunner.UsedCssCustomProperties.TryAdd(span.Property.ToString(), value);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessFunctionsAndTrackDependencies() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}

		return sourceCss;
	}
	
	/// <summary>
	/// Iterate UsedCssCustomProperties[] and UsedCss[] and set their values from AppRunnerSettings.SfumatoBlockItems[].
	/// </summary>
	/// <param name="appRunner"></param>
	private static void ProcessTrackedDependencyValues(AppRunner appRunner)
	{
		try
		{
			foreach (var usedCssCustomProperty in appRunner.UsedCssCustomProperties.ToList())
			{
				if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(usedCssCustomProperty.Key, out var value) == false)
					continue;
					
				appRunner.UsedCssCustomProperties[usedCssCustomProperty.Key] = value;

				if (value.Contains("--") == false)
					continue;
					
				foreach (var span in value.EnumerateCssCustomProperties(namesOnly: true))
				{
					if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(span.Property.ToString(), out var valueValue))
					{
						appRunner.UsedCssCustomProperties.TryAdd(span.Property.ToString(), valueValue);
						
						foreach (var span2 in valueValue.EnumerateCssCustomProperties(namesOnly: true))
						{
							if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(span2.Property.ToString(), out var valueValue2))
								appRunner.UsedCssCustomProperties.TryAdd(span2.Property.ToString(), valueValue2);
						}
					}
				}
			}

			foreach (var usedCss in appRunner.UsedCss.ToList())
			{
				if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(usedCss.Key, out var value) == false)
					continue;
					
				appRunner.UsedCss[usedCss.Key] = value;
						
				if (value.Contains("--") == false)
					continue;
					
				foreach (var span in value.EnumerateCssCustomProperties(namesOnly: true))
				{
					if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(span.Property.ToString(), out var valueValue))
						appRunner.UsedCssCustomProperties.TryAdd(span.Property.ToString(), valueValue);
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessTrackedDependencyValues() - {e.Message}");
			Environment.Exit(1);
		}
	}

	/// <summary>
	/// Generate :root{} CSS from the UsedCssCustomProperties[] and UsedCss[] dictionaries.
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder InjectRootDependenciesCss(this StringBuilder sourceCss, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();
		var workingSb2 = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			ProcessTrackedDependencyValues(appRunner);

			if (appRunner.UsedCssCustomProperties.IsEmpty == false)
			{
				#region @layer properties
				
				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
					workingSb
						.Append("@layer properties {")
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);

				workingSb
					.Append("*, ::before, ::after, ::backdrop {")
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);

				foreach (var ccp in appRunner.UsedCssCustomProperties.Where(c => (c.Key.StartsWith("--sf-") || c.Key.StartsWith("--form-")) && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
				{
					if (appRunner.AppRunnerSettings.UseForms == false && ccp.Key.StartsWith("--form-"))
						continue;
					
					workingSb
						.Append(ccp.Key)
						.Append(": ")
						.Append(ccp.Value)
						.Append(';');
				}

				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
					workingSb
						.Append('}')
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);
						
				workingSb
					.Append('}')
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);	
				
				#endregion

				#region @property rules
				
				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
				{
					foreach (var ccp in appRunner.UsedCssCustomProperties)
					{
						if (ccp.Key.StartsWith('-') == false)
							continue;
						
						if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue($"@property {ccp.Key}", out var prop) == false)
							continue;
						
						workingSb2
							.Append($"@property {ccp.Key} ")
							.Append(prop)
							.Append(appRunner.AppRunnerSettings.LineBreak);
					}
				}				

				#endregion
				
				#region @layer theme
				
				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
					workingSb
						.Append("@layer theme {")
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);

				workingSb
					.Append(":root, :host {")
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);
			
				foreach (var ccp in appRunner.UsedCssCustomProperties.Where(c => c.Key.StartsWith("--sf-") == false && c.Key.StartsWith("--form-") == false && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
				{
					workingSb
						.Append(ccp.Key)
						.Append(": ")
						.Append(ccp.Value)
						.Append(';');
					
					if (ccp.Key.StartsWith("--animate-", StringComparison.Ordinal) == false)
						continue;
						
					var key = $"@keyframes {ccp.Key.TrimStart("--animate-")}";

					if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(key, out var value))
						appRunner.UsedCss.TryAdd(key, value);
				}

				if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
					workingSb
						.Append('}')
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);
						
				workingSb
					.Append('}')
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);	
				
				#endregion
				
				#region @keyframes, etc.
				
				if (appRunner.UsedCss.Count > 0)
				{
					foreach (var ccp in appRunner.UsedCss.Where(c => string.IsNullOrEmpty(c.Value) == false))
					{
						workingSb2
							.Append(ccp.Key)
							.Append(' ')
							.Append(ccp.Value);
					}
				}
				
				#endregion
			}
			
			sourceCss.Insert(0, workingSb);
			sourceCss.Append(workingSb2);
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}InjectRootDependenciesCss() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
			appRunner.AppState.StringBuilderPool.Return(workingSb2);
		}
		
		return sourceCss;
	}

	/// <summary>
	/// Generate utility class CSS from the AppRunner.UtilityClasses dictionary. 
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	public static StringBuilder InjectUtilityClassesCss(this StringBuilder sourceCss, AppRunner appRunner)
	{
		var root = new VariantBranch
		{
			Fingerprint = 0,
			Depth = 0,
			WrapperCss = string.Empty
		};

		foreach (var cssClass in appRunner.UtilityClasses.Values.OrderBy(c => c.WrapperSort))
			_ProcessVariantBranchRecursive(root, cssClass);
		
		var sb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
				sb
					.Append("@layer utilities {")
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);	

			_GenerateUtilityClassesCss(root, sb);

			if (appRunner.AppRunnerSettings.UseCompatibilityMode == false)
				sb
					.Append('}')
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);	

			sourceCss.Replace("::sfumato{}", sb.ToString());
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}InjectUtilityClassesCss() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(sb);
		}
		
		return sourceCss;
	}

	public static StringBuilder MoveComponentsLayer(this StringBuilder sourceCss, AppRunner appRunner)
	{
		if (appRunner.AppRunnerSettings.UseCompatibilityMode)
			return sourceCss;

		// Pre-cache frequently accessed values
		var lineBreak = appRunner.AppRunnerSettings.LineBreak;
		var componentsLayerText = "@layer components";
		var utilitiesLayerText = "@layer utilities";
		
		var sb = appRunner.AppState.StringBuilderPool.Get();
		var blocksFound = false;
		var searchIndex = 0;

		try
		{
			// Process all blocks in a single pass, collecting removal ranges
			var removalRanges = new List<(int start, int length)>();
			
			while (true)
			{
				var (start, length) = sourceCss.ExtractCssBlock(componentsLayerText, searchIndex);
				
				if (start == -1 || length == 0)
					break;

				blocksFound = true;
				
				// Extract content between braces more efficiently
				var openBraceIndex = start + componentsLayerText.Length;
				
				// Skip whitespace to find opening brace
				while (openBraceIndex < start + length && char.IsWhiteSpace(sourceCss[openBraceIndex]))
					openBraceIndex++;
				
				if (openBraceIndex < start + length && sourceCss[openBraceIndex] == '{')
				{
					var contentStart = openBraceIndex + 1;
					var contentLength = length - (contentStart - start) - 1; // -1 for closing brace
					
					sb.Append(sourceCss, contentStart, contentLength);
					sb.Append(lineBreak);
				}
				
				// Store removal range for later (process in reverse order)
				removalRanges.Add((start, length));
				searchIndex = start + length;
			}

			if (!blocksFound)
				return sourceCss;

			// Remove blocks in reverse order to maintain indices
			for (int i = removalRanges.Count - 1; i >= 0; i--)
			{
				var (start, length) = removalRanges[i];
				sourceCss.Remove(start, length);
			}

			// Build the consolidated block
			sb.Insert(0, componentsLayerText + " {" + lineBreak);
			sb.Append("}" + lineBreak);

			// Find insertion point
			var insertionPoint = sourceCss.IndexOf(utilitiesLayerText, 0, StringComparison.Ordinal);
			if (insertionPoint < 0)
				insertionPoint = sourceCss.Length;

			sourceCss.Insert(insertionPoint, sb);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(sb);
		}

		return sourceCss;
	}

	/// <summary>
	/// Find all dark theme media blocks and duplicate as wrapped classes theme-dark
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder ProcessDarkThemeClasses(this StringBuilder sourceCss, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();
		var darkSb = appRunner.AppState.StringBuilderPool.Get();

		const string blockPrefix = "@media (prefers-color-scheme: dark) {";

		char[] selectorPrefixes = ['.', '#', '[', ':', '*', '>', '+', '~'];
		string[] prefixes = [" ", appRunner.AppRunnerSettings.LineBreak, "\t", ",", "}", "{"];
		string[] suffixes = [" ", appRunner.AppRunnerSettings.LineBreak, "\t", ",", "{"];

		try
		{
			foreach (var block in sourceCss.ToString().FindMediaBlocks(blockPrefix))
			{
				workingSb.Clear();
				workingSb.Append(block.Trim());

				darkSb.Clear();
				darkSb.Append(block.Trim());
				darkSb.TrimStart(blockPrefix);
				darkSb.Trim();

				if (darkSb.Length > 0 && darkSb[^1] == '}')
					darkSb.Remove(darkSb.Length - 1, 1);

				var distinctSelectors = block.GetCssSelectors().Distinct().ToList();

				foreach (var selector in distinctSelectors)
				{
					if (selector.StartsWithAny(selectorPrefixes))
					{
						foreach (var suffix in suffixes)
						{
							var sel = $"{selector}{suffix}";

							if (workingSb.StartsWith(sel))
								workingSb.Remove(0, sel.Length).Insert(0, $".theme-auto ---{selector}---, .theme-auto---{selector}---{suffix}");

							if (darkSb.StartsWith(sel))
								darkSb.Remove(0, sel.Length).Insert(0, $".theme-dark ---{selector}---, .theme-dark---{selector}---{suffix}");
						}

						foreach (var prefix in prefixes)
						{
							foreach (var suffix in suffixes)
							{
								workingSb.Replace($"{prefix}{selector}{suffix}", $"{prefix}.theme-auto ---{selector}---, .theme-auto---{selector}---{suffix}");
								darkSb.Replace($"{prefix}{selector}{suffix}", $"{prefix}.theme-dark ---{selector}---, .theme-dark---{selector}---{suffix}");
							}
						}
					}
					else
					{
						foreach (var suffix in suffixes)
						{
							var sel = $"{selector}{suffix}";

							if (workingSb.StartsWith(sel))
								workingSb.Remove(0, sel.Length).Insert(0, $".theme-auto ---{selector}---{suffix}");

							if (darkSb.StartsWith(sel))
								darkSb.Remove(0, sel.Length).Insert(0, $".theme-dark ---{selector}---{suffix}");
						}

						foreach (var prefix in prefixes)
						{
							foreach (var suffix in suffixes)
							{
								workingSb.Replace($"{prefix}{selector}{suffix}", $"{prefix}.theme-auto ---{selector}---{suffix}");
								darkSb.Replace($"{prefix}{selector}{suffix}", $"{prefix}.theme-dark ---{selector}---{suffix}");
							}
						}
					}
				}

				foreach (var selector in distinctSelectors)
				{
					workingSb.Replace($"---{selector}---", selector);
					darkSb.Replace($"---{selector}---", selector);
				}

				workingSb
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(darkSb);

				sourceCss.Replace(block, workingSb.ToString());
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessDarkThemeClasses() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
			appRunner.AppState.StringBuilderPool.Return(darkSb);
		}

		return sourceCss;
	}
	
	/// <summary>
	/// Recursive method for traversing the variant tree and generating utility class CSS.
	/// </summary>
	/// <param name="branch"></param>
	/// <param name="workingSb"></param>
	private static void _GenerateUtilityClassesCss(VariantBranch branch, StringBuilder workingSb)
	{
		try
		{
			var isWrapped = string.IsNullOrEmpty(branch.WrapperCss) == false;
			
			if (isWrapped)
				workingSb.Append(branch.WrapperCss);
			
			foreach (var cssClass in branch.CssClasses.OrderBy(c => c.SelectorSort))
				workingSb
					.Append(cssClass.EscapedSelector)
					.Append(" {")
					.Append(cssClass.Styles)
					.Append('}');

			if (branch.Branches.Count > 0)
			{
				foreach (var subBranch in branch.Branches)
					_GenerateUtilityClassesCss(subBranch, workingSb);
			}

			if (string.IsNullOrEmpty(branch.WrapperCss))
				return;
			
			workingSb
				.Append('}');
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}_GenerateUtilityClassesCss() - {e.Message}");
			Environment.Exit(1);
		}
	}

	/// <summary>
	/// Traverse the variant branch tree recursively, adding CSS classes to the current branch.
	/// Essentially performs a recursive traversal but does not use recursion.
	/// </summary>
	/// <param name="rootBranch"></param>
	/// <param name="cssClass"></param>
	private static void _ProcessVariantBranchRecursive(VariantBranch rootBranch, CssClass cssClass)
	{
		try
		{
			var wrappers = cssClass.Wrappers.ToArray();
			var index = 0;
			var depth = 1;
			
			while (true)
			{
				if (index >= wrappers.Length)
				{
					rootBranch.CssClasses.Add(cssClass);
					return;
				}

				var (fingerprint, wrapperCss) = wrappers[index];

				var candidate = new VariantBranch { Fingerprint = fingerprint, WrapperCss = wrapperCss, Depth = depth };

				if (rootBranch.Branches.TryGetValue(candidate, out var existing) == false)
				{
					rootBranch.Branches.Add(candidate);
					existing = candidate;
				}

				rootBranch = existing;
				index += 1;
				depth += 1;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}_ProcessVariantBranchRecursive() - {e.Message}");
			Environment.Exit(1);
		}
	}
}