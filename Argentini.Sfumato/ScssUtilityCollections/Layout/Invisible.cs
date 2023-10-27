namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Invisible : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "invisible";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "visibility: hidden;",
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