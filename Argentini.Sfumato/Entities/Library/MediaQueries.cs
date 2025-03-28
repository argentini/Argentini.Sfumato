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
		{
			"supports-backdrop-blur",
	        new VariantMetadata
	        {
	            PrefixOrder = 7,
	            PrefixType = "supports",
	            Statement = "((-webkit-backdrop-filter:blur(0)) or (backdrop-filter:blur(0))) or (-webkit-backdrop-filter:blur(0))"
	        }
        },
		
		// Grid Breakpoints

		{
			"max-2xl",
			new VariantMetadata
			{
				PrefixOrder = 8,
				PrefixType = "media",
				Statement = "(width < 96rem})"
			}
		},
		{
			"max-xl",
			new VariantMetadata
			{
				PrefixOrder = 9,
				PrefixType = "media",
				Statement = "(width < 80rem)"
			}
		},
		{
			"max-lg",
			new VariantMetadata
			{
				PrefixOrder = 10,
				PrefixType = "media",
				Statement = "(width < 64rem)"
			}
		},
		{
			"max-md",
			new VariantMetadata
			{
				PrefixOrder = 11,
				PrefixType = "media",
				Statement = "(width < 48rem)"
			}
		},
		{
			"max-sm",
			new VariantMetadata
			{
				PrefixOrder = 12,
				PrefixType = "media",
				Statement = "(width < 40rem)"
			}
		},
		{
			"2xl",
			new VariantMetadata
			{
				PrefixOrder = 13,
				PrefixType = "media",
				Statement = "(width >= 96rem})"
			}
		},
		{
			"xl",
			new VariantMetadata
			{
				PrefixOrder = 14,
				PrefixType = "media",
				Statement = "(width >= 80rem)"
			}
		},
		{
			"lg",
			new VariantMetadata
			{
				PrefixOrder = 15,
				PrefixType = "breakpoint",
				Statement = "(width >= 64rem)"
			}
		},
		{
			"md",
			new VariantMetadata
			{
				PrefixOrder = 16,
				PrefixType = "media",
				Statement = "(width >= 48rem)"
			}
		},
		{
	        "sm",
			new VariantMetadata
			{
				PrefixOrder = 17,
				PrefixType = "media",
				Statement = "(width >= 40rem)"
			}
		},
		
		// Adaptive Breakpoints
		
		{
			"max-vast",
			new VariantMetadata
			{
				PrefixOrder = 18,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 2.333333329999)"
			}
		},
		{
			"max-wide",
			new VariantMetadata
			{
				PrefixOrder = 19,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 1.777777779999)"
			}
		},
		{
			"max-desk",
			new VariantMetadata
			{
				PrefixOrder = 20,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 1.499999999999)"
			}
		},
		{
			"max-tabl",
			new VariantMetadata
			{
				PrefixOrder = 21,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 0.999999999999)"
			}
		},
		{
			"max-tabp",
			new VariantMetadata
			{
				PrefixOrder = 22,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 0.624999999999)"
			}
		},
		{
			"max-phab",
			new VariantMetadata
			{
				PrefixOrder = 23,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 0.574999999999)"
			}
		},
		{
			"vast",
			new VariantMetadata
			{
				PrefixOrder = 24,
				PrefixType = "media",
				Statement = "(min-aspect-ratio: 2.33333333)"
			}
		},
		{
			"wide",
			new VariantMetadata
			{
				PrefixOrder = 25,
				PrefixType = "media",
				Statement = "(min-aspect-ratio: 1.77777778)"
			}
		},
		{
			"desk",
			new VariantMetadata
			{
				PrefixOrder = 26,
				PrefixType = "media",
				Statement = "(min-aspect-ratio: 1.5)"
			}
		},
		{
			"tabl",
			new VariantMetadata
			{
				PrefixOrder = 27,
				PrefixType = "media",
				Statement = "(min-aspect-ratio: 1.0)"
			}
		},
		{
			"tabp",
			new VariantMetadata
			{
				PrefixOrder = 28,
				PrefixType = "media",
				Statement = "(min-aspect-ratio: 0.625)"
			}
		},
		{
			"phab",
			new VariantMetadata
			{
				PrefixOrder = 29,
				PrefixType = "media",
				Statement = "(min-aspect-ratio: 0.575)"
			}
		},
		{
			"mobi",
			new VariantMetadata
			{
				PrefixOrder = 30,
				PrefixType = "media",
				Statement = "(max-aspect-ratio: 0.624999999999)"
			}
		},
    };
}