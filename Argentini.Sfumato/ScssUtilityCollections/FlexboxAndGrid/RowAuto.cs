namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class RowAuto : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-auto";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "grid-row: auto;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}