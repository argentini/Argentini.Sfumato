namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Sizing;

public class MaxW : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "max-w";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "max-width: 0px;",
        ["none"] = "max-width: none;",
        ["xs"] = "max-width: 20rem;",
        ["sm"] = "max-width: 24rem;",
        ["md"] = "max-width: 28rem;",
        ["lg"] = "max-width: 32rem;",
        ["xl"] = "max-width: 36rem;",
        ["2xl"] = "max-width: 42rem;",
        ["3xl"] = "max-width: 48rem;",
        ["4xl"] = "max-width: 56rem;",
        ["5xl"] = "max-width: 64rem;",
        ["6xl"] = "max-width: 72rem;",
        ["7xl"] = "max-width: 80rem;",
        ["full"] = "max-width: 100%;",
        ["min"] = "max-width: min-content;",
        ["max"] = "max-width: max-content;",
        ["fit"] = "max-width: fit-content;",
        ["prose"] = "max-width: 65ch;",
        ["screen-zero"] = "max-width: calc(#{$phab-breakpoint} - 1px);",
        ["screen-phab"] = "max-width: #{$tabp-breakpoint};",
        ["screen-tabp"] = "max-width: #{$tabl-breakpoint};",
        ["screen-tabl"] = "max-width: #{$note-breakpoint};",
        ["screen-note"] = "max-width: #{$desk-breakpoint};",
        ["screen-desk"] = "max-width: #{$elas-breakpoint};",
        ["screen-elas"] = "max-width: #{$tabp-breakpoint};"
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
        
        if (ProcessArbitraryValues("integer,length,percentage", cssSelector, "max-width: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}