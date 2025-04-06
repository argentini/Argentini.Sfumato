namespace Argentini.Sfumato;

public sealed class AppRunnerSettings
{
    public string CssFilePath { get; set; } = string.Empty;
    public string CssOutputFilePath { get; set; } = "sfumato.css";

    public bool UseMinify { get; set; }
    public bool UseReset { get; set; } = true;
    public bool UseForms { get; set; } = true;

    public List<string> Paths { get; } = [];
    public List<string> NotPaths { get; } = [];

    public string CssContent { get; set; } = string.Empty;
    public string SfumatoCssBlock { get; set; } = string.Empty;
    public string TrimmedCssContent { get; set; } = string.Empty;
    public Dictionary<string, string> SfumatoBlockItems { get; } = [];

    public void ExtractCssContent()
    {
	    try
	    {
	        CssContent = AppRunner.WhitespaceBeforeLineBreakRegex().Replace(CssContent, string.Empty);
	        CssContent = AppRunner.RemoveBlockCommentsRegex().Replace(CssContent, string.Empty);
				    
	        var quoteMatches = AppRunner.SfumatoCssBlockRegex().Matches(CssContent);

	        if (quoteMatches.Count == 0)
	        {
	            Console.WriteLine($"{AppState.CliErrorPrefix}No ::sfumato {{}} block in file: {CssFilePath}");
	            Environment.Exit(1);
	        }

	        if (quoteMatches.Count != 1)
	        {
	            Console.WriteLine($"{AppState.CliErrorPrefix}Only one ::sfumato {{}} block allowed in file: {CssFilePath}");
	            Environment.Exit(1);
	        }

	        SfumatoCssBlock = quoteMatches[0].Value;

	        var lineBreaks = CssContent.Contains("\r\n") ? "\r\n\r\n" : "\n\n";
			    
	        TrimmedCssContent = AppRunner.ConsolidateLineBreaksRegex().Replace(CssContent.Replace(SfumatoCssBlock, string.Empty), lineBreaks);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
    
    public void ExtractSfumatoItems()
    {
	    try
	    {
	        var sfumatoCssBlock = SfumatoCssBlock.Trim()[SfumatoCssBlock.IndexOf('{')..].TrimEnd('}').Trim();

	        var quoteMatches = AppRunner.CssCustomPropertiesRegex().Matches(sfumatoCssBlock);

	        foreach (Match match in quoteMatches)
	            SfumatoBlockItems.Add(match.Value[..match.Value.IndexOf(':')].Trim(), match.Value[(match.Value.IndexOf(':') + 1)..].TrimEnd(';').Trim());
			    
	        quoteMatches = AppRunner.CssKeyframeBlocksRegex().Matches(sfumatoCssBlock);

	        foreach (Match match in quoteMatches)
	            SfumatoBlockItems.Add(match.Value[..match.Value.IndexOf('{')].Trim(), match.Value[match.Value.IndexOf('{')..].TrimEnd(';').Trim());

	        if (SfumatoBlockItems.Count > 0)
	            return;
	        
	        Console.WriteLine($"{AppState.CliErrorPrefix}No options specified in file: {CssFilePath}");
	        Environment.Exit(1);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }

    public void ProcessProjectSettings()
    {
	    try
	    {
		    if (SfumatoBlockItems.TryGetValue("--use-reset", out var useReset))
			    UseReset = useReset.Equals("true", StringComparison.Ordinal);

		    if (SfumatoBlockItems.TryGetValue("--use-forms", out var useForms))
			    UseForms = useForms.Equals("true", StringComparison.Ordinal);

		    if (UseMinify == false && SfumatoBlockItems.TryGetValue("--use-minify", out var useMinify))
			    UseMinify = useMinify.Equals("true", StringComparison.Ordinal);

		    if (SfumatoBlockItems.TryGetValue("--output-path", out var outputPath))
			    CssOutputFilePath = string.IsNullOrEmpty(outputPath) ? "sfumato.css" : outputPath;

		    if (SfumatoBlockItems.TryGetValue("--paths", out var pathsValue))
		    {
			    var paths = AppRunner.ConsolidateSpacesRegex().Replace(pathsValue, " ").TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (paths.Length != 0)
			    {
				    foreach (var p in paths)
					    Paths.Add(p.Trim('\"'));
			    }
		    }

		    if (SfumatoBlockItems.TryGetValue("--not-paths", out var notPathsValue))
		    {
			    var notPaths = AppRunner.ConsolidateSpacesRegex().Replace(notPathsValue, " ").TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (notPaths.Length != 0)
			    {
				    foreach (var p in notPaths)
					    NotPaths.Add(p.Trim('\"'));
			    }
		    }

		    if (string.IsNullOrEmpty(CssOutputFilePath))
		    {
			    Console.WriteLine($"{AppState.CliErrorPrefix}Must specify --output-path in file: {CssFilePath}");
			    Environment.Exit(1);
		    }

		    if (Paths.Count > 0)
			    return;
		    
		    Console.WriteLine($"{AppState.CliErrorPrefix}Must specify --paths: [] in file: {CssFilePath}");
		    Environment.Exit(1);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
}