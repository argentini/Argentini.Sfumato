namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class BorderSpacingX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-spacing-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "border-spacing: 0px var(--sf-border-spacing-y);",
        ["px"] = "border-spacing: 1px var(--sf-border-spacing-y);",
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "border-spacing: {value} var(--sf-border-spacing-y);", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-spacing: {value} var(--sf-border-spacing-y);", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}