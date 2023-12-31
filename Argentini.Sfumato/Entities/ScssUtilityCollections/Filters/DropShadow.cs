namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class DropShadow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "drop-shadow";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DropShadowStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DropShadowStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "filter: drop-shadow({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}