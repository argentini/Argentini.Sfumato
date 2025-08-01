namespace Argentini.Sfumato.Entities.Library;

public static class LibrarySupportsQueries
{
    public static PrefixTrie<VariantMetadata> SupportsQueryPrefixes { get; } = new()
	{
		{
			"supports-",
	        new VariantMetadata
	        {
	            PrefixOrder = 1,
	            PrefixType = "supports",
	            Statement = "({0})",
	            SpecialCase = true,
	        }
        },
		{
			"not-supports-",
			new VariantMetadata
			{
				PrefixOrder = 1,
				PrefixType = "supports",
				Statement = "not ({0})",
				SpecialCase = true,
			}
		},
		{
			"supports-backdrop-blur",
	        new VariantMetadata
	        {
	            PrefixOrder = 1,
	            PrefixType = "supports",
	            Statement = "((-webkit-backdrop-filter:blur(0)) or (backdrop-filter:blur(0))) or (-webkit-backdrop-filter:blur(0))"
	        }
        },
    };
}