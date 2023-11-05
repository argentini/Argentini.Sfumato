namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Indent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "indent";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.IndentStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.IndentStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "text-indent: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length", cssSelector, "text-indent: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}