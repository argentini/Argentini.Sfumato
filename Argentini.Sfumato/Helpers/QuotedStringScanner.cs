namespace Argentini.Sfumato.Helpers;

public static class QuotedStringScanner
{
    // ReSharper disable once NotAccessedPositionalProperty.Global
    public readonly record struct Hit(int Start, int Length, string Delimiter);

    /// <summary>
    /// Scans <paramref name="source"/> and yields every quoted substring.
    /// Recognized sequences:  " … "   ' … '   ` … `   \" … \"
    /// </summary>
    public static IEnumerable<Hit> Scan(string source)
    {
        if (string.IsNullOrEmpty(source))
            yield break;

        var n = source.Length;

        for (var i = 0; i < n;)
        {
            var escStart = source[i] == '\\' && i + 1 < n && source[i + 1] == '"';
            var quote = escStart ? '"' : source[i];

            // not a real opener? → skip
            if (escStart == false && quote is not ('"' or '\'' or '`'))
            {
                i++;
                continue;
            }

            var contentStart = i + (escStart ? 2 : 1);
            var j = contentStart;

            while (j < n)
            {
                if (source[j] == '\\')
                {
                    j += 2; // skip escape + payload
                    continue;
                }

                var atCloser = escStart ? source[j] == '\\' && j + 1 < n && source[j + 1] == quote : source[j] == quote;

                if (atCloser)
                {
                    var len   = j - contentStart;
                    var dl = escStart ? "\\\"" : quote.ToString();

                    yield return new Hit(contentStart, len, dl);

                    i = j + (escStart ? 2 : 1); // resume scan
                    
                    goto NextOuter;
                }

                j++;
            }

            // unterminated literal → stop
            yield break;

        NextOuter: ;
        }
    }
}