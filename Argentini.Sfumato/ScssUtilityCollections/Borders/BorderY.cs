namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class BorderY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-y";

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
        
        // Color preset (e.g. border-x-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"""
                   border-top-color: {color};
                   border-bottom-color: {color};
                   """;

        // Value preset (e.g. border-x-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-top-width: {size};
                   border-bottom-width: {size};
                   """;
        
        #endregion
        
        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            
            if (valueType == "integer")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(modifierValue) / 100m;

                return $"""
                        border-top-color: {color.Replace(",1.0)", $",{opacity:F2})")};
                        border-bottom-color: {color.Replace(",1.0)", $",{opacity:F2})")};
                        """;
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"""
                   border-top-color: {cssSelector.ArbitraryValue};
                   border-bottom-color: {cssSelector.ArbitraryValue};
                   """;

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   border-top-width: {cssSelector.ArbitraryValue};
                   border-bottom-width: {cssSelector.ArbitraryValue};
                   """;

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"""
                    border-top-style: {cssSelector.ArbitraryValue};
                    border-bottom-style: {cssSelector.ArbitraryValue};
                    """;
        
        #endregion

        return string.Empty;
    }
}