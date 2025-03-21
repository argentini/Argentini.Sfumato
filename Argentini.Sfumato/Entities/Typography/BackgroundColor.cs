// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.Typography;

public sealed class BackgroundColor : ClassDictionaryBase
{
    public BackgroundColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>()
        {
            {
                "bg-", new ClassDefinition
                {
                    UsesColor = true,
                    UsesSlashModifier = true,
                    Template = """
                               background-color: {0};
                               """,
                    ModifierTemplate = """
                                       background-color: {0};
                                       """
                }
            },
        });
    }
}