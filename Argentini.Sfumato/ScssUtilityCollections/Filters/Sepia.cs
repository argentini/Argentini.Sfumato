namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Sepia : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "sepia";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "filter: sepia(100%);",
        ["0"] = "filter: sepia(0);"
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
        
        if (ProcessArbitraryValues("percentage", cssSelector, "filter: sepia({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}