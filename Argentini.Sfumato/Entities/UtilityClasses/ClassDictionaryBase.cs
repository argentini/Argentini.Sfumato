namespace Argentini.Sfumato.Entities.UtilityClasses;

public class ClassDictionaryBase
{
    public Dictionary<string, ClassDefinition> Data { get; } = new (StringComparer.Ordinal);
    
    public ClassDefinition this[string key]
    {
        get => Data[key];
        set => Data[key] = value;
    }
}