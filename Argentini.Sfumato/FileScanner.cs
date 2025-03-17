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

    private const string PatternCssCustomPropertyAssignment = @"^--[\w-]+\s*:\s*[^;]+;?$";
    
    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    private static partial Regex UtilityClassRegex();

    [GeneratedRegex(PatternCssCustomPropertyAssignment, RegexOptions.Compiled)]
    private static partial Regex PatternCssCustomPropertyAssignmentRegex();

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
        var root = input.TrimEnd('!');
        
        if (root[^1] == ']')
        {
            var lastBracketIndex = root.LastIndexOf('[');

            if (lastBracketIndex == -1)
                return false;

            if (lastBracketIndex == 0 || root[lastBracketIndex - 1] == ':')
                root = root[lastBracketIndex..];
            else
                root = root[..lastBracketIndex];
        }

        if (root[0] == '[' && root[^1] == ']') // Arbitrary CSS
        {
            if (PatternCssCustomPropertyAssignmentRegex().Match(root.TrimStart('[').TrimEnd(']')).Success)
                return true;

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (library.CssPropertyNamesWithColons.Any(substring => root.Contains(substring, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }

        root = root.Split(':', StringSplitOptions.RemoveEmptyEntries)[^1];

        if (library.UtilityClassPrefixes.Any(substring => root.Contains(substring, StringComparison.OrdinalIgnoreCase)))
            return true;

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (library.StaticUtilityClasses.Any(substring => root.Contains(substring, StringComparison.OrdinalIgnoreCase)))
            return true;

        return false;
    }
    
    #endregion
}