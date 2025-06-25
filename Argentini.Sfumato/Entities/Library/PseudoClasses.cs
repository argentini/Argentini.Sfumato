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
		        SelectorSuffix = ":hover"
	        }
	    },
        {
	        "focus",
	        new VariantMetadata
	        {
		        PrefixType = "pseudoclass",
		        SelectorSuffix = ":focus"
	        }
	    },
	    {
		    "focus-within",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":focus-within"
		    }
	    },
	    {
		    "focus-visible",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":focus-visible"
		    }
	    },
	    {
		    "active",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":active"
		    }
	    },
	    {
		    "visited",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":visited"
		    }
	    },
	    {
		    "target", 
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":target"
		    }
	    },
	    {
		    "first", 
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":first-child"
		    }
	    },
	    {
		    "last",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":last-child"
		    }
	    },
	    {
		    "only",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":only-child"
		    }
	    },
	    {
		    "odd",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":nth-child(odd)"
		    }
	    },
	    {
		    "even",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":nth-child(even)"
		    }
	    },
	    {
		    "first-of-type",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":first-of-type"
		    }
	    },
	    {
		    "last-of-type",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":last-of-type"
		    }
	    },
	    {
		    "only-of-type",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":only-of-type"
		    }
	    },
	    {
		    "empty",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":empty"
		    }
	    },
	    {
		    "disabled",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":disabled"
		    }
	    },
	    {
		    "enabled",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":enabled"
		    }
	    },
	    {
		    "checked",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":checked"
		    }
	    },
	    {
		    "indeterminate",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":indeterminate"
		    }
	    },
	    {
		    "default",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":default"
		    }
	    },
	    {
		    "required",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":required"
		    }
	    },
	    {
		    "valid",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":valid"
		    }
	    },
	    {
		    "invalid",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":invalid"
		    }
	    },
	    {
		    "in-range",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":in-range"
		    }
	    },
	    {
		    "out-of-range",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":out-of-range"
		    }
	    },
	    {
		    "placeholder-shown",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":placeholder-shown"
		    }
	    },
	    {
		    "autofill",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":autofill"
		    }
	    },
	    {
		    "read-only",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":read-only"
		    }
	    },
	    {
		    "before",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::before"
		    }
	    },
	    {
		    "after",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::after"
		    }
	    },
	    {
		    "first-letter",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::first-letter"
		    }
	    },
	    {
		    "first-line",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::first-line"
		    }
	    },
	    {
		    "marker",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::marker"
		    }
	    },
	    {
		    "selection",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::selection"
		    }
	    },
	    {
		    "file",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::file-selector-button"
		    }
	    },
	    {
		    "backdrop",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::backdrop"
		    }
	    },
	    {
		    "placeholder",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "::placeholder"
		    }
	    },
	    {
		    "open",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":is([open],:popover-open,:open)"
		    }
	    },
	    {
		    "closed",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":is([closed],:popover-closed,:closed)"
		    }
	    },
	    {
		    "inert",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[inert]"
		    }
	    },
	    {
		    "ltr",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":where(:dir(ltr),[dir=ltr],[dir=ltr] *)"
		    }
	    },
	    {
		    "rtl",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ":where(:dir(rtl),[dir=rtl],[dir=rtl] *)"
		    }
	    },
	    {
		    "aria-busy",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-busy=\"true\"]"
		    }
	    },
	    {
		    "aria-checked",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-checked=\"true\"]"
		    }
	    },
	    {
		    "aria-disabled",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-disabled=\"true\"]"
		    }
	    },
	    {
		    "aria-expanded",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-expanded=\"true\"]"
		    }
	    },
	    {
		    "aria-hidden",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-hidden=\"true\"]"
		    }
	    },
	    {
		    "aria-pressed",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-pressed=\"true\"]"
		    }
	    },
	    {
		    "aria-readonly",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-readonly=\"true\"]"
		    }
	    },
	    {
		    "aria-required",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-required=\"true\"]"
		    }
	    },
	    {
		    "aria-selected",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = "[aria-selected=\"true\"]"
		    }
	    },
	    {
		    "*",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ""
		    }
	    },
	    {
		    "**",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ""
		    }
	    },
	    {
		    "data-",
		    new VariantMetadata
		    {
			    PrefixType = "pseudoclass",
			    SelectorSuffix = ""
		    }
	    },
    };
}