namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Content : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ContentStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ContentStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("string", cssSelector, "content: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}