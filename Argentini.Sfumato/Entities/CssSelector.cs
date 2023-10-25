namespace Argentini.Sfumato.Entities;

public sealed class CssSelector
{
	/*
	 * Structure of a CSS selector:
	 * ============================
	 * 
	 * Starts with optional variants:
	 * {media-queries}: {pseudo-classes}:
	 *
	 * Followed by optional important flag
	 * {!}
	 *
	 * Arbitrary CSS:
	 * [{arbitrary-css}]
	 *
	 * OR
	 *
	 * Utility class:
	 * Prefix, core, modifier, and arbitrary value segments:
	 * {prefix}-{core} { /{modifier-value} or /[{arbitrary-modifier-value}] or -[{arbitrary-value}] }
	 *
	 * Segment Examples:
	 * -----------------
	 * text-base		  {prefix:text}-{core:base}
	 * text-base/5        {prefix:text}-{core:base}/{modifier-value:5}
	 * text-base/[3rem]   {prefix:text}-{core:base}/[{arbitrary-modifier-value:3rem}]
	 * bg-[#aabbcc]       {prefix:bg}-[{arbitrary-value:#aabbcc}]
	 * bg-rose/50         {prefix:bg}-{core:rose}/{modifier-value:5}
	 * w-1/2	          {prefix:w}-{core:1/2} 
	 * sr-only	          {prefix:sr-only}
	 *
	 * Selector examples:
	 * ------------------
	 * tabp:focus:bg-rose/50
	 * dark:tabp:hover:!text-base/5
	 * 
	 * Parsing order:
	 * --------------
	 * 1. Process variants
	 * 2. Identify optional important flag
	 * 3. Identify arbitrary CSS
	 * 4. Identify prefix segment from UtilityClassIndex; if not, SKIP
	 * 5. Identify core segment from UtilityClassIndex; if not, SKIP
	 * 6. Find "/[" and "]" and arbitrary modifier value
	 * 7. Find "[" and "]" and arbitrary value
	 * 
	 */
	
    #region Properties

    private string _value = string.Empty;

    public string Value
    {
        get => _value;

        set
        {
            var oldValue = _value;
            
            _value = value;

            if (oldValue != _value)
                ProcessValue();
        }
    }    
    public string FixedValue { get; private set; } = string.Empty;
    public List<string> MediaQueries { get; } = new();
    public List<string> PseudoClasses { get; } = new();
    public List<string> AllPrefixes { get; } = new();
    public string RootClassSegment { get; private set; } = string.Empty;
    public string CustomValueSegment { get; private set; } = string.Empty;
    public string CustomValue { get; private set; } = string.Empty;
    public string CustomValueType { get; private set; } = string.Empty;
    public string SlashValue { get; private set; } = string.Empty;
    public string SlashValueType { get; private set; } = string.Empty;
    public string EscapedSelector => IsInvalid ? string.Empty : EscapeCssClassName();

    public int Depth => MediaQueries.Count + PseudoClasses.Count;
    public bool IsImportant { get; private set; }
    public bool IsArbitraryCss { get; private set; }
    public bool IsInvalid { get; private set; } = true;

    #endregion

    public CssSelector()
    {}

    public CssSelector(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Establish all property values from parsing the Value property.
    /// </summary>
    public void ProcessValue()
    {
        FixedValue = string.Empty;
        RootClassSegment = string.Empty;
        CustomValueSegment = string.Empty;
        CustomValueType = string.Empty;
        SlashValue = string.Empty;
        SlashValueType = string.Empty;

        MediaQueries.Clear();
        PseudoClasses.Clear();
        AllPrefixes.Clear();

        IsImportant = false;
        IsArbitraryCss = false;
        IsInvalid = true;

        if (string.IsNullOrEmpty(Value))
            return;

        var indexOfColon = Value.IndexOf(':');
        var indexOfBracket = Value.IndexOf('[');
        var indexOfBracketClose = Value.IndexOf(']');
        var indexOfSlash = Value.IndexOf('/');
        
        if (indexOfColon == 0 || indexOfBracket > indexOfBracketClose)
            return;

        if (indexOfBracketClose == indexOfBracket + 1)
            return;

        FixedValue = Value;
        RootClassSegment = Value;
        
        var prefixesIndex = -1;

        if (indexOfColon > 0 && (indexOfBracket == -1 || (indexOfBracket > -1 && indexOfColon < indexOfBracket)))
        {
            for (var x = 0; x < Value.Length; x++)
            {
                if (Value[x] == '[' || Value[x] == '!')
                    break;

                if (Value[x] == ':')
                    prefixesIndex = x;
            }

            if (prefixesIndex > 0)
            {
                var prefixSegment = Value[..(prefixesIndex + 1)];
                var segments = prefixSegment.Split(':', StringSplitOptions.RemoveEmptyEntries);

                RootClassSegment = Value[(prefixesIndex + 1)..];

                if (segments.Length > 0)
                {
                    var lastType = string.Empty;

                    foreach (var breakpoint in SfumatoScss.MediaQueryPrefixes.Where(p => segments.Contains(p.Prefix)).OrderBy(k => k.PrefixOrder))
                    {
                        if (breakpoint.PrefixType == lastType)
                            continue;

                        MediaQueries.Add(breakpoint.Prefix);

                        lastType = breakpoint.PrefixType;
                    }

                    foreach (var segment in segments)
                    {
                        if (SfumatoScss.PseudoclassPrefixes.ContainsKey(segment) == false)
                            continue;

                        PseudoClasses.Add(segment);
                    }
                    
                    FixedValue = string.Empty;

                    if (MediaQueries.Count > 0)
                        FixedValue += $"{string.Join(':', MediaQueries)}:";

                    if (PseudoClasses.Count > 0)
                        FixedValue += $"{string.Join(':', PseudoClasses)}:";

                    FixedValue += RootClassSegment;
                }
                
                AllPrefixes.AddRange(MediaQueries);
                AllPrefixes.AddRange(PseudoClasses);
            }
        }

        if (indexOfSlash > 0 && indexOfSlash < Value.Length - 1)
        {
	        SlashValue = Value[(indexOfSlash + 1)..];
	        SlashValueType = SetCustomValueType(SlashValue);
        }
        
        if (string.IsNullOrEmpty(RootClassSegment) == false)
        {
            IsImportant = RootClassSegment.StartsWith('!');

            if (IsImportant)
	            RootClassSegment = RootClassSegment.TrimStart('!');

            indexOfBracket = RootClassSegment.IndexOf('[');

            if (indexOfBracket == 0)
            {
                IsArbitraryCss = true;
                CustomValueSegment = RootClassSegment;
                RootClassSegment = string.Empty;
            }

            else if (indexOfBracket > 0)
            {
                CustomValueSegment = RootClassSegment[indexOfBracket..];
                RootClassSegment = RootClassSegment[..indexOfBracket];
            }
            
            SetCustomValue();

            CustomValueType = SetCustomValueType(CustomValue);
        }
        
        if (RootClassSegment.Length > 0 || CustomValueSegment.Length > 0)
        {
            IsInvalid = false;
            return;
        }

        FixedValue = string.Empty;
    }

    /// <summary>
    /// Set the custom value from parsing the custom value segment.
    /// </summary>
    /// <returns></returns>
    private void SetCustomValue()
    {
	    if (string.IsNullOrEmpty(CustomValueSegment))
	    {
		    CustomValue = string.Empty;
		    return;
	    }
	    
	    CustomValue = CustomValueSegment.TrimStart('[').TrimEnd(']').Replace('_', ' ').Replace("\\ ", "\\_");
            
	    foreach (var arbitraryType in SfumatoScss.ArbitraryValueTypes)
		    if (CustomValue?.StartsWith($"{arbitraryType}:") ?? false)
			    CustomValue = CustomValue.TrimStart($"{arbitraryType}:") ?? string.Empty;
    }
    
    /// <summary>
    /// Get the value type of the custom class value (e.g. "length:...", "color:...", etc.)
    /// </summary>
    /// <returns></returns>
    public string SetCustomValueType(string value)
    {
	    if (string.IsNullOrEmpty(value))
		    return string.Empty;
	    
	    if (value.Contains(':'))
	    {
		    var segments = value.Split(':', StringSplitOptions.RemoveEmptyEntries);

		    if (segments.Length > 1)
		    {
			    if (SfumatoScss.ArbitraryValueTypes.Contains(segments[0]))
				    return segments[0];
		    }
	    }

	    // Determine value type based on value (e.g. text-[red])

	    if (value.EndsWith('%') && double.TryParse(value.TrimEnd('%'), out _))
		    return "percentage";

	    if (value.Contains('.') == false && int.TryParse(value, out _))
		    return "integer";

	    if (double.TryParse(value, out _))
		    return "number";

	    #region length
	    
	    var unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "length";

	    #endregion

	    if (value.EndsWith("fr") && double.TryParse(value.TrimEnd("fr"), out _))
		    return "flex";

	    if (value.IsValidWebHexColor() || value.StartsWith("rgb(") || value.StartsWith("rgba(") || SfumatoScss.CssNamedColors.Contains(value))
		    return "color";

	    if (value.StartsWith('\'') && value.EndsWith('\''))
		    return "string";

	    if (value.StartsWith("url(", StringComparison.Ordinal) && value.EndsWith(')') || (value.StartsWith('/') && Uri.TryCreate(value, UriKind.Relative, out _)) || (Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
		    return "url";
	    
	    #region angle

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssAngleUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "angle";

	    #endregion

	    #region time

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssTimeUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "time";

	    #endregion

	    #region frequency

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssFrequencyUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "frequency";

	    #endregion

	    #region resolution

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssResolutionUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "resolution";

	    #endregion
	    
	    #region ratio
	    
	    if (value.Length < 3 || value.IndexOf('/') < 1 || value.IndexOf('/') == value.Length - 1)
		    return string.Empty;
	    
	    var customValues = value.Replace('_', ' ').Split('/', StringSplitOptions.RemoveEmptyEntries);

	    if (customValues.Length != 2)
		    return string.Empty;

	    if (double.TryParse(customValues[0], out _) && double.TryParse(customValues[1], out _))
		    return "ratio";

	    #endregion

	    return string.Empty;
    }
    
    /// <summary>
    /// Escape the CSS class name to be used in a CSS selector.
    /// </summary>
    /// <returns></returns>
    private string EscapeCssClassName()
    {
        if (string.IsNullOrEmpty(Value))
            return Value;

        var value = new StringBuilder();

        for (var i = 0; i < Value.Length; i++)
        {
            var c = Value[i];
            
            if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_'))
                value.Append('\\');
            
            value.Append(c);
        }

        return value.ToString();
    }
}
