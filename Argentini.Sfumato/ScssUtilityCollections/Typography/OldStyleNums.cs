namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class OldStyleNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "oldstyle-nums";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-variant-numeric: oldstyle-nums;",
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