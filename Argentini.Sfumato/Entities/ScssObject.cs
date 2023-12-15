namespace Argentini.Sfumato.Entities;

public sealed class ScssObject
{
    public string Prefix { get; set; } = string.Empty; // e.g. dark

    private string _variantsPathValue = string.Empty;
    public string VariantsPath // e.g. dark:sm:
    {
        get => _variantsPathValue;

        set
        {
            _variantsPathValue = value;
            Level = 0;

            if (string.IsNullOrEmpty(_variantsPathValue))
                return;

            var segmentCount = _variantsPathValue.Split(':', StringSplitOptions.RemoveEmptyEntries).Length;

            Level = segmentCount;
        }
    }
    public int Level { get; set; }
    public List<CssSelector> Classes { get; set; } = new();
    public List<ScssObject> Nodes { get; set; } = new();
}
