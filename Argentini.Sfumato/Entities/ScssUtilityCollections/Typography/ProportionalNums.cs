namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class ProportionalNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "proportional-nums";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ProportionalNumsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ProportionalNumsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}