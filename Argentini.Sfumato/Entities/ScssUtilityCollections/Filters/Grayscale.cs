namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class Grayscale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grayscale";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.GrayscaleStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.GrayscaleStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("percentage", cssSelector, "filter: grayscale({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}