// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable MemberCanBePrivate.Global

namespace Argentini.Sfumato.Entities.Scanning;

public static partial class ContentScanner
{
    #region Validation

    public static bool ValidForScan(this AppRunner appRunner, string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;

        var fileExtension = Path.GetExtension(filePath);
        
        if (appRunner.Library.ValidFileExtensions.Contains(fileExtension) == false)
            return false;

        var filePathSegments = filePath.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

        return appRunner.Library.IgnoreFolderNames.Any(filePathSegments.Contains) == false && File.Exists(filePath);
    }
    
    #endregion
    
    #region File Parsing Methods
    
    public static Dictionary<string,CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new List<string>();

        foreach (var match in QuotedStringScanner.Scan(fileContent))
            quotedSubstrings.Add(match);
        
        var results = new Dictionary<string,CssClass>(StringComparer.Ordinal);
        
        foreach (var quotedSubstring in quotedSubstrings)
            ScanStringForClasses(quotedSubstring, results, appRunner);
        
        return results;
    }

    private static void ScanStringForClasses(string quotedString, Dictionary<string,CssClass> results, AppRunner appRunner)
    {
        foreach (var substring in quotedString.SplitByNonWhitespace())
        {
            var cssClass = new CssClass(appRunner, substring);
            
            if (cssClass.IsValid)
                results.TryAdd(substring, cssClass);
        }
    }
    
    #endregion
}