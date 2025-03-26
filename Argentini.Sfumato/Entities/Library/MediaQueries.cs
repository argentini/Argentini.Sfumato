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
			    PrefixType = "theme",
			    Statement = "@media (prefers-color-scheme: dark) {"
		    }
	    },
	    {
		    "portrait",
			new VariantMetadata
			{
				PrefixOrder = 2,
				Priority = 128,
				PrefixType = "orientation",
				Statement = "@media (orientation: portrait) {"
			}
		},
		{
			"landscape",
			new VariantMetadata
			{
				PrefixOrder = 3,
				Priority = 256,
				PrefixType = "orientation",
				Statement = "@media (orientation: landscape) {"
			}
		},
		{
			"print",
			new VariantMetadata
			{
				PrefixOrder = 4,
				Priority = 512,
				PrefixType = "output",
				Statement = "@media print {"
			}
		},
		{
			"motion-safe",
			new VariantMetadata
			{
				PrefixOrder = 5,
				Priority = 1024,
				PrefixType = "animation",
				Statement = "@media (prefers-reduced-motion: no-preference) {"
			}
		},
		{
			"motion-reduced",
			new VariantMetadata
			{
				PrefixOrder = 6,
				Priority = 2048,
				PrefixType = "animation",
				Statement = "@media (prefers-reduced-motion: reduce) {"
			}
		},
		{
			"supports-backdrop-blur",
	        new VariantMetadata
	        {
	            PrefixOrder = 7,
	            Priority = 4096,
	            PrefixType = "features",
	            Statement = "@supports ((-webkit-backdrop-filter:blur(0)) or (backdrop-filter:blur(0))) or (-webkit-backdrop-filter:blur(0)) {"
	        }
        },
	    {
	        "sm",
			new VariantMetadata
			{
				PrefixOrder = 8,
				Priority = 4,
				PrefixType = "breakpoint",
				Statement = "@media (width >= 40rem) {"
			}
		},
		{
			"md",
			new VariantMetadata
			{
				PrefixOrder = 9,
				Priority = 8,
				PrefixType = "breakpoint",
				Statement = "@media (width >= 48rem) {"
			}
		},
		{
			"lg",
			new VariantMetadata
			{
				PrefixOrder = 10,
				Priority = 16,
				PrefixType = "breakpoint",
				Statement = "@media (width >= 64rem) {"
			}
		},
		{
			"xl",
			new VariantMetadata
			{
				PrefixOrder = 11,
				Priority = 32,
				PrefixType = "breakpoint",
				Statement = "@media (width >= 80rem) {"
			}
		},
			{
				"2xl",
				new VariantMetadata
				{
					PrefixOrder = 12,
					Priority = 64,
					PrefixType = "breakpoint",
					Statement = "@media (width >= 96rem}) {"
				}
		},
		{
			"mobi",
			new VariantMetadata
			{
				PrefixOrder = 13,
				Priority = 128,
				PrefixType = "breakpoint",
				Statement = "@media screen and (max-aspect-ratio: 0.624999999999) {"
			}
		},
		{
			"phab",
			new VariantMetadata
			{
				PrefixOrder = 14,
				Priority = 256,
				PrefixType = "breakpoint",
				Statement = "@media screen and (min-aspect-ratio: 0.575) {"
			}
		},
		{
			"tabp",
			new VariantMetadata
			{
				PrefixOrder = 15,
				Priority = 512,
				PrefixType = "breakpoint",
				Statement = "@media screen and (min-aspect-ratio: 0.625) {"
			}
		},
		{
			"tabl",
			new VariantMetadata
			{
				PrefixOrder = 16,
				Priority = 1024,
				PrefixType = "breakpoint",
				Statement = "@media screen and (min-aspect-ratio: 1.0) {"
			}
		},
		{
			"desk",
			new VariantMetadata
			{
				PrefixOrder = 17,
				Priority = 2048,
				PrefixType = "breakpoint",
				Statement = "@media screen and (min-aspect-ratio: 1.5) {"
			}
		},
		{
			"wide",
			new VariantMetadata
			{
				PrefixOrder = 18,
				Priority = 4096,
				PrefixType = "breakpoint",
				Statement = "@media screen and (min-aspect-ratio: 1.77777778) {"
			}
		},
		{
			"vast",
			new VariantMetadata
			{
				PrefixOrder = 19,
				Priority = 8192,
				PrefixType = "breakpoint",
				Statement = "@media screen and (min-aspect-ratio: 2.33333333) {"
			}
		}
    };
}