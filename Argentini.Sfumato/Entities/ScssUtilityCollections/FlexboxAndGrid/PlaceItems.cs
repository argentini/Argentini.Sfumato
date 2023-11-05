namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceItems : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-items";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.PlaceItemsStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.PlaceItemsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}