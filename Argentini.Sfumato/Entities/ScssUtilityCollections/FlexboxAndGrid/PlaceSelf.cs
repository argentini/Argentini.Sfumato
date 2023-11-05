namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceSelf : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-self";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.PlaceSelfStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.PlaceSelfStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}