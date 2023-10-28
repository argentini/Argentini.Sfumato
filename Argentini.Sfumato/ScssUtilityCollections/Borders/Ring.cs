namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Ring : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring";
    public override string Category => "ring";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.BorderWidthOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"--sf-ring-color: {color};";

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"box-shadow: var(--sf-ring-inset) 0 0 0 calc({size} + var(--sf-ring-offset-width)) var(--sf-ring-color);";
        
        #endregion

        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            
            if (valueType == "integer")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(modifierValue) / 100m;

                return $"--sf-ring-color: {color.Replace(",1.0)", $",{opacity:F2})")};";
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"--sf-ring-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"box-shadow: var(--sf-ring-inset) 0 0 0 calc({cssSelector.ArbitraryValue} + var(--sf-ring-offset-width)) var(--sf-ring-color);";
        
        #endregion

        return string.Empty;
    }
}