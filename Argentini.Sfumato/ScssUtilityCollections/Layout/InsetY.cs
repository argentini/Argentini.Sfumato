namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class InsetY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inset-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                top: 0px;
                bottom: 0px;
                """,
        ["px"] = """
                 top: 1px;
                 bottom: 1px;
                 """,
        ["auto"] = """
                   top: auto;
                   bottom: auto;
                   """,
    };
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.VerbatimFractionOptions.Keys)
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
                   top: {unit};
                   bottom: {unit};
                   """;

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"""
                   top: {fraction};
                   bottom: {fraction};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   top: {cssSelector.ArbitraryValue};
                   bottom: {cssSelector.ArbitraryValue};
                   """;
      
        #endregion

        return string.Empty;
    }
}