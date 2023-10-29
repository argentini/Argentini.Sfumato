namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Ring : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring";
    public override string Category => "ring";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.BorderWidthOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "--sf-ring-color: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);", out Result))
            return Result;
        
        #endregion

        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "--sf-ring-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "--sf-ring-color: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "box-shadow: var(--sf-ring-inset) 0 0 0 calc({value} + var(--sf-ring-offset-width)) var(--sf-ring-color);", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}