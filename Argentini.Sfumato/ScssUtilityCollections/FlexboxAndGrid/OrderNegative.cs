namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class OrderNegative : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "-order";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.FlexboxAndGridNegativeWholeNumberOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Calculated Utilities
        
        // Value preset (e.g. -order-1)
        if (cssSelector.AppState.FlexboxAndGridNegativeWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"order: {unit};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "integer")
            return $"order: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}