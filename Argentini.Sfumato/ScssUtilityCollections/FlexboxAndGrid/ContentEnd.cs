namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentEnd : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-end";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: flex-end;",
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