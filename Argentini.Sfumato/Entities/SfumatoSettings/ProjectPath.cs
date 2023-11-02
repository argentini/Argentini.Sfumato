namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class ProjectPath
{
    public string Path { get; set; } = string.Empty;
    public string FileSpec { get; set; } = "*.html";
    public bool Recurse { get; set; } = false;
    public bool IsFilePath { get; set; }
}
