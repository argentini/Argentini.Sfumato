namespace Argentini.Sfumato.Entities;

public sealed class CssSelector
{
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
    
    public List<string> MediaQueries { get; } = new();
    public List<string> PseudoClasses { get; } = new();
    public string Root { get; private set; } = string.Empty;
    public string CustomValue { get; private set; } = string.Empty;
    public string EscapedSelector { get; private set; } = string.Empty;
    public bool IsArbitraryCss { get; private set; }
    public bool IsInvalid { get; private set; }

    private void ProcessValue()
    {
        Root = Value;
        MediaQueries.Clear();
        PseudoClasses.Clear();
        CustomValue = string.Empty;
        IsArbitraryCss = false;
        EscapedSelector = string.Empty;
        IsInvalid = false;

        if (string.IsNullOrEmpty(Value))
        {
            IsInvalid = true;
            Root = string.Empty;
            return;
        }

        if (Value.IndexOf('[') > Value.IndexOf(']'))
        {
            IsInvalid = true;
            Root = string.Empty;
            return;
        }

        if (Value.Contains("[]") || Value.EndsWith("/"))
        {
            IsInvalid = true;
            Root = string.Empty;
            return;
        }
        
        EscapeCssClassName();
        
        var index = -1;

        for (var x = 0; x < Value.Length; x++)
        {
            if (Value[x] == '[')
                break;

            if (Value[x] == ':')
                index = x;
        }

        if (index > -1)
        {
            var prefixSegment = Value[..(index + 1)];

            if (prefixSegment.Length < Value.Length)
                Root = Value[prefixSegment.Length..];

            var segments = prefixSegment.Split(':', StringSplitOptions.RemoveEmptyEntries);

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
            }
        }

        if (string.IsNullOrEmpty(Root) == false)
        {
            var indexOfBracket = Root.IndexOf('[');
            var indexOfSlash = Root.IndexOf('/');

            if (indexOfBracket == 0)
                IsArbitraryCss = true;

            if (IsArbitraryCss)
            {
                CustomValue = Root;
                Root = string.Empty;
            }

            else if (indexOfBracket > 0)
            {
                CustomValue = Root[indexOfBracket..];
                Root = Root[..indexOfBracket];
            }

            else if (indexOfSlash > -1 && indexOfSlash < Root.Length - 1)
            {
                CustomValue = Root[(indexOfSlash + 1)..];
                Root = Root[..(indexOfSlash + 1)];
            }
        }

        if (string.IsNullOrEmpty(Root) && string.IsNullOrEmpty(CustomValue))
            IsInvalid = true;
    }
    
    /// <summary>
    /// Escape a CSS class name to be used in a CSS selector.
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
