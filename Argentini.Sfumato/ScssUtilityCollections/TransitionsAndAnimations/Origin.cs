namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class Origin : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "origin";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["center"] = "transform-origin: center;",
        ["top"] = "transform-origin: top;",
        ["top-right"] = "transform-origin: top right;",
        ["right"] = "transform-origin: right;",
        ["bottom-right"] = "transform-origin: bottom right;",
        ["bottom"] = "transform-origin: bottom;",
        ["bottom-left"] = "transform-origin: bottom left;",
        ["left"] = "transform-origin: left;",
        ["top-left"] = "transform-origin: top left;"
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
            return $"transform-origin: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}