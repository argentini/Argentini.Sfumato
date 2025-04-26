namespace Argentini.Sfumato.Helpers;

public static class CssClassAndAtBlockScanner
{
    /// <summary>
    /// Enumerates every .class or @kind block.
    /// Header – everything before (but not including) the opening brace, trimmed of trailing whitespace
    /// Body – the code including any outer braces.
    /// </summary>
    public static CssBlockEnumerable EnumerateCssClassAndAtBlocks(this string? css) => new(css);

    public readonly ref struct CssBlockMatch
    {
        public readonly ReadOnlySpan<char> Header; // No trailing WS
        public readonly ReadOnlySpan<char> Body; // Includes braces

        internal CssBlockMatch(ReadOnlySpan<char> header, ReadOnlySpan<char> body)
        {
            Header = header;
            Body   = body;
        }
    }

    public readonly ref struct CssBlockEnumerable
    {
        private readonly ReadOnlySpan<char> _src;
        internal CssBlockEnumerable(string? css) => _src = css is null ? ReadOnlySpan<char>.Empty : css.AsSpan();

        public Enumerator GetEnumerator() => new(_src);

        public ref struct Enumerator(ReadOnlySpan<char> src)
        {
            private readonly ReadOnlySpan<char> _s = src;
            private int _i = 0;
            private CssBlockMatch _current = default;

            public CssBlockMatch Current => _current;

            public bool MoveNext()
            {
                var s = _s;
                var n = s.Length;

                while (_i < n)
                {
                    var ch = s[_i];

                    // ReSharper disable once ConvertIfStatementToSwitchStatement
                    if (ch == '.')
                    {
                        var headerStart = _i++;
                        
                        while (_i < n && IsIdentChar(s[_i]))
                            _i++;

                        if (_i == headerStart + 1)
                        {
                            _i++;
                            continue;
                        }

                        var headerEnd = _i;
                        
                        SkipWs(ref _i, s);

                        if (_i >= n || s[_i] != '{')
                        {
                            _i++;
                            continue;
                        }

                        var bracePos = _i;
                        var bodyEnd  = FindMatchingBrace(bracePos + 1, s);
                        
                        if (bodyEnd < 0)
                            return false;

                        _current = new CssBlockMatch(s.Slice(headerStart, headerEnd - headerStart), s.Slice(bracePos, bodyEnd - bracePos + 1));

                        _i = bodyEnd + 1;

                        return true;
                    }

                    if (ch == '@')
                    {
                        var headerStart = _i++;
                        
                        while (_i < n && IsIdentChar(s[_i]))
                            _i++;

                        if (_i == headerStart + 1)
                        {
                            _i++;
                            continue;
                        }

                        SkipWs(ref _i, s);

                        while (_i < n && IsIdentChar(s[_i]))
                            _i++;
                        
                        var headerEnd = _i;
                        
                        SkipWs(ref _i, s);

                        if (_i >= n || s[_i] != '{')
                        {
                            _i++;
                            continue;
                        }
                        
                        var bracePos = _i;
                        var bodyEnd  = FindMatchingBrace(bracePos + 1, s);
                        
                        if (bodyEnd < 0) return false;

                        _current = new CssBlockMatch(s.Slice(headerStart, headerEnd - headerStart), s.Slice(bracePos, bodyEnd - bracePos + 1));

                        _i = bodyEnd + 1;

                        return true;
                    }

                    _i++; // Ordinary char
                }

                return false;
            }

            private static bool IsIdentChar(char c) => char.IsLetterOrDigit(c) || c == '_' || c == '-';

            private static void SkipWs(ref int i, ReadOnlySpan<char> s)
            {
                while (i < s.Length && char.IsWhiteSpace(s[i])) i++;
            }

            private static int FindMatchingBrace(int pos, ReadOnlySpan<char> s)
            {
                var depth = 1;

                while (pos < s.Length)
                {
                    var rel = s[pos..].IndexOfAny('{', '}');
                    
                    if (rel < 0)
                        break;

                    pos += rel;
                    depth += s[pos] == '{' ? 1 : -1;
                    
                    if (depth == 0)
                        return pos;
                    
                    pos++;
                }

                return -1;
            }
        }
    }
}