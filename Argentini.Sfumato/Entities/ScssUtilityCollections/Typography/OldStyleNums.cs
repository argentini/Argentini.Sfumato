namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class OldStyleNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "oldstyle-nums";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.OldStyleNumsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.OldStyleNumsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}