namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class MaxH : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "max-h";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "max-height: 0px;",
        ["px"] = "max-height: 1px;",
        ["none"] = "max-height: none;",
        ["full"] = "max-height: 100%;",
        ["screen"] = "max-height: 100vh;",
        ["min"] = "max-height: min-content;",
        ["max"] = "max-height: max-content;",
        ["fit"] = "max-height: fit-content;",
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
        
        if (cssSelector.ArbitraryValueType is "integer" or "length" or "percentage")
            return $"max-height: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}