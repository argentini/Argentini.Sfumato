namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class StackedFractions : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "stacked-fractions";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.StackedFractionsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.StackedFractionsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}