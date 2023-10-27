namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Order : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "order";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["first"] = $"order: {int.MinValue.ToString()};",
        ["last"] = $"order: {int.MaxValue.ToString()};",
        ["none"] = "order: 0;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Value preset (e.g. order-1)
        if (cssSelector.AppState.FlexboxAndGridWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
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