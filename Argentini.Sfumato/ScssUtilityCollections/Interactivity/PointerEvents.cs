namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class PointerEvents : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "pointer-events";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "pointer-events: none;",
        ["auto"] = "pointer-events: auto;",
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