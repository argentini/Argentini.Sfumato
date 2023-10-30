namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Text : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["left"] = "text-align: left;",
        ["center"] = "text-align: center;",
        ["right"] = "text-align: right;",
        ["justify"] = "text-align: justify;",
        ["start"] = "text-align: start;",
        ["end"] = "text-align: end;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.TextSizeOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "color: {value};", out Result))
            return Result;

        if (cssSelector.AppState.TextSizeOptions.TryGetValue(cssSelector.CoreSegment, out var fontSize))
            if (ProcessDictionaryOptions(cssSelector.AppState.TextSizeLeadingOptions, cssSelector,
                $$"""
                 font-size: {{fontSize}};
                 line-height: {value};
                 """, out Result))
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("color", cssSelector, "color: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "font-size: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}