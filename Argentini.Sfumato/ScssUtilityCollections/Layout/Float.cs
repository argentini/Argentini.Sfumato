namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Float : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "float";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["right"] = "float: right;",
        ["left"] = "float: left;",
        ["none"] = "float: none;",
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