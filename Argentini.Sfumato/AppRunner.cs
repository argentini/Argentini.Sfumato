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

	[GeneratedRegex(@"\s+", RegexOptions.Compiled)]
	private static partial Regex ConsolidateSpacesRegex();
	
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
			    AppRunnerSettings.SfumatoBlockItems.Add(match.Value[..match.Value.IndexOf(':')].Trim(), match.Value[(match.Value.IndexOf(':') + 1)..].TrimEnd(';').Trim());
		    }
		    
		    quoteMatches = CssKeyframeBlocksRegex().Matches(sfumatoCssBlock);

		    foreach (Match match in quoteMatches)
		    {
			    AppRunnerSettings.SfumatoBlockItems.Add(match.Value[..match.Value.IndexOf('{')].Trim(), match.Value[match.Value.IndexOf('{')..].TrimEnd(';').Trim());
		    }

		    if (AppRunnerSettings.SfumatoBlockItems.Count == 0)
			    return;

		    if (AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-reset", out var useReset))
			    AppRunnerSettings.UseReset = useReset.Equals("true", StringComparison.Ordinal);

		    if (AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-forms", out var useForms))
			    AppRunnerSettings.UseForms = useForms.Equals("true", StringComparison.Ordinal);

		    if (AppRunnerSettings.UseMinify == false && AppRunnerSettings.SfumatoBlockItems.TryGetValue("--use-minify", out var useMinify))
			    AppRunnerSettings.UseMinify = useMinify.Equals("true", StringComparison.Ordinal);

		    if (AppRunnerSettings.SfumatoBlockItems.TryGetValue("--output-path", out var outputPath))
			    AppRunnerSettings.CssOutputFilePath = string.IsNullOrEmpty(outputPath) ? "sfumato.css" : outputPath;

		    if (AppRunnerSettings.SfumatoBlockItems.TryGetValue("--paths", out var pathsValue))
		    {
			    var paths = ConsolidateSpacesRegex().Replace(pathsValue, " ").TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (paths.Length != 0)
			    {
				    foreach (var p in paths)
					    AppRunnerSettings.Paths.Add(p.Trim('\"'));
			    }
		    }

		    if (AppRunnerSettings.SfumatoBlockItems.TryGetValue("--not-paths", out var notPathsValue))
		    {
			    var notPaths = ConsolidateSpacesRegex().Replace(notPathsValue, " ").TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (notPaths.Length != 0)
			    {
				    foreach (var p in notPaths)
					    AppRunnerSettings.NotPaths.Add(p.Trim('\"'));
			    }
		    }

		    #region Validate Settings

		    if (string.IsNullOrEmpty(AppRunnerSettings.CssOutputFilePath))
		    {
			    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}Must specify --output-path in file: {AppRunnerSettings.CssFilePath}");
			    Environment.Exit(1);
		    }

		    if (AppRunnerSettings.Paths.Count == 0)
		    {
			    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}Must specify --paths: [] in file: {AppRunnerSettings.CssFilePath}");
			    Environment.Exit(1);
		    }

		    #endregion
	    }
	    catch (Exception e)
	    {
		    await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
}
