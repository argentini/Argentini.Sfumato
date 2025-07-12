namespace Argentini.Sfumato.Entities.Library;

public static class LibraryMediaQueries
{
    public static Dictionary<string, VariantMetadata> MediaQueryPrefixes { get; } = new()
	{
	    {
		    "dark",
		    new VariantMetadata
		    {
			    PrefixOrder = 99999,
			    PrefixType = "media",
			    Statement = "(prefers-color-scheme: dark)"
		    }
	    },
	    {
		    "print",
		    new VariantMetadata
		    {
			    PrefixOrder = 90000,
			    PrefixType = "media",
			    Statement = "print"
		    }
	    },
	    {
		    "portrait",
			new VariantMetadata
			{
				PrefixOrder = 85000,
				PrefixType = "media",
				Statement = "(orientation: portrait)"
			}
		},
		{
			"landscape",
			new VariantMetadata
			{
				PrefixOrder = 85000,
				PrefixType = "media",
				Statement = "(orientation: landscape)"
			}
		},
		{
			"motion-safe",
			new VariantMetadata
			{
				PrefixOrder = 80000,
				PrefixType = "media",
				Statement = "(prefers-reduced-motion: no-preference)"
			}
		},
		{
			"motion-reduced",
			new VariantMetadata
			{
				PrefixOrder = 80000,
				PrefixType = "media",
				Statement = "(prefers-reduced-motion: reduce)"
			}
		},
		{
			"pointer-fine",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(pointer: fine)"
			}
		},
		{
			"pointer-coarse",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(pointer: coarse)"
			}
		},
		{
			"pointer-none",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(pointer: none)"
			}
		},
		{
			"any-pointer-fine",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(any-pointer: fine)"
			}
		},
		{
			"any-pointer-coarse",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(any-pointer: coarse)"
			}
		},
 		{
			"any-pointer-none",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(any-pointer: none)"
			}
		},
 		{
			"noscript",
			new VariantMetadata
			{
				PrefixOrder = 75000,
				PrefixType = "media",
				Statement = "(scripting: none)"
			}
		},
    };
}