namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Basis : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "basis";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Value preset (e.g. basis-0.5)
        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"flex-basis: {unit};";
        
        // Value preset (e.g. basis-1/2)
        if (cssSelector.AppState.FractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"flex-basis: {fraction};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"flex-basis: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}