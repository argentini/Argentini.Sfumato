// ReSharper disable RedundantAssignment
// ReSharper disable MemberCanBePrivate.Global

using System.Globalization;
using System.Reflection;
using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Scanning;
using Sfumato.Entities.UtilityClasses;
using Sfumato.Helpers;
using Strings = Sfumato.Helpers.Strings;

// ReSharper disable RedundantBoolCompare
// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable InvertIf

namespace Sfumato.Entities.Runners;

public static class AppRunnerExtensions
{
	#region Process Default Sfumato Settings
	
    /// <summary>
    /// Processes CSS settings for colors, breakpoints, etc., and uses reflection to load all others per utility class file.  
    /// </summary>
    public static bool ProcessSfumatoBlockSettings(AppRunner appRunner, bool usingDefaults = false)
    {
	    #region Read color definitions
	    
	    foreach (var match in appRunner.AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--color-")))
	    {
		    var key = Strings.TrimStart(match.Key, "--color-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (appRunner.Library.ColorsByName.TryAdd(key, match.Value) == false)
			    appRunner.Library.ColorsByName[key] = match.Value;
	    }
	    
	    #endregion

	    #region Read breakpoints

	    var prefixOrder = 100;

	    appRunner.AppRunnerSettings.BreakpointSizes.Clear();

	    foreach (var match in appRunner.AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (match.Key.StartsWith("--breakpoint-") == false)
			    continue;
		    
		    var key = Strings.TrimStart(match.Key, "--breakpoint-") ?? string.Empty;

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
		    
		    foreach (var unit in appRunner.Library.CssLengthUnits.OrderByDescending(u => u.Length))
			    if (match.Value.EndsWith(unit))
			    {
				    if (double.TryParse(Strings.TrimEnd(match.Value, unit), out var value))
					    appRunner.AppRunnerSettings.BreakpointSizes.TryAdd(key, value);

				    break;
			    }
	    }
	    
	    foreach (var breakpoint in appRunner.AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (breakpoint.Key.StartsWith("--adaptive-breakpoint-") == false)
			    continue;

		    var key = Strings.TrimStart(breakpoint.Key, "--adaptive-breakpoint-") ?? string.Empty;

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
		    var key = Strings.TrimStart(breakpoint.Key, "--container-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (appRunner.Library.ContainerQueryPrefixes.TryAdd($"@{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width >= {breakpoint.Value})",
			        SpecialCase = true
		        }) == false)
		    {
			    appRunner.Library.ContainerQueryPrefixes[$"@{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width >= {breakpoint.Value})"
			    };
		    }

		    if (appRunner.Library.ContainerQueryPrefixes.TryAdd($"@@{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width >= {breakpoint.Value})",
			        SpecialCase = true,
			        IsRazorSyntax = true
		        }) == false)
		    {
			    appRunner.Library.ContainerQueryPrefixes[$"@@{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width >= {breakpoint.Value})",
				    IsRazorSyntax = true
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (appRunner.Library.ContainerQueryPrefixes.TryAdd($"@max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width < {breakpoint.Value})",
			        SpecialCase = true
		        }) == false)
		    {
			    appRunner.Library.ContainerQueryPrefixes[$"@max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width < {breakpoint.Value})"
			    };
		    }

		    if (appRunner.Library.ContainerQueryPrefixes.TryAdd($"@@max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width < {breakpoint.Value})",
			        SpecialCase = true,
			        IsRazorSyntax = true
		        }) == false)
		    {
			    appRunner.Library.ContainerQueryPrefixes[$"@@max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width < {breakpoint.Value})",
				    IsRazorSyntax = true
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

			    var statement = $"{Strings.TrimStart(match.Value, $"@{prefixType}")?.Trim()}";
			    
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
				appRunner.Library.ScannerClassNamePrefixes.Insert(segments[1], null);
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

	    appRunner.Library.AllVariants.AddRange(appRunner.Library.MediaQueryPrefixes);
	    appRunner.Library.AllVariants.AddRange(appRunner.Library.PseudoclassPrefixes);
	    appRunner.Library.AllVariants.AddRange(appRunner.Library.ContainerQueryPrefixes);
	    appRunner.Library.AllVariants.AddRange(appRunner.Library.SupportsQueryPrefixes);
	    appRunner.Library.AllVariants.AddRange(appRunner.Library.StartingStyleQueryPrefixes);

	    #region Read project settings

	    var workingSb = appRunner.StringBuilderPool.Get();

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
					    var path = Path.GetFullPath(Path.Combine(appRunner.AppRunnerSettings.NativeCssFilePathOnly, p.Trim('\"')));

					    if (Directory.Exists(path) == false)
					    {
						    Console.WriteLine($"{Constants.CliErrorPrefix}Skipping invalid project path: {path}");
					    }
					    else
					    {
						    appRunner.AppRunnerSettings.Paths.Add(p.Trim('\"'));
						    appRunner.AppRunnerSettings.AbsolutePaths.Add(path);
					    }
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
			    Console.WriteLine($"{Constants.CliErrorPrefix}Must specify --output-path in file: {appRunner.AppRunnerSettings.CssFilePath}");

			    return false;
		    }

		    if (appRunner.AppRunnerSettings.Paths.Count > 0)
			    return true;
		    
		    Console.WriteLine($"{Constants.CliErrorPrefix}Must specify --paths: [] in file: {appRunner.AppRunnerSettings.CssFilePath}");

		    return false;
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{Constants.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
	    finally
	    {
		    appRunner.StringBuilderPool.Return(workingSb);
		}
	    
	    #endregion

	    return false;
    }
	
	#endregion
	
	#region Load Source CSS File, Sfumato Settings
	
	public static async Task<bool> LoadCssFileAsync(this AppRunner appRunner)
	{
		var workingSb = appRunner.StringBuilderPool.Get();

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
			appRunner.StringBuilderPool.Return(workingSb);
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
		appRunner.CustomCssSegment.Content.ReplaceContent(css);

		var (index, length) = appRunner.CustomCssSegment.Content.ExtractSfumatoBlock(appRunner);

		if (index > -1)
		{
			appRunner.CustomCssSegment.Content.Remove(index, length);
			appRunner.CssContentWithoutSettings.ReplaceContent(appRunner.CustomCssSegment.Content);
			
			ImportSfumatoBlockSettingsItems(appRunner);

			return ProcessSfumatoBlockSettings(appRunner);
		}

		return false;
	}
	
	#endregion
	
	#region Extract Sfumato Block

	/// <summary>
	/// Extract the Sfumato layer block from a CSS string.
	/// Stores it into SfumatoSegment.Content.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="appRunner"></param>
	/// <param name="startIndex"></param>
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
		
		appRunner.SfumatoSegment.Content.ReplaceContent(css, blockStartIndex, length - (blockStartIndex - index) - 1);

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
	        foreach (var pos in appRunner.SfumatoSegment.Content.EnumerateCssCustomPropertyPositions())
		        if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryAdd(appRunner.SfumatoSegment.Content.Substring(pos.PropertyStart, pos.PropertyLength), appRunner.SfumatoSegment.Content.Substring(pos.ValueStart, pos.ValueLength)) == false)
		        {
			        appRunner.AppRunnerSettings.SfumatoBlockItems[appRunner.SfumatoSegment.Content.Substring(pos.PropertyStart, pos.PropertyLength)] = appRunner.SfumatoSegment.Content.Substring(pos.ValueStart, pos.ValueLength);
		        }

	        foreach (var pos in appRunner.SfumatoSegment.Content.EnumerateCssClassAndAtBlockPositions())
		        if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryAdd(appRunner.SfumatoSegment.Content.Substring(pos.HeaderStart, pos.HeaderLength), appRunner.SfumatoSegment.Content.Substring(pos.BodyStart, pos.BodyLength).TrimEnd(';').Trim()) == false)
		        {
			        appRunner.AppRunnerSettings.SfumatoBlockItems[appRunner.SfumatoSegment.Content.Substring(pos.HeaderStart, pos.HeaderLength)] = appRunner.SfumatoSegment.Content.Substring(pos.BodyStart, pos.BodyLength).TrimEnd(';').Trim();
		        }

	        foreach (var pos in appRunner.SfumatoSegment.Content.EnumerateCustomVariantPositions())
		        if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryAdd(appRunner.SfumatoSegment.Content.Substring(pos.NameStart, pos.NameLength), appRunner.SfumatoSegment.Content.Substring(pos.ContentStart, pos.ContentLength).TrimEnd(';')) == false)
		        {
			        appRunner.AppRunnerSettings.SfumatoBlockItems[appRunner.SfumatoSegment.Content.Substring(pos.NameStart, pos.NameLength)] = appRunner.SfumatoSegment.Content.Substring(pos.ContentStart, pos.ContentLength).TrimEnd(';');
		        }

	        if (appRunner.AppRunnerSettings.SfumatoBlockItems.Count > 0)
	            return;
	        
	        Console.WriteLine($"{Constants.CliErrorPrefix}No Sfumato options specified in CSS file.");
	        Environment.Exit(1);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{Constants.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
	
	#endregion



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
	public static (int start, int length) ExtractCssImportStatements(this StringBuilder? css, AppRunner? appRunner, bool isRoot, string parentLayerName = "", string parentPath = "")
	{
	    if (css is null || appRunner is null)
	        return (-1, -1);

	    var settings = appRunner.AppRunnerSettings;
	    var importsSeg = appRunner.ImportsCssSegment;
	    var compsSeg = appRunner.ComponentsCssSegment;
	    var pool = appRunner.StringBuilderPool;
	    var baseDir = settings.NativeCssFilePathOnly;

	    if (isRoot)
	    {
	        importsSeg.Content.Clear();
	        compsSeg.Content.Clear();

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

	        // extract the raw between "@import " and ";"
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
	            Console.WriteLine($"{Constants.CliErrorPrefix}File does not exist: {filePath}");

	            importsSeg.Content
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
	                importsSeg.Content
	                  .Append("@layer ")
	                  .Append(layerName)
	                  .Append(" {")
	                  .Append(settings.LineBreak);

	            // recurse into the imported file
	            _ = ExtractCssImportStatements(
	                importFile.CssContent,
	                appRunner,
	                false,
	                layerName,
	                Path.GetDirectoryName(filePath) ?? string.Empty
	            );

	            if (hasLayer)
	                importsSeg.Content
	                  .Append('}')
	                  .Append(settings.LineBreak);
	        }

	        // next match
	        idx = src.IndexOf(importToken, scanPos, StringComparison.Ordinal);
	    }

	    //––– finalize –––
	    if (isRoot)
	    {
		    foreach (var (k, v) in settings.CssImports)
			    if (v.Touched == false)
					settings.CssImports.Remove(k);

	        if (firstIdx < 0 || lastEnd < firstIdx)
	            return (-1, -1);
	        
	        return (firstIdx, lastEnd - firstIdx);
	    }
	    
        // leaf: append everything after the last “;”
        if (lastEnd >= totalLength)
            return (-1, -1);

        var targetSeg = parentLayerName == "components" ? compsSeg : importsSeg;

        targetSeg.Content
	        .Append(src, lastEnd, totalLength - lastEnd)
	        .Trim();

        targetSeg.Content
          .Append(settings.LineBreak);

        return (-1, -1);
	}
	
	#endregion

	#region Process All Components Layers / Remaining CSS

	public static void ProcessAllComponentsLayersAndCss(this AppRunner appRunner)
	{
		var (index, length) = appRunner.CustomCssSegment.Content.ExtractCssBlock("@layer components");
		
		while (index > -1)
		{
			var openBraceIndex = appRunner.CustomCssSegment.Content.IndexOf('{', index + 1);

			if (openBraceIndex > -1)
				appRunner.ComponentsCssSegment.Content.Append(appRunner.CustomCssSegment.Content, openBraceIndex + 1, length - (openBraceIndex - index) - 2);
			
			appRunner.CustomCssSegment.Content.Remove(index, length);
			
			(index, length) = appRunner.CustomCssSegment.Content.ExtractCssBlock("@layer components");
		}
	}

	#endregion

	#region Generate Utility Classes
	
	/// <summary>
	/// Generate utility class CSS from the AppRunner.UtilityClasses dictionary. 
	/// </summary>
	/// <param name="appRunner"></param>
	public static void GenerateUtilityClasses(this AppRunner appRunner)
	{
		appRunner.UtilitiesCssSegment.Content.Clear();
		
		var root = new VariantBranch
		{
			Fingerprint = 0,
			Depth = 0,
			WrapperCss = string.Empty
		};

		foreach (var (_, cssClass) in appRunner.UtilityClasses.ToList().OrderBy(c => c.Value.WrapperSort).ThenBy(c => c.Value.SelectorSort))
			ProcessVariantBranchRecursive(root, cssClass);
		
		GenerateUtilityClassesCss(root, appRunner.UtilitiesCssSegment.Content);
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
			Console.WriteLine($"{Constants.CliErrorPrefix}_ProcessVariantBranchRecursive() - {e.Message}");
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

	
	
    #region Process CSS Segment @apply Statements

    /// <summary>
    /// Convert @apply statements in segment CSS to utility class property statements.
    /// </summary>
    /// <param name="appRunner"></param>
    /// <param name="segment"></param>
	public static ValueTask ProcessSegmentAtApplyStatementsAsync(this AppRunner appRunner, GenerationSegment segment)
	{
	    var css = segment.Content.ToString();

	    if (css.IndexOf("@apply", StringComparison.Ordinal) < 0)
	        return ValueTask.CompletedTask;

	    var anyChange = false;
	    var sbOut = appRunner.StringBuilderPool.Get();
	    var prev = 0;

	    try
	    {
		    sbOut.EnsureCapacity(css.Length + (css.Length >> 3)); // small headroom

		    // Enumerate spans on the ORIGINAL css string (single pass only)
		    foreach (var span in css.EnumerateAtApplyStatements())
		    {
			    var spanStart = span.Index;
			    var spanEnd = span.Index + span.Full.Length; // exclusive

			    // 1) Copy text before this span (verbatim)
			    if (spanStart > prev)
				    sbOut.Append(css, prev, spanStart - prev);

			    // 2) Transform ALL @apply tokens inside span.Full (verbatim-preserving rewriter)
			    var transformed = TransformAllApplyTokensInsideSpan(appRunner, css.AsSpan(spanStart, spanEnd - spanStart), ref anyChange);

			    // 3) Append transformed span
			    sbOut.Append(transformed);

			    // 4) Advance prev to end of this span in the ORIGINAL css
			    prev = spanEnd;
		    }

		    // 5) Copy any trailing text after the last span
		    if (prev < css.Length)
			    sbOut.Append(css, prev, css.Length - prev);

		    // Replace once
		    if (anyChange)
			    segment.Content.ReplaceContent(sbOut);

		    return ValueTask.CompletedTask;
	    }
	    finally
	    {
		    appRunner.StringBuilderPool.Return(sbOut); // no change; just return builder
	    }
	    
	    static string TransformAllApplyTokensInsideSpan(AppRunner appRunner, ReadOnlySpan<char> spanText, ref bool anyChange)
	    {
	        // Stream through spanText, replacing *every* "@apply ..." token.
	        var sb = appRunner.StringBuilderPool.Get();

	        try
	        {
		        sb.EnsureCapacity(Math.Max(64, spanText.Length + 64)); // capacity hint

		        var i = 0;
			    var n = spanText.Length;
		        var inStr = false;
		        var q = '\0';
		        var inLine = false;
			    var inBlock = false;

			    while (i < n)
		        {
			        var c = spanText[i];

			        // line comment
			        if (inLine)
			        {
				        sb.Append(c);
				    
				        if (c == '\n') 
					        inLine = false;
				        
				        i++;
				        
				        continue;
			        }

			        // block comment
			        if (inBlock)
			        {
				        sb.Append(c);
				        
				        if (c == '*' && i + 1 < n && spanText[i + 1] == '/')
				        {
					        sb.Append('/');
					        i += 2;
				        }
				        else i++;

				        continue;
			        }

			        // string
			        if (inStr)
			        {
				        sb.Append(c);

				        if (c == '\\' && i + 1 < n)
				        {
					        sb.Append(spanText[i + 1]);
					        i += 2;
					        continue;
				        }

				        if (c == q)
				        {
					        inStr = false;
					        q = '\0';
				        }

				        i++;

				        continue;
			        }

			        // enter string/comment
			        if (c is '"' or '\'')
			        {
				        inStr = true;
				        q = c;
				        sb.Append(c);
				        i++;
				        
				        continue;
			        }

			        if (c == '/' && i + 1 < n)
			        {
				        if (spanText[i + 1] == '/')
				        {
					        inLine = true;
					        sb.Append("//");
					        i += 2;

					        continue;
				        }

				        if (spanText[i + 1] == '*')
				        {
					        inBlock = true;
					        sb.Append("/*");
					        i += 2;
					        
					        continue;
				        }
			        }

			        // look for "@apply"
			        if (c == '@')
			        {
				        const string kw = "@apply";
				        
				        if (i + kw.Length <= n && spanText.Slice(i, kw.Length).SequenceEqual(kw))
				        {
					        var k = i + kw.Length;

					        // Next must be CSS whitespace (avoid matching "@appliance")
					        if (k < n && IsCssWs(spanText[k]) == false)
					        {
						        sb.Append(c);
						        i++;

						        continue;
					        }

					        // skip whitespace after "@apply"
					        while (k < n && IsCssWs(spanText[k])) 
						        k++;

					        // collect args until ';' OR before a '}' that would close the current rule
					        var argsStart = k;
					        var scan = k;
					        var foundSemicolon = false;

					        while (scan < n)
					        {
						        var ch = spanText[scan];

						        if (ch is '"' or '\'')
						        {
							        // skip quoted bits inside args
							        var qq = ch;
							        
							        scan++;
							        
							        while (scan < n)
							        {
								        if (spanText[scan] == '\\' && scan + 1 < n)
								        {
									        scan += 2;
									        continue;
								        }

								        if (spanText[scan] == qq)
								        {
									        scan++;
									        break;
								        }

								        scan++;
							        }

							        continue;
						        }

						        if (ch == ';')
						        {
							        foundSemicolon = true;
							        break;
						        }

						        if (ch is '}' or '{')
							        break; // stop before closing/opening brace

						        scan++;
					        }

					        var argsEnd = scan;
					        var args = spanText.Slice(argsStart, Math.Max(0, argsEnd - argsStart));
					        var utils = ParseUtils(appRunner, args); // Build replacement declarations (keeps existing ordering rules)

					        if (utils.Count > 0)
					        {
						        foreach (var u in utils)
							        sb.Append(u.Styles); // declarations only ("prop: value;")

						        anyChange = true;
					        }
					        else
					        {
						        // Nothing recognized — keep original token text
						        var tokenLen = (foundSemicolon ? (argsEnd + 1) : argsEnd) - i;
						        
						        if (tokenLen > 0) 
							        sb.Append(spanText.Slice(i, tokenLen));
					        }

					        // Advance i past the token (consume ';' if present)
					        i = foundSemicolon ? argsEnd + 1 : argsEnd;

					        continue;
				        }
			        }

			        // default copy-through
			        sb.Append(c);
			        i++;
		        }

		        var result = sb.ToString();

		        return result;

		        static bool IsCssWs(char c) => c is ' ' or '\t' or '\r' or '\n' or '\f';
	        }
	        finally
	        {
		        appRunner.StringBuilderPool.Return(sb);
	        }
	    }

	    static bool IsCssWs(char c) => c is ' ' or '\t' or '\r' or '\n' or '\f';

	    static List<CssClass> ParseUtils(AppRunner appRunner, ReadOnlySpan<char> args)
	    {
	        var utils = new List<CssClass>();
	        var j = 0;

	        while (j < args.Length)
	        {
	            while (j < args.Length && IsCssWs(args[j])) 
		            j++;
	        
	            if (j >= args.Length) 
		            break;

	            var s = j;
	            
	            while (j < args.Length && !IsCssWs(args[j])) 
		            j++;

	            var tok = args.Slice(s, j - s);

	            // Zero-alloc unless a backslash exists
	            var name = UnescapeBackslashes(tok);
	            var cssClass = new CssClass(appRunner, selector: name);

	            if (cssClass.IsValid) 
		            utils.Add(cssClass);
	        }

	        if (utils.Count > 1)
	            utils.Sort((a, b) => a.SelectorSort.CompareTo(b.SelectorSort));

	        return utils;
	    }

	    // Allocates only if a backslash exists; otherwise returns tok.ToString() directly.
	    static string UnescapeBackslashes(ReadOnlySpan<char> tok)
	    {
	        var idx = tok.IndexOf('\\');
	        
	        if (idx < 0)
	            return tok.ToString();

	        var slashCount = 0;
	        
	        for (var t = idx; t < tok.Length; t++)
	            if (tok[t] == '\\') 
		            slashCount++;

	        var arr = new char[tok.Length - slashCount];
	        var w = 0;
	        
	        // ReSharper disable once ForCanBeConvertedToForeach
	        for (var t = 0; t < tok.Length; t++)
	        {
	            var ch = tok[t];
	            
	            if (ch != '\\') 
		            arr[w++] = ch;
	        }

	        return new string(arr, 0, w);
	    }
	}

	#endregion

	#region Process CSS Segment Functions

	/// <summary>
	/// Convert functions in a segment's CSS to equivalent statements (e.g. --alpha()).
	/// </summary>
	/// <param name="appRunner"></param>
	/// <param name="segment"></param>
	public static async ValueTask ProcessSegmentFunctionsAsync(this AppRunner appRunner, GenerationSegment segment)
    {
        var used = segment.UsedCssCustomProperties;
        var colors = appRunner.Library.ColorsByName;
        var pool = appRunner.StringBuilderPool;

        // 1) snapshot to string once
        var src = segment.Content.ToString();
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

        // 3) write back if changes
        if (output.Length != segment.Content.Length)
	        segment.Content.ReplaceContent(output);

        pool.Return(output);
        
        await Task.CompletedTask;
    }

    #endregion

    #region Process CSS Segment @variant Statements
	
    /// <summary>
    /// Convert @variant statements in source CSS to media query statements.
    /// </summary>
    /// <param name="appRunner"></param>
    /// <param name="segment"></param>
    public static async ValueTask ProcessSegmentAtVariantStatementsAsync(this AppRunner appRunner, GenerationSegment segment)
    {
	    foreach (var span in segment.Content.ToString().EnumerateAtVariantStatements())
	    {
		    if (appRunner.Library.MediaQueryPrefixes.TryGetValue(span.Name.ToString(), out var variantMetadata))
		    {
			    segment.Content.Replace(span.Full, $"@{variantMetadata.PrefixType} {variantMetadata.Statement} {{");
		    }
		    else if (appRunner.Library.SupportsQueryPrefixes.TryGetValue(span.Name.ToString(), out variantMetadata))
		    {
			    segment.Content.Replace(span.Full, $"@{variantMetadata.PrefixType} {variantMetadata.Statement} {{");
		    }
	    }
		
	    await Task.CompletedTask;
    }
	
    #endregion

    
    
	#region Process CSS Segment Dark Theme Classes
	
	/// <summary>
	/// Find all dark theme media blocks and duplicate as wrapped classes theme-dark
	/// </summary>
	/// <param name="appRunner"></param>
	/// <param name="segment"></param>
	/// <returns></returns>
    public static async ValueTask ProcessDarkThemeClassesAsync(this AppRunner appRunner, GenerationSegment segment)
    {
        const string mediaPrefix = "@media (prefers-color-scheme: dark) {";

        if (appRunner.AppRunnerSettings.UseDarkThemeClasses == false)
            return;

        var css = segment.Content.ToString();
        var outCss = new StringBuilder(css.Length * 2);
        var pos = 0;
        var lineBreak = appRunner.AppRunnerSettings.LineBreak;

        while (true)
        {
            // Find next dark-mode media block
            var mediaIdx = css.IndexOf(mediaPrefix, pos, StringComparison.Ordinal);

            if (mediaIdx < 0)
            {
                outCss.Append(css, pos, css.Length - pos);
                break;
            }

            // Copy everything before the media query
            outCss.Append(css, pos, mediaIdx - pos);

            // Brace-match the media body to capture its inner content
            var bodyStart = mediaIdx + mediaPrefix.Length;
            var p = bodyStart;
            var depth = 1;

            while (p < css.Length && depth > 0)
            {
                var ch = css[p++];
            
                if (ch == '{')
	                depth++;
                else if (ch == '}')
	                depth--;
            }
            
            var bodyEnd = p - 1;
            var inner = css.AsSpan(bodyStart, bodyEnd - bodyStart);

            var insideSelector = IsInsideSelector(css, mediaIdx);

            if (insideSelector)
            {
                // Nested in a selector: use &:where(...) wrappers
                outCss.Append(mediaPrefix).Append(lineBreak)
                      .Append("&:where(.theme-auto, .theme-auto *) {")
                      .Append(inner)
                      .Append('}')
                      .Append('}')
                      .Append(lineBreak).Append(lineBreak);

                outCss.Append("&:where(.theme-dark, .theme-dark *) {")
                      .Append(inner)
                      .Append('}')
                      .Append(lineBreak).Append(lineBreak);
            }
            else
            {
                // Not in a selector (root or only at-rules): append :where(...) to each selector
                outCss.Append(mediaPrefix).Append(lineBreak);

                var tmp = appRunner.StringBuilderPool.Get();
                
                RewriteRootInner(inner, tmp, sel => AppendWhere(sel, ".theme-auto"));
                
                outCss.Append(tmp);
                outCss.Append('}').Append(lineBreak).Append(lineBreak);
                appRunner.StringBuilderPool.Return(tmp);
	                
                var tmp2 = appRunner.StringBuilderPool.Get();

                RewriteRootInner(inner, tmp2, sel => AppendWhere(sel, ".theme-dark"));
                
                outCss.Append(tmp2);
                outCss.Append(lineBreak).Append(lineBreak);
                appRunner.StringBuilderPool.Return(tmp);
            }

            pos = bodyEnd + 1;
        }

        // only replace it if changed
        if (outCss.Length != segment.Content.Length)
            segment.Content.ReplaceContent(outCss);

        await Task.CompletedTask;

        return;

        // Rewrites each selector in `inner` by appending :where(themeClass, themeClass *)
        // Used when the dark media is not inside a selector rule.
        static void RewriteRootInner(ReadOnlySpan<char> inner, StringBuilder dst, Func<string, string> selectorRewriter)
        {
	        var i = 0;

	        while (i < inner.Length)
	        {
		        while (i < inner.Length && char.IsWhiteSpace(inner[i]))
		        {
			        dst.Append(inner[i]);
			        i++;
		        }
		    
		        if (i >= inner.Length) 
			        break;

		        var rel = inner[i..].IndexOf('{');
		        
		        if (rel < 0)
		        {
			        dst.Append(inner[i..]);
			        break;
		        }

		        var headerSpan = inner.Slice(i, rel);
		        var contentStart = i + rel + 1;

		        // brace-match this block
		        var depth = 1;
		        var p = contentStart;
		        
		        while (p < inner.Length && depth > 0)
		        {
			        if (inner[p] == '{')
				        depth++;
			        else if (inner[p] == '}')
				        depth--;
			        
			        p++;
		        }
		        
		        var contentEnd = p - 1;
		        var ruleContent = inner.Slice(contentStart, contentEnd - contentStart);
		        var trimmed = headerSpan.TrimStart();

		        if (trimmed.Length > 0 && trimmed[0] == '@')
		        {
			        // keep nested @-rules verbatim and recurse
			        dst.Append(headerSpan).Append('{');
			        RewriteRootInner(ruleContent, dst, selectorRewriter);
			        dst.Append('}');
		        }
		        else
		        {
			        var selectors = new List<string>();
			        
			        AddSelectors(headerSpan, selectors);

			        var first = true;
			        
			        foreach (var sel in selectors)
			        {
				        if (first == false)
					        dst.Append(", ");
				        
				        first = false;
				        dst.Append(selectorRewriter(sel));
			        }

			        dst.Append(" {").Append(ruleContent).Append('}');
		        }

		        i = contentEnd + 1;
	        }
        }

        static string AppendWhere(string sel, string themeClass) => $"{sel}:where({themeClass}, {themeClass} *)";

        // True if the media block is nested inside ANY selector rule (class/id/tag),
        // false if it's only inside at-rules (@layer/@supports/...) or at the root.
        static bool IsInsideSelector(string s, int mediaIdx)
        {
	        var scan = mediaIdx - 1;

	        while (true)
	        {
		        var open = FindEnclosingOpenBrace(s, scan);
		        
		        if (open < 0) 
			        return false; // no container => root-like

		        var headerStart = FindHeaderStart(s, open - 1);
		        var header = s.AsSpan(headerStart, open - headerStart).Trim();

		        if (header.Length == 0) 
			        return false;

		        // At-rule containers begin with '@'; keep climbing outwards.
		        if (header[0] == '@')
		        {
			        scan = headerStart - 1;
			        continue;
		        }

		        // Otherwise we have a selector (e.g., .foo, #bar, button, etc.)
		        return true;
	        }
        }

        // Given the index of an opening brace '{', find the start index of its header text
        // by scanning back to the previous '}' or start of string.
        static int FindHeaderStart(string s, int before)
        {
	        for (var i = before; i >= 0; i--)
	        {
		        if (s[i] == '}') 
			        return i + 1;
	        }
	     
	        return 0;
        }

        // Walk backward from `from` to find the opening brace '{' that encloses that position.
        static int FindEnclosingOpenBrace(string s, int from)
        {
	        var depth = 0;

	        for (var i = from; i >= 0; i--)
	        {
		        var c = s[i];

		        if (c == '}')
		        {
			        depth++;
		        }
		        else if (c == '{')
		        {
			        if (depth == 0) 
				        return i;

			        depth--;
		        }
	        }

	        return -1;
        }
    }

    private static void AddSelectors(ReadOnlySpan<char> header, List<string> output)
    {
        var paren    = 0;
        var square   = 0;
        var start    = 0;
        var inQuote  = false;
        var escaped  = false;
        var quoteCh  = '\0';

        for (var idx = 0; idx < header.Length; idx++)
        {
            var c = header[idx];

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
                if (c == quoteCh)
	                inQuote = false;
                
                continue;
            }
            
            if (c is '"' or '\'')
            {
                inQuote = true;
                quoteCh = c;
                continue;
            }

            switch (c)
            {
                case '(': paren++; break;
                case ')': if (paren > 0) paren--; break;
                case '[': square++; break;
                case ']': if (square > 0) square--; break;
                case ',' when paren == 0 && square == 0:
                    
	                var sel = header.Slice(start, idx - start).Trim();

                    if (sel.IsEmpty == false)
	                    output.Add(sel.ToString());
                    
                    start = idx + 1;
                    
                    break;
            }
        }

        var tail = header[start..].Trim();

        if (tail.IsEmpty == false)
	        output.Add(tail.ToString());
    }
	
	#endregion

	#region Gather CSS Segment Custom Propery References

	/// <summary>
	/// Scan a CSS string for custom property references (e.g. --sf-color-primary)
	/// </summary>
	/// <param name="css"></param>
	public static HashSet<string> GatherCssCustomProperties(this string css)
	{
		// 1) materialize the content
		var used = new HashSet<string>(StringComparer.Ordinal);
		var length = css.Length;
		var i = 0;

		// 2) scan for “--name” + delimiter
		while (true)
		{
			// find the next “--”
			var idx = css.IndexOf("--", i, StringComparison.Ordinal);
			
			if (idx < 0) 
				break;

			i = idx + 2;

			// consume [A-Za-z0-9_-]+
			while (i < length)
			{
				var c = css[i];

				if (c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '-' or '_')
				{
					i++;
				}
				else
				{
					break;
				}
			}

			// must be more than just “--”
			var nameLen = i - idx;
			
			if (nameLen <= 2)
				continue;

			// skip any whitespace
			while (i < length)
			{
				var c = css[i];

				if (c is ' ' or '\t' or '\r' or '\n')
					i++;
				else
					break;
			}
			
			if (i >= length) 
				break;

			// only accept var-refs ")" or "," or declarations ":"
			var delim = css[i];
			
			if (delim != ')' && delim != ',' && delim != ':') 
				continue;

			// Save item
			used.Add(css.Substring(idx, nameLen));

			// continue scanning after the name
			i = idx + nameLen;
		}

		return used;
	}
	
	/// <summary>
	/// Scan a CSS segment for custom property references (e.g. --sf-color-primary)
	/// and add them to the segment's UsedCssCustomProperties.
	/// </summary>
	/// <param name="appRunner"></param>
	/// <param name="segment"></param>
	public static void GatherSegmentCssCustomPropertyRefs(this AppRunner appRunner, GenerationSegment segment)
	{
		var settings = appRunner.AppRunnerSettings.SfumatoBlockItems;
		var used = segment.UsedCssCustomProperties;

		foreach (var item in segment.Content.ToString().GatherCssCustomProperties())
		{
			if (settings.TryGetValue(item, out var val))
				used.TryAdd(item, val);
		}
	}

	#endregion

	#region Gather CSS Segment Used CSS (@keyframes)

	/// <summary>
	/// Scan a CSS segment for custom property references (e.g. --sf-color-primary)
	/// and add them to the segment's UsedCssCustomProperties.
	/// </summary>
	/// <param name="appRunner"></param>
	/// <param name="segment"></param>
	public static async ValueTask GatherSegmentUsedCssRefsAsync(this AppRunner appRunner, GenerationSegment segment)
	{
		if (segment.UsedCssCustomProperties.Count <= 0)
			return;
	    
		foreach (var ccp in segment.UsedCssCustomProperties.Where(c => c.Key.StartsWith("--sf-") == false && c.Key.StartsWith("--form-") == false && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
		{
			if (ccp.Key.StartsWith("--animate-", StringComparison.Ordinal) == false)
				continue;
					
			var key = $"@keyframes {MemoryExtensions.TrimStart(ccp.Key, "--animate-")}";

			if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(key, out var value))
				segment.UsedCss.TryAdd(key, value);
		}

		await Task.CompletedTask;
	}

	#endregion

	#region Merge CSS Segments Dependencies
	
	public static void MergeUsedDependencies(this AppRunner appRunner)
	{
		appRunner.UsedCss.Clear();
	
		appRunner.UsedCss.AddRange(appRunner.DefaultsCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.BrowserResetCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.FormsCssSegment.UsedCss);

		appRunner.UsedCss.AddRange(appRunner.ImportsCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.SfumatoSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.ComponentsCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.CustomCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.UtilitiesCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.ThemeCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.PropertiesCssSegment.UsedCss);
		appRunner.UsedCss.AddRange(appRunner.PropertyListCssSegment.UsedCss);

		appRunner.UsedCssCustomProperties.Clear();
	
		appRunner.UsedCssCustomProperties.AddRange(appRunner.DefaultsCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.BrowserResetCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.FormsCssSegment.UsedCssCustomProperties);

		appRunner.UsedCssCustomProperties.AddRange(appRunner.ImportsCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.SfumatoSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.ComponentsCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.CustomCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.UtilitiesCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.ThemeCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.PropertiesCssSegment.UsedCssCustomProperties);
		appRunner.UsedCssCustomProperties.AddRange(appRunner.PropertyListCssSegment.UsedCssCustomProperties);
	}
	
	#endregion
	
	
	
	#region Generate @properties Layer / @property Rules
	
	/// <summary>
	/// Iterate UsedCssCustomProperties[] and UsedCss[] and set their values from AppRunnerSettings.SfumatoBlockItems[].
	/// </summary>
	/// <param name="appRunner"></param>
	private static void ProcessTrackedDependencyValues(this AppRunner appRunner)
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
					
				foreach (var item in value.GatherCssCustomProperties())
				{
					if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(item, out var valueValue))
					{
						appRunner.UsedCssCustomProperties.TryAdd(item, valueValue);
						
						foreach (var item2 in valueValue.GatherCssCustomProperties())
						{
							if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(item2, out var valueValue2))
								appRunner.UsedCssCustomProperties.TryAdd(item2, valueValue2);
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
					
				foreach (var item in value.GatherCssCustomProperties())
				{
					if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(item, out var valueValue))
						appRunner.UsedCssCustomProperties.TryAdd(item, valueValue);
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"{Constants.CliErrorPrefix}ProcessTrackedDependencyValues() => {e.Message}");
			Environment.Exit(1);
		}
	}
	
	/// <summary>
	/// Generate @properties Layer / @property Rules
	/// </summary>
	/// <param name="appRunner"></param>
	/// <returns></returns>
	public static void GeneratePropertiesAndThemeLayers(this AppRunner appRunner)
	{
		appRunner.ProcessTrackedDependencyValues();

		var collection = appRunner.UsedCssCustomProperties.OrderBy(i => i.Key).ToList();
		
		if (appRunner.UsedCssCustomProperties.Count > 0)
		{
			#region @layer properties
			
			appRunner.PropertiesCssSegment.Content
				.Append("*, ::before, ::after, ::backdrop {")
				.Append(appRunner.AppRunnerSettings.LineBreak);

			foreach (var ccp in collection.Where(c => (c.Key.StartsWith("--sf-") || c.Key.StartsWith("--form-")) && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
			{
				if (appRunner.AppRunnerSettings.UseForms == false && ccp.Key.StartsWith("--form-"))
					continue;
				
				appRunner.PropertiesCssSegment.Content
					.Append(ccp.Key)
					.Append(": ")
					.Append(ccp.Value)
					.Append(';');
			}

			appRunner.PropertiesCssSegment.Content
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
					
					appRunner.PropertyListCssSegment.Content
						.Append($"@property {ccp.Key} ")
						.Append(prop)
						.Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}				

			#endregion
			
			#region @layer theme
			
			appRunner.ThemeCssSegment.Content
				.Append(":root, :host {")
				.Append(appRunner.AppRunnerSettings.LineBreak)
				.Append(appRunner.AppRunnerSettings.LineBreak);
		
			foreach (var ccp in collection.Where(c => c.Key.StartsWith("--sf-") == false && c.Key.StartsWith("--form-") == false && string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
			{
				appRunner.ThemeCssSegment.Content
					.Append(ccp.Key)
					.Append(": ")
					.Append(ccp.Value)
					.Append(';');
				
				if (ccp.Key.StartsWith("--animate-", StringComparison.Ordinal) == false)
					continue;
					
				var key = $"@keyframes {MemoryExtensions.TrimStart(ccp.Key, "--animate-")}";

				if (appRunner.AppRunnerSettings.SfumatoBlockItems.TryGetValue(key, out var value))
					appRunner.UsedCss.TryAdd(key, value);
			}

			appRunner.ThemeCssSegment.Content
				.Append('}')
				.Append(appRunner.AppRunnerSettings.LineBreak)
				.Append(appRunner.AppRunnerSettings.LineBreak);	
			
			#endregion
			
			#region @keyframes, etc.
			
			if (appRunner.UsedCss.Count > 0)
			{
				foreach (var ccp in appRunner.UsedCss.Where(c => string.IsNullOrEmpty(c.Value) == false).OrderBy(i => i.Key))
				{
					appRunner.PropertyListCssSegment.Content
						.Append(ccp.Key)
						.Append(' ')
						.Append(ccp.Value);
				}
			}
			
			#endregion
		}
	}
	
	#endregion
	

	
	#region Build CSS
	
	public static async Task<string> FullBuildCssAsync(AppRunner appRunner)
	{
		appRunner.CustomCssSegment.Content.ReplaceContent(appRunner.CssContentWithoutSettings);

		var (index, length) = appRunner.CustomCssSegment.Content.ExtractCssImportStatements(appRunner, true);

		if (index > -1)
			appRunner.CustomCssSegment.Content.Remove(index, length);
		
		appRunner.ProcessAllComponentsLayersAndCss();
		appRunner.GenerateUtilityClasses();

		await appRunner.ProcessSegmentAtApplyStatementsAsync(appRunner.ImportsCssSegment);
		await appRunner.ProcessSegmentAtApplyStatementsAsync(appRunner.ComponentsCssSegment);
		await appRunner.ProcessSegmentAtApplyStatementsAsync(appRunner.CustomCssSegment);

		await appRunner.ProcessSegmentFunctionsAsync(appRunner.BrowserResetCssSegment);
		await appRunner.ProcessSegmentFunctionsAsync(appRunner.FormsCssSegment);
		await appRunner.ProcessSegmentFunctionsAsync(appRunner.UtilitiesCssSegment);
		await appRunner.ProcessSegmentFunctionsAsync(appRunner.ImportsCssSegment);
		await appRunner.ProcessSegmentFunctionsAsync(appRunner.ComponentsCssSegment);
		await appRunner.ProcessSegmentFunctionsAsync(appRunner.CustomCssSegment);

		await appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.UtilitiesCssSegment);
		await appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.ImportsCssSegment);
		await appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.ComponentsCssSegment);
		await appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.CustomCssSegment);

		if (appRunner.AppRunnerSettings.UseDarkThemeClasses)
		{
			await appRunner.ProcessDarkThemeClassesAsync(appRunner.UtilitiesCssSegment);
			await appRunner.ProcessDarkThemeClassesAsync(appRunner.ImportsCssSegment);
			await appRunner.ProcessDarkThemeClassesAsync(appRunner.ComponentsCssSegment);
			await appRunner.ProcessDarkThemeClassesAsync(appRunner.CustomCssSegment);
		}

		#region Gather And Merge Dependencies

		appRunner.PropertiesCssSegment.Content.Clear();
		appRunner.PropertyListCssSegment.Content.Clear();
		appRunner.ThemeCssSegment.Content.Clear();

		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.BrowserResetCssSegment);
		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.FormsCssSegment);
		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.UtilitiesCssSegment);
		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.ImportsCssSegment);
		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.ComponentsCssSegment);
		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.CustomCssSegment);

		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.BrowserResetCssSegment);
		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.FormsCssSegment);
		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.UtilitiesCssSegment);
		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.ImportsCssSegment);
		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.ComponentsCssSegment);
		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.CustomCssSegment);
		
		appRunner.MergeUsedDependencies();
		appRunner.GeneratePropertiesAndThemeLayers();
		
		#endregion

		appRunner.FinalCssAssembly();

		return appRunner.GenerateFinalCss();
	}

	public static async Task<string> ProjectChangeBuildCssAsync(AppRunner appRunner)
	{
		appRunner.GenerateUtilityClasses();

		await appRunner.ProcessSegmentFunctionsAsync(appRunner.UtilitiesCssSegment);
		await appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.UtilitiesCssSegment);

		if (appRunner.AppRunnerSettings.UseDarkThemeClasses)
		{
			await appRunner.ProcessDarkThemeClassesAsync(appRunner.UtilitiesCssSegment);
		}

		#region Gather And Merge Dependencies

		appRunner.PropertiesCssSegment.Content.Clear();
		appRunner.PropertyListCssSegment.Content.Clear();
		appRunner.ThemeCssSegment.Content.Clear();

		appRunner.GatherSegmentCssCustomPropertyRefs(appRunner.UtilitiesCssSegment);

		await appRunner.GatherSegmentUsedCssRefsAsync(appRunner.UtilitiesCssSegment);
		
		appRunner.MergeUsedDependencies();
		appRunner.GeneratePropertiesAndThemeLayers();
		
		#endregion

		appRunner.FinalCssAssembly();

		return appRunner.GenerateFinalCss();
	}
	
	public static void FinalCssAssembly(this AppRunner appRunner)
	{
		appRunner.CombinedSegmentCss.Clear();
			
		var useLayers = appRunner.AppRunnerSettings.UseCompatibilityMode == false;

		if (useLayers)
		{
			appRunner.CombinedSegmentCss.Append("@layer properties, theme, base, forms, components, utilities;");
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			
			appRunner.CombinedSegmentCss.Append("@layer theme {");
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);

			appRunner.CombinedSegmentCss.Append(appRunner.ThemeCssSegment.Content);
			
			appRunner.CombinedSegmentCss.Append('}');
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}
		else
		{
			appRunner.CombinedSegmentCss.Append(appRunner.ThemeCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		if (appRunner.AppRunnerSettings.UseReset)
		{
			if (useLayers)
			{
				appRunner.CombinedSegmentCss.Append("@layer base {");
				appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			
				appRunner.CombinedSegmentCss.Append(appRunner.BrowserResetCssSegment.Content);
				
				appRunner.CombinedSegmentCss.Append('}');
				appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			}
			else
			{
				appRunner.CombinedSegmentCss.Append(appRunner.BrowserResetCssSegment.Content);
				appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			}
		}

		if (appRunner.AppRunnerSettings.UseForms)
		{
			if (useLayers)
			{
				appRunner.CombinedSegmentCss.Append("@layer forms {");
				appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			
				appRunner.CombinedSegmentCss.Append(appRunner.FormsCssSegment.Content);
				
				appRunner.CombinedSegmentCss.Append('}');
				appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			}
			else
			{
				appRunner.CombinedSegmentCss.Append(appRunner.FormsCssSegment.Content);
				appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			}
		}

		if (useLayers && appRunner.ComponentsCssSegment.Content.Length > 0)
		{
			appRunner.CombinedSegmentCss.Append("@layer components {");
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			
			appRunner.CombinedSegmentCss.Append(appRunner.ComponentsCssSegment.Content);
				
			appRunner.CombinedSegmentCss.Append('}');
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}
		else
		{
			appRunner.CombinedSegmentCss.Append(appRunner.ComponentsCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		if (useLayers)
		{
			appRunner.CombinedSegmentCss.Append("@layer utilities {");
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			
			appRunner.CombinedSegmentCss.Append(appRunner.UtilitiesCssSegment.Content);
				
			appRunner.CombinedSegmentCss.Append('}');
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}
		else
		{
			appRunner.CombinedSegmentCss.Append(appRunner.UtilitiesCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		if (appRunner.ImportsCssSegment.Content.Length > 0)
		{
			appRunner.CombinedSegmentCss.Append(appRunner.ImportsCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		if (appRunner.CustomCssSegment.Content.Length > 0)
		{
			appRunner.CombinedSegmentCss.Append(appRunner.CustomCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		if (appRunner.PropertyListCssSegment.Content.Length > 0)
		{
			appRunner.CombinedSegmentCss.Append(appRunner.PropertyListCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}

		if (useLayers)
		{
			appRunner.CombinedSegmentCss.Append("@layer properties {");
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
			
			appRunner.CombinedSegmentCss.Append(appRunner.PropertiesCssSegment.Content);
				
			appRunner.CombinedSegmentCss.Append('}');
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}
		else
		{
			appRunner.CombinedSegmentCss.Append(appRunner.PropertiesCssSegment.Content);
			appRunner.CombinedSegmentCss.Append(appRunner.AppRunnerSettings.LineBreak);
		}
	}

	public static string GenerateFinalCss(this AppRunner appRunner)
	{
		var workingSb = appRunner.StringBuilderPool.Get();

		try
		{
			return appRunner.AppRunnerSettings.UseMinify ? appRunner.CombinedSegmentCss.ToString().CompactCss(workingSb) : appRunner.CombinedSegmentCss.ReformatCss().ToString().NormalizeLinebreaks(appRunner.AppRunnerSettings.LineBreak);
		}
		finally
		{
			appRunner.StringBuilderPool.Return(workingSb);
		}
	}
	
	#endregion 
}