using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Trie;

namespace Sfumato.Entities.Library;

public static class LibraryContainerQueries
{
    public static PrefixTrie<VariantMetadata> ContainerQueryPrefixes { get; } = new()
    {
        {
            "@min-",
            new VariantMetadata
            {
                PrefixOrder = 70000,
                PrefixType = "container",
                Statement = "(width >= {0})",
                SpecialCase = true
            }
        },
        {
            "@@min-",
            new VariantMetadata
            {
                PrefixOrder = 70000,
                PrefixType = "container",
                Statement = "(width >= {0})",
                SpecialCase = true,
                IsRazorSyntax = true
            }
        },
        {
            "@max-",
            new VariantMetadata
            {
                PrefixOrder = 70100,
                PrefixType = "container",
                Statement = "(width < {0})",
                SpecialCase = true
            }
        },
        {
            "@@max-",
            new VariantMetadata
            {
                PrefixOrder = 70100,
                PrefixType = "container",
                Statement = "(width < {0})",
                SpecialCase = true,
                IsRazorSyntax = true
            }
        },
    };
}
