namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Interactivity;

public class ScrollPe : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-pe";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "scroll-padding-inline-end: 0;",
        ["px"] = "scroll-padding-inline-end: 1px;",
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "scroll-padding-inline-end: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "scroll-padding-inline-end: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}