// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverQueried.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.Runners;

public sealed class AppRunnerSettings
{
	#region Properties

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

	private string _cssOutputFilePath = string.Empty;
	public string CssOutputFilePath
	{
		get => _cssOutputFilePath;
		set
		{
			_cssOutputFilePath = value;

			if (string.IsNullOrEmpty(value))
				return;

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

	public Dictionary<string, CssImportFile> CssImports { get; } = new(StringComparer.Ordinal);
	
	public HashSet<string> Paths { get; } = new (StringComparer.Ordinal);
	public HashSet<string> AbsolutePaths { get; } = new (StringComparer.Ordinal);
	public HashSet<string> NotPaths { get; } = new (StringComparer.Ordinal);
	public HashSet<string> AbsoluteNotPaths { get; } = new (StringComparer.Ordinal);
	public HashSet<string> NotFolderNames { get; } = new (StringComparer.Ordinal);

    public Dictionary<string, string> SfumatoBlockItems { get; } = new(StringComparer.Ordinal);

    public Dictionary<string, double> BreakpointSizes { get; } = new(StringComparer.Ordinal);

    #endregion
}