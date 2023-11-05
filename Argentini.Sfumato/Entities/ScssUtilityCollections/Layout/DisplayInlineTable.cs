namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class DisplayInlineTable : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inline-table";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DisplayInlineTableStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DisplayInlineTableStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}