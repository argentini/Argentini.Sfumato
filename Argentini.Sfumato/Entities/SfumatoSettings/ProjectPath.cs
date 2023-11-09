using YamlDotNet.Serialization;

namespace Argentini.Sfumato.Entities.SfumatoSettings;

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
    public bool Recurse { get; set; } = true;
    
    [YamlIgnore]
    public List<string> ExtensionsList { get; } = new();

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
