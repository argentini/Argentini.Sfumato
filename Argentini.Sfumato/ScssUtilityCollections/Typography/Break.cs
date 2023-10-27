namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Break : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["all"] = "word-break: break-all;",
        ["keep"] = "word-break: keep-all;",
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