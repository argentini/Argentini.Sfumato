namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropGrayscale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-grayscale";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "backdrop-filter: grayscale(100%);",
        ["0"] = "backdrop-filter: grayscale(0);"
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
        
        if (ProcessArbitraryValues("percentage", cssSelector, "backdrop-filter: grayscale({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}