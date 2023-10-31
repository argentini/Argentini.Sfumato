namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Container : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "container";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               width: 100%;
               margin-left: auto;
               margin-right: auto;
               
               @include sf-media($from: phab) {
                  max-width: $phab-breakpoint;
               }

               @include sf-media($from: tabp) {
                  max-width: $tabp-breakpoint;
               }

               @include sf-media($from: tabl) {
                  max-width: $tabl-breakpoint;
               }

               @include sf-media($from: note) {
                  max-width: $note-breakpoint;
               }

               @include sf-media($from: desk) {
                  max-width: $desk-breakpoint;
               }

               @include sf-media($from: elas) {
                  max-width: $elas-breakpoint;
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

        return string.Empty;
    }
}