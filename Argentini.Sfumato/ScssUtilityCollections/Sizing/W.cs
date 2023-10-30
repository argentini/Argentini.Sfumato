namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class W : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "w";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "width: 0px;",
        ["px"] = "width: 1px;",
        ["auto"] = "width: auto;",
        ["screen"] = "width: 100vw;",
        ["min"] = "width: min-content;",
        ["max"] = "width: max-content;",
        ["fit"] = "width: fit-content;"
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "width: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.VerbatimFractionOptions, cssSelector, "width: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "width: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}