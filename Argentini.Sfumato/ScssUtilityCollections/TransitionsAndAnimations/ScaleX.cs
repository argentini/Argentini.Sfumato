namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class ScaleX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scale-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: scaleX(0);",
        ["50"] = "transform: scaleX(0.5);",
        ["75"] = "transform: scaleX(0.75);",
        ["90"] = "transform: scaleX(0.90);",
        ["95"] = "transform: scaleX(0.95);",
        ["100"] = "transform: scaleX(1.0);",
        ["105"] = "transform: scaleX(1.05);",
        ["110"] = "transform: scaleX(1.1);",
        ["125"] = "transform: scaleX(1.25);",
        ["150"] = "transform: scaleX(1.5);"
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
        
        if (cssSelector.ArbitraryValueType == "number")
            return $"transform: scaleX({cssSelector.ArbitraryValue});";
      
        #endregion

        return string.Empty;
    }
}