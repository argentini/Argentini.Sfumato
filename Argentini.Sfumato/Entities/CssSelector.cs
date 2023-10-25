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

    public SfumatoAppState? AppState { get; set; }

    #region Selector
    
    private string _selector = string.Empty;
    public string Selector
    {
        get => _selector;

        set
        {
            var oldValue = _selector;
            
            _selector = value;

            if (oldValue != _selector)
                ProcessValue();
        }
    }    
    public string FixedSelector { get; private set; } = string.Empty;
    
    private string _escapedSelector = string.Empty;
    public string EscapedSelector
    {
	    get
	    {
		    if (_escapedSelector == string.Empty)
			    _escapedSelector = EscapeCssClassName();

		    return _escapedSelector;
	    }

	    set => _escapedSelector = value;
    }    

    #endregion
    
    #region Variants
    
    public List<string> MediaQueryVariants { get; } = new();
    public List<string> PseudoClassVariants { get; } = new();
    public List<string> AllVariants { get; } = new();
    
    #endregion

    #region Segments
    
    public string PrefixSegment { get; private set; } = string.Empty;
    public string CoreSegment { get; private set; } = string.Empty;
    public string ModifierValue { get; private set; } = string.Empty;
    public string ModifierValueType { get; private set; } = string.Empty;
    public string ArbitraryValue { get; private set; } = string.Empty;
    public string ArbitraryValueType { get; private set; } = string.Empty;
    
	#endregion    

	#region Calculated Properties
	
    public int Depth => MediaQueryVariants.Count + PseudoClassVariants.Count;
    public bool IsImportant { get; private set; }
    public bool IsArbitraryCss { get; private set; }
    public bool IsInvalid { get; private set; } = true;

    #endregion
    
    #region Legacy Properties
    
    public string RootClassSegment { get; private set; } = string.Empty;
    
    #endregion

    #endregion
    
    public CssSelector(SfumatoAppState appState)
    {
	    AppState = appState;
    }

    public CssSelector(SfumatoAppState appState, string selector)
    {
	    AppState = appState;
        Selector = selector;
    }

    /// <summary>
    /// Establish all property values from parsing the Value property.
    /// </summary>
    public void ProcessValue()
    {
        FixedSelector = string.Empty;
        EscapedSelector = string.Empty;
        RootClassSegment = string.Empty;
        ArbitraryValue = string.Empty;
        ArbitraryValueType = string.Empty;
        ModifierValue = string.Empty;
        ModifierValueType = string.Empty;

        MediaQueryVariants.Clear();
        PseudoClassVariants.Clear();
        AllVariants.Clear();

        IsImportant = false;
        IsArbitraryCss = false;
        IsInvalid = true;

        if (string.IsNullOrEmpty(Selector))
            return;

        var indexOfColon = Selector.IndexOf(':');
        var indexOfBracket = Selector.IndexOf('[');
        var indexOfBracketClose = Selector.IndexOf(']');
        var indexOfSlash = Selector.IndexOf('/');
        
        if (indexOfColon == 0 || indexOfBracket > indexOfBracketClose)
            return;

        if (indexOfBracketClose == indexOfBracket + 1)
            return;

        FixedSelector = Selector;
        RootClassSegment = Selector;
        
        var prefixesIndex = -1;

        if (indexOfColon > 0 && (indexOfBracket == -1 || (indexOfBracket > -1 && indexOfColon < indexOfBracket)))
        {
            for (var x = 0; x < Selector.Length; x++)
            {
                if (Selector[x] == '[' || Selector[x] == '!')
                    break;

                if (Selector[x] == ':')
                    prefixesIndex = x;
            }

            if (prefixesIndex > 0)
            {
                var prefixSegment = Selector[..(prefixesIndex + 1)];
                var segments = prefixSegment.Split(':', StringSplitOptions.RemoveEmptyEntries);

                RootClassSegment = Selector[(prefixesIndex + 1)..];

                if (segments.Length > 0)
                {
                    var lastType = string.Empty;

                    foreach (var breakpoint in SfumatoScss.MediaQueryPrefixes.Where(p => segments.Contains(p.Prefix)).OrderBy(k => k.PrefixOrder))
                    {
                        if (breakpoint.PrefixType == lastType)
                            continue;

                        MediaQueryVariants.Add(breakpoint.Prefix);

                        lastType = breakpoint.PrefixType;
                    }

                    foreach (var segment in segments)
                    {
                        if (SfumatoScss.PseudoclassPrefixes.ContainsKey(segment) == false)
                            continue;

                        PseudoClassVariants.Add(segment);
                    }
                    
                    FixedSelector = string.Empty;

                    if (MediaQueryVariants.Count > 0)
                        FixedSelector += $"{string.Join(':', MediaQueryVariants)}:";

                    if (PseudoClassVariants.Count > 0)
                        FixedSelector += $"{string.Join(':', PseudoClassVariants)}:";

                    FixedSelector += RootClassSegment;
                }
                
                AllVariants.AddRange(MediaQueryVariants);
                AllVariants.AddRange(PseudoClassVariants);
            }
        }

        if (indexOfSlash > 0 && indexOfSlash < Selector.Length - 1)
        {
	        ModifierValue = Selector[(indexOfSlash + 1)..];
	        ModifierValueType = SetCustomValueType(ModifierValue);
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
                SetArbitraryValue(RootClassSegment);
                RootClassSegment = string.Empty;
            }

            else if (indexOfBracket > 0)
            {
	            SetArbitraryValue(RootClassSegment[indexOfBracket..]);
                RootClassSegment = RootClassSegment[..indexOfBracket];
            }
            
            ArbitraryValueType = SetCustomValueType(ArbitraryValue);
        }
        
        if (RootClassSegment.Length > 0 || ArbitraryValue.Length > 0)
        {
            IsInvalid = false;
            return;
        }

        FixedSelector = string.Empty;
    }

    /// <summary>
    /// Set the arbitrary value from parsing the arbitrary value segment.
    /// </summary>
    /// <returns></returns>
    private void SetArbitraryValue(string segment)
    {
	    if (string.IsNullOrEmpty(segment))
	    {
		    ArbitraryValue = string.Empty;
		    return;
	    }
	    
	    ArbitraryValue = segment.TrimStart('[').TrimEnd(']').Replace('_', ' ').Replace("\\ ", "\\_");
            
	    foreach (var arbitraryType in SfumatoScss.ArbitraryValueTypes)
		    if (ArbitraryValue?.StartsWith($"{arbitraryType}:") ?? false)
			    ArbitraryValue = ArbitraryValue.TrimStart($"{arbitraryType}:") ?? string.Empty;
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
        if (string.IsNullOrEmpty(Selector))
            return Selector;

        var value = new StringBuilder();

        for (var i = 0; i < Selector.Length; i++)
        {
            var c = Selector[i];
            
            if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_'))
                value.Append('\\');
            
            value.Append(c);
        }

        return value.ToString();
    }
}
