namespace Argentini.Sfumato.Entities;

public class ClassDictionaryBase
{
    public Dictionary<string, ClassDefinition> Data { get; } = new (StringComparer.Ordinal);
    
    public ClassDefinition this[string key]
    {
        get => Data[key];
        set => Data[key] = value;
    }

    protected void AddFractions(string key, string template)
    {
        var denominators = new int[2, 3, 4, 5, 6, 8, 10, 12];

        foreach (var denominator in denominators)
        {
            for (var numerator = 1; numerator < denominator; numerator++)
            {
                Data.Add($"{key}{numerator}/{denominator}", new ClassDefinition
                    {
                        IsSimpleUtility = true,
                        Template = template.Replace("{0}", $"{numerator}").Replace("{1}", $"{denominator}")
                    }
                );
            }
        }
    }
}