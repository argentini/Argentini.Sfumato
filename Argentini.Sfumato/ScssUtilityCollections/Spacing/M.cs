namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class M : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "m";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "margin: auto;",
        ["0"] = "margin: 0px;",
        ["px"] = "margin: 1px;",
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "margin: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "margin: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}