namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Align : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "align";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["baseline"] = "vertical-align: baseline;",
        ["top"] = "vertical-align: top;",
        ["middle"] = "vertical-align: middle;",
        ["bottom"] = "vertical-align: bottom;",
        ["text-top"] = "vertical-align: text-top;",
        ["text-bottom"] = "vertical-align: text-bottom;",
        ["sub"] = "vertical-align: sub;",
        ["super"] = "vertical-align: super;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length")
            return $"vertical-align: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}