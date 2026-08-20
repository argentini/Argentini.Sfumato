// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.Scanning;

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
    
    public string FileName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;

    public Dictionary<string, CssClass> UtilityClasses { get; set; } = new();

    public ScannedFile(string filePath)
    {
        AbsoluteFilePath = filePath;
    }
    
    public async Task LoadAndScanFileAsync(AppRunner appRunner)
    {
        var fileContent = await Storage.ReadAllTextWithRetriesAsync(AbsoluteFilePath, 5000);

        UtilityClasses = string.IsNullOrEmpty(fileContent)
            ? []
            : ContentScanner.ScanFileForUtilityClasses(fileContent, appRunner);
    }
}
