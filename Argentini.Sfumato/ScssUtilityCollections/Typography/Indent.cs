namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Indent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "indent";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "text-indent: 0px;",
        ["px"] = "text-indent: 1px;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
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
        
        #region Calculated Utilities

        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"text-indent: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length")
            return $"text-indent: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}