using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato.Entities.Library;

public static class LibraryMediaQueries
{
    public static Dictionary<string, VariantMetadata> MediaQueryPrefixes { get; } = new()
	{
	    {
		    "dark",
		    new VariantMetadata
		    {
			    PrefixOrder = 1,
			    PrefixType = "media",
			    Statement = "(prefers-color-scheme: dark)"
		    }
	    },
	    {
		    "print",
		    new VariantMetadata
		    {
			    PrefixOrder = 2,
			    PrefixType = "media",
			    Statement = "print"
		    }
	    },
	    {
		    "portrait",
			new VariantMetadata
			{
				PrefixOrder = 3,
				PrefixType = "media",
				Statement = "(orientation: portrait)"
			}
		},
		{
			"landscape",
			new VariantMetadata
			{
				PrefixOrder = 4,
				PrefixType = "media",
				Statement = "(orientation: landscape)"
			}
		},
		{
			"motion-safe",
			new VariantMetadata
			{
				PrefixOrder = 5,
				PrefixType = "media",
				Statement = "(prefers-reduced-motion: no-preference)"
			}
		},
		{
			"motion-reduced",
			new VariantMetadata
			{
				PrefixOrder = 6,
				PrefixType = "media",
				Statement = "(prefers-reduced-motion: reduce)"
			}
		},
    };
}