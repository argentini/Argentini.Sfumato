namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class BoxDecoration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "box-decoration";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["clone"] = "box-decoration-break: clone;",
        ["slice"] = "box-decoration-break: slice;",
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