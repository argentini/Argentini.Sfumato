namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceSelf : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-self";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.PlaceSelfStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.PlaceSelfStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}