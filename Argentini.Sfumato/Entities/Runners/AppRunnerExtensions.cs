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
		try
		{
			if (appRunner.AppRunnerSettings.UseReset)
			{
				sourceCss
					.Append(File.ReadAllText(Path.Combine(appRunner.AppState.EmbeddedCssPath, "browser-reset.css")).NormalizeLinebreaks(appRunner.AppRunnerSettings.LineBreak).Trim())
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);
			}
		}		
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}AppendResetCss() - {e.Message}");
			Environment.Exit(1);
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
				sourceCss
					.Append(File.ReadAllText(Path.Combine(appRunner.AppState.EmbeddedCssPath, "forms.css")).NormalizeLinebreaks(appRunner.AppRunnerSettings.LineBreak).Trim())
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
					var depth = 0d;

					if (appRunner.AppRunnerSettings.UseMinify == false)
					{
						var spaceIncrement = 1.0d / (appRunner.AppRunnerSettings.Indentation.Length > 0
							? appRunner.AppRunnerSettings.Indentation.Length
							: 0.25d);

						if (span.Index > 0)
						{
							for (var i = span.Index - 1; i >= 0; i--)
							{
								if (sourceCss[i] == ' ')
									depth += spaceIncrement;
								else if (sourceCss[i] == '\t')
									depth += 1;
								else
									break;
							}
						}
					}

					foreach (var utilityClass in utilityClasses.OrderBy(c => c.SelectorSort))
					{
						if (appRunner.AppRunnerSettings.UseMinify == false)
						{
							var props = utilityClass.Styles.NormalizeLinebreaks()
								.Split('\n', StringSplitOptions.RemoveEmptyEntries);

							foreach (var prop in props)
							{
								workingSb
									.Append(appRunner.AppRunnerSettings.Indentation.Repeat((int)Math.Ceiling(depth)))
									.Append(prop.Trim())
									.Append(appRunner.AppRunnerSettings.LineBreak);
							}
						}
						else
						{
							workingSb.Append(utilityClass.Styles);
						}
					}
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
		try
		{
			foreach (var span in sourceCss.ToString().EnumerateAtVariantStatements())
			{
				if (span.Name.ToString().TryVariantIsMediaQuery(appRunner, out var variant))
				{
					sourceCss.Replace(span.Full, $"@{variant?.PrefixType} {variant?.Statement} {{");
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessAtVariantStatements() - {e.Message}");
			Environment.Exit(1);
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
		try
		{
			foreach (var match in sourceCss.EnumerateTokenWithOuterParenthetical("--alpha"))
			{
				if (match.Contains("var(--color-", StringComparison.Ordinal) && match.Contains('%'))
				{
					var colorKey = match[..match.IndexOf(')')].TrimStart("--alpha(")?.Trim().TrimStart("var(")?.Trim().TrimStart("--color-");

					if (string.IsNullOrEmpty(colorKey))
						continue;
						
					if (appRunner.Library.ColorsByName.TryGetValue(colorKey, out var colorValue) == false)
						continue;
						
					var alphaValue = match[(match.LastIndexOf('/') + 1)..].TrimEnd(')','%',' ').Trim();
							
					if (int.TryParse(alphaValue, out var pct))
						sourceCss.Replace(match, colorValue.SetWebColorAlpha(pct));
				}
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
						
				sourceCss.Replace(match, $"calc(var(--spacing) * {value})");
							
				appRunner.UsedCssCustomProperties.TryAdd("--spacing", string.Empty);
			}
			
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
						appRunner.UsedCssCustomProperties.TryAdd(span.Property.ToString(), valueValue);
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

		try
		{
			ProcessTrackedDependencyValues(appRunner);

			if (appRunner.UsedCssCustomProperties.Count > 0)
			{
				var wrappers = new [] { ":root, :host {", "*, :before, :after, ::backdrop {" };

				for (var i = 0; i < wrappers.Length; i++)
				{
					var items = appRunner.UsedCssCustomProperties.Where(c => c.Key.StartsWith("--sf-") == (i != 0) && string.IsNullOrEmpty(c.Value) == false).ToList();
					
					if (items.Count == 0)
						continue;
					
					workingSb
						.Append(wrappers[i])
						.Append(appRunner.AppRunnerSettings.LineBreak);

					foreach (var ccp in items.OrderBy(c => c.Key))
					{
						if (appRunner.AppRunnerSettings.UseMinify == false)
							workingSb.Append(appRunner.AppRunnerSettings.Indentation);

						workingSb
							.Append(ccp.Key)
							.Append(": ")
							.Append(ccp.Value)
							.Append(';');

						if (appRunner.AppRunnerSettings.UseMinify == false)
							workingSb.Append(appRunner.AppRunnerSettings.LineBreak);

						if (ccp.Key.StartsWith("--animate-", StringComparison.Ordinal) == false)
							continue;
						
						var key = $"@keyframes {ccp.Key.TrimStart("--animate-")}";

						if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(key, out var value))
							appRunner.UsedCss.TryAdd(key, value.Replace($"{appRunner.AppRunnerSettings.LineBreak}{appRunner.AppRunnerSettings.Indentation}", appRunner.AppRunnerSettings.LineBreak));
					}

					workingSb.Append('}');

					if (appRunner.AppRunnerSettings.UseMinify == false)
						workingSb
							.Append(appRunner.AppRunnerSettings.LineBreak)
							.Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}

			if (appRunner.UsedCss.Count > 0)
			{
				foreach (var ccp in appRunner.UsedCss.Where(c => string.IsNullOrEmpty(c.Value) == false))
				{
					workingSb
						.Append(ccp.Key)
						.Append(' ')
						.Append(ccp.Value);

					if (appRunner.AppRunnerSettings.UseMinify == false)
						workingSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (appRunner.AppRunnerSettings.UseMinify == false)
					workingSb.Append(appRunner.AppRunnerSettings.LineBreak);

				foreach (var ccp in appRunner.UsedCssCustomProperties)
				{
					if (ccp.Key.StartsWith('-') == false)
						continue;
					
					if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue($"@property {ccp.Key}", out var prop) == false)
						continue;
					
					workingSb
						.Append($"@property {ccp.Key} ")
						.Append(prop.Replace(appRunner.AppRunnerSettings.LineBreak + appRunner.AppRunnerSettings.Indentation, appRunner.AppRunnerSettings.LineBreak));
					
					if (appRunner.AppRunnerSettings.UseMinify == false)
						workingSb.Append(appRunner.AppRunnerSettings.LineBreak).Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}

			sourceCss.Insert(0, workingSb);
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}InjectRootDependenciesCss() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
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
			_GenerateUtilityClassesCss(appRunner, root, sb);

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
	
	/// <summary>
	/// Recursive method for traversing the variant tree and generating utility class CSS.
	/// </summary>
	/// <param name="appRunner"></param>
	/// <param name="branch"></param>
	/// <param name="workingSb"></param>
	/// <param name="depth"></param>
	private static void _GenerateUtilityClassesCss(AppRunner appRunner, VariantBranch branch, StringBuilder workingSb, int depth = 0)
	{
		try
		{
			var isWrapped = string.IsNullOrEmpty(branch.WrapperCss) == false;
			
			if (isWrapped)
			{
				if (appRunner.AppRunnerSettings.UseMinify == false)
					workingSb.Append(appRunner.AppRunnerSettings.Indentation.Repeat(depth - 1));
				
				workingSb.Append(branch.WrapperCss);
				
				if (appRunner.AppRunnerSettings.UseMinify == false)
					workingSb
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);
			}
			
			foreach (var cssClass in branch.CssClasses.OrderBy(c => c.SelectorSort))
			{
				if (appRunner.AppRunnerSettings.UseMinify == false)
					workingSb
						.Append(appRunner.AppRunnerSettings.Indentation.Repeat(depth))
						.Append(cssClass.EscapedSelector)
						.Append(" {")
						.Append(appRunner.AppRunnerSettings.LineBreak);
				else			
					workingSb
						.Append(cssClass.EscapedSelector)
						.Append(" {");

				if (appRunner.AppRunnerSettings.UseMinify == false)
				{
					var props = cssClass.Styles.NormalizeLinebreaks().Split('\n', StringSplitOptions.RemoveEmptyEntries);

					foreach (var prop in props)
					{
						workingSb
							.Append(appRunner.AppRunnerSettings.Indentation.Repeat(depth + 1))
							.Append(prop.Trim())
							.Append(appRunner.AppRunnerSettings.LineBreak);
					}
				}
				else
					workingSb
						.Append(cssClass.Styles);

				if (appRunner.AppRunnerSettings.UseMinify == false)
					workingSb
						.Append(appRunner.AppRunnerSettings.Indentation.Repeat(depth))
						.Append('}')
						.Append(appRunner.AppRunnerSettings.LineBreak)
						.Append(appRunner.AppRunnerSettings.LineBreak);
				else
					workingSb
						.Append('}');
			}

			if (branch.Branches.Count > 0)
			{
				foreach (var subBranch in branch.Branches)
				{
					_GenerateUtilityClassesCss(appRunner, subBranch, workingSb, depth + 1);
				}
			}

			if (string.IsNullOrEmpty(branch.WrapperCss))
				return;
			
			if (appRunner.AppRunnerSettings.UseMinify == false)
				workingSb
					.Append(appRunner.AppRunnerSettings.Indentation.Repeat(depth - 1))
					.Append('}')
					.Append(appRunner.AppRunnerSettings.LineBreak)
					.Append(appRunner.AppRunnerSettings.LineBreak);
			else
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