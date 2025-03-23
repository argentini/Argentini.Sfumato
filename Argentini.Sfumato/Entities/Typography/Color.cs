// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class Color : ClassDictionaryBase
{
    public Color()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "text-", new ClassDefinition
                {
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = """
                               color: {0};
                               """,
                    ModifierTemplate = """
                                       color: {0};
                                       """
                }
            },
        });
    }
}