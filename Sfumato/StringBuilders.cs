using System;
using System.Text;

namespace Sfumato;

/// <summary>
/// Various tools to make using StringBuilders more like using strings. 
/// </summary>
public static class StringBuilders
{
	/// <summary>
	/// Remove a string from the beginning of a StringBuilder
	/// </summary>
	/// <param name="source">The StringBuilder to search</param>
	/// <param name="substring">The substring to remove</param>
	public static void TrimStart(this StringBuilder source, string substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
	{
		while (source.Length >= substring.Length && source.Substring(0, substring.Length).Equals(substring, stringComparison))
		{
			source.Remove(0, substring.Length);
		}
	}
	
    /// <summary>
    /// Remove a string from the end of a StringBuilder
    /// </summary>
    /// <param name="source">The StringBuilder to search</param>
    /// <param name="substring">The substring to remove</param>
    public static void TrimEnd(this StringBuilder source, string substring = " ", StringComparison stringComparison = StringComparison.Ordinal)
    {
	    while (source.Length >= substring.Length && source.Substring(source.Length - substring.Length, substring.Length).Equals(substring, stringComparison))
	    {
		    source.Remove(source.Length - substring.Length, substring.Length);
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
		{
			result += source[x];
		}

		return result;
	}
}
