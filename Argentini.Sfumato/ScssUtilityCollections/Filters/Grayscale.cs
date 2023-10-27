namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Grayscale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grayscale";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "filter: grayscale(100%);",
        ["0"] = "filter: grayscale(0);"
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
        
        if (cssSelector.ArbitraryValueType == "percentage")
            return $"filter: grayscale({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}