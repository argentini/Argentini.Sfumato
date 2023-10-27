namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Relative : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "relative";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "position: relative;",
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