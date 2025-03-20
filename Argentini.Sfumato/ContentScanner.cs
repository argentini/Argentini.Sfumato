// ReSharper disable ConvertIfStatementToSwitchStatement

using Argentini.Sfumato.Entities;

namespace Argentini.Sfumato;

public static partial class ContentScanner
{
    #region Regular Expressions
    
    public const string PatternQuotedStrings =
        """
        (?<delim>(\\")|["'`])
        (?<content>(?:\\.|(?!\k<delim>).){3,}?)
        \k<delim>
        """;

    private const string PatternQuotedSubstrings = @"\S+";

    private const string PatternCssCustomPropertyAssignment = @"^--[\w-]+\s*:\s*[^;]+;?$";
    
    private const string SplitByColons = @":(?!(?:[^\[\]]*\]))(?!(?:[^\(\)]*\)))";

    private const string SplitByHyphens = @"-(?!(?:[^\[\]]*\]))(?!(?:[^\(\)]*\)))";

    private const string SplitBySlashes = @"/(?!(?:[^\[\]]*\]))(?!(?:[^\(\)]*\)))";

    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    public static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    public static partial Regex UtilityClassRegex();

    [GeneratedRegex(PatternCssCustomPropertyAssignment, RegexOptions.Compiled)]
    public static partial Regex PatternCssCustomPropertyAssignmentRegex();

    [GeneratedRegex(SplitByColons, RegexOptions.Compiled)]
    public static partial Regex SplitByColonsRegex();

    [GeneratedRegex(SplitByHyphens, RegexOptions.Compiled)]
    public static partial Regex SplitByHyphensRegex();

    [GeneratedRegex(SplitBySlashes, RegexOptions.Compiled)]
    public static partial Regex SplitBySlashesRegex();

    #endregion
    
    #region File Parsing Methods
    
    public static Dictionary<string,CssClass> ScanFileForUtilityClasses(string fileContent, AppState appState)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new List<string>();

        ScanForQuotedStrings(fileContent, quotedSubstrings);

        var results = new Dictionary<string,CssClass>();

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
            if (match.Value.GetLikelyUtilityClass(appState) is { } cssClass)
            {
                // todo: fully validate class
                
                
                
                
                
                results.TryAdd(match.Value, cssClass);
            }
        }
    }

    // ReSharper disable ConvertIfStatementToReturnStatement
    public static CssClass? GetLikelyUtilityClass(this string input, AppState appState)
    {
        var cssClass = new CssClass(appState, input);

        #region Validate bracketed arbitrary CSS
        
        if (cssClass.AllSegments[^1][0] == '[' && cssClass.AllSegments[^1][^1] == ']')
        {
            if (PatternCssCustomPropertyAssignmentRegex().Match(cssClass.AllSegments[^1].TrimStart('[').TrimEnd(']')).Success)
                return cssClass;

            if (appState.Library.CssPropertyNamesWithColons.Any(substring => cssClass.AllSegments[^1].Contains(substring, StringComparison.Ordinal)))
                return cssClass;

            return null;
        }

        #endregion
        
        #region Validate static and utility classes

        if (appState.Library.StaticClasses.TryGetValue(cssClass.AllSegments[^1], out var definition))
        {
            cssClass.ClassDefinition = definition;

            return cssClass;
        }

        if (appState.Library.ScannerClassNamePrefixes.Any(key => cssClass.AllSegments[^1].StartsWith(key, StringComparison.Ordinal)))
            return cssClass;
        
        return null;
        
        #endregion
    }
    
    #endregion
}