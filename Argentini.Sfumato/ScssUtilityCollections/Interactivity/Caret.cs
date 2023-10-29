namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Caret : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "caret";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "caret-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities

        if (ProcessColorModifierOptions(cssSelector, "caret-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "caret-color: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}