namespace Sfumato.Entities.Scanning;

/// <summary>
/// Extension entry-point: foreach (var p in css.EnumerateCssCustomProperties())
/// </summary>
public static partial class CssCustomPropertyExtensions
{
    public static CssPropertyEnumerable EnumerateCssCustomProperties(this string? css) => new(css);
}

/// <summary>
/// Lightweight record that carries one declaration slice.
/// </summary>
public readonly ref struct CssPropertySpan(ReadOnlySpan<char> prop, ReadOnlySpan<char> val)
{
    public readonly ReadOnlySpan<char> Property = prop;
    public readonly ReadOnlySpan<char> Value = val;
}

/// <summary>
/// The stack-only enumerable / enumerator pair.
/// </summary>
public readonly ref struct CssPropertyEnumerable
{
    private readonly ReadOnlySpan<char> _buffer;

    internal CssPropertyEnumerable(string? css)
    {
        _buffer = css is null ? ReadOnlySpan<char>.Empty : css.AsSpan();
    }

    public Enumerator GetEnumerator() => new(_buffer);

    public ref struct Enumerator
    {
        private readonly ReadOnlySpan<char> _s;
        private int _i, _propStart, _propEnd, _valStart, _valEnd;

        internal Enumerator(ReadOnlySpan<char> source)
        {
            _s = source;
            _i = _propStart = _propEnd = _valStart = _valEnd = 0;
        }

        public CssPropertySpan Current
            => new(
                _s.Slice(_propStart, _propEnd - _propStart),
                _s.Slice(_valStart,  _valEnd   - _valStart)
               );

        public bool MoveNext()
        {
            var s = _s;
            var len = s.Length;
            var i = _i;

            for (; i < len - 1; i++)
            {
                // Fast-scan for “--”
                if (s[i] != '-' || s[i + 1] != '-')
                    continue;

                var propStart = i;

                i += 2;

                while (i < len && (char.IsLetterOrDigit(s[i]) || s[i] is '_' or '-'))
                    i++;

                if (i == propStart + 2)
                    continue; // just “--”

                var propEnd = i;

                // Skip whitespace, then require ':'
                while (i < len && char.IsWhiteSpace(s[i]))
                    i++;

                if (i >= len || s[i] != ':')
                    continue;

                // consume ':'
                i++;

                // skip whitespace before value
                while (i < len && char.IsWhiteSpace(s[i]))
                    i++;

                var valStart = i;
                var inS = false;
                var inD = false;
                var esc = false;

                // scan until ';', respecting quotes and escapes
                for (; i < len; i++)
                {
                    var c = s[i];

                    if (esc)
                    {
                        esc = false;
                        continue;
                    }
                    
                    if (c == '\\')
                    {
                        esc = true;
                        continue;
                    }
                    
                    if (inS)
                    {
                        if (c == '\'') inS = false;
                    }
                    else if (inD)
                    {
                        if (c == '"') inD = false;
                    }
                    else if (c == '\'')
                    {
                        inS = true;
                    }
                    else if (c == '"')
                    {
                        inD = true;
                    }
                    else if (c == ';')
                    {
                        break;
                    }
                }

                var valEnd = i;
                
                while (valEnd > valStart && char.IsWhiteSpace(s[valEnd - 1]))
                    valEnd--;

                _propStart = propStart;
                _propEnd = propEnd;
                _valStart = valStart;
                _valEnd = valEnd;

                if (i < len && s[i] == ';')
                    i++;

                _i = i;

                return true;
            }

            _i = len;

            return false;
        }
    }
}
