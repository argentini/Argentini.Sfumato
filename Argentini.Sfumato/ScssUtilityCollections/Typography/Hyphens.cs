namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Hyphens : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hyphens";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "hyphens: none;",
        ["manual"] = "hyphens: manual;",
        ["auto"] = "hyphens: auto;",
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