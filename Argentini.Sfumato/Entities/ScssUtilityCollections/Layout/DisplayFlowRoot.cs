namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class DisplayFlowRoot : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "flow-root";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DisplayFlowRootStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DisplayFlowRootStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}