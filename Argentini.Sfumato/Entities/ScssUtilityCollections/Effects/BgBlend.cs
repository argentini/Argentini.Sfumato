namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Effects;

public class BgBlend : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg-blend";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BlendModeOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BlendModeOptions, cssSelector, "background-blend-mode: {value};", AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}