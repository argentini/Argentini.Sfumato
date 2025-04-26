// ReSharper disable MemberCanBePrivate.Global

namespace Argentini.Sfumato.Helpers;

/// <summary>
/// Various tools for working with strings. 
/// </summary>
public static partial class Strings
{
	#region Constants

	public static string ArrowRight => "\u2b95";
	public static string TriangleRight => "\u23f5";
	public static string ThinLine => "\u23bb";
	public static string ThickLine => "\u2501";
	public static string DotLine => "\u2504";
	
	public static Dictionary<string, string> CssNamedColors { get; } = new(StringComparer.Ordinal)
	{
	    ["aliceblue"] = "rgb(240, 248, 255)",
	    ["antiquewhite"] = "rgb(250, 235, 215)",
	    ["aqua"] = "rgb(0, 255, 255)",
	    ["aquamarine"] = "rgb(127, 255, 212)",
	    ["azure"] = "rgb(240, 255, 255)",
	    ["beige"] = "rgb(245, 245, 220)",
	    ["bisque"] = "rgb(255, 228, 196)",
	    ["black"] = "rgb(0, 0, 0)",
	    ["blanchedalmond"] = "rgb(255, 235, 205)",
	    ["blue"] = "rgb(0, 0, 255)",
	    ["blueviolet"] = "rgb(138, 43, 226)",
	    ["brown"] = "rgb(165, 42, 42)",
	    ["burlywood"] = "rgb(222, 184, 135)",
	    ["cadetblue"] = "rgb(95, 158, 160)",
	    ["chartreuse"] = "rgb(127, 255, 0)",
	    ["chocolate"] = "rgb(210, 105, 30)",
	    ["coral"] = "rgb(255, 127, 80)",
	    ["cornflowerblue"] = "rgb(100, 149, 237)",
	    ["cornsilk"] = "rgb(255, 248, 220)",
	    ["crimson"] = "rgb(220, 20, 60)",
	    ["cyan"] = "rgb(0, 255, 255)",
	    ["darkblue"] = "rgb(0, 0, 139)",
	    ["darkcyan"] = "rgb(0, 139, 139)",
	    ["darkgoldenrod"] = "rgb(184, 134, 11)",
	    ["darkgray"] = "rgb(169, 169, 169)",
	    ["darkgreen"] = "rgb(0, 100, 0)",
	    ["darkgrey"] = "rgb(169, 169, 169)",
	    ["darkkhaki"] = "rgb(189, 183, 107)",
	    ["darkmagenta"] = "rgb(139, 0, 139)",
	    ["darkolivegreen"] = "rgb(85, 107, 47)",
	    ["darkorange"] = "rgb(255, 140, 0)",
	    ["darkorchid"] = "rgb(153, 50, 204)",
	    ["darkred"] = "rgb(139, 0, 0)",
	    ["darksalmon"] = "rgb(233, 150, 122)",
	    ["darkseagreen"] = "rgb(143, 188, 143)",
	    ["darkslateblue"] = "rgb(72, 61, 139)",
	    ["darkslategray"] = "rgb(47, 79, 79)",
	    ["darkslategrey"] = "rgb(47, 79, 79)",
	    ["darkturquoise"] = "rgb(0, 206, 209)",
	    ["darkviolet"] = "rgb(148, 0, 211)",
	    ["deeppink"] = "rgb(255, 20, 147)",
	    ["deepskyblue"] = "rgb(0, 191, 255)",
	    ["dimgray"] = "rgb(105, 105, 105)",
	    ["dimgrey"] = "rgb(105, 105, 105)",
	    ["dodgerblue"] = "rgb(30, 144, 255)",
	    ["firebrick"] = "rgb(178, 34, 34)",
	    ["floralwhite"] = "rgb(255, 250, 240)",
	    ["forestgreen"] = "rgb(34, 139, 34)",
	    ["fuchsia"] = "rgb(255, 0, 255)",
	    ["gainsboro"] = "rgb(220, 220, 220)",
	    ["ghostwhite"] = "rgb(248, 248, 255)",
	    ["gold"] = "rgb(255, 215, 0)",
	    ["goldenrod"] = "rgb(218, 165, 32)",
	    ["gray"] = "rgb(128, 128, 128)",
	    ["green"] = "rgb(0, 128, 0)",
	    ["greenyellow"] = "rgb(173, 255, 47)",
	    ["grey"] = "rgb(128, 128, 128)",
	    ["honeydew"] = "rgb(240, 255, 240)",
	    ["hotpink"] = "rgb(255, 105, 180)",
	    ["indianred"] = "rgb(205, 92, 92)",
	    ["indigo"] = "rgb(75, 0, 130)",
	    ["ivory"] = "rgb(255, 255, 240)",
	    ["khaki"] = "rgb(240, 230, 140)",
	    ["lavender"] = "rgb(230, 230, 250)",
	    ["lavenderblush"] = "rgb(255, 240, 245)",
	    ["lawngreen"] = "rgb(124, 252, 0)",
	    ["lemonchiffon"] = "rgb(255, 250, 205)",
	    ["lightblue"] = "rgb(173, 216, 230)",
	    ["lightcoral"] = "rgb(240, 128, 128)",
	    ["lightcyan"] = "rgb(224, 255, 255)",
	    ["lightgoldenrodyellow"] = "rgb(250, 250, 210)",
	    ["lightgray"] = "rgb(211, 211, 211)",
	    ["lightgreen"] = "rgb(144, 238, 144)",
	    ["lightgrey"] = "rgb(211, 211, 211)",
	    ["lightpink"] = "rgb(255, 182, 193)",
	    ["lightsalmon"] = "rgb(255, 160, 122)",
	    ["lightseagreen"] = "rgb(32, 178, 170)",
	    ["lightskyblue"] = "rgb(135, 206, 250)",
	    ["lightslategray"] = "rgb(119, 136, 153)",
	    ["lightslategrey"] = "rgb(119, 136, 153)",
	    ["lightsteelblue"] = "rgb(176, 196, 222)",
	    ["lightyellow"] = "rgb(255, 255, 224)",
	    ["lime"] = "rgb(50, 205, 50)",
	    ["limegreen"] = "rgb(50, 205, 50)",
	    ["linen"] = "rgb(250, 240, 230)",
	    ["magenta"] = "rgb(255, 0, 255)",
	    ["maroon"] = "rgb(128, 0, 0)",
	    ["mediumaquamarine"] = "rgb(102, 205, 170)",
	    ["mediumblue"] = "rgb(0, 0, 205)",
	    ["mediumorchid"] = "rgb(186, 85, 211)",
	    ["mediumpurple"] = "rgb(147, 112, 219)",
	    ["mediumseagreen"] = "rgb(60, 179, 113)",
	    ["mediumslateblue"] = "rgb(123, 104, 238)",
	    ["mediumspringgreen"] = "rgb(0, 250, 154)",
	    ["mediumturquoise"] = "rgb(72, 209, 204)",
	    ["mediumvioletred"] = "rgb(199, 21, 133)",
	    ["midnightblue"] = "rgb(25, 25, 112)",
	    ["mintcream"] = "rgb(245, 255, 250)",
	    ["mistyrose"] = "rgb(255, 228, 225)",
	    ["moccasin"] = "rgb(255, 228, 181)",
	    ["navajowhite"] = "rgb(255, 222, 173)",
	    ["navy"] = "rgb(0, 0, 128)",
	    ["oldlace"] = "rgb(253, 245, 230)",
	    ["olive"] = "rgb(128, 128, 0)",
	    ["olivedrab"] = "rgb(107, 142, 35)",
	    ["orange"] = "rgb(255, 165, 0)",
	    ["orangered"] = "rgb(255, 69, 0)",
	    ["orchid"] = "rgb(218, 112, 214)",
	    ["palegoldenrod"] = "rgb(238, 232, 170)",
	    ["palegreen"] = "rgb(152, 251, 152)",
	    ["paleturquoise"] = "rgb(175, 238, 238)",
	    ["palevioletred"] = "rgb(219, 112, 147)",
	    ["papayawhip"] = "rgb(255, 239, 213)",
	    ["peachpuff"] = "rgb(255, 218, 185)",
	    ["peru"] = "rgb(205, 133, 63)",
	    ["pink"] = "rgb(255, 192, 203)",
	    ["plum"] = "rgb(221, 160, 221)",
	    ["powderblue"] = "rgb(176, 224, 230)",
	    ["purple"] = "rgb(128, 0, 128)",
	    ["rebeccapurple"] = "rgb(102, 51, 153)",
	    ["red"] = "rgb(255, 0, 0)",
	    ["rosybrown"] = "rgb(188, 143, 143)",
	    ["royalblue"] = "rgb(65, 105, 225)",
	    ["saddlebrown"] = "rgb(139, 69, 19)",
	    ["salmon"] = "rgb(250, 128, 114)",
	    ["sandybrown"] = "rgb(244, 164, 96)",
	    ["seagreen"] = "rgb(46, 139, 87)",
	    ["seashell"] = "rgb(255, 245, 238)",
	    ["sienna"] = "rgb(160, 82, 45)",
	    ["silver"] = "rgb(192, 192, 192)",
	    ["skyblue"] = "rgb(135, 206, 235)",
	    ["slateblue"] = "rgb(106, 90, 205)",
	    ["slategray"] = "rgb(112, 128, 144)",
	    ["slategrey"] = "rgb(112, 128, 144)",
	    ["snow"] = "rgb(255, 250, 250)",
	    ["springgreen"] = "rgb(0, 255, 127)",
	    ["steelblue"] = "rgb(70, 130, 180)",
	    ["tan"] = "rgb(210, 180, 140)",
	    ["teal"] = "rgb(0, 128, 128)",
	    ["thistle"] = "rgb(216, 191, 216)",
	    ["tomato"] = "rgb(255, 99, 71)",
	    ["transparent"] = "rgb(0, 0, 0, 0)",
	    ["turquoise"] = "rgb(64, 224, 208)",
	    ["violet"] = "rgb(238, 130, 238)",
	    ["wheat"] = "rgb(245, 222, 179)",
	    ["white"] = "rgb(255, 255, 255)",
	    ["whitesmoke"] = "rgb(245, 245, 245)",
	    ["yellow"] = "rgb(255, 255, 0)",
	    ["yellowgreen"] = "rgb(154, 205, 50)"	    
	};
	
	#endregion
	
	#region Trimming
	
	/// <summary>
	/// Trim the common prefix of from sourceString from targetString.
	/// </summary>
	/// <param name="targetString"></param>
	/// <param name="sourceString"></param>
	/// <returns></returns>
	public static string TrimCommonPrefix(this string targetString, string sourceString)
	{
		var minLength = Math.Min(targetString.Length, sourceString.Length);
		var commonLength = 0;

		for (var i = 0; i < minLength; i++)
		{
			if (targetString[i] != sourceString[i])
				break;
			
			commonLength++;
		}

		return targetString[commonLength..];
	}

    /// <summary>
    /// Truncates the middle of the string if it exceeds <paramref name="maxLength"/>, 
    /// preserving at least <paramref name="minStartLength"/> chars at the start and 
    /// <paramref name="minEndLength"/> chars at the end, with a Unicode ellipsis in between.
    /// </summary>
    /// <param name="path">The original string (e.g. a file or URL path).</param>
    /// <param name="minStartLength">Minimum number of chars to keep at the start.</param>
    /// <param name="minEndLength">Minimum number of chars to keep at the end.</param>
    /// <param name="maxLength">Maximum total length of the returned string (including the ellipsis).</param>
    /// <returns>
    /// The original string if it’s short enough; otherwise a truncated version 
    /// of length ≤ <paramref name="maxLength"/> with ‘ … ’ in the middle.
    /// </returns>
    /// <exception cref="ArgumentNullException">If <paramref name="path"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="maxLength"/> is less than <c>minStartLength + minEndLength + 1</c>.
    /// </exception>
    public static string TruncateCenter(this string path, int minStartLength, int minEndLength, int maxLength)
    {
        if (string.IsNullOrEmpty(path)) 
            return string.Empty;
        
        if (maxLength < minStartLength + minEndLength + 3) 
            throw new ArgumentException(
                $"maxLength must be at least minStartLength + minEndLength + 1 (got {maxLength}).", 
                nameof(maxLength));

        if (path.Length <= maxLength)
            return path;

        // Total chars aside from the ellipsis
        var charsToShow = maxLength - 3;

        // Start by splitting them roughly in half
        var startLen = charsToShow / 2;
        var endLen = charsToShow - startLen;

        if (startLen < minStartLength)
        {
            startLen = minStartLength;
            endLen = charsToShow - startLen;
        }

        if (endLen < minEndLength)
        {
            endLen = minEndLength;
            startLen = charsToShow - endLen;
        }

        var prefix = path[..startLen];
        var suffix = path.Substring(path.Length - endLen, endLen);

        return prefix + " \u2026 " + suffix;
    }

    /// <summary>
	/// Trim the common prefix from targetString that matches sourceString,
	/// but only up to the last Path.DirectorySeparatorChar in the matched portion.
	/// </summary>
	/// <param name="targetString">The string to trim.</param>
	/// <param name="sourceString">The source string to match against.</param>
	/// <returns>The trimmed string.</returns>
	public static string TrimCommonPathPrefix(this string targetString, string sourceString)
	{
		var minLength = Math.Min(targetString.Length, sourceString.Length);
		var lastSeparatorIndex = 0;

		for (var i = 0; i < minLength; i++)
		{
			if (targetString[i] != sourceString[i])
				break;

			if (targetString[i] == Path.DirectorySeparatorChar)
				lastSeparatorIndex = i + 1; // include separator in the prefix
		}

		return targetString[lastSeparatorIndex..];
	}
	
	/// <summary>
    /// Remove a specified number of characters from the beginning of a string
    /// </summary>
    /// <param name="value">String to trim</param>
    /// <param name="count">Number of characters to remove</param>
    /// <returns>Trimmed string</returns>
	public static string TrimStart(this string? value, int count)
	{
		if (value is not null && value.Length >= count)
			return value.Right(value.Length - count);

		return value ?? string.Empty;
	}

	/// <summary>
    /// Remove a specified number of characters from the end of a string
    /// </summary>
    /// <param name="value">String to trim</param>
    /// <param name="count">Number of characters to remove</param>
    /// <returns>Trimmed string</returns>
	public static string TrimEnd(this string? value, int count)
	{
		if (value is not null && value.Length >= count)
			return value.Left(value.Length - count);

		return value ?? string.Empty;
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

		return source.StartsWith(substring, stringComparison) 
			? source[substring.Length..] 
			: source;
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

		return source.EndsWith(substring, stringComparison)
			? source[..^substring.Length]
			: source;
	}

	/// <summary>
	/// Trim directory separator characters from the end of a string base on the current operating system.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string TrimEndingPathSeparators(this string? value)
	{
		return string.IsNullOrEmpty(value) ? string.Empty : value.TrimEnd(Path.DirectorySeparatorChar).TrimEnd(Path.AltDirectorySeparatorChar);
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
    /// Determine if two strings are not equal, ignoring case.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool InvariantNotEquals(this string? source, string? value)
    {
        if (source == null && value == null) return false;
        if (source is not null && value == null) return true;
        if (source == null && value is not null) return true;
		
        return source?.Equals(value, StringComparison.InvariantCultureIgnoreCase) == false;
    }

    /// <summary>
    /// Determine if two strings are equal, ignoring case.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool InvariantEquals(this string? source, string? value)
    {
        return source.InvariantNotEquals(value) == false;
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
	
	#region Conversion
	
	/// <summary>
	/// Convert a string to a byte array.
	/// </summary>
	/// <param name="value">String to evaluate</param>
	/// <returns>Byte array</returns>
	public static byte[] ToByteArray(this string? value)
	{
		if (value is null || string.IsNullOrEmpty(value))
			return [];
		
		var encoding = new UTF8Encoding();

		return encoding.GetBytes(value);
	}
	
	#endregion
	
	#region Parsing

    /// <summary>
    /// Scans <paramref name="source"/> and returns every whitespace‑/punctuation‑delimited
    /// token that appears inside any quoted literal (plain, verbatim, multi‑quote raw, or
    /// JavaScript template‑literal).
    /// </summary>
    public static IReadOnlyCollection<string> ScanQuotedStrings(this string? source)
    {
        if (string.IsNullOrEmpty(source))
            return [];

        var results = new HashSet<string>(StringComparer.Ordinal);
        var i = 0;
        var n = source.Length;

        while (i < n)
        {
            // consume optional $, @ prefix
            while (i < n && (source[i] == '$' || source[i] == '@'))
                i++;
            
            if (i >= n)
                break;

            var opener = source[i];

            if (opener is not ('"' or '\'' or '`'))
            {
                i++;
                
                continue;
            }

            // run length of identical opener chars (", """, etc.)
            var quoteLen = 0;
            
            while (i + quoteLen < n && source[i + quoteLen] == opener)
                quoteLen++;
            
            i += quoteLen;
            
            var contentStart = i;

            var backtick = opener == '`';
            var multi    = opener == '"' && quoteLen >= 2;         // $"" … "" or $""" … """
            var verbatim = source[i - quoteLen - 1] == '@' && opener == '"';

            if (backtick)
            {
                var depth = 0;

                while (i < n)
                {
                    if (source[i] == '\\')
                    {
                        i += 2;
                        
                        continue;
                    }
                    
                    if (depth == 0 && source[i] == '`')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        i++;
                        
                        break;
                    }
                    
                    // ReSharper disable once ConvertIfStatementToSwitchStatement
                    if (depth == 0 && source[i] == '$' && i + 1 < n && source[i + 1] == '{')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        depth = 1;
                        i += 2;
                        
                        continue;
                    }
                    
                    if (depth > 0)
                    {
                        switch (source[i])
                        {
                            case '{':
                                depth++;
                                break;
                            case '}':
                                depth--;
                                break;
                            case '"' or '\'' or '`':
                                SkipSimpleString(source[i]);
                                break;
                        }
                    }

                    i++;
                }

                continue;
            }

            if (verbatim)
            {
                while (i < n)
                {
                    if (source[i] == '"' && i + 1 < n && source[i + 1] == '"')
                    {
                        i += 2;

                        continue;
                    }
                    
                    if (source[i] == '"')
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        i++;
                        
                        break;
                    }
                    
                    i++;
                }

                continue;
            }

            if (multi)
            {
                while (i < n)
                {
                    if (source[i] != '"')
                    {
                        i++;
                        
                        continue;
                    }

                    var run = 0;
                    var k = i;

                    while (k < n && source[k] == '"')
                    {
                        run++;
                        k++;
                    }

                    if (run >= quoteLen)
                    {
                        SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);
                        
                        i = k;
                        
                        break;
                    }
                    
                    i = k; // keep scanning
                }

                continue;
            }

            // ordinary "…" or '…'
            while (i < n)
            {
                if (source[i] == '\\')
                {
                    i += 2;

                    continue;
                }
                
                if (source[i] == opener)
                {
                    SplitAndAdd(source.AsSpan(contentStart, i - contentStart), results);

                    break;
                }

                i++;
            }
        }

        return results;

        void SkipSimpleString(char q)
        {
            i++; // past the opener
            
            while (i < n)
            {
                if (source[i] == '\\')
                {
                    i += 2;
                    
                    continue;
                }

                if (source[i] == q)
                {
                    i++;
                    
                    break;
                }

                i++;
            }
        }

        static void SplitAndAdd(ReadOnlySpan<char> span, HashSet<string> bag)
        {
            var start = -1;
            
            for (var k = 0; k < span.Length; k++)
            {
                if (IsDelim(span[k]))
                {
                    if (start == -1)
                        continue;
                    
                    bag.Add(span[start..k].ToString());
                    start = -1;
                }
                else if (start == -1)
                {
                    start = k; // begin a token
                }
            }
            
            if (start != -1)
                bag.Add(span[start..].ToString());

            return;

            // delimiter characters that end a token (besides whitespace)
            //static bool IsDelim(char c) => char.IsWhiteSpace(c) || c is '"' or '\'' or '`' or '<' or '>' or '$' or '{' or '}' or '(' or ')' or '=';
            static bool IsDelim(char c) => char.IsWhiteSpace(c) || c is '"' or '\'' or '`' or '<' or '>' or '$' or '{' or '}' or '=';
        }
    }
	
	/// <summary>
	/// Enumerates all substrings of <paramref name="input"/> that consist of one or more non-whitespace characters.
	/// Equivalent to Regex.Matches(input, @"\S+").
	/// </summary>
	public static IEnumerable<string> SplitByNonWhitespace(this string input)
	{
		if (string.IsNullOrEmpty(input))
			yield break;

		var length = input.Length;
		var pos = 0;

		while (pos < length)
		{
			// Skip any leading whitespace
			while (pos < length && char.IsWhiteSpace(input[pos]))
				pos++;

			if (pos >= length)
				yield break;

			// Mark the start of the non-whitespace run
			var start = pos;

			// Advance until the next whitespace (or end of string)
			while (pos < length && char.IsWhiteSpace(input[pos]) == false)
				pos++;

			// Return the chunk
			yield return input.Substring(start, pos - start);
		}
	}
	
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
    /// Removes SPACE and TAB characters that appear <em>immediately</em> before
    /// an LF (<c>"\n"</c>) or CRLF (<c>"\r\n"</c>) sequence.
    /// </summary>
    /// <param name="text">Input string (may be <c>null</c> or empty).</param>
    /// <param name="scratch">
    /// Optional <see cref="StringBuilder"/> taken from an <em>ObjectPool</em>.  
    /// If supplied it is cleared up-front and used only when a rewrite
    /// becomes necessary.</param>
    public static string TrimWhitespaceBeforeLineBreaks(this string? text, StringBuilder? scratch = null)
    {
        if (string.IsNullOrEmpty(text))
            return text ?? string.Empty;

        var src = text.AsSpan();
        var len = src.Length;

        scratch?.Clear();
        StringBuilder? sb  = null;
        
        var tail = 0; // start of segment not yet copied
        var i    = 0;

        while (i < len)
        {
            var rel = NextNl(src[i..]);
            
            if (rel < 0)
                break; // no more newlines

            var nlPos = i + rel; // absolute index of first NL char

            // Identify kind of line-break token
            int tokenLen;

            if (src[nlPos] == '\n')
            {
                tokenLen = 1; // LF
            }
            else if (nlPos + 1 < len && src[nlPos + 1] == '\n')
            {
                tokenLen = 2; // CRLF
            }
            else
            {
                // Lone CR – not part of CRLF; leave unchanged
                i = nlPos + 1;
                continue;
            }

            // Walk backwards over spaces / tabs
            var wsStart = nlPos;

            while (wsStart > tail)
            {
                var ch = src[wsStart - 1];
                
                if (ch != ' ' && ch != '\t')
	                break;
                
                wsStart--;
            }

            if (wsStart == nlPos)
            {
                // No trailing WS on this line
                i = nlPos + tokenLen;
                continue;
            }

            sb ??= (scratch ?? new StringBuilder(text.Length));
            sb.Append(src.Slice(tail, wsStart - tail)); // Copy up to WS
            sb.Append(src.Slice(nlPos, tokenLen)); // The newline

            tail = nlPos + tokenLen; // Skip WS + NL
            i = tail; // Continue scan
        }

        if (sb is null)
            return text; // Nothing trimmed, return original string

        sb.Append(src[tail..]); // Copy remainder

        return sb.ToString();

        // SIMD-accelerated search for '\n' or '\r'
        static int NextNl(ReadOnlySpan<char> span) => span.IndexOfAny('\n', '\r');
    }	
	
    /// <summary>
    /// Collapses every run of <paramref name="minTokens"/> or more newline
    /// tokens (LF or CRLF) down to exactly two newline tokens.
    /// </summary>
    /// <param name="text">Input. May be <c>null</c> or empty.</param>
    /// <param name="useCrlf">
    /// When <c>true</c>, the replacement is “<c>\r\n\r\n</c>”; otherwise
    /// “<c>\n\n</c>”.</param>
    /// <param name="scratch">
    /// Optional <see cref="StringBuilder"/> (typically from an <em>ObjectPool</em>).
    /// It is always <b>cleared</b> before use.</param>
    /// <param name="minTokens">Defaults to <c>3</c>.</param>
    /// <remarks>Returns the original string unchanged when no consolidation is
    /// required; thus zero allocations on the fast path.</remarks>
    public static string ConsolidateLineBreaks(this string? text, bool useCrlf = false, StringBuilder? scratch = null, int minTokens = 3)
    {
        if (string.IsNullOrEmpty(text))
            return text ?? string.Empty;

        var src = text.AsSpan();

        /*--------------- FAST VETO -----------------
         * If we cannot find “minTokens” consecutive LF characters or
         * “minTokens” consecutive CRLF pairs, we know no rewrite is needed.
         * The simple IndexOf/Contains calls are heavily vectorised in
         * CoreCLR and save us from a char-by-char scan in the common case.
         */
        if (src.IndexOf("\n\n\n".AsSpan()[..minTokens]) < 0 && src.IndexOf("\r\n\r\n\r\n".AsSpan()[..(minTokens * 2)]) < 0)
	        return text;

        scratch?.Clear();
        StringBuilder? sb = null;

        var tail = 0; // start of segment not yet copied
        var i = 0;
        var len = src.Length;

        ReadOnlySpan<char> token = useCrlf ? "\r\n\r\n" : "\n\n";

        while (i < len)
        {
            // Jump straight to the next \n or \r (if any)
            var rel = NextNewline(src[i..]);

            if (rel < 0)
                break; // Remainder has no newlines

            var pos = i + rel; // Absolute index of 1st NL
            var runTokens = 0; // Length in NL tokens

            while (pos < len)
            {
                if (src[pos] == '\n')
                {
                    runTokens++; pos++; // LF
                }
                else if (src[pos] == '\r' && pos + 1 < len && src[pos + 1] == '\n')
                {
                    runTokens++; pos += 2; // CRLF
                }
                else
                {
                    break; // Not part of NL run
                }
            }

            if (runTokens < minTokens)
            {
                // Keep it verbatim – move on
                i = pos;
                continue;
            }

            sb ??= (scratch ?? new StringBuilder(text.Length));
            sb.Append(src.Slice(tail, (i + rel) - tail)); // Copy head
            sb.Append(token); // Always 2 tokens
            tail = pos; // Skip the run
            i = pos; // Resume scanning
        }

        if (sb is null)
            return text; // No changes, return original string

        sb.Append(src[tail..]); // Copy tail

        return sb.ToString();

        static int NextNewline(ReadOnlySpan<char> span) => span.IndexOfAny('\n', '\r'); // SIMD-accelerated
    }
    
	/// <summary>
	/// Removes all C-style block comments ( /* … */ ) from <paramref name="source"/>.
	/// If a closing <c>*/</c> is missing the rest of the text is treated as a comment.
	/// Nested block comments are NOT supported; the first closing token ends the comment.
	/// </summary>
	public static string RemoveBlockComments(this string? source, StringBuilder? workingSb = null)
	{
		if (string.IsNullOrEmpty(source))
			return source ?? string.Empty;

		workingSb ??= new StringBuilder(source.Length);
		workingSb.Clear();
		
		var span = source.AsSpan();
		var i = 0;
		var n = span.Length;

		while (i < n)
		{
			if (span[i] == '/' && i + 1 < n && span[i + 1] == '*')
			{
				var depth = 1; // we just saw the outermost "/*"
				i += 2;

				// consume until depth returns to 0 or input ends
				while (i < n && depth > 0)
				{
					switch (span[i])
					{
						case '/' when i + 1 < n && span[i + 1] == '*':
							depth++; // nested opener
							i += 2;
							break;
						case '*' when i + 1 < n && span[i + 1] == '/':
							depth--; // closer
							i += 2;
							break;
						default:
							i++; // ordinary char inside comment
							break;
					}
				}

				// if EOF hit with depth > 0 the whole tail was a comment → done
				continue;
			}

			workingSb.Append(span[i]);
			i++;
		}

		return workingSb.ToString();
	}	
	
	public static string CssSelectorEscape(this string value)
	{
		if (string.IsNullOrEmpty(value))
			return value;

		var maxLength = value.Length * 2;

		var buffer = maxLength <= 512 // an arbitrary cutoff
			? stackalloc char[maxLength]
			: new char[maxLength]; // fallback to heap if too large

		var position = 0;

		for (var i = 0; i < value.Length; i++)
		{
			var c = value[i];
			
			if ((i == 0 && char.IsDigit(c)) || (!char.IsLetterOrDigit(c) && c != '-' && c != '_'))
				buffer[position++] = '\\';

			buffer[position++] = c;
		}

		return new string(buffer[..position]);
	}
	
    public static string ToSentenceCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return char.ToUpper(input[0]) + input[1..];
    }
    
	/// <summary>
	/// Removes spaces and newlines from a string.
	/// </summary>
	/// <param name="css"></param>
	/// <param name="workingSb"></param>
	/// <returns></returns>
	public static string CompactCss(this string css, StringBuilder? workingSb = null)
	{
		workingSb ??= new StringBuilder();
		workingSb.Clear();
		
	    // Remove line breaks, block comments
	    var result = css
		    .RemoveBlockComments(workingSb)
			.RemoveCssRemovals(workingSb);

	    // Consolidate spaces
	    result = result.ConsolidateSpaces(workingSb).Trim();

	    // Spaces before delimiters are not needed 
	    result = result.RemoveDelimiterSpaces(workingSb);

	    // Last property value does not need a semicolon
	    result = result.Replace(";}", "}");

	    // 0 values do not need a unit
	    result = result.RemoveZeroUnits(workingSb);

        return result;
    }

	private static string RemoveCssRemovals(this string css, StringBuilder? workingSb = null)
	{
		workingSb ??= new StringBuilder(css.Length);
		workingSb.Clear();
		
		var i = 0;
		var n = css.Length;

		while (i < n)
		{
			var c = css[i];

			if (c is '\r' or '\n')
			{
				// skip all CR/LF
				while (i < n && (css[i] == '\r' || css[i] == '\n'))
					i++;

				// skip any following whitespace (but not newlines)
				while (i < n && char.IsWhiteSpace(css[i]) && css[i] != '\r' && css[i] != '\n')
					i++;

				// collapse into single space (unless we're at start or already have one)
				if (workingSb.Length > 0 && workingSb[^1] != ' ')
					workingSb.Append(' ');
			}
			else
			{
				workingSb.Append(c);
				i++;
			}
		}

		return workingSb.ToString();
	}	
	
	public static string ConsolidateSpaces(this string css, StringBuilder? workingSb = null)
	{
		workingSb ??= new StringBuilder(css.Length);
		workingSb.Clear();

		var i = 0;
		var n = css.Length;
		var lastWasSpace = false;

		while (i < n)
		{
			var c = css[i];
			
			if (char.IsWhiteSpace(c))
			{
				// append one space if previous char wasn't a space
				if (lastWasSpace == false)
				{
					workingSb.Append(' ');
					lastWasSpace = true;
				}

				// skip all following whitespace
				while (i < n && char.IsWhiteSpace(css[i]))
					i++;
			}
			else
			{
				workingSb.Append(c);
				lastWasSpace = false;
				i++;
			}
		}

		return workingSb.ToString();
	}	
	
	private static string RemoveDelimiterSpaces(this string css, StringBuilder? workingSb = null)
	{
		workingSb ??= new StringBuilder(css.Length);
		workingSb.Clear();
		
		if (string.IsNullOrEmpty(css))
			return css;

		// delimiters we want to “hug”
		var delimiters = new HashSet<char> { ':', ',', ';', '{', '}' };

		var i = 0;
		var n = css.Length;

		while (i < n)
		{
			var c = css[i];

			// If we see any whitespace, peek to see if it's around a delimiter
			if (char.IsWhiteSpace(c))
			{
				var j = i;
				
				// skip all whitespace
				while (j < n && char.IsWhiteSpace(css[j]))
					j++;

				// if next real char is a delimiter, emit that and skip trailing whitespace
				if (j < n && delimiters.Contains(css[j]))
				{
					workingSb.Append(css[j]);
					j++;
					
					while (j < n && char.IsWhiteSpace(css[j]))
						j++;
					
					i = j;
					
					continue;
				}

				// otherwise, preserve a single space (optional—remove this if you never want any whitespace here)
				workingSb.Append(' ');

				i = j;
			}
			else if (delimiters.Contains(c))
			{
				// emit the delimiter, then skip any whitespace that follows
				workingSb.Append(c);

				var j = i + 1;
				
				while (j < n && char.IsWhiteSpace(css[j]))
					j++;
				
				i = j;
			}
			else
			{
				workingSb.Append(c);

				i++;
			}
		}

		return workingSb.ToString();
	}
	
	private static string RemoveZeroUnits(this string css, StringBuilder? workingSb = null)
	{
		workingSb ??= new StringBuilder(css.Length);
		workingSb.Clear();

		if (string.IsNullOrEmpty(css)) return css;

		// Units sorted by descending length so we match "cqmax" before "cq" etc.
		string[] units =
		[
			"cqmin","cqmax","vmax","vmin","cqw","cqh","cqi","cqb","rem","cm","in","mm","pc","pt","px","ch","em","ex","vw","vh","Q","%"
		];

		var i = 0;
		var n = css.Length;

		while (i < n)
		{
			var c = css[i];

			// if whitespace or colon, and next char is '0', maybe strip a unit
			if ((char.IsWhiteSpace(c) || c == ':') && i + 1 < n && css[i + 1] == '0')
			{
				var stripped = false;

				// try each unit
				foreach (var u in units)
				{
					var len = u.Length;
					
					if (i + 2 + len <= n && string.Compare(css, i + 2, u, 0, len, StringComparison.Ordinal) == 0)
					{
						// emit the whitespace/colon + '0', skip over the unit
						workingSb.Append(c).Append('0');
						i += 2 + len;
						stripped = true;
					
						break;
					}
				}

				if (stripped)
					continue;
			}

			// default: copy one char
			workingSb.Append(c);
			i++;
		}

		return workingSb.ToString();
	}
	
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

        if (count < 0)
            count = 0;

        if (count == 0)
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

	/// <summary>
	/// Convert a numeric pixel value to a rem value string, including the "rem" unit.
	/// </summary>
	/// <param name="pixels"></param>
	/// <returns></returns>
	public static string PxToRem(this decimal pixels)
	{
		return $"{(pixels / 16m):#0.######}rem";
	}

	/// <summary>
	/// Convert a numeric pixel value to a rem value string, including the "rem" unit.
	/// </summary>
	/// <param name="pixels"></param>
	/// <returns></returns>
	public static string PxToRem(this int pixels)
	{
		return $"{(pixels / 16m):#0.######}rem";
	}

    /// <summary>
    /// Convert a numeric pixel value to a rem value string, including the "rem" unit.
    /// </summary>
    /// <param name="pixels"></param>
    /// <returns></returns>
    public static string PxToRem(this double pixels)
    {
        return $"{(pixels / 16):#0.######}rem";
    }

    /// <summary>
    /// Convert a pixel string value to a rem value string, including the "rem" unit.
    /// </summary>
    /// <param name="pixelVal"></param>
    /// <returns></returns>
    public static string PxToRem(this string pixelVal)
	{
		if (decimal.TryParse(pixelVal.Trim().TrimEnd("px")?.Trim(), out var pixels) == false)
			return "0rem";
		
		return $"{(pixels / 16):#0.######}rem";
	}

	/// <summary>
	/// Convert a web color to a rgba() value.
	/// Handles rgb() and hex colors.
	/// Optionally set a new opacity value (0-100).
	/// </summary>
	/// <param name="color"></param>
	/// <param name="opacityPct"></param>
	/// <returns>rgba() value, or rgba(0,0,0,-0) on error</returns>
	public static string SetWebColorAlpha(this string color, int opacityPct)
	{
		return color.SetWebColorAlpha(opacityPct / 100d);
	}

	/// <summary>
	/// Add an alpha value to a web color (hex, rgb, rgba, oklch, etc.).
	/// </summary>
	/// <param name="color"></param>
	/// <param name="opacity">0.0 to 1.0</param>
	/// <returns>rgba() value, or rgba(0,0,0,-0) on error</returns>
	public static string SetWebColorAlpha(this string color, double opacity = 1.0)
	{
		if (string.IsNullOrEmpty(color.Trim()))
			return string.Empty;
		
		if (opacity is < 0 or > 1.0)
			return color;

		color = color.Trim();
		
		var indexOpen = color.IndexOf('(');
		var indexClose = color.LastIndexOf(')');
		
		#region OKLCH Color

		var oklchIndex = color.IndexOf("oklch", StringComparison.Ordinal);

		if (oklchIndex == 0)
		{
			if (indexOpen < 0 || indexClose < 0 || indexClose <= indexOpen)
				return color;

			var segments = (color.Replace("/", " / ").TrimStart("oklch(")?.TrimEnd(')') ?? string.Empty).Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
			if (segments.Length != 3 && segments.Length != 5)
				return color;

			var alpha = opacity;

			if (segments.Length == 5 && opacity < 1.0d)
			{
				if (double.TryParse(segments[4], out var existing))
					alpha = (opacity > 0 ? existing * opacity : 0);
			}
			
			return alpha >= 1.0d ? $"oklch({segments[0]} {segments[1]} {segments[2]})" : $"oklch({segments[0]} {segments[1]} {segments[2]} / {$"{alpha:0.#######}".TrimEnd('0')})";
		}
        
		#endregion

		color = color.Trim().Replace(" ", string.Empty);

		var hexIndex = color.IndexOf('#');
		var rgbIndex = color.IndexOf("rgb", StringComparison.Ordinal);

		#region Named Color Conversion
		
		if (hexIndex == -1 && rgbIndex == -1)
		{
			if (CssNamedColors.TryGetValue(color, out var namedColor))
			{
				color = namedColor;
				rgbIndex = color.IndexOf("rgb", StringComparison.Ordinal);
			}
		}
		
		#endregion

		#region Hex Color

		if (hexIndex == 0)
		{
			color = color[1..];

			if (color.Length is not (3 or 4 or 6 or 8))
				return color;
		
			if (color.Length is 3)
				color = color[0].ToString() + color[0] + color[1] + color[1] + color[2] + color[2];

			if (color.Length is 4)
				color = color[0].ToString() + color[0] + color[1] + color[1] + color[2] + color[2] + color[3] + color[3];

			var red = Convert.ToInt32(color[..2], 16);
			var green = Convert.ToInt32(color.Substring(2, 2), 16);
			var blue = Convert.ToInt32(color.Substring(4, 2), 16);
			var alpha = 1d;

			if (color.Length == 8 && opacity < 1.0d)
			{
				var a = Convert.ToInt32(color[6..], 16);
				alpha = Math.Round((double)a / 255, 2);
			}

			var o1 = opacity > 0 ? alpha * opacity : 0;

			return o1 >= 1.0d ?  $"rgb({red},{green},{blue})" : $"rgba({red},{green},{blue},{$"{o1:0.#######}".TrimEnd('0')})";
		}		
		
		#endregion		
		
		#region RGB/A Color

		if (rgbIndex == 0)
		{
			color = color.TrimStart("rgb(") ?? string.Empty;
			color = color.TrimStart("rgba(") ?? string.Empty;
			color = color.TrimEnd(';');
			color = color.TrimEnd(')');

			var segments = color.Split(',', StringSplitOptions.RemoveEmptyEntries);

			if (segments.Length != 3 && segments.Length != 4)
				return color;

			if (int.TryParse(segments[0].Trim('-'), out var red) == false)
				return color;
			
			if (int.TryParse(segments[1].Trim('-'), out var green) == false)
				return color;

			if (int.TryParse(segments[2].Trim('-'), out var blue) == false)
				return color;

			if (red < 0 || green < 0 || blue < 0)
				return color;

			if (red > 255 || green > 255 || blue > 255)
				return color;
			
			var alpha = 1d;

			if (opacity < 1.0d)
			{
				if (segments.Length == 4)
					_ = double.TryParse(segments[3].Trim('-'), out alpha);

				if (alpha < 0)
					alpha = 0;

				if (alpha > 1)
					alpha = 1;
			}

			var o1 = opacity > 0 ? alpha * opacity : 0;

			return o1 >= 1.0d ?  $"rgb({red},{green},{blue})" : $"rgba({red},{green},{blue},{$"{o1:0.#######}".TrimEnd('0')})";
		}

		#endregion

		return color;
	}
	
	#endregion
	
	#region Time

	/// <summary>
	/// Format the elapsed time as a more friendly time span with a custom delimiter.
	/// Like: 3d : 5h : 12m : 15s or 3d+5h+12m+15s
	/// </summary>
	/// <param name="timer"></param>
	/// <param name="delimiter">Text to separate time elements; defaults to " : ".</param>
	/// <returns>Formatted timespan</returns>
	public static string FormatTimer(this Stopwatch timer, string delimiter = ":")
	{
		return FormatTimer(TimeSpan.FromMilliseconds(timer.ElapsedMilliseconds), delimiter);
	}
	
	/// <summary>
	/// Format the elapsed time as a more friendly time span with a custom delimiter.
	/// Like: 3d : 5h : 12m : 15s or 3d+5h+12m+15s
	/// </summary>
	/// <param name="timespan"></param>
	/// <param name="delimiter">Text to separate time elements; defaults to " : ".</param>
	/// <returns>Formatted timespan</returns>
	public static string FormatTimer(this TimeSpan timespan, string delimiter = ":")
	{
		var seconds = $"{timespan.TotalSeconds:0.00000000000000000}";

		if (timespan.TotalSeconds < 60)
			return $"{seconds[..(seconds.IndexOf('.') + 4)]}s";

		seconds = $"{timespan.Seconds:00.00000000000000000}";
		
		if (timespan is { Days: 0, Hours: 0 })
			return $"{timespan.Minutes:00}m{delimiter}{seconds[..(seconds.IndexOf('.') + 4)]}s";

		if (timespan.Days == 0)
		{
			return $"{timespan.Hours:00}h{delimiter}{timespan.Minutes:00}m{delimiter}{seconds[..(seconds.IndexOf('.') + 4)]}s";
		}

		return $"{timespan.Days:00}d{delimiter}{timespan.Hours:00}h{delimiter}{timespan.Minutes:00}m{delimiter}{seconds[..(seconds.IndexOf('.') + 4)]}s";
	}
	
	#endregion
    
    #region Console

    public static IEnumerable<string> WrapTextAtMaxWidth(string input, int maxLength)
    {
        var result = new List<string>();
        var indentation = IndentationSpacesRegex().Match(input).Value;
        var words = input.TrimStart().Split(' ');
        var currentLineLength = indentation.Length;
        var currentLine = new StringBuilder(indentation);
        var bulletIndentation = string.Empty;

        if (input.Trim().StartsWith("- ") || input.Trim().StartsWith("* "))
            bulletIndentation = "  ";
	    
        foreach (var word in words)
        {
            if (currentLineLength + word.Length + (result.Count > 0 ? bulletIndentation.Length : 0) > maxLength)
            {
                result.Add($"{(result.Count > 0 ? bulletIndentation : string.Empty)}{currentLine}");
                currentLine.Clear();
                currentLine.Append(indentation);
                currentLineLength = indentation.Length;
            }

            currentLine.Append(word);
            currentLine.Append(' ');
            currentLineLength += word.Length + 1;
        }

        if (currentLine.Length > indentation.Length + (result.Count > 0 ? bulletIndentation.Length : 0))
            result.Add($"{(result.Count > 0 ? bulletIndentation : string.Empty)}{currentLine}");

        return result;
    }
	
    public static void WriteToConsole(this string text, int maxCharacters)
    {
        if (string.IsNullOrEmpty(text))
            return;

        var result = new List<string>();
        var lines = text.Trim().NormalizeLinebreaks().Replace("_\n", " ").Split('\n');

        foreach (var line in lines)
            result.AddRange(WrapTextAtMaxWidth(line, maxCharacters));

        foreach (var line in result)
        {
            Console.WriteLine(line.NormalizeLinebreaks(Environment.NewLine));
        }
    }

    [GeneratedRegex(@"^\s+")]
    private static partial Regex IndentationSpacesRegex();

    #endregion
}
