namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RingOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-offset";
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

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector,
            """
            --sf-ring-offset-color: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """, out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector,
                """
                --sf-ring-offset-width: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities

        if (ProcessColorModifierOptions(cssSelector,
                """
                --sf-ring-offset-color: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector,
                """
                --sf-ring-offset-color: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector,
                """
                --sf-ring-offset-width: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}