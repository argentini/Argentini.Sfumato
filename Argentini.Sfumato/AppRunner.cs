// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Entities.Library;

namespace Argentini.Sfumato;

public sealed class AppRunner
{
	#region Run Mode Properties

	public AppState AppState { get; }
	public Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; set; }

	private readonly string _cssFilePath;
	private readonly string _cssContent = string.Empty;
	private readonly bool _useMinify;
	
    #endregion

    public AppRunner(AppState appState, string cssFilePath = "", bool useMinify = false)
    {
	    AppState = appState;

	    _cssFilePath = cssFilePath;
	    _useMinify = useMinify;
	    
	    if (string.IsNullOrEmpty(_cssFilePath) == false)
		    _cssContent = File.ReadAllText(_cssFilePath);

	    AppRunnerSettings = new AppRunnerSettings
	    {
		    CssFilePath = _cssFilePath,
		    UseMinify = _useMinify,
		    CssContent = _cssContent
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
			    UseMinify = _useMinify,
			    CssContent = _cssContent
		    };
		    
		    AppRunnerSettings.ExtractCssContent(); // Extract Sfumato settings and CSS content
		    AppRunnerSettings.ExtractSfumatoItems(); // Parse all the Sfumato settings into a Dictionary<string,string>()
		    AppRunnerSettings.ProcessProjectSettings(); // Read project/operation settings



		    
		    
		    



	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
}
