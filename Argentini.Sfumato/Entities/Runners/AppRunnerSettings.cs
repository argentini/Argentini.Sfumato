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
		set
		{
			_cssContent = value;
			LineBreak = _cssContent.Contains("\r\n") ? "\r\n" : "\n";
		}
	}

	public bool UseMinify { get; set; }
	public bool UseReset { get; set; } = true;
	public bool UseForms { get; set; } = true;
	public bool UseDarkThemeClasses { get; set; }
	public bool UseCompatibilityMode { get; set; }

	public string NativeCssFilePath { get; private set; } = string.Empty;
	public string NativeCssFilePathOnly { get; private set; } = string.Empty;
	public string CssFileNameOnly { get; private set; } = string.Empty;
	public string NativeCssOutputFilePath { get; private set; } = string.Empty;
	public string LineBreak { get; private set; } = "\n";

	public Dictionary<string, CssImportFile> CssImports { get; } = [];
	
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
	    var workingSb = AppRunner?.AppState.StringBuilderPool.Get();

		if (sb is null || workingSb is null)
			return;

		try
		{
			if (string.IsNullOrEmpty(CssFilePath) == false)
				sb.Append(File.ReadAllText(Path.GetFullPath(CssFilePath.SetNativePathSeparators())).TrimWhitespaceBeforeLineBreaks(workingSb).RemoveBlockComments(workingSb));

			#region Extract Sfumato settings block

			(var start, var length) = sb.ExtractCssBlock("@layer sfumato");

			if (start == -1)
			{
				(start, length) = sb.ExtractCssBlock("@theme sfumato");

				if (start == -1)
				{
					Console.WriteLine($"{AppState.CliErrorPrefix}No @layer sfumato {{ :root {{ }} }} block in file: {CssFilePath}");
					Environment.Exit(1);
				}
			}

			CssContent = sb.ToString();
			SfumatoCssBlock = sb.Substring(start, length);

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
			AppRunner?.AppState.StringBuilderPool.Return(workingSb);
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
}