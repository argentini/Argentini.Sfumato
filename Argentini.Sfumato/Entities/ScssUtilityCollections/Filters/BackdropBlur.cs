namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class BackdropBlur : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-blur";
    public override string Category => "backdrop";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 0;

        await AddToIndexAsync(appState.FilterSizeOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FilterSizeOptions, cssSelector, "--sf-backdrop-blur: blur({value});", AppState, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length", cssSelector, "--sf-backdrop-blur: blur({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}