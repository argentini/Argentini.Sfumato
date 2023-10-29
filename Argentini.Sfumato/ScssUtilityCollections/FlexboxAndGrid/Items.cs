namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Items : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "items";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["start"] = "align-items: flex-start;",
        ["end"] = "align-items: flex-end;",
        ["center"] = "align-items: center;",
        ["baseline"] = "align-items: baseline;",
        ["stretch"] = "align-items: stretch;"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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