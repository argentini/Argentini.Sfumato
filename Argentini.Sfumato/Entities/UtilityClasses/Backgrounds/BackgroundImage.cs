// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class BackgroundImage : ClassDictionaryBase
{
    public BackgroundImage()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "bg-", new ClassDefinition
                {
                    UsesImageUrl = true,
                    Template = """
                               background-image: {0};
                               """,
                }
            },
        });
    }
}