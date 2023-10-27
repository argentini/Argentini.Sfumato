namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class ScrollPy : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-py";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                scroll-padding-top: 0;
                scroll-padding-bottom: 0;
                """,
        ["px"] = """
                 scroll-padding-top: 1px;
                 scroll-padding-bottom: 1px;
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
                   scroll-padding-top: {unit};
                   scroll-padding-bottom: {unit};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   scroll-padding-top: {cssSelector.ArbitraryValue};
                   scroll-padding-bottom: {cssSelector.ArbitraryValue};
                   """;
        
        #endregion

        return string.Empty;
    }
}