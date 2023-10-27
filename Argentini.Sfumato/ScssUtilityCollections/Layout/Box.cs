namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Box : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "box";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["border"] = "box-sizing: border-box;",
        ["content"] = "box-sizing: content-box;",
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