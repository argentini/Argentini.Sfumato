// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable MemberCanBePrivate.Global

using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato;

public static partial class ContentScanner
{
    #region Regular Expressions
    
    public const string PatternQuotedStrings =
        """
        (?<delim>(\\")|["'`])
        (?<content>(?:(?!\k<delim>)[\s\S])*?)
        \k<delim>
        """;
    
    private const string PatternQuotedSubstrings = @"\S+";

    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    public static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    public static partial Regex UtilityClassRegex();
    
    #endregion
    
    #region File Parsing Methods
    
    public static Dictionary<string,CssClass> ScanFileForUtilityClasses(string fileContent, AppState appState)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new List<string>();

        ScanForQuotedStrings(fileContent, quotedSubstrings);

        var results = new Dictionary<string,CssClass>(StringComparer.Ordinal);

        foreach (var quotedSubstring in quotedSubstrings)
            ScanStringForClasses(quotedSubstring, results, appState);
        
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

    private static void ScanStringForClasses(string quotedString, Dictionary<string,CssClass> results, AppState appState)
    {
        foreach (Match match in UtilityClassRegex().Matches(quotedString))
        {
            var cssClass = new CssClass(appState, match.Value);
            
            if (cssClass.IsValid)
                results.TryAdd(match.Value, cssClass);
        }
    }
    
    #endregion
}