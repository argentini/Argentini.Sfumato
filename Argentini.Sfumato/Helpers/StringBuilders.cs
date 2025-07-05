// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable MemberCanBePrivate.Global

using System.Globalization;

namespace Argentini.Sfumato.Helpers;

/// <summary>
/// Various tools to make using StringBuilders more like using strings. 
/// </summary>
public static class StringBuilders
{
	/// <summary>
	/// One-liner for replacing the content in a StringBuilder with all or part of a string.
	/// </summary>
	/// <param name="sb"></param>
	/// <param name="content"></param>
	/// <param name="index"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public static StringBuilder? ReplaceContent(this StringBuilder? sb, string? content, int index = -1, int length = -1)
	{
		if (sb is null)
			return null;

		sb.Clear();

		if (string.IsNullOrEmpty(content))
			return sb;

		var contentLen = content.Length;

		// Determine start:
		var start = index < 0 ? 0 : index;
		
		if (start >= contentLen) 
			return sb; // nothing to append

		var count = length < 0 ? contentLen - start : length;

		if (count <= 0) 
			return sb; // nothing to append

		if (start + count > contentLen)
			count = contentLen - start;

		if (start == 0 && count == contentLen)
			sb.Append(content);
		else
			sb.Append(content, start, count);

		return sb;
	}

	/// <summary>
	/// One-liner for replacing the content in a StringBuilder with all or part of another StringBuilder.
	/// </summary>
	/// <param name="sb"></param>
	/// <param name="content"></param>
	/// <param name="index"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public static StringBuilder? ReplaceContent(this StringBuilder sb, StringBuilder content, int index = -1, int length = -1)
	{
		sb.Clear();

		if (index == -1 && length == -1)
		{
			sb.Append(content);
			return sb;
		}

		var contentLen = content.Length;

		// Determine start:
		var start = index < 0 ? 0 : index;
		
		if (start >= contentLen) 
			return sb; // nothing to append

		var count = length < 0 ? contentLen - start : length;

		if (count <= 0) 
			return sb; // nothing to append

		if (start + count > contentLen)
			count = contentLen - start;

		if (start == 0 && count == contentLen)
			sb.Append(content);
		else
			sb.Append(content, start, count);

		return sb;
	}

	/// <summary>
    /// Normalize all “\r\n”, “\r” and “\n” sequences into the specified linebreak,
    /// modifying the passed-in StringBuilder in place.
    /// </summary>
    public static void NormalizeLinebreaks(this StringBuilder? sb, string linebreak = "\n")
    {
        if (sb is null || sb.Length == 0)
            return;

        // First pass: count how many CRLF vs. lone CR or LF we have
        var len = sb.Length;
        var countCrlf = 0;
	    var countSingle = 0;

	    for (var i = 0; i < len; i++)
        {
            var c = sb[i];
            
            if (c == '\r')
            {
                if (i + 1 < len && sb[i + 1] == '\n')
                {
                    countCrlf++;
                    i++; // skip the \n
                }
                else
                {
                    countSingle++;
                }
            }
            else if (c == '\n')
            {
                countSingle++;
            }
        }

        // Nothing to normalize?
        if (countCrlf == 0 && countSingle == 0)
            return;

        // Compute new total length
        var brLen = linebreak.Length;
        var totalTokens = countCrlf + countSingle;
        var newLen = len
                     - (countCrlf * 2 + countSingle * 1)
                     + totalTokens * brLen;

        // Second pass: build into a single char[] buffer
        var buffer = new char[newLen];
        var dst = 0;
        
        for (var i = 0; i < len; i++)
        {
            var c = sb[i];
            
            if (c == '\r')
            {
                if (i + 1 < len && sb[i + 1] == '\n')
                {
                    // CRLF → linebreak
                    for (var j = 0; j < brLen; j++)
                        buffer[dst + j] = linebreak[j];

                    dst += brLen;
                    i++;
                }
                else
                {
                    // lone CR → linebreak
                    for (var j = 0; j < brLen; j++)
                        buffer[dst + j] = linebreak[j];

                    dst += brLen;
                }
            }
            else if (c == '\n')
            {
                // lone LF → linebreak
                for (var j = 0; j < brLen; j++)
                    buffer[dst + j] = linebreak[j];
                
                dst += brLen;
            }
            else
            {
                buffer[dst++] = c;
            }
        }

        // Replace the builder’s contents in one go
        sb.Clear();
        sb.Append(buffer, 0, newLen);
    }

    /// <summary>
    /// Normalize all ‘/’ and ‘\’ characters in the StringBuilder to the native 
    /// Path.DirectorySeparatorChar, modifying the builder in-place.
    /// </summary>
    /// <param name="sb">The StringBuilder whose contents will be normalized.</param>
    public static void SetNativePathSeparators(this StringBuilder? sb)
    {
	    if (sb is null || sb.Length == 0) 
		    return;

	    var sep = Path.DirectorySeparatorChar;
	    var len = sb.Length;

	    for (var i = 0; i < len; i++)
	    {
		    var c = sb[i];
		    
		    // if it's a slash of either kind, and not already the native sep, replace it
		    if (c is '/' or '\\' && c != sep)
		    {
			    sb[i] = sep;
		    }
	    }
    }

    /// <summary>
    /// Returns true if the StringBuilder ends with the given character.
    /// </summary>
    /// <param name="sb">The StringBuilder to inspect.</param>
    /// <param name="value">The character to compare to the last character.</param>
    /// <returns>True if sb is non‐empty and its last character equals value; otherwise false.</returns>
    public static bool EndsWith(this StringBuilder sb, char value)
    {
	    var len = sb.Length;
	    
	    if (len == 0)
		    return false;

	    return sb[len - 1] == value;
    }
    
    /// <summary>
    /// Returns the index of the last occurrence of a specified character in the StringBuilder,
    /// or -1 if the character is not found.
    /// </summary>
    /// <param name="sb">The StringBuilder to search.</param>
    /// <param name="value">The character to locate.</param>
    /// <returns>
    /// The zero-based index position of value if found; otherwise, –1. 
    /// </returns>
    public static int LastIndexOf(this StringBuilder sb, char value)
    {
	    for (var i = sb.Length - 1; i >= 0; i--)
	    {
		    if (sb[i] == value)
			    return i;
	    }

	    return -1;
    }
    
    /// <summary>
    /// Removes all leading characters in <paramref name="trimChars"/> from the start of the builder.
    /// </summary>
    public static void TrimStart(this StringBuilder sb, params char[] trimChars)
    {
	    if (trimChars.Length == 0)
		    return;

	    var len = sb.Length;
	    var start = 0;
	    
	    while (start < len && trimChars.Contains(sb[start]))
		    start++;

	    if (start > 0)
		    sb.Remove(0, start);
    }

    /// <summary>
    /// Removes all trailing characters in <paramref name="trimChars"/> from the end of the builder.
    /// </summary>
    public static void TrimEnd(this StringBuilder sb, params char[] trimChars)
    {
	    if (trimChars.Length == 0)
		    return;

	    var end = sb.Length - 1;
	    
	    while (end >= 0 && trimChars.Contains(sb[end]))
		    end--;

	    // end is now index of last keep-char, so remove everything after it
	    if (end < sb.Length - 1)
		    sb.Remove(end + 1, sb.Length - end - 1);
    }

    /// <summary>
    /// Removes all leading and trailing characters in <paramref name="trimChars"/>.
    /// </summary>
    public static void Trim(this StringBuilder sb, params char[] trimChars)
    {
	    sb.TrimStart(trimChars);
	    sb.TrimEnd(trimChars);
    }
    
	/// <summary>
	/// Extract a block of CSS by its starting declaration (e.g. "@layer components").
	/// </summary>
	/// <param name="sourceCss"></param>
	/// <param name="cssBlockStart"></param>
	/// <param name="startIndex"></param>
	/// <returns></returns>
	public static (int Start, int Length) ExtractCssBlock(this StringBuilder? sourceCss, string cssBlockStart, int startIndex = 0)
	{
		if (sourceCss is null || sourceCss.Length == 0 || string.IsNullOrEmpty(cssBlockStart))
			return (-1, 0);

		// snapshot once
		var text = sourceCss.ToString();
		var n = text.Length;
		
		if (n == 0 || string.IsNullOrEmpty(cssBlockStart) || startIndex < 0 || startIndex >= n)
			return (-1, 0);

		// find the selector
		var blockStart = text.IndexOf(cssBlockStart, startIndex, StringComparison.Ordinal);
		
		if (blockStart < 0) 
			return (-1, 0);

		var span = text.AsSpan();
		var pos = blockStart + cssBlockStart.Length;

		// skip ASCII whitespace until the brace
		while (pos < n)
		{
			var c = span[pos];
			
			if (c == '{')
			{
				pos++;    // enter block
				break;
			}
			
			// ASCII whitespace: space, tab, LF, CR, FF, VT
			if (c > ' ' || (c != ' ' && c != '\t' && c != '\n' && c != '\r' && c != '\f' && c != '\v'))
				return (-1, 0);  // malformed: non-space before brace
			
			pos++;
		}

		if (pos >= n) 
			return (-1, 0);

		// scan the block for matching braces
		var depth = 1;
		var i     = pos;
		
		for (; i < n; i++)
		{
			var c = span[i];

			if (c == '{')
			{
				depth++;
			}
			else if (c == '}')
			{
				depth--;

				if (depth == 0)
					return (blockStart, i - blockStart + 1); // found matching '}' for our initial '{'
			}
		}

		// unterminated
		return (-1, 0);
	}

	/// <summary>
	/// Find the index of a character in a StringBuilder starting at a given index.
	/// </summary>
	/// <param name="sb"></param>
	/// <param name="value"></param>
	/// <param name="startIndex"></param>
	/// <returns></returns>
	public static int IndexOf(this StringBuilder? sb, char value, int startIndex)
	{
		if (sb == null || startIndex < 0 || startIndex >= sb.Length)
			return -1;

		for (var i = startIndex; i < sb.Length; i++)
		{
			if (sb[i] == value)
				return i;
		}

		return -1;
	}
	
	/// <summary>
	/// Find the index a substring in a StringBuilder.
	/// </summary>
	/// <param name="sb"></param>
	/// <param name="value"></param>
	/// <param name="startIndex"></param>
	/// <param name="comparisonType"></param>
	/// <returns></returns>
	public static int IndexOf(this StringBuilder? sb, string? value, int startIndex = 0, StringComparison comparisonType = StringComparison.Ordinal)
	{
		if (sb == null || string.IsNullOrEmpty(value) || (sb.Length - startIndex) < value.Length)
			return -1;

		for (var i = startIndex; i <= sb.Length - value.Length; i++)
		{
			var found = value.Where((t, j) => sb[i + j].ToString().Equals(t.ToString(), comparisonType) == false).Any() == false;

			if (found)
				return i;
		}

		return -1;
	}
	
	/// <summary>
	/// Reformats the CSS in this StringBuilder so that each block is indented
	/// by <paramref name="indentSize"/> spaces per nesting level.
	/// Also, properly indents /* … */ comments (both standalone and inline).
	/// </summary>
	/// <param name="source">The StringBuilder containing the CSS to reformat.</param>
	/// <param name="workingSb">Working StringBuilder instance (will be cleared).</param>
	/// <param name="indentSize">Number of spaces per indentation level (default: 4).</param>
	/// <returns>The same StringBuilder, now cleared and repopulated with formatted CSS.</returns>
	public static StringBuilder ReformatCss(this StringBuilder source, StringBuilder workingSb, int indentSize = 4)
	{
	    var css = source.ToString();
	    var depth = 0;
	    var inString = false;
	    var stringDelimiter = '\0';
	    var inComment = false;

	    for (var i = 0; i < css.Length; i++)
	    {
	        var c    = css[i];
	        var next = i + 1 < css.Length ? css[i + 1] : '\0';

	        // Enter comment?
	        if (inString == false && inComment == false && c == '/' && next == '*')
	        {
	            var atLineStart = workingSb.Length == 0 || workingSb[^1] == '\n';

	            if (atLineStart)
	                AppendIndent();
	            else
	                workingSb.Append(' ');

	            workingSb.Append("/*");
	            inComment = true;
	            i++; // consume '*'
	            
	            continue;
	        }

	        // Inside comment: copy until "*/", indent newlines
	        if (inComment)
	        {
		        // ReSharper disable once ConvertIfStatementToSwitchStatement
		        if (c == '*' && next == '/')
	            {
	                workingSb.Append("*/");
	                inComment = false;
	                i++; // consume '/'
	                workingSb.AppendLine();
	                AppendIndent();
	            }
	            else if (c == '\n')
	            {
	                workingSb.AppendLine();
	                AppendIndent();
	            }
	            else if (c != '\r')
	            {
	                workingSb.Append(c);
	            }

		        continue;
	        }

	        // Toggle string state
	        // ReSharper disable once ConvertIfStatementToSwitchStatement
	        if (inString == false && c is '"' or '\'')
	        {
	            inString = true;
	            stringDelimiter = c;
	            workingSb.Append(c);

	            continue;
	        }
	        
	        if (inString && c == stringDelimiter)
	        {
	            inString = false;
	            workingSb.Append(c);
	            
	            continue;
	        }
	        
	        if (inString == false)
	        {
	            // Outside strings: handle braces and semicolons
	            switch (c)
	            {
	                case '{':
	                    workingSb.Append(c);
	                    workingSb.AppendLine();
	                    depth++;
	                    AppendIndent();
	                    break;

	                case '}':
	                    workingSb.AppendLine();
	                    depth = Math.Max(0, depth - 1);
	                    AppendIndent();
	                    workingSb.Append(c);
	                    workingSb.AppendLine();
	                    AppendIndent();
	                    break;

	                case ';':
	                    workingSb.Append(c);
	                    var hasInlineComment = false;
	                    for (var j = i + 1; j < css.Length; j++)
	                    {
	                        if (char.IsWhiteSpace(css[j]))
	                        {
	                            continue;
	                        }
	                        if (css[j] == '/' && j + 1 < css.Length && css[j + 1] == '*')
	                        {
	                            hasInlineComment = true;
	                        }
	                        break;
	                    }
	                    if (hasInlineComment)
	                    {
	                        workingSb.Append(' ');
	                    }
	                    else
	                    {
	                        workingSb.AppendLine();
	                        AppendIndent();
	                    }
	                    break;

	                default:
	                    if (char.IsWhiteSpace(c))
	                    {
	                        if (workingSb.Length > 0 && !char.IsWhiteSpace(workingSb[^1]))
	                        {
	                            workingSb.Append(' ');
	                        }
	                    }
	                    else
	                    {
	                        workingSb.Append(c);
	                    }
	                    break;
	            }
	            
	            continue;
	        }

	        // Inside string: copy verbatim
	        workingSb.Append(c);
	    }

	    // Remove blank or whitespace‑only lines
	    var lines = workingSb
		    .ToString()
		    .Split(["\r\n", "\n"], StringSplitOptions.None);

	    workingSb.Clear();

	    foreach (var line in lines)
	    {
		    if (line.Trim().Length > 0)
			    workingSb.AppendLine(line);
	    }
	    
	    source.Clear();

	    return source.Append(workingSb);

	    void AppendIndent() => workingSb.Append(new string(' ', depth * indentSize));
	}
	
	public static byte[] ToByteArray(this StringBuilder sb, Encoding encoding)
	{
		var charCount = sb.Length;
		var charArray = new char[charCount];
		sb.CopyTo(0, charArray, 0, charCount);

		return encoding.GetBytes(charArray);
	}

	/// <summary>
	/// Remove a string from the beginning of a StringBuilder
	/// </summary>
	/// <param name="source">The StringBuilder to search</param>
	/// <param name="substring">The substring to remove</param>
	/// <param name="stringComparison"></param>
	public static void TrimStart(this StringBuilder source, string substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
	{
		while (source.Length >= substring.Length && source.Substring(0, substring.Length).Equals(substring, stringComparison))
			source.Remove(0, substring.Length);
	}

	/// <summary>
	/// Remove a string from the end of a StringBuilder
	/// </summary>
	/// <param name="source">The StringBuilder to search</param>
	/// <param name="substring">The substring to remove</param>
	/// <param name="stringComparison"></param>
	public static void TrimEnd(this StringBuilder source, string substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
    {
	    while (source.Length >= substring.Length && source.Substring(source.Length - substring.Length, substring.Length).Equals(substring, stringComparison))
		    source.Remove(source.Length - substring.Length, substring.Length);
    }
    
	/// <summary>
	/// Get a substring in a StringBuilder object.
	/// Exponentially faster than .ToString().Substring().
	/// </summary>
	/// <param name="source">The source StringBuilder object</param>
	/// <param name="startIndex">A zero-based start index</param>
	/// <param name="length">String length to retrieve</param>
	/// <returns>Substring or empty string if not found</returns>
	public static string Substring(this StringBuilder source, int startIndex, int length)
	{
		var result = string.Empty;

		if (source.Length <= 0) return result;
		if (startIndex < 0 || length <= 0) return result;
		if (startIndex + length > source.Length) return result;
		
		for (var x = startIndex; x < startIndex + length; x++)
			result += source[x];

		return result;
		
	}
	
    /// <summary>
    /// Determines if a StringBuilder object has a value (is not null or empty).
    /// </summary>
    /// <param name="sb">String to evaluate</param>
    public static bool HasValue(this StringBuilder? sb)
    {
		return sb is {Length: > 0};
    }

    /// <summary>
    /// Determines if a StringBuilder is empty or null.
    /// </summary>
    /// <param name="sb"></param>
    public static bool IsEmpty(this StringBuilder sb)
	{
		return sb.HasValue() == false;
	}

	/// <summary>
	/// Get the index of the last occurrence of a substring, or -1 if not found
	/// </summary>
	/// <param name="source"></param>
	/// <param name="substring"></param>
	/// <param name="stringComparison"></param>
	/// <returns></returns>
    public static int LastIndexOf(this StringBuilder? source, string? substring, StringComparison stringComparison = StringComparison.Ordinal)
    {
	    var result = -1;

	    if (source is null || source.IsEmpty() || string.IsNullOrEmpty(substring) || source.Length <= substring.Length)
		    return result;
	    
	    for (var x = source.Length - substring.Length - 1; x > -1; x--)
	    {
		    if (source.Substring(x, substring.Length).NotEquals(substring, stringComparison))
			    continue;
		    
		    result = x;
		    x = -1;
	    }

		return result;
    }

	/// <summary>
	/// Remove whitespace from the beginning and end of a StringBuilder
	/// </summary>
	/// <param name="source">The StringBuilder to trim</param>
	public static void Trim(this StringBuilder source)
	{
		var whitespace = new [] { ' ', '\t', '\n', '\r' };
		
		while (source.Length >= 1 && whitespace.Contains(source[0]))
			source.Remove(0, 1);
		
		while (source.Length >= 1 && whitespace.Contains(source[^1]))
			source.Remove(source.Length - 1, 1);
	}
	
	/// <summary>
	/// Count substring occurrences in a StringBuilder object.
	/// </summary>
	/// <param name="source">The StringBuilder to search</param>
	/// <param name="substring">The substring to count</param>
	/// <returns>Number of times the substring is within the source.</returns>
	public static int SubstringCount(this StringBuilder source, string substring)
	{
		return source.ToString().Split([substring], StringSplitOptions.None).Length - 1;
	}

	/// <summary>
	/// Alter StringBuilder object to become a substring.
	/// Exponentially faster than .ToString().Substring().
	/// </summary>
	/// <param name="source">The source StringBuilder object</param>
	/// <param name="startIndex">A zero-based start index</param>
	/// <param name="length">String length to retrieve</param>
	/// <returns>Substring or empty string if not found</returns>
	public static void MakeSubstring(this StringBuilder source, int startIndex, int length)
	{
		if (source.Length <= 0) return;
		if (startIndex < 0 || length <= 0) return;
		if (startIndex + length > source.Length) return;

		var newLength = startIndex + length;
		
		source.Remove(newLength, source.Length - newLength);
		
		if (startIndex > 0)
			source.Remove(0, startIndex);
	}

	/// <summary>
	/// Get the index of the first occurrence of a specific character in the StringBuilder.
	/// </summary>
	/// <param name="source">The source StringBuilder object</param>
	/// <param name="character">character to find</param>
	/// <returns>Index or -1 if not found</returns>
	public static int IndexOf(this StringBuilder source, char character)
	{
		var result = -1;

		if (source.Length < 1)
			return result;

		for (var x = 0; x < source.Length; x++)
		{
			if (source[x] != character)
				continue;
			
			result = x;
			x = source.Length;
		}

		return result;
	}
	
	/// <summary>
	/// Determine if a StringBuilder object starts with a string.
	/// </summary>
	/// <returns>True if the StringBuilder object starts with the substring</returns>
	public static bool StartsWith(this StringBuilder? sb, string? prefix, StringComparison comparison = StringComparison.Ordinal)
	{
		if (sb is null)
			return false;
		
		if (prefix is null)
			return false;

		var len = prefix.Length;
	
		if (len > sb.Length)
			return false;

		switch (comparison)
        {
            case StringComparison.Ordinal:
                
                for (var i = 0; i < len; i++)
                    if (sb[i] != prefix[i])
                        return false;
                
                return true;

            case StringComparison.OrdinalIgnoreCase:
                
                for (var i = 0; i < len; i++)
                    if (char.ToUpperInvariant(sb[i]) != char.ToUpperInvariant(prefix[i]))
                        return false;
                
                return true;

            case StringComparison.CurrentCulture:
            case StringComparison.CurrentCultureIgnoreCase:
            case StringComparison.InvariantCulture:
            case StringComparison.InvariantCultureIgnoreCase:

                Span<char> buffer = stackalloc char[len];
                
                for (var i = 0; i < len; i++)
                    buffer[i] = sb[i];

                var pattern = prefix.AsSpan();
                var options = comparison is StringComparison.CurrentCultureIgnoreCase or StringComparison.InvariantCultureIgnoreCase
                        ? CompareOptions.IgnoreCase
                        : CompareOptions.None;
                var culture = comparison is StringComparison.InvariantCulture or StringComparison.InvariantCultureIgnoreCase
                        ? CultureInfo.InvariantCulture
                        : CultureInfo.CurrentCulture;

                return culture.CompareInfo.IsPrefix(buffer, pattern, options);

            default:

	            var segment = sb.ToString(0, len);
                return segment.StartsWith(prefix, comparison);
        }
	}
	
	/// <summary>
	/// Determine if a StringBuilder object ends with a string.
	/// </summary>
	/// <param name="source">The StringBuilder object to evaluate</param>
	/// <param name="substring">Substring to find</param>
	/// <param name="caseInsensitive">Ignore case if true</param>
	/// <returns>True is the StringBuilder object ends with the substring</returns>
	public static bool EndsWith(this StringBuilder source, string substring, bool caseInsensitive = false)
	{
		return (caseInsensitive ? Substring(source, source.Length - substring.Length, substring.Length).ToUpper() : Substring(source, source.Length - substring.Length, substring.Length)) == (caseInsensitive ? substring.ToUpper() : substring);
	}
	
	/// <summary>
	/// Clone a StringBuilder instance
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	public static StringBuilder Clone(this StringBuilder source)
	{
		var maxCapacity = source.MaxCapacity;
		var capacity = source.Capacity;
		var newSb = new StringBuilder(capacity, maxCapacity);

		newSb.Append(source);

		return newSb;
	}
	
	/// <summary>
	/// Check a StringBuilder for a substring
	/// </summary>
	/// <param name="source">The StringBuilder to trim</param>
	/// <param name="substring">Substring to find</param>
	/// <param name="stringComparison">Comparison mode</param>
	public static bool Contains(this StringBuilder source, string substring, StringComparison stringComparison = StringComparison.Ordinal)
	{
		if (source.Length == 0 || string.IsNullOrEmpty(substring)) return false;
		if (substring.Length > source.Length) return false;

		for (var x = 0; x < source.Length; x++)
		{
			if (source.Substring(x, substring.Length).Equals(substring, stringComparison) == false)
				continue;

			return true;
		}

		return false;
	}

	/// <summary>
	/// Check a StringBuilder for a character
	/// </summary>
	/// <param name="source">The StringBuilder to trim</param>
	/// <param name="character">Substring to find</param>
	public static bool Contains(this StringBuilder source, char? character)
	{
		if (source.Length == 0 || character is null) return false;

		for (var x = 0; x < source.Length; x++)
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			if (source.Substring(x, 1).Equals(character.Value) == false)
				continue;

			return true;
		}

		return false;
	}
	
    /// <summary>
    /// Enumerates every occurrence of a token that is
    /// immediately followed (optionally after whitespace) by a balanced
    /// parenthetical expression.
    /// Each yielded string consists of the token plus the complete outer
    /// set of parentheses.
    /// </summary>
    /// <param name="sb">The source <see cref="StringBuilder"/>.</param>
    /// <param name="token">The literal text to search for (case-sensitive).</param>
    /// <returns>IEnumerable&lt;string&gt; of matches, streamed as they are found.</returns>
    public static IEnumerable<string> EnumerateTokenWithOuterParenthetical(this StringBuilder sb, string token)
    {
	    if (sb is null)
		    throw new ArgumentNullException(nameof(sb));
	    
	    if (string.IsNullOrEmpty(token))
		    throw new ArgumentException("Token must be non-empty.", nameof(token));

	    // 1) Snapshot to a single string for fast indexing + IndexOf
	    var text = sb.ToString();
	    var length = text.Length;
	    var tokenLen = token.Length;
	    var pos = 0;

	    // 2) Find each occurrence of `token`
	    while ((pos = text.IndexOf(token, pos, StringComparison.Ordinal)) != -1)
	    {
		    // 3) Skip any whitespace after the token
		    var p = pos + tokenLen;

		    while (p < length && char.IsWhiteSpace(text[p]))
			    p++;

		    // 4) Must be an opening '('
		    if (p >= length || text[p] != '(')
		    {
			    pos += tokenLen; // jump past this token
			    continue;
		    }

		    // 5) Balanced-parenthesis scan
		    var depth = 1;
		    var j = p + 1;

		    while (j < length && depth > 0)
		    {
			    switch (text[j++])
			    {
				    case '(': depth++; break;
				    case ')': depth--; break;
			    }
		    }

		    if (depth == 0)
		    {
			    // 6) We found the matching ')'
			    yield return text.Substring(pos, j - pos);

			    // 7) Advance `pos` so we don't re‐scan inside this match
			    pos = j;
		    }
		    else
		    {
			    // unbalanced → no more valid matches
			    yield break;
		    }
	    }
    }
}
