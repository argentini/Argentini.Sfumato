namespace Argentini.Sfumato.Entities.ScssUtilityCollections.TransitionsAndAnimations;

public class SkewX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "skew-x";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.SkewXStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.SkewXStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("angle", cssSelector, "transform: skewX({value});", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}