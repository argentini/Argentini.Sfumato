namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class ScrollMy : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-my";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                scroll-margin-top: 0;
                scroll-margin-bottom: 0;
                """,
        ["px"] = """
                 scroll-margin-top: 1px;
                 scroll-margin-bottom: 1px;
                 """,
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
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
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"""
                   scroll-margin-top: {unit};
                   scroll-margin-bottom: {unit};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   scroll-margin-top: {cssSelector.ArbitraryValue};
                   scroll-margin-bottom: {cssSelector.ArbitraryValue};
                   """;
        
        #endregion

        return string.Empty;
    }
}