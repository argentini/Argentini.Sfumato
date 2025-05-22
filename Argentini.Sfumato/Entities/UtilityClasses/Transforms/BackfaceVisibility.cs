// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class BackfaceVisibility : ClassDictionaryBase
{
    public BackfaceVisibility()
    {
        Description = "";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "backface-hidden", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        backface-visibility: hidden;
                        """,
                }
            },
            {
                "backface-visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        backface-visibility: visible;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}