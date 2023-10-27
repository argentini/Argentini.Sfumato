namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class ProportionalNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "proportional-nums";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-variant-numeric: proportional-nums;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}