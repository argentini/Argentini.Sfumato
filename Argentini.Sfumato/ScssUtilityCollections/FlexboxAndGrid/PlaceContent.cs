namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceContent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-content";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "place-content: start;",
        ["end"] = "place-content: end;",
        ["center"] = "place-content: center;",
        ["between"] = "place-content: space-between;",
        ["around"] = "place-content: space-around;",
        ["evenly"] = "place-content: space-evenly;",
        ["baseline"] = "place-content: baseline;",
        ["stretch"] = "place-content: stretch;"
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