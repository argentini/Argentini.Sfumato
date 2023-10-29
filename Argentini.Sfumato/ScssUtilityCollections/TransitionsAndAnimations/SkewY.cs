namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class SkewY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "skew-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: skewY(0deg);",
        ["1"] = "transform: skewY(1deg);",
        ["2"] = "transform: skewY(2deg);",
        ["3"] = "transform: skewY(3deg);",
        ["6"] = "transform: skewY(6deg);",
        ["12"] = "transform: skewY(12deg);",
        ["45"] = "transform: skewY(45deg);",
        ["90"] = "transform: skewY(90deg);",
        ["180"] = "transform: skewY(180deg);"
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
        
        if (cssSelector.ArbitraryValueType == "angle")
            return $"transform: skewY({cssSelector.ArbitraryValue});";
      
        #endregion

        return string.Empty;
    }
}