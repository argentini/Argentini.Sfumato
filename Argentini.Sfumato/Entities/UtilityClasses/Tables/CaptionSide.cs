// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Tables;

public sealed class CaptionSide : ClassDictionaryBase
{
    public CaptionSide()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "caption-top", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        caption-side: top;
                        """,
                }
            },
            {
                "caption-bottom", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template =
                        """
                        caption-side: bottom;
                        """,
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}