namespace Argentini.Sfumato.ScssUtilityCollections.Transforms;

public class Ease : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ease";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["linear"] = "transition-timing-function: linear;",
        ["in"] = "transition-timing-function: cubic-bezier(0.4, 0, 1, 1);",
        ["out"] = "transition-timing-function: cubic-bezier(0, 0, 0.2, 1);",
        ["in-out"] = "transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);",
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
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"transition-timing-function: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}