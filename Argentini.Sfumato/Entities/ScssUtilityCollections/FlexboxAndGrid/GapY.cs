namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class GapY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "gap-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "row-gap: auto;",
        ["px"] = "row-gap: min-content;",
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "row-gap: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "row-gap: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}