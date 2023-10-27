namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayContents : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "contents";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: contents;"
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