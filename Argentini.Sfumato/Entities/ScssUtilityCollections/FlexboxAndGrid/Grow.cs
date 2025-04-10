namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class Grow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grow";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        
        await AddToIndexAsync(appState.FlexGrowStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.FlexGrowStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer,number", cssSelector, "flex-grow: {value};", AppState, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}