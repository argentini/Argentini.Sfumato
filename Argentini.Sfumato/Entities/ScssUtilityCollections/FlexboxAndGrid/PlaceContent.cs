namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceContent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-content";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.PlaceContentStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.PlaceContentStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}