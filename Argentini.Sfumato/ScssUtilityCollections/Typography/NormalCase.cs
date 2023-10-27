namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class NormalCase : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "normal-case";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-transform: none;",
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