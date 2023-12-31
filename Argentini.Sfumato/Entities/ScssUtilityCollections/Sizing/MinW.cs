namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Sizing;

public class MinW : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "min-w";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.MinWStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.MinWStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer,length,percentage", cssSelector, "min-width: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}