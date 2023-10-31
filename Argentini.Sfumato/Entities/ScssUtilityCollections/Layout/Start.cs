namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Start : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "start";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "inset-inline-start: 0px;",
        ["px"] = "inset-inline-start: 1px;",
        ["auto"] = "inset-inline-start: auto;",
        ["full"] = "inset-inline-start: 100%;",
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
        
        if (ProcessFractionModifierOptions(cssSelector, "inset-inline-start: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "inset-inline-start: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "inset-inline-start: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "inset-inline-start: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}