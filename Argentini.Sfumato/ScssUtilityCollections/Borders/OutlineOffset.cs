namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class OutlineOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "outline-offset";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.BorderWidthOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "outline-offset: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "outline-offset: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}