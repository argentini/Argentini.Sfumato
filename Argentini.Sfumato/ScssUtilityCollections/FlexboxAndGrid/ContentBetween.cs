namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentBetween : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-between";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: space-between;",
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