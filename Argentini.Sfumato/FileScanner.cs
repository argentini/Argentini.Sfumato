using Argentini.Sfumato.Entities;

namespace Argentini.Sfumato;

public static partial class FileScanner
{
    #region Regular Expressions
    
    private const string PatternQuotedStrings =
        """
        (?<delim>(\\")|["'`])
        (?<content>(?:\\.|(?!\k<delim>).){3,}?)
        \k<delim>
        """;

    private const string PatternQuotedSubstrings = @"\S+";

    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    private static partial Regex UtilityClassRegex();
    
    #endregion
    
    #region File Parsing Methods
    
    public static HashSet<string> ScanFileForClasses(string fileContent, Library library)
    {
        var results = new HashSet<string>();
        var quotedSubstrings = ScanForQuotedStrings(fileContent);
        
        foreach (var quotedSubstring in quotedSubstrings)
        {
            var localClasses = ScanStringForClasses(quotedSubstring, library);

            results.UnionWith(localClasses);
        }
        
        return results;
    }
    
    public static List<string> ScanForQuotedStrings(string input)
    {
        var results = new List<string>();
        var quoteMatches = QuotedStringsRegex().Matches(input);

        foreach (Match qm in quoteMatches)
        {
            results.Add(qm.Groups["content"].Value);
            
            if (qm.Groups["content"].Value.Contains('\"') || qm.Groups["content"].Value.Contains('\'') || qm.Groups["content"].Value.Contains('`'))
                results.AddRange(ScanForQuotedStrings(qm.Groups["content"].Value));
        }

        return results;
    }

    public static HashSet<string> ScanStringForClasses(string quotedString, Library library)
    {
        var results = new HashSet<string>();

        results.UnionWith(UtilityClassRegex()
            .Matches(quotedString)
            .Where(m => m.Value.IsLikelyUtilityClass(library))
            .Select(m => m.Value));

        return results;
    }

    private static bool IsLikelyUtilityClass(this string input, Library library)
    {
        if (input[0] == '[' && input[^1] == ']') // Arbitrary CSS
        {
            if (library.ValidCssPropertyNames.Any(substring => input.Contains(substring, StringComparison.OrdinalIgnoreCase)))
                return true;
        }

        if (library.UtilityClassPrefixes.Any(substring => input.Contains(substring, StringComparison.OrdinalIgnoreCase)))
            return true;

        if (library.StaticUtilityClasses.Any(substring => input.Contains(substring, StringComparison.OrdinalIgnoreCase)))
            return true;

        return false;
    }
    
    #endregion
}