namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class TableAuto : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-auto";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "table-layout: auto;",
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