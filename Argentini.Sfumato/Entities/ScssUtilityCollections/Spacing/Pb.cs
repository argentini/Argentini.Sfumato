namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Spacing;

public class Pb : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "pb";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.PbStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }

    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.PbStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "padding-bottom: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "padding-bottom: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}