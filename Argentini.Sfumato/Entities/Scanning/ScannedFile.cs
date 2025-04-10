using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato.Entities.Scanning;

public sealed class ScannedFile
{
    public string AbsoluteFilePath { get; set; } = string.Empty;
    public string FileContent { get; set; } = string.Empty;

    public string FileName => Path.GetFileName(AbsoluteFilePath) ?? string.Empty;
    public string FilePath => Path.GetDirectoryName(AbsoluteFilePath) ?? string.Empty;
    
    public Dictionary<string,CssClass> UtilityClasses { get; set; } = new(StringComparer.Ordinal);
}