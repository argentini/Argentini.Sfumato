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
    
    #region Dictionaries

    protected readonly Dictionary<string, string> Sizes = new ()
    {
        {
            "auto", "auto"
        },
        {
            "px", "1px"
        },
        {
            "full", "100%"
        },
        {
            "screen", "100vw"
        },
        {
            "dvw", "100dvw"
        },
        {
            "dvh", "100dvh"
        },
        {
            "lvw", "100lvw"
        },
        {
            "lvh", "100lvh"
        },
        {
            "svw", "100svw"
        },
        {
            "svh", "100svh"
        },
        {
            "min", "min-content"
        },
        {
            "max", "max-content"
        },
        {
            "fit", "fit-content"
        },
    };
    
    #endregion
}