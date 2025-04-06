using Argentini.Sfumato.Entities.Library;

namespace Argentini.Sfumato;

public partial class AppRunner
{
	#region Regular Expressions

	[GeneratedRegex(@"(/\*[\d\D]*?\*/)", RegexOptions.Compiled)]
	public static partial Regex RemoveBlockCommentsRegex();
	
	[GeneratedRegex(@"::sfumato\s*\{(?:(?>[^{}]+)|\{(?<bal>)|\}(?<-bal>))*(?(bal)(?!))\}", RegexOptions.Compiled | RegexOptions.Singleline)]
	public static partial Regex SfumatoCssBlockRegex();

	[GeneratedRegex(@"((?<property>--[\w-]+)\s*:\s*(?<value>(?:""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'|[^;])+)\s*;)", RegexOptions.Compiled)]
	public static partial Regex CssCustomPropertiesRegex();

	[GeneratedRegex(@"@keyframes\s+(?<name>[a-zA-Z0-9_-]+)\s*\{(?:(?>[^{}]+)|\{(?<bal>)|\}(?<-bal>))*(?(bal)(?!))\}", RegexOptions.Compiled | RegexOptions.Singleline)]
	public static partial Regex CssKeyframeBlocksRegex();

	[GeneratedRegex(@"\s+", RegexOptions.Compiled)]
	public static partial Regex ConsolidateSpacesRegex();
	
	[GeneratedRegex(@"\s+(?=\r\n|\n)", RegexOptions.Compiled)]
	public static partial Regex WhitespaceBeforeLineBreakRegex();
	
	[GeneratedRegex(@"(?:\r\n|\n){3,}", RegexOptions.Compiled)]
	public static partial Regex ConsolidateLineBreaksRegex();
	
	#endregion
	
	#region Run Mode Properties

	public AppState AppState { get; }
	public Library Library { get; } = new();
	public AppRunnerSettings AppRunnerSettings { get; }

    #endregion

    public AppRunner(AppState appState, string cssFilePath = "", bool useMinify = false)
    {
	    AppState = appState;

	    AppRunnerSettings = new AppRunnerSettings
	    {
			CssFilePath = cssFilePath,
			UseMinify = useMinify,
	    };
	    
	    if (string.IsNullOrEmpty(cssFilePath) == false)
		    AppRunnerSettings.CssContent = File.ReadAllText(AppRunnerSettings.CssFilePath);
    }

    public async Task LoadCssSettingsAsync()
    {
	    try
	    {
		    AppRunnerSettings.ExtractCssContent();
		    AppRunnerSettings.ExtractSfumatoItems();
		    AppRunnerSettings.ProcessProjectSettings();






	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
}
