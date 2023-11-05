namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class UnderlineOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "underline-offset";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.UnderlineOffsetStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.UnderlineOffsetStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("length", cssSelector, "text-underline-offset: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}