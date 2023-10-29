namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

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