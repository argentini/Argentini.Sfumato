namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Capitalize : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "capitalize";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-transform: capitalize;",
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