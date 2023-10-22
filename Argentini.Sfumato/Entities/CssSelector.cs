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
    public string RootClass { get; private set; } = string.Empty;
    public string CustomValueSegment { get; private set; } = string.Empty;
    public string CustomValue => GetCustomValue();
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
        RootClass = string.Empty;
        CustomValueSegment = string.Empty;

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
        RootClass = Value;
        
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

                RootClass = RootSegment = Value[(prefixesIndex + 1)..];

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
            
            var indexOfSlash = RootSegment.IndexOf('/');

            indexOfBracket = RootSegment.IndexOf('[');

            if (indexOfBracket == 0)
            {
                IsArbitraryCss = true;
                CustomValueSegment = RootSegment;
                RootSegment = string.Empty;
                RootClass = string.Empty;
            }

            else if (indexOfBracket > 0)
            {
                CustomValueSegment = RootSegment[indexOfBracket..];
                RootSegment = RootSegment[..indexOfBracket];
                RootClass = RootSegment;
            }

            else if (indexOfSlash > -1 && indexOfSlash < RootSegment.Length - 1)
            {
                CustomValueSegment = RootSegment[(indexOfSlash + 1)..];
                RootClass = RootSegment[..(indexOfSlash + 1)];
            }
        }

        if (RootSegment.Length > 0 || CustomValueSegment.Length > 0)
        {
            IsInvalid = false;
            return;
        }

        FixedValue = string.Empty;
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

    /// <summary>
    /// Get the custom value from parsing the custom value segment.
    /// </summary>
    /// <returns></returns>
    private string GetCustomValue()
    {
        var customValue = CustomValueSegment.TrimStart('[').TrimEnd(']').Replace('_', ' ').Replace("\\ ", "\\_");
            
        foreach (var arbitraryType in SfumatoScss.ArbitraryValueTypes)
            if (customValue?.StartsWith($"{arbitraryType}:") ?? false)
                customValue = customValue.TrimStart($"{arbitraryType}:") ?? string.Empty;

        return customValue ?? string.Empty;
    }
}
