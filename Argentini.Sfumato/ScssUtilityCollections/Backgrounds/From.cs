namespace Argentini.Sfumato.ScssUtilityCollections.Backgrounds;

public class From : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "from";
    public override string Category => "gradients";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.PercentageOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"""
                   --sf-gradient-from: {color} var(--sf-gradient-from-position);
                   --sf-gradient-to: transparent var(--sf-gradient-to-position);
                   --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                   """;

        // Color stops from percentages (e.g. from-50%)
        if (cssSelector.AppState.PercentageOptions.TryGetValue(cssSelector.CoreSegment, out var colorStop))
            return $"--sf-gradient-from-position: {colorStop};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"""
                   --sf-gradient-from: {cssSelector.ArbitraryValue} var(--sf-gradient-from-position);
                   --sf-gradient-to: transparent var(--sf-gradient-to-position);
                   --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                   """;
        
        #endregion

        return string.Empty;
    }
}