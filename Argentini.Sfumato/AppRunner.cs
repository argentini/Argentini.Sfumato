using Argentini.Sfumato.Entities.Library;

namespace Argentini.Sfumato;

public partial class AppRunner
{
	#region Regular Expressions

	[GeneratedRegex(@"(/\*[\d\D]*?\*/)", RegexOptions.Compiled)]
	private static partial Regex RemoveBlockCommentsRegex();
	
	[GeneratedRegex(@"::sfumato\s*\{(?:(?>[^{}]+)|\{(?<bal>)|\}(?<-bal>))*(?(bal)(?!))\}", RegexOptions.Compiled | RegexOptions.Singleline)]
	private static partial Regex SfumatoCssBlockRegex();

	[GeneratedRegex(@"((?<property>--[\w-]+)\s*:\s*(?<value>(?:""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'|[^;])+)\s*;)", RegexOptions.Compiled)]
	private static partial Regex CssCustomPropertiesRegex();

	[GeneratedRegex(@"@keyframes\s+(?<name>[a-zA-Z0-9_-]+)\s*\{(?:(?>[^{}]+)|\{(?<bal>)|\}(?<-bal>))*(?(bal)(?!))\}", RegexOptions.Compiled | RegexOptions.Singleline)]
	private static partial Regex CssKeyframeBlocksRegex();

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
		    var quoteMatches = SfumatoCssBlockRegex().Matches(AppRunnerSettings.CssContent);

		    if (quoteMatches.Count == 0)
		    {
			    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}No ::sfumato {{}} block in file: {AppRunnerSettings.CssFilePath}");
			    Environment.Exit(1);
		    }

		    if (quoteMatches.Count != 1)
		    {
			    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}Only one ::sfumato {{}} block allowed in file: {AppRunnerSettings.CssFilePath}");
			    Environment.Exit(1);
		    }

		    AppRunnerSettings.SfumatoCssBlock = quoteMatches[0].Value;
		    AppRunnerSettings.TrimmedCssContent = AppRunnerSettings.CssContent.Replace(AppRunnerSettings.SfumatoCssBlock, string.Empty).Trim();

		    var sfumatoCssBlock = AppRunnerSettings.SfumatoCssBlock.Trim()[AppRunnerSettings.SfumatoCssBlock.IndexOf('{')..].TrimEnd('}').Trim();

		    sfumatoCssBlock = RemoveBlockCommentsRegex().Replace(sfumatoCssBlock, string.Empty);

		    quoteMatches = CssCustomPropertiesRegex().Matches(sfumatoCssBlock);

		    foreach (Match match in quoteMatches)
		    {
			    AppRunnerSettings.SfumatoBlockItems.Add(match.Value[..match.Value.IndexOf(':')].Trim(), match.Value[(match.Value.IndexOf(':') + 1)..].Trim());
		    }
		    
		    quoteMatches = CssKeyframeBlocksRegex().Matches(sfumatoCssBlock);

		    foreach (Match match in quoteMatches)
		    {
			    AppRunnerSettings.SfumatoBlockItems.Add(match.Value[..match.Value.IndexOf('{')].Trim(), match.Value[match.Value.IndexOf('{')..].Trim());
		    }
	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
}
