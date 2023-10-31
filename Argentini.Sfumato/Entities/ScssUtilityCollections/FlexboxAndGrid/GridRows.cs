namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class GridRows : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grid-rows";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "grid-template-rows: none;",
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.FlexboxAndGridWholeNumberOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FlexboxAndGridWholeNumberOptions, cssSelector, "grid-template-rows: repeat({value}, minmax(0, 1fr));", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "grid-template-rows: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}