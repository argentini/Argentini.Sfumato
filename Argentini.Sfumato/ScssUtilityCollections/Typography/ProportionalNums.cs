namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class ProportionalNums : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "proportional-nums";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-variant-numeric: proportional-nums;",
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