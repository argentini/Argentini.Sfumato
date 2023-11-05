namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Tracking : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "tracking";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.TrackingStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.TrackingStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length", cssSelector, "letter-spacing: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}