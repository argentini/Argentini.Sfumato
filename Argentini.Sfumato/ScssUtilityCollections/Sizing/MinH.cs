namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class MinH : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "min-h";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "min-height: 0px;",
        ["full"] = "min-height: 100%;",
        ["screen"] = "min-height: 100vh;",
        ["min"] = "min-height: min-content;",
        ["max"] = "min-height: max-content;",
        ["fit"] = "min-height: fit-content;"
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
        
        if (ProcessArbitraryValues("integer,length,percentage", cssSelector, "min-height: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}