namespace Argentini.Sfumato.Helpers;

/// <summary>
/// Extension entry-point: foreach (var p in css.EnumerateCustomProperties())
/// </summary>
public static class CssCustomPropertyExtensions
{
    public static CssPropertyEnumerable EnumerateCssCustomProperties(this string? css, bool namesOnly = false) => new(css, namesOnly);
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
    private readonly bool _namesOnly;
    internal CssPropertyEnumerable(string? css, bool namesOnly)
    {
        _buffer = css is null ? ReadOnlySpan<char>.Empty : css.AsSpan();
        _namesOnly = namesOnly;
    }

    public Enumerator GetEnumerator() => new(_buffer, _namesOnly);

    public ref struct Enumerator
    {
        private readonly ReadOnlySpan<char> _s;
        private readonly bool _namesOnly;
        private int  _i;
        private int  _propStart, _propEnd;
        private int  _valStart,  _valEnd;

        internal Enumerator(ReadOnlySpan<char> source, bool namesOnly)
        {
            _s = source;
            _namesOnly = namesOnly; 
            _i = 0;
            _propStart = _propEnd = _valStart = _valEnd = 0;
        }

        public CssPropertySpan Current => new(_s.Slice(_propStart, _propEnd - _propStart), _s.Slice(_valStart , _valEnd  - _valStart));

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

                // Read property name
                var propStart = i;
                
                i += 2;
                
                while (i < len && (char.IsLetterOrDigit(s[i]) || s[i] is '_' or '-'))
                    i++;
                
                if (i == propStart + 2)
                    continue; // Just “--”

                var propEnd = i;

                // Skip whitespace, then see if this is a declaration (':') or a var-ref (')')
                while (i < len && char.IsWhiteSpace(s[i]))
                    i++;

                if (i >= len)
                    continue;

                var isVarRef = _namesOnly && s[i] == ')';
                var isDecl   = s[i] == ':';
                
                if (isVarRef == false && isDecl == false)
                    continue;

                if (_namesOnly)
                {
                    _propStart = propStart;
                    _propEnd   = propEnd;
                    _valStart  = _valEnd = 0; // Empty span in namesOnly
                    _i = isVarRef
                        ? i + 1 // Consume ')'
                        : propEnd; // Back up to after identifier to continue scanning value

                    return true;
                }

                // Consume the ':'
                i++;

                // Skip whitespace before the value
                while (i < len && char.IsWhiteSpace(s[i]))
                    i++;

                // Mark start of the value
                var valStart = i;
                var inS = false;
                var inD = false;
                var esc = false;

                // Scan until semicolon, respecting quotes and escapes
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
                        if (c == '\'')
                            inS = false;
                    }
                    else if (inD)
                    {
                        if (c == '"')
                            inD = false;
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

                // Trim trailing whitespace from the value
                var valEnd = i;
                
                while (valEnd > valStart && char.IsWhiteSpace(s[valEnd - 1]))
                    valEnd--;

                // Assign the spans
                _propStart = propStart;
                _propEnd   = propEnd;
                _valStart  = valStart;
                _valEnd    = valEnd;

                // Consume the ';' if present
                if (i < len && s[i] == ';')
                    i++;

                // Advance cursor and emit
                _i = i;

                return true;
            }

            _i = len;

            return false;
        }
    }
}