namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class DiagonalFractions : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "diagonal-fractions";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DiagonalFractionsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DiagonalFractionsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}