namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class TabularNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "tabular-nums";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-variant-numeric: tabular-nums;",
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