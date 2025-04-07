// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Entities.Library;
using Argentini.Sfumato.Entities.UtilityClasses;

namespace Argentini.Sfumato;

public sealed class AppRunner
{
	#region Properties

	public AppState AppState { get; }
	public Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; }

	private readonly string _cssFilePath;
	private readonly bool _useMinify;
	
    #endregion

    #region Construction
    
    public AppRunner(AppState appState, string cssFilePath = "", bool useMinify = false)
    {
	    AppState = appState;

	    _cssFilePath = cssFilePath;
	    _useMinify = useMinify;
	    
	    AppRunnerSettings = new AppRunnerSettings
	    {
		    CssFilePath = _cssFilePath,
		    UseMinify = _useMinify
	    };
	    
	    try
	    {
		    AppRunnerSettings = new AppRunnerSettings();
		    AppRunnerSettings.ExtractSfumatoItems(File.ReadAllText(Path.Combine(AppState.EmbeddedCssPath, "defaults.css")));

		    ProcessSettings();
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }

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

		    ProcessSettings();
	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }

    #endregion
    
    #region Process Settings

    public void ProcessSettings()
    {
	    foreach (var color in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--color-")))
	    {
			Library.ColorsByName.Add(color.Key[8..], color.Value);
	    }
	    
	    foreach (var font in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--font-")))
	    {
		    if (font.Key.StartsWith("--font-weight-"))
		    {
			    var key = $"font-{font.Key["--font-weight-".Length..]}";
			    var value = new ClassDefinition
			    {
					IsSimpleUtility = true,
					Template = 
						$"""
						 font-weight: var({font.Key});"
						 """,
					UsesCssCustomProperties = [font.Key]
			    };

			    if (Library.SimpleClasses.TryAdd(key, value))
				    Library.ScannerClassNamePrefixes.Insert(key);
			    else
				    Library.SimpleClasses[key] = value;
		    }
            else if (font.Key.StartsWith("--font-"))
		    {
			    var key = font.Key.Trim('-');
			    var value = new ClassDefinition
			    {
				    IsSimpleUtility = true,
				    Template = 
					    $"font-family: var({font.Key});",
				    UsesCssCustomProperties = [font.Key]
			    };

			    if (Library.SimpleClasses.TryAdd(key, value))
				    Library.ScannerClassNamePrefixes.Insert(key);
			    else
				    Library.SimpleClasses[key] = value;
		    }
	    }

	    foreach (var text in AppRunnerSettings.SfumatoBlockItems.Where(i => i.Key.StartsWith("--text-")))
	    {
		    if (text.Key.EndsWith("--line-height"))
			    continue;
		    
		    var key = text.Key.Trim('-');
		    var value = new ClassDefinition
		    {
			    IsSimpleUtility = true,
			    UsesSlashModifier = true,
			    Template =
				    $"""
				    font-size: var({text.Key});
				    line-height: var({text.Key}--line-height);
				    """,
			    ModifierTemplate =
				    $$"""
				    font-size: var({{text.Key}});
				    line-height: calc(var(--spacing) * {1});
				    """,
			    ArbitraryModifierTemplate =
				    $$"""
				    font-size: var({{text.Key}});
				    line-height: {1};
				    """,
			    UsesCssCustomProperties =
			    [
				    "--spacing", text.Key, $"{text.Key}--line-height"
			    ]
		    };

		    if (Library.SimpleClasses.TryAdd(key, value))
			    Library.ScannerClassNamePrefixes.Insert(key);
		    else
			    Library.SimpleClasses[key] = value;
	    }
	    
	    
	    
	    
	    
	    
	    
	    
    }
    
    #endregion
}
