namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Aspect : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "aspect";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "aspect-ratio: auto;",
        ["square"] = "aspect-ratio: 1/1;",
        ["video"] = "aspect-ratio: 16/9;",
        ["screen"] = "aspect-ratio: 4/3;"
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "ratio")
            return $"aspect-ratio: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}