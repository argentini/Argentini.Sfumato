namespace Argentini.Sfumato.Entities;

public sealed class ScssNode
{
    public string Prefix { get; set; } = string.Empty; // e.g. dark

    public string PrefixPathValue = string.Empty;
    public string PrefixPath // e.g. dark:tabp:
    {
        get => PrefixPathValue;

        set
        {
            PrefixPathValue = value;
            Level = 0;

            if (string.IsNullOrEmpty(PrefixPathValue))
                return;

            var segmentCount = PrefixPathValue.Split(':', StringSplitOptions.RemoveEmptyEntries).Length;

            Level = segmentCount;
        }
    }
    public int Level { get; set; }
    public List<ScssClass> Classes { get; set; } = new();
    public List<ScssNode> Nodes { get; set; } = new();
}
