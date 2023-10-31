namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class UnderlineOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "underline-offset";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "text-underline-offset: auto;",
        ["0"] = "text-underline-offset: 0px;",
        ["1"] = "text-underline-offset: 1px;",
        ["2"] = $"text-underline-offset: {2.PxToRem()};",
        ["4"] = $"text-underline-offset: {4.PxToRem()};",
        ["8"] = $"text-underline-offset: {8.PxToRem()};"
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("length", cssSelector, "text-underline-offset: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}