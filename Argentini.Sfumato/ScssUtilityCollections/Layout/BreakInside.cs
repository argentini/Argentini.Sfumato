namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class BreakInside : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break-inside";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "break-inside: auto;",
        ["avoid"] = "break-inside: avoid;",
        ["avoid-page"] = "break-inside: avoid-page;",
        ["avoid-column"] = "break-inside: column;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        return string.Empty;
    }
}