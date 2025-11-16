// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class AspectRatio : ClassDictionaryBase
{
    public AspectRatio()
    {
        Group = "aspect-ratio";
        Description = "Utilities for specifying the aspect ratio of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "aspect-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    InRatioCollection = true,
                    Template =
                        """
                        aspect-ratio: {0};
                        """,
                    ArbitraryCssValueTemplate =
                        """
                        aspect-ratio: {0};
                        """
                }
            },
            {
                "aspect-screen", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               aspect-ratio: 4 / 3;
                               """
                }
            },
            {
                "aspect-square", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               aspect-ratio: 1 / 1;
                               """
                }
            },
            {
                "aspect-video", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               aspect-ratio: 16 / 9;
                               """
                }
            },
            {
                "aspect-auto", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               aspect-ratio: auto;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}