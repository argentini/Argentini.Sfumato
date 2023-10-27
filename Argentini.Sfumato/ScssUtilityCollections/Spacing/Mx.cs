namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Mx : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "mx";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                margin-left: 0px;
                margin-right: 0px;
                """,
        ["px"] = """
                 margin-left: 1px;
                 margin-right: 1px;
                 """,
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"""
                   margin-left: {unit};
                   margin-right: {unit};
                   """;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   margin-left: {cssSelector.ArbitraryValue};
                   margin-right: {cssSelector.ArbitraryValue};
                   """;
      
        #endregion

        return string.Empty;
    }
}