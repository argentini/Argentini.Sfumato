namespace Argentini.Sfumato.ScssUtilityCollections.Effects;

public class MixBlend : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "mix-blend";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.BlendModeOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. mix-blend-normal)
        if (cssSelector.AppState.BlendModeOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"mix-blend-mode: {value};";

        #endregion

        return string.Empty;
    }
}