namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class H : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "h";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "height: 0px;",
        ["px"] = "height: 1px;",
        ["auto"] = "height: auto;",
        ["screen"] = "height: 100vh;",
        ["min"] = "height: min-content;",
        ["max"] = "height: max-content;",
        ["fit"] = "height: fit-content;"
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "height: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.VerbatimFractionOptions, cssSelector, "height: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "height: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}