namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class WillChange : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "will-change";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "will-change: auto;",
        ["scroll"] = "will-change: scroll-position;",
        ["contents"] = "will-change: contents;",
        ["transform"] = "will-change: transform;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        return string.Empty;
    }
}