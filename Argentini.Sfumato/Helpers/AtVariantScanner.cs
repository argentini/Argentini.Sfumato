namespace Argentini.Sfumato.Helpers;

public static class AtVariantScanner
{
    /// <summary>
    /// Enumerates every “@variant name” statement.
    /// Full – Entire statement up to and including the “{”  
    /// Name – The identifier that follows @variant
    /// </summary>
    public static VariantEnumerable EnumerateAtVariantStatements(this string? css) => new(css);

    public readonly ref struct VariantMatch
    {
        public readonly ReadOnlySpan<char> Full;
        public readonly ReadOnlySpan<char> Name;

        internal VariantMatch(ReadOnlySpan<char> full, ReadOnlySpan<char> name)
        {
            Full = full;
            Name = name;
        }
    }

    public readonly ref struct VariantEnumerable
    {
        private readonly ReadOnlySpan<char> _src;
        internal VariantEnumerable(string? css) => _src = css is null ? ReadOnlySpan<char>.Empty : css.AsSpan();

        public Enumerator GetEnumerator() => new(_src);

        public ref struct Enumerator(ReadOnlySpan<char> src)
        {
            private readonly ReadOnlySpan<char> _s = src;
            private int _i = 0;
            private VariantMatch _current = default;

            private static ReadOnlySpan<char> Marker => "@variant";

            public VariantMatch Current => _current;

            public bool MoveNext()
            {
                var s = _s;
                var n = s.Length;
                var mark = Marker;

                while (_i < n)
                {
                    // Find the next "@variant"
                    var rel = s[_i..].IndexOf(mark);
                    
                    if (rel < 0)
                        return false;

                    var pos = _i + rel; // '@'
                    var p = pos + mark.Length; // After keyword

                    SkipWs(ref p, s);

                    if (p >= n)
                    {
                        _i = pos + 1;
                        continue;
                    }

                    // Read <name>
                    var nameStart = p;
                    
                    while (p < n && IsIdentChar(s[p]))
                        p++;

                    if (p == nameStart)
                    {
                        // No ident
                        _i = pos + 1;
                        continue;
                    }  

                    var name = s.Slice(nameStart, p - nameStart);

                    // Optional whitespace then '{'
                    SkipWs(ref p, s);

                    if (p >= n || s[p] != '{')
                    {
                        _i = pos + 1;
                        continue;
                    }

                    var full = s.Slice(pos, p - pos + 1); // Include '{'

                    _current = new VariantMatch(full, name);

                    _i = p + 1; // Resume scan
                    
                    return true;
                }

                return false;
            }

            private static void SkipWs(ref int idx, ReadOnlySpan<char> s)
            {
                while (idx < s.Length && char.IsWhiteSpace(s[idx]))
                    idx++;
            }

            private static bool IsIdentChar(char c) => char.IsLetterOrDigit(c) || c is '_' or '-';
        }
    }
}