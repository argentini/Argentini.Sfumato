namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Sizing;

public class MaxH : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "max-h";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.MaxHStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.MaxHStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer,length,percentage", cssSelector, "max-height: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}