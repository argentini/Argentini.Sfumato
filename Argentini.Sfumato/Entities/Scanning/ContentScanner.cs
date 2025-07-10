namespace Argentini.Sfumato.Entities.Scanning;

public static class ContentScanner
{
    #region File Parsing Methods
    
    public static Dictionary<string,CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner, bool fromRazorFile = false)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new HashSet<string>(StringComparer.Ordinal);
        var sb = appRunner.AppState.StringBuilderPool.Get();

        fileContent.ScanQuotedStrings(quotedSubstrings, sb);

        appRunner.AppState.StringBuilderPool.Return(sb);
        
        var results = new Dictionary<string,CssClass>(StringComparer.Ordinal);
        
        foreach (var quotedSubstring in quotedSubstrings)
            ScanStringForClasses(quotedSubstring, results, appRunner, fromRazorFile);
        
        return results;
    }

    private static void ScanStringForClasses(string quotedString, Dictionary<string,CssClass> results, AppRunner appRunner, bool fromRazorFile)
    {
        foreach (var substring in quotedString.SplitByNonWhitespace())
        {
            var cssClass = new CssClass(appRunner, substring, fromRazorFile);
            
            if (cssClass.IsValid)
                results.TryAdd(substring, cssClass);
        }
    }
    
    #endregion
}