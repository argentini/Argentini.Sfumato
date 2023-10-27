namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class JustifySelf : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "justify-self";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "justify-self: auto;",
        ["start"] = "justify-self: start;",
        ["end"] = "justify-self: end;",
        ["center"] = "justify-self: center;",
        ["stretch"] = "justify-self: stretch;"
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