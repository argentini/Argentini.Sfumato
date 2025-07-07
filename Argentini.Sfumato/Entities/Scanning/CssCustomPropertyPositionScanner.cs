namespace Argentini.Sfumato.Entities.Scanning;

/// <summary>
/// Yields index/length pairs for each --custom-property in a StringBuilder.
/// </summary>
public static partial class CssCustomPropertyExtensions
{
    public static CssPropertyPositionEnumerable EnumerateCssCustomPropertyPositions(this StringBuilder? sb)
        => new(sb ?? new StringBuilder());
}

/// <summary>
/// Just carries the four ints: property start/length, value start/length.
/// </summary>
public readonly struct CssPropertyPosition
{
    public int PropertyStart { get; }
    public int PropertyLength { get; }
    public int ValueStart { get; }
    public int ValueLength { get; }

    internal CssPropertyPosition(int propStart, int propLen, int valStart, int valLen)
    {
        PropertyStart = propStart;
        PropertyLength = propLen;
        ValueStart = valStart;
        ValueLength = valLen;
    }
}

public readonly struct CssPropertyPositionEnumerable
{
    private readonly StringBuilder _s;

    internal CssPropertyPositionEnumerable(StringBuilder s)
    {
        _s = s;
    }

    public Enumerator GetEnumerator() => new(_s);

    public struct Enumerator
    {
        private readonly StringBuilder _s;
        private int _i, _propStart, _propEnd, _valStart, _valEnd;

        internal Enumerator(StringBuilder s)
        {
            _s            = s;
            _i            = _propStart = _propEnd = _valStart = _valEnd = 0;
        }

        public CssPropertyPosition Current
            => new(
                _propStart,
                _propEnd   - _propStart,
                _valStart,
                _valEnd    - _valStart
               );

        public bool MoveNext()
        {
            var len = _s.Length;
            var i   = _i;

            for (; i < len - 1; i++)
            {
                // scan for “--”
                if (_s[i] != '-' || _s[i + 1] != '-')
                    continue;

                var propStart = i;
                i += 2;

                while (i < len && (char.IsLetterOrDigit(_s[i]) || _s[i] is '_' or '-'))
                    i++;

                if (i == propStart + 2)
                    continue; // just “--”

                var propEnd = i;

                while (i < len && char.IsWhiteSpace(_s[i]))
                    i++;

                if (i >= len)
                    continue;

                // only declarations
                if (_s[i] != ':')
                    continue;

                // consume “:”
                i++;

                while (i < len && char.IsWhiteSpace(_s[i]))
                    i++;

                var valStart = i;
                var inS      = false;
                var inD      = false;
                var esc      = false;

                for (; i < len; i++)
                {
                    var c = _s[i];

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

                while (valEnd > valStart && char.IsWhiteSpace(_s[valEnd - 1]))
                    valEnd--;

                _propStart = propStart;
                _propEnd   = propEnd;
                _valStart  = valStart;
                _valEnd    = valEnd;

                if (i < len && _s[i] == ';')
                    i++;

                _i = i;
                return true;
            }

            _i = len;
            return false;
        }
    }
}