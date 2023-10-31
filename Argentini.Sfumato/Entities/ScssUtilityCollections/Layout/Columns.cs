namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Columns : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "columns";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "columns: auto;",
        ["3xs"] = "columns: 16rem;",
        ["2xs"] = "columns: 18rem;",
        ["xs"] = "columns: 20rem;",
        ["sm"] = "columns: 24rem;",
        ["md"] = "columns: 28rem;",
        ["lg"] = "columns: 32rem;",
        ["xl"] = "columns: 36rem;",
        ["2xl"] = "columns: 42rem;",
        ["3xl"] = "columns: 48rem;",
        ["4xl"] = "columns: 56rem;",
        ["5xl"] = "columns: 64rem;",
        ["6xl"] = "columns: 72rem;",
        ["7xl"] = "columns: 80rem;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutWholeNumberOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutWholeNumberOptions, cssSelector, "columns: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer,length,percentage", cssSelector, "columns: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}