namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Visible : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "visible";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "visibility: visible;",
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