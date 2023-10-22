namespace Argentini.Sfumato.Entities;

public sealed class ScssNode
{
    public string Prefix { get; set; } = string.Empty; // e.g. dark

    private string _prefixPathValue = string.Empty;
    public string PrefixPath // e.g. dark:tabp:
    {
        get => _prefixPathValue;

        set
        {
            _prefixPathValue = value;
            Level = 0;

            if (string.IsNullOrEmpty(_prefixPathValue))
                return;

            var segmentCount = _prefixPathValue.Split(':', StringSplitOptions.RemoveEmptyEntries).Length;

            Level = segmentCount;
        }
    }
    public int Level { get; set; }
    public List<ScssClass> Classes { get; set; } = new();
    public List<ScssNode> Nodes { get; set; } = new();
}
