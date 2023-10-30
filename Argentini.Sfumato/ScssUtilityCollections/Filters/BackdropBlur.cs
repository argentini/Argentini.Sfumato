namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropBlur : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-blur";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.FilterSizeOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FilterSizeOptions, cssSelector, "backdrop-filter: blur({value});", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length", cssSelector, "backdrop-filter: blur({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}