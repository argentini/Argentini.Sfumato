// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Tables;

public sealed class CaptionSide : ClassDictionaryBase
{
    public CaptionSide()
    {
        Description = "Utilities for configuring captionside.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "caption-top", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template =
                        """
                        caption-side: top;
                        """,
                }
            },
            {
                "caption-bottom", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
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