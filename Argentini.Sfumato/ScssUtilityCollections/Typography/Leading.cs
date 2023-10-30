namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Leading : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "leading";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.LeadingOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LeadingOptions, cssSelector, "line-height: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,integer,number", cssSelector, "line-height: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}