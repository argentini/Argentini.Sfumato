namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class Flex : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "flex";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: flex;",

        ["row"] = "flex-direction: row;",
        ["row-reverse"] = "flex-direction: row-reverse;",
        ["col"] = "flex-direction: column;",
        ["col-reverse"] = "flex-direction: column-reverse;",
        
        ["wrap"] = "flex-wrap: wrap;",
        ["wrap-reverse"] = "flex-wrap: wrap-reverse;",
        ["nowrap"] = "flex-wrap: nowrap;",
        
        ["1"] = "flex: 1 1 0%;",
        ["auto"] = "flex: 1 1 auto;",
        ["initial"] = "flex: 0 1 auto;",
        ["none"] = "flex: none;",
        
        ["grow"] = "flex-grow: 1;",
        ["grow-0"] = "flex-grow: 0;",
        ["shrink"] = "flex-shrink: 1;",
        ["shrink-0"] = "flex-shrink: 0;"
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "flex: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}