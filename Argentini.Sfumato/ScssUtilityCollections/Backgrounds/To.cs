namespace Argentini.Sfumato.ScssUtilityCollections.Backgrounds;

public class To : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "to";
    public override string Category => "gradients";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.PercentageOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "--sf-gradient-to: {value} var(--sf-gradient-to-position);", out Result))
            return Result;
        
        if (ProcessDictionaryOptions(cssSelector.AppState.PercentageOptions, cssSelector, "--sf-gradient-to-position: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "--sf-gradient-to: {value} var(--sf-gradient-to-position);", out Result))
            return Result;

        if (ProcessArbitraryValues("percentage", cssSelector, "--sf-gradient-to-position: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}