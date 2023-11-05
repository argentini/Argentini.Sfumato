namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Z : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "z";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ZStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ZStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "z-index: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}