namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Static : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "static";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "position: static;",
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