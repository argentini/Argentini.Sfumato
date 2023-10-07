namespace Argentini.Sfumato.Extensions;

/// <summary>
/// Various tools for working with strings. 
/// </summary>
public static class Strings
{
	#region Trimming
	
	/// <summary>
    /// Remove a specified number of characters from the beginning of a string
    /// </summary>
    /// <param name="value">String to trim</param>
    /// <param name="count">Number of characters to remove</param>
    /// <returns>Trimmed string</returns>
	public static string? TrimStart(this string? value, int count)
	{
		if (value is not null && value.Length >= count)
			return value.Right(value.Length - count);

		return value;
	}

	/// <summary>
    /// Remove a specified number of characters from the end of a string
    /// </summary>
    /// <param name="value">String to trim</param>
    /// <param name="count">Number of characters to remove</param>
    /// <returns>Trimmed string</returns>
	public static string? TrimEnd(this string? value, int count)
	{
		if (value is not null && value.Length >= count)
			return value.Left(value.Length - count);

		return value;
	}

	/// <summary>
	/// Remove a string from the beginning of a string
	/// </summary>
	/// <param name="source">The string to search</param>
	/// <param name="substring">The substring to remove</param>
	/// <param name="stringComparison"></param>
	/// <returns>Trimmed source</returns>
	public static string? TrimStart(this string? source, string? substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
	{
		if (source == null || source.IsEmpty() || substring is null or "")
			return null;

		var result = new StringBuilder(source);
		
		result.TrimStart(substring, stringComparison);

		return result.ToString();
	}

	/// <summary>
	/// Remove a string from the end of a string
	/// </summary>
	/// <param name="source">The string to search</param>
	/// <param name="substring">The substring to remove</param>
	/// <param name="stringComparison"></param>
	/// <returns>Trimmed source</returns>
	public static string? TrimEnd(this string? source, string? substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
	{
		if (source == null || source.IsEmpty() || substring is null or "")
			return null;

		var result = new StringBuilder(source);
		
		result.TrimEnd(substring, stringComparison);

		return result.ToString();
	}
	
	#endregion
	
	#region Comparison	

	/// <summary>
	/// Determines if a string has a value (is not null and not empty).
	/// </summary>
	/// <param name="value">String to evaluate</param>
	public static bool HasValue(this string? value)
	{
		return string.IsNullOrEmpty(value?.Trim()) == false;
	}

	/// <summary>
	/// Determines if a string is empty or null.
	/// </summary>
	/// <param name="value">String to evaluate</param>
	public static bool IsEmpty(this string? value)
	{
		return string.IsNullOrEmpty(value);
	}
	
	/// <summary>
	/// Determine if two strings are not equal.
	/// </summary>
	/// <param name="source"></param>
	/// <param name="value"></param>
	/// <param name="comparisonType"></param>
	/// <returns></returns>
	public static bool NotEquals(this string? source, string? value, StringComparison comparisonType = StringComparison.Ordinal)
	{
		if (source == null && value == null) return false;
		if (source is not null && value == null) return true;
		if (source == null && value is not null) return true;
		
		return source?.Equals(value, comparisonType) == false;
	}

	/// <summary>
	/// Determine if a string starts with any value from a string array.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="substrings"></param>
	/// <param name="stringComparison"></param>
	/// <returns></returns>
	public static bool StartsWith(this string value, string[]? substrings, StringComparison stringComparison = StringComparison.Ordinal)
	{
		if (substrings is not {Length: > 0}) return false;

		var result = false;
        
		for (var x = 0; x < substrings.Length; x++)
		{
			if (value.StartsWith(substrings[x], stringComparison) == false) continue;
            
			result = true;
			x = substrings.Length;
		}

		return result;
	}
	
	#endregion
	
	#region Parsing

	/// <summary>
	/// Get the index of the first non-space character in a string.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static int FirstNonSpaceCharacter(this string? value)
	{
		if (string.IsNullOrEmpty(value))
			return 0;

		var index = -1;
		
		foreach (var c in value)
		{
			index++;

			if (char.IsWhiteSpace(c))
				continue;
			
			break;
		}

		return index;
	}
	
	/// <summary>
	/// Get the left "length" characters of a string.
	/// </summary>
	/// <param name="value">String value</param>
	/// <param name="length">Number of characters</param>
	/// <returns>Left portion of a string</returns>
	public static string Left(this string? value, int length)
	{
		if (value == null || value.IsEmpty() || length < 1) return string.Empty;
		if (length > value.Length) return value;
		
		return value[..length];
	}

	/// <summary>
	/// Get the left characters of a string up to but not including the first instance of "marker".
	/// If marker is not found the original value is returned.
	/// </summary>
	/// <param name="value">String value</param>
	/// <param name="marker">Delimiter to denote the cut off point</param>
	/// <returns>Left portion of a string</returns>
	public static string Left(this string? value, string? marker)
	{
		if (value == null || value.IsEmpty()) return string.Empty;
		if (marker == null || marker.IsEmpty()) return value;

		if (value.Length <= marker.Length) return string.Empty;

		if (value.Contains(marker))
			return value[..value.IndexOf(marker, StringComparison.Ordinal)];

		return value;
	}
	
	/// <summary>
	/// Get the right "length" characters of a string.
	/// </summary>
	/// <param name="value">String value</param>
	/// <param name="length">Number of characters</param>
	/// <returns>Right portion of a string</returns>
	public static string Right(this string? value, int length)
	{
		if (value == null || value.IsEmpty() || length < 1) return string.Empty;
		if (length > value.Length) return value;

		return value[^length..];
	}

	/// <summary>
	/// Get the right characters of a string up to but not including the last instance of "marker" (right to left).
	/// If marker is not found the original value is returned.
	/// </summary>
	/// <param name="value">String value</param>
	/// <param name="marker">Delimiter to denote the cut off point</param>
	/// <returns>Right portion of a string</returns>
	public static string Right(this string? value, string? marker)
	{
		if (value == null || value.IsEmpty()) return string.Empty;
		if (marker == null || marker.IsEmpty()) return value;

		if (value.Length <= marker.Length) return string.Empty;
		
		if (value.Contains(marker))
			return value[(value.LastIndexOf(marker, StringComparison.Ordinal) + marker.Length)..];

		return value;
	}
	
	#endregion
	
	#region Transformations
	
	/// <summary>
	/// Repeat a string a specified number of times.
	/// </summary>
	/// <param name="text"></param>
	/// <param name="n"></param>
	/// <returns></returns>
	public static string Repeat(this string text, int n)
	{
		if (string.IsNullOrEmpty(text) || n < 1)
			return string.Empty;
		
		var textAsSpan = text.AsSpan();
		var span = new Span<char>(new char[textAsSpan.Length * n]);
	
		for (var i = 0; i < n; i++)
		{
			textAsSpan.CopyTo(span.Slice(i * textAsSpan.Length, textAsSpan.Length));
		}

		return span.ToString();
	}
	
	/// <summary>
	/// Indent text with whitespace based on line breaks
	/// </summary>
	/// <param name="block"></param>
	/// <param name="count"></param>
	/// <returns></returns>
	public static string Indent(this string block, int count)
	{
		if (string.IsNullOrEmpty(block))
			return block;

		var whitespace = " ".Repeat(count);
		var result = block.Replace("\n", $"\n{whitespace}");

		if (result.EndsWith($"\n{whitespace}"))
			result = result[..^count];
		
		return (result.StartsWith('\r') || result.StartsWith('\n') ? string.Empty : whitespace) + result;
	}
	
	/// <summary>
	/// Normalize line breaks.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="linebreak">Line break to use (default: "\n")</param>
	public static string NormalizeLinebreaks(this string value, string linebreak = "\n")
	{
		if (value.IsEmpty())
			return string.Empty;

		if (value.Contains('\r'))
		{
			if (linebreak != "\r\n")
				return value.Replace("\r\n", linebreak);
		}

		else
		{
			if (linebreak != "\n")
				return value.Replace("\n", linebreak);
		}
		
		return value;        
	}

	/// <summary>
	/// Normalize path separators to those used by the current OS.
	/// </summary>
	/// <param name="value"></param>
	public static string SetNativePathSeparators(this string value)
	{
		if (string.IsNullOrEmpty(value))
			return string.Empty;

		var path = value;

		path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		
		if (Path.DirectorySeparatorChar != '/')
			path = path.Replace('/', Path.DirectorySeparatorChar);

		if (Path.DirectorySeparatorChar != '\\')
			path = path.Replace('\\', Path.DirectorySeparatorChar);
		
		return path;        
	}
	
	/// <summary>
	/// Convert bytes into a user-friendly size (e.g. 1024 becomes 1kb).
	/// </summary>
	/// <param name="val">Number of bytes</param>
	/// <param name="forStorage">When true sizes are calculated using base 10 (so 1kb = 1,000 bytes), false uses base 2 (so 1kb = 1,024 bytes).false Defaults to true</param>
	/// <returns>User-friendly size representation</returns>
	public static string FormatBytes<T>(this T val, bool forStorage = true)
	{
		var value = Convert.ToDecimal(val);
		var result = "0 bytes";
		decimal divisor = 1000;
		var mb = (divisor * divisor);
		var gb = (mb * divisor);
		var tb = (gb * divisor);
		var pb = (tb * divisor);

		if (forStorage == false)
		{
			divisor = 1024;
		}

		if (value <= 0)
			return result;
		
		// BYTES
		if (value < divisor)
		{
			result = (value.ToString("#,##0")) + " bytes";
		}

		// KILOBYTE
		if (value >= divisor && value < mb)
		{
			var newVal = (value / divisor);
			result = (newVal.ToString("#,##0.#")) + "kb";
		}

		// MEGABYTES
		if (value >= mb && value < gb)
		{
			var newVal = (value / divisor) / divisor;
			result = (newVal.ToString("#,##0.#")) + "mb";
		}

		// GIGABYTES
		if (value >= gb && value < tb)
		{
			var newVal = ((value / divisor) / divisor) / divisor;
			result = (newVal.ToString("#,##0.#")) + "gb";
		}

		// TERABYTES
		if (value >= tb && value < pb)
		{
			var newVal = (((value / divisor) / divisor) / divisor) / divisor;
			result = (newVal.ToString("#,##0.#")) + "tb";
		}

		// PETABYTES
		if (value >= pb)
		{
			var newVal = ((((value / divisor) / divisor) / divisor) / divisor) / divisor;
			result = (newVal.ToString("#,##0.#")) + "pb";
		}

		return result;
	}
	
	#endregion
}
