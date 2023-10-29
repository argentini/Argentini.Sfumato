namespace Argentini.Sfumato.ScssUtilityCollections.Transforms;

public class Duration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "duration";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transition-duration: 0s;",
        ["75"] = "transition-duration: 75ms;",
        ["100"] = "transition-duration: 100ms;",
        ["150"] = "transition-duration: 150ms;",
        ["200"] = "transition-duration: 200ms;",
        ["300"] = "transition-duration: 300ms;",
        ["500"] = "transition-duration: 500ms;",
        ["700"] = "transition-duration: 700ms;",
        ["1000"] = "transition-duration: 1000ms;"
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
        
        if (cssSelector.ArbitraryValueType == "time")
            return $"transition-duration: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}