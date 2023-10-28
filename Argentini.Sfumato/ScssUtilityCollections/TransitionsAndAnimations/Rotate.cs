namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class Rotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "rotate";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: rotate(0deg);",
        ["1"] = "transform: rotate(1deg);",
        ["2"] = "transform: rotate(2deg);",
        ["3"] = "transform: rotate(3deg);",
        ["6"] = "transform: rotate(6deg);",
        ["12"] = "transform: rotate(12deg);",
        ["45"] = "transform: rotate(45deg);",
        ["90"] = "transform: rotate(90deg);",
        ["180"] = "transform: rotate(180deg);"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
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
            return $"transform: rotate({cssSelector.ArbitraryValue});";
      
        #endregion

        return string.Empty;
    }
}