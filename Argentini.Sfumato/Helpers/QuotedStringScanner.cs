namespace Argentini.Sfumato.Helpers;

public static class QuotedStringScanner
{
    /// <summary>
    /// Scans <paramref name="source"/> and returns every whitespace‑/punctuation‑delimited
    /// token that appears inside any quoted literal (plain, verbatim, multi‑quote raw, or
    /// JavaScript template‑literal). **No allow‑list or exclusion filtering is applied** –
    /// the routine merely breaks on obvious delimiter characters so composite strings like
    /// <c>text‑sm/1"${myDate...</c> are no longer produced.
    /// </summary>
    public static IReadOnlyCollection<string> Scan(string? source)
    {
        if (string.IsNullOrEmpty(source))
            return Array.Empty<string>();

        var results = new HashSet<string>(StringComparer.Ordinal);
        int i = 0, n = source.Length;

        /* ------------------------------------------------ main walker --------- */
        while (i < n)
        {
            // consume optional $, @ prefix
            while (i < n && (source[i] == '$' || source[i] == '@')) i++;
            if (i >= n) break;

            char opener = source[i];
            if (opener is not ('"' or '\'' or '`')) { i++; continue; }

            // run length of identical opener chars (", """, etc.)
            int quoteLen = 0;
            while (i + quoteLen < n && source[i + quoteLen] == opener) quoteLen++;
            i += quoteLen;
            int contentStart = i;

            bool backtick = opener == '`';
            bool multi    = opener == '"' && quoteLen >= 2;         // $"" … "" or $""" … """
            bool verbatim = source[i - quoteLen - 1] == '@' && opener == '"';

            if (backtick)
            {
                int depth = 0;
                while (i < n)
                {
                    if (source[i] == '\\')                   { i += 2; continue; }
                    if (depth == 0 && source[i] == '`')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        i++; break;
                    }
                    if (depth == 0 && source[i] == '$' && i + 1 < n && source[i + 1] == '{')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        depth = 1; i += 2; continue;
                    }
                    if (depth > 0)
                    {
                        if      (source[i] == '{') depth++;
                        else if (source[i] == '}') depth--;
                        else if (source[i] is '"' or '\'' or '`') SkipSimpleString(source[i]);
                        i++;
                        continue;
                    }
                    i++;
                }
                continue;
            }

            if (verbatim)
            {
                while (i < n)
                {
                    if (source[i] == '"' && i + 1 < n && source[i + 1] == '"') { i += 2; continue; }
                    if (source[i] == '"')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        i++; break;
                    }
                    i++;
                }
                continue;
            }

            if (multi)
            {
                while (i < n)
                {
                    if (source[i] != '"') { i++; continue; }

                    int run = 0, k = i;
                    while (k < n && source[k] == '"') { run++; k++; }

                    if (run >= quoteLen)
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        i = k; break;
                    }
                    i = k; // keep scanning
                }
                continue;
            }

            // ordinary "…" or '…'
            while (i < n)
            {
                if (source[i] == '\\') { i += 2; continue; }
                if (source[i] == opener)
                {
                    SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                    break;
                }
                i++;
            }
        }

        return results;

        void SkipSimpleString(char q)
        {
            i++;                               // past the opener
            while (i < n)
            {
                if (source[i] == '\\') { i += 2; continue; }
                if (source[i] == q)      { i++;      break; }
                i++;
            }
        }

        /* ------------------------------------------------ helpers ---------- */
        static void SplitAndAdd(ReadOnlySpan<char> span, HashSet<string> bag)
        {
            int start = -1;
            
            for (int k = 0; k < span.Length; k++)
            {
                if (IsDelim(span[k]))
                {
                    if (start != -1)
                    {
                        bag.Add(span[start..k].ToString());
                        start = -1;
                    }
                }
                else if (start == -1)
                {
                    start = k;                 // begin a token
                }
            }
            
            if (start != -1)
                bag.Add(span[start..].ToString());

            return;

            // delimiter characters that end a token (besides whitespace)
            //static bool IsDelim(char c) => char.IsWhiteSpace(c) || c is '"' or '\'' or '`' or '<' or '>' or '$' or '{' or '}' or '(' or ')' or '=';
            static bool IsDelim(char c) => char.IsWhiteSpace(c) || c is '"' or '\'' or '`' or '<' or '>' or '$' or '{' or '}' or '=';
        }
    }
}
