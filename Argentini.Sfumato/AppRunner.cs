// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Entities.Library;

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
    }

    /// <summary>
    /// Resets settings, loads CSS content, processes CSS content.
    /// </summary>
    public async Task LoadCssSettingsAsync()
    {
	    try
	    {
		    AppRunnerSettings = new AppRunnerSettings
		    {
			    CssFilePath = _cssFilePath,
			    UseMinify = _useMinify
		    };
		    
		    AppRunnerSettings.ExtractCssContent(); // Extract Sfumato settings and CSS content
		    AppRunnerSettings.ExtractSfumatoItems(); // Parse all the Sfumato settings into a Dictionary<string,string>()
		    AppRunnerSettings.ProcessProjectSettings(); // Read project/operation settings
		    AppRunnerSettings.ImportPartials(); // Read in all CSS partial files (@import "...")








	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
    
    #endregion
    
    #region Process Settings

    public void LoadDefaultSettings()
    {
	    
    }
    
    public void ProcessUserSettings()
    {
	    
    }
    
    #endregion
}
