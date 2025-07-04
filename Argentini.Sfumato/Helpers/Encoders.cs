using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace Argentini.Sfumato.Helpers;

public static class Encoders
{
    #region Fingerprinting

    private const uint SGenerator = 0xEDB88320;

    private static uint[] InitializeCrc32Table()
    {
        return Enumerable.Range(0, 256).Select(i =>
        {
            var tableEntry = (uint)i;

            for (var j = 0; j < 8; j++)
            {
                tableEntry = (tableEntry & 1) != 0
                    ? SGenerator ^ (tableEntry >> 1)
                    : tableEntry >> 1;
            }

            return tableEntry;

        }).ToArray();
    }

    // Public static property to expose the CRC-32 table.
    private static uint[] Crc32Table { get; } = InitializeCrc32Table();

    public static uint GenerateCrc32(this string input)
    {
        return Encoding.UTF8.GetBytes(input).GenerateCrc32();
    }

    public static uint GenerateCrc32(this StringBuilder sb)
    {
        return sb.ToByteArray(Encoding.UTF8).GenerateCrc32();
    }

    private static uint GenerateCrc32(this IEnumerable<byte> data)
    {
        uint crc = 0;

        if (Sse42.IsSupported)
        {
            crc = data.Aggregate(crc, Sse42.Crc32);
        }
        else if (Crc32.IsSupported)
        {
            crc = data.Aggregate(crc, Crc32.ComputeCrc32);
        }
        else
        {
            crc = _GenerateCrc32(data);
        }

        return crc;
    }

    /// <summary>
    /// Calculate the CRC-32 of a byte array.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    private static uint _GenerateCrc32(this IEnumerable<byte> payload)
    {
        try
        {
            // Initialize checksumRegister to 0xFFFFFFFF and calculate the checksum.
            return ~payload.Aggregate(0xFFFFFFFF, (checksumRegister, currentByte) =>
                Crc32Table[(checksumRegister & 0xFF) ^ Convert.ToByte(currentByte)] ^ (checksumRegister >> 8));
        }
        catch (FormatException e)
        {
            throw new Exception("Could not read the stream out as bytes.", e);
        }
        catch (InvalidCastException e)
        {
            throw new Exception("Could not read the stream out as bytes.", e);
        }
        catch (OverflowException e)
        {
            throw new Exception("Could not read the stream out as bytes.", e);
        }
    }

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

    /// Build a 64-bit primary sort key from the first
    /// four *alphanumeric* characters in the selector.
    /// Earlier characters are packed into the *higher* bytes,
    /// giving the same order as an ordinal string compare.
    /// Punctuation ('.', '-', ':', etc.) is skipped.
    /// If you know the data is always lower-case you can
    /// drop the ToUpperInvariant call.
    public static ulong GetNameSort(this string selector)
    {
        if (string.IsNullOrEmpty(selector))
            return 0;

        ulong key = 0;
        var packed = 0;

        foreach (char ch in selector)
        {
            if (char.IsLetterOrDigit(ch) == false)
                continue;

            key = (key << 16) | ch;
            packed++;

            if (packed == 4)
                break;
        }

        // shift left so that shorter strings still compare correctly
        key <<= (4 - packed) * 16;

        return key;
    }

    #endregion
}