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
    public int PrefixSortOrder { get; set; }
    public int SortOrder { get; set; }
    
    #endregion
    
    public UsedScssClass()
    {}

    public UsedScssClass(SfumatoAppState appState, string selector, bool isArbitraryCss = false)
    {
        CssSelector = new CssSelector(appState, selector, isArbitraryCss);
        _ = CssSelector.ProcessValue();
    }

    public void BuildPrefixSortOrder()
    {
        PrefixSortOrder = 0;

        if (CssSelector is null)
            return;
        
        foreach (var breakpoint in SfumatoScss.MediaQueryPrefixes)
            if (CssSelector.MediaQueryVariants.Contains(breakpoint.Prefix))
                PrefixSortOrder += breakpoint.Priority;    
    }
}