namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Lowercase : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "lowercase";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-transform: lowercase;",
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