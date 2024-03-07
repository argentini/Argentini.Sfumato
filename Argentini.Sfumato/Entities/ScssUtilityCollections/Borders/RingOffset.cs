namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class RingOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-offset";
    public override string Category => "ring";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.BorderWidthOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Modifier Utilities

        if (ProcessColorModifierOptions(cssSelector,
                """
                --sf-ring-offset-color: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector,
            """
            --sf-ring-offset-color: {value};
            box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
            """, AppState, out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector,
                """
                --sf-ring-offset-width: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector,
                """
                --sf-ring-offset-color: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector,
                """
                --sf-ring-offset-width: {value};
                box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                """, AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}