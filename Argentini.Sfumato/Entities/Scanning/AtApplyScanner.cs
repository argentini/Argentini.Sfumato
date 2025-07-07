namespace Argentini.Sfumato.Entities.Scanning;

public static class AtApplyScanner
{
    /// <summary>
    /// Enumerates “@apply …;” statements.
    /// Full - the entire text including the trailing semicolon.  
    /// Arguments – the part after the mandatory whitespace and before the terminating semicolon (trimmed of leading / trailing whitespace)
    /// </summary>
    public static ApplyEnumerable EnumerateAtApplyStatements(this string? css) => new(css);

    public readonly ref struct ApplyMatch
    {
        public readonly int Index;
        public readonly ReadOnlySpan<char> Full;
        public readonly ReadOnlySpan<char> Arguments;

        internal ApplyMatch(int index, ReadOnlySpan<char> full, ReadOnlySpan<char> args)
        {
            Index = index;
            Full = full;
            Arguments = args;
        }
    }

    public readonly ref struct ApplyEnumerable
    {
        private readonly ReadOnlySpan<char> _src;
        internal ApplyEnumerable(string? css) => _src = css is null ? ReadOnlySpan<char>.Empty : css.AsSpan();

        public Enumerator GetEnumerator() => new(_src);

        public ref struct Enumerator(ReadOnlySpan<char> src)
        {
            private readonly ReadOnlySpan<char> _s = src;
            private int _i = 0;
            private ApplyMatch _current = default;

            private static ReadOnlySpan<char> Marker => "@apply";

            public ApplyMatch Current => _current;

            public bool MoveNext()
            {
                var s = _s;
                var n = s.Length;
                var mark = Marker;

                while (_i < n)
                {
                    // Find "@apply"
                    var rel = s[_i..].IndexOf(mark);
                    
                    if (rel < 0)
                        return false;

                    var pos = _i + rel; // At '@'
                    var p = pos + mark.Length; // After keyword

                    // Require at least one whitespace char
                    if (p >= n || !char.IsWhiteSpace(s[p]))
                    {
                        _i = pos + 1;
                        continue;
                    }

                    while (p < n && char.IsWhiteSpace(s[p]))
                        p++; // Skip all WS

                    if (p >= n)
                    {
                        _i = pos + 1;
                        continue;
                    }

                    var argsStart = p;

                    // Find trailing ';'
                    var semiRel = s[p..].IndexOf(';'); // SIMD
                    
                    if (semiRel < 0)
                        return false; // Unterminated

                    var semiPos = p + semiRel; // Absolute ';'

                    if (semiPos == argsStart)
                    {
                        // Need 1+ argument char
                        _i = pos + 1;
                        continue;
                    } 

                    // Trim trailing WS inside args
                    var argsEnd = semiPos - 1;

                    while (argsEnd >= argsStart && char.IsWhiteSpace(s[argsEnd]))
                        argsEnd--;

                    if (argsEnd < argsStart)
                    {
                        // Skip if only WS
                        _i = pos + 1;
                        continue;
                    } 

                    var full = s.Slice(pos, semiPos - pos + 1);
                    var args = s.Slice(argsStart, argsEnd - argsStart + 1);

                    _current = new ApplyMatch(pos, full, args);

                    _i = semiPos + 1; // Continue after ';'

                    return true;
                }

                return false;
            }
        }
    }
}