namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class JustifyItems : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "justify-items";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "justify-items: start;",
        ["end"] = "justify-items: end;",
        ["center"] = "justify-items: center;",
        ["stretch"] = "justify-items: stretch;"
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