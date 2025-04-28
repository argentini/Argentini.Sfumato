// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.Runners;

public sealed class AppRunnerSettings(AppRunner? appRunner)
{
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
			NativeCssFilePath = Path.Combine(NativeCssFilePathOnly, CssFileNameOnly);
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

	public string NativeCssFilePath { get; private set; } = string.Empty;
	public string NativeCssFilePathOnly { get; private set; } = string.Empty;
	public string CssFileNameOnly { get; private set; } = string.Empty;
	public string NativeCssOutputFilePath { get; private set; } = string.Empty;
	public string LineBreak { get; private set; } = "\n";

	public string Indentation { get; set; } = "    ";
	
	public List<FileInfo> Imports { get; } = [];
    public List<string> Paths { get; } = [];
    public List<string> AbsolutePaths { get; } = [];
    public List<string> NotPaths { get; } = [];
    public List<string> AbsoluteNotPaths { get; } = [];
    public List<string> NotFolderNames { get; } = [];

    public string SfumatoCssBlock { get; private set; } = string.Empty;
    public string ProcessedCssContent { get; set; } = string.Empty;
    public Dictionary<string, string> SfumatoBlockItems { get; } = [];

    #endregion

    /// <summary>
    /// Load CSS file content, extract the Sfumato settings block, remove from CSS content.
    /// Also removes block comments and whitespace before line breaks.
    /// Puts trimmed CSS content into ProcessedCssContent.
    /// </summary>
    public void LoadCssAndExtractSfumatoBlock()
    {
	    var sb = AppRunner?.AppState.StringBuilderPool.Get();

	    if (sb is null)
		    return;
	    
	    try
	    {
		    if (string.IsNullOrEmpty(CssFilePath) == false)
			    CssContent = File.ReadAllText(Path.GetFullPath(CssFilePath.SetNativePathSeparators()));

	        CssContent = CssContent.TrimWhitespaceBeforeLineBreaks(sb);
	        CssContent = CssContent.RemoveBlockComments(sb);

	        #region Extract Sfumato settings block

		    var sfumatoBlockStart = CssContent.IndexOf("::sfumato", StringComparison.Ordinal);
	        
		    if (sfumatoBlockStart < 0)
		    {
			    Console.WriteLine($"{AppState.CliErrorPrefix}No ::sfumato {{}} block in file: {CssFilePath}");
			    Environment.Exit(1);
		    }

		    var openBraceIndex = CssContent.IndexOf('{', sfumatoBlockStart);
	        
		    if (openBraceIndex <= sfumatoBlockStart)
		    {
			    Console.WriteLine($"{AppState.CliErrorPrefix}::sfumato block has no opening {{ in file: {CssFilePath}");
			    Environment.Exit(1);
		    }

			var braceCount = 0;
			var closingBraceIndex = -1;

			for (var i = openBraceIndex; i < CssContent.Length; i++)
			{
				if (closingBraceIndex > -1)
					break;
				
				if (CssContent[i] == '{')
					braceCount++;
				else if (CssContent[i] == '}')
					braceCount--;

				if (braceCount == 0)
					closingBraceIndex = i;
			}

			if (closingBraceIndex < 0)
			{
				Console.WriteLine($"{AppState.CliErrorPrefix}::sfumato block has no closing }} in file: {CssFilePath}");
				Environment.Exit(1);
			}

			SfumatoCssBlock = CssContent[sfumatoBlockStart..(closingBraceIndex + 1)];
			ProcessedCssContent = CssContent.Replace(SfumatoCssBlock, string.Empty);

	        #endregion
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"{AppState.CliErrorPrefix}{e.Message}");
		    Environment.Exit(1);
	    }
	    finally
	    {
		    AppRunner?.AppState.StringBuilderPool.Return(sb);
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

		    var cssCustomPropertyIndex = sfumatoCssBlock.IndexOf("--", StringComparison.Ordinal);

		    #region Determine Indentation and spaces/tabs from first ::sfumato{} CSS custom property item
	        
		    if (cssCustomPropertyIndex > 0)
		    {
			    var depth = 0;
			    var usesTabs = false;
		        
			    for (var i = cssCustomPropertyIndex - 1; i >= 0; i--)
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
	        
		    #endregion
	        
	        foreach (var span in sfumatoCssBlock.EnumerateCssCustomProperties())
		        if (SfumatoBlockItems.TryAdd(span.Property.ToString(), span.Value.ToString()) == false)
		        {
			        SfumatoBlockItems[span.Property.ToString()] = span.Value.ToString();
		        }

	        foreach (var span in sfumatoCssBlock.EnumerateCssClassAndAtBlocks())
		        if (SfumatoBlockItems.TryAdd(span.Header.ToString(), span.Body.ToString().TrimEnd(';').Trim()) == false)
		        {
			        SfumatoBlockItems[span.Header.ToString()] = span.Body.ToString().TrimEnd(';').Trim();
		        }

	        foreach (var span in sfumatoCssBlock.EnumerateCustomVariants())
		        if (SfumatoBlockItems.TryAdd($"@custom-variant {span.Name}", span.Content.ToString().TrimEnd(';')) == false)
		        {
			        SfumatoBlockItems[$"@custom-variant {span.Name}"] = span.Content.ToString().TrimEnd(';');
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
	    var workingSb = AppRunner?.AppState.StringBuilderPool.Get();

	    if (workingSb is null)
		    return;

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
			    var paths = pathsValue.ConsolidateSpaces(workingSb).TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (paths.Length != 0)
			    {
				    foreach (var p in paths)
				    {
					    Paths.Add(p.Trim('\"'));
					    AbsolutePaths.Add(Path.GetFullPath(Path.Combine(NativeCssFilePathOnly, p.Trim('\"'))));
				    }
			    }
		    }

		    if (SfumatoBlockItems.TryGetValue("--not-paths", out var notPathsValue))
		    {
			    var notPaths = notPathsValue.ConsolidateSpaces(workingSb).TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (notPaths.Length != 0)
			    {
				    foreach (var p in notPaths)
				    {
					    NotPaths.Add(p.Trim('\"'));
					    AbsoluteNotPaths.Add(Path.GetFullPath(Path.Combine(NativeCssFilePathOnly, p.Trim('\"'))));
				    }
			    }
		    }

		    if (SfumatoBlockItems.TryGetValue("--not-folder-names", out var notFolderNamesValue))
		    {
			    var notFolderNames = notFolderNamesValue.ConsolidateSpaces(workingSb).TrimStart('[').TrimEnd(']').Trim().Replace("\", \"", "\",\"").Split("\",\"", StringSplitOptions.RemoveEmptyEntries);

			    if (notFolderNames.Length != 0)
			    {
				    foreach (var p in notFolderNames)
					    NotFolderNames.Add(p.Trim('\"'));
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
	    finally
	    {
		    AppRunner?.AppState.StringBuilderPool.Return(workingSb);
		}
    }

    /// <summary>
    /// Read all nested partial references (e.g. @import "...") from ProcessedCssContent
    /// and replace import statements with partial file content.
    /// </summary>
    public void ImportPartials()
    {
	    Imports.Clear();
	    
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
				    
				    sb.Replace(importStatement, $"/* Could not import: {filePath} */");
			    }
			    else
			    {
				    Imports.Add(new FileInfo(filePath));

				    var childCss = File.ReadAllText(filePath);
				    var injectedCss = ImportPartials(childCss, Path.GetDirectoryName(filePath) ?? string.Empty);

				    sb.Replace(importStatement, injectedCss);
			    }

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