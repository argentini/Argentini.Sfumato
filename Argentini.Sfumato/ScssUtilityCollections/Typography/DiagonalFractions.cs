namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class DiagonalFractions : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "diagonal-fractions";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-variant-numeric: diagonal-fractions;",
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