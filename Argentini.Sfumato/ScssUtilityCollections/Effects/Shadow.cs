namespace Argentini.Sfumato.ScssUtilityCollections.Effects;

public class Shadow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "shadow";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = $"box-shadow: 0 1px {3.PxToRem()} 0 rgb(0 0 0 / 0.1), 0 1px {2.PxToRem()} -1px rgb(0 0 0 / 0.1);",
        ["xs"] = "box-shadow: 0 0 0 1px rgb(0 0 0 / 0.05);",
        ["sm"] = $"box-shadow: 0 1px {2.PxToRem()} 0 rgb(0 0 0 / 0.05);",
        ["md"] = $"box-shadow: 0 {4.PxToRem()} {6.PxToRem()} -1px rgb(0 0 0 / 0.1), 0 {2.PxToRem()} {4.PxToRem()} -{2.PxToRem()} rgb(0 0 0 / 0.1);",
        ["lg"] = $"box-shadow: 0 {10.PxToRem()} {15.PxToRem()} -{3.PxToRem()} rgb(0 0 0 / 0.1), 0 {4.PxToRem()} {6.PxToRem()} -{4.PxToRem()} rgb(0 0 0 / 0.1);",
        ["xl"] = $"box-shadow: 0 {20.PxToRem()} {25.PxToRem()} -{5.PxToRem()} rgb(0 0 0 / 0.1), 0 {8.PxToRem()} {10.PxToRem()} -{6.PxToRem()} rgb(0 0 0 / 0.1);",
        ["2xl"] = $"box-shadow: 0 {25.PxToRem()} {50.PxToRem()} -{12.PxToRem()} rgb(0 0 0 / 0.25);",
        ["inner"] = $"box-shadow: inset 0 {2.PxToRem()} {4.PxToRem()} 0 rgb(0 0 0 / 0.05);",
        ["none"] = "box-shadow: 0 0 #0000;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"--sf-shadow-color: {color};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"--sf-shadow-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"box-shadow: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}