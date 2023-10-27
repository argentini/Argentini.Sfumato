namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentEvenly : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-evenly";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: space-evenly;",
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