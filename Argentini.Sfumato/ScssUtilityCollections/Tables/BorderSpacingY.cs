namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class BorderSpacingY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-spacing-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "border-spacing: var(--sf-border-spacing-x) 0px;",
        ["px"] = "border-spacing: var(--sf-border-spacing-x) 1px;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "border-spacing: var(--sf-border-spacing-x) {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-spacing: var(--sf-border-spacing-x) {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}