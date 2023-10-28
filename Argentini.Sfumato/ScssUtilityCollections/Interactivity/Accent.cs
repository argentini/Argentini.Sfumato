namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Accent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "accent";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"accent-color: {color};";
        
        #endregion
        
        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

            if (valueType == "integer")
            {
                var value = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(value) / 100m;
                
                return $"accent-color: {color.Replace(",1.0)", $",{opacity:F2})")};";
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"accent-color: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}