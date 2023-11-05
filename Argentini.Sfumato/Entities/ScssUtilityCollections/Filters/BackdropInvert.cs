namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class BackdropInvert : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-invert";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BackdropInvertStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BackdropInvertStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("percentage", cssSelector, "backdrop-filter: invert({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}