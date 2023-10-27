namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Accent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "accent";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. bg-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"accent-color: {color};";
        
        #endregion
        
        #region Modifier Utilities
        
        // Color with opacity modifier (e.g. bg-rose-100/25, bg-rose-100/[25])
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out var colorWithOpacity))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            var value = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
            
            if (valueType != "integer")
                return string.Empty;

            var opacity = int.Parse(value) / 100m;
            
            return $"accent-color: {colorWithOpacity.Replace(",1.0)", $",{opacity:F2})")};";
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