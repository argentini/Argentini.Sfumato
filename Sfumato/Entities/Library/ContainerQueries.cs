using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Trie;

namespace Sfumato.Entities.Library;

public static class LibraryContainerQueries
{
    public static PrefixTrie<VariantMetadata> ContainerQueryPrefixes { get; } = new();
}