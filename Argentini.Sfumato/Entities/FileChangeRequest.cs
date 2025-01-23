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
    public List<string> IgnoreFoldersList { get; } = [];
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
    
    public bool IsIgnorePath(string? relativeFilePath)
    {
        if (string.IsNullOrEmpty(relativeFilePath))
            return false;

        var segments = relativeFilePath.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length < 2)
            return false;

        if (IgnoreFoldersList.Any(s => segments.Contains(s, StringComparer.CurrentCultureIgnoreCase)))
            return true;

        return false;
    }
}