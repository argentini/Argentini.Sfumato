namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class BreakWords : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break-words";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "overflow-wrap: break-word;",
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