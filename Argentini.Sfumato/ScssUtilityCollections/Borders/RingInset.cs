namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RingInset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-inset";
    public override string Category => "ring";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "--sf-ring-inset: inset;",
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