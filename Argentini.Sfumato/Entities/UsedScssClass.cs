using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.Entities;

public class UsedScssClass
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
    public ScssUtilityClass? ScssUtilityClass { get; set; }
    public bool IsArbitraryCss => ScssUtilityClass is null;
    public int PrefixSortOrder { get; set; }
    public int SortOrder { get; set; }
    
    #endregion
    
    public UsedScssClass()
    {}

    public UsedScssClass(string selector)
    {
        CssSelector = new CssSelector
        {
            Value = selector
        };
    }

    public void BuildPrefixSortOrder()
    {
        PrefixSortOrder = 0;

        if (CssSelector is null)
            return;
        
        foreach (var breakpoint in SfumatoScss.MediaQueryPrefixes)
            if (CssSelector.MediaQueries.Contains(breakpoint.Prefix))
                PrefixSortOrder += breakpoint.Priority;    
    }
}