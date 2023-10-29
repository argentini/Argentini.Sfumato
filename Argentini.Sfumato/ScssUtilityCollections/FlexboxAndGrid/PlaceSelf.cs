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
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}