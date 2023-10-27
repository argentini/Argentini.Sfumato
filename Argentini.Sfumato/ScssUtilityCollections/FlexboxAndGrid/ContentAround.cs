namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentAround : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-around";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: space-around;",
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