namespace Argentini.Sfumato.Entities.ScssUtilityCollections.TransitionsAndAnimations;

public class ScaleY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scale-y";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ScaleYStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ScaleYStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("number", cssSelector, "transform: scaleY({value});", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}