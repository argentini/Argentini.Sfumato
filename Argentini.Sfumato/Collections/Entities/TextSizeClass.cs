namespace Argentini.Sfumato.Collections.Entities;

public sealed class TextSizeClass
{
    public TextSizeClass()
    {
        Classes.Add(new ScssClass("text-")
        {
            GlobalGrouping = GlobalGrouping,
            Value = "",
            ValueTypes = "length,percentage",
            Template = "font-size: {value};"
        });
        
        foreach (var size in SfumatoScss.TextSizes)
        {
            Classes.Add(new ScssClass($"text-{size.Key}/")
            {
                GlobalGrouping = GlobalGrouping,
                Value = "",
                ValueTypes = "length,percentage,number",
                Template = $"font-size: {size.Value}; line-height: {{value}};"
            });

            Classes.Add(new ScssClass($"text-{size.Key}")
            {
                GlobalGrouping = GlobalGrouping,
                Value = size.Value,
                Template = "font-size: {value};"
            });

            foreach (var leading in SfumatoScss.Leading)
            {
                Classes.Add(new ScssClass($"text-{size.Key}/{leading.Key}")
                {
                    GlobalGrouping = GlobalGrouping,
                    Value = size.Value,
                    Template = $"font-size: {{value}}; line-height: {leading.Value};"
                });
            }
        }
    }
 
    public string GlobalGrouping { get; set; } = string.Empty;
    
    // ReSharper disable once CollectionNeverQueried.Global
    public List<ScssClass> Classes { get; } = new ();
}