using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato.Entities.Library;

public static class LibraryContainerQueries
{
    public static Dictionary<string, VariantMetadata> ContainerQueryPrefixes { get; } = new()
	{
		{
			"@max-2xl",
			new VariantMetadata
			{
				PrefixOrder = 8,
				PrefixType = "container",
				Statement = "(width < 96rem})"
			}
		},
		{
			"@max-xl",
			new VariantMetadata
			{
				PrefixOrder = 9,
				PrefixType = "container",
				Statement = "(width < 80rem)"
			}
		},
		{
			"@max-lg",
			new VariantMetadata
			{
				PrefixOrder = 10,
				PrefixType = "container",
				Statement = "(width < 64rem)"
			}
		},
		{
			"@max-md",
			new VariantMetadata
			{
				PrefixOrder = 11,
				PrefixType = "container",
				Statement = "(width < 48rem)"
			}
		},
		{
			"@max-sm",
			new VariantMetadata
			{
				PrefixOrder = 12,
				PrefixType = "container",
				Statement = "(width < 40rem)"
			}
		},
		{
			"@2xl",
			new VariantMetadata
			{
				PrefixOrder = 13,
				PrefixType = "container",
				Statement = "(width >= 96rem})"
			}
		},
		{
			"@xl",
			new VariantMetadata
			{
				PrefixOrder = 14,
				PrefixType = "container",
				Statement = "(width >= 80rem)"
			}
		},
		{
			"@lg",
			new VariantMetadata
			{
				PrefixOrder = 15,
				PrefixType = "breakpoint",
				Statement = "(width >= 64rem)"
			}
		},
		{
			"@md",
			new VariantMetadata
			{
				PrefixOrder = 16,
				PrefixType = "container",
				Statement = "(width >= 48rem)"
			}
		},
		{
	        "@sm",
			new VariantMetadata
			{
				PrefixOrder = 17,
				PrefixType = "container",
				Statement = "(width >= 40rem)"
			}
		},
    };
}