namespace Argentini.Sfumato.Collections;

public sealed class TextSizeClass
{
    public Dictionary<string, string> TextSizes { get; } = new()
    {
        ["xs"] = "0.75rem",
        ["sm"] = "0.875rem",
        ["base"] = "1rem",
        ["lg"] = "1.125rem",
        ["xl"] = "1.25rem",
        ["2xl"] = "1.5rem",
        ["3xl"] = "1.875rem",
        ["4xl"] = "2.25rem",
        ["5xl"] = "3rem",
        ["6xl"] = "3.75rem",
        ["7xl"] = "4.5rem",
        ["8xl"] = "6rem",
        ["9xl"] = "8rem"
    };

    public Dictionary<string, string> Leading { get; } = new()
    {
        ["3"] = "0.75rem",
        ["4"] = "1rem",
        ["5"] = "1.25rem",
        ["6"] = "1.5rem",
        ["7"] = "1.75rem",
        ["8"] = "2rem",
        ["9"] = "2.25rem",
        ["10"] = "2.5rem",
        ["none"] = "1",
        ["tight"] = "1.25",
        ["snug"] = "1.375",
        ["normal"] = "1.5",
        ["relaxed"] = "1.625",
        ["loose"] = "2"
    };
    
    public TextSizeClass()
    {
        foreach (var size in TextSizes)
        {
            Classes.Add($"text-{size.Key}/", new ScssClass
            {
                Value = "",
                ValueTypes = "length,percentage,number",
                Template = $"font-size: {size.Value}; line-height: {{value}};"
            });

            Classes.Add($"text-{size.Key}", new ScssClass
            {
                Value = size.Value,
                Template = "font-size: {value};"
            });

            foreach (var leading in Leading)
            {
                Classes.Add($"text-{size.Key}/{leading.Key}", new ScssClass
                {
                    Value = size.Value,
                    Template = $"font-size: {{value}}; line-height: {leading.Value};"
                });
            }
        }
    }
    
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["text-"] = new ScssClass
        {
            Value = "",
            ValueTypes = "length,percentage",
            Template = "font-size: {value};"
        }
    };
}