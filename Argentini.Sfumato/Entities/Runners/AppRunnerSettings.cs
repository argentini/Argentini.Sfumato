// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.Runners;

public partial class AppRunnerSettings(AppRunner? appRunner)
{
	#region Regular Expressions

	[GeneratedRegex(@"(/\*[\d\D]*?\*/)")]
	private static partial Regex RemoveBlockCommentsRegex();
	
	[GeneratedRegex(@"::sfumato\s*\{(?:(?>[^{}]+)|\{(?<bal>)|\}(?<-bal>))*(?(bal)(?!))\}", RegexOptions.Singleline)]
	private static partial Regex SfumatoCssBlockRegex();

	[GeneratedRegex(@"((?<property>--[\w-]+)\s*:\s*(?<value>(?:""(?:\\.|[^""\\])*""|'(?:\\.|[^'\\])*'|[^;])+)\s*;)")]
	private static partial Regex CssCustomPropertiesRegex();

	[GeneratedRegex(@"(\.(?<class>[a-zA-Z0-9_-]+)|(@(?<kind>[a-zA-Z0-9_-]+)\s+(?<name>[a-zA-Z0-9_-]+)))\s*\{(?:(?>[^{}]+)|\{(?<bal>)|\}(?<-bal>))*(?(bal)(?!))\}", RegexOptions.Singleline)]
	private static partial Regex CssAtAndClassBlocksRegex();

	// Matches:
	// @custom-variant <name> ( ... );
	//  - group "name"    ⇒ the custom-variant identifier (e.g. "tab-4")
	//  - group "content" ⇒ everything inside the outer parens, including nested (...)
	[GeneratedRegex(@"@custom-variant\s+(?<name>[\w-]+)\s*\(\s*(?<content>(?:[^()]|\((?<Depth>)|\)(?<-Depth>))*)(?(Depth)(?!))\s*\);")]
	private static partial Regex AtCustomVariantRegex();

	[GeneratedRegex(@"\s+")]
	private static partial Regex ConsolidateSpacesRegex();
	
	[GeneratedRegex(@"[ \t]+(?=\r\n|\n)")]
	private static partial Regex WhitespaceBeforeLineBreakRegex();
	
	#endregion

	#region Properties

	private AppRunner? AppRunner { get; set; } = appRunner;

	private string _cssFilePath = string.Empty;
	public string CssFilePath
	{
		get => _cssFilePath;
		set
		{
			_cssFilePath = value;

			if (string.IsNullOrEmpty(_cssFilePath))
				return;
			
			NativeCssFilePathOnly = Path.GetFullPath(Path.GetDirectoryName(CssFilePath.SetNativePathSeparators()) ?? string.Empty);
			CssFileNameOnly = Path.GetFileName(CssFilePath.SetNativePathSeparators());
			NativeCssOutputFilePath = Path.GetFullPath(Path.Combine(NativeCssFilePathOnly, CssOutputFilePath.SetNativePathSeparators()));
		}
	}

	private string _cssOutputFilePath = "sfumato.css";
	public string CssOutputFilePath
	{
		get => _cssOutputFilePath;
		set
		{
			_cssOutputFilePath = value;

			NativeCssOutputFilePath = Path.GetFullPath(Path.Combine(NativeCssFilePathOnly, CssOutputFilePath.SetNativePathSeparators()));
		}
	}

	private string _cssContent = string.Empty;
	public string CssContent
	{
		get => _cssContent;
		private set
		{
			_cssContent = value;
			LineBreak = _cssContent.Contains("\r\n") ? "\r\n" : "\n";
		}
	}

	public bool UseMinify { get; set; }
	public bool UseReset { get; set; } = true;
	public bool UseForms { get; set; } = true;

	public string NativeCssFilePathOnly { get; private set; } = string.Empty;
	public string CssFileNameOnly { get; private set; } = string.Empty;
	public string NativeCssOutputFilePath { get; private set; } = string.Empty;
	public string LineBreak { get; private set; } = "\n";

	public string Indentation { get; set; } = "    ";
	
    public List<string> Paths { get; } = [];
    public List<string> NotPaths { get; } = [];

    public string SfumatoCssBlock { get; private set; } = string.Empty;
    public string ProcessedCssContent { get; set; } = string.Empty;
    public Dictionary<string, string> SfumatoBlockItems { get; } = [];

    #endregion

    /// <summary>
    /// Load CSS file content, extract the Sfumato settings block, remove from CSS content.
    /// Also removes block comments and whitespace before line breaks.
    /// Puts trimmed CSS content into ProcessedCssContent.
    /// </summary>
    public void LoadAndExtractCssContent()
    {
	    try
	    {
		    if (string.IsNullOrEmpty(CssFilePath) == false)
			    CssContent = File.ReadAllText(Path.GetFullPath(CssFilePath.SetNativePathSeparators()));

	        CssContent = WhitespaceBeforeLineBreakRegex().Replace(CssContent, string.Empty);
	        CssContent = RemoveBlockCommentsRegex().Replace(CssContent, string.Empty);

	        var sfumatoBlockMatches = SfumatoCssBlockRegex().Matches(CssContent);

	        if (sfumatoBlockMatches.Count == 0)
	        {
	            Console.WriteLine($"{AppState.CliErrorPrefix}No ::sfumato {{}} block in file: {CssFilePath}");
	            Environment.Exit(1);
	        }

	        if (sfumatoBlockMatches.Count != 1)
	        {
	            Console.WriteLine($"{AppState.CliErrorPrefix}Only one ::sfumato {{}} block allowed in file: {CssFilePath}");
	            Environment.Exit(1);
	        }

	        SfumatoCssBlock = sfumatoBlockMatches[0].Value;

	        ProcessedCssContent = CssContent.Replace(SfumatoCssBlock, string.Empty);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
    }
    
    /// <summary>
    /// Parse Sfumato settings block into dictionary items. 
    /// </summary>
    public void ExtractSfumatoItems(string sfumatoCssBlock = "")
    {
	    try
	    {
		    sfumatoCssBlock = string.IsNullOrEmpty(sfumatoCssBlock) ? SfumatoCssBlock.Trim()[SfumatoCssBlock.IndexOf('{')..].TrimEnd('}').Trim() : sfumatoCssBlock.Trim()[sfumatoCssBlock.IndexOf('{')..].TrimEnd('}').Trim();
		    
	        var quoteMatches = CssCustomPropertiesRegex().Matches(sfumatoCssBlock);

	        #region Determine Indentation and spaces/tabs from first ::sfumato{} CSS custom property item
	        
	        if (quoteMatches.Count > 0)
	        {
		        if (quoteMatches[0].Index > 0)
		        {
			        var depth = 0;
			        var usesTabs = false;
			        
			        for (var i = quoteMatches[0].Index - 1; i >= 0; i--)
			        {
				        if (sfumatoCssBlock[i] == ' ' || sfumatoCssBlock[i] == '\t')
					        depth += 1;
				        else
					        break;

				        if (sfumatoCssBlock[i] == '\t')
					        usesTabs = true;
			        }

			        if (depth > 0)
			        {
				        Indentation = string.Empty;

				        for (var i = 0; i < depth; i++)
				        {
					        Indentation += (usesTabs ? "\t" : " ");
				        }
			        }
		        }
	        }
	        
	        #endregion
	        
	        foreach (Match match in quoteMatches)
		        if (SfumatoBlockItems.TryAdd(match.Value[..match.Value.IndexOf(':')].Trim(), match.Value[(match.Value.IndexOf(':') + 1)..].TrimEnd(';').Trim()) == false)
		        {
			        SfumatoBlockItems[match.Value[..match.Value.IndexOf(':')].Trim()] = match.Value[(match.Value.IndexOf(':') + 1)..].TrimEnd(';').Trim();
		        }

	        quoteMatches = CssAtAndClassBlocksRegex().Matches(sfumatoCssBlock);

	        foreach (Match match in quoteMatches)
		        if (SfumatoBlockItems.TryAdd(match.Value[..match.Value.IndexOf('{')].Trim(), match.Value[match.Value.IndexOf('{')..].TrimEnd(';').Trim()) == false)
		        {
			        SfumatoBlockItems[match.Value[..match.Value.IndexOf('{')].Trim()] = match.Value[match.Value.IndexOf('{')..].TrimEnd(';').Trim();
		        }

	        quoteMatches = AtCustomVariantRegex().Matches(sfumatoCssBlock);

	        foreach (Match match in quoteMatches)
	        {
		        if (SfumatoBlockItems.TryAdd($"@custom-variant {match.Groups["name"].Value}", match.Groups["content"].Value.TrimEnd(';')) == false)
		        {
			        SfumatoBlockItems[$"@custom-variant {match.Groups["name"].Value}"] = match.Groups["content"].Value.TrimEnd(';');
		        }
	        }

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

    /// <summary>
    /// Process project settings from the dictionary.
    /// Only handles operation settings like minify, paths, etc.
    /// </summary>
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
			    CssOutputFilePath = (string.IsNullOrEmpty(outputPath) ? "sfumato.css" : outputPath).Trim('\"');

		    if (SfumatoBlockItems.TryGetValue("--paths", out var pathsValue))
		    {
			    var paths = ConsolidateSpacesRegex().Replace(pathsValue, " ").TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (paths.Length != 0)
			    {
				    foreach (var p in paths)
					    Paths.Add(p.Trim('\"'));
			    }
		    }

		    if (SfumatoBlockItems.TryGetValue("--not-paths", out var notPathsValue))
		    {
			    var notPaths = ConsolidateSpacesRegex().Replace(notPathsValue, " ").TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

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

    /// <summary>
    /// Read all nested partial references (e.g. @import "...") from ProcessedCssContent
    /// and replace import statements with partial file content.
    /// </summary>
    public void ImportPartials()
    {
	    ProcessedCssContent = ImportPartials(ProcessedCssContent, NativeCssFilePathOnly).Trim();
    }

    private string ImportPartials(string css, string parentPath = "")
    {
	    if (AppRunner is null)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}ImportPartials(); No AppRunner");
		    Environment.Exit(1);
	    }
	    
	    var sb = AppRunner.AppState.StringBuilderPool.Get();
	    var filePath = string.Empty;

	    sb.Append(css);
	    
	    try
	    {
		    var importIndex = css.IndexOf("@import ", StringComparison.Ordinal);

		    while (importIndex > -1)
		    {
			    var importStatement = css[importIndex..(css.IndexOf(';', importIndex) + 1)];
			    var importPath = importStatement.Replace("@import", string.Empty).Trim().Trim(';').Trim('\"').SetNativePathSeparators();
			    
			    filePath = Path.GetFullPath(Path.Combine(parentPath, importPath));

			    if (File.Exists(filePath) == false)
			    {
				    Console.WriteLine($"{AppState.CliErrorPrefix}File does not exist: {filePath}");
				    Environment.Exit(1);
			    }

			    var childCss = File.ReadAllText(filePath);
				var injectedCss = ImportPartials(childCss, Path.GetDirectoryName(filePath) ?? string.Empty);

			    sb.Replace(importStatement, injectedCss);

			    if (importIndex >= css.Length - 1)
				    break;
			    
			    importIndex = css.IndexOf("@import ", importIndex + 1, StringComparison.Ordinal);
		    }
		    
		    return sb.ToString();
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}Error reading file: {filePath}; {e.Message}");
		    Environment.Exit(1);
	    }
	    finally
	    {
		    AppRunner.AppState.StringBuilderPool.Return(sb);
	    }

	    return css;
    }
}