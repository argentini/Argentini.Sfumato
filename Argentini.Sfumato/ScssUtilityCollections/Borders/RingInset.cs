namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RingInset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-inset";
    public override string Category => "ring";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "--sf-ring-inset: inset;",
    }; 
    
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