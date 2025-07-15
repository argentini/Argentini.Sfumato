namespace Argentini.Sfumato.Entities.Scanning;

public static class ContentScanner
{
    #region File Parsing Methods
    
    public static Dictionary<string, CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner, bool fromRazorFile = false)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var sb = appRunner.AppState.StringBuilderPool.Get();
        var quotedSubstrings = new HashSet<string>(StringComparer.Ordinal);

        try
        {
            fileContent.ScanForUtilities(quotedSubstrings, sb);

            var results = new Dictionary<string, CssClass>();

            foreach (var quotedSubstring in quotedSubstrings)
                ScanStringForClasses(quotedSubstring, results, appRunner, fromRazorFile);

            return results;
        }
        finally
        {
            appRunner.AppState.StringBuilderPool.Return(sb);
        }
    }

    private static void ScanStringForClasses(string quotedString, Dictionary<string, CssClass> results, AppRunner appRunner, bool fromRazorFile)
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