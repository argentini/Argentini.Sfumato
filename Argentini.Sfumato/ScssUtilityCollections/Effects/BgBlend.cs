namespace Argentini.Sfumato.ScssUtilityCollections.Effects;

public class BgBlend : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg-blend";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BlendModeOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BlendModeOptions, cssSelector, "background-blend-mode: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}