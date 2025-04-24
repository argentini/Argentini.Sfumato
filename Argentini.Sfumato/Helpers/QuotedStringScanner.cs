namespace Argentini.Sfumato.Helpers;

public static class QuotedStringScanner
{
    /// <summary>
    /// Scans <paramref name="source"/> and returns every whitespace‑/punctuation‑delimited
    /// token that appears inside any quoted literal (plain, verbatim, multi‑quote raw, or
    /// JavaScript template‑literal).
    /// </summary>
    public static IReadOnlyCollection<string> Scan(string? source)
    {
        if (string.IsNullOrEmpty(source))
            return [];

        var results = new HashSet<string>(StringComparer.Ordinal);
        var i = 0;
        var n = source.Length;

        while (i < n)
        {
            // consume optional $, @ prefix
            while (i < n && (source[i] == '$' || source[i] == '@'))
                i++;
            
            if (i >= n)
                break;

            var opener = source[i];

            if (opener is not ('"' or '\'' or '`'))
            {
                i++;
                
                continue;
            }

            // run length of identical opener chars (", """, etc.)
            var quoteLen = 0;
            
            while (i + quoteLen < n && source[i + quoteLen] == opener)
                quoteLen++;
            
            i += quoteLen;
            
            var contentStart = i;

            var backtick = opener == '`';
            var multi    = opener == '"' && quoteLen >= 2;         // $"" … "" or $""" … """
            var verbatim = source[i - quoteLen - 1] == '@' && opener == '"';

            if (backtick)
            {
                var depth = 0;

                while (i < n)
                {
                    if (source[i] == '\\')
                    {
                        i += 2;
                        
                        continue;
                    }
                    
                    if (depth == 0 && source[i] == '`')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        i++;
                        
                        break;
                    }
                    
                    // ReSharper disable once ConvertIfStatementToSwitchStatement
                    if (depth == 0 && source[i] == '$' && i + 1 < n && source[i + 1] == '{')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        depth = 1;
                        i += 2;
                        
                        continue;
                    }
                    
                    if (depth > 0)
                    {
                        switch (source[i])
                        {
                            case '{':
                                depth++;
                                break;
                            case '}':
                                depth--;
                                break;
                            case '"' or '\'' or '`':
                                SkipSimpleString(source[i]);
                                break;
                        }
                    }

                    i++;
                }

                continue;
            }

            if (verbatim)
            {
                while (i < n)
                {
                    if (source[i] == '"' && i + 1 < n && source[i + 1] == '"')
                    {
                        i += 2;

                        continue;
                    }
                    
                    if (source[i] == '"')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        i++;
                        
                        break;
                    }
                    
                    i++;
                }

                continue;
            }

            if (multi)
            {
                while (i < n)
                {
                    if (source[i] != '"')
                    {
                        i++;
                        
                        continue;
                    }

                    var run = 0;
                    var k = i;

                    while (k < n && source[k] == '"')
                    {
                        run++;
                        k++;
                    }

                    if (run >= quoteLen)
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        i = k;
                        
                        break;
                    }
                    
                    i = k; // keep scanning
                }

                continue;
            }

            // ordinary "…" or '…'
            while (i < n)
            {
                if (source[i] == '\\')
                {
                    i += 2;

                    continue;
                }
                
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
            i++; // past the opener
            
            while (i < n)
            {
                if (source[i] == '\\')
                {
                    i += 2;
                    
                    continue;
                }

                if (source[i] == q)
                {
                    i++;
                    
                    break;
                }

                i++;
            }
        }

        static void SplitAndAdd(ReadOnlySpan<char> span, HashSet<string> bag)
        {
            var start = -1;
            
            for (var k = 0; k < span.Length; k++)
            {
                if (IsDelim(span[k]))
                {
                    if (start == -1)
                        continue;
                    
                    bag.Add(span[start..k].ToString());
                    start = -1;
                }
                else if (start == -1)
                {
                    start = k; // begin a token
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
