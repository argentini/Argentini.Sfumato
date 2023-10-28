namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class AutoCols : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "auto-cols";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "grid-auto-columns: auto;",
        ["min"] = "grid-auto-columns: min-content;",
        ["max"] = "grid-auto-columns: max-content;",
        ["fr"] = "grid-auto-columns: minmax(0, 1fr);"
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
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"grid-auto-columns: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}