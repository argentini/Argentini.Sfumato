namespace Argentini.Sfumato.Entities.ScssUtilityCollections.TransitionsAndAnimations;

public class Origin : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "origin";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.OriginStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.OriginStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "transform-origin: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}