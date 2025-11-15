// ReSharper disable UseCollectionExpression

using System.Runtime.CompilerServices;

namespace Sfumato.Entities.CssClassProcessing;

public static class DelimitedSplitExtensions
{
    /// <summary>
    /// List the source as slices separated by delimiter
    /// when that delimiter is **not** inside square-brackets or parentheses.
    /// </summary>
    public static DelimitedSplitEnumerable SplitByTopLevel(this string? source, char delimiter) => new(source, delimiter);

    
    /// <summary>
    /// Return only the last segment of <paramref name="source"/>, split by <paramref name="delimiter"/>
    /// at top-level (i.e., ignoring delimiters inside [ ] or ( )).
    /// Allocates exactly one string (the final segment), or returns null/empty if <paramref name="source"/> is null/empty.
    /// </summary>
    public static string? LastByTopLevel(this string? source, char delimiter)
    {
        if (source is null)
            return null;

        if (source.Length == 0)
            return string.Empty;

        ReadOnlySpan<char> last = default;

        // use the existing fast, zero-alloc Span-enumerator
        foreach (var segment in source.SplitByTopLevel(delimiter))
            last = segment;

        // allocate only one string for the very last segment
        return last.ToString();
    }
}

/// <summary>
/// Stack-only enumerable / enumerator pair – no regex, no allocations.
/// </summary>
public readonly ref struct DelimitedSplitEnumerable
{
    private readonly ReadOnlySpan<char> _buffer;
    private readonly char _delimiter;

    // ReSharper disable once ConvertToPrimaryConstructor
    public DelimitedSplitEnumerable(string? s, char delimiter)
    {
        _buffer = string.IsNullOrEmpty(s) ? [] : s[^1] == '!' ? s.AsSpan(0, s.Length - 1) : s.AsSpan();

        _delimiter = delimiter;
    }

    public Enumerator GetEnumerator() => new(_buffer, _delimiter);

    public ref struct Enumerator
    {
        private ReadOnlySpan<char> _rest;
        private ReadOnlySpan<char> _current;
        private readonly char _delimiter;

        internal Enumerator(ReadOnlySpan<char> buffer, char delimiter)
        {
            _rest      = buffer;
            _current   = default;
            _delimiter = delimiter;
        }

        public readonly ReadOnlySpan<char> Current => _current;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (_rest.IsEmpty)
                return false;

            var original = _rest;
            var span = _rest;
            var pos = 0;
            var bracket = 0;
            var paren = 0;

            // Only allocate this once per MoveNext.
            ReadOnlySpan<char> seps = stackalloc char[] {
                _delimiter,
                '[', ']', '(', ')'
            };

            while (true)
            {
                var idx = span.IndexOfAny(seps);

                if (idx < 0)
                {
                    // no more top‐level delimiter → emit the *whole* remainder
                    _current = original;
                    _rest = [];
                    return true;
                }

                var ch = span[idx];

                switch (ch)
                {
                    case '[':
                        bracket++;
                        break;
                    case ']':
                        if (bracket > 0) bracket--;
                        break;
                    case '(':
                        paren++;
                        break;
                    case ')':
                        if (paren > 0) paren--;
                        break;
                    case var d when d == _delimiter && bracket == 0 && paren == 0:
                        
                        // Found a top-level delimiter at absolute index (pos + idx)
                        var splitAt = pos + idx;
                        
                        _current = original[..splitAt];
                        _rest = original[(splitAt + 1)..];
                        
                        return true;
                }

                // Not a top-level delimiter, skip past this char
                // and continue scanning from there.
                pos += idx + 1;
                span = span[(idx + 1)..];
            }
        }
    }
}