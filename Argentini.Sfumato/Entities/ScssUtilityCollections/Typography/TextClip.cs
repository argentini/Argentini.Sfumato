namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class TextClip : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text-clip";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.TextClipStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.TextClipStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}