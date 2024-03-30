namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Text : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.TextAlignStaticUtilities);
        await AddToIndexAsync(appState.TextWrapStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.TextSizeOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.TextAlignStaticUtilities, cssSelector, AppState, out Result))
            return Result;

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.TextWrapStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities

        if (ProcessTextSizeLeadingModifierOptions(cssSelector,
                """
                font-size: {fontSize};
                line-height: {value};
                """, out Result))
            return Result;
            
        if (ProcessColorModifierOptions(cssSelector, "color: {value};", out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "color: {value};", AppState, out Result))
            return Result;

        if (cssSelector.AppState.TextSizeOptions.TryGetValue(cssSelector.CoreSegment, out var fontSize))
            if (ProcessDictionaryOptions(cssSelector.AppState.TextSizeLeadingOptions, cssSelector,
                $$"""
                 font-size: {{fontSize}};
                 line-height: {value};
                 """, AppState, out Result))
                return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("color", cssSelector, "color: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "font-size: {value};", AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}