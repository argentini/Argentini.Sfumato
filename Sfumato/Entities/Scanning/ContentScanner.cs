using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.Scanning;

public static class ContentScanner
{
    public static Dictionary<string, CssClass> ScanFileForUtilityClasses(string fileContent, AppRunner appRunner)
    {
		if (string.IsNullOrEmpty(fileContent))
			return [];

		var sb = appRunner.StringBuilderPool.Get();
		var candidates = new Dictionary<string, string?>(StringComparer.Ordinal);

		try
		{
			fileContent.ScanForUtilities(candidates, appRunner.Library.ScannerClassNamePrefixes, sb);

			var results = new Dictionary<string, CssClass>(candidates.Count, StringComparer.Ordinal);

			foreach (var candidate in candidates)
				appRunner.ScanStringForClasses(candidate, results);

			return results;
		}
		finally
		{
			appRunner.StringBuilderPool.Return(sb);
		}
    }

    private static void ScanStringForClasses(this AppRunner appRunner, KeyValuePair<string,string?> kvp, Dictionary<string, CssClass> results)
    {
        var cssClass = new CssClass(appRunner, selector: kvp.Key, prefix: kvp.Value);
        
        if (cssClass.IsValid)
            results.TryAdd(cssClass.Selector, cssClass);
    }
}
