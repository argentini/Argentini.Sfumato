namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Absolute : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "absolute";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "position: absolute;",
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