namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class GridFlow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grid-flow";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["row"] = "grid-auto-flow: row;",
        ["col"] = "grid-auto-flow: column;",
        ["dense"] = "grid-auto-flow: dense;",
        ["row-dense"] = "grid-auto-flow: row dense;",
        ["col-dense"] = "grid-auto-flow: column dense;"
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