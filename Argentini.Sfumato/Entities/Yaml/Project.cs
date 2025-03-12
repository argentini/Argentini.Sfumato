namespace Argentini.Sfumato.Entities.Yaml;

public sealed class Project
{
    public string ProjectName { get; set; } = string.Empty;
    public List<ProjectPath> ProjectPaths { get; set;  } = [];
    public string DarkMode { get; set; } = "media";
    public bool UseAutoTheme { get; set; }
}