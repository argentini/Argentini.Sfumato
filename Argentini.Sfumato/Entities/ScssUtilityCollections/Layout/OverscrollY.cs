namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class OverscrollY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overscroll-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overscroll-behavior-y: auto;",
        ["contain"] = "overscroll-behavior-y: contain;",
        ["none"] = "overscroll-behavior-y: none;",
    }; 
        
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}