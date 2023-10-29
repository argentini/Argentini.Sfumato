namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Basis : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "basis";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.FlexRemUnitOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.FractionOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FlexRemUnitOptions, cssSelector, "flex-basis: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.FractionOptions, cssSelector, "flex-basis: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "flex-basis: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}