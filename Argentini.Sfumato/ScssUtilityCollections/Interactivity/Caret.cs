namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Caret : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "caret";

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
        
        // Color preset (e.g. caret-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"caret-color: {color};";
        
        #endregion
        
        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            
            if (valueType == "integer")
            {
                var value = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(value) / 100m;
                
                return $"caret-color: {color.Replace(",1.0)", $",{opacity:F2})")};";
            }

            if (valueType == "number")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                return $"caret-color: {color.Replace(",1.0)", $",{modifierValue})")};";
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"caret-color: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}