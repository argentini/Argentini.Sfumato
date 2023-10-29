namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class GridCols : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grid-cols";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "grid-template-columns: none;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.FlexboxAndGridWholeNumberOptions.Keys)
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
        
        #region Calculated Utilities
        
        // Value preset (e.g. grid-cols-1)
        if (cssSelector.AppState.FlexboxAndGridWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"grid-template-columns: repeat({unit}, minmax(0, 1fr));";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"grid-template-columns: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}