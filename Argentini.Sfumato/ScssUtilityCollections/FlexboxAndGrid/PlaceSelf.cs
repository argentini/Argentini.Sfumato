namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class PlaceSelf : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "place-self";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "place-self: auto;",
        ["start"] = "place-self: start;",
        ["end"] = "place-self: end;",
        ["center"] = "place-self: center;",
        ["stretch"] = "place-self: stretch;"
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