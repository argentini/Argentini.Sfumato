namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class SkewX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "skew-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: skewX(0deg);",
        ["1"] = "transform: skewX(1deg);",
        ["2"] = "transform: skewX(2deg);",
        ["3"] = "transform: skewX(3deg);",
        ["6"] = "transform: skewX(6deg);",
        ["12"] = "transform: skewX(12deg);",
        ["45"] = "transform: skewX(45deg);",
        ["90"] = "transform: skewX(90deg);",
        ["180"] = "transform: skewX(180deg);"
    };
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("angle", cssSelector, "transform: skewX({value});", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}