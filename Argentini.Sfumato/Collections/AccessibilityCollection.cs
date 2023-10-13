using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssUtilityBaseClass ScreenReaders { get; } = new()
    {
        Selector = "sr-only",
        Template = """
                   position: absolute;
                   width: 1px;
                   height: 1px;
                   padding: 0;
                   margin: -1px;
                   overflow: hidden;
                   clip: rect(0, 0, 0, 0);
                   white-space: nowrap;
                   border-width: 0;
                   """
    };
    
    public ScssUtilityBaseClass NotScreenReaders { get; } = new()
    {
        Selector = "not-sr-only",
        Template = """
                   position: static;
                   width: auto;
                   height: auto;
                   padding: 0;
                   margin: 0;
                   overflow: visible;
                   clip: auto;
                   white-space: normal;
                   """
    };
}