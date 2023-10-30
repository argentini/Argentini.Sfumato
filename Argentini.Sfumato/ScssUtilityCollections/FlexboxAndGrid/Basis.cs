namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Basis : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "basis";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.FlexRemUnitOptions);

        await AddToIndexAsync(appState.FractionOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FlexRemUnitOptions, cssSelector, "flex-basis: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.FractionOptions, cssSelector, "flex-basis: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "flex-basis: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}