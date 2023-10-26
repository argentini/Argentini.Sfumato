namespace Argentini.Sfumato.ScssUtilityCollections.Backgrounds;

public class To : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "to";
    public override string Category => "gradients";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["inherit"] = "--sf-gradient-to: inherit var(--sf-gradient-to-position);",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Color preset (e.g. bg-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"--sf-gradient-to: {color} var(--sf-gradient-to-position);";

        // Color stops from percentages (e.g. from-50%)
        if (cssSelector.AppState.PercentageOptions.TryGetValue(cssSelector.CoreSegment, out var colorStop))
            return $"--sf-gradient-to-position: {colorStop};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"--sf-gradient-to: {cssSelector.ArbitraryValue} var(--sf-gradient-to-position);";
        
        if (cssSelector.ArbitraryValueType == "percentage")
            return $"--sf-gradient-to-position: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}