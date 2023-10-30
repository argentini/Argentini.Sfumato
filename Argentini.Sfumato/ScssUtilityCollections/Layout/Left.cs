namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Left : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "left";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "left: 0px;",
        ["px"] = "left: 1px;",
        ["auto"] = "left: auto;",
    };
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutRemUnitOptions);

        await AddToIndexAsync(appState.VerbatimFractionOptions);
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "left: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.VerbatimFractionOptions, cssSelector, "left: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "left: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}