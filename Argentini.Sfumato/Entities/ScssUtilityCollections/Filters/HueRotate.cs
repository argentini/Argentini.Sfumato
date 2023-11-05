namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class HueRotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hue-rotate";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.HueRotateStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.HueRotateStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("angle", cssSelector, "filter: hue-rotate({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}