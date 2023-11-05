namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class Divide : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DivideStaticUtilities);
        
        await AddToIndexAsync(appState.ColorOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DivideStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector,
                """
                & > * + * {
                    border-color: {value};
                }
                """, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, 
                """
                & > * + * {
                    border-color: {value};
                }
                """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector,
                """
                & > * + * {
                    border-color: {value};
                }
                """, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}