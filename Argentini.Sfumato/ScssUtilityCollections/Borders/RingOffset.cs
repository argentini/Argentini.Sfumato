namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RingOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-offset";
    public override string Category => "ring";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.BorderWidthOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"""
                   --sf-ring-offset-color: {color};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   --sf-ring-offset-width: {size};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
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
                        --sf-ring-offset-color: {color.Replace(",1.0)", $",{opacity:F2})")};
                        box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                        """;
            }

            if (valueType == "number")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                return $"""
                        --sf-ring-offset-color: {color.Replace(",1.0)", $",{modifierValue})")};
                        box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                        """;
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"""
                   --sf-ring-offset-color: {cssSelector.ArbitraryValue};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   --sf-ring-offset-width: {cssSelector.ArbitraryValue};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;
        
        #endregion

        return string.Empty;
    }
}