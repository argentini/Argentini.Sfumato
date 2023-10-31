namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

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
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
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