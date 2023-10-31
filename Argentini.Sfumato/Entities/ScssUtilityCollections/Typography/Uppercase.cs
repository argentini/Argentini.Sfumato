namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Uppercase : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "uppercase";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-transform: uppercase;",
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