// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class PerspectiveOrigin : ClassDictionaryBase
{
    public PerspectiveOrigin()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "perspective-origin-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template =
                        """
                        perspective-origin: {0};
                        """,
                }
            },
            {
                "perspective-origin-center", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        perspective-origin: center;
                        """,
                }
            },
            {
                "perspective-origin-top-left", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        perspective-origin: top left;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}