namespace Argentini.Sfumato;

public sealed partial class FileScanner
{
    public const string PatternQuotedStrings =
        """
        (?<delim>(\\")|["'`])
        (?<content>(?:\\.|(?!\k<delim>).){3,}?)
        \k<delim>
        """;
    
    public const string PatternQuotedSubstrings = @"\S+";

    public static List<string> ScanForQuotedStrings(string input)
    {
        var results = new List<string>();
        var quoteMatches = QuotedStringsRegex().Matches(input);

        foreach (Match qm in quoteMatches)
        {
            var subStringMatches = QuotedStringsRegex().Matches(qm.Groups["content"].Value);

            if (subStringMatches.Count > 0)
            {
                foreach (Match sqm in subStringMatches)
                    results.Add(sqm.Groups["content"].Value);
            }
            else
            {
                results.Add(qm.Groups["content"].Value);
            }
        }

        return results;
    }

    public static List<string> ScanForClasses(string quotedString)
    {
        var results = new List<string>();

        results.AddRange(UtilityClassRegex().Matches(quotedString).Select(m => m.Value));

        return results;
    }

    [GeneratedRegex(PatternQuotedStrings, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex QuotedStringsRegex();
    
    [GeneratedRegex(PatternQuotedSubstrings, RegexOptions.Compiled)]
    private static partial Regex UtilityClassRegex();
}