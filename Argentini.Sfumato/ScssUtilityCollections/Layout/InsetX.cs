namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class InsetX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inset-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                left: 0px;
                right: 0px;
                """,
        ["px"] = """
                 left: 1px;
                 right: 1px;
                 """,
        ["auto"] = """
                   left: auto;
                   right: auto;
                   """,
    };
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.VerbatimFractionOptions.Keys)
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
                   left: {unit};
                   right: {unit};
                   """;

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"""
                   left: {fraction};
                   right: {fraction};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   left: {cssSelector.ArbitraryValue};
                   right: {cssSelector.ArbitraryValue};
                   """;
      
        #endregion

        return string.Empty;
    }
}