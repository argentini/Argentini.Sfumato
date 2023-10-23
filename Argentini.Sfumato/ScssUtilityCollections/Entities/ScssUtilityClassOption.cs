namespace Argentini.Sfumato.ScssUtilityCollections.Entities;

public sealed class ScssUtilityClassOption
{
    public string Selector { get; set; } = string.Empty;
    public string[] ArbitraryValueTypes { get; set; } = Array.Empty<string>();
    private string _value = string.Empty;
    public string Value
    {
        get => _value;
        set
        {
            _value = value;
            BuildScssMarkup();
        }
    }
    
    private string _scssTemplate = string.Empty;
    public string ScssTemplate
    {
        get => _scssTemplate;
        set
        {
            _scssTemplate = value;
            BuildScssMarkup();
        }
    }
    
    public string ScssMarkup { get; private set; } = string.Empty;

    /// <summary>
    /// Use the ScssTemplate to generate a value for ScssMarkup,
    /// injecting the value of the option.
    /// </summary>
    /// <param name="important"></param>
    public void BuildScssMarkup()
    {
        ScssMarkup = ScssTemplate.Replace("{value}", Value);
    }
}