using System.Globalization;

namespace Argentini.Sfumato.Helpers;

/// <summary>
/// Various tools to make using StringBuilders more like using strings. 
/// </summary>
public static class StringBuilders
{
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

    /// <summary>
	/// Finds all blocks like “@media (prefers-color-scheme: dark) { … }” blocks in this StringBuilder,
	/// matching braces even if there are nested blocks inside.
	/// </summary>
	/// <param name="sb">Source CSS</param>
	/// <param name="cssBlockDeclaration">Set as block declaration value like "@media (prefers-color-scheme: dark) {"</param>
	public static IEnumerable<string> FindMediaBlocks(this StringBuilder? sb, string cssBlockDeclaration)
	{
		if (sb is null)
			yield break;

		// Grab the full content once
		var content = sb.ToString();
		var searchPos = 0;

		while (true)
		{
			// Find the next css block declaration start
			var startIdx = content.IndexOf(cssBlockDeclaration, searchPos, StringComparison.Ordinal);
			
			if (startIdx == -1)
				yield break;

			// The “{” is the last character of cssBlockDeclaration
			var braceIdx = startIdx + cssBlockDeclaration.Length - 1;
			var pos = braceIdx + 1;
			var depth = 1;

			// Advance, adjusting depth for every { or }
			while (pos < content.Length && depth > 0)
			{
				var c = content[pos];
				
				if (c == '{')
					depth++;
				else if (c == '}')
					depth--;
				
				pos++;
			}

			// If we closed everything, slice it out
			if (depth == 0)
			{
				var length = pos - startIdx; 
				
				yield return content.Substring(startIdx, length);
				
				// Continue searching after this block
				searchPos = pos;
			}
			else
			{
				// Unbalanced braces—stop scanning
				yield break;
			}
		}
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
	/// Normalize line breaks in a StringBuilder
	/// </summary>
	/// <param name="source"></param>
	/// <param name="linebreak">Line break to use (default: "\n")</param>
	public static void NormalizeLinebreaks(this StringBuilder source, string? linebreak = "\n")
	{
		if (source.IsEmpty()) return;

		if (linebreak == null || linebreak.IsEmpty()) return;
        
		if (source.Contains("\r\n") && linebreak != "\r\n")
			source.Replace("\r\n", linebreak);

		else if (source.Contains('\r') && linebreak != "\r")
			source.Replace("\r", linebreak);

		else if (source.Contains('\n') && linebreak != "\n")
			source.Replace("\n", linebreak);
	}
	
    /// <summary>
    /// Enumerates every occurrence of token that is
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
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Token must be non-empty.", nameof(token));

        var length = sb.Length;
        var tokenLen = token.Length;
        var t0 = token[0]; // first char for quick scan

        for (var i = 0; i <= length - tokenLen; i++)
        {
            // 1. Quick-fail: first character must match
            if (sb[i] != t0)
	            continue;

            // 2. Check the rest of the token
            var match = true;
            
            for (var k = 1; k < tokenLen; k++)
            {
                if (sb[i + k] != token[k])
                {
                    match = false;
                    break;
                }
            }
            
            if (match == false)
	            continue;

            // 3. Skip whitespace after the token
            var p = i + tokenLen;
            
            while (p < length && char.IsWhiteSpace(sb[p]))
	            p++;

            // 4. Must see an opening parenthesis
            if (p >= length || sb[p] != '(')
	            continue;

            // 5. Walk forward, tracking depth to find the matching ')' for the *outer* '('
            var depth = 0;
            var j = p;
            
            while (j < length)
            {
                var c = sb[j];
                
                if (c == '(')
                {
                    depth++;
                }
                else if (c == ')')
                {
                    depth--;
                
                    if (depth == 0)
                    {
                        // Found the balancing ')' – emit the slice
                        yield return sb.ToString(i, j - i + 1);

                        // Advance i so we don’t rescan inside the just-matched span
                        i = j;
                    
                        break;
                    }
                }

                j++;
            }

            // If the loop exits without depth hitting zero, parentheses were unbalanced;
            // we simply fall through and continue searching (no match yielded).
        }
    }
}
