namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Justify : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "justify";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["normal"] = "justify-content: normal;",
        ["start"] = "justify-content: flex-start;",
        ["end"] = "justify-content: flex-end;",
        ["center"] = "justify-content: center;",
        ["between"] = "justify-content: space-between;",
        ["around"] = "justify-content: space-around;",
        ["evenly"] = "justify-content: space-evenly;",
        ["stretch"] = "justify-content: stretch;"
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