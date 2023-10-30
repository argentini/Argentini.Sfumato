namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class Shrink : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "shrink";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);
        
        await Task.CompletedTask;
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer,number", cssSelector, "flex-shrink: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}