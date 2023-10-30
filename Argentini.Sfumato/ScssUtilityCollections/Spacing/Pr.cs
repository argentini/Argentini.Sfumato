namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Pr : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "pr";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "padding-right: 0px;",
        ["px"] = "padding-right: 1px;",
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "padding-right: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "padding-right: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}