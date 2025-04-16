// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

using System.Reflection;
using Argentini.Sfumato.Entities.CssClassProcessing;
using Argentini.Sfumato.Entities.Library;
using Argentini.Sfumato.Entities.Scanning;
using Argentini.Sfumato.Entities.UtilityClasses;

namespace Argentini.Sfumato;

public partial class AppRunner
{
	#region Regular Expressions
	
	[GeneratedRegex(@"@apply\s+[^;]+?;")]
	private static partial Regex AtApplyRegex();
	
	[GeneratedRegex(@"--[\w-]+")]
	private static partial Regex CssCustomPropertiesRegex();
	
	[GeneratedRegex(@"@variant\s*([\w-]+)\s*{")]
	private static partial Regex AtVariantRegex();	
	
	[GeneratedRegex(@"(?:\r\n|\n){3,}")]
	private static partial Regex ConsolidateLineBreaksRegex();
	
	#endregion
	
	#region Properties

	public AppState AppState { get; }
	public Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; } = new(null);
	public Dictionary<string,ScannedFile> ScannedFiles { get; set; } = new(StringComparer.Ordinal);
	public Dictionary<string,string> UsedCssCustomProperties { get; set; } = new(StringComparer.Ordinal);
	public Dictionary<string,string> UsedCss { get; set; } = new(StringComparer.Ordinal);
	public Dictionary<string,CssClass> UtilityClasses { get; set; } = new(StringComparer.Ordinal);

	private readonly string _cssFilePath;
	private readonly bool _useMinify;
	
    #endregion

    #region Construction
    
    public AppRunner(AppState appState, string cssFilePath = "", bool useMinify = false)
    {
	    AppState = appState;

	    _cssFilePath = cssFilePath;
	    _useMinify = useMinify;

	    Initialize();
    }

    #endregion
    
    #region Process Settings

    /// <summary>
    /// Clears AppRunnerSettings and loads default settings from defaults.css.
    /// </summary>
    public void Initialize()
    {
	    try
	    {
		    AppRunnerSettings = new AppRunnerSettings(this);
		    AppRunnerSettings.ExtractSfumatoItems(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "defaults.css")));

		    ProcessCssSettings();
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}Initialize() - {e.Message}");
		    Environment.Exit(1);
	    }
    }

    /// <summary>
    /// Loads the CSS file, imports partials, extracts the Sfumato settings block, and processes it.
    /// </summary>
    public async Task LoadCssFileAsync()
    {
	    try
	    {
		    AppRunnerSettings.CssFilePath = _cssFilePath;
		    AppRunnerSettings.UseMinify = _useMinify;

		    AppRunnerSettings.LoadAndExtractCssContent(); // Extract Sfumato settings and CSS content
		    AppRunnerSettings.ExtractSfumatoItems(); // Parse all the Sfumato settings into a Dictionary<string,string>()
		    AppRunnerSettings.ProcessProjectSettings(); // Read project/operation settings
		    AppRunnerSettings.ImportPartials(); // Read in all CSS partial files (@import "...")

		    ProcessCssSettings();
	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}LoadCssFileAsync() - {e.Message}");
		    Environment.Exit(1);
	    }
    }

    /// <summary>
    /// Processes CSS settings for colors and breakpoints, and uses reflection to load all others per utility class file.  
    /// </summary>
    public void ProcessCssSettings()
    {
	    #region Read color definitions
	    
	    foreach (var color in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--color-")))
	    {
		    var key = color.Key.TrimStart("--color-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (Library.ColorsByName.TryAdd(key, color.Value) == false)
			    Library.ColorsByName[key] = color.Value;
	    }
	    
	    #endregion

	    #region Read breakpoints

	    var prefixOrder = 100;

	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (breakpoint.Key.StartsWith("--breakpoint-") == false)
			    continue;
		    
		    var key = breakpoint.Key.TrimStart("--breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(width >= {breakpoint.Value})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[key] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(width >= {breakpoint.Value})"
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (Library.MediaQueryPrefixes.TryAdd($"max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(width < {breakpoint.Value})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[$"max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(width < {breakpoint.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }
	    
	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems)
	    {
		    if (breakpoint.Key.StartsWith("--adaptive-breakpoint-") == false)
			    continue;

		    var key = breakpoint.Key.TrimStart("--adaptive-breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (double.TryParse(breakpoint.Value, out var maxValue) == false)
			    continue;

		    if (Library.MediaQueryPrefixes.TryAdd(key, new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(min-aspect-ratio: {breakpoint.Value})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[key] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(min-aspect-ratio: {breakpoint.Value})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (Library.MediaQueryPrefixes.TryAdd($"max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "media",
			        Statement = $"(max-aspect-ratio: {maxValue - 0.000000000001})"
		        }) == false)
		    {
			    Library.MediaQueryPrefixes[$"max-{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "media",
				    Statement = $"(max-aspect-ratio: {maxValue - 0.000000000001})"
			    };
		    }
		    
		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;
	    }

	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
	    {
		    var key = breakpoint.Key.TrimStart("--container-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (Library.ContainerQueryPrefixes.TryAdd($"@{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width >= {breakpoint.Value})"
		        }) == false)
		    {
			    Library.ContainerQueryPrefixes[$"@{key}"] = new VariantMetadata
			    {
				    PrefixOrder = prefixOrder,
				    PrefixType = "container",
				    Statement = $"(width >= {breakpoint.Value})"
			    };
		    }

		    if (prefixOrder < int.MaxValue - 100)
			    prefixOrder += 100;

		    if (Library.ContainerQueryPrefixes.TryAdd($"@max-{key}", new VariantMetadata
		        {
			        PrefixOrder = prefixOrder,
			        PrefixType = "container",
			        Statement = $"(width < {breakpoint.Value})"
		        }) == false)
		    {
			    Library.ContainerQueryPrefixes[$"@max-{key}"] = new VariantMetadata
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
	    
		#region Read theme settings from ClassDictionary instances (e.g. --text-xs, etc.)
	    
	    var derivedTypes = Assembly.GetExecutingAssembly()
		    .GetTypes()
		    .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

	    foreach (var type in derivedTypes)
	    {
		    if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
			    continue;
		    
		    instance.ProcessThemeSettings(this);
	    }
	    
	    #endregion
    }

	#endregion

	// todo: scan file paths for utility classes  

	// todo: watchers

	#region CSS Generation
    
    /// <summary>
    /// Gather dependencies from all scanned files, consolidate them, and generate the CSS output file.
    /// </summary>
    public string BuildCss()
	{
		var outputCss = AppState.StringBuilderPool.Get();
		var utilityCss = AppState.StringBuilderPool.Get();
		var workingSb = AppState.StringBuilderPool.Get();

		try
		{
			UsedCssCustomProperties.Clear();
			UsedCss.Clear();

			#region Gather scanned file utility class dependencies lists

			foreach (var utilityClass in ScannedFiles.SelectMany(scannedFile => scannedFile.Value.UtilityClasses))
			{
				UtilityClasses.TryAdd(utilityClass.Key, utilityClass.Value);
					
				foreach (var dependency in utilityClass.Value.ClassDefinition?.UsesCssCustomProperties ?? [])
				{
					if (dependency.StartsWith("--", StringComparison.Ordinal))
						UsedCssCustomProperties.TryAddUpdate(dependency, string.Empty);
					else
						UsedCss.TryAddUpdate(dependency, string.Empty);
				}
			}

			#endregion

			#region Inject optional reset
			
			if (AppRunnerSettings.UseReset)
			{
				outputCss.Append(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "browser-reset.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak).Trim())
					.Append(AppRunnerSettings.LineBreak)
					.Append(AppRunnerSettings.LineBreak);
			}

			#endregion

			#region Inject optional forms classes
			
			if (AppRunnerSettings.UseForms)
			{
				outputCss.Append(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "forms.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak).Trim())
					.Append(AppRunnerSettings.LineBreak)
					.Append(AppRunnerSettings.LineBreak);
			}

			#endregion

			#region Add temp marker for utility class content
			
			outputCss.Append("::sfumato{}")
				.Append(AppRunnerSettings.LineBreak)
				.Append(AppRunnerSettings.LineBreak);

			#endregion
			
			#region Inject processed source CSS
			
			outputCss.Append(AppRunnerSettings.ProcessedCssContent.Trim());

			#endregion
			
			#region Process @apply usage (and dependencies)

			foreach (var match in AtApplyRegex().Matches(outputCss.ToString()).ToList())
			{
				var utilityClassStrings = (match.Value.TrimStart("@apply")?.TrimEnd(';').Trim() ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var utilityClasses = utilityClassStrings
					.Select(utilityClass => new CssClass(this, utilityClass.Replace("\\", string.Empty)))
					.Where(cssClass => cssClass.IsValid)
					.ToList();

				if (utilityClasses.Count > 0)
				{
					workingSb.Clear();

					foreach (var utilityClass in utilityClasses.OrderBy(c => c.SelectorSort))
					{
						foreach (var dependency in utilityClass.ClassDefinition?.UsesCssCustomProperties ?? [])
						{
							if (dependency.StartsWith("--", StringComparison.Ordinal))
								UsedCssCustomProperties.TryAddUpdate(dependency, string.Empty);
							else
								UsedCss.TryAddUpdate(dependency, string.Empty);
						}
						
						if (workingSb.Length > 0)
							workingSb.Append(' ');

						workingSb.Append(utilityClass.Styles.Replace(AppRunnerSettings.LineBreak, " "));
					}
				}

				outputCss.Replace(match.Value, workingSb.ToString());
			}

			#endregion
			
			#region Process @variant in CSS source

			foreach (var match in AtVariantRegex().Matches(outputCss.ToString()).ToList())
			{
				var segment = match.Value
					.Replace("@variant", string.Empty, StringComparison.Ordinal)
					.Replace("{", string.Empty, StringComparison.Ordinal)
					.Trim();
				
				if (segment.TryVariantIsMediaQuery(this, out var variant))
				{
					outputCss.Replace(match.Value, $"@{variant?.PrefixType} {variant?.Statement} {{");
				}
			}

			#endregion

			// todo: process functions, like --alpha()
			
			#region Add referenced CSS custom properties used in CSS source

			foreach (var match in CssCustomPropertiesRegex().Matches(outputCss.ToString()).ToList())
			{
				if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(match.Value, out var value))
					UsedCssCustomProperties.TryAdd(match.Value, value);
			}

			#endregion

			#region Add values to all tracked CSS custom property dependencies, process values as well
			
			foreach (var usedCssCustomProperty in UsedCssCustomProperties.ToList())
			{
				if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(usedCssCustomProperty.Key, out var value) == false)
					continue;
				
				UsedCssCustomProperties[usedCssCustomProperty.Key] = value;

				if (value.Contains("--") == false)
					continue;
				
				foreach (var match in CssCustomPropertiesRegex().Matches(value).ToList())
				{
					if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(match.Value, out var valueValue))
						UsedCssCustomProperties.TryAdd(match.Value, valueValue);
				}
			}

			foreach (var usedCss in UsedCss.ToList())
			{
				if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(usedCss.Key, out var value) == false)
					continue;
				
				UsedCss[usedCss.Key] = value;
					
				if (value.Contains("--") == false)
					continue;
				
				foreach (var match in CssCustomPropertiesRegex().Matches(value).ToList())
				{
					if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(match.Value, out var valueValue))
						UsedCssCustomProperties.TryAdd(match.Value, valueValue);
				}
			}

			#endregion

			#region Build consolidated variant structure, generate utility CSS

			var root = new VariantBranch
			{
				Fingerprint = 0,
				Depth = 0,
				WrapperCss = string.Empty
			};

			foreach (var cssClass in UtilityClasses.Values.OrderBy(c => c.WrapperSort))
			{
				var wrappers = cssClass.Wrappers.ToArray();

				ProcessVariantBranchRecursive(root, wrappers, cssClass);
			}			

			GenerateCssFromVariantTree(root, utilityCss);

			#endregion

			#region Build root dependencies, inject at top of generated CSS

			workingSb.Clear();
			
			if (UsedCssCustomProperties.Count > 0)
			{
				workingSb.Append(":root {").Append(AppRunnerSettings.LineBreak);

				foreach (var ccp in UsedCssCustomProperties.Where(c => string.IsNullOrEmpty(c.Value) == false).OrderBy(c => c.Key))
					workingSb.Append(AppRunnerSettings.Indentation).Append(ccp.Key).Append(": ").Append(ccp.Value).Append(';').Append(AppRunnerSettings.LineBreak);

				workingSb.Append('}').Append(AppRunnerSettings.LineBreak).Append(AppRunnerSettings.LineBreak);
			}

			if (UsedCss.Count > 0)
			{
				foreach (var ccp in UsedCss.Where(c => string.IsNullOrEmpty(c.Value) == false))
					workingSb.Append(ccp.Key).Append(' ').Append(ccp.Value).Append(AppRunnerSettings.LineBreak);

				workingSb.Append(AppRunnerSettings.LineBreak);
			}

			outputCss.Insert(0, workingSb);
			
			#endregion

			outputCss.Replace("::sfumato{}", utilityCss.ToString());			
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}BuildCssFile() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			AppState.StringBuilderPool.Return(outputCss);
			AppState.StringBuilderPool.Return(utilityCss);
			AppState.StringBuilderPool.Return(workingSb);
		}
		
		return AppRunnerSettings.UseMinify ? outputCss.ToString().CompactCss() : ConsolidateLineBreaksRegex().Replace(outputCss.ToString().Trim(), AppRunnerSettings.LineBreak + AppRunnerSettings.LineBreak);
	}

	private static void ProcessVariantBranchRecursive(VariantBranch branch, KeyValuePair<ulong, string>[] wrappers, CssClass cssClass)
	{
		var index = 0;
		var depth = 1;
		
		while (true)
		{
			if (index >= wrappers.Length)
			{
				branch.CssClasses.Add(cssClass);
				return;
			}

			var (fingerprint, wrapperCss) = wrappers[index];

			var candidate = new VariantBranch { Fingerprint = fingerprint, WrapperCss = wrapperCss, Depth = depth };

			if (branch.Branches.TryGetValue(candidate, out var existing) == false)
			{
				branch.Branches.Add(candidate);
				existing = candidate;
			}

			branch = existing;
			index += 1;
			depth += 1;
		}
	}

	private void GenerateCssFromVariantTree(VariantBranch branch, StringBuilder outputCss, int depth = 0)
	{
		var isWrapped = string.IsNullOrEmpty(branch.WrapperCss) == false;
		
		if (isWrapped)
		{
			outputCss.Append(AppRunnerSettings.Indentation.Repeat(depth - 1)).Append(branch.WrapperCss).Append(AppRunnerSettings.LineBreak).Append(AppRunnerSettings.LineBreak);
		}
		
		foreach (var cssClass in branch.CssClasses.OrderBy(c => c.ClassDefinition?.SelectorSort ?? 0))
		{
			outputCss.Append(AppRunnerSettings.Indentation.Repeat(depth)).Append(cssClass.EscapedSelector).Append(" {").Append(AppRunnerSettings.LineBreak);
			outputCss.Append(AppRunnerSettings.Indentation.Repeat(depth + 1)).Append(cssClass.Styles.Replace(AppRunnerSettings.LineBreak, AppRunnerSettings.LineBreak + AppRunnerSettings.Indentation.Repeat(depth + (isWrapped ? 2 : 1)))).Append(AppRunnerSettings.LineBreak);
			outputCss.Append(AppRunnerSettings.Indentation.Repeat(depth)).Append('}').Append(AppRunnerSettings.LineBreak).Append(AppRunnerSettings.LineBreak);
		}

		if (branch.Branches.Count > 0)
		{
			foreach (var subBranch in branch.Branches)
			{
				GenerateCssFromVariantTree(subBranch, outputCss, depth + 1);
			}
		}
		
		if (string.IsNullOrEmpty(branch.WrapperCss) == false)
		{
			outputCss.Append(AppRunnerSettings.Indentation.Repeat(depth - 1)).Append('}').Append(AppRunnerSettings.LineBreak).Append(AppRunnerSettings.LineBreak);
		}
	}
	
	#endregion
}
