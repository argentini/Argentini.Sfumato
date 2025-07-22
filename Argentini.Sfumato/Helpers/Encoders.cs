namespace Argentini.Sfumato.Helpers;

public static class Encoders
{
    public static ulong Fnv1AHash64(this string s)
    {
        const ulong fnvOffsetBasis = 14695981039346656037UL;
        const ulong fnvPrime = 1099511628211UL;

        var hash = fnvOffsetBasis;

        foreach (var c in s)
        {
            hash ^= c;
            hash *= fnvPrime;
        }

        return hash;
    }

    public static ulong Fnv1AHash64(this StringBuilder sb)
    {
        const ulong fnvOffsetBasis = 14695981039346656037UL;
        const ulong fnvPrime = 1099511628211UL;

        var hash = fnvOffsetBasis;

        for (var i = 0; i < sb.Length; i++)
        {
            hash ^= sb[i];
            hash *= fnvPrime;
        }

        return hash;
    }
}