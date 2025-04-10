using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato.Entities.Library;

public static class LibrarySupportsQueries
{
    public static Dictionary<string, VariantMetadata> SupportsQueryPrefixes { get; } = new()
	{
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