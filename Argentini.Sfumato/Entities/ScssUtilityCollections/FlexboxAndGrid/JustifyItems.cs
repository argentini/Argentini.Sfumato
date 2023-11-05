namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class JustifyItems : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "justify-items";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.JustifyItemsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.JustifyItemsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}