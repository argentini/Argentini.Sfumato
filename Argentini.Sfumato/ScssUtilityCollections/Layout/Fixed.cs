namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Fixed : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "fixed";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "position: fixed;",
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