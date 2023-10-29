namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class ScaleY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scale-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: scaleY(0);",
        ["50"] = "transform: scaleY(0.5);",
        ["75"] = "transform: scaleY(0.75);",
        ["90"] = "transform: scaleY(0.90);",
        ["95"] = "transform: scaleY(0.95);",
        ["100"] = "transform: scaleY(1.0);",
        ["105"] = "transform: scaleY(1.05);",
        ["110"] = "transform: scaleY(1.1);",
        ["125"] = "transform: scaleY(1.25);",
        ["150"] = "transform: scaleY(1.5);"
    };
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

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
            return $"transform: scaleY({cssSelector.ArbitraryValue});";
      
        #endregion

        return string.Empty;
    }
}