namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class RowSpan : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-span";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["full"] = "grid-row: 1 / -1;",
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
        
        // Value preset (e.g. basis-0.5)
        if (cssSelector.AppState.FlexboxAndGridWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"grid-row: span {unit} / span {unit};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"grid-row: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}