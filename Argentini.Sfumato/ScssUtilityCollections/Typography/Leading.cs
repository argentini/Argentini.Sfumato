namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Leading : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "leading";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "line-height: 1;",
        ["tight"] = "line-height: 1.25;",
        ["snug"] = "line-height: 1.375;",
        ["normal"] = "line-height: 1.5;",
        ["relaxed"] = "line-height: 1.625;",
        ["loose"] = "line-height: 2;",
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
        
        if (cssSelector.AppState.TypographyRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"line-height: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "integer" or "number")
            return $"line-height: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}