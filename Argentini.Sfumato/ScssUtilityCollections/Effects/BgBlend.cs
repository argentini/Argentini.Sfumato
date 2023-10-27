namespace Argentini.Sfumato.ScssUtilityCollections.Effects;

public class BgBlend : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg-blend";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. mix-blend-normal)
        if (cssSelector.AppState.BlendModeOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"background-blend-mode: {value};";

        #endregion

        return string.Empty;
    }
}