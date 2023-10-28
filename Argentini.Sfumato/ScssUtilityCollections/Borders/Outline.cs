namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Outline : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "outline";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "outline-style: solid;",
        ["dashed"] = "outline-style: dashed;",
        ["dotted"] = "outline-style: dotted;",
        ["double"] = "outline-style: double;",
        ["none"] = "outline-style: none;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"outline-color: {color};";

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"outline-width: {size};";
        
        #endregion
        
        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            
            if (valueType == "integer")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(modifierValue) / 100m;

                return $"outline-color: {color.Replace(",1.0)", $",{opacity:F2})")};";
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"outline-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"outline-width: {cssSelector.ArbitraryValue};";
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"outline-style: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}