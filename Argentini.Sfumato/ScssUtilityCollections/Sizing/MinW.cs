namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class MinW : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "min-w";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "min-width: 0px;",
        ["px"] = "min-width: 1px;",
        ["screen"] = "min-width: 100vw;",
        ["min"] = "min-width: min-content;",
        ["max"] = "min-width: max-content;",
        ["fit"] = "min-width: fit-content;"
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
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"min-width: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}