namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class BorderY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-y";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
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
                border-top-color: {value};
                border-bottom-color: {value};
                """, out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector,
                """
                border-top-color: {value};
                border-bottom-color: {value};
                """, out Result))
            return Result;
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector,
                """
                border-top-width: {value};
                border-bottom-width: {value};
                """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector,
                """
                border-top-color: {value};
                border-bottom-color: {value};
                """, out Result))
            return Result;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
                """
                border-top-width: {value};
                border-bottom-width: {value};
                """, out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector,
                """
                border-top-style: {value};
                border-bottom-style: {value};
                """, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}