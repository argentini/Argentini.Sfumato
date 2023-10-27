namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Italic : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "italic";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-style: italic;",
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