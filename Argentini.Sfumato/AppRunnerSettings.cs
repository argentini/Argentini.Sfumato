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
    
}