namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Transforms;

public class Animate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "animate";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "animation: none;",
        ["spin"] = """
                   animation: spin 1s linear infinite;

                   @keyframes spin {
                      from {
                          transform: rotate(0deg);
                       }
                       to {
                          transform: rotate(360deg);
                       }
                   }
                   """,
        ["ping"] = """
                   animation: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite;
                   
                   @keyframes pulse {
                       0%, 100% {
                           opacity: 1;
                       }
                       50% {
                           opacity: .5;
                       }
                   }
                   """,
        ["pulse"] = """
                    animation: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite;
                   
                    @keyframes ping {
                        75%, 100% {
                            transform: scale(2);
                            opacity: 0;
                        }
                    }
                    """,
        ["bounce"] = """
                     animation: bounce 1s infinite;
                   
                     @keyframes bounce {
                         0%, 100% {
                             transform: translateY(-25%);
                             animation-timing-function: cubic-bezier(0.8, 0, 1, 1);
                         }
                         50% {
                             transform: translateY(0);
                             animation-timing-function: cubic-bezier(0, 0, 0.2, 1);
                         }
                     }
                     """,
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
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "animation: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}