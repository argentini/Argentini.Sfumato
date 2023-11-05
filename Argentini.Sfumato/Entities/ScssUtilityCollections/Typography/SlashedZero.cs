namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class SlashedZero : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "slashed-zero";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.SlashedZeroStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.SlashedZeroStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}