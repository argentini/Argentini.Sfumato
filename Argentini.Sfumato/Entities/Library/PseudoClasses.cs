using Argentini.Sfumato.Entities.CssClassProcessing;

namespace Argentini.Sfumato.Entities.Library;

public static class LibraryPseudoClasses
{
    public static Dictionary<string, VariantMetadata> PseudoclassPrefixes { get; } = new ()
    {
        {
	        "hover",
	        new VariantMetadata
	        {
		        PrefixType = "pseudoclass",
		        Statement = ":hover"
	        }
	    },
        {
	        "focus",
	        new VariantMetadata
	        {
		        PrefixType = "pseudoclass",
		        Statement = ":focus"
	        }
	    },
	    {
		    "focus-within",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":focus-within"
		    }
	    },
	    {
		    "focus-visible",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":focus-visible"
		    }
	    },
	    {
		    "active",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":active"
		    }
	    },
	    {
		    "visited",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":visited"
		    }
	    },
	    {
		    "target", 
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":target"
		    }
	    },
	    {
		    "first", 
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":first-child"
		    }
	    },
	    {
		    "last",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":last-child"
		    }
	    },
	    {
		    "only",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":only-child"
		    }
	    },
	    {
		    "odd",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":nth-child(odd)"
		    }
	    },
	    {
		    "even",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":nth-child(even)"
		    }
	    },
	    {
		    "first-of-type",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":first-of-type"
		    }
	    },
	    {
		    "last-of-type",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":last-of-type"
		    }
	    },
	    {
		    "only-of-type",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":only-of-type"
		    }
	    },
	    {
		    "empty",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":empty"
		    }
	    },
	    {
		    "disabled",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":disabled"
		    }
	    },
	    {
		    "enabled",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":enabled"
		    }
	    },
	    {
		    "checked",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":checked"
		    }
	    },
	    {
		    "indeterminate",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":indeterminate"
		    }
	    },
	    {
		    "default",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":default"
		    }
	    },
	    {
		    "required",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":required"
		    }
	    },
	    {
		    "valid",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":valid"
		    }
	    },
	    {
		    "invalid",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":invalid"
		    }
	    },
	    {
		    "in-range",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":in-range"
		    }
	    },
	    {
		    "out-of-range",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":out-of-range"
		    }
	    },
	    {
		    "placeholder-shown",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":placeholder-shown"
		    }
	    },
	    {
		    "autofill",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":autofill"
		    }
	    },
	    {
		    "read-only",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":read-only"
		    }
	    },
	    {
		    "before",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::before"
		    }
	    },
	    {
		    "after",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::after"
		    }
	    },
	    {
		    "first-letter",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::first-letter"
		    }
	    },
	    {
		    "first-line",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::first-line"
		    }
	    },
	    {
		    "marker",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::marker"
		    }
	    },
	    {
		    "selection",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::selection"
		    }
	    },
	    {
		    "file",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::file-selector-button"
		    }
	    },
	    {
		    "backdrop",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::backdrop"
		    }
	    },
	    {
		    "placeholder",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "::placeholder"
		    }
	    },
	    {
		    "open",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":is([open],:popover-open,:open)"
		    }
	    },
	    {
		    "closed",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":is([closed],:popover-closed,:closed)"
		    }
	    },
	    {
		    "inert",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[inert]"
		    }
	    },
	    {
		    "ltr",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":where(:dir(ltr),[dir=ltr],[dir=ltr] *)"
		    }
	    },
	    {
		    "rtl",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = ":where(:dir(rtl),[dir=rtl],[dir=rtl] *)"
		    }
	    },
	    {
		    "aria-busy",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-busy=\"true\"]"
		    }
	    },
	    {
		    "aria-checked",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-checked=\"true\"]"
		    }
	    },
	    {
		    "aria-disabled",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-disabled=\"true\"]"
		    }
	    },
	    {
		    "aria-expanded",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-expanded=\"true\"]"
		    }
	    },
	    {
		    "aria-hidden",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-hidden=\"true\"]"
		    }
	    },
	    {
		    "aria-pressed",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-pressed=\"true\"]"
		    }
	    },
	    {
		    "aria-readonly",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-readonly=\"true\"]"
		    }
	    },
	    {
		    "aria-required",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-required=\"true\"]"
		    }
	    },
	    {
		    "aria-selected",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    Statement = "[aria-selected=\"true\"]"
		    }
	    },
    };
}