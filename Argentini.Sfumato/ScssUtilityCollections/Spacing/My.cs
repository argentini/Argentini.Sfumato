namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class My : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "my";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                margin-top: 0px;
                margin-bottom: 0px;
                """,
        ["px"] = """
                 margin-top: 1px;
                 margin-bottom: 1px;
                 """,
        ["auto"] = """
                 margin-top: auto;
                 margin-bottom: auto;
                 """,
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
            return $"""
                   margin-top: {unit};
                   margin-bottom: {unit};
                   """;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   margin-top: {cssSelector.ArbitraryValue};
                   margin-bottom: {cssSelector.ArbitraryValue};
                   """;
      
        #endregion

        return string.Empty;
    }
}