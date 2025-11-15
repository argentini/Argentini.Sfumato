using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Library;
using Sfumato.Entities.Trie;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable RedundantBoolCompare

namespace Sfumato.Helpers;

/// <summary>
/// Various tools for working with strings. 
/// </summary>
public static partial class Strings
{
	#region Constants

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
	/// Trims any of the specified characters from the start and end of the span.
	/// </summary>
	public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, params char[]? trimChars)
	{
		if (span.IsEmpty || trimChars is null || trimChars.Length == 0)
			return span;

		var start = 0;
		var end = span.Length - 1;

		// advance start past any trimChar
		while (start <= end && IsTrimChar(span[start], trimChars))
			start++;

		// retreat end before any trimChar
		while (end >= start && IsTrimChar(span[end], trimChars))
			end--;

		// if nothing left, return empty; else slice
		return start == 0 && end == span.Length - 1
			? span
			: span.Slice(start, end - start + 1);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsTrimChar(char c, char[] trimChars)
	{
		// a tiny linear scan is usually fastest for small trim arrays
		foreach (var t in trimChars)
			if (c == t)
				return true;

		return false;
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

	/// <param name="source">The string to search</param>
	extension(string? source)
	{
		/// <summary>
		/// Remove a string from the beginning of a string
		/// </summary>
		/// <param name="substring">The substring to remove</param>
		/// <param name="stringComparison"></param>
		/// <returns>Trimmed source</returns>
		public string? TrimStart(string? substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
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
		/// <param name="substring">The substring to remove</param>
		/// <param name="stringComparison"></param>
		/// <returns>Trimmed source</returns>
		public string? TrimEnd(string? substring = " ",
			StringComparison stringComparison = StringComparison.Ordinal)
		{
			if (source == null || source.IsEmpty() || substring is null or "")
				return null;

			return source.EndsWith(substring, stringComparison)
				? source[..^substring.Length]
				: source;
		}
	}

	#endregion

	#region Comparison

	/// <summary>
	/// Determines if a string is empty or null.
	/// </summary>
	/// <param name="value">String to evaluate</param>
	public static bool IsEmpty(this string? value)
	{
		return string.IsNullOrEmpty(value);
	}

	#endregion

	#region Parsing

	/// <summary>
	/// Identify substrings enclosed in quotes (single, double, or backtick) within a string,
	/// Fills the bag with matches.
	/// </summary>
	/// <param name="source"></param>
	/// <param name="bag"></param>
	/// <param name="scannerClassNamePrefixes"></param>
	/// <param name="sb"></param>
	public static void ScanForUtilities(this string? source, Dictionary<string,string?>? bag, PrefixTrie<object?> scannerClassNamePrefixes, StringBuilder? sb = null)
	{
	    if (bag is null || string.IsNullOrEmpty(source))
	        return;

	    if (sb is null)
	        sb = new StringBuilder(source);
	    else
	        sb.ReplaceContent(source);

	    sb.Replace("\\\"", "\"");
	    sb.Replace("@\"", "\"");
	    sb.Replace("$\"", "\"");
	    sb.Replace("\",\"", "\", \"");
	    sb.Replace("\",@\"", "\", @\"");
	    sb.Replace("\",$\"", "\", $\"");
	    sb.Replace("\"\"", "\"");
	    sb.Replace("','", "', '");
	    sb.Replace("`,`", "`, `");
	    sb.Replace("`,$`", "`, $`");

	    var splits = sb.ToString().SplitByNonWhitespace();

	    foreach (var segment in splits)
	        ProcessSubstrings(segment, bag, scannerClassNamePrefixes);
	}

	private static readonly char[] Delimiters = ['\"', '\'', '`'];
	private const char DoubleQuote = '\"';

	public static void ProcessSubstrings(this string source, Dictionary<string,string?> bag, PrefixTrie<object?> scannerClassNamePrefixes)
	{
	    // Early exit for empty strings
	    if (string.IsNullOrEmpty(source))
	        return;

	    // Use Span to avoid allocations during trimming
	    var span = source.AsSpan();
	    
	    // Trim delimiters from both ends
	    var start = 0;
	    var end = span.Length - 1;
	    
	    // Trim from the start
	    while (start <= end && IsDelimiter(span[start]))
	        start++;
	    
	    // Trim from the end  
	    while (end >= start && IsDelimiter(span[end]))
	        end--;
	    
	    if (start > end)
	        return;
	    
	    // Only create substring if we actually trimmed
	    var trimmedSource = (start == 0 && end == source.Length - 1) 
	        ? source 
	        : source.Substring(start, end - start + 1);

	    // Use single IndexOf() call for quote checking
	    var quoteIndex = trimmedSource.IndexOf(DoubleQuote);
	    var addSource = quoteIndex == -1 || quoteIndex != trimmedSource.LastIndexOf(DoubleQuote);

	    if (addSource && trimmedSource.IsLikelyUtilityClass(scannerClassNamePrefixes, out var prefix))
	        bag.TryAdd(trimmedSource, prefix);

	    // Check each delimiter once
	    for (var d = 0; d < Delimiters.Length; d++)
	    {
	        var index = trimmedSource.IndexOf(Delimiters[d]);

	        if (index <= 0 || index >= trimmedSource.Length - 1)
	            continue;

	        var subsegments = trimmedSource.Split(Delimiters[d], StringSplitOptions.RemoveEmptyEntries);
	        
	        // Skip if split didn't split anything
	        if (subsegments.Length == 1)
	            continue;
	            
	        foreach (var subsegment in subsegments)
	            ProcessSubstrings(subsegment, bag, scannerClassNamePrefixes);
	        
	        // Exit after first delimiter found and processed
	        break;
	    }
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsDelimiter(char c)
	{
	    return c is '\"' or '\'' or '`';
	}

	extension(string source)
	{
		/// <summary>
		/// Fast check for Tailwind-style “utility” class names.
		/// Runs in a single pass over the string and does
		/// no heap allocations.
		/// </summary>
		public bool IsLikelyUtilityClass(PrefixTrie<object?> scannerClassNamePrefixes, out string? prefix)
		{
			prefix = null;
		
			if (source.Length < 3)
				return false;

			if (source.IndexOf("=\"", StringComparison.Ordinal) > 0)
				return false;

			var lastSegment = source.IndexOf(':') > 0 ? source.LastByTopLevel(':') ?? source : source[^1] == '!' ? source[..^1] : source; 

			if (lastSegment[0] == '[')
				return true;
		
			var slashIndex = lastSegment.IndexOf('/');

			if (scannerClassNamePrefixes.TryGetLongestMatchingPrefix(slashIndex > -1 ? lastSegment[..slashIndex] : lastSegment, out prefix, out _) == false)
				return false;

			if (source[^1] == ':')
				return false;

			if (lastSegment[^1] is >= 'a' and <= 'z' || lastSegment[^1] is >= '0' and <= '9' || lastSegment[^1] == ']' || lastSegment[^1] == ')' || lastSegment[^1] == '%')
				return true;

			return false;
		}

		/// <summary>
		/// Enumerates all substrings of <paramref name="source"/> that consist of one or more non-whitespace characters.
		/// Equivalent to Regex.Matches(input, @"\S+").
		/// </summary>
		public IEnumerable<string> SplitByNonWhitespace()
		{
			if (string.IsNullOrEmpty(source))
				yield break;

			var length = source.Length;
			var pos = 0;

			while (pos < length)
			{
				// Skip any leading whitespace
				while (pos < length && char.IsWhiteSpace(source[pos]))
					pos++;

				if (pos >= length)
					yield break;

				// Mark the start of the non-whitespace run
				var start = pos;

				// Advance until the next whitespace (or end of string)
				while (pos < length && char.IsWhiteSpace(source[pos]) == false)
					pos++;

				// Return the chunk
				yield return source.Substring(start, pos - start);
			}
		}

		public string ConsolidateAtSymbols()
		{
			// count how many pairs we’ll collapse so we know the final length
			var collapseCount = 0;
        
			for (var i = 0; i < source.Length - 1; i++)
			{
				if (source[i] != '@' || source[i + 1] != '@')
					continue;
            
				collapseCount++;
				i++; // skip the second '@'
			}

			var newLength = source.Length - collapseCount;
        
			// allocate exactly once and fill in the collapsed data
			return string.Create(newLength, source, (dest, src) =>
			{
				var di = 0;
            
				for (var si = 0; si < src.Length; si++)
				{
					if (src[si] == '@' && si + 1 < src.Length && src[si + 1] == '@')
					{
						dest[di++] = '@';
						si++; // skip the second '@'
					}
					else
					{
						dest[di++] = src[si];
					}
				}
			});
		}
	}

	#endregion
	
	#region Transformations

	/// <summary>
	/// Convert Pascal casing to spaced string.
	/// (e.g. "MyXMLParser" => "My XML Parser")
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string PascalCaseToSpaced(this string input)
	{
		return PascalCaseToSpacedRegex().Replace(input, " $1");
	}

	/// <param name="text">Input string (it may be <c>null</c> or empty).</param>
	extension(string? text)
	{
		/// <summary>
		/// Removes SPACE and TAB characters that appear <em>immediately</em> before
		/// an LF (<c>"\n"</c>) or CRLF (<c>"\r\n"</c>) sequence.
		/// </summary>
		/// <param name="scratch">
		/// Optional <see cref="StringBuilder"/> taken from an <em>ObjectPool</em>.  
		/// If supplied, it is cleared up-front and used only when a rewrite
		/// becomes necessary.</param>
		public string TrimWhitespaceBeforeLineBreaks(StringBuilder? scratch = null)
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
		/// Removes all C-style block comments ( /* … */ ) from <paramref name="text"/>.
		/// If a closing <c>*/</c> is missing the rest of the text is treated as a comment.
		/// Nested block comments are NOT supported; the first closing token ends the comment.
		/// </summary>
		public string RemoveBlockComments(StringBuilder? workingSb = null)
		{
			if (string.IsNullOrEmpty(text))
				return text ?? string.Empty;

			workingSb ??= new StringBuilder(text.Length);
			workingSb.Clear();
		
			var span = text.AsSpan();
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
	}

	/// <param name="value"></param>
	extension(string value)
	{
		/// <summary>
		/// Escape a string for use in a CSS selector.
		/// </summary>
		/// <returns></returns>
		public string CssSelectorEscape()
		{
			if (string.IsNullOrEmpty(value))
				return value;

			var maxLength = value.Length * 2;
			var buffer = maxLength <= 1024 ? stackalloc char[maxLength] : new char[maxLength];
			var position = 0;

			buffer[position++] = '.';

			for (var i = 0; i < value.Length; i++)
			{
				var c = value[i];

				if (i == 0 && char.IsDigit(c))
				{
					buffer[position++] = '\\';
					buffer[position++] = '3';
					buffer[position++] = c;
					buffer[position++] = ' ';

					continue;
				}
			
				if ((i == 0 && char.IsDigit(c)) || (!char.IsLetterOrDigit(c) && c != '-' && c != '_'))
					buffer[position++] = '\\';

				buffer[position++] = c;
			}

			return value.Length == position ? value : new string(buffer[..position]);
		}

		/// <summary>
		/// Removes spaces and newlines from a string.
		/// </summary>
		/// <param name="workingSb"></param>
		/// <returns></returns>
		public string CompactCss(StringBuilder? workingSb = null)
		{
			workingSb ??= new StringBuilder();
			workingSb.Clear();
		
			// Remove line breaks, block comments
			var result = value
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

		private string RemoveCssRemovals(StringBuilder? workingSb = null)
		{
			workingSb ??= new StringBuilder(value.Length);
			workingSb.Clear();
		
			var i = 0;
			var n = value.Length;

			while (i < n)
			{
				var c = value[i];

				if (c is '\r' or '\n')
				{
					// skip all CR/LF
					while (i < n && (value[i] == '\r' || value[i] == '\n'))
						i++;

					// skip any following whitespace (but not newlines)
					while (i < n && char.IsWhiteSpace(value[i]) && value[i] != '\r' && value[i] != '\n')
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

		public string ConsolidateSpaces(StringBuilder? workingSb = null)
		{
			workingSb ??= new StringBuilder(value.Length);
			workingSb.Clear();

			var i = 0;
			var n = value.Length;
			var lastWasSpace = false;

			while (i < n)
			{
				var c = value[i];
			
				if (char.IsWhiteSpace(c))
				{
					// append one space if previous char wasn't a space
					if (lastWasSpace == false)
					{
						workingSb.Append(' ');
						lastWasSpace = true;
					}

					// skip all following whitespace
					while (i < n && char.IsWhiteSpace(value[i]))
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

		private string RemoveDelimiterSpaces(StringBuilder? workingSb = null)
		{
			workingSb ??= new StringBuilder(value.Length);
			workingSb.Clear();
		
			if (string.IsNullOrEmpty(value))
				return value;

			// delimiters we want to “hug”
			var delimiters = new HashSet<char> { ':', ',', ';', '{', '}' };

			var i = 0;
			var n = value.Length;

			while (i < n)
			{
				var c = value[i];

				// If we see any whitespace, peek to see if it's around a delimiter
				if (char.IsWhiteSpace(c))
				{
					var j = i;
				
					// skip all whitespace
					while (j < n && char.IsWhiteSpace(value[j]))
						j++;

					// if next real char is a delimiter, emit that and skip trailing whitespace
					if (j < n && delimiters.Contains(value[j]))
					{
						workingSb.Append(value[j]);
						j++;
					
						while (j < n && char.IsWhiteSpace(value[j]))
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
				
					while (j < n && char.IsWhiteSpace(value[j]))
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

		private string RemoveZeroUnits(StringBuilder? workingSb = null)
		{
			workingSb ??= new StringBuilder(value.Length);
			workingSb.Clear();

			if (string.IsNullOrEmpty(value)) return value;

			// Units sorted by descending length so we match "cqmax" before "cq" etc.
			string[] units =
			[
				"cqmin","cqmax","vmax","vmin","cqw","cqh","cqi","cqb","rem","cm","in","mm","pc","pt","px","ch","em","ex","vw","vh","Q","%"
			];

			var i = 0;
			var n = value.Length;

			while (i < n)
			{
				var c = value[i];

				// if whitespace or colon, and next char is '0', maybe strip a unit
				if ((char.IsWhiteSpace(c) || c == ':') && i + 1 < n && value[i + 1] == '0')
				{
					var stripped = false;

					// try each unit
					foreach (var u in units)
					{
						var len = u.Length;
					
						if (i + 2 + len <= n && string.Compare(value, i + 2, u, 0, len, StringComparison.Ordinal) == 0)
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
		/// <param name="n"></param>
		/// <returns></returns>
		public string Repeat(int n)
		{
			if (string.IsNullOrEmpty(value) || n < 1)
				return string.Empty;
		
			var textAsSpan = value.AsSpan();
			var span = new Span<char>(new char[textAsSpan.Length * n]);
	
			for (var i = 0; i < n; i++)
			{
				textAsSpan.CopyTo(span.Slice(i * textAsSpan.Length, textAsSpan.Length));
			}

			return span.ToString();
		}

		/// <summary>
		/// Normalize all “\r\n”, “\r”, and “\n” sequences into the specified linebreak,
		/// doing at most one allocation and two scans.
		/// </summary>
		public string NormalizeLinebreaks(string linebreak = "\n")
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;

			var len = value.Length;
			var countCrlf = 0;
			var countSingle = 0;

			// First pass: count how many CRLF vs. lone CR or LF we have
			for (var i = 0; i < len; i++)
			{
				var c = value[i];
	        
				if (c == '\r')
				{
					if (i + 1 < len && value[i + 1] == '\n')
					{
						countCrlf++;
						i++;
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
				return value;

			var breakLen = linebreak.Length;
	    
			// Compute new length:
			//   remove 2 chars per CRLF, 1 per lone CR or LF
			//   then add back breakLen chars per token
			var newLen = len
			             - (countCrlf * 2 + countSingle * 1)
			             + (countCrlf + countSingle) * breakLen;

			// Second pass: fill directly into the new string’s buffer
			return string.Create(newLen, (value, linebreak), (span, state) =>
			{
				var (src, br) = state;
				var dst = 0;

				for (var i = 0; i < src.Length; i++)
				{
					var c = src[i];
	            
					if (c == '\r')
					{
						if (i + 1 < src.Length && src[i + 1] == '\n')
						{
							// CRLF
							for (var j = 0; j < br.Length; j++)
								span[dst + j] = br[j];
							dst += br.Length;
							i++;
						}
						else
						{
							// lone CR
							for (var j = 0; j < br.Length; j++)
								span[dst + j] = br[j];
							dst += br.Length;
						}
					}
					else if (c == '\n')
					{
						// lone LF
						for (var j = 0; j < br.Length; j++)
							span[dst + j] = br[j];
						dst += br.Length;
					}
					else
					{
						span[dst++] = c;
					}
				}
			});
		}

		/// <summary>
		/// Normalize both ‘/’ and ‘\’ to the native directory separator in one pass.
		/// </summary>
		public string SetNativePathSeparators()
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;

			// If there are no separators, return the original string
			if (value.IndexOfAny(['/', '\\']) < 0)
				return value;

			var sep = Path.DirectorySeparatorChar;

			// Allocate exactly one new string of the right length and fill it:
			return string.Create(value.Length, (value, sep), (span, state) =>
			{
				var (src, separator) = state;
			
				for (var i = 0; i < src.Length; i++)
				{
					var c = src[i];
				
					span[i] = c is '/' or '\\' ? separator : c;
				}
			});
		}
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

	/// <param name="color"></param>
	extension(string color)
	{
		/// <summary>
		/// Convert a web color to a rgba() value.
		/// Handles rgb() and hex colors.
		/// Optionally set a new opacity value (0-100).
		/// </summary>
		/// <param name="opacityPct"></param>
		/// <returns>rgba() value, or rgba(0,0,0,-0) on error</returns>
		public string SetWebColorAlpha(int opacityPct)
		{
			return color.SetWebColorAlpha(opacityPct / 100d);
		}

		/// <summary>
		/// Convert a web color to a rgba() value.
		/// Handles rgb() and hex colors.
		/// Optionally set a new opacity value (0-100).
		/// </summary>
		/// <param name="opacityPct"></param>
		/// <returns>rgba() value, or rgba(0,0,0,-0) on error</returns>
		public string SetWebColorAlphaByPercentage(double opacityPct)
		{
			return color.SetWebColorAlpha(opacityPct / 100);
		}
	}

	/// <summary>
	/// Add an alpha value to a web color (hex, rgb, rgba, oklch, etc.).
	/// </summary>
	/// <param name="color"></param>
	/// <param name="opacity">0.0 to 1.0</param>
	/// <returns>rgba() value, or rgba(0,0,0,-0) on error</returns>
	public static string SetWebColorAlpha(this string color, double opacity = -1)
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

			return opacity >= 1.0d ? $"oklch({segments[0]} {segments[1]} {segments[2]})" : $"oklch({segments[0]} {segments[1]} {segments[2]} / {$"{opacity:0.#######}".TrimEnd('0')})";
		}
        
		#endregion

		color = color.Trim().Replace(" ", string.Empty);

		var hexIndex = color.IndexOf('#');
		var rgbIndex = color.IndexOf("rgb", StringComparison.Ordinal);

		#region Named Color Conversion
		
		// ReSharper disable once ConvertIfStatementToSwitchStatement
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

			return opacity >= 1.0d ?  $"rgb({red},{green},{blue})" : $"rgba({red},{green},{blue},{$"{opacity:0.#######}".TrimEnd('0')})";
		}		
		
		#endregion		
		
		#region RGB/A Color

		// ReSharper disable once InvertIf
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
			
			return opacity >= 1.0d ?  $"rgb({red},{green},{blue})" : $"rgba({red},{green},{blue},{$"{opacity:0.#######}".TrimEnd('0')})";
		}

		#endregion

		return color;
	}
	
    extension(string s)
    {
	    /// <summary>
	    /// Scans the input once and returns every percentage literal it finds
	    /// that is immediately preceded by whitespace or '/':
	    /// e.g. " 12.5%", "/.75%", " 8%", " /12.%" etc.
	    /// </summary>
	    public IEnumerable<string> ExtractPercentages()
	    {
		    if (string.IsNullOrEmpty(s))
			    yield break;

		    var len = s.Length;

		    for (var i = 0; i < len; i++)
		    {
			    var c = s[i];

			    // possible start of a percentage: digit or '.' with digit after
			    var looksLikeNumberStart =
				    c is >= '0' and <= '9' || (c == '.' && i + 1 < len && s[i + 1] >= '0' && s[i + 1] <= '9');

			    if (looksLikeNumberStart == false)
				    continue;

			    // enforce preceding char is whitespace or '/'
			    if (i > 0 && !(char.IsWhiteSpace(s[i - 1]) || s[i - 1] == '/'))
				    continue;

			    var start = i;
			    var hasDigits = false;
			    var seenDecimal = false;
			    var j = i;

			    // if we start on '.', consume it as decimal point
			    if (s[j] == '.')
			    {
				    seenDecimal = true;
				    j++;
			    }

			    // consume integer digits
			    while (j < len && s[j] >= '0' && s[j] <= '9')
			    {
				    hasDigits = true;
				    j++;
			    }

			    // optional fractional part if we haven't yet seen a decimal
			    if (seenDecimal == false && j < len && s[j] == '.')
			    {
				    // ReSharper disable once RedundantAssignment
				    seenDecimal = true;
				    j++;

				    while (j < len && s[j] >= '0' && s[j] <= '9')
				    {
					    hasDigits = true;
					    j++;
				    }
			    }

			    // final check for '%' suffix
			    if (hasDigits && j < len && s[j] == '%')
			    {
				    yield return s.Substring(start, j - start + 1);
				    i = j;  // skip past it
			    }

			    // else fall through and continue scanning
		    }
	    }

	    /// <summary>
	    /// Scans the input once and returns every CSS web‑color literal it finds:
	    /// #RGB, #RGBA, #RRGGBB, #RRGGBBAA, rgb(...), rgba(...), oklch(...).
	    /// </summary>
	    public IEnumerable<string> ExtractWebColors()
	    {
		    if (string.IsNullOrEmpty(s))
			    yield break;

		    var len = s.Length;

		    for (var i = 0; i < len; i++)
		    {
			    var c = s[i];

			    // ---- 1) Hex codes: # then 3–8 hex digits ----
			    if (c == '#')
			    {
				    var start = i;
				    var j = i + 1;
                
				    // scan up to 8 hex digits
				    while (j < len && j - start - 1 < 8 && IsHexDigit(s[j]))
					    j++;

				    var count = j - start - 1;
                
				    if (count is 3 or 4 or 6 or 8)
					    yield return s.Substring(start, 1 + count);

				    // skip whatever we consumed (valid or not), move i to last checked
				    i = j - 1;

				    continue;
			    }

			    // ---- 2) rgba(...) first (so we don't mistake it for rgb(...)) ----
			    if (i + 4 < len
			        && (s[i] == 'r' || s[i] == 'R')
			        && (s[i+1] == 'g' || s[i+1] == 'G')
			        && (s[i+2] == 'b' || s[i+2] == 'B')
			        && (s[i+3] == 'a' || s[i+3] == 'A')
			        && s[i+4] == '(')
			    {
				    yield return ExtractFunction(s, ref i, 5);
				    continue;
			    }

			    // ---- 3) rgb(...) ----
			    if (i + 3 < len
			        && (s[i] == 'r' || s[i] == 'R')
			        && (s[i+1] == 'g' || s[i+1] == 'G')
			        && (s[i+2] == 'b' || s[i+2] == 'B')
			        && s[i+3] == '(')
			    {
				    yield return ExtractFunction(s, ref i, 4);
				    continue;
			    }

			    // ---- 4) oklch(...) ----
			    if (i + 5 < len
			        && (s[i] == 'o' || s[i] == 'O')
			        && (s[i+1] == 'k' || s[i+1] == 'K')
			        && (s[i+2] == 'l' || s[i+2] == 'L')
			        && (s[i+3] == 'c' || s[i+3] == 'C')
			        && (s[i+4] == 'h' || s[i+4] == 'H')
			        && s[i+5] == '(')
			    {
				    yield return ExtractFunction(s, ref i, 6);
			    }
		    }
	    }
    }

    // Helper: pull out a "(...)" block starting at 's[i]', where 'offset' is the length of 'name('.
    // Advances 'i' to the last character of the function and returns the full substring.
    private static string ExtractFunction(string s, ref int i, int offset)
    {
        var start = i;
        var len = s.Length;
        var depth = 1;
        
        i += offset; // position right after the opening '('

        // consume until matching ')'
        while (i < len && depth > 0)
        {
            if (s[i] == '(')
	            depth++;
            else if (s[i] == ')')
	            depth--;
            
            i++;
        }

        // substring from start to current `i`, where `i` is one past the closing ')'
        return s.Substring(start, i - start);
    }

    // Simple inline hex‑digit check
    private static bool IsHexDigit(char c)
    {
        return c is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f';
    }
        
	#endregion
	
	#region Time

	/// <summary>
	/// Formats the elapsed time in the most appropriate unit:
	/// seconds (s), milliseconds (ms), or microseconds (μs).
	/// </summary>
	public static string FormatTimer(this TimeSpan timeSpan)
	{
		if (timeSpan.TotalNanoseconds < 1000)
			return $"{timeSpan.TotalNanoseconds:0} ns";
		
		if (timeSpan.TotalMicroseconds < 1000)
			return $"{timeSpan.TotalMicroseconds:0} μs";
		
		if (timeSpan.TotalMilliseconds < 1000)
			return $"{timeSpan.TotalMilliseconds:0} ms";

		if (timeSpan.TotalSeconds < 60)
			return $"{timeSpan.TotalSeconds:N3} s";

		return $"{timeSpan.Hours:0}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000} s";
	}

	/// <summary>
	/// Formats the elapsed time in the most appropriate unit:
	/// seconds (s), milliseconds (ms), microseconds (μs), or nanoseconds (ns).
	/// </summary>
	public static string FormatTimerFromNanoseconds(this double nanoseconds)
	{
		if (nanoseconds < 1_000d)
			return $"{nanoseconds:0} ns";
		
		if (nanoseconds < 1_000_000d)
			return $"{nanoseconds / 1_000:0} μs";
		
		return nanoseconds < 1_000_000_000d ? $"{nanoseconds / 1_000_000:0} ms" : $"{nanoseconds / 1_000_000_000:N3} s";
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
	
    public static void WriteToOutput(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        if (string.IsNullOrWhiteSpace(text))
        {
	        if (SfumatoService.Configuration?.Logger is not null)
#pragma warning disable CA1873
		        SfumatoService.Configuration.Logger.LogInformation("");
#pragma warning restore CA1873
	        else
		        Console.WriteLine(string.Empty);

	        return;
        }
        
        var result = new List<string>();
        var lines = text.Trim().NormalizeLinebreaks().Replace("_\n", " ").Split('\n');

        foreach (var line in lines)
            result.AddRange(WrapTextAtMaxWidth(line, Library.MaxConsoleWidth));

        foreach (var line in result)
        {
	        if (SfumatoService.Configuration?.Logger is not null)
#pragma warning disable CA1873
		        SfumatoService.Configuration.Logger.LogInformation("{line}", line.NormalizeLinebreaks(Environment.NewLine));
#pragma warning restore CA1873
	        else
		        Console.WriteLine(line.NormalizeLinebreaks(Environment.NewLine));
        }
    }

    [GeneratedRegex(@"^\s+")]
    private static partial Regex IndentationSpacesRegex();

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex PascalCaseToSpacedRegex();

    #endregion
}
