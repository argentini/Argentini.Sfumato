namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class Flex : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "flex";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.FlexStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.FlexStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "flex: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}