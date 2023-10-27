namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class TextEllipsis : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text-ellipsis";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-overflow: ellipsis;",
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