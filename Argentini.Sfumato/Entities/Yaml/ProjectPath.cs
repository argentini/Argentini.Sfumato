using YamlDotNet.Serialization;

namespace Argentini.Sfumato.Entities.Yaml;

public sealed class ProjectPath
{
    public string Path { get; set; } = string.Empty;
    private string _extensions = "html,htm,shtml,cshtml,razor,js,ts,jsx,tsx,vue,erb,php,tpl,twig,jsp,pug,hbs,handlebars,svelte,gohtml,tmpl,mustache";
    public string Extensions
    {
        get => _extensions;
        set
        {
            _extensions = value.ToLower();

            SetExtensionsList();
        }
    }
    private string _ignoreFolders = string.Empty;
    public string IgnoreFolders
    {
        get => _ignoreFolders;
        set
        {
            _ignoreFolders = value.ToLower();

            SetIgnoreFoldersList();
        }
    }
    public bool Recurse { get; set; } = true;
    
    [YamlIgnore]
    public List<string> ExtensionsList { get; } = new();

    [YamlIgnore]
    public List<string> IgnoreFoldersList { get; } = new();

    public ProjectPath()
    {
        SetExtensionsList();
    }

    private void SetExtensionsList()
    {
        ExtensionsList.Clear();
            
        foreach (var extension in _extensions.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var ext = extension.TrimStart('.').Trim();
                    
            if (ExtensionsList.Contains(ext) == false)
                ExtensionsList.Add(ext);
        }
    }

    private void SetIgnoreFoldersList()
    {
        IgnoreFoldersList.Clear();
            
        foreach (var ignoreFolder in _ignoreFolders.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var folder = ignoreFolder.Trim();
                    
            if (IgnoreFoldersList.Contains(folder) == false)
                IgnoreFoldersList.Add(folder);
        }
    }

    public static string GetMatchingFileExtension(string? fileName, List<string> extensionsList)
    {
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;
            
        foreach (var extension in extensionsList)
        {
            if (fileName.EndsWith(extension))
                return extension;
        }

        return string.Empty;
    }
}
