namespace Argentini.Sfumato.Entities.Library;

public static class LibraryStartingStyleQueries
{
    public static PrefixTrie<VariantMetadata> StartingStyleQueryPrefixes { get; } = new()
	{
		{
			"starting",
	        new VariantMetadata
	        {
	            PrefixOrder = 1,
	            PrefixType = "starting-style",
	        }
        },
    };
}