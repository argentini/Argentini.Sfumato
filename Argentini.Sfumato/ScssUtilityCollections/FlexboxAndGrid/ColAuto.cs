namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ColAuto : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "col-auto";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "grid-column: auto;",
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