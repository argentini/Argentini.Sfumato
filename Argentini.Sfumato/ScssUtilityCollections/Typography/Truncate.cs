namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Truncate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "truncate";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               overflow: hidden;
               text-overflow: ellipsis;
               white-space: nowrap;
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