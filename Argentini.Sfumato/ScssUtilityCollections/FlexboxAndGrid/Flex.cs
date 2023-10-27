namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Flex : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "flex";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: flex;",
        ["row"] = "flex-direction: row;",
        ["row-reverse"] = "flex-direction: row-reverse;",
        ["col"] = "flex-direction: column;",
        ["col-reverse"] = "flex-direction: column-reverse;",
        ["wrap"] = "flex-wrap: wrap;",
        ["wrap-reverse"] = "flex-wrap: wrap-reverse;",
        ["nowrap"] = "flex-wrap: nowrap;",
        ["1"] = "flex: 1 1 0%;",
        ["auto"] = "flex: 1 1 auto;",
        ["initial"] = "flex: 0 1 auto;",
        ["none"] = "flex: none;",
        ["grow"] = "flex-grow: 1;",
        ["grow-0"] = "flex-grow: 0;",
        ["shrink"] = "flex-shrink: 1;",
        ["shrink-0"] = "flex-shrink: 0;"
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
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"flex: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}