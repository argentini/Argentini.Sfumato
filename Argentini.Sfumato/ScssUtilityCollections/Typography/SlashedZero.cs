namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class SlashedZero : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "slashed-zero";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-variant-numeric: slashed-zero;",
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