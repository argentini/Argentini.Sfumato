// ReSharper disable RedundantAssignment
// ReSharper disable MemberCanBePrivate.Global

using System.Globalization;
using System.Reflection;

namespace Argentini.Sfumato.Entities.Runners;

public static class AppRunnerExtensions
{
	#region Extract CSS Imports / Components Layer

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
    // reuse a single static token
	public static (int start, int length) ProcessCssImportStatements(this StringBuilder? css, AppRunner? appRunner, bool isRoot, string parentLayerName = "", string parentPath = "")
	{
	    if (css is null || appRunner is null)
	        return (-1, -1);

	    var settings = appRunner.AppRunnerSettings;
	    var importsSeg = appRunner.ImportsCssSegment;
	    var compsSeg = appRunner.ComponentsCssSegment;
	    var pool = appRunner.AppState.StringBuilderPool;
	    var baseDir = settings.NativeCssFilePathOnly;

	    if (isRoot)
	    {
	        importsSeg.Clear();
	        compsSeg.Clear();

	        foreach (var kv in settings.CssImports)
	            kv.Value.Touched = false;
	    }

	    //––– snapshot to string + prep –––
	    const string importToken = "@import ";
	    var src = css.ToString();
	    var totalLength = src.Length;
	    var tokenLen = importToken.Length;
	    var scanPos = 0;
	    var firstIdx = -1;
	    var lastEnd = 0; // ← initialize to 0, not –1

	    // find the very first @import
	    var idx = src.IndexOf(importToken, scanPos, StringComparison.Ordinal);
	    
	    if (idx >= 0) firstIdx = idx;

	    while (idx >= 0)
	    {
	        var semicolon = src.IndexOf(';', idx + tokenLen);
	        
	        if (semicolon < 0) break;

	        lastEnd = semicolon + 1;
	        scanPos = lastEnd;

	        // extract the raw between “@import ” and “;”
	        var rawSpan = src.AsSpan(idx + tokenLen, semicolon - (idx + tokenLen)).Trim();

	        // split off optional layer name
	        var layerName = "";
	        
	        if (rawSpan.Length > 0)
	        {
	            var lastChar = rawSpan[^1];
	            
	            if (lastChar != '\'' && lastChar != '"')
	            {
	                var sp = rawSpan.LastIndexOf(' ');
	                
	                if (sp >= 0)
	                {
	                    layerName = new string(rawSpan[(sp + 1)..]);
	                    rawSpan   = rawSpan[..sp];
	                }
	            }
	        }

	        rawSpan = rawSpan.Trim(' ', '\'', '"');

	        // resolve path + cache
	        var relPath  = new string(rawSpan);
	        var filePath = Path.GetFullPath(Path.Combine(baseDir, parentPath, relPath));
	        
	        if (File.Exists(filePath) == false)
	        {
	            Console.WriteLine($"{AppState.CliErrorPrefix}File does not exist: {filePath}");

	            importsSeg
	              .Append("/* Could not import: ")
	              .Append(filePath)
	              .Append(" */")
	              .Append(settings.LineBreak);
	        }
	        else
	        {
	            if (settings.CssImports.TryGetValue(filePath, out var importFile) == false)
	            {
	                importFile = new CssImportFile {
	                    Touched    = true,
	                    Pool       = pool,
	                    FileInfo   = new FileInfo(filePath),
	                    CssContent = pool.Get().Append(File.ReadAllText(filePath))
	                };

	                settings.CssImports[filePath] = importFile;
	            }
	            else
	            {
	                importFile.Touched = true;
	                
	                var fi = new FileInfo(filePath);
	                
	                if (fi.Length != importFile.FileInfo.Length || fi.CreationTimeUtc  != importFile.FileInfo.CreationTimeUtc || fi.LastWriteTimeUtc != importFile.FileInfo.LastWriteTimeUtc)
	                {
	                    importFile.FileInfo = fi;
	                    importFile.CssContent?.Clear().Append(File.ReadAllText(filePath));
	                }
	            }

	            var hasLayer = string.IsNullOrEmpty(layerName) == false && layerName != "components";
	            
	            if (hasLayer)
	                importsSeg
	                  .Append("@layer ")
	                  .Append(layerName)
	                  .Append(" {")
	                  .Append(settings.LineBreak);

	            // recurse into the imported file
	            _ = ProcessCssImportStatements(
	                importFile.CssContent,
	                appRunner,
	                false,
	                layerName,
	                Path.GetDirectoryName(filePath) ?? string.Empty
	            );

	            if (hasLayer)
	                importsSeg
	                  .Append('}')
	                  .Append(settings.LineBreak);
	        }

	        // next match
	        idx = src.IndexOf(importToken, scanPos, StringComparison.Ordinal);
	    }

	    //––– finalize –––
	    if (isRoot)
	    {
	        settings.CssImports.RemoveWhere((_, v) => v.Touched == false);

	        if (firstIdx < 0 || lastEnd < firstIdx)
	            return (-1, -1);
	        
	        return (firstIdx, lastEnd - firstIdx);
	    }
	    
        // leaf: append everything after the last “;”
        if (lastEnd >= totalLength)
            return (-1, -1);

        var targetSeg = parentLayerName == "components" ? compsSeg : importsSeg;

        targetSeg
	        .Append(src, lastEnd, totalLength - lastEnd)
	        .Trim();

        targetSeg
          .Append(settings.LineBreak);

        return (-1, -1);
	}
	
	#endregion
	
	#region Extract Sfumato Block
	
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
				Console.WriteLine("No @layer sfumato { :root { } } block found.");
				return (-1, -1);
			}
		}

		var blockStartIndex = css.IndexOf('{', index) + 1;
		
		appRunner.SfumatoSegment.ReplaceContent(css, blockStartIndex, length - (blockStartIndex - index) - 1);

		return (index, length);
	}
	
	#endregion

	#region Import Sfumato Block Items
	
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
	
	#region Generate Components Layer / Remaining CSS

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

	#region Generate Utility Classes
	
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

		foreach (var cssClass in branch.CssClasses.OrderBy(c => c.SelectorSort).ThenBy(c => c.Selector))
			workingSb
				.Append(cssClass.EscapedSelector)
				.Append(" {")
				.Append(cssClass.Styles)
				.Append('}');

		if (branch.Branches.Count > 0)
		{
			foreach (var subBranch in branch.Branches)
				GenerateUtilityClassesCss(subBranch, workingSb);
		}

		if (string.IsNullOrEmpty(branch.WrapperCss))
			return;

		workingSb
			.Append('}');
	}
	
	#endregion
	
	#region Process @apply Statements
	
	/// <summary>
	/// Convert @apply statements in source CSS to utility class property statements.
	/// Also tracks any used CSS custom properties or CSS snippets.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	public static async ValueTask ProcessAtApplyStatementsAsync(this StringBuilder css, AppRunner appRunner)
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
		
		await Task.CompletedTask;
	}
	
	#endregion
	
	#region Process Functions
	
    /// <summary>
    /// Convert functions in source CSS to CSS statements (e.g. --alpha()).
    /// Also tracks any used CSS custom properties or CSS snippets.
    /// </summary>
    /// <param name="css"></param>
    /// <param name="appRunner"></param>
    public static async ValueTask ProcessFunctionsAsync(this StringBuilder css, AppRunner appRunner) // 410
    {
        // locals
        var settings = appRunner.AppRunnerSettings;
        var used     = appRunner.UsedCssCustomProperties;
        var colors   = appRunner.Library.ColorsByName;
        var pool     = appRunner.AppState.StringBuilderPool;

        // 1) snapshot to string once
        var src = css.ToString();
        var n = src.Length;
        var data = src.AsSpan();

        // 2) prepare output SB
        var output = pool.Get();

        output.Clear();
        output.EnsureCapacity(n);

        var i = 0;
        
        while (i < n)
        {
            // 2a) bulk-copy until next '-'
            var nextDash = data[i..].IndexOf('-');
            
            if (nextDash < 0)
            {
                // no more dashes: copy the rest
                output.Append(src, i, n - i);
                break;
            }

            if (nextDash > 0)
            {
                // copy up to the dash
                output.Append(src, i, nextDash);
                i += nextDash;
                // fall through to handle dash
            }

            // 2b) at data[i] == '-'
            // Check for "--alpha("
            if (i + 8 <= n
                && data[i + 1] == '-'
                && data[i + 2] == 'a'
                && data[i + 3] == 'l'
                && data[i + 4] == 'p'
                && data[i + 5] == 'h'
                && data[i + 6] == 'a'
                && data[i + 7] == '(')
            {
                var start = i;
                var j     = i + 8;
                var depth = 1;

                // find matching ')'
                while (j < n && depth > 0)
                {
                    var c = data[j++];
                    
                    if (c == '(')
	                    depth++;
                    else if (c == ')')
	                    depth--;
                }

                if (depth == 0)
                {
                    var matchLen = j - start;
                    
                    // inner content without a final ')'
                    var inner = data.Slice(start + 8, matchLen - 9).Trim();  
                    
                    // look for "var(--color-" and '/'
                    var vp = inner.IndexOf("var(--color-", StringComparison.Ordinal);
                    var sp = inner.LastIndexOf('/');
                    
                    if (vp >= 0 && sp >= 0)
                    {
                        // find end of var(...)
                        var vend = inner[vp..].IndexOf(')') + vp;
                        
                        if (vend > vp)
                        {
                            // extract name after "--color-"
                            const string prefix = "--color-";
                            
                            var keyStart = vp + "var(".Length + prefix.Length;
                            var keyLen   = vend - keyStart;

                            if (keyLen > 0)
                            {
                                var keySpan = inner.Slice(keyStart, keyLen);
                                
                                if (colors.TryGetValue(keySpan.ToString(), out var colVal))
                                {
                                    // parse percent
                                    var pctSpan = inner[(sp + 1)..].Trim(' ', '%');
                                    
                                    if (int.TryParse(pctSpan, out var pct))
                                    {
                                        output.Append(colVal.SetWebColorAlpha(pct));
                                        i = start + matchLen;

                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    // fallback: copy literally
                    output.Append(src, start, matchLen);
                    i = start + matchLen;

                    continue;
                }
                // else unbalanced → treat '-' as literal
            }

            // 2c) Check for "--spacing("
            if (i + 10 <= n
                && data[i + 1] == '-'
                && data[i + 2] == 's'
                && data[i + 3] == 'p'
                && data[i + 4] == 'a'
                && data[i + 5] == 'c'
                && data[i + 6] == 'i'
                && data[i + 7] == 'n'
                && data[i + 8] == 'g'
                && data[i + 9] == '(')
            {
                var start = i;
                var j = i + 10;
                var depth = 1;
                
	            while (j < n && depth > 0)
                {
                    var c = data[j++];
                    
                    if (c == '(')
	                    depth++;
                    else if (c == ')')
	                    depth--;
                }

                if (depth == 0)
                {
                    var matchLen = j - start;
                    var numSpan  = data.Slice(start + 10, matchLen - 11).Trim();
                    
                    if (numSpan.Length > 0 && double.TryParse(numSpan, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                    {
                        output
                          .Append("calc(var(--spacing) * ")
                          .Append(val)
                          .Append(')');

                        used.TryAdd("--spacing", string.Empty);
                        i = start + matchLen;
                        
                        continue;
                    }

                    // fallback
                    output.Append(src, start, matchLen);
                    i = start + matchLen;

                    continue;
                }
                // else treat dash as literal
            }

            // 2d) no token: copy the dash
            output.Append('-');
            i++;
        }

        // 3) write back
        css.Clear();
        css.Append(output);
        pool.Return(output);

        // 4) custom-property tracking
        foreach (var pos in css.EnumerateCssCustomPropertyPositions(namesOnly: true))
        {
            var prop = css.ToString(pos.PropertyStart, pos.PropertyLength);

            if (settings.SfumatoBlockItems.TryGetValue(prop, out var val))
                used.TryAdd(prop, val);
        }
        
        await Task.CompletedTask;
    }

    #endregion
	
	#region Process @variant Statements
	
	/// <summary>
	/// Convert @variant statements in source CSS to media query statements.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	public static async ValueTask ProcessAtVariantStatementsAsync(this StringBuilder css, AppRunner appRunner)
	{
		foreach (var span in css.ToString().EnumerateAtVariantStatements())
		{
			if (span.Name.ToString().TryVariantIsMediaQuery(appRunner, out var variant))
				css.Replace(span.Full, $"@{variant?.PrefixType} {variant?.Statement} {{");
		}
		
		await Task.CompletedTask;
	}
	
	#endregion
	
	#region Generate @properties Layer / @property Rules
	
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

		var collection = appRunner.UsedCssCustomProperties.OrderBy(i => i.Key).ToList();
		
		if (appRunner.UsedCssCustomProperties.Count > 0)
		{
			#region @layer properties
			
			appRunner.PropertiesCssSegment
				.Append("*, ::before, ::after, ::backdrop {")
				.Append(appRunner.AppRunnerSettings.LineBreak);

			foreach (var ccp in collection.Where(c => (c.Key.StartsWith("--sf-") || c.Key.StartsWith("--form-")) && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
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
				foreach (var ccp in collection)
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
		
			foreach (var ccp in collection.Where(c => c.Key.StartsWith("--sf-") == false && c.Key.StartsWith("--form-") == false && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
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
				foreach (var ccp in appRunner.UsedCss.Where(c => string.IsNullOrEmpty(c.Value) == false).OrderBy(i => i.Key))
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
	
	#region Process Dark Theme Classes
	
	/// <summary>
	/// Find all dark theme media blocks and duplicate as wrapped classes theme-dark
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static async ValueTask ProcessDarkThemeAsync(this StringBuilder sourceCss, AppRunner appRunner)
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

			// copy everything before this @media as-is
			outCss.Append(css, pos, mediaIdx - pos);

			// locate the matching '}' that closes the @media
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

			// rewrite the whole inner block in ONE pass
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

		await Task.CompletedTask;
		
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
					// copy @-rules, but rewrite inside them
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
	
	#region Load Source CSS File, Sfumato Settings
	
	public static async Task<bool> LoadCssFileAsync(this AppRunner appRunner)
	{
		var workingSb = appRunner.AppState.StringBuilderPool.Get();

		try
		{
			if (string.IsNullOrEmpty(appRunner.AppRunnerSettings.CssFilePath) == false)
			{
				var filePath = Path.GetFullPath(appRunner.AppRunnerSettings.CssFilePath.SetNativePathSeparators());

				if (File.Exists(filePath) == false)
				{
					Console.WriteLine($"Could not find file => {filePath}");
					Environment.Exit(1);
					return false;
				}
				
				appRunner.AppRunnerSettings.CssContent = (await File.ReadAllTextAsync(filePath))
					.TrimWhitespaceBeforeLineBreaks(workingSb)
					.RemoveBlockComments(workingSb)
					.NormalizeLinebreaks(appRunner.AppRunnerSettings.LineBreak)
					.Trim();

				return appRunner.AppRunnerSettings.CssContent.LoadSfumatoSettings(appRunner);
			}
			
			/*
			Console.WriteLine("No CSS file specified");
			Environment.Exit(1);
			*/

			return false;
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error reading file => {e.Message}");
			Environment.Exit(1);

			return false;
		}
		finally
		{
			appRunner.AppState.StringBuilderPool.Return(workingSb);
		}
	}
	
	/// <summary>
	/// Load Sfumato settings from CSS file, remove block from source.
	/// Returns true on success.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static bool LoadSfumatoSettings(this string css, AppRunner appRunner)
	{
		appRunner.CustomCssSegment.ReplaceContent(css);

		var (index, length) = appRunner.CustomCssSegment.ExtractSfumatoBlock(appRunner);

		if (index > -1)
		{
			appRunner.CustomCssSegment.Remove(index, length);
			appRunner.CssContentWithoutSettings.ReplaceContent(appRunner.CustomCssSegment);
			
			ImportSfumatoBlockSettingsItems(appRunner);

			return ProcessSfumatoBlockSettings(appRunner);
		}

		return false;
	}
	
	#endregion

	#region Process Default Sfumato Settings
	
    /// <summary>
    /// Processes CSS settings for colors, breakpoints, etc., and uses reflection to load all others per utility class file.  
    /// </summary>
    public static bool ProcessSfumatoBlockSettings(AppRunner appRunner, bool usingDefaults = false)
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
			    appRunner.AppRunnerSettings.CssOutputFilePath = (string.IsNullOrEmpty(outputPath) ? string.Empty : outputPath).Trim('\"');

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

		    if (usingDefaults)
			    return true;
		    
		    if (string.IsNullOrEmpty(appRunner.AppRunnerSettings.CssOutputFilePath))
		    {
			    Console.WriteLine($"{AppState.CliErrorPrefix}Must specify --output-path in file: {appRunner.AppRunnerSettings.CssFilePath}");

			    return false;
		    }

		    if (appRunner.AppRunnerSettings.Paths.Count > 0)
			    return true;
		    
		    Console.WriteLine($"{AppState.CliErrorPrefix}Must specify --paths: [] in file: {appRunner.AppRunnerSettings.CssFilePath}");

		    return false;
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

	    return false;
    }
	
	#endregion
	
	#region Build CSS
	
	public static async Task<string> FullBuildCssAsync(AppRunner appRunner)
	{
		appRunner.CustomCssSegment.ReplaceContent(appRunner.CssContentWithoutSettings);
		appRunner.PropertiesCssSegment.Clear();
		appRunner.PropertyListCssSegment.Clear();
		appRunner.ThemeCssSegment.Clear();

		var (index, length) = appRunner.CustomCssSegment.ProcessCssImportStatements(appRunner, true);

		if (index > -1)
			appRunner.CustomCssSegment.Remove(index, length);
		
		ProcessComponentsLayerAndCss(appRunner);
		ProcessUtilityClasses(appRunner);

		await ProcessAtApplyStatementsAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessAtApplyStatementsAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessAtApplyStatementsAsync(appRunner.CustomCssSegment, appRunner);

		await ProcessFunctionsAsync(appRunner.BrowserResetCss, appRunner);
		await ProcessFunctionsAsync(appRunner.FormsCss, appRunner);
		await ProcessFunctionsAsync(appRunner.UtilitiesCssSegment, appRunner);
		await ProcessFunctionsAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessFunctionsAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessFunctionsAsync(appRunner.CustomCssSegment, appRunner);

		await ProcessAtVariantStatementsAsync(appRunner.UtilitiesCssSegment, appRunner);
		await ProcessAtVariantStatementsAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessAtVariantStatementsAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessAtVariantStatementsAsync(appRunner.CustomCssSegment, appRunner);

		GeneratePropertiesAndThemeLayers(appRunner);

		if (appRunner.AppRunnerSettings.UseDarkThemeClasses == false)
			return FinalCssAssembly(appRunner);

		await ProcessDarkThemeAsync(appRunner.UtilitiesCssSegment, appRunner);
		await ProcessDarkThemeAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessDarkThemeAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessDarkThemeAsync(appRunner.CustomCssSegment, appRunner);

		return FinalCssAssembly(appRunner);
	}

	public static async Task<string> ProjectChangeBuildCssAsync(AppRunner appRunner)
	{
		appRunner.CustomCssSegment.ReplaceContent(appRunner.CssContentWithoutSettings);
		appRunner.PropertiesCssSegment.Clear();
		appRunner.PropertyListCssSegment.Clear();
		appRunner.ThemeCssSegment.Clear();

		var (index, length) = appRunner.CustomCssSegment.ProcessCssImportStatements(appRunner, true);

		if (index > -1)
			appRunner.CustomCssSegment.Remove(index, length);
		
		ProcessComponentsLayerAndCss(appRunner);
		ProcessUtilityClasses(appRunner);

		await ProcessAtApplyStatementsAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessAtApplyStatementsAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessAtApplyStatementsAsync(appRunner.CustomCssSegment, appRunner);

		await ProcessFunctionsAsync(appRunner.BrowserResetCss, appRunner);
		await ProcessFunctionsAsync(appRunner.FormsCss, appRunner);
		await ProcessFunctionsAsync(appRunner.UtilitiesCssSegment, appRunner);
		await ProcessFunctionsAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessFunctionsAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessFunctionsAsync(appRunner.CustomCssSegment, appRunner);

		await ProcessAtVariantStatementsAsync(appRunner.UtilitiesCssSegment, appRunner);
		await ProcessAtVariantStatementsAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessAtVariantStatementsAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessAtVariantStatementsAsync(appRunner.CustomCssSegment, appRunner);

		GeneratePropertiesAndThemeLayers(appRunner);

		if (appRunner.AppRunnerSettings.UseDarkThemeClasses == false)
			return FinalCssAssembly(appRunner);

		await ProcessDarkThemeAsync(appRunner.UtilitiesCssSegment, appRunner);
		await ProcessDarkThemeAsync(appRunner.ImportsCssSegment, appRunner);
		await ProcessDarkThemeAsync(appRunner.ComponentsCssSegment, appRunner);
		await ProcessDarkThemeAsync(appRunner.CustomCssSegment, appRunner);

		return FinalCssAssembly(appRunner);
	}

	public static string FinalCssAssembly(AppRunner appRunner)
	{
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
}