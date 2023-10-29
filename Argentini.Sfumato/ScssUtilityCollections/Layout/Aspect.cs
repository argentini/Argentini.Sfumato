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
        
        if (ProcessArbitraryValues("ratio", cssSelector, "aspect-ratio: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}