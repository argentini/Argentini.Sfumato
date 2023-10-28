namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class MinH : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "min-h";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "min-height: 0px;",
        ["full"] = "min-height: 100%;",
        ["screen"] = "min-height: 100vh;",
        ["min"] = "min-height: min-content;",
        ["max"] = "min-height: max-content;",
        ["fit"] = "min-height: fit-content;"
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
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"min-height: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}