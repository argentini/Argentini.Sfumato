namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Text : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["left"] = "text-align: left;",
        ["center"] = "text-align: center;",
        ["right"] = "text-align: right;",
        ["justify"] = "text-align: justify;",
        ["start"] = "text-align: start;",
        ["end"] = "text-align: end;"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.ColorOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.TextSizeOptions.Keys)
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

        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"color: {color};";

        if (cssSelector.AppState.TextSizeOptions.TryGetValue(cssSelector.CoreSegment, out var fontSize))
            return $"""
                   font-size: {fontSize};
                   line-height: {cssSelector.AppState.TextSizeLeadingOptions[cssSelector.CoreSegment]};
                   """; 
        
        #endregion
        
        #region Modifier Utilities

        if (cssSelector.HasModifierValue || cssSelector.HasArbitraryValue)
        {
            if (cssSelector.AppState.TextSizeOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out fontSize))
            {
                var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

                if (valueType is "length" or "percentage")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    return $"""
                            font-size: {fontSize};
                            line-height: {modifierValue};
                            """;
                }
                
                if (valueType is "integer" or "number" or "")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    if (valueType is "integer" or "" && cssSelector.AppState.LeadingOptions.TryGetValue(modifierValue, out var option))
                    {
                        return $"""
                                font-size: {fontSize};
                                line-height: {option};
                                """;
                    }

                    if (valueType is "integer" or "number")
                    {
                        return $"""
                                font-size: {fontSize};
                                line-height: {modifierValue};
                                """;
                    }
                }
            }
            
            if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
            {
                var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

                if (valueType == "integer")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                    var opacity = int.Parse(modifierValue) / 100m;

                    return $"color: {color.Replace(",1.0)", $",{opacity:F2})")};";
                }

                if (valueType == "number")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    return $"color: {color.Replace(",1.0)", $",{modifierValue})")};";
                }
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"font-size: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}