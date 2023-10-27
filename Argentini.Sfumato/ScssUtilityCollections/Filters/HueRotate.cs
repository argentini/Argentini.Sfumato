namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class HueRotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hue-rotate";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "filter: hue-rotate(0deg);",
        ["15"] = "filter: hue-rotate(15deg);",
        ["30"] = "filter: hue-rotate(30deg);",
        ["60"] = "filter: hue-rotate(60deg);",
        ["90"] = "filter: hue-rotate(90deg);",
        ["180"] = "filter: hue-rotate(180deg);"
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
        
        if (cssSelector.ArbitraryValueType == "angle")
            return $"filter: hue-rotate({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}