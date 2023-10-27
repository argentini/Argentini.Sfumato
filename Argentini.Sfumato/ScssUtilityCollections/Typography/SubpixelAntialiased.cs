namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class SubpixelAntialiased : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "subpixel-antialiased";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               -webkit-font-smoothing: auto;
               -moz-osx-font-smoothing: auto;
               """,
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