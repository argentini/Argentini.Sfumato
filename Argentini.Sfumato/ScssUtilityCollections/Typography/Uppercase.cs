namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Uppercase : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "uppercase";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-transform: uppercase;",
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