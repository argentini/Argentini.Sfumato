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
	
	[GeneratedRegex(@"@apply\s+[^;]+?;", RegexOptions.Compiled)]
	private static partial Regex AtApplyRegex();
	
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
    public void BuildCssFile()
	{
		var outputCss = AppState.StringBuilderPool.Get();
		var generatedCss = AppState.StringBuilderPool.Get();
		var workingSb = AppState.StringBuilderPool.Get();

		try
		{
			UsedCssCustomProperties.Clear();
			UsedCss.Clear();

			outputCss.Append(AppRunnerSettings.ProcessedCssContent);

			#region Always include root CSS custom properties

			UsedCssCustomProperties.TryAddUpdate("--spacing", string.Empty);			

			UsedCssCustomProperties.TryAddUpdate("--font-sans", string.Empty);			
			UsedCssCustomProperties.TryAddUpdate("--font-sans--font-feature-settings", string.Empty);			
			UsedCssCustomProperties.TryAddUpdate("--font-sans--font-variation-settings", string.Empty);			
			
			UsedCssCustomProperties.TryAddUpdate("--font-serif", string.Empty);			
			UsedCssCustomProperties.TryAddUpdate("--font-serif--font-feature-settings", string.Empty);			
			UsedCssCustomProperties.TryAddUpdate("--font-serif--font-variation-settings", string.Empty);			

			UsedCssCustomProperties.TryAddUpdate("--font-mono", string.Empty);			
			UsedCssCustomProperties.TryAddUpdate("--font-mono--font-feature-settings", string.Empty);			
			UsedCssCustomProperties.TryAddUpdate("--font-mono--font-variation-settings", string.Empty);			

			UsedCssCustomProperties.TryAddUpdate("--default-transition-duration", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-transition-timing-function", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-font-family", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-font-feature-settings", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-font-variation-settings", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-mono-font-family", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-mono-font-feature-settings", string.Empty);
			UsedCssCustomProperties.TryAddUpdate("--default-mono-font-variation-settings", string.Empty);

			if (AppRunnerSettings.UseForms)
			{
				UsedCssCustomProperties.TryAddUpdate("--form-field-background-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-placeholder-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-border-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-focus-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-check-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-button-bg-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-button-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-button-hover-bg-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-button-hover-color", string.Empty);
    
				UsedCssCustomProperties.TryAddUpdate("--form-dark-background-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-dark-color", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-dark-placeholder-color", string.Empty);

				UsedCssCustomProperties.TryAddUpdate("--form-field-focus-border-width", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-border-radius", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-border-width", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-padding", string.Empty);
				UsedCssCustomProperties.TryAddUpdate("--form-field-max-height", string.Empty);

				UsedCssCustomProperties.TryAddUpdate("--form-button-padding", string.Empty);

				foreach (var item in AppRunnerSettings.SfumatoBlockItems)
				{
					if (item.Key.StartsWith("--form-") == false || item.Value.StartsWith("var(") == false)
						continue;
					
					var key = item.Value.TrimStart("var(").TrimEnd(")") ?? string.Empty;

					if (string.IsNullOrEmpty(key) == false)
						UsedCssCustomProperties.TryAddUpdate(key, string.Empty);
				}
			}
			
			#endregion
			
			#region Process @apply and CSS custom property usage in CSS source

			foreach (var match in AtApplyRegex().Matches(AppRunnerSettings.ProcessedCssContent).ToList())
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

			#region Process scanned file dependencies

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
			
			#region Add values to CSS custom property dependencies
			
			foreach (var usedCssCustomProperty in UsedCssCustomProperties)
			{
				if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(usedCssCustomProperty.Key, out var value))
					UsedCssCustomProperties[usedCssCustomProperty.Key] = value;
			}

			foreach (var usedCss in UsedCss)
			{
				if (AppRunnerSettings.SfumatoBlockItems.TryGetValue(usedCss.Key, out var value))
					UsedCss[usedCss.Key] = value;
			}

			#endregion

			#region Inject used CSS custom properties and CSS into :root
			
			if (UsedCssCustomProperties.Count > 0)
			{
				generatedCss.Append(":root {").Append(AppRunnerSettings.LineBreak);

				foreach (var ccp in UsedCssCustomProperties)
					generatedCss.Append(AppRunnerSettings.Indentation).Append(ccp.Key).Append(": ").Append(ccp.Value).Append(';').Append(AppRunnerSettings.LineBreak);

				generatedCss.Append('}').Append(AppRunnerSettings.LineBreak).Append(AppRunnerSettings.LineBreak);
			}

			if (UsedCss.Count > 0)
			{
				foreach (var ccp in UsedCss)
					generatedCss.Append(ccp.Key).Append(' ').Append(ccp.Value).Append(AppRunnerSettings.LineBreak);

				generatedCss.Append(AppRunnerSettings.LineBreak);
			}
			
			#endregion

			#region Inject optional reset
			
			if (AppRunnerSettings.UseReset)
			{
				generatedCss.Append(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "browser-reset.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak)).Append(AppRunnerSettings.LineBreak);
			}

			#endregion

			#region Inject optional forms classes
			
			if (AppRunnerSettings.UseForms)
			{
				generatedCss.Append(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "forms.css")).NormalizeLinebreaks(AppRunnerSettings.LineBreak)).Append(AppRunnerSettings.LineBreak);
			}

			#endregion

			#region Generate CSS: build consolidated variant structure, generate CSS

			workingSb.Clear();
			
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

			GenerateCssFromVariantTree(root, generatedCss);

			#endregion

			outputCss.Insert(0, generatedCss);
		}
		catch (Exception e)
		{
			Console.WriteLine($"{AppState.CliErrorPrefix}BuildCssFile() - {e.Message}");
			Environment.Exit(1);
		}
		finally
		{
			AppState.StringBuilderPool.Return(outputCss);
			AppState.StringBuilderPool.Return(generatedCss);
			AppState.StringBuilderPool.Return(workingSb);
		}
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
