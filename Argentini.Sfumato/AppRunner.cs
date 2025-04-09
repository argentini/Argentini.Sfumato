// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Reflection;
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
	    
	    var derivedTypes = Assembly.GetExecutingAssembly()
		    .GetTypes()
		    .Where(t => typeof(ClassDictionaryBase).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

	    foreach (var type in derivedTypes)
	    {
		    if (Activator.CreateInstance(type) is not ClassDictionaryBase instance)
			    continue;
		    
		    instance.ProcessThemeSettings(this);
	    }
    }
    
    #endregion
}
