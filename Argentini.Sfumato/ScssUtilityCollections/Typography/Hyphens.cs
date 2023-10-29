namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Hyphens : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hyphens";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "hyphens: none;",
        ["manual"] = "hyphens: manual;",
        ["auto"] = "hyphens: auto;",
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