namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class ScrollMx : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-mx";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                scroll-margin-left: 0;
                scroll-margin-right: 0;
                """,
        ["px"] = """
                 scroll-margin-left: 1px;
                 scroll-margin-right: 1px;
                 """,
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"""
                   scroll-margin-left: {unit};
                   scroll-margin-right: {unit};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   scroll-margin-left: {cssSelector.ArbitraryValue};
                   scroll-margin-right: {cssSelector.ArbitraryValue};
                   """;
        
        #endregion

        return string.Empty;
    }
}