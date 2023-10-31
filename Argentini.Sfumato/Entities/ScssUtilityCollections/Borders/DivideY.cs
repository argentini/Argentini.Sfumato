namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class DivideY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide-y";
    
    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["reverse"] = "--sf-divide-y-reverse: 1;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
        
        await AddToIndexAsync(appState.DivideWidthOptions);
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.DivideWidthOptions, cssSelector,
                """
                & > * + * {
                    border-top-width: 0px;
                    border-bottom-width: {value};
                }
                """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
                """
                & > * + * {
                   border-top-width: 0px;
                   border-bottom-width: {value};
                }
                """, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}