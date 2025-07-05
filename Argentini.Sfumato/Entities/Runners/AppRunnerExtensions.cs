// ReSharper disable RedundantAssignment
// ReSharper disable MemberCanBePrivate.Global

using System.Reflection;

namespace Argentini.Sfumato.Entities.Runners;

public static class AppRunnerExtensions
{
	#region V2: Extract CSS Imports / Components Layer

	/// <summary>
	/// Iterate @import statements and add content to AppRunner.ImportsCssSegment;
	/// puts any @layer components content into AppRunner.ComponentsCssSegment;
	/// does not process the content beyond adding specified layers.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	/// <param name="isRoot"></param>
	/// <param name="parentLayerName"></param>
	/// <param name="parentPath"></param>
	/// <returns>index and length of the import statements</returns>
	public static (int,int) ProcessCssImportStatements(this StringBuilder? css, AppRunner? appRunner, bool isRoot, string parentLayerName = "", string parentPath = "")
	{
		if (css is null || appRunner is null)
			return (-1, -1);

		if (isRoot) // Clear segment, untouch for orphan deletion
		{
			appRunner.ImportsCssSegment.Clear();
			appRunner.ComponentsCssSegment.Clear();
			
			var e = appRunner.AppRunnerSettings.CssImports.GetEnumerator();

			while (e.MoveNext())
			{
				e.Current.Value.Touched = false;
			}
		}

		var importPathSb = appRunner.AppState.StringBuilderPool.Get();
		var layerName = string.Empty;
		var filePath = string.Empty;

	    try
	    {
		    var index = css.IndexOf("@import ");
		    var importsStartIndex = index;
		    var cssStartIndex = 0;

		    while (index > -1)
		    {
			    var eolIndex = css.IndexOf(';', index);

			    cssStartIndex = eolIndex + 1;
			    importPathSb.ReplaceContent(css, index + 8, eolIndex - index - 8);

			    if (importPathSb.EndsWith('\'') == false && importPathSb.EndsWith('\"') == false)
			    {
				    var layerNameIndex = importPathSb.LastIndexOf(' ') + 1;
				    
				    layerName = importPathSb.Substring(layerNameIndex, importPathSb.Length - layerNameIndex);
				    
				    importPathSb.Remove(layerNameIndex - 1, importPathSb.Length - layerNameIndex + 1);
				    importPathSb.Trim(' ', '\'', '\"');
			    }
			    else
			    {
				    layerName = string.Empty;
				    importPathSb.Trim(' ', '\'', '\"');
			    }
		    
			    filePath = Path.GetFullPath(Path.Combine(appRunner.AppRunnerSettings.NativeCssFilePathOnly, parentPath, importPathSb.ToString()));

			    if (File.Exists(filePath) == false)
			    {
				    Console.WriteLine($"{AppState.CliErrorPrefix}File does not exist: {filePath}");

				    appRunner.ImportsCssSegment.Append($"/* Could not import: {filePath} */{appRunner.AppRunnerSettings.LineBreak}");
			    }
			    else
			    {
				    var fileInfo = new FileInfo(filePath);
				    CssImportFile? cssImportFile;

				    if (appRunner.AppRunnerSettings.CssImports.TryGetValue(filePath, out var current))
				    {
					    current.Touched = true;

					    if (fileInfo.Length != current.FileInfo.Length || fileInfo.CreationTimeUtc != current.FileInfo.CreationTimeUtc || fileInfo.LastWriteTimeUtc != current.FileInfo.LastWriteTimeUtc)
					    {
						    current.Pool = appRunner.AppState.StringBuilderPool;
						    current.FileInfo = fileInfo;
						    current.CssContent = appRunner.AppState.StringBuilderPool.Get().Append(File.ReadAllText(filePath));
						}

					    cssImportFile = current;
				    }
				    else
				    {
					    cssImportFile = new CssImportFile
					    {
						    Touched = true,
						    Pool = appRunner.AppState.StringBuilderPool,
						    FileInfo = fileInfo,
						    CssContent = appRunner.AppState.StringBuilderPool.Get().Append(File.ReadAllText(filePath))
					    };

					    appRunner.AppRunnerSettings.CssImports.Add(filePath, cssImportFile);
				    }

				    var layered = string.IsNullOrEmpty(layerName) == false && layerName != "components";
				    
				    if (layered)
					    appRunner.ImportsCssSegment.Append($"@layer {layerName} {{{appRunner.AppRunnerSettings.LineBreak}");

				    _ = ProcessCssImportStatements(cssImportFile.CssContent, appRunner, false, layerName, Path.GetDirectoryName(filePath) ?? string.Empty);
				    
				    if (layered)
					    appRunner.ImportsCssSegment.Append($"}}{appRunner.AppRunnerSettings.LineBreak}");
			    }

			    index = css.IndexOf("@import ", index + 1);
		    }

		    if (isRoot)
		    {
			    appRunner.AppRunnerSettings.CssImports.RemoveWhere((_, v) => v.Touched == false);

			    return importsStartIndex == -1 ? (-1, -1) : cssStartIndex < 0 || cssStartIndex < importsStartIndex ? (-1, -1) : (importsStartIndex, cssStartIndex - importsStartIndex);
		    }

		    if (cssStartIndex < 0 || cssStartIndex >= css.Length - 1)
			    return (-1, -1);

		    if (string.IsNullOrEmpty(parentLayerName) == false && parentLayerName == "components")
		    {
			    appRunner.ComponentsCssSegment.Append(css, cssStartIndex, css.Length - cssStartIndex).Trim();
			    appRunner.ComponentsCssSegment.Append(appRunner.AppRunnerSettings.LineBreak);
		    }
		    else
		    {
			    appRunner.ImportsCssSegment.Append(css, cssStartIndex, css.Length - cssStartIndex).Trim();
			    appRunner.ImportsCssSegment.Append(appRunner.AppRunnerSettings.LineBreak);
		    }

		    return (-1, -1);
	    }
	    finally
	    {
		    appRunner.AppState.StringBuilderPool.Return(importPathSb);
	    }
	}

	#endregion
	
	#region V2: Extract Sfumato Block
	
	/// <summary>
	/// Extract the Sfumato layer block;
	/// </summary>
	/// <returns>the index and length of the sfumato block</returns>
	public static (int, int) ExtractSfumatoBlock(this StringBuilder? css, AppRunner? appRunner, int startIndex = 0)
	{
		if (css is null || appRunner is null)
			return (-1, -1);

		var (index, length) = css.ExtractCssBlock("@layer sfumato", startIndex);

		if (index == -1)
		{
			(index, length) = css.ExtractCssBlock("@theme sfumato", startIndex);

			if (index == -1)
			{
				Console.WriteLine($"{AppState.CliErrorPrefix}No @layer sfumato {{ :root {{ }} }} block found.");
				Environment.Exit(1);
			}
		}

		var blockStartIndex = css.IndexOf('{', index) + 1;
		
		appRunner.SfumatoSegment.ReplaceContent(css, blockStartIndex, length - (blockStartIndex - index) - 1);

		return (index, length);
	}
	
	#endregion

	#region V2: Import Sfumato Block Items
	
    /// <summary>
    /// Parse Sfumato settings block into dictionary items. 
    /// </summary>
    public static void ImportSfumatoBlockSettingsItems(AppRunner appRunner)
    {
	    try
	    {
	        foreach (var pos in appRunner.SfumatoSegment.EnumerateCssCustomPropertyPositions())
		        if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryAdd(appRunner.SfumatoSegment.Substring(pos.PropertyStart, pos.PropertyLength), appRunner.SfumatoSegment.Substring(pos.ValueStart, pos.ValueLength)) == false)
		        {
			        appRunner.AppRunnerSettings.SfumatoBlockItems[appRunner.SfumatoSegment.Substring(pos.PropertyStart, pos.PropertyLength)] = appRunner.SfumatoSegment.Substring(pos.ValueStart, pos.ValueLength);
		        }

	        foreach (var pos in appRunner.SfumatoSegment.EnumerateCssClassAndAtBlockPositions())
		        if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryAdd(appRunner.SfumatoSegment.Substring(pos.HeaderStart, pos.HeaderLength), appRunner.SfumatoSegment.Substring(pos.BodyStart, pos.BodyLength).TrimEnd(';').Trim()) == false)
		        {
			        appRunner.AppRunnerSettings.SfumatoBlockItems[appRunner.SfumatoSegment.Substring(pos.HeaderStart, pos.HeaderLength)] = appRunner.SfumatoSegment.Substring(pos.BodyStart, pos.BodyLength).TrimEnd(';').Trim();
		        }

	        foreach (var pos in appRunner.SfumatoSegment.EnumerateCustomVariantPositions())
		        if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryAdd(appRunner.SfumatoSegment.Substring(pos.NameStart, pos.NameLength), appRunner.SfumatoSegment.Substring(pos.ContentStart, pos.ContentLength).TrimEnd(';')) == false)
		        {
			        appRunner.AppRunnerSettings.SfumatoBlockItems[appRunner.SfumatoSegment.Substring(pos.NameStart, pos.NameLength)] = appRunner.SfumatoSegment.Substring(pos.ContentStart, pos.ContentLength).TrimEnd(';');
		        }

	        if (appRunner.AppRunnerSettings.SfumatoBlockItems.Count > 0)
	            return;
	        
	        Console.WriteLine($"{AppState.CliErrorPrefix}No Sfumato options specified in CSS file.");
	        Environment.Exit(1);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
	
	#endregion
	
	#region V2: Generate Components Layer / Remaining CSS

	public static void ProcessComponentsLayerAndCss(AppRunner appRunner)
	{
		var (index, length) = appRunner.CustomCssSegment.ExtractCssBlock("@layer components");
		
		while (index > -1)
		{
			var openBraceIndex = appRunner.CustomCssSegment.IndexOf('{', index + 1);

			if (openBraceIndex > -1)
				appRunner.ComponentsCssSegment.Append(appRunner.CustomCssSegment, openBraceIndex + 1, length - (openBraceIndex - index) - 2);
			
			appRunner.CustomCssSegment.Remove(index, length);
			
			(index, length) = appRunner.CustomCssSegment.ExtractCssBlock("@layer components");
		}
	}

	#endregion

	#region V2: Generate Utility Classes
	
	/// <summary>
	/// Generate utility class CSS from the AppRunner.UtilityClasses dictionary. 
	/// </summary>
	/// <param name="appRunner"></param>
	public static void ProcessUtilityClasses(AppRunner appRunner)
	{
		appRunner.UtilitiesCssSegment.Clear();
		
		var root = new VariantBranch
		{
			Fingerprint = 0,
			Depth = 0,
			WrapperCss = string.Empty
		};

		foreach (var cssClass in appRunner.UtilityClasses.Values.OrderBy(c => c.WrapperSort).ThenBy(c => c.SelectorSort))
			ProcessVariantBranchRecursive(root, cssClass);
		
		GenerateUtilityClassesCss(root, appRunner.UtilitiesCssSegment);
	}

	/// <summary>
	/// Traverse the variant branch tree recursively, adding CSS classes to the current branch.
	/// Essentially performs a recursive traversal but does not use recursion.
	/// </summary>
	/// <param name="rootBranch"></param>
	/// <param name="cssClass"></param>
	private static void ProcessVariantBranchRecursive(VariantBranch rootBranch, CssClass cssClass)
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

	/// <summary>
	/// Recursive method for traversing the variant tree and generating utility class CSS at the branch level.
	/// </summary>
	/// <param name="branch"></param>
	/// <param name="workingSb"></param>
	private static void GenerateUtilityClassesCss(VariantBranch branch, StringBuilder workingSb)
	{
		var isWrapped = string.IsNullOrEmpty(branch.WrapperCss) == false;

		if (isWrapped)
			workingSb.Append(branch.WrapperCss);

		foreach (var cssClass in branch.CssClasses.OrderBy(c => c.SelectorSort).ThenBy(c => c.ClassDefinition?.NameSortOrder ?? 0).ThenBy(c => c.Selector))
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
	
	#endregion
	
	#region V2: Process @apply Statements
	
	/// <summary>
	/// Convert @apply statements in source CSS to utility class property statements.
	/// Also tracks any used CSS custom properties or CSS snippets.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	public static void ProcessAtApplyStatements(this StringBuilder css, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			foreach (var span in css.ToString().EnumerateAtApplyStatements())
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

				workingSb.Trim();
				css.Replace(span.Full, workingSb.ToString());
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessAtApplyStatements() => {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}
	}
	
	#endregion
	
	#region V2: Process Functions
	
    /// <summary>
    /// Convert functions in source CSS to CSS statements (e.g. --alpha()).
    /// Also tracks any used CSS custom properties or CSS snippets.
    /// </summary>
    /// <param name="css"></param>
    /// <param name="appRunner"></param>
	public static void ProcessFunctions(this StringBuilder css, AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			workingSb.Append(css);

			foreach (var match in workingSb.EnumerateTokenWithOuterParenthetical("--alpha"))
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
					css.Replace(match, colorValue.SetWebColorAlpha(pct));
			}
			
			foreach (var match in workingSb.EnumerateTokenWithOuterParenthetical("--spacing"))
			{
				if (match.Length <= 11)
					continue;

				var valueString = match.TrimStart("--spacing(")?.Trim().TrimEnd(')', ' ');

				if (string.IsNullOrEmpty(valueString))
					continue;

				if (double.TryParse(valueString, out var value) == false)
					continue;

				css.Replace(match, $"calc(var(--spacing) * {value})");

				appRunner.UsedCssCustomProperties.TryAdd("--spacing", string.Empty);
			}

			foreach (var pos in workingSb.EnumerateCssCustomPropertyPositions(namesOnly: true))
			{
				if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(workingSb.Substring(pos.PropertyStart, pos.PropertyLength), out var value))
					appRunner.UsedCssCustomProperties.TryAdd(workingSb.Substring(pos.PropertyStart, pos.PropertyLength), value);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessFunctions() => {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}
	}
	
	#endregion
	
	#region V2: Process @variant Statements
	
	/// <summary>
	/// Convert @variant statements in source CSS to media query statements.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	public static void ProcessAtVariantStatements(this StringBuilder css, AppRunner appRunner)
	{
		foreach (var span in css.ToString().EnumerateAtVariantStatements())
		{
			if (span.Name.ToString().TryVariantIsMediaQuery(appRunner, out var variant))
				css.Replace(span.Full, $"@{variant?.PrefixType} {variant?.Statement} {{");
		}
	}
	
	#endregion
	
	#region V2: Generate @properties Layer / @property Rules
	
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
			Console.WriteLine($"{AppState.CliErrorPrefix}ProcessTrackedDependencyValues() => {e.Message}");
			Environment.Exit(1);
		}
	}
	
	/// <summary>
	/// Generate @properties Layer / @property Rules
	/// </summary>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static void GeneratePropertiesAndThemeLayers(AppRunner appRunner)
	{
		ProcessTrackedDependencyValues(appRunner);

		if (appRunner.UsedCssCustomProperties.Count > 0)
		{
			#region @layer properties
			
			appRunner.PropertiesCssSegment
				.Append("*, ::before, ::after, ::backdrop {")
				.Append(appRunner.AppRunnerSettings.LineBreak);

			foreach (var ccp in appRunner.UsedCssCustomProperties.Where(c => (c.Key.StartsWith("--sf-") || c.Key.StartsWith("--form-")) && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
			{
				if (appRunner.AppRunnerSettings.UseForms == false && ccp.Key.StartsWith("--form-"))
					continue;
				
				appRunner.PropertiesCssSegment
					.Append(ccp.Key)
					.Append(": ")
					.Append(ccp.Value)
					.Append(';');
			}

			appRunner.PropertiesCssSegment
				.Append('}')
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
					
					appRunner.PropertyListCssSegment
						.Append($"@property {ccp.Key} ")
						.Append(prop)
						.Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}				

			#endregion
			
			#region @layer theme
			
			appRunner.ThemeCssSegment
				.Append(":root, :host {")
				.Append(appRunner.AppRunnerSettings.LineBreak)
				.Append(appRunner.AppRunnerSettings.LineBreak);
		
			foreach (var ccp in appRunner.UsedCssCustomProperties.Where(c => c.Key.StartsWith("--sf-") == false && c.Key.StartsWith("--form-") == false && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
			{
				appRunner.ThemeCssSegment
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

			appRunner.ThemeCssSegment
				.Append('}')
				.Append(appRunner.AppRunnerSettings.LineBreak)
				.Append(appRunner.AppRunnerSettings.LineBreak);	
			
			#endregion
			
			#region @keyframes, etc.
			
			if (appRunner.UsedCss.Count > 0)
			{
				foreach (var ccp in appRunner.UsedCss.Where(c => string.IsNullOrEmpty(c.Value) == false))
				{
					appRunner.PropertyListCssSegment
						.Append(ccp.Key)
						.Append(' ')
						.Append(ccp.Value);
				}
			}
			
			#endregion
		}
	}
	
	#endregion
	
	#region V2: Process Dark Theme Classes
	
	/// <summary>
	/// Find all dark theme media blocks and duplicate as wrapped classes theme-dark
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static void ProcessDarkTheme(this StringBuilder sourceCss, AppRunner appRunner)
	{
		const string mediaPrefix = "@media (prefers-color-scheme: dark) {";

		var outCss = new StringBuilder(sourceCss.Length * 2);
		var css = sourceCss.ToString();
		var pos = 0;

		while (true)
		{
			var mediaIdx = css.IndexOf(mediaPrefix, pos, StringComparison.Ordinal);

			if (mediaIdx < 0)
			{
				// no more blocks – copy the tail and finish
				outCss.Append(css, pos, css.Length - pos);
				break;
			}

			// 1) copy everything before this @media as-is
			outCss.Append(css, pos, mediaIdx - pos);

			// 2) locate the matching '}' that closes the @media
			var bodyStart = mediaIdx + mediaPrefix.Length;
			var p = bodyStart;
			var depth = 1; // we are just after the '{'

			while (p < css.Length && depth > 0)
			{
				var c = css[p++];

				if (c == '{')
					depth++;
				else if (c == '}')
					depth--;
			}

			var bodyEnd = p - 1; // index of the '}' that closes the @media
			var inner = css.AsSpan(bodyStart, bodyEnd - bodyStart);

			// 3) rewrite the whole inner block in ONE pass
			var autoSb = appRunner.AppState.StringBuilderPool.Get();
			var darkSb = appRunner.AppState.StringBuilderPool.Get();

			try
			{
				autoSb.Clear();
				darkSb.Clear();

				RewriteContent(inner, autoSb, darkSb);

				// @media with ".theme-auto…" selectors
				outCss.Append(mediaPrefix).Append(autoSb).Append('}');

				// blank line between the two copies (match original behaviour)
				outCss.Append(appRunner.AppRunnerSettings.LineBreak)
					  .Append(appRunner.AppRunnerSettings.LineBreak);

				// ".theme-dark…" selectors outside any media query
				outCss.Append(darkSb);
			}
			finally
			{
				appRunner.AppState.StringBuilderPool.Return(autoSb);
				appRunner.AppState.StringBuilderPool.Return(darkSb);
			}

			pos = bodyEnd + 1; // skip the original @media block
		}

		sourceCss.ReplaceContent(outCss);

		return;

		// recursively rewrites one block body
		static void RewriteContent(ReadOnlySpan<char> src, StringBuilder autoSb, StringBuilder darkSb)
		{
			var i = 0;

			while (i < src.Length)
			{
				// copy leading whitespace verbatim
				var headerStart = i;

				while (headerStart < src.Length && char.IsWhiteSpace(src[headerStart]))
				{
					autoSb.Append(src[headerStart]);
					darkSb.Append(src[headerStart]);
					headerStart++;
				}

				if (headerStart >= src.Length)
					return;

				i = headerStart;

				// find the next '{' that starts the rule
				var braceRel = src[i..].IndexOf('{');

				if (braceRel < 0) // malformed CSS – bail out
				{
					autoSb.Append(src[i..]);
					darkSb.Append(src[i..]);

					return;
				}

				var bracePos = i + braceRel;
				var headerSpan = src.Slice(i, braceRel);

				// find the matching '}' for this rule
				var contentStart = bracePos + 1;
				var depth = 1;
				var p = contentStart;

				while (p < src.Length && depth > 0)
				{
					var c = src[p++];

					if (c == '{')
						depth++;
					else if (c == '}')
						depth--;
				}

				var contentEnd = p - 1; // position of '}'
				var ruleContent = src[contentStart..contentEnd];
				var isAtRule = headerSpan.TrimStart().Length > 0 && headerSpan.TrimStart()[0] == '@';

				if (isAtRule)
				{
					// copy @-rules unchanged, but rewrite inside them
					autoSb.Append(headerSpan).Append('{');
					darkSb.Append(headerSpan).Append('{');

					RewriteContent(ruleContent, autoSb, darkSb);

					autoSb.Append('}');
					darkSb.Append('}');
				}
				else
				{
					// selector list: add the two theme prefixes
					var selectors = new List<string>();
					AddSelectors(headerSpan, selectors);

					var first = true;

					foreach (var sel in selectors)
					{
						if (first == false)
						{
							autoSb.Append(", ");
							darkSb.Append(", ");
						}

						first = false;

						var needsTwo = NeedsDoubleForm(sel);

						autoSb.Append(".theme-auto ").Append(sel);

						if (needsTwo)
							autoSb.Append(", .theme-auto").Append(sel);

						darkSb.Append(".theme-dark ").Append(sel);

						if (needsTwo)
							darkSb.Append(", .theme-dark").Append(sel);
					}

					autoSb.Append(" {");
					darkSb.Append(" {");

					// copy rule body verbatim
					autoSb.Append(ruleContent);
					darkSb.Append(ruleContent);

					autoSb.Append('}');
					darkSb.Append('}');
				}

				i = contentEnd + 1; // continue after this rule
			}
		}

		// does the selector need the “no space” twin?
		static bool NeedsDoubleForm(string sel)
		{
			if (string.IsNullOrEmpty(sel))
				return false;

			return sel[0] switch
			{
				'.' or '#' or '[' or ':' or '*' or '>' or '+' or '~' => true,
				_ => false,
			};
		}

		// split a selector list on top-level (unescaped) commas
		static void AddSelectors(ReadOnlySpan<char> header, List<string> output)
		{
			var paren = 0;
			var square = 0;
			var start = 0;

			var quoteChar = '\0';

			var inQuote = false;
			var escaped = false;

			for (var i = 0; i < header.Length; i++)
			{
				var c = header[i];

				if (escaped)
				{
					escaped = false;
					continue;
				}

				if (c == '\\')
				{
					escaped = true;
					continue;
				}

				if (inQuote)
				{
					if (c == quoteChar)
						inQuote = false;

					continue;
				}

				if (c is '"' or '\'')
				{
					inQuote = true;
					quoteChar = c;

					continue;
				}

				switch (c)
				{
					case '(': paren++; break;
					case ')': if (paren > 0) paren--; break;
					case '[': square++; break;
					case ']': if (square > 0) square--; break;
					case ',':
						if (paren == 0 && square == 0)
						{
							var sel = header[start..i].Trim();

							if (sel.IsEmpty == false)
								output.Add(sel.ToString());

							start = i + 1;
						}

						break;
				}
			}

			var tail = header[start..].Trim();

			if (tail.IsEmpty == false)
				output.Add(tail.ToString());
		}
	}
	
	#endregion
	
	#region V2: Load Source CSS File, Sfumato Settings
	
	public static async Task LoadCssFileAsync(this AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			if (string.IsNullOrEmpty(appRunner.AppRunnerSettings.CssFilePath) == false)
				appRunner.AppRunnerSettings.CssContent = (await File.ReadAllTextAsync(Path.GetFullPath(appRunner.AppRunnerSettings.CssFilePath.SetNativePathSeparators())))
					.TrimWhitespaceBeforeLineBreaks(workingSb)
					.RemoveBlockComments(workingSb)
					.NormalizeLinebreaks(appRunner.AppRunnerSettings.LineBreak)
					.Trim();

			appRunner.AppRunnerSettings.CssContent.LoadSfumatoSettings(appRunner);
		}
		catch (Exception e)
		{
			appRunner.Messages.Add($"{AppState.CliErrorPrefix}LoadCssFileAsync() => {e.Message}");
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}
	}
	
	public static void LoadSfumatoSettings(this string css, AppRunner appRunner)
	{
		appRunner.CustomCssSegment.ReplaceContent(css);

		var (index, length) = appRunner.CustomCssSegment.ExtractSfumatoBlock(appRunner);

		if (index > -1)
			appRunner.CustomCssSegment.Remove(index, length);

		ImportSfumatoBlockSettingsItems(appRunner);
		ProcessSfumatoBlockSettings(appRunner);
	}
	
	#endregion

	#region V2: Process Default Sfumato Settings
	
    /// <summary>
    /// Processes CSS settings for colors, breakpoints, etc., and uses reflection to load all others per utility class file.  
    /// </summary>
    public static void ProcessSfumatoBlockSettings(AppRunner appRunner, bool usingDefaults = false)
    {
	    #region Read color definitions
	    
	    foreach (var match in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--color-")))
	    {
		    var key = match.Key.TrimStart("--color-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (appRunner.Library.ColorsByName.TryAdd(key, match.Value) == false)
			    appRunner.Library.ColorsByName[key] = match.Value;
	    }
	    
	    #endregion

	    #region Read breakpoints

	    var prefixOrder = 100;

	    foreach (var match in appRunner.AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (match.Key.StartsWith("--breakpoint-") == false)
			    continue;
		    
		    var key = match.Key.TrimStart("--breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (appRunner.Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(width >= {match.Value})"
		        }) == false)
		    {
			    appRunner.Library.MediaQueryPrefixes[key] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(width >= {match.Value})"
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (appRunner.Library.MediaQueryPrefixes.TryAdd($"max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(width < {match.Value})"
		        }) == false)
		    {
			    appRunner.Library.MediaQueryPrefixes[$"max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(width < {match.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }
	    
	    foreach (var breakpoint in appRunner.AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (breakpoint.Key.StartsWith("--adaptive-breakpoint-") == false)
			    continue;

		    var key = breakpoint.Key.TrimStart("--adaptive-breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (double.TryParse(breakpoint.Value, out var maxValue) == false)
			    continue;

		    if (appRunner.Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(min-aspect-ratio: {breakpoint.Value})"
		        }) == false)
		    {
			    appRunner.Library.MediaQueryPrefixes[key] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(min-aspect-ratio: {breakpoint.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (appRunner.Library.MediaQueryPrefixes.TryAdd($"max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(max-aspect-ratio: {maxValue - 0.0001})"
		        }) == false)
		    {
			    appRunner.Library.MediaQueryPrefixes[$"max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(max-aspect-ratio: {maxValue - 0.0001})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }

	    foreach (var breakpoint in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
	    {
		    var key = breakpoint.Key.TrimStart("--container-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (appRunner.Library.ContainerQueryPrefixes.TryAdd($"@{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width >= {breakpoint.Value})"
		        }) == false)
		    {
			    appRunner.Library.ContainerQueryPrefixes[$"@{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width >= {breakpoint.Value})"
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (appRunner.Library.ContainerQueryPrefixes.TryAdd($"@max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width < {breakpoint.Value})"
		        }) == false)
		    {
			    appRunner.Library.ContainerQueryPrefixes[$"@max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width < {breakpoint.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }

	    #endregion
	    
	    #region Read @custom-variant shorthand items

	    foreach (var match in appRunner.AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (match.Key.StartsWith("@custom-variant") == false)
			    continue;

		    if (match.Value.StartsWith("&:", StringComparison.Ordinal) == false && match.Value.StartsWith('@') == false)
			    continue;

		    var keySegments = match.Key.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			    
		    if (keySegments.Length < 2)
			    continue;

		    var key = keySegments[1];

		    if (match.Value.StartsWith("&:"))
		    {
			    if (appRunner.Library.PseudoclassPrefixes.TryAdd(key, new VariantMetadata
			        {
				        PrefixType = "pseudoclass",
				        SelectorSuffix = $"{match.Value.TrimStart('&')}"
			        }) == false)
			    {
				    appRunner.Library.PseudoclassPrefixes[key] = new VariantMetadata
				    {
					    PrefixType = "pseudoclass",
					    SelectorSuffix = $"{match.Value.TrimStart('&')}"
				    };
			    }

			    appRunner.Library.MediaQueryPrefixes.Remove(key);
		    }
		    else
		    {
			    if (string.IsNullOrEmpty(key))
				    continue;

			    var wrapperSegments = match.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			    
			    if (wrapperSegments.Length < 2)
				    continue;

			    var prefixType = wrapperSegments[0].TrimStart('@').TrimStart('&');

			    if (string.IsNullOrEmpty(prefixType))
				    continue;

			    var statement = $"{match.Value.TrimStart($"@{prefixType}")?.Trim()}";
			    
			    if (prefixType.Equals("media", StringComparison.OrdinalIgnoreCase))
			    {
				    if (appRunner.Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
				        {
					        PrefixOrder = prefixOrder,
					        PrefixType = prefixType,
					        Statement = statement
				        }) == false)
				    {
					    appRunner.Library.MediaQueryPrefixes[key] = new VariantMetadata
					    {
						    PrefixOrder = prefixOrder,
						    PrefixType = prefixType,
						    Statement = statement
					    };
				    }

				    if (prefixOrder < int.MaxValue - 100)
					    prefixOrder += 100;
				    
				    appRunner.Library.PseudoclassPrefixes.Remove(key);
			    }
			    else if (prefixType.Equals("supports", StringComparison.OrdinalIgnoreCase))
			    {
				    if (appRunner.Library.SupportsQueryPrefixes.TryAdd(key, new VariantMetadata
				        {
					        PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
					        PrefixType = prefixType,
					        Statement = statement
				        }) == false)
				    {
					    appRunner.Library.SupportsQueryPrefixes[key] = new VariantMetadata
					    {
						    PrefixOrder = appRunner.Library.SupportsQueryPrefixes.Count + 1,
						    PrefixType = prefixType,
						    Statement = statement
					    };
				    }
				    
				    appRunner.Library.PseudoclassPrefixes.Remove(key);
			    }
		    }
	    }
	    
	    #endregion
	    
		#region Read @utility items

		foreach (var match in appRunner.AppRunnerSettings.SfumatoBlockItems)
		{
			if (match.Key.StartsWith("@utility") == false)
				continue;

			var segments = match.Key.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			if (segments.Length != 2)
				continue;

			if (appRunner.Library.SimpleClasses.TryAdd(segments[1], new ClassDefinition
			{
				InSimpleUtilityCollection = true,
				Template = match.Value.Trim().TrimStart('{').TrimEnd('}').Trim()
			}))
			{
				appRunner.Library.ScannerClassNamePrefixes.Insert(segments[1]);
			}
		}

	    #endregion
	    
		#region Read theme settings from ClassDictionary instances (e.g. --text-xs, etc.)
	    
	    var derivedTypes = Assembly.GetExecutingAssembly()
		    .GetTypes()
		    .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

		foreach (var type in derivedTypes)
		{
			if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
				continue;

			instance.ProcessThemeSettings(appRunner);
	    }
	    
	    #endregion

	    if (usingDefaults)
		    return;
	    
	    #region Read project settings

	    var workingSb = appRunner.AppState.StringBuilderPool.Get();

	    try
	    {
		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-reset", out var useReset))
			    appRunner.AppRunnerSettings.UseReset = useReset.Equals("true", StringComparison.Ordinal);

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-forms", out var useForms))
			    appRunner.AppRunnerSettings.UseForms = useForms.Equals("true", StringComparison.Ordinal);

		    if (appRunner.AppRunnerSettings.UseMinify == false && appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-minify", out var useMinify))
			    appRunner.AppRunnerSettings.UseMinify = useMinify.Equals("true", StringComparison.Ordinal);

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-dark-theme-classes", out var useDarkThemeClasses))
			    appRunner.AppRunnerSettings.UseDarkThemeClasses = useDarkThemeClasses.Equals("true", StringComparison.Ordinal);

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-compatibility-mode", out var useCompatibilityMode))
			    appRunner.AppRunnerSettings.UseCompatibilityMode = useCompatibilityMode.Equals("true", StringComparison.Ordinal);

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--output-path", out var outputPath))
			    appRunner.AppRunnerSettings.CssOutputFilePath = (string.IsNullOrEmpty(outputPath) ? "sfumato.css" : outputPath).Trim('\"');

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--paths", out var pathsValue))
		    {
			    var paths = pathsValue.ConsolidateSpaces(workingSb).TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (paths.Length != 0)
			    {
				    foreach (var p in paths)
				    {
					    appRunner.AppRunnerSettings.Paths.Add(p.Trim('\"'));
					    appRunner.AppRunnerSettings.AbsolutePaths.Add(Path.GetFullPath(Path.Combine(appRunner.AppRunnerSettings.NativeCssFilePathOnly, p.Trim('\"'))));
				    }
			    }
		    }

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--not-paths", out var notPathsValue))
		    {
			    var notPaths = notPathsValue.ConsolidateSpaces(workingSb).TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (notPaths.Length != 0)
			    {
				    foreach (var p in notPaths)
				    {
					    appRunner.AppRunnerSettings.NotPaths.Add(p.Trim('\"'));
					    appRunner.AppRunnerSettings.AbsoluteNotPaths.Add(Path.GetFullPath(Path.Combine(appRunner.AppRunnerSettings.NativeCssFilePathOnly, p.Trim('\"'))).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar);
				    }
			    }
		    }

		    if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue("--not-folder-names", out var notFolderNamesValue))
		    {
			    var notFolderNames = notFolderNamesValue.ConsolidateSpaces(workingSb).TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (notFolderNames.Length != 0)
			    {
				    foreach (var p in notFolderNames)
					    appRunner.AppRunnerSettings.NotFolderNames.Add(p.Trim('\"'));
			    }
		    }

		    if (string.IsNullOrEmpty(appRunner.AppRunnerSettings.CssOutputFilePath))
		    {
			    Console.WriteLine($"{AppState.CliErrorPrefix}Must specify --output-path in file: {appRunner.AppRunnerSettings.CssFilePath}");
			    Environment.Exit(1);
		    }

		    if (appRunner.AppRunnerSettings.Paths.Count > 0)
			    return;
		    
		    Console.WriteLine($"{AppState.CliErrorPrefix}Must specify --paths: [] in file: {appRunner.AppRunnerSettings.CssFilePath}");
		    Environment.Exit(1);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
	    finally
	    {
		    appRunner.AppState.StringBuilderPool.Return(workingSb);
		}
	    
	    #endregion
    }
	
	#endregion
	
	#region V2: Build CSS
	
	public static string BuildCss(this string css, AppRunner appRunner)
	{
		var index = 0;
		var length = 0;
		
		appRunner.CustomCssSegment.ReplaceContent(css);
		
		(index, length) = appRunner.CustomCssSegment.ExtractSfumatoBlock(appRunner);

		if (index > -1)
			appRunner.CustomCssSegment.Remove(index, length);

		(index, length) = appRunner.CustomCssSegment.ProcessCssImportStatements(appRunner, true);

		if (index > -1)
			appRunner.CustomCssSegment.Remove(index, length);
		
		ProcessComponentsLayerAndCss(appRunner);
		ProcessUtilityClasses(appRunner);

		ProcessAtApplyStatements(appRunner.ImportsCssSegment, appRunner);
		ProcessAtApplyStatements(appRunner.ComponentsCssSegment, appRunner);
		ProcessAtApplyStatements(appRunner.CustomCssSegment, appRunner);

		ProcessFunctions(appRunner.BrowserResetCss, appRunner);
		ProcessFunctions(appRunner.FormsCss, appRunner);
		ProcessFunctions(appRunner.UtilitiesCssSegment, appRunner);
		ProcessFunctions(appRunner.ImportsCssSegment, appRunner);
		ProcessFunctions(appRunner.ComponentsCssSegment, appRunner);
		ProcessFunctions(appRunner.CustomCssSegment, appRunner);

		ProcessAtVariantStatements(appRunner.UtilitiesCssSegment, appRunner);
		ProcessAtVariantStatements(appRunner.ImportsCssSegment, appRunner);
		ProcessAtVariantStatements(appRunner.ComponentsCssSegment, appRunner);
		ProcessAtVariantStatements(appRunner.CustomCssSegment, appRunner);

		GeneratePropertiesAndThemeLayers(appRunner);

		if (appRunner.AppRunnerSettings.UseDarkThemeClasses)
		{
			ProcessDarkTheme(appRunner.UtilitiesCssSegment, appRunner);
			ProcessDarkTheme(appRunner.ImportsCssSegment, appRunner);
			ProcessDarkTheme(appRunner.ComponentsCssSegment, appRunner);
			ProcessDarkTheme(appRunner.CustomCssSegment, appRunner);
		}

		var workingSb = appRunner.AppState.StringBuilderPool.Get();
		var outputSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			var useLayers = appRunner.AppRunnerSettings.UseCompatibilityMode == false;

			if (useLayers)
			{
				outputSb.Append("@layer properties, theme, base, forms, components, utilities;");
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
				outputSb.Append("@layer theme {");
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

				outputSb.Append(appRunner.ThemeCssSegment);
				
				outputSb.Append('}');
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
			}
			else
			{
				outputSb.Append(appRunner.ThemeCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			if (appRunner.AppRunnerSettings.UseReset)
			{
				if (useLayers)
				{
					outputSb.Append("@layer base {");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
					outputSb.Append(appRunner.BrowserResetCss);
					
					outputSb.Append('}');
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

				}
				else
				{
					outputSb.Append(appRunner.BrowserResetCss);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}

			if (appRunner.AppRunnerSettings.UseForms)
			{
				if (useLayers)
				{
					outputSb.Append("@layer forms {");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
					outputSb.Append(appRunner.FormsCss);
					
					outputSb.Append('}');
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

				}
				else
				{
					outputSb.Append(appRunner.FormsCss);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}

			if (useLayers && appRunner.ComponentsCssSegment.Length > 0)
			{
				outputSb.Append("@layer components {");
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
				outputSb.Append(appRunner.ComponentsCssSegment);
					
				outputSb.Append('}');
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

			}
			else
			{
				outputSb.Append(appRunner.ComponentsCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			if (useLayers)
			{
				outputSb.Append("@layer utilities {");
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
				outputSb.Append(appRunner.UtilitiesCssSegment);
					
				outputSb.Append('}');
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}
			else
			{
				outputSb.Append(appRunner.UtilitiesCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			if (appRunner.ImportsCssSegment.Length > 0)
			{
				outputSb.Append(appRunner.ImportsCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			if (appRunner.CustomCssSegment.Length > 0)
			{
				outputSb.Append(appRunner.CustomCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			if (appRunner.PropertyListCssSegment.Length > 0)
			{
				outputSb.Append(appRunner.PropertyListCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			if (useLayers)
			{
				outputSb.Append("@layer properties {");
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				
				outputSb.Append(appRunner.PropertiesCssSegment);
					
				outputSb.Append('}');
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}
			else
			{
				outputSb.Append(appRunner.PropertiesCssSegment);
				outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
			}

			return appRunner.AppRunnerSettings.UseMinify ? outputSb.ToString().CompactCss(workingSb) : outputSb.ReformatCss(workingSb).ToString().NormalizeLinebreaks(appRunner.AppRunnerSettings.LineBreak);
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
			appRunner.AppState.StringBuilderPool.Return(outputSb);
		}
	}
	
	#endregion
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
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
	public static StringBuilder ProcessAtVariants(this StringBuilder sourceCss, AppRunner appRunner)
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

			if (appRunner.UsedCssCustomProperties.Count > 0)
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

		foreach (var cssClass in appRunner.UtilityClasses.Values.OrderBy(c => c.WrapperSort).ThenBy(c => c.SelectorSort))
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

			foreach (var cssClass in branch.CssClasses.OrderBy(c => c.SelectorSort).ThenBy(c => c.ClassDefinition?.NameSortOrder ?? 0).ThenBy(c => c.Selector))
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

	public static StringBuilder MoveComponentsLayer(this StringBuilder sourceCss, AppRunner appRunner)
	{
		if (appRunner.AppRunnerSettings.UseCompatibilityMode)
			return sourceCss;

		const string components = "@layer components";
		const string utilities = "@layer utilities";

		var componentsSb = appRunner.AppState.StringBuilderPool.Get();
		var rewrittenSb = new StringBuilder(sourceCss.Length);

		try
		{
			componentsSb.Clear();

			var css = sourceCss.ToString();
			var pos = 0;

			// single pass over the original CSS
			while (true)
			{
				var compIdx = css.IndexOf(components, pos, StringComparison.Ordinal);

				if (compIdx < 0)
				{
					// copy the tail & finish
					rewrittenSb.Append(css, pos, css.Length - pos);
					break;
				}

				// 1. copy everything *before* this @layer components verbatim
				rewrittenSb.Append(css, pos, compIdx - pos);

				// 2. parse the block to collect its body
				var i = compIdx + components.Length;

				// skip whitespace
				while (i < css.Length && char.IsWhiteSpace(css[i]))
					i++;

				if (i >= css.Length || css[i] != '{')
				{
					// malformed – treat the keyword as plain text and continue
					rewrittenSb.Append(components);
					pos = i;

					continue;
				}

				var contentStart = ++i; // skip '{'
				var depth = 1;

				while (i < css.Length && depth > 0)
				{
					var c = css[i++];

					if (c == '{')
						depth++;
					else if (c == '}')
						depth--;
				}

				var contentEnd = i - 1; // position of the matching '}'

				if (contentEnd > contentStart)
				{
					componentsSb.Append(css, contentStart, contentEnd - contentStart);
					componentsSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				pos = contentEnd + 1; // continue after the block we removed
			}

			// no components blocks?  just return the source unchanged
			if (componentsSb.Length == 0)
				return sourceCss;

			// wrap the gathered body into a single @layer components { … }
			componentsSb.Insert(0, components + " {" + appRunner.AppRunnerSettings.LineBreak);
			componentsSb.Append('}').Append(appRunner.AppRunnerSettings.LineBreak);

			// insert before the first @layer utilities, or at the end
			var utilitiesPos = rewrittenSb.IndexOf(utilities);

			if (utilitiesPos < 0)
				rewrittenSb.Append(componentsSb);
			else
				rewrittenSb.Insert(utilitiesPos, componentsSb);

			// overwrite the incoming StringBuilder and hand it back
			sourceCss.Clear().Append(rewrittenSb);

			return sourceCss;
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(componentsSb);
		}
	}

	/// <summary>
	/// Find all dark theme media blocks and duplicate as wrapped classes theme-dark
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static StringBuilder ProcessDarkThemeClasses(this StringBuilder sourceCss, AppRunner appRunner)
	{
		const string mediaPrefix = "@media (prefers-color-scheme: dark) {";

		var outCss = new StringBuilder(sourceCss.Length * 2);
		var css = sourceCss.ToString();
		var pos = 0;

		while (true)
		{
			var mediaIdx = css.IndexOf(mediaPrefix, pos, StringComparison.Ordinal);

			if (mediaIdx < 0)
			{
				// no more blocks – copy the tail and finish
				outCss.Append(css, pos, css.Length - pos);
				break;
			}

			// 1) copy everything before this @media as-is
			outCss.Append(css, pos, mediaIdx - pos);

			// 2) locate the matching '}' that closes the @media
			var bodyStart = mediaIdx + mediaPrefix.Length;
			var p = bodyStart;
			var depth = 1; // we are just after the '{'

			while (p < css.Length && depth > 0)
			{
				var c = css[p++];

				if (c == '{')
					depth++;
				else if (c == '}')
					depth--;
			}

			var bodyEnd = p - 1; // index of the '}' that closes the @media
			var inner = css.AsSpan(bodyStart, bodyEnd - bodyStart);

			// 3) rewrite the whole inner block in ONE pass
			var autoSb = appRunner.AppState.StringBuilderPool.Get();
			var darkSb = appRunner.AppState.StringBuilderPool.Get();

			try
			{
				autoSb.Clear();
				darkSb.Clear();

				RewriteContent(inner, autoSb, darkSb);

				// @media with ".theme-auto…" selectors
				outCss.Append(mediaPrefix).Append(autoSb).Append('}');

				// blank line between the two copies (match original behaviour)
				outCss.Append(appRunner.AppRunnerSettings.LineBreak)
					  .Append(appRunner.AppRunnerSettings.LineBreak);

				// ".theme-dark…" selectors outside any media query
				outCss.Append(darkSb);
			}
			finally
			{
				appRunner.AppState.StringBuilderPool.Return(autoSb);
				appRunner.AppState.StringBuilderPool.Return(darkSb);
			}

			pos = bodyEnd + 1; // skip the original @media block
		}

		sourceCss.Clear().Append(outCss);

		return sourceCss;

		// recursively rewrites one block body
		static void RewriteContent(ReadOnlySpan<char> src, StringBuilder autoSb, StringBuilder darkSb)
		{
			var i = 0;

			while (i < src.Length)
			{
				// copy leading whitespace verbatim
				var headerStart = i;

				while (headerStart < src.Length && char.IsWhiteSpace(src[headerStart]))
				{
					autoSb.Append(src[headerStart]);
					darkSb.Append(src[headerStart]);
					headerStart++;
				}

				if (headerStart >= src.Length)
					return;

				i = headerStart;

				// find the next '{' that starts the rule
				var braceRel = src[i..].IndexOf('{');

				if (braceRel < 0) // malformed CSS – bail out
				{
					autoSb.Append(src[i..]);
					darkSb.Append(src[i..]);

					return;
				}

				var bracePos = i + braceRel;
				var headerSpan = src.Slice(i, braceRel);

				// find the matching '}' for this rule
				var contentStart = bracePos + 1;
				var depth = 1;
				var p = contentStart;

				while (p < src.Length && depth > 0)
				{
					var c = src[p++];

					if (c == '{')
						depth++;
					else if (c == '}')
						depth--;
				}

				var contentEnd = p - 1; // position of '}'
				var ruleContent = src[contentStart..contentEnd];
				var isAtRule = headerSpan.TrimStart().Length > 0 && headerSpan.TrimStart()[0] == '@';

				if (isAtRule)
				{
					// copy @-rules unchanged, but rewrite inside them
					autoSb.Append(headerSpan).Append('{');
					darkSb.Append(headerSpan).Append('{');

					RewriteContent(ruleContent, autoSb, darkSb);

					autoSb.Append('}');
					darkSb.Append('}');
				}
				else
				{
					// selector list: add the two theme prefixes
					var selectors = new List<string>();
					AddSelectors(headerSpan, selectors);

					var first = true;

					foreach (var sel in selectors)
					{
						if (first == false)
						{
							autoSb.Append(", ");
							darkSb.Append(", ");
						}

						first = false;

						var needsTwo = NeedsDoubleForm(sel);

						autoSb.Append(".theme-auto ").Append(sel);

						if (needsTwo)
							autoSb.Append(", .theme-auto").Append(sel);

						darkSb.Append(".theme-dark ").Append(sel);

						if (needsTwo)
							darkSb.Append(", .theme-dark").Append(sel);
					}

					autoSb.Append(" {");
					darkSb.Append(" {");

					// copy rule body verbatim
					autoSb.Append(ruleContent);
					darkSb.Append(ruleContent);

					autoSb.Append('}');
					darkSb.Append('}');
				}

				i = contentEnd + 1; // continue after this rule
			}
		}

		// does the selector need the “no space” twin?
		static bool NeedsDoubleForm(string sel)
		{
			if (string.IsNullOrEmpty(sel))
				return false;

			return sel[0] switch
			{
				'.' or '#' or '[' or ':' or '*' or '>' or '+' or '~' => true,
				_ => false,
			};
		}

		// split a selector list on top-level (unescaped) commas
		static void AddSelectors(ReadOnlySpan<char> header, List<string> output)
		{
			var paren = 0;
			var square = 0;
			var start = 0;

			var quoteChar = '\0';

			var inQuote = false;
			var escaped = false;

			for (var i = 0; i < header.Length; i++)
			{
				var c = header[i];

				if (escaped)
				{
					escaped = false;
					continue;
				}

				if (c == '\\')
				{
					escaped = true;
					continue;
				}

				if (inQuote)
				{
					if (c == quoteChar)
						inQuote = false;

					continue;
				}

				if (c == '"' || c == '\'')
				{
					inQuote = true;
					quoteChar = c;

					continue;
				}

				switch (c)
				{
					case '(': paren++; break;
					case ')': if (paren > 0) paren--; break;
					case '[': square++; break;
					case ']': if (square > 0) square--; break;
					case ',':
						if (paren == 0 && square == 0)
						{
							var sel = header[start..i].Trim();

							if (sel.IsEmpty == false)
								output.Add(sel.ToString());

							start = i + 1;
						}

						break;
				}
			}

			var tail = header[start..].Trim();

			if (tail.IsEmpty == false)
				output.Add(tail.ToString());
		}
	}
}