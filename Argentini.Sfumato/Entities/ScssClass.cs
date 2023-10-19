namespace Argentini.Sfumato.Entities;

public class ScssClass
{
    #region Properties

    private CssSelector? _cssSelector;
    public CssSelector? CssSelector
    {
        get => _cssSelector;
        set
        {
            _cssSelector = value;
            BuildPrefixSortOrder();
        }
    }
    public string Value { get; set; } = string.Empty;
    public string ValueTypes { get; set; } = string.Empty;
    public string ChildSelector { get; set; } = string.Empty;
    public string Template { get; set; } = string.Empty;
    public int PrefixSortOrder { get; set; }
    public int SortOrder { get; set; }
    public string GlobalGrouping { get; set; } = string.Empty; // For creating shared styles for a group of classes
    
    #endregion
    
    public ScssClass()
    {}

    public ScssClass(string selector)
    {
        CssSelector = new CssSelector
        {
            Value = selector
        };

        BuildPrefixSortOrder();
    }

    protected void BuildPrefixSortOrder()
    {
        PrefixSortOrder = 0;

        if (CssSelector is null)
            return;
        
        foreach (var breakpoint in SfumatoScss.MediaQueryPrefixes)
        {
            if (CssSelector.MediaQueries.Contains(breakpoint.Prefix))
                PrefixSortOrder += breakpoint.Priority;    
        }
    }
    
    public string GetStyles()
    {
        return Template.Replace("{value}", Value);
    }
}