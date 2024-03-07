namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class OrderNegative : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "-order";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.FlexboxAndGridNegativeWholeNumberOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FlexboxAndGridNegativeWholeNumberOptions, cssSelector, "order: {value};", AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "order: {value};", AppState, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}