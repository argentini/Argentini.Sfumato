namespace Argentini.Sfumato.Entities.Scanning;

public static class CustomVariantScanner
{
    /// <summary>
    /// Enumerates every declaration of the form "@custom-variant name" 
    /// Name – the identifier after @custom-variant
    /// Content – everything inside the ( … ), with nested parentheses respected and leading/trailing whitespace trimmed
    /// </summary>
    public static CustomVariantEnumerable EnumerateCustomVariants(this string? css) => new(css);

    public readonly ref struct CustomVariantMatch
    {
        public readonly ReadOnlySpan<char> Name;     // "tab-4"
        public readonly ReadOnlySpan<char> Content;  // inside ( … )

        internal CustomVariantMatch(ReadOnlySpan<char> name, ReadOnlySpan<char> content)
        {
            Name    = name;
            Content = content;
        }
    }

    /* ───── enumerable / enumerator ───── */

    public readonly ref struct CustomVariantEnumerable
    {
        private readonly ReadOnlySpan<char> _src;
        internal CustomVariantEnumerable(string? css) =>
            _src = css is null ? ReadOnlySpan<char>.Empty : css.AsSpan();

        public Enumerator GetEnumerator() => new(_src);

        public ref struct Enumerator
        {
            private readonly ReadOnlySpan<char> _s;
            private int _i;
            private CustomVariantMatch _current;

            private const string Keyword = "@custom-variant";

            public Enumerator(ReadOnlySpan<char> src)
            {
                _s       = src;
                _i       = 0;
                _current = default;
            }

            public CustomVariantMatch Current => _current;

            public bool MoveNext()
            {
                var s = _s;
                int n = s.Length;

                while (_i < n)
                {
                    /* -------- find the next "@custom-variant" -------- */
                    int hit = s.Slice(_i).IndexOf(Keyword, StringComparison.Ordinal);
                    if (hit < 0) break;

                    int pos = _i + hit + Keyword.Length;

                    /* -------- name -------- */
                    SkipWs(ref pos, s);
                    int nameStart = pos;
                    while (pos < n && IsIdentChar(s[pos])) pos++;
                    if (pos == nameStart)          // no identifier: skip keyword
                    { _i = _i + hit + 1; continue; }

                    ReadOnlySpan<char> name = s.Slice(nameStart, pos - nameStart);

                    /* -------- opening '(' -------- */
                    SkipWs(ref pos, s);
                    if (pos >= n || s[pos] != '(')
                    { _i = _i + hit + 1; continue; }

                    int openParPos = pos++;
                    /* -------- content (nested parens allowed) -------- */
                    int depth = 1;
                    int contentStart = pos;
                    while (pos < n && depth > 0)
                    {
                        char c = s[pos++];
                        if (c == '(') depth++;
                        else if (c == ')') depth--;
                    }
                    if (depth != 0)
                        return false;              // unbalanced – bail

                    int closeParPos  = pos - 1;     // position of ')'

                    /* trim trailing whitespace inside (…) */
                    int contentEnd = closeParPos;
                    while (contentEnd > contentStart &&
                           char.IsWhiteSpace(s[contentEnd - 1]))
                        contentEnd--;

                    ReadOnlySpan<char> content = s.Slice(contentStart,
                                                          contentEnd - contentStart);

                    /* -------- expect ';' -------- */
                    SkipWs(ref pos, s);
                    if (pos >= n || s[pos] != ';')
                    { _i = _i + hit + 1; continue; }

                    /* -------- produce match -------- */
                    _current = new CustomVariantMatch(name, content);
                    _i = pos + 1;                  // continue after ';'
                    return true;
                }

                return false;
            }

            /* ───── helpers ───── */

            private static bool IsIdentChar(char c) =>
                char.IsLetterOrDigit(c) || c is '_' or '-';

            private static void SkipWs(ref int i, ReadOnlySpan<char> s)
            {
                while (i < s.Length && char.IsWhiteSpace(s[i])) i++;
            }
        }
    }
}