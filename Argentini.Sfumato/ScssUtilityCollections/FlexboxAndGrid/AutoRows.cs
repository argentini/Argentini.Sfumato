namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class AutoRows : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "auto-rows";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "grid-auto-rows: auto;",
        ["min"] = "grid-auto-rows: min-content;",
        ["max"] = "grid-auto-rows: max-content;",
        ["fr"] = "grid-auto-rows: minmax(0, 1fr);"
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
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"grid-auto-rows: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}