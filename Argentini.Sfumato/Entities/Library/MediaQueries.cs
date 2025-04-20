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
    };
}