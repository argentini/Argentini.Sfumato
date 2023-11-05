namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class GridFlow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grid-flow";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.GridFlowStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.GridFlowStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}