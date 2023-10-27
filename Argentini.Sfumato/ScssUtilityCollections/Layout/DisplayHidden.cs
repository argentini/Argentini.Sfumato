namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayHidden : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hidden";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: none;"
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