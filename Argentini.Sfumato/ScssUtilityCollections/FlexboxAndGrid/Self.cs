namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Self : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "self";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "align-self: auto;",
        ["start"] = "align-self: flex-start;",
        ["end"] = "align-self: flex-end;",
        ["center"] = "align-self: center;",
        ["baseline"] = "align-self: baseline;",
        ["stretch"] = "align-self: stretch;"
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