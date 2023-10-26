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
    
    #endregion
    
    public UsedScssClass()
    {}

    public UsedScssClass(SfumatoAppState appState, string selector, bool isArbitraryCss = false)
    {
        CssSelector = new CssSelector(appState, selector, isArbitraryCss);
        _ = CssSelector.ProcessSelector();
    }

    public void BuildPrefixSortOrder()
    {
        PrefixSortOrder = 0;

        if (CssSelector is null)
            return;
        
        foreach (var breakpoint in CssSelector.AppState?.MediaQueryPrefixes ?? Array.Empty<CssMediaQuery>())
            if (CssSelector.MediaQueryVariants.Contains(breakpoint.Prefix))
                PrefixSortOrder += breakpoint.Priority;    
    }
}