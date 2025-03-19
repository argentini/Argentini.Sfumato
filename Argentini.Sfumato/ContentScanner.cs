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
    
    public static HashSet<string> ScanFileForUtilityClasses(string fileContent, Library library)
    {
        if (string.IsNullOrEmpty(fileContent))
            return [];
        
        var quotedSubstrings = new List<string>();
        var results = new HashSet<string>();

        ScanForQuotedStrings(fileContent, quotedSubstrings);
        
        foreach (var quotedSubstring in quotedSubstrings)
            ScanStringForClasses(quotedSubstring, results, library);
        
        return results;
    }
    
    private static void ScanForQuotedStrings(string input, List<string> results)
    {
        var quoteMatches = QuotedStringsRegex().Matches(input);

        foreach (Match qm in quoteMatches)
        {
            results.Add(qm.Groups["content"].Value);
            
            if (qm.Groups["content"].Value.Contains('\"') || qm.Groups["content"].Value.Contains('\'') || qm.Groups["content"].Value.Contains('`'))
                ScanForQuotedStrings(qm.Groups["content"].Value, results);
        }
    }

    private static void ScanStringForClasses(string quotedString, HashSet<string> results, Library library)
    {
        results.UnionWith(UtilityClassRegex()
            .Matches(quotedString)
            .Where(m => m.Value.IsLikelyUtilityClass(library))
            .Select(m => m.Value));
    }

    // ReSharper disable ConvertIfStatementToReturnStatement
    public static bool IsLikelyUtilityClass(this string input, Library library)
    {
        // dark:group-[.is-published]:[&.active]:tabp:hover:text-[1rem]/6!
        // dark
        // group-[.is-published]
        // [&.active]
        // tabp
        // hover
        // text-[1rem]/6

        // dark:group-[.is-published]:[&.active]:tabp:hover:text-[color:var(--my-color-var)]
        // dark
        // group-[.is-published]
        // [&.active]
        // tabp
        // hover
        // text-[color
        // var(--my-color-var)]

        // dark:group-[.is-published]:[&.active]:tabp:hover:[font-weight:700]!
        // dark
        // group-[.is-published]
        // [&.active]
        // tabp
        // hover
        // [font-weight: 700]

        // dark:group-[.is-published]:[&.active]:tabp:hover:text-(length:--my-text-var)
        // dark
        // group-[.is-published]
        // [&.active]
        // tabp
        // hover
        // text-(length:--my-text-var)

        // dark:group-[.is-published]:[&.active]:tabp:hover:text-[color:var(--my-color-var)]/[0.1]
        // dark
        // group-[.is-published]
        // [&.active]
        // tabp
        // hover
        // text-[color
        // var(--my-color-var)]/[0.1]

        var segments = new List<string>(SplitClassIntoSegmentsRegex().Split(input.TrimEnd('!'))).ToList();

        #region Validate bracketed arbitrary CSS
        
        if (segments[^1][0] == '[' && segments[^1][^1] == ']')
        {
            if (PatternCssCustomPropertyAssignmentRegex().Match(segments[^1].TrimStart('[').TrimEnd(']')).Success)
                return true;

            if (library.CssPropertyNamesWithColons.Any(substring => segments[^1].Contains(substring, StringComparison.Ordinal)))
                return true;

            return false;
        }

        #endregion
        
        #region Validate static and utility classes
        
        if (library.ScannerClassNamePrefixes.Any(key => segments[^1].StartsWith(key, StringComparison.Ordinal)))
            return true;
        
        return false;
        
        #endregion
    }
    
    #endregion
}