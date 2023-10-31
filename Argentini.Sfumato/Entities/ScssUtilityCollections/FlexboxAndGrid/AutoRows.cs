namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class AutoRows : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "auto-rows";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "grid-auto-rows: auto;",
        ["min"] = "grid-auto-rows: min-content;",
        ["max"] = "grid-auto-rows: max-content;",
        ["fr"] = "grid-auto-rows: minmax(0, 1fr);"
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
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "grid-auto-rows: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}