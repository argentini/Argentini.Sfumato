namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class RowStart : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-start";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "grid-row-start: auto;",
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

        if (ProcessDictionaryOptions(cssSelector.AppState.FlexboxAndGridWholeNumberOptions, cssSelector, "grid-row-start: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "grid-row-start: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}