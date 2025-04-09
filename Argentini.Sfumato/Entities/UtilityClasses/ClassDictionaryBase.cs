namespace Argentini.Sfumato.Entities.UtilityClasses;

public abstract class ClassDictionaryBase
{
    public Dictionary<string, ClassDefinition> Data { get; } = new (StringComparer.Ordinal);
    
    public ClassDefinition this[string key]
    {
        get => Data[key];
        set => Data[key] = value;
    }

    public abstract void ProcessThemeSettings(AppRunner appRunner);
}