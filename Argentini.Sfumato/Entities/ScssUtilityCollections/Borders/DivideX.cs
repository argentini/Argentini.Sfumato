namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class DivideX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide-x";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DivideXStaticUtilities);
        await AddToIndexAsync(appState.DivideWidthOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DivideXStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.DivideWidthOptions, cssSelector,
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """, AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}