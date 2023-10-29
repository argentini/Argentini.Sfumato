namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropHueRotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-hue-rotate";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "backdrop-filter: hue-rotate(0deg);",
        ["15"] = "backdrop-filter: hue-rotate(15deg);",
        ["30"] = "backdrop-filter: hue-rotate(30deg);",
        ["60"] = "backdrop-filter: hue-rotate(60deg);",
        ["90"] = "backdrop-filter: hue-rotate(90deg);",
        ["180"] = "backdrop-filter: hue-rotate(180deg);"
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
            return $"backdrop-filter: hue-rotate({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}