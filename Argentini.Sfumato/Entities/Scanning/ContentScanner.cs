namespace Argentini.Sfumato.Entities.Scanning;

public static class ContentScanner
{
    #region File Parsing Methods
    
    public static Dictionary<string, CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner, bool fromRazorFile = false)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var sb = appRunner.StringBuilderPool.Get();
        var quotedSubstrings = new HashSet<string>(StringComparer.Ordinal);

        try
        {
            fileContent.ScanForUtilities(quotedSubstrings, appRunner.Library.ScannerClassNamePrefixes, sb);

            var results = new Dictionary<string, CssClass>(StringComparer.Ordinal);

            foreach (var utilityClass in quotedSubstrings)
                ScanStringForClasses(utilityClass, results, appRunner, fromRazorFile);

            return results;
        }
        finally
        {
            appRunner.StringBuilderPool.Return(sb);
        }
    }

    private static void ScanStringForClasses(string utilityClass, Dictionary<string, CssClass> results, AppRunner appRunner, bool fromRazorFile)
    {
        var cssClass = new CssClass(appRunner, utilityClass, fromRazorFile);
        
        if (cssClass.IsValid)
            results.TryAdd(utilityClass, cssClass);
    }
    
    #endregion
}