namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class LiningNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "lining-nums";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.LiningNumsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.LiningNumsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}