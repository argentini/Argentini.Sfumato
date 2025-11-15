using System.Buffers;

namespace Sfumato.Helpers;

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
	public static void ReplaceContent(this StringBuilder? sb, string? content, int index = -1, int length = -1)
	{
		if (sb is null)
			return;

		sb.Clear();

		if (string.IsNullOrEmpty(content))
			return;

		var contentLen = content.Length;

		// Determine start:
		var start = index < 0 ? 0 : index;
		
		if (start >= contentLen) 
			return; // nothing to append

		var count = length < 0 ? contentLen - start : length;

		if (count <= 0) 
			return; // nothing to append

		if (start + count > contentLen)
			count = contentLen - start;

		if (start == 0 && count == contentLen)
			sb.Append(content);
		else
			sb.Append(content, start, count);
	}

	/// <summary>
	/// One-liner for replacing the content in a StringBuilder with all or part of another StringBuilder.
	/// </summary>
	/// <param name="sb"></param>
	/// <param name="content"></param>
	/// <param name="index"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public static void ReplaceContent(this StringBuilder sb, StringBuilder content, int index = -1, int length = -1)
	{
		sb.Clear();

		if (index == -1 && length == -1)
		{
			sb.Append(content);
			return;
		}

		var contentLen = content.Length;

		// Determine start:
		var start = index < 0 ? 0 : index;
		
		if (start >= contentLen) 
			return; // nothing to append

		var count = length < 0 ? contentLen - start : length;

		if (count <= 0) 
			return; // nothing to append

		if (start + count > contentLen)
			count = contentLen - start;

		if (start == 0 && count == contentLen)
			sb.Append(content);
		else
			sb.Append(content, start, count);
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
					return (blockStart, i - blockStart + 1); // initial '{' has a matching '}'
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
	
	private const int MaxSpaces = 4096;
	private static readonly char[] SpacesBuffer = CreateSpaceBuffer();
	private static char[] CreateSpaceBuffer()
	{
		var buf = new char[MaxSpaces];

		for (var i = 0; i < MaxSpaces; i++)
			buf[i] = ' ';
		
		return buf;
	}
	
	/// <summary>
	/// Reformats the CSS in this StringBuilder so that each block is indented
	/// by <paramref name="indentSize"/> spaces per nesting level.
	/// Also removes comments.
	/// </summary>
	/// <param name="source">The StringBuilder containing the CSS to reformat.</param>
	/// <param name="indentSize">Number of spaces per indentation level (default: 4).</param>
	/// <returns>The same StringBuilder, now cleared and repopulated with formatted CSS.</returns>
    public static StringBuilder ReformatCss(this StringBuilder source, int indentSize = 4)
    {
        // 1) Read input and rent pooled buffer
        var css = source.ToString();
        var inLen = css.Length;
        var bufLen = inLen + (inLen >> 2) + 128;
        var buf = ArrayPool<char>.Shared.Rent(bufLen);
        var outIdx = 0;

        // 2) State
        var depth = 0;
        var inString = false;
        var stringDelim = '\0';

        // 3) Blank-line suppression and line-start tracking
        var lineStartIdx = 0;
        var anyContentInLine = false;

        // 4) One-pass formatting
        for (var i = 0; i < inLen; i++)
        {
            var c = css[i];
            var next = (i + 1 < inLen) ? css[i + 1] : '\0';

            // — skip /*…*/ entirely
            if (inString == false && c == '/' && next == '*')
            {
                i += 2;

                while (i + 1 < inLen && (css[i] == '*' && css[i + 1] == '/') == false)
                    i++;
                
                if (i + 1 < inLen)
                    i++;
                
                continue;
            }

            // — skip //… to the end of the line
            if (inString == false && c == '/' && next == '/')
            {
                i += 2;
                
                while (i < inLen && css[i] != '\n')
                    i++;
                
                if (i < inLen && css[i] == '\n')
                    WriteNewline();
                
                continue;
            }

            // — toggle into string literal
            if (inString == false && c is '"' or '\'')
            {
                if (outIdx == lineStartIdx)
                {
                    AppendIndent();
                }

                inString = true;
                stringDelim = c;
                buf[outIdx++] = c;
                anyContentInLine = true;
                
                continue;
            }

            // — toggle out of string
            if (inString && c == stringDelim)
            {
                buf[outIdx++] = c;
                anyContentInLine = true;
                inString = false;
                
                continue;
            }

            // — inside string, copy verbatim
            if (inString)
            {
                buf[outIdx++] = c;
                anyContentInLine = true;
                
                continue;
            }

            // — outside strings and comments: handle CSS syntax
            switch (c)
            {
                case '{':
                
	                if (outIdx == lineStartIdx)
                    {
                        AppendIndent();
                    }
                    
	                buf[outIdx++] = '{';
                    anyContentInLine = true;
                    
	                WriteNewline();
                    
	                depth++;
                    
	                break;

                case '}':
                    
	                WriteNewline();
                    
	                depth = Math.Max(0, depth - 1);
                    
	                AppendIndent();
                    
	                buf[outIdx++] = '}';
                    anyContentInLine = true;
                    
	                WriteNewline();
                    
	                break;

                case ';':
                    
	                if (outIdx == lineStartIdx)
                    {
                        AppendIndent();
                    }
                    
	                buf[outIdx++] = ';';
                    anyContentInLine = true;
                    
	                WriteNewline();
                    
	                break;

                case '\n':
                    
	                WriteNewline();
                    
	                break;

                default:
                    if (IsAsciiWhite(c) == false)
                    {
                        if (outIdx == lineStartIdx)
                        {
                            AppendIndent();
                        }
                    
                        buf[outIdx++] = c;
                        anyContentInLine = true;
                    }
                    else
                    {
                        // collapse runs of ASCII whitespace
                        if (outIdx > 0 && buf[outIdx - 1] != ' ' && buf[outIdx - 1] != '\n')
                            buf[outIdx++] = ' ';
                    }
                    break;
            }
        }

        // 5) Final newline if needed
        if (anyContentInLine)
            buf[outIdx++] = '\n';

        // 6) Flush back into source
        source.Clear();
        source.EnsureCapacity(outIdx);
        source.Append(buf, 0, outIdx);

        // 7) Return pooled buffer
        ArrayPool<char>.Shared.Return(buf);

        return source;

        // Fast ASCII whitespace check
        static bool IsAsciiWhite(char c) => c is ' ' or '\t' or '\r';

        // Indent via block-copy
        void AppendIndent()
        {
            var total = depth * indentSize;

            if (total > MaxSpaces)
                total = MaxSpaces;
            
            Array.Copy(SpacesBuffer, 0, buf, outIdx, total);
            
            outIdx += total;
            // still atLineStart until we emit a non-space
        }

        void WriteNewline()
        {
            if (anyContentInLine)
            {
                buf[outIdx++] = '\n';
                lineStartIdx = outIdx;
            }
            else
            {
                // drop whitespace-only line
                outIdx = lineStartIdx;
            }
            
            anyContentInLine = false;
        }
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
}
