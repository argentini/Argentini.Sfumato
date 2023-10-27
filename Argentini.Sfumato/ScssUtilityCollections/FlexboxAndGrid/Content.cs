namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Content : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["normal"] = "align-content: normal;",
        ["start"] = "align-content: flex-start;",
        ["end"] = "align-content: flex-end;",
        ["center"] = "align-content: center;",
        ["between"] = "align-content: space-between;",
        ["around"] = "align-content: space-around;",
        ["evenly"] = "align-content: space-evenly;",
        ["baseline"] = "align-content: baseline;",
        ["stretch"] = "align-content: stretch;"
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