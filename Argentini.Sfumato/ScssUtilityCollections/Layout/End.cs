namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class End : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "end";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "inset-inline-end: 0px;",
        ["px"] = "inset-inline-end: 1px;",
        ["auto"] = "inset-inline-end: auto;",
        ["full"] = "inset-inline-end: 100%;",
    };
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutRemUnitOptions);

        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector, "inset-inline-end: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "inset-inline-end: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "inset-inline-end: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "inset-inline-end: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}