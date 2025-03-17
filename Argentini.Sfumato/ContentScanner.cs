using Argentini.Sfumato.Entities;

namespace Argentini.Sfumato;

public static partial class ContentScanner
{
    #region Regular Expressions
    
    private const string PatternQuotedStrings =
        """
        (?<delim>(\\")|["'`])
        (?<content>(?:\\.|(?!\k<delim>).){3,}?)
        \k<delim>
        """;

    private const string PatternQuotedSubstrings = @"\S+";

    private const string PatternCssCustomPropertyAssignment = @"^--[\w-]+\s*:\s*[^;]+;?$";
    
    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    private static partial Regex UtilityClassRegex();

    [GeneratedRegex(PatternCssCustomPropertyAssignment, RegexOptions.Compiled)]
    private static partial Regex PatternCssCustomPropertyAssignmentRegex();

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
    private static bool IsLikelyUtilityClass(this string input, Library library)
    {
        var root = input.TrimEnd('!'); // Strip important flag

        #region Differentiate arbitrary CSS from utility class modifiers
        
        if (root[^1] == ']')
        {
            var lastBracketIndex = root.LastIndexOf('[');

            if (lastBracketIndex == -1)
                return false;

            if (lastBracketIndex == 0 || root[lastBracketIndex - 1] == ':')
                root = root[lastBracketIndex..]; // Strip prefixes from arbitrary CSS
            else
                root = root[..lastBracketIndex]; // Strip modifier from utility class
        }

        #endregion
        
        #region Validate bracketed arbitrary CSS
        
        if (root[0] == '[' && root[^1] == ']')
        {
            if (PatternCssCustomPropertyAssignmentRegex().Match(root.TrimStart('[').TrimEnd(']')).Success)
                return true;

            if (library.CssPropertyNamesWithColons.Any(substring => root.Contains(substring, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }

        #endregion
        
        #region Validate static and utility classes
        
        root = root.Split(':', StringSplitOptions.RemoveEmptyEntries)[^1];

        if (library.UtilityClassPrefixes.Any(substring => root.Contains(substring, StringComparison.OrdinalIgnoreCase)))
            return true;

        if (library.StaticUtilityClasses.Any(substring => root.Contains(substring, StringComparison.OrdinalIgnoreCase)))
            return true;

        return false;
        
        #endregion
    }
    
    #endregion
}