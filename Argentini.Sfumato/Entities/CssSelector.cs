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
            _value = value;
            ProcessValue();
        }
    }
    
    public string FixedValue { get; private set; } = string.Empty;
    public List<string> MediaQueries { get; } = new();
    public List<string> PseudoClasses { get; } = new();
    public List<string> AllPrefixes { get; } = new();
    public string RootSegment { get; private set; } = string.Empty;
    public string RootClass { get; private set; } = string.Empty;
    public string CustomValueSegment { get; private set; } = string.Empty;
    public string CustomValue { get; private set; } = string.Empty;
    public string EscapedSelector { get; private set; } = string.Empty;
    public int Depth { get; private set; }
    public bool IsArbitraryCss { get; private set; }
    public bool IsInvalid { get; private set; }

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
    private void ProcessValue()
    {
        FixedValue = string.Empty;
        RootSegment = string.Empty;
        RootClass = string.Empty;
        MediaQueries.Clear();
        PseudoClasses.Clear();
        AllPrefixes.Clear();
        CustomValueSegment = string.Empty;
        CustomValue = string.Empty;
        EscapedSelector = string.Empty;

        Depth = 0;
        
        IsArbitraryCss = false;
        IsInvalid = false;

        if (string.IsNullOrEmpty(Value))
        {
            IsInvalid = true;
            return;
        }

        if (Value.IndexOf('[') > Value.IndexOf(']'))
        {
            IsInvalid = true;
            return;
        }

        if (Value.Contains("[]"))
        {
            IsInvalid = true;
            return;
        }

        FixedValue = Value;
        RootSegment = Value;
        RootClass = Value;
        
        EscapeCssClassName();

        var index = -1;

        for (var x = 0; x < Value.Length; x++)
        {
            if (Value[x] == '[')
                break;

            if (Value[x] == ':')
                index = x;
        }

        if (index > 0)
        {
            var prefixSegment = Value[..(index + 1)];
            var segments = prefixSegment.Split(':', StringSplitOptions.RemoveEmptyEntries);

            RootClass = RootSegment = Value[(index + 1)..];

            if (segments.Length > 0)
            {
                var lastType = string.Empty;

                foreach (var breakpoint in SfumatoScss.MediaQueryPrefixes.OrderBy(k => k.PrefixOrder))
                {
                    if (breakpoint.PrefixType == lastType)
                        continue;

                    if (MediaQueries.Contains(breakpoint.Prefix))
                        continue;

                    if (segments.Contains(breakpoint.Prefix) == false)
                        continue;

                    MediaQueries.Add(breakpoint.Prefix);

                    lastType = breakpoint.PrefixType;
                }

                foreach (var segment in segments)
                {
                    if (PseudoClasses.Contains(segment))
                        continue;

                    if (SfumatoScss.PseudoclassPrefixes.ContainsKey(segment) == false)
                        continue;

                    PseudoClasses.Add(segment);
                }

                AllPrefixes.AddRange(MediaQueries);
                AllPrefixes.AddRange(PseudoClasses);
            }
        }

        if (string.IsNullOrEmpty(RootSegment) == false)
        {
            var indexOfBracket = RootSegment.IndexOf('[');
            var indexOfSlash = RootSegment.IndexOf('/');

            if (indexOfBracket == 0)
                IsArbitraryCss = true;

            if (IsArbitraryCss)
            {
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
            
            CustomValue = CustomValueSegment.TrimStart('[').TrimEnd(']').Replace('_', ' ').Replace("\\ ", "\\_");
            
            foreach (var arbitraryType in SfumatoScss.ArbitraryValueTypes)
                if (CustomValue?.StartsWith($"{arbitraryType}:") ?? false)
                    CustomValue = CustomValue.TrimStart($"{arbitraryType}:") ?? string.Empty;
        }

        if (MediaQueries.Count > 0 || PseudoClasses.Count > 0)
        {
            FixedValue = string.Empty;

            if (MediaQueries.Count > 0)
                FixedValue += $"{string.Join(':', MediaQueries)}:";

            if (PseudoClasses.Count > 0)
                FixedValue += $"{string.Join(':', PseudoClasses)}:";
            
            FixedValue += RootClass;
            FixedValue += CustomValueSegment;
        }

        Depth = AllPrefixes.Count;
        
        if (string.IsNullOrEmpty(RootSegment) == false || string.IsNullOrEmpty(CustomValueSegment) == false)
            return;
        
        IsInvalid = true;
        FixedValue = string.Empty;
    }
    
    /// <summary>
    /// Escape the CSS class name to be used in a CSS selector.
    /// </summary>
    /// <returns></returns>
    private void EscapeCssClassName()
    {
        if (string.IsNullOrEmpty(Value))
            return;

        var value = new StringBuilder();

        for (var i = 0; i < Value.Length; i++)
        {
            var c = Value[i];
            
            if ((i == 0 && char.IsDigit(c)) || (char.IsLetterOrDigit(c) == false && c != '-' && c != '_'))
                value.Append('\\');
            
            value.Append(c);
        }

        EscapedSelector = value.ToString();
    }
}
