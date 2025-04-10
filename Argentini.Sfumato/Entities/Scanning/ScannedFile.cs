// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato.Entities.Scanning;

public sealed class ScannedFile
{
    private string _absoluteFilePath = string.Empty;
    public string AbsoluteFilePath
    {
        get => _absoluteFilePath;
        set
        {
            _absoluteFilePath = value;

            FileName = Path.GetFileName(value);
            FilePath = Path.GetDirectoryName(value) ?? string.Empty;
        }
    }
    
    public string FileContent { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;

    public bool IsValid { get; set; }

    public Dictionary<string,CssClass> UtilityClasses { get; set; } = new(StringComparer.Ordinal);

    public ScannedFile(string filePath)
    {
        AbsoluteFilePath = filePath;
    }
    
    public async Task LoadAndScanFileAsync(AppRunner appRunner)
    {
        FileContent = await Storage.ReadAllTextWithRetriesAsync(AbsoluteFilePath, 5000);
        UtilityClasses = ContentScanner.ScanFileForUtilityClasses(FileContent, appRunner);
    }
}