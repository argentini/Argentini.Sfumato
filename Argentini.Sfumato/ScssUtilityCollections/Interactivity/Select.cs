namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Select : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "select";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "user-select: none;",
        ["text"] = "user-select: text;",
        ["all"] = "user-select: all;",
        ["auto"] = "user-select: auto;"
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