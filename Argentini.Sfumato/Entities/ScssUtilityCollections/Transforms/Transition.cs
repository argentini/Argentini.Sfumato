namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Transforms;

public class Transition : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "transition";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               transition-property: color, background-color, border-color, text-decoration-color, fill, stroke, opacity, box-shadow, transform, filter, backdrop-filter;
               transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
               transition-duration: 150ms;
               """,
        ["none"] = """
                   transition-property: none;
                   transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                   transition-duration: 150ms;
                   """,
        ["all"] = """
                  transition-property: all;
                  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                  transition-duration: 150ms;
                  """,
        ["colors"] = """
                     transition-property: color, background-color, border-color, text-decoration-color, fill, stroke;
                     transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                     transition-duration: 150ms;
                     """,
        ["opacity"] = """
                      transition-property: opacity;
                      transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                      transition-duration: 150ms;
                      """,
        ["shadow"] = """
                     transition-property: box-shadow;
                     transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                     transition-duration: 150ms;
                     """,
        ["transform"] = """
                        transition-property: transform;
                        transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
                        transition-duration: 150ms;
                        """
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
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "transition: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}