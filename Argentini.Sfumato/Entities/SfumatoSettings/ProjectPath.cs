namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class ProjectPath
{
    public string Path { get; set; } = string.Empty;
    public string Extension { get; set; } = "html";
    public bool Recurse { get; set; }
}
