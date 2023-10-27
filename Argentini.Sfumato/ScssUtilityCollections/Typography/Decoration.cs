namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Decoration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "decoration";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["solid"] = "text-decoration-style: solid;",
        ["double"] = "text-decoration-style: double;",
        ["dotted"] = "text-decoration-style: dotted;",
        ["dashed"] = "text-decoration-style: dashed;",
        ["wavy"] = "text-decoration-style: wavy;",
        ["auto"] = "text-decoration-thickness: auto;",
        ["from-font"] = "text-decoration-thickness: from-font;",
        ["0"] = "text-decoration-thickness: 0px;",
        ["1"] = "text-decoration-thickness: 1px;",
        ["2"] = $"text-decoration-thickness: {2.PxToRem()};",
        ["4"] = $"text-decoration-thickness: {4.PxToRem()};",
        ["8"] = $"text-decoration-thickness: {8.PxToRem()};"
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

        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"text-decoration-color: {color};";
        
        #endregion

        if (cssSelector.HasModifierValue || cssSelector.HasArbitraryValue)
        {
            if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
            {
                var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

                if (valueType == "integer")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                    var opacity = int.Parse(modifierValue) / 100m;

                    return $"text-decoration-color: {color.Replace(",1.0)", $",{opacity:F2})")};";
                }
            }
        }
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"text-decoration-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType == "length")
            return $"text-decoration-thickness: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}