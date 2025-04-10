// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class Color : ClassDictionaryBase
{
    public Color()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = """
                               color: {0};
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}