namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Select : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "select";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "user-select: none;",
        ["text"] = "user-select: text;",
        ["all"] = "user-select: all;",
        ["auto"] = "user-select: auto;"
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