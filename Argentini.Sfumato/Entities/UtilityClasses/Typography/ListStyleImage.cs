// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class ListStyleImage : ClassDictionaryBase
{
    public ListStyleImage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "list-image-", new ClassDefinition
                {
                    UsesImageUrl = true,
                    Template = """
                               list-style-image: {0};
                               """
                }
            },
            {
                "list-image-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               list-style-image: none;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}