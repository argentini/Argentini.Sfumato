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
			    Priority = int.MaxValue,
			    PrefixType = "media",
			    Statement = "(prefers-color-scheme: dark)"
		    }
	    },
	    {
		    "print",
		    new VariantMetadata
		    {
			    PrefixOrder = 2,
			    Priority = int.MaxValue / 2,
			    PrefixType = "media",
			    Statement = "print"
		    }
	    },
	    {
		    "portrait",
			new VariantMetadata
			{
				PrefixOrder = 3,
				Priority = int.MaxValue / 4,
				PrefixType = "media",
				Statement = "(orientation: portrait)"
			}
		},
		{
			"landscape",
			new VariantMetadata
			{
				PrefixOrder = 4,
				Priority = int.MaxValue / 8,
				PrefixType = "media",
				Statement = "(orientation: landscape)"
			}
		},
		{
			"motion-safe",
			new VariantMetadata
			{
				PrefixOrder = 5,
				Priority = int.MaxValue / 16,
				PrefixType = "media",
				Statement = "(prefers-reduced-motion: no-preference)"
			}
		},
		{
			"motion-reduced",
			new VariantMetadata
			{
				PrefixOrder = 6,
				Priority = int.MaxValue / 32,
				PrefixType = "media",
				Statement = "(prefers-reduced-motion: reduce)"
			}
		},
		{
			"supports-backdrop-blur",
	        new VariantMetadata
	        {
	            PrefixOrder = 7,
	            Priority = int.MaxValue / 64,
	            PrefixType = "supports",
	            Statement = "((-webkit-backdrop-filter:blur(0)) or (backdrop-filter:blur(0))) or (-webkit-backdrop-filter:blur(0))"
	        }
        },
		
		// Breakpoints
		
	    {
	        "sm",
			new VariantMetadata
			{
				PrefixOrder = 8,
				Priority = 2,
				PrefixType = "media",
				Statement = "(width >= 40rem)"
			}
		},
		{
			"max-sm",
			new VariantMetadata
			{
				PrefixOrder = 9,
				Priority = 2,
				PrefixType = "media",
				Statement = "(width < 40rem)"
			}
		},
		{
			"md",
			new VariantMetadata
			{
				PrefixOrder = 10,
				Priority = 4,
				PrefixType = "media",
				Statement = "(width >= 48rem)"
			}
		},
		{
			"max-md",
			new VariantMetadata
			{
				PrefixOrder = 11,
				Priority = 4,
				PrefixType = "media",
				Statement = "(width < 48rem)"
			}
		},
		{
			"lg",
			new VariantMetadata
			{
				PrefixOrder = 12,
				Priority = 8,
				PrefixType = "breakpoint",
				Statement = "(width >= 64rem)"
			}
		},
		{
			"max-lg",
			new VariantMetadata
			{
				PrefixOrder = 13,
				Priority = 8,
				PrefixType = "media",
				Statement = "(width < 64rem)"
			}
		},
		{
			"xl",
			new VariantMetadata
			{
				PrefixOrder = 14,
				Priority = 16,
				PrefixType = "media",
				Statement = "(width >= 80rem)"
			}
		},
		{
			"max-xl",
			new VariantMetadata
			{
				PrefixOrder = 15,
				Priority = 16,
				PrefixType = "media",
				Statement = "(width < 80rem)"
			}
		},
		{
			"2xl",
			new VariantMetadata
			{
				PrefixOrder = 16,
				Priority = 32,
				PrefixType = "media",
				Statement = "(width >= 96rem})"
			}
		},
		{
			"max-2xl",
			new VariantMetadata
			{
				PrefixOrder = 17,
				Priority = 32,
				PrefixType = "media",
				Statement = "(width < 96rem})"
			}
		},
		{
			"mobi",
			new VariantMetadata
			{
				PrefixOrder = 18,
				Priority = 64,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 0.624999999999)"
			}
		},
		{
			"phab",
			new VariantMetadata
			{
				PrefixOrder = 19,
				Priority = 128,
				PrefixType = "media",
				Statement = "screen and (min-aspect-ratio: 0.575)"
			}
		},
		{
			"max-phab",
			new VariantMetadata
			{
				PrefixOrder = 20,
				Priority = 128,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 0.574999999999)"
			}
		},
		{
			"tabp",
			new VariantMetadata
			{
				PrefixOrder = 21,
				Priority = 256,
				PrefixType = "media",
				Statement = "screen and (min-aspect-ratio: 0.625)"
			}
		},
		{
			"max-tabp",
			new VariantMetadata
			{
				PrefixOrder = 22,
				Priority = 256,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 0.624999999999)"
			}
		},
		{
			"tabl",
			new VariantMetadata
			{
				PrefixOrder = 23,
				Priority = 512,
				PrefixType = "media",
				Statement = "screen and (min-aspect-ratio: 1.0)"
			}
		},
		{
			"max-tabl",
			new VariantMetadata
			{
				PrefixOrder = 24,
				Priority = 512,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 0.999999999999)"
			}
		},
		{
			"desk",
			new VariantMetadata
			{
				PrefixOrder = 25,
				Priority = 1024,
				PrefixType = "media",
				Statement = "screen and (min-aspect-ratio: 1.5)"
			}
		},
		{
			"max-desk",
			new VariantMetadata
			{
				PrefixOrder = 26,
				Priority = 1024,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 1.499999999999)"
			}
		},
		{
			"wide",
			new VariantMetadata
			{
				PrefixOrder = 27,
				Priority = 2048,
				PrefixType = "media",
				Statement = "screen and (min-aspect-ratio: 1.77777778)"
			}
		},
		{
			"max-wide",
			new VariantMetadata
			{
				PrefixOrder = 28,
				Priority = 2048,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 1.777777779999)"
			}
		},
		{
			"vast",
			new VariantMetadata
			{
				PrefixOrder = 29,
				Priority = 4096,
				PrefixType = "media",
				Statement = "screen and (min-aspect-ratio: 2.33333333)"
			}
		},
		{
			"max-vast",
			new VariantMetadata
			{
				PrefixOrder = 30,
				Priority = 4096,
				PrefixType = "media",
				Statement = "screen and (max-aspect-ratio: 2.333333329999)"
			}
		},
    };
}