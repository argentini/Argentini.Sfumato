namespace Sfumato.Entities.Scanning;

/// <summary>
/// Enumerates every .class or @kind block in a StringBuilder,
/// returning index/length pairs for header and body.
/// </summary>
public static class CssClassAndAtBlockScannerExtensions
{
    public static CssBlockPositionEnumerable EnumerateCssClassAndAtBlockPositions(this StringBuilder? sb) => new(sb ?? new StringBuilder());
}

/// <summary>
/// Carries four ints: header start/length, body start/length.
/// </summary>
public readonly struct CssBlockPosition
{
    public int HeaderStart { get; }
    public int HeaderLength { get; }
    public int BodyStart { get; }
    public int BodyLength { get; }

    internal CssBlockPosition(int headerStart, int headerLength, int bodyStart, int bodyLength)
    {
        HeaderStart  = headerStart;
        HeaderLength = headerLength;
        BodyStart    = bodyStart;
        BodyLength   = bodyLength;
    }
}

public readonly struct CssBlockPositionEnumerable
{
    private readonly StringBuilder _sb;
    internal CssBlockPositionEnumerable(StringBuilder sb) => _sb = sb;

    public Enumerator GetEnumerator() => new(_sb);

    public struct Enumerator
    {
        private readonly StringBuilder _s;
        private int _i;
        private CssBlockPosition _current;

        internal Enumerator(StringBuilder sb)
        {
            _s        = sb;
            _i        = 0;
            _current  = default;
        }

        public CssBlockPosition Current => _current;

        public bool MoveNext()
        {
            var s   = _s;
            var n   = s.Length;
            var i   = _i;

            while (i < n)
            {
                var ch = s[i];

                // • .class block
                if (ch == '.')
                {
                    var headerStart = i++;
                    
                    // scan identifier
                    while (i < n && IsIdentChar(s[i]))
                        i++;
                    
                    if (i == headerStart + 1)
                    {
                        // lone dot
                        i++;
                        continue;
                    }
                    
                    var headerEnd = i;
                    SkipWs(ref i, s);

                    if (i >= n || s[i] != '{')
                    {
                        i++;
                        continue;
                    }

                    var bracePos = i;
                    var bodyEnd  = FindMatchingBrace(bracePos + 1, s);
                    
                    if (bodyEnd < 0)
                        return false;

                    _current = new CssBlockPosition(
                        headerStart,
                        headerEnd - headerStart,
                        bracePos,
                        bodyEnd - bracePos + 1
                    );
                    
                    _i = bodyEnd + 1;
                    
                    return true;
                }

                // • @at-rule block
                if (ch == '@')
                {
                    var headerStart = i++;
                    
                    // scan first ident (e.g. "@media")
                    while (i < n && IsIdentChar(s[i]))
                        i++;
                    
                    SkipWs(ref i, s);
                    
                    // optional second ident (e.g. "screen" in "@media screen")
                    while (i < n && IsIdentChar(s[i]))
                        i++;
                    
                    var headerEnd = i;
                    SkipWs(ref i, s);

                    if (i >= n || s[i] != '{')
                    {
                        i++;
                        continue;
                    }

                    var bracePos = i;
                    var bodyEnd  = FindMatchingBrace(bracePos + 1, s);

                    if (bodyEnd < 0)
                        return false;

                    _current = new CssBlockPosition(
                        headerStart,
                        headerEnd - headerStart,
                        bracePos,
                        bodyEnd - bracePos + 1
                    );

                    _i = bodyEnd + 1;
                    
                    return true;
                }

                i++;
            }

            _i = n;
            
            return false;
        }

        private static bool IsIdentChar(char c) => char.IsLetterOrDigit(c) || c == '_' || c == '-';

        private static void SkipWs(ref int i, StringBuilder s)
        {
            var n = s.Length;
            
            while (i < n && char.IsWhiteSpace(s[i]))
                i++;
        }

        private static int FindMatchingBrace(int pos, StringBuilder s)
        {
            var depth = 1;
            var n     = s.Length;

            while (pos < n)
            {
                var c = s[pos];

                if (c == '{')
                {
                    depth++;
                }
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0)
                        return pos;
                }
                
                pos++;
            }

            return -1;
        }
    }
}
