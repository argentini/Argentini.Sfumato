namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Scroll : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "scroll-behavior: auto;",
        ["smooth"] = "scroll-behavior: smooth;",
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