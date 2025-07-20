namespace Argentini.Sfumato.Entities.Scanning;

public static class ContentScanner
{
    #region File Parsing Methods
    
    public static Dictionary<string, CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner, bool fromRazorFile = false)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var sb = appRunner.StringBuilderPool.Get();
        var bag = new Dictionary<string,string?>(StringComparer.Ordinal);

        try
        {
            fileContent.ScanForUtilities(bag, appRunner.Library.ScannerClassNamePrefixes, sb);

            var results = new Dictionary<string, CssClass>(StringComparer.Ordinal);

            foreach (var kvp in bag)
                appRunner.ScanStringForClasses(kvp, results, fromRazorFile);

            return results;
        }
        finally
        {
            appRunner.StringBuilderPool.Return(sb);
        }
    }

    private static void ScanStringForClasses(this AppRunner appRunner, KeyValuePair<string,string?> kvp, Dictionary<string, CssClass> results, bool fromRazorFile)
    {
        var cssClass = new CssClass(appRunner, selector: kvp.Key, fromRazorFile: fromRazorFile, prefix: kvp.Value);
        
        if (cssClass.IsValid)
            results.TryAdd(kvp.Key, cssClass);
    }
    
    #endregion
}