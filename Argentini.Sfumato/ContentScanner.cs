// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable MemberCanBePrivate.Global

using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato;

public static partial class ContentScanner
{
    #region Regular Expressions
    
    [GeneratedRegex(
        """
        (?<delim>(\\")|["'`])
        (?<content>(?:(?!\k<delim>)[\s\S])*?)
        \k<delim>
        """, RegexOptions.IgnorePatternWhitespace)]
    public static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(@"\S+")]
    public static partial Regex UtilityClassRegex();
    
    #endregion
    
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

        ScanForQuotedStrings(fileContent, quotedSubstrings);

        var results = new Dictionary<string,CssClass>(StringComparer.Ordinal);

        foreach (var quotedSubstring in quotedSubstrings)
            ScanStringForClasses(quotedSubstring, results, appRunner);
        
        return results;
    }
    
    private static void ScanForQuotedStrings(string input, List<string> quotedSubstrings)
    {
        var quoteMatches = QuotedStringsRegex().Matches(input);

        foreach (Match qm in quoteMatches)
        {
            quotedSubstrings.Add(qm.Groups["content"].Value);
            
            if (qm.Groups["content"].Value.Contains('\"') || qm.Groups["content"].Value.Contains('\'') || qm.Groups["content"].Value.Contains('`'))
                ScanForQuotedStrings(qm.Groups["content"].Value, quotedSubstrings);
        }
    }

    private static void ScanStringForClasses(string quotedString, Dictionary<string,CssClass> results, AppRunner appRunner)
    {
        foreach (Match match in UtilityClassRegex().Matches(quotedString))
        {
            var cssClass = new CssClass(appRunner, match.Value);
            
            if (cssClass.IsValid)
                results.TryAdd(match.Value, cssClass);
        }
    }
    
    #endregion
}