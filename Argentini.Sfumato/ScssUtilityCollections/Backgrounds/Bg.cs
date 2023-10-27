namespace Argentini.Sfumato.ScssUtilityCollections.Backgrounds;

public class Bg : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["repeat"] = "background-repeat: repeat;",
        ["no-repeat"] = "background-repeat: no-repeat;",
        ["repeat-x"] = "background-repeat: border-box;",
        ["repeat-y"] = "background-repeat: padding-box;",
        ["repeat-round"] = "background-repeat: round;",
        ["repeat-space"] = "background-repeat: space;",
        ["origin-border"] = "background-origin: border-box;",
        ["origin-padding"] = "background-origin: padding-box;",
        ["origin-content"] = "background-origin: content-box;",
        ["fixed"] = "background-attachment: fixed;",
        ["local"] = "background-attachment: local;",
        ["scroll"] = "background-attachment: scroll;",
        ["border"] = "background-clip: border-box;",
        ["padding"] = "background-clip: padding-box;",
        ["content"] = "background-clip: content-box;",
        ["text"] = "background-clip: text;",
        ["bottom"] = "background-position: bottom;",
        ["center"] = "background-position: center;",
        ["left"] = "background-position: left;",
        ["left-bottom"] = "background-position: left bottom;",
        ["left-top"] = "background-position: left top;",
        ["right"] = "background-position: right;",
        ["right-bottom"] = "background-position: right bottom;",
        ["right-top"] = "background-position: right top;",
        ["top"] = "background-position: top;",
        ["auto"] = "background-size: auto;",
        ["cover"] = "background-size: cover;",
        ["contain"] = "background-size: contain;",
        ["none"] = "background-image: none;",
        ["gradient-to-t"] = "background-image: linear-gradient(to top, var(--sf-gradient-stops));",
        ["gradient-to-tr"] = "background-image: linear-gradient(to top right, var(--sf-gradient-stops));",
        ["gradient-to-r"] = "background-image: linear-gradient(to right, var(--sf-gradient-stops));",
        ["gradient-to-br"] = "background-image: linear-gradient(to bottom right, var(--sf-gradient-stops));",
        ["gradient-to-b"] = "background-image: linear-gradient(to bottom, var(--sf-gradient-stops));",
        ["gradient-to-bl"] = "background-image: linear-gradient(to bottom left, var(--sf-gradient-stops));",
        ["gradient-to-l"] = "background-image: linear-gradient(to left, var(--sf-gradient-stops));",
        ["gradient-to-tl"] = "background-image: linear-gradient(to top left, var(--sf-gradient-stops));"
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
            return $"background-color: {color};";
        
        #endregion
        
        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            
            if (valueType == "integer")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(modifierValue) / 100m;

                return $"background-color: {color.Replace(",1.0)", $",{opacity:F2})")};";
            }
        }

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"background-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"background-size: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "url")
            return $"background-image: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"background-position: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}