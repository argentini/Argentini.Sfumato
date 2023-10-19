namespace Argentini.Sfumato.Entities;

public class ScssClass
{
    public string RootClassName { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string ValueTypes { get; set; } = string.Empty;
    public string ChildSelector { get; set; } = string.Empty;
    public string Template { get; set; } = string.Empty;
    public int Depth { get; set; }
    public int PrefixSortOrder { get; set; }
    public int SortOrder { get; set; }

    public bool IsUtilityClass { get; set; }
    public string GlobalGrouping { get; set; } = string.Empty; // For creating shared styles for a group of classes
    
    private string _userClassName = string.Empty;
    public string UserClassName
    {
        get => _userClassName;

        set
        {
            _userClassName = value;
            Prefixes = Array.Empty<string>();

            if (_userClassName.Contains(":[") && _userClassName.EndsWith(']'))
            {
                var segments = _userClassName[.._userClassName.IndexOf('[')].Split(':', StringSplitOptions.RemoveEmptyEntries);

                Prefixes = new string[segments.Length];

                Array.Copy(segments, Prefixes, segments.Length);
            }

            else
            {
                var trimmed = _userClassName;

                if (_userClassName.EndsWith(']') && _userClassName.Contains('['))
                {
                    trimmed = _userClassName[.._userClassName.IndexOf('[')];
                }

                if (trimmed.LastIndexOf(':') == -1 || trimmed.LastIndexOf(':') >= trimmed.Length - 1)
                    return;

                var segments = trimmed.Split(':', StringSplitOptions.RemoveEmptyEntries);

                if (segments.Length < 2)
                    return;

                Prefixes = new string[segments.Length - 1];

                Array.Copy(segments, Prefixes, segments.Length - 1);
            }
        }
    }
    
    public string[] Prefixes { get; private set; } = Array.Empty<string>();
    
    public string GetStyles()
    {
        return Template.Replace("{value}", Value);
    }
}

public sealed class ArbitraryScssClass : ScssClass
{
}