// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Tables;

public sealed class BorderCollapse : ClassDictionaryBase
{
    public BorderCollapse()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "border-collapse", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-collapse: collapse;
                        """,
                }
            },
            {
                "border-separate", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        border-collapse: separate;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}