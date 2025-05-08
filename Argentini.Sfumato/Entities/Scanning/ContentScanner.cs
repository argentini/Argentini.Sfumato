namespace Argentini.Sfumato.Entities.Scanning;

public static class ContentScanner
{
    #region File Parsing Methods
    
    public static Dictionary<string,CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new HashSet<string>(StringComparer.Ordinal);

        fileContent.ScanQuotedStrings(quotedSubstrings);

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