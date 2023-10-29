namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Appearance : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "appearance";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "appearance: none;",
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
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}