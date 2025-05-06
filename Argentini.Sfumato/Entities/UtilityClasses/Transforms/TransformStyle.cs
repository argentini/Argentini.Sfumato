// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Transforms;

public sealed class TransformStyle : ClassDictionaryBase
{
    public TransformStyle()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "transform-3d", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-style: preserve-3d;
                        """,
                }
            },
            {
                "transform-flat", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        transform-style: flat;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}