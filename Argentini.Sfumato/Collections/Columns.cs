namespace Argentini.Sfumato.Collections;

public sealed class Columns
{
    public Dictionary<string, string> ColumnSizes { get; } = new()
    {
        ["auto"] = "auto",
        ["3xs"] = "16rem",
        ["2xs"] = "18rem",
        ["xs"] = "20rem",
        ["sm"] = "24rem",
        ["md"] = "28rem",
        ["lg"] = "32rem",
        ["xl"] = "36rem",
        ["2xl"] = "42rem",
        ["3xl"] = "48rem",
        ["4xl"] = "56rem",
        ["5xl"] = "64rem",
        ["6xl"] = "72rem",
        ["7xl"] = "80rem"
    };
    
    public Columns()
    {
        for (var x = 1; x < 13; x++)
        {
            Classes.Add($"columns-{x}", new ScssClass
            {
                Value = "1",
                Template = $"columns: {x};"
            });
        }
        
        foreach (var colSize in ColumnSizes)
        {
            Classes.Add($"columns-{colSize.Key}", new ScssClass
            {
                Value = colSize.Value,
                Template = "columns: {value};"
            });
        }
    }
    
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["columns-"] = new ScssClass
        {
            Value = "",
            ValueTypes = "length,percentage,integer",
            Template = "columns: {value};"
        }
    };
}