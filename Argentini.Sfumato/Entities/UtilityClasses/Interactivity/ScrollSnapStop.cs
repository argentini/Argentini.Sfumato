// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class ScrollSnapStop : ClassDictionaryBase
{
    public ScrollSnapStop()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "snap-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-snap-stop: normal;
                               """,
                }
            },
            {
                "snap-always", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               scroll-snap-stop: always;
                               """,
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}