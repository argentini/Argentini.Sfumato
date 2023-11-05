namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Sizing;

public class MinH : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "min-h";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.MinHStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.MinHStaticUtilities, cssSelector, out Result))
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