namespace Argentini.Sfumato.Collections;

public sealed class AspectRatio
{
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["aspect-"] = new ScssClass
        {
            Value = "",
            ValueTypes = "ratio",
            Template = "aspect-ratio: {value};"
        },
        ["aspect-auto"] = new ScssClass
        {
            Value = "auto",
            Template = "aspect-ratio: {value};"
        },
        ["aspect-square"] = new ScssClass
        {
            Value = "1/1",
            Template = "aspect-ratio: {value};"
        },
        ["aspect-video"] = new ScssClass
        {
            Value = "16/9",
            Template = "aspect-ratio: {value};"
        },
        ["aspect-screen"] = new ScssClass
        {
            Value = "4/3",
            Template = "aspect-ratio: {value};"
        }
    };
}