namespace Argentini.Sfumato.Collections;

public sealed class ElasticContainer
{
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["elastic-container"] = new ScssClass
        {
            Template = @"width: 100%;

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
"
        }
    };
}