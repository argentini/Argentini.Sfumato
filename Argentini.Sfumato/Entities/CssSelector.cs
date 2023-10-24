namespace Argentini.Sfumato.Entities;

public sealed class CssSelector
{
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
    public List<string> AllPrefixes => MediaQueries.Concat(PseudoClasses).ToList();
    public string RootSegment { get; private set; } = string.Empty;
    public string CustomValueSegment { get; private set; } = string.Empty;
    public string CustomValue { get; private set; } = string.Empty;
    public string CustomValueType { get; private set; } = string.Empty;
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
        RootSegment = string.Empty;
        CustomValueSegment = string.Empty;
        CustomValueType = string.Empty;

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
        
        if (indexOfColon == 0 || indexOfBracket > indexOfBracketClose)
            return;

        if (indexOfBracketClose == indexOfBracket + 1)
            return;

        FixedValue = Value;
        RootSegment = Value;
        
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

                RootSegment = Value[(prefixesIndex + 1)..];

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

                    FixedValue += RootSegment;
                }
            }
        }

        if (string.IsNullOrEmpty(RootSegment) == false)
        {
            IsImportant = RootSegment.StartsWith('!');

            if (IsImportant)
	            RootSegment = RootSegment.TrimStart('!');

            indexOfBracket = RootSegment.IndexOf('[');

            if (indexOfBracket == 0)
            {
                IsArbitraryCss = true;
                CustomValueSegment = RootSegment;
                RootSegment = string.Empty;
            }

            else if (indexOfBracket > 0)
            {
                CustomValueSegment = RootSegment[indexOfBracket..];
                RootSegment = RootSegment[..indexOfBracket];
            }
        }

        SetCustomValue();
        SetCustomValueType();

        if (RootSegment.Length > 0 || CustomValueSegment.Length > 0)
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
    public void SetCustomValueType()
    {
	    if (string.IsNullOrEmpty(CustomValue))
		    return;
	    
	    if (CustomValue.Contains(':'))
	    {
		    var segments = CustomValue.Split(':', StringSplitOptions.RemoveEmptyEntries);

		    if (segments.Length > 1)
		    {
			    if (SfumatoScss.ArbitraryValueTypes.Contains(segments[0]))
			    {
				    CustomValueType = segments[0];
				    return;
			    }
		    }
	    }

	    // Determine CustomValue type based on CustomValue (e.g. text-[red])

	    if (CustomValue.EndsWith('%') && double.TryParse(CustomValue.TrimEnd('%'), out _))
	    {
		    CustomValueType = "percentage";
		    return;
	    }

	    if (CustomValue.Contains('.') == false && int.TryParse(CustomValue, out _))
	    {
		    CustomValueType = "integer";
		    return;
	    }

	    if (double.TryParse(CustomValue, out _))
	    {
		    CustomValueType = "number";
		    return;
	    }

	    #region length
	    
	    var unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssUnits)
	    {
		    unitless = CustomValue.TrimEnd(unit) ?? string.Empty;
		    
		    if (CustomValue.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    CustomValueType = "length";
		    return;
	    }

	    #endregion

	    if (CustomValue.EndsWith("fr") && double.TryParse(CustomValue.TrimEnd("fr"), out _))
	    {
		    CustomValueType = "flex";
		    return;
	    }

	    if (CustomValue.IsValidWebHexColor() || CustomValue.StartsWith("rgb(") || CustomValue.StartsWith("rgba(") || SfumatoScss.CssNamedColors.Contains(CustomValue))
	    {
		    CustomValueType = "color";
		    return;
		}

	    if (CustomValue.StartsWith('\'') && CustomValue.EndsWith('\''))
	    {
		    CustomValueType = "string";
		    return;
	    }

	    if (CustomValue.StartsWith("url(", StringComparison.Ordinal) && CustomValue.EndsWith(')') || (CustomValue.StartsWith('/') && Uri.TryCreate(CustomValue, UriKind.Relative, out _)) || (Uri.TryCreate(CustomValue, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
	    {
		    CustomValueType = "url";
		    return;
		}
	    
	    #region angle

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssAngleUnits)
	    {
		    unitless = CustomValue.TrimEnd(unit) ?? string.Empty;
		    
		    if (CustomValue.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    CustomValueType = "angle";
		    return;
	    }

	    #endregion

	    #region time

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssTimeUnits)
	    {
		    unitless = CustomValue.TrimEnd(unit) ?? string.Empty;
		    
		    if (CustomValue.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    CustomValueType = "time";
		    return;
	    }

	    #endregion

	    #region frequency

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssFrequencyUnits)
	    {
		    unitless = CustomValue.TrimEnd(unit) ?? string.Empty;
		    
		    if (CustomValue.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    CustomValueType = "frequency";
		    return;
	    }

	    #endregion

	    #region resolution

	    unitless = string.Empty;

	    foreach (var unit in SfumatoScss.CssResolutionUnits)
	    {
		    unitless = CustomValue.TrimEnd(unit) ?? string.Empty;
		    
		    if (CustomValue.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
	    {
		    CustomValueType = "resolution";
		    return;
	    }

	    #endregion
	    
	    #region ratio
	    
	    if (CustomValue.Length < 3 || CustomValue.IndexOf('/') < 1 || CustomValue.IndexOf('/') == CustomValue.Length - 1)
		    return;
	    
	    var customValues = CustomValue.Replace('_', ' ').Split('/', StringSplitOptions.RemoveEmptyEntries);

	    if (customValues.Length != 2)
		    return;

	    if (double.TryParse(customValues[0], out _) && double.TryParse(customValues[1], out _))
		    CustomValueType = "ratio";

	    #endregion
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
