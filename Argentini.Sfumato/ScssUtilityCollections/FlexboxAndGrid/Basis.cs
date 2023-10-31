namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Basis : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "basis";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["full"] = "100%"
    };

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
        
        await AddToIndexAsync(appState.FlexRemUnitOptions);

        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector, "flex-basis: {value};", out Result))
            return Result;
        
        #endregion

        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FlexRemUnitOptions, cssSelector, "flex-basis: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "flex-basis: {value};", out Result))
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