namespace Argentini.Sfumato.Entities.ScssUtilityCollections.TransitionsAndAnimations;

public class Rotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "rotate";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.RotateStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.RotateStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("angle", cssSelector, "transform: rotate({value});", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}