namespace Sfumato.Entities.Scanning;

/// <summary>
/// Enumerates every declaration of the form "@custom-variant name(content);"
/// returning index/length pairs for Name and Content.
/// </summary>
public static class CustomVariantScannerExtensions
{
    public static CustomVariantPositionEnumerable EnumerateCustomVariantPositions(this StringBuilder? sb) => new(sb ?? new StringBuilder());
}

/// <summary>
/// Holds start+length for the identifier and the parenthesized content.
/// </summary>
public readonly struct CustomVariantPosition
{
    public int NameStart { get; }
    public int NameLength { get; }
    public int ContentStart { get; }
    public int ContentLength { get; }

    internal CustomVariantPosition(int nameStart, int nameLength, int contentStart, int contentLength)
    {
        NameStart = nameStart;
        NameLength = nameLength;
        ContentStart = contentStart;
        ContentLength = contentLength;
    }
}

public readonly struct CustomVariantPositionEnumerable
{
    private readonly StringBuilder _s;
    internal CustomVariantPositionEnumerable(StringBuilder sb) => _s = sb;
    public Enumerator GetEnumerator() => new(_s);

    public struct Enumerator
    {
        private readonly StringBuilder _s;
        private int _i;
        private CustomVariantPosition _current;
        private const string Keyword = "@custom-variant";

        internal Enumerator(StringBuilder sb)
        {
            _s = sb;
            _i = 0;
            _current = default;
        }

        public CustomVariantPosition Current => _current;

        public bool MoveNext()
        {
            var s = _s;
            var n = s.Length;
            var i = _i;

            while (i < n)
            {
                // find next '@'
                while (i < n && s[i] != '@')
                    i++;
                
                if (i >= n)
                    break;
                
                var headerStart = i;

                // check for the exact keyword
                if (n - headerStart < Keyword.Length)
                {
                    // not enough room
                    _i = headerStart + 1;
                    i = headerStart + 1;
                   
                    continue;
                }

                var match = true;

                for (var j = 0; j < Keyword.Length; j++)
                {
                    if (s[headerStart + j] != Keyword[j])
                    {
                        match = false;
                        break;
                    }
                }
                
                if (match == false)
                {
                    _i = headerStart + 1;
                    i = headerStart + 1;

                    continue;
                }

                // after keyword: skip WS, read identifier
                var pos = headerStart + Keyword.Length;
                
                while (pos < n && char.IsWhiteSpace(s[pos]))
                    pos++;

                var nameStart = pos;
                
                while (pos < n && (char.IsLetterOrDigit(s[pos]) || s[pos] == '_' || s[pos] == '-'))
                    pos++;

                if (pos == nameStart)
                {
                    // no name; skip this '@'
                    _i = headerStart + 1;
                    i = headerStart + 1;

                    continue;
                }

                var nameEnd = pos;

                // skip WS, expect '('
                while (pos < n && char.IsWhiteSpace(s[pos]))
                    pos++;
                
                if (pos >= n || s[pos] != '(')
                {
                    _i = headerStart + 1;
                    i = headerStart + 1;
                    continue;
                }

                // 5) parse parenthesized content (nested)
                pos++; // skip '('

                var contentStart = pos;
                var depth = 1;
                
                while (pos < n && depth > 0)
                {
                    var c = s[pos++];
                    
                    if (c == '(')
                        depth++;
                    else if (c == ')')
                        depth--;
                }

                if (depth != 0)
                    return false; // unbalanced

                var closeParPos = pos - 1;
                var contentEnd  = closeParPos;

                // trim trailing whitespace inside ( )
                while (contentEnd > contentStart && char.IsWhiteSpace(s[contentEnd - 1]))
                    contentEnd--;

                // skip WS, expect ';'
                while (pos < n && char.IsWhiteSpace(s[pos]))
                    pos++;
                
                if (pos >= n || s[pos] != ';')
                {
                    _i = headerStart + 1;
                    i = headerStart + 1;
                    continue;
                }

                // yield name _and_ keyword, so NameStart points at the '@'
                var matchLength = nameEnd - headerStart;
                
                _current = new CustomVariantPosition(
                    headerStart,
                    matchLength,
                    contentStart,
        contentEnd - contentStart
                );
                
                _i = pos + 1;
                
                return true;
            }

            _i = n;
            
            return false;
        }
    }
}
