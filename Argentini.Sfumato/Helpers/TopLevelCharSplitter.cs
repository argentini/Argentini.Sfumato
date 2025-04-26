namespace Argentini.Sfumato.Helpers;

public static class DelimitedSplitExtensions
{
    /// <summary>
    /// Enumerates source as slices separated by delimiter
    /// when that delimiter is **not** inside square-brackets or parentheses.
    /// </summary>
    public static DelimitedSplitEnumerable SplitByTopLevel(this string? source, char delimiter) => new(source, delimiter);
}

/// <summary>
/// Stack-only enumerable / enumerator pair – no regex, no allocations.
/// </summary>
public readonly ref struct DelimitedSplitEnumerable
{
    private readonly ReadOnlySpan<char> _buffer;
    private readonly char _delimiter;

    internal DelimitedSplitEnumerable(string? s, char delimiter)
    {
        _buffer    = s is null ? ReadOnlySpan<char>.Empty : s.AsSpan();
        _delimiter = delimiter;
    }

    public Enumerator GetEnumerator() => new(_buffer, _delimiter);

    public ref struct Enumerator
    {
        private ReadOnlySpan<char> _rest;
        private ReadOnlySpan<char> _current;
        private readonly char _delimiter;
        private int _bracketDepth;
        private int _parenDepth;

        internal Enumerator(ReadOnlySpan<char> buffer, char delimiter)
        {
            _rest         = buffer;
            _current      = default;
            _delimiter    = delimiter;
            _bracketDepth = 0;
            _parenDepth   = 0;
        }

        public ReadOnlySpan<char> Current => _current;

        public bool MoveNext()
        {
            if (_rest.IsEmpty)
                return false;

            for (var i = 0; i < _rest.Length; i++)
            {
                var ch = _rest[i];

                switch (ch)
                {
                    case '[':
                        _bracketDepth++;
                        break;
                    case ']':
                        if (_bracketDepth > 0)
                            _bracketDepth--;
                        break;

                    case '(':
                        _parenDepth++;
                        break;
                    case ')':
                        if (_parenDepth   > 0)
                            _parenDepth--;
                        break;
                }

                if (ch != _delimiter || _bracketDepth != 0 || _parenDepth != 0)
                    continue;
                
                _current = _rest[..i]; // Slice before the delimiter
                _rest    = _rest[(i + 1)..]; // Advance past it

                return true;
            }

            // Last slice (no more delimiters)
            _current = _rest;
            _rest    = ReadOnlySpan<char>.Empty;
            
            return true;
        }
    }
}