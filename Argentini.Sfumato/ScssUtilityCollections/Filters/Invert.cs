namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Invert : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "invert";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "filter: invert(100%);",
        ["0"] = "filter: invert(0);"
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
        
        if (ProcessArbitraryValues("percentage", cssSelector, "filter: invert({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}