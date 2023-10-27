namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class DropShadow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "drop-shadow";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = $"filter: drop-shadow(0 1px {2.PxToRem()} rgb(0 0 0 / 0.1)) drop-shadow(0 1px 1px rgb(0 0 0 / 0.06));",
        ["sm"] = "filter: drop-shadow(0 1px 1px rgb(0 0 0 / 0.05));",
        ["md"] = $"filter: drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.07)) drop-shadow(0 {2.PxToRem()} {2.PxToRem()} rgb(0 0 0 / 0.06));",
        ["lg"] = $"filter: drop-shadow(0 {10.PxToRem()} {8.PxToRem()} rgb(0 0 0 / 0.04)) drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.1));",
        ["xl"] = $"filter: drop-shadow(0 {20.PxToRem()} {13.PxToRem()} rgb(0 0 0 / 0.03)) drop-shadow(0 {8.PxToRem()} {5.PxToRem()} rgb(0 0 0 / 0.08));",
        ["2xl"] = $"filter: drop-shadow(0 {25.PxToRem()} {25.PxToRem()} rgb(0 0 0 / 0.15));",
        ["none"] = "filter: drop-shadow(0 0 #0000);"
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"filter: drop-shadow({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}