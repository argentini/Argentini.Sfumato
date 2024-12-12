namespace Argentini.Sfumato.Entities;

public sealed class FileChangeRequest
{
    private string _extensions = string.Empty;
    public string Extensions
    {
        get => _extensions;
        set
        {
            _extensions = value.ToLower();

            SetExtensionsList();
        }
    }
    public List<string> ExtensionsList { get; } = [];
    public FileSystemEventArgs? FileSystemEventArgs { get; set; }

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

    public bool IsMatchingFile(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return false;
            
        foreach (var extension in ExtensionsList)
        {
            if (fileName.EndsWith(extension))
                return true;
        }

        return false;
    }
}