// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Reflection;
using Argentini.Sfumato.Entities.CssClassProcessing;
using Argentini.Sfumato.Entities.Library;
using Argentini.Sfumato.Entities.Scanning;
using Argentini.Sfumato.Entities.UtilityClasses;

namespace Argentini.Sfumato;

public sealed class AppRunner
{
	#region Properties

	public AppState AppState { get; }
	public Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; } = new();
	public Dictionary<string,ScannedFile> ScannedFiles { get; set; } = new(StringComparer.Ordinal);
	public Dictionary<string,string> UsedCssCustomProperties { get; set; } = new(StringComparer.Ordinal);
	public Dictionary<string,string> UsedCss { get; set; } = new(StringComparer.Ordinal);

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
		    AppRunnerSettings = new AppRunnerSettings();
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

		    Library.ColorsByName.Add(key, color.Value);
	    }
	    
	    #endregion

	    #region Read breakpoints
	    
	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--breakpoint-")))
	    {
		    var key = breakpoint.Key.TrimStart("--breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;
		    
		    Library.MediaQueryPrefixes.Add(key, new VariantMetadata
		    {
			    PrefixOrder = Library.MediaQueryPrefixes.Count + 1,
			    PrefixType = "media",
			    Statement = $"(width >= {breakpoint.Value})"
		    });
		    
		    Library.MediaQueryPrefixes.Add($"max-{key}", new VariantMetadata
		    {
			    PrefixOrder = Library.MediaQueryPrefixes.Count + 1,
			    PrefixType = "media",
			    Statement = $"(width < {breakpoint.Value})"
		    });
	    }
	    
	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--adaptive-breakpoint-")))
	    {
		    var key = breakpoint.Key.TrimStart("--adaptive-breakpoint-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    if (double.TryParse(breakpoint.Value, out var maxValue) == false)
			    continue;

		    Library.MediaQueryPrefixes.Add(key, new VariantMetadata
		    {
			    PrefixOrder = Library.MediaQueryPrefixes.Count + 1,
			    PrefixType = "media",
			    Statement = $"(min-aspect-ratio: {breakpoint.Value})"
		    });
		    
		    Library.MediaQueryPrefixes.Add($"max-{key}", new VariantMetadata
		    {
			    PrefixOrder = Library.MediaQueryPrefixes.Count + 1,
			    PrefixType = "media",
			    Statement = $"(max-aspect-ratio: {maxValue - 0.000000000001})"
		    });
	    }

	    foreach (var breakpoint in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--container-")))
	    {
		    var key = breakpoint.Key.TrimStart("--container-") ?? string.Empty;

		    if (string.IsNullOrEmpty(key))
			    continue;

		    Library.ContainerQueryPrefixes.Add($"@{key}", new VariantMetadata
		    {
			    PrefixOrder = Library.ContainerQueryPrefixes.Count + 1,
			    PrefixType = "container",
			    Statement = $"(width >= {breakpoint.Value})"
		    });
		    
		    Library.ContainerQueryPrefixes.Add($"@max-{key}", new VariantMetadata
		    {
			    PrefixOrder = Library.ContainerQueryPrefixes.Count + 1,
			    PrefixType = "container",
			    Statement = $"(width < {breakpoint.Value})"
		    });
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


    
	// todo: scan file paths for utility classes  

	// todo: watchers
	
    
    
    /// <summary>
    /// Gather dependencies from all scanned files, consolidate them, and generate the CSS output file.
    /// </summary>
    public void BuildCssFile()
	{
	    try
	    {
		    #region Consolidate dependencies

		    UsedCssCustomProperties.Clear();
		    UsedCss.Clear();

		    foreach (var scannedFile in ScannedFiles)
		    {
			    foreach (var utilityClass in scannedFile.Value.UtilityClasses)
			    {
				    foreach (var dependency in utilityClass.Value.ClassDefinition?.UsesCssCustomProperties ?? [])
				    {
					    if (dependency.StartsWith("--", StringComparison.Ordinal))
						    UsedCssCustomProperties.TryAddUpdate(dependency, string.Empty);
					    else
						    UsedCss.TryAddUpdate(dependency, string.Empty);
				    }
			    }
		    }
		    
		    #endregion

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

		    // todo: inject dependencies into CSS output

		    // todo: iterate utility classes and inject into CSS output

		    // todo: process @apply and CSS custom property usage in CSS source
		    
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}BuildCssFile() - {e.Message}");
		    Environment.Exit(1);
	    }
	}
    
    #endregion
}
