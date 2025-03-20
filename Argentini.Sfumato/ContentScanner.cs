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
    
    private const string SplitClassIntoSegments = @":(?!(?:[^\[\]]*\]))(?!(?:[^\(\)]*\)))";
    
    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    public static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    public static partial Regex UtilityClassRegex();

    [GeneratedRegex(PatternCssCustomPropertyAssignment, RegexOptions.Compiled)]
    public static partial Regex PatternCssCustomPropertyAssignmentRegex();

    [GeneratedRegex(SplitClassIntoSegments, RegexOptions.Compiled)]
    public static partial Regex SplitClassIntoSegmentsRegex();

    #endregion
    
    #region File Parsing Methods
    
    public static Dictionary<string,CssClass> ScanFileForUtilityClasses(string fileContent, Library library)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new List<string>();

        ScanForQuotedStrings(fileContent, quotedSubstrings);

        var results = new Dictionary<string,CssClass>();

        foreach (var quotedSubstring in quotedSubstrings)
            ScanStringForClasses(quotedSubstring, results, library);
        
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

    private static void ScanStringForClasses(string quotedString, Dictionary<string,CssClass> results, Library library)
    {
        foreach (Match match in UtilityClassRegex().Matches(quotedString))
        {
            if (match.Value.GetLikelyUtilityClass(library) is { } cssClass)
                results.TryAdd(match.Value, cssClass);
        }
    }

    // ReSharper disable ConvertIfStatementToReturnStatement
    public static CssClass? GetLikelyUtilityClass(this string input, Library library)
    {
        var cssClass = new CssClass
        {
            Name = input,
            NameSegments = new List<string>(SplitClassIntoSegmentsRegex().Split(input.TrimEnd('!'))).ToList()
        };

        #region Validate bracketed arbitrary CSS
        
        if (cssClass.NameSegments[^1][0] == '[' && cssClass.NameSegments[^1][^1] == ']')
        {
            if (PatternCssCustomPropertyAssignmentRegex().Match(cssClass.NameSegments[^1].TrimStart('[').TrimEnd(']')).Success)
                return cssClass;

            if (library.CssPropertyNamesWithColons.Any(substring => cssClass.NameSegments[^1].Contains(substring, StringComparison.Ordinal)))
                return cssClass;

            return null;
        }

        #endregion
        
        #region Validate static and utility classes
        
        if (library.ScannerClassNamePrefixes.Any(key => cssClass.NameSegments[^1].StartsWith(key, StringComparison.Ordinal)))
            return cssClass;
        
        return null;
        
        #endregion
    }
    
    #endregion
}