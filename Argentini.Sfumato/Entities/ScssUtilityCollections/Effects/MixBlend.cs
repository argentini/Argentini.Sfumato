namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Effects;

public class MixBlend : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "mix-blend";

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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BlendModeOptions, cssSelector, "mix-blend-mode: {value};", AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}