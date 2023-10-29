namespace Argentini.Sfumato.ScssUtilityCollections.Effects;

public class BgBlend : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg-blend";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.BlendModeOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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