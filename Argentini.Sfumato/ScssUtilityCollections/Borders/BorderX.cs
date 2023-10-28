namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class BorderX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-x";

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
                   border-left-color: {color};
                   border-right-color: {color};
                   """;

        // Value preset (e.g. border-x-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-left-width: {size};
                   border-right-width: {size};
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
                        border-left-color: {color.Replace(",1.0)", $",{opacity:F2})")};
                        border-right-color: {color.Replace(",1.0)", $",{opacity:F2})")};
                        """;
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"""
                   border-left-color: {cssSelector.ArbitraryValue};
                   border-right-color: {cssSelector.ArbitraryValue};
                   """;

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   border-left-width: {cssSelector.ArbitraryValue};
                   border-right-width: {cssSelector.ArbitraryValue};
                   """;

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"""
                   border-left-style: {cssSelector.ArbitraryValue};
                   border-right-style: {cssSelector.ArbitraryValue};
                   """;
        
        #endregion

        return string.Empty;
    }
}