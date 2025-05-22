// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Effects;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class Opacity : ClassDictionaryBase
{
    public Opacity()
    {
        Group = "opacity";
        Description = "Utilities for adjusting the transparency of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "opacity-", new ClassDefinition
                {
                    InFloatNumberCollection = true,
                    Template =
                        """
                        opacity: calc({0} * 0.01);
                        """,
                    ArbitraryCssValueTemplate = 
                        """
                        opacity: {0};
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
