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
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}