namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceItems : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-items";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "place-items: start;",
        ["end"] = "place-items: end;",
        ["center"] = "place-items: center;",
        ["baseline"] = "place-items: baseline;",
        ["stretch"] = "place-items: stretch;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}