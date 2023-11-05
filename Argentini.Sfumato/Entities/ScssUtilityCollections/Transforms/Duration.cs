namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Transforms;

public class Duration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "duration";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DurationStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DurationStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("time", cssSelector, "transition-duration: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}