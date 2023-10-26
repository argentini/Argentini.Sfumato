using Argentini.Sfumato.ScssUtilityCollections;
using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato;

public sealed class SfumatoAppState
{
	#region Theme Options

	public IEnumerable<CssMediaQuery> MediaQueryPrefixes { get; } = new[]
	{
		new CssMediaQuery
		{
			PrefixOrder = 1,
			Priority = 1024,
			Prefix = "dark",
			PrefixType = "theme",
			Statement = "@media (prefers-color-scheme: dark) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 2,
			Priority = 128,
			Prefix = "portrait",
			PrefixType = "orientation",
			Statement = "@media (orientation: portrait) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 3,
			Priority = 256,
			Prefix = "landscape",
			PrefixType = "orientation",
			Statement = "@media (orientation: landscape) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 4,
			Priority = 512,
			Prefix = "print",
			PrefixType = "output",
			Statement = "@media print {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 5,
			Priority = 1,
			Prefix = "zero",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: zero) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 6,
			Priority = 2,
			Prefix = "phab",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: phab) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 7,
			Priority = 4,
			Prefix = "tabp",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: tabp) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 8,
			Priority = 8,
			Prefix = "tabl",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: tabl) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 9,
			Priority = 16,
			Prefix = "note",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: note) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 10,
			Priority = 32,
			Prefix = "desk",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: desk) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 11,
			Priority = 64,
			Prefix = "elas",
			PrefixType = "breakpoint",
			Statement = "@include sf-media($from: elas) {"
		}
	};
	
    public Dictionary<string,string> ColorOptions { get; } = new()
    {
        ["inherit"] = "inherit",
        ["current"] = "currentColor",
        ["transparent"] = "transparent",
        ["black"] = "rgb(0,0,0)",
        ["white"] = "rgb(255,255,255)",
        ["slate"] = "rgb(203,213,225)",
        ["slate-50"] = "rgb(248,250,252)",
        ["slate-100"] = "rgb(241,245,249)",
        ["slate-200"] = "rgb(226,232,240)",
        ["slate-300"] = "rgb(203,213,225)",
        ["slate-400"] = "rgb(148,163,184)",
        ["slate-500"] = "rgb(100,116,139)",
        ["slate-600"] = "rgb(71,85,105)",
        ["slate-700"] = "rgb(51,65,85)",
        ["slate-800"] = "rgb(30,41,59)",
        ["slate-900"] = "rgb(15,23,42)",
        ["slate-950"] = "rgb(2,6,23)",
        ["gray"] = "rgb(209,213,219)",
        ["gray-50"] = "rgb(249,250,251)",
        ["gray-100"] = "rgb(243,244,246)",
        ["gray-200"] = "rgb(229,231,235)",
        ["gray-300"] = "rgb(209,213,219)",
        ["gray-400"] = "rgb(156,163,175)",
        ["gray-500"] = "rgb(107,114,128)",
        ["gray-600"] = "rgb(75,85,99)",
        ["gray-700"] = "rgb(55,65,81)",
        ["gray-800"] = "rgb(31,41,55)",
        ["gray-900"] = "rgb(17,24,39)",
        ["gray-950"] = "rgb(3,7,18)",
        ["zinc"] = "rgb(212,212,216)",
        ["zinc-50"] = "rgb(250,250,250)",
        ["zinc-100"] = "rgb(244,244,245)",
        ["zinc-200"] = "rgb(228,228,231)",
        ["zinc-300"] = "rgb(212,212,216)",
        ["zinc-400"] = "rgb(161,161,170)",
        ["zinc-500"] = "rgb(113,113,122)",
        ["zinc-600"] = "rgb(82,82,91)",
        ["zinc-700"] = "rgb(63,63,70)",
        ["zinc-800"] = "rgb(39,39,42)",
        ["zinc-900"] = "rgb(24,24,27)",
        ["zinc-950"] = "rgb(9,9,11)",
        ["neutral"] = "rgb(212,212,212)",
        ["neutral-50"] = "rgb(250,250,250)",
        ["neutral-100"] = "rgb(245,245,245)",
        ["neutral-200"] = "rgb(229,229,229)",
        ["neutral-300"] = "rgb(212,212,212)",
        ["neutral-400"] = "rgb(163,163,163)",
        ["neutral-500"] = "rgb(115,115,115)",
        ["neutral-600"] = "rgb(82,82,82)",
        ["neutral-700"] = "rgb(64,64,64)",
        ["neutral-800"] = "rgb(38,38,38)",
        ["neutral-900"] = "rgb(23,23,23)",
        ["neutral-950"] = "rgb(10,10,10)",
        ["stone"] = "rgb(214,211,209)",
        ["stone-50"] = "rgb(250,250,249)",
        ["stone-100"] = "rgb(245,245,244)",
        ["stone-200"] = "rgb(231,229,228)",
        ["stone-300"] = "rgb(214,211,209)",
        ["stone-400"] = "rgb(168,162,158)",
        ["stone-500"] = "rgb(120,113,108)",
        ["stone-600"] = "rgb(87,83,78)",
        ["stone-700"] = "rgb(68,64,60)",
        ["stone-800"] = "rgb(41,37,36)",
        ["stone-900"] = "rgb(28,25,23)",
        ["stone-950"] = "rgb(12,10,9)",
        ["red"] = "rgb(252,165,165)",
        ["red-50"] = "rgb(254,242,242)",
        ["red-100"] = "rgb(254,226,226)",
        ["red-200"] = "rgb(254,202,202)",
        ["red-300"] = "rgb(252,165,165)",
        ["red-400"] = "rgb(248,113,113)",
        ["red-500"] = "rgb(239,68,68)",
        ["red-600"] = "rgb(220,38,38)",
        ["red-700"] = "rgb(185,28,28)",
        ["red-800"] = "rgb(153,27,27)",
        ["red-900"] = "rgb(127,29,29)",
        ["red-950"] = "rgb(69,10,10)",
        ["orange"] = "rgb(253,186,116)",
        ["orange-50"] = "rgb(255,247,237)",
        ["orange-100"] = "rgb(255,237,213)",
        ["orange-200"] = "rgb(254,215,170)",
        ["orange-300"] = "rgb(253,186,116)",
        ["orange-400"] = "rgb(251,146,60)",
        ["orange-500"] = "rgb(249,115,22)",
        ["orange-600"] = "rgb(234,88,12)",
        ["orange-700"] = "rgb(194,65,12)",
        ["orange-800"] = "rgb(154,52,18)",
        ["orange-900"] = "rgb(124,45,18)",
        ["orange-950"] = "rgb(67,20,7)",
        ["amber"] = "rgb(252,211,77)",
        ["amber-50"] = "rgb(255,251,235)",
        ["amber-100"] = "rgb(254,243,199)",
        ["amber-200"] = "rgb(253,230,138)",
        ["amber-300"] = "rgb(252,211,77)",
        ["amber-400"] = "rgb(251,191,36)",
        ["amber-500"] = "rgb(245,158,11)",
        ["amber-600"] = "rgb(217,119,6)",
        ["amber-700"] = "rgb(180,83,9)",
        ["amber-800"] = "rgb(146,64,14)",
        ["amber-900"] = "rgb(120,53,15)",
        ["amber-950"] = "rgb(69,26,3)",
        ["yellow"] = "rgb(253,224,71)",
        ["yellow-50"] = "rgb(254,252,232)",
        ["yellow-100"] = "rgb(254,249,195)",
        ["yellow-200"] = "rgb(254,240,138)",
        ["yellow-300"] = "rgb(253,224,71)",
        ["yellow-400"] = "rgb(250,204,21)",
        ["yellow-500"] = "rgb(234,179,8)",
        ["yellow-600"] = "rgb(202,138,4)",
        ["yellow-700"] = "rgb(161,98,7)",
        ["yellow-800"] = "rgb(133,77,14)",
        ["yellow-900"] = "rgb(113,63,18)",
        ["yellow-950"] = "rgb(66,32,6)",
        ["lime"] = "rgb(190,242,100)",
        ["lime-50"] = "rgb(247,254,231)",
        ["lime-100"] = "rgb(236,252,203)",
        ["lime-200"] = "rgb(217,249,157)",
        ["lime-300"] = "rgb(190,242,100)",
        ["lime-400"] = "rgb(163,230,53)",
        ["lime-500"] = "rgb(132,204,22)",
        ["lime-600"] = "rgb(101,163,13)",
        ["lime-700"] = "rgb(77,124,15)",
        ["lime-800"] = "rgb(63,98,18)",
        ["lime-900"] = "rgb(54,83,20)",
        ["lime-950"] = "rgb(26,46,5)",
        ["green"] = "rgb(134,239,172)",
        ["green-50"] = "rgb(240,253,244)",
        ["green-100"] = "rgb(220,252,231)",
        ["green-200"] = "rgb(187,247,208)",
        ["green-300"] = "rgb(134,239,172)",
        ["green-400"] = "rgb(74,222,128)",
        ["green-500"] = "rgb(34,197,94)",
        ["green-600"] = "rgb(22,163,74)",
        ["green-700"] = "rgb(21,128,61)",
        ["green-800"] = "rgb(22,101,52)",
        ["green-900"] = "rgb(20,83,45)",
        ["green-950"] = "rgb(5,46,22)",
        ["emerald"] = "rgb(110,231,183)",
        ["emerald-50"] = "rgb(236,253,245)",
        ["emerald-100"] = "rgb(209,250,229)",
        ["emerald-200"] = "rgb(167,243,208)",
        ["emerald-300"] = "rgb(110,231,183)",
        ["emerald-400"] = "rgb(52,211,153)",
        ["emerald-500"] = "rgb(16,185,129)",
        ["emerald-600"] = "rgb(5,150,105)",
        ["emerald-700"] = "rgb(4,120,87)",
        ["emerald-800"] = "rgb(6,95,70)",
        ["emerald-900"] = "rgb(6,78,59)",
        ["emerald-950"] = "rgb(2,44,34)",
        ["teal"] = "rgb(94,234,212)",
        ["teal-50"] = "rgb(240,253,250)",
        ["teal-100"] = "rgb(204,251,241)",
        ["teal-200"] = "rgb(153,246,228)",
        ["teal-300"] = "rgb(94,234,212)",
        ["teal-400"] = "rgb(45,212,191)",
        ["teal-500"] = "rgb(20,184,166)",
        ["teal-600"] = "rgb(13,148,136)",
        ["teal-700"] = "rgb(15,118,110)",
        ["teal-800"] = "rgb(17,94,89)",
        ["teal-900"] = "rgb(19,78,74)",
        ["teal-950"] = "rgb(4,47,46)",
        ["cyan"] = "rgb(103,232,249)",
        ["cyan-50"] = "rgb(236,254,255)",
        ["cyan-100"] = "rgb(207,250,254)",
        ["cyan-200"] = "rgb(165,243,252)",
        ["cyan-300"] = "rgb(103,232,249)",
        ["cyan-400"] = "rgb(34,211,238)",
        ["cyan-500"] = "rgb(6,182,212)",
        ["cyan-600"] = "rgb(8,145,178)",
        ["cyan-700"] = "rgb(14,116,144)",
        ["cyan-800"] = "rgb(21,94,117)",
        ["cyan-900"] = "rgb(22,78,99)",
        ["cyan-950"] = "rgb(8,51,68)",
        ["sky"] = "rgb(125,211,252)",
        ["sky-50"] = "rgb(240,249,255)",
        ["sky-100"] = "rgb(224,242,254)",
        ["sky-200"] = "rgb(186,230,253)",
        ["sky-300"] = "rgb(125,211,252)",
        ["sky-400"] = "rgb(56,189,248)",
        ["sky-500"] = "rgb(14,165,233)",
        ["sky-600"] = "rgb(2,132,199)",
        ["sky-700"] = "rgb(3,105,161)",
        ["sky-800"] = "rgb(7,89,133)",
        ["sky-900"] = "rgb(12,74,110)",
        ["sky-950"] = "rgb(8,47,73)",
        ["blue"] = "rgb(147,197,253)",
        ["blue-50"] = "rgb(239,246,255)",
        ["blue-100"] = "rgb(219,234,254)",
        ["blue-200"] = "rgb(191,219,254)",
        ["blue-300"] = "rgb(147,197,253)",
        ["blue-400"] = "rgb(96,165,250)",
        ["blue-500"] = "rgb(59,130,246)",
        ["blue-600"] = "rgb(37,99,235)",
        ["blue-700"] = "rgb(29,78,216)",
        ["blue-800"] = "rgb(30,64,175)",
        ["blue-900"] = "rgb(30,58,138)",
        ["blue-950"] = "rgb(23,37,84)",
        ["indigo"] = "rgb(165,180,252)",
        ["indigo-50"] = "rgb(238,242,255)",
        ["indigo-100"] = "rgb(224,231,255)",
        ["indigo-200"] = "rgb(199,210,254)",
        ["indigo-300"] = "rgb(165,180,252)",
        ["indigo-400"] = "rgb(129,140,248)",
        ["indigo-500"] = "rgb(99,102,241)",
        ["indigo-600"] = "rgb(79,70,229)",
        ["indigo-700"] = "rgb(67,56,202)",
        ["indigo-800"] = "rgb(55,48,163)",
        ["indigo-900"] = "rgb(49,46,129)",
        ["indigo-950"] = "rgb(30,27,75)",
        ["violet"] = "rgb(196,181,253)",
        ["violet-50"] = "rgb(245,243,255)",
        ["violet-100"] = "rgb(237,233,254)",
        ["violet-200"] = "rgb(221,214,254)",
        ["violet-300"] = "rgb(196,181,253)",
        ["violet-400"] = "rgb(167,139,250)",
        ["violet-500"] = "rgb(139,92,246)",
        ["violet-600"] = "rgb(124,58,237)",
        ["violet-700"] = "rgb(109,40,217)",
        ["violet-800"] = "rgb(91,33,182)",
        ["violet-900"] = "rgb(76,29,149)",
        ["violet-950"] = "rgb(46,16,101)",
        ["purple"] = "rgb(216,180,254)",
        ["purple-50"] = "rgb(250,245,255)",
        ["purple-100"] = "rgb(243,232,255)",
        ["purple-200"] = "rgb(233,213,255)",
        ["purple-300"] = "rgb(216,180,254)",
        ["purple-400"] = "rgb(192,132,252)",
        ["purple-500"] = "rgb(168,85,247)",
        ["purple-600"] = "rgb(147,51,234)",
        ["purple-700"] = "rgb(126,34,206)",
        ["purple-800"] = "rgb(107,33,168)",
        ["purple-900"] = "rgb(88,28,135)",
        ["purple-950"] = "rgb(59,7,100)",
        ["fuchsia"] = "rgb(240,171,252)",
        ["fuchsia-50"] = "rgb(253,244,255)",
        ["fuchsia-100"] = "rgb(250,232,255)",
        ["fuchsia-200"] = "rgb(245,208,254)",
        ["fuchsia-300"] = "rgb(240,171,252)",
        ["fuchsia-400"] = "rgb(232,121,249)",
        ["fuchsia-500"] = "rgb(217,70,239)",
        ["fuchsia-600"] = "rgb(192,38,211)",
        ["fuchsia-700"] = "rgb(162,28,175)",
        ["fuchsia-800"] = "rgb(134,25,143)",
        ["fuchsia-900"] = "rgb(112,26,117)",
        ["fuchsia-950"] = "rgb(74,4,78)",
        ["pink"] = "rgb(249,168,212)",
        ["pink-50"] = "rgb(253,242,248)",
        ["pink-100"] = "rgb(252,231,243)",
        ["pink-200"] = "rgb(251,207,232)",
        ["pink-300"] = "rgb(249,168,212)",
        ["pink-400"] = "rgb(244,114,182)",
        ["pink-500"] = "rgb(236,72,153)",
        ["pink-600"] = "rgb(219,39,119)",
        ["pink-700"] = "rgb(190,24,93)",
        ["pink-800"] = "rgb(157,23,77)",
        ["pink-900"] = "rgb(131,24,67)",
        ["pink-950"] = "rgb(80,7,36)",
        ["rose"] = "rgb(253,164,175)",
        ["rose-50"] = "rgb(255,241,242)",
        ["rose-100"] = "rgb(255,228,230)",
        ["rose-200"] = "rgb(254,205,211)",
        ["rose-300"] = "rgb(253,164,175)",
        ["rose-400"] = "rgb(251,113,133)",
        ["rose-500"] = "rgb(244,63,94)",
        ["rose-600"] = "rgb(225,29,72)",
        ["rose-700"] = "rgb(190,18,60)",
        ["rose-800"] = "rgb(159,18,57)",
        ["rose-900"] = "rgb(136,19,55)",
        ["rose-950"] = "rgb(76,5,25)"
    };
    
    public Dictionary<string,string> FractionOptions { get; } = new()
    {
	    ["1/2"] = "50%",
	    ["1/3"] = "33.333333%",
	    ["2/3"] = "66.666667%",
	    ["1/4"] = "25%",
	    ["2/4"] = "50%",
	    ["3/4"] = "75%",
	    ["1/5"] = "20%",
	    ["2/5"] = "40%",
	    ["3/5"] = "60%",
	    ["4/5"] = "80%",
	    ["1/6"] = "16.666667%",
	    ["2/6"] = "33.333333%",
	    ["3/6"] = "50%",
	    ["4/6"] = "66.666667%",
	    ["5/6"] = "83.333333%",
	    ["1/12"] = "8.333333%",
	    ["2/12"] = "16.666667%",
	    ["3/12"] = "25%",
	    ["4/12"] = "33.333333%",
	    ["5/12"] = "41.666667%",
	    ["6/12"] = "50%",
	    ["7/12"] = "58.333333%",
	    ["8/12"] = "66.666667%",
	    ["9/12"] = "75%",
	    ["10/12"] = "83.333333%",
	    ["11/12"] = "91.666667%",
	    ["full"] = "100%"
    };
	
    public Dictionary<string,string> TextSizeOptions { get; } = new()
    {
	    ["xs"] = "0.75rem",
	    ["sm"] = "0.875rem",
	    ["base"] = "1rem",
	    ["lg"] = "1.125rem",
	    ["xl"] = "1.25rem",
	    ["2xl"] = "1.5rem",
	    ["3xl"] = "1.875rem",
	    ["4xl"] = "2.25rem",
	    ["5xl"] = "3rem",
	    ["6xl"] = "3.75rem",
	    ["7xl"] = "4.5rem",
	    ["8xl"] = "6rem",
	    ["9xl"] = "8rem"
    };

    public Dictionary<string,string> LeadingOptions { get; } = new()
    {
	    ["3"] = "0.75rem",
	    ["4"] = "1rem",
	    ["5"] = "1.25rem",
	    ["6"] = "1.5rem",
	    ["7"] = "1.75rem",
	    ["8"] = "2rem",
	    ["9"] = "2.25rem",
	    ["10"] = "2.5rem",
	    ["none"] = "1",
	    ["tight"] = "1.25",
	    ["snug"] = "1.375",
	    ["normal"] = "1.5",
	    ["relaxed"] = "1.625",
	    ["loose"] = "2"
    };
    
    public Dictionary<string, string> RoundedOptions { get; } = new()
    {
	    [""] = "0.25rem",
	    ["none"] = "0px",
	    ["sm"] = "0.125rem",
	    ["md"] = "0.375rem",
	    ["lg"] = "0.5rem",
	    ["xl"] = "0.75rem",
	    ["2xl"] = "1rem",
	    ["3xl"] = "1.5rem",
	    ["full"] = "9999px"
    };
    
    public Dictionary<string, string> BorderWidthOptions { get; } = new()
    {
	    [""] = "1px",
	    ["0"] = "0px",
	    ["2"] = 2.PxToRem(),
	    ["4"] = 4.PxToRem(),
	    ["8"] = 8.PxToRem()
    };
   
    public Dictionary<string, string> DivideWidthOptions { get; } = new()
    {
	    [""] = "1px",
	    ["0"] = "0px",
	    ["2"] = 2.PxToRem(),
	    ["4"] = 4.PxToRem(),
	    ["8"] = 8.PxToRem()
    };
    
    public Dictionary<string, string> BlendModeOptions { get; } = new()
    {
	    ["normal"] = "normal",
	    ["multiply"] = "multiply",
	    ["screen"] = "multiply",
	    ["overlay"] = "overlay",
	    ["darken"] = "darken",
	    ["lighten"] = "lighten",
	    ["color-dodge"] = "color-dodge",
	    ["color-burn"] = "color-burn",
	    ["hard-light"] = "hard-light",
	    ["soft-light"] = "soft-light",
	    ["difference"] = "difference",
	    ["exclusion"] = "exclusion",
	    ["hue"] = "hue",
	    ["saturation"] = "saturation",
	    ["color"] = "color",
	    ["luminosity"] = "luminosity",
	    ["plus-lighter"] = "plus-lighter"
    };

    public Dictionary<string, string> LayoutRemUnitOptions { get; set; } = new();
    public Dictionary<string, string> LayoutWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> TypographyRemUnitOptions { get; set; } = new();
    public Dictionary<string, string> EffectsFiltersOneBasedPercentageOptions { get; set; } = new();
    public Dictionary<string, string> FlexboxAndGridWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> FlexboxAndGridNegativeWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> VerbatrimFractionOptions { get; set; } = new();
    
    #endregion
    
    #region CSS Constants
    
	public IEnumerable<string> ArbitraryValueTypes { get; } = new[]
	{
		"string", "url", "custom-ident", "dashed-ident",
		"integer", "number", "percentage", "ratio", "flex",
		"length", "angle", "time", "frequency", "resolution",
		"color"
	};

	public IEnumerable<string> CssUnits { get; } = new[]
	{
		// Order here matters as truncating values like 'em' also work on values ending with 'rem'

		"rem", "vmin", "vmax",
		"cm", "in", "mm", "pc", "pt", "px",
		"ch", "em", "ex", "vw", "vh"
	};

	public IEnumerable<string> CssAngleUnits { get; } = new[]
	{
		// Order here matters as truncating values like 'rad' also work on values ending with 'grad'

		"grad", "turn", "deg", "rad"
	};

	public IEnumerable<string> CssTimeUnits { get; } = new[]
	{
		// Order here matters as truncating values like 's' also work on values ending with 'ms'
		
		"ms", "s"
	};

	public IEnumerable<string> CssFrequencyUnits { get; } = new[]
	{
		// Order here matters as truncating values like 'Hz' also work on values ending with 'kHz'
		
		"kHz", "Hz"
	};

	public IEnumerable<string> CssResolutionUnits { get; } = new[]
	{
		// Order here matters as truncating values like 'x' also work on values ending with 'dppx'
		
		"dpcm", "dppx", "dpi", "x"
	};
	
	public IEnumerable<string> CssNamedColors { get; } = new[]
	{
	    "aliceblue",
	    "antiquewhite",
	    "aqua",
	    "aquamarine",
	    "azure",
	    "beige",
	    "bisque",
	    "black",
	    "blanchedalmond",
	    "blue",
	    "blueviolet",
	    "brown",
	    "burlywood",
	    "cadetblue",
	    "chartreuse",
	    "chocolate",
	    "coral",
	    "cornflowerblue",
	    "cornsilk",
	    "crimson",
	    "cyan",
	    "darkblue",
	    "darkcyan",
	    "darkgoldenrod",
	    "darkgray",
	    "darkgreen",
	    "darkgrey",
	    "darkkhaki",
	    "darkmagenta",
	    "darkolivegreen",
	    "darkorange",
	    "darkorchid",
	    "darkred",
	    "darksalmon",
	    "darkseagreen",
	    "darkslateblue",
	    "darkslategray",
	    "darkslategrey",
	    "darkturquoise",
	    "darkviolet",
	    "deeppink",
	    "deepskyblue",
	    "dimgray",
	    "dimgrey",
	    "dodgerblue",
	    "firebrick",
	    "floralwhite",
	    "forestgreen",
	    "fuchsia",
	    "gainsboro",
	    "ghostwhite",
	    "gold",
	    "goldenrod",
	    "gray",
	    "green",
	    "greenyellow",
	    "grey",
	    "honeydew",
	    "hotpink",
	    "indianred",
	    "indigo",
	    "ivory",
	    "khaki",
	    "lavender",
	    "lavenderblush",
	    "lawngreen",
	    "lemonchiffon",
	    "lightblue",
	    "lightcoral",
	    "lightcyan",
	    "lightgoldenrodyellow",
	    "lightgray",
	    "lightgreen",
	    "lightgrey",
	    "lightpink",
	    "lightsalmon",
	    "lightseagreen",
	    "lightskyblue",
	    "lightslategray",
	    "lightslategrey",
	    "lightsteelblue",
	    "lightyellow",
	    "lime",
	    "limegreen",
	    "linen",
	    "magenta",
	    "maroon",
	    "mediumaquamarine",
	    "mediumblue",
	    "mediumorchid",
	    "mediumpurple",
	    "mediumseagreen",
	    "mediumslateblue",
	    "mediumspringgreen",
	    "mediumturquoise",
	    "mediumvioletred",
	    "midnightblue",
	    "mintcream",
	    "mistyrose",
	    "moccasin",
	    "navajowhite",
	    "navy",
	    "oldlace",
	    "olive",
	    "olivedrab",
	    "orange",
	    "orangered",
	    "orchid",
	    "palegoldenrod",
	    "palegreen",
	    "paleturquoise",
	    "palevioletred",
	    "papayawhip",
	    "peachpuff",
	    "peru",
	    "pink",
	    "plum",
	    "powderblue",
	    "purple",
	    "rebeccapurple",
	    "red",
	    "rosybrown",
	    "royalblue",
	    "saddlebrown",
	    "salmon",
	    "sandybrown",
	    "seagreen",
	    "seashell",
	    "sienna",
	    "silver",
	    "skyblue",
	    "slateblue",
	    "slategray",
	    "slategrey",
	    "snow",
	    "springgreen",
	    "steelblue",
	    "tan",
	    "teal",
	    "thistle",
	    "tomato",
	    "turquoise",
	    "violet",
	    "wheat",
	    "white",
	    "whitesmoke",
	    "yellow",
	    "yellowgreen"
	};
	
	public IEnumerable<string> CssPropertyNames { get; } = new[]
	{
	    "align-content",
	    "align-items",
	    "align-self",
	    "all",
	    "animation",
	    "animation-delay",
	    "animation-direction",
	    "animation-duration",
	    "animation-fill-mode",
	    "animation-iteration-count",
	    "animation-name",
	    "animation-play-state",
	    "animation-timing-function",
	    "appearance",
	    "backdrop-filter",
	    "backface-visibility",
	    "background",
	    "background-attachment",
	    "background-blend-mode",
	    "background-clip",
	    "background-color",
	    "background-image",
	    "background-origin",
	    "background-position",
	    "background-position-x",
	    "background-position-y",
	    "background-repeat",
	    "background-size",
	    "block-size",
	    "border",
	    "border-block",
	    "border-block-color",
	    "border-block-end",
	    "border-block-end-color",
	    "border-block-end-style",
	    "border-block-end-width",
	    "border-block-start",
	    "border-block-start-color",
	    "border-block-start-style",
	    "border-block-start-width",
	    "border-block-style",
	    "border-block-width",
	    "border-bottom",
	    "border-bottom-color",
	    "border-bottom-left-radius",
	    "border-bottom-right-radius",
	    "border-bottom-style",
	    "border-bottom-width",
	    "border-collapse",
	    "border-color",
	    "border-image",
	    "border-image-outset",
	    "border-image-repeat",
	    "border-image-slice",
	    "border-image-source",
	    "border-image-width",
	    "border-inline",
	    "border-inline-color",
	    "border-inline-end",
	    "border-inline-end-color",
	    "border-inline-end-style",
	    "border-inline-end-width",
	    "border-inline-start",
	    "border-inline-start-color",
	    "border-inline-start-style",
	    "border-inline-start-width",
	    "border-inline-style",
	    "border-inline-width",
	    "border-left",
	    "border-left-color",
	    "border-left-style",
	    "border-left-width",
	    "border-radius",
	    "border-right",
	    "border-right-color",
	    "border-right-style",
	    "border-right-width",
	    "border-spacing",
	    "border-style",
	    "border-top",
	    "border-top-color",
	    "border-top-left-radius",
	    "border-top-right-radius",
	    "border-top-style",
	    "border-top-width",
	    "border-width",
	    "bottom",
	    "box-decoration-break",
	    "box-shadow",
	    "box-sizing",
	    "break-after",
	    "break-before",
	    "break-inside",
	    "caption-side",
	    "caret-color",
	    "clear",
	    "clip",
	    "clip-path",
	    "color",
	    "color-adjust",
	    "column-count",
	    "column-fill",
	    "column-gap",
	    "column-rule",
	    "column-rule-color",
	    "column-rule-style",
	    "column-rule-width",
	    "columns",
	    "column-span",
	    "column-width",
	    "contain",
	    "content",
	    "counter-increment",
	    "counter-reset",
	    "cursor",
	    "direction",
	    "display",
	    "empty-cells",
	    "filter",
	    "flex",
	    "flex-basis",
	    "flex-direction",
	    "flex-flow",
	    "flex-grow",
	    "flex-shrink",
	    "flex-wrap",
	    "float",
	    "font",
	    "font-family",
	    "font-feature-settings",
	    "font-kerning",
	    "font-language-override",
	    "font-optical-sizing",
	    "font-size",
	    "font-size-adjust",
	    "font-stretch",
	    "font-style",
	    "font-synthesis",
	    "font-variant",
	    "font-variant-alternates",
	    "font-variant-caps",
	    "font-variant-east-asian",
	    "font-variant-ligatures",
	    "font-variant-numeric",
	    "font-variant-position",
	    "font-weight",
	    "gap",
	    "grid",
	    "grid-area",
	    "grid-auto-columns",
	    "grid-auto-flow",
	    "grid-auto-rows",
	    "grid-column",
	    "grid-column-end",
	    "grid-column-start",
	    "grid-row",
	    "grid-row-end",
	    "grid-row-start",
	    "grid-template",
	    "grid-template-areas",
	    "grid-template-columns",
	    "grid-template-rows",
	    "hanging-punctuation",
	    "height",
	    "hyphens",
	    "image-orientation",
	    "image-rendering",
	    "image-resolution",
	    "inset",
	    "isolation",
	    "justify-content",
	    "justify-items",
	    "justify-self",
	    "left",
	    "letter-spacing",
	    "line-break",
	    "line-height",
	    "list-style",
	    "list-style-image",
	    "list-style-position",
	    "list-style-type",
	    "margin",
	    "margin-block",
	    "margin-block-end",
	    "margin-block-start",
	    "margin-bottom",
	    "margin-inline",
	    "margin-inline-end",
	    "margin-inline-start",
	    "margin-left",
	    "margin-right",
	    "margin-top",
	    "mask",
	    "mask-border",
	    "mask-border-mode",
	    "mask-border-outset",
	    "mask-border-repeat",
	    "mask-border-slice",
	    "mask-border-source",
	    "mask-border-width",
	    "mask-clip",
	    "mask-composite",
	    "mask-image",
	    "mask-mode",
	    "mask-origin",
	    "mask-position",
	    "mask-repeat",
	    "mask-size",
	    "mask-type",
	    "max-block-size",
	    "max-height",
	    "max-inline-size",
	    "max-width",
	    "min-block-size",
	    "min-height",
	    "min-inline-size",
	    "min-width",
	    "mix-blend-mode",
	    "object-fit",
	    "object-position",
	    "opacity",
	    "order",
	    "orphans",
	    "outline",
	    "outline-color",
	    "outline-offset",
	    "outline-style",
	    "outline-width",
	    "overflow",
	    "overflow-anchor",
	    "overflow-block",
	    "overflow-clip-box",
	    "overflow-inline",
	    "overflow-wrap",
	    "overflow-x",
	    "overflow-y",
	    "overscroll-behavior",
	    "overscroll-behavior-block",
	    "overscroll-behavior-inline",
	    "overscroll-behavior-x",
	    "overscroll-behavior-y",
	    "padding",
	    "padding-block",
	    "padding-block-end",
	    "padding-block-start",
	    "padding-bottom",
	    "padding-inline",
	    "padding-inline-end",
	    "padding-inline-start",
	    "padding-left",
	    "padding-right",
	    "padding-top",
	    "page-break-after",
	    "page-break-before",
	    "page-break-inside",
	    "perspective",
	    "perspective-origin",
	    "place-content",
	    "place-items",
	    "place-self",
	    "pointer-events",
	    "position",
	    "quotes",
	    "resize",
	    "right",
	    "rotate",
	    "row-gap",
	    "ruby-align",
	    "ruby-position",
	    "scale",
	    "scroll-behavior",
	    "scroll-margin",
	    "scroll-margin-block",
	    "scroll-margin-block-end",
	    "scroll-margin-block-start",
	    "scroll-margin-bottom",
	    "scroll-margin-inline",
	    "scroll-margin-inline-end",
	    "scroll-margin-inline-start",
	    "scroll-margin-left",
	    "scroll-margin-right",
	    "scroll-margin-top",
	    "scroll-padding",
	    "scroll-padding-block",
	    "scroll-padding-block-end",
	    "scroll-padding-block-start",
	    "scroll-padding-bottom",
	    "scroll-padding-inline",
	    "scroll-padding-inline-end",
	    "scroll-padding-inline-start",
	    "scroll-padding-left",
	    "scroll-padding-right",
	    "scroll-padding-top",
	    "scroll-snap-align",
	    "scroll-snap-margin",
	    "scroll-snap-margin-bottom",
	    "scroll-snap-margin-left",
	    "scroll-snap-margin-right",
	    "scroll-snap-margin-top",
	    "scroll-snap-stop",
	    "scroll-snap-type",
	    "shape-image-threshold",
	    "shape-margin",
	    "shape-outside",
	    "tab-size",
	    "table-layout",
	    "text-align",
	    "text-align-last",
	    "text-combine-upright",
	    "text-decoration",
	    "text-decoration-color",
	    "text-decoration-line",
	    "text-decoration-skip",
	    "text-decoration-skip-ink",
	    "text-decoration-style",
	    "text-decoration-thickness",
	    "text-emphasis",
	    "text-emphasis-color",
	    "text-emphasis-position",
	    "text-emphasis-style",
	    "text-indent",
	    "text-justify",
	    "text-orientation",
	    "text-overflow",
	    "text-rendering",
	    "text-shadow",
	    "text-transform",
	    "text-underline-offset",
	    "text-underline-position",
	    "top",
	    "touch-action",
	    "transform",
	    "transform-box",
	    "transform-origin",
	    "transform-style",
	    "transition",
	    "transition-delay",
	    "transition-duration",
	    "transition-property",
	    "transition-timing-function",
	    "translate",
	    "unicode-bidi",
	    "user-select",
	    "vertical-align",
	    "visibility",
	    "white-space",
	    "widows",
	    "width",
	    "will-change",
	    "word-break",
	    "word-spacing",
	    "word-wrap",
	    "writing-mode",
	    "z-index",
	    "zoom"
	};
	
	public Dictionary<string,string> PseudoclassPrefixes { get; } = new ()
	{
		{ "hover", "&:hover {"},
		{ "focus", "&:focus {" },
		{ "focus-within", "&:focus-within {" },
		{ "focus-visible", "&:focus-visible {" },
		{ "active", "&:active {" },
		{ "visited", "&:visited {" },
		{ "target", "&:target {" },
		{ "first", "&:first-child {" },
		{ "last", "&:last-child {" },
		{ "only", "&:only-child {" },
		{ "odd", "&:nth-child(odd) {" },
		{ "even", "&:nth-child(even) {" },
		{ "first-of-type", "&:first-of-type {" },
		{ "last-of-type", "&:last-of-type {" },
		{ "only-of-type", "&:only-of-type {" },
		{ "empty", "&:empty {" },
		{ "disabled", "&:disabled {" },
		{ "enabled", "&:enabled {" },
		{ "checked", "&:checked {" },
		{ "indeterminate", "&:indeterminate {" },
		{ "default", "&:default {" },
		{ "required", "&:required {" },
		{ "valid", "&:valid {" },
		{ "invalid", "&:invalid {" },
		{ "in-range", "&:in-range {" },
		{ "out-of-range", "&:out-of-range {" },
		{ "placeholder-shown", "&:placeholder-shown {" },
		{ "autofill", "&:autofill {" },
		{ "read-only", "&:read-only {" },
		{ "before", "&::before {" },
		{ "after", "&::after {" },
		{ "first-letter", "&::first-letter {" },
		{ "first-line", "&::first-line {" },
		{ "marker", "&::marker {" },
		{ "selection", "&::selection {" },
		{ "file", "&::file-selector-button {" },
		{ "backdrop", "&::backdrop {" },
		{ "placeholder", "&::placeholder {" }
	};
	
	#endregion
	
    #region Regex Properties
    
    public Regex ArbitraryCssRegex { get; }
    public Regex CoreClassRegex { get; }
    public Regex SfumatoScssIncludesRegex { get; }
    
    #endregion
    
    #region Run Mode Properties

    public bool Minify { get; set; }
    public bool WatchMode { get; set; }
    public bool VersionMode { get; set; }
    public bool HelpMode { get; set; }
    public bool DiagnosticMode { get; set; }
	
    #endregion
    
    #region Collection Properties

    public List<string> CliArguments { get; } = new();
    public ConcurrentDictionary<string,WatchedFile> WatchedFiles { get; } = new();
    public ConcurrentDictionary<string,WatchedScssFile> WatchedScssFiles { get; } = new();
    public ConcurrentDictionary<string,UsedScssClass> UsedClasses { get; } = new();
    public ConcurrentDictionary<string,ScssUtilityClassGroupBase> UtilityClassCollection { get; } = new();
    
    #endregion
    
    #region App State Properties

    public static string CliErrorPrefix => "Sfumato => ";
    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public SfumatoJsonSettings Settings { get; set; } = new();
    public ConcurrentDictionary<string,string> DiagnosticOutput { get; set; } = new();
    public string WorkingPathOverride { get; set; } = string.Empty;
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public string SfumatoScssOutputPath { get; set; } = string.Empty;
    public List<string> AllPrefixes { get; } = new();
    public StringBuilder ScssCoreInjectable { get; } = new();
    public StringBuilder ScssSharedInjectable { get; } = new();
    
    #endregion

    public SfumatoAppState()
    {
	    AllPrefixes.Clear();
	    AllPrefixes.AddRange(MediaQueryPrefixes.Select(p => p.Prefix));
	    AllPrefixes.AddRange(PseudoclassPrefixes.Select(p => p.Key));
	    
	    #region Establish Theme Dictionaries

	    LayoutRemUnitOptions.AddNumberedRemUnitOptions(0.5m, 96m);
	    LayoutWholeNumberOptions.AddWholeNumberOptions(1, 24);
	    TypographyRemUnitOptions.AddNumberedRemUnitOptions(3m, 10m, 1m);
	    EffectsFiltersOneBasedPercentageOptions.AddOneBasedPercentageOptions(0m, 200m);
	    FlexboxAndGridWholeNumberOptions.AddWholeNumberOptions(1, 25);
	    FlexboxAndGridNegativeWholeNumberOptions.AddWholeNumberOptions(1, 25, true);
	    VerbatrimFractionOptions.AddVerbatimFractionOptions(FractionOptions);
	    
	    #endregion
	    
	    #region Regular Expressions
	    
	    var arbitraryCssExpression = $$"""
(?<=[\s"'`])
({{string.Join("\\:|", AllPrefixes) + "\\:"}}){0,10}
([\!]{0,1}\[({{string.Join("|", CssPropertyNames)}})\:[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\])
(?=[\s"'`])
""";
	    
	    ArbitraryCssRegex = new Regex(arbitraryCssExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    var coreClassExpression = $$"""
(?<=[\s"'`])
({{string.Join("\\:|", AllPrefixes) + "\\:"}}){0,10}
(
	([\!]{0,1}[a-z\-][a-z0-9\-\.%]{2,100})
	(
		(/[a-z0-9\-\.]{1,100})|([/]{0,1}\[[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,100}\]){0,1}
	)
)
(?=[\s"'`])
""";
	    
	    CoreClassRegex = new Regex(coreClassExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);

	    const string sfumatoScssIncludesRegexExpression = @"^\s*@sfumato\s+((core)|(base))[\s]{0,};\r?\n";
	    
	    SfumatoScssIncludesRegex = new Regex(sfumatoScssIncludesRegexExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    #endregion
   }
    
    #region Entry Points
    
    /// <summary>
    /// Initialize the app state. Loads settings JSON file from working path.
    /// Sets up runtime environment for the runner.
    /// </summary>
    /// <param name="args">CLI arguments</param>
    public async Task InitializeAsync(IEnumerable<string> args)
    {
	    var timer = new Stopwatch();

	    timer.Start();

	    await ProcessCliArgumentsAsync(args);

	    if (VersionMode || HelpMode)
		    return;
	    
	    #region Find sfumato.json file

        if (string.IsNullOrEmpty(WorkingPathOverride) == false)
            WorkingPath = WorkingPathOverride;
        
        SettingsFilePath = Path.Combine(WorkingPath, "sfumato.json");

        if (File.Exists(SettingsFilePath) == false)
        {
            await Console.Out.WriteLineAsync($"Could not find sfumato.json settings file at path {WorkingPath}");
            await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
            Environment.Exit(1);
        }

        #endregion
        
        try
        {
            #region Load sfumato.json file

            var json = await File.ReadAllTextAsync(SettingsFilePath);
            var jsonSettings = JsonSerializer.Deserialize<SfumatoJsonSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IncludeFields = true
            });
		
            if (jsonSettings is null)
            {
                await Console.Out.WriteLineAsync($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
                Environment.Exit(1);
            }
            
            #endregion
            
            #region Import settings
            
            Settings.CssOutputPath = Path.Combine(WorkingPath, jsonSettings.CssOutputPath.SetNativePathSeparators());
        
            if (Directory.Exists(Settings.CssOutputPath) == false)
            {
                await Console.Out.WriteLineAsync($"{CliErrorPrefix}Could not find CSS output path: {Settings.CssOutputPath}");
                Environment.Exit(1);
            }
        
            Settings.ThemeMode = jsonSettings.ThemeMode switch
            {
                "system" => "system",
                "class" => "class",
                _ => "system"
            };

            Settings.UseAutoTheme = jsonSettings.UseAutoTheme;
            
            SassCliPath = await GetEmbeddedSassPathAsync();
            ScssPath = await GetEmbeddedScssPathAsync();

            Settings.ProjectPaths.Clear();
        
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
	            if (string.IsNullOrEmpty(projectPath.FileSpec))
		            continue;
	            
                projectPath.Path = Path.Combine(WorkingPath, projectPath.Path.SetNativePathSeparators());
                
                if (projectPath.FileSpec.Contains('.') && projectPath.FileSpec.StartsWith("*", StringComparison.Ordinal) == false)
                {
	                projectPath.IsFilePath = true;
	                Settings.ProjectPaths.Add(projectPath);

	                continue;
                }
                
                var tempFileSpec = projectPath.FileSpec.Replace("*", string.Empty).Replace(".", string.Empty);

                if (string.IsNullOrEmpty(tempFileSpec) == false)
                    projectPath.FileSpec = $"*.{tempFileSpec}";
            
                Settings.ProjectPaths.Add(projectPath);
            }

            jsonSettings.Breakpoints.Adapt(Settings.Breakpoints);
            jsonSettings.FontSizeUnits.Adapt(Settings.FontSizeUnits);
            
            #endregion
        }

        catch
        {
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        SfumatoScssOutputPath = Path.Combine(Settings.CssOutputPath, "sfumato.scss");
        
        if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0", $"Initialized settings in {timer.FormatTimer()}{Environment.NewLine}");

        ScssCoreInjectable.Clear();
        ScssCoreInjectable.Append(await SfumatoScss.GetCoreScssAsync(this, DiagnosticOutput));

        ScssSharedInjectable.Clear();
        ScssSharedInjectable.Append(await SfumatoScss.GetSharedScssAsync(this, DiagnosticOutput));
        
        #region Load Utility Classes

        ScssUtilityClassGroupBase utilityClassGroup;

        utilityClassGroup = new Bg();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        #endregion
        
        if (DiagnosticMode)
	        DiagnosticOutput.TryAdd("init1", $"Loaded utility classes in {timer.FormatTimer()}{Environment.NewLine}");
    }
    
    #endregion

	#region Initialization Methods
	
	/// <summary>
	/// Process CLI arguments and set properties accordingly.
	/// </summary>
	/// <param name="args"></param>
	public async Task ProcessCliArgumentsAsync(IEnumerable<string>? args)
	{
		CliArguments.Clear();
		CliArguments.AddRange(args?.ToList() ?? new List<string>());

		if (CliArguments.Count < 1)
			return;

		if (CliArguments.Count == 0)
		{
			HelpMode = true;
		}

		else
		{
			if (CliArguments[0] != "help" && CliArguments[0] != "version" && CliArguments[0] != "build" && CliArguments[0] != "watch")
			{
				await Console.Out.WriteLineAsync("Invalid command specified; must be: help, version, build, or watch");
				await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
				Environment.Exit(1);
			}			
			
			if (CliArguments[0] == "help")
			{
				HelpMode = true;
				return;
			}

			if (CliArguments[0] == "version")
			{
				VersionMode = true;
				return;
			}

			if (CliArguments[0] == "watch")
			{
				WatchMode = true;
			}

			if (CliArguments.Count > 1)
			{
				for (var x = 1; x < CliArguments.Count; x++)
				{
					var arg = CliArguments[x];
					
					if (arg.Equals("--minify", StringComparison.OrdinalIgnoreCase))
						Minify = true;

					else if (arg.Equals("--path", StringComparison.OrdinalIgnoreCase))
						if (++x < CliArguments.Count)
						{
							var path = CliArguments[x].SetNativePathSeparators();

							if (path.Contains(Path.DirectorySeparatorChar) == false &&
							    path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
								continue;

							if (path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
								path = path[..path.LastIndexOf(Path.DirectorySeparatorChar)];

							try
							{
								WorkingPathOverride = Path.GetFullPath(path);
							}

							catch
							{
								await Console.Out.WriteLineAsync($"{CliErrorPrefix}Invalid project path at {path}");
								Environment.Exit(1);
							}
						}
				}
			}
		}
	}
	
    public static string GetWorkingPath()
    {
        var workingPath = Directory.GetCurrentDirectory();
        
#if DEBUG
        var index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

        if (index > -1)
        {
            workingPath = Path.Combine(workingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
        }

        else
        {
            index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato.Tests" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

            if (index > -1)
                workingPath = Path.Combine(workingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
        }
#endif

        return workingPath;
    }

    public async Task<string> GetEmbeddedSassPathAsync()
    {
        var osPlatform = Identify.GetOsPlatform();
        var processorArchitecture = Identify.GetProcessorArchitecture();
        var sassPath = string.Empty;
		var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

		while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
		{
			workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];

#if DEBUG
			if (Directory.Exists(Path.Combine(workingPath, "sass")) == false)
				continue;

			var tempPath = workingPath; 
			
			workingPath = Path.Combine(tempPath, "sass");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "sass");
#endif
			
			if (osPlatform == OSPlatform.Windows)
			{
				if (processorArchitecture is Architecture.X64 or Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-windows-x64", "sass.bat");
			}
				
			else if (osPlatform == OSPlatform.OSX)
			{
				if (processorArchitecture == Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-macos-x64", "sass");
				else if (processorArchitecture == Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-macos-arm64", "sass");
			}
				
			else if (osPlatform == OSPlatform.Linux)
			{
				if (processorArchitecture == Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-linux-x64", "sass");
				else if (processorArchitecture == Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-linux-arm64", "sass");
			}

			break;
		}
		
		if (string.IsNullOrEmpty(sassPath) || File.Exists(sassPath) == false)
		{
			await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded Dart Sass cannot be found.");
			Environment.Exit(1);
		}
		
		var sb = StringBuilderPool.Get();
		var cmd = Cli.Wrap(sassPath)
			.WithArguments(arguments =>
			{
				arguments.Add("--version");
			})
			.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
			.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

		try
		{
			_ = cmd.ExecuteAsync().GetAwaiter().GetResult();
		}

		catch
		{
			await Console.Out.WriteLineAsync($"{CliErrorPrefix}Dart Sass is embedded but cannot be found.");
			Environment.Exit(1);
		}

		StringBuilderPool.Return(sb);
		
		return sassPath;
    }

    public static async Task<string> GetEmbeddedScssPathAsync()
    {
	    var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];
            
#if DEBUG
		    if (Directory.Exists(Path.Combine(workingPath, "scss")) == false)
			    continue;

		    var tempPath = workingPath; 
			
		    workingPath = Path.Combine(tempPath, "scss");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "scss");
#endif
		    break;
		}

        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
        {
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded SCSS resources cannot be found.");
            Environment.Exit(1);
        }
        
		return workingPath;
	}
    
    #endregion
    
    #region Runner Methods
    
	/// <summary>
	/// Gather all watched files defined in settings.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GatherWatchedFilesAsync()
	{
		var timer = new Stopwatch();
		var totalTimer = new Stopwatch();

		totalTimer.Start();

		if (Settings.ProjectPaths.Count == 0)
		{
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} No project paths specified");
			return;
		}
		
		var tasks = new List<Task>();

		timer.Start();
		
		// Gather files lists
		
		foreach (var projectPath in Settings.ProjectPaths)
			tasks.Add(RecurseProjectPathAsync(projectPath.Path, projectPath.FileSpec, projectPath.IsFilePath, projectPath.Recurse));

		await Task.WhenAll(tasks);
		
		tasks.Clear();

		if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0a", $"{Strings.TriangleRight} RecurseProjectPathAsync => {timer.FormatTimer()}{Environment.NewLine}");

		timer.Restart();
		
		// Add matches to files lists

		foreach (var watchedFile in WatchedFiles)
			tasks.Add(ProcessFileMatchesAsync(watchedFile.Value));
		
		await Task.WhenAll(tasks);

		if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0b", $"{Strings.TriangleRight} ProcessFileMatchesAsync => {timer.FormatTimer()}{Environment.NewLine}");

		timer.Restart();
		
		// Generate used classes list

		await ExamineWatchedFilesForUsedClassesAsync();

		if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init0c", $"{Strings.TriangleRight} ExamineWatchedFilesForUsedClassesAsync => {timer.FormatTimer()}{Environment.NewLine}");
		
		if (WatchedFiles.IsEmpty)
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified no used classes");
		else
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified {WatchedFiles.Count:N0} file{(WatchedFiles.Count == 1 ? string.Empty : "s")} using {UsedClasses.Count:N0} classes in {totalTimer.FormatTimer()}");
	}
	
	/// <summary>
	/// Recurse a project path to collect all matching files.
	/// </summary>
	/// <param name="sourcePath"></param>
	/// <param name="fileSpec"></param>
	/// <param name="isFilePath"></param>
	/// <param name="watchedFiles"></param>
	/// <param name="recurse"></param>
	public async Task RecurseProjectPathAsync(string? sourcePath, string fileSpec, bool isFilePath, bool recurse = false)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty())
			return;

		FileInfo[] files = null!;
		DirectoryInfo[] dirs = null!;
		
		if (isFilePath)
		{
			recurse = false;
			files = new [] { new FileInfo(Path.Combine(sourcePath, fileSpec)) };
		}

		else
		{
			var dir = new DirectoryInfo(sourcePath);

			if (dir.Exists == false)
			{
				await Console.Out.WriteLineAsync($"Source directory does not exist or could not be found: {sourcePath}");
				Environment.Exit(1);
			}

			dirs = dir.GetDirectories();
			files = dir.GetFiles().Where(f => f.Name.EndsWith(fileSpec.TrimStart('*'))).ToArray();
		}

		var tasks = new List<Task>();
		
		foreach (var projectFile in files)
		{
			tasks.Add(AddProjectFileToCollection(projectFile, fileSpec));
		}

		await Task.WhenAll(tasks);

		if (recurse == false)
			return;
		
		foreach (var subDir in dirs.OrderBy(d => d.Name))
			await RecurseProjectPathAsync(subDir.FullName, fileSpec, isFilePath, recurse);
	}

	/// <summary>
	/// Read a FileInfo object and add it to the appropriate collection.
	/// </summary>
	/// <param name="projectFile"></param>
	/// <param name="fileSpec"></param>
	public async Task AddProjectFileToCollection(FileInfo projectFile, string fileSpec)
	{
		var markup = await File.ReadAllTextAsync(projectFile.FullName);

		if (fileSpec.EndsWith(".scss"))
		{
			WatchedScssFiles.TryAdd(projectFile.FullName, new WatchedScssFile
			{
				FilePath = projectFile.FullName,
				Scss = markup
			});
		}

		else
		{
			WatchedFiles.TryAdd(projectFile.FullName, new WatchedFile
			{
				FilePath = projectFile.FullName,
				Markup = markup
			});
		}
	}
	
	/// <summary>
	/// Identify class matches in a given watched file.
	/// </summary>
	/// <param name="watchedFile"></param>
	public async Task ProcessFileMatchesAsync(WatchedFile watchedFile)
	{
		watchedFile.CoreClassMatches.Clear();
		watchedFile.ArbitraryCssMatches.Clear();
		
		var tasks = new List<Task>();
		var matches = CoreClassRegex.Matches(watchedFile.Markup);

		if (matches.Count > 0)
		{
			foreach (Match match in matches)
				tasks.Add(AddCssSelectorToCollection(watchedFile.CoreClassMatches, this, match.Value));

			await Task.WhenAll(tasks);
			tasks.Clear();
		}

		matches = ArbitraryCssRegex.Matches(watchedFile.Markup);

		if (matches.Count > 0)
		{
			foreach (Match match in matches)
				tasks.Add(AddCssSelectorToCollection(watchedFile.ArbitraryCssMatches, this, match.Value, true));

			await Task.WhenAll(tasks);
		}
	}

	public static async Task AddCssSelectorToCollection(ConcurrentDictionary<string,CssSelector> collection, SfumatoAppState appState, string value, bool isArbitraryCss = false)
	{
		var cssSelector = new CssSelector(appState, value, isArbitraryCss);

		if (cssSelector.IsInvalid == false)
			if (collection.TryAdd(value, cssSelector))
				await cssSelector.ProcessSelector();

		await Task.CompletedTask;
	}
	
	/// <summary>
	/// Examine all watched files for used classes.
	/// Generates the UsedClasses collection.
	/// </summary>
	public async Task ExamineWatchedFilesForUsedClassesAsync()
	{
		UsedClasses.Clear();

		var tasks = new List<Task>();
		
		foreach (var watchedFile in WatchedFiles)
			tasks.Add(ExamineMarkupForUsedClassesAsync(watchedFile.Value));
		
		await Task.WhenAll(tasks);
	}
	
	/// <summary>
	/// Examine markup for used classes and add them to the UsedClasses collection.
	/// </summary>
	/// <param name="watchedFile"></param>
	public async Task ExamineMarkupForUsedClassesAsync(WatchedFile watchedFile)
	{
		foreach (var cssSelector in watchedFile.CoreClassMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedSelector))
				continue;

			if (cssSelector.ScssUtilityClassGroup is null)
				continue;

			var usedScssClass = new UsedScssClass
			{
				CssSelector = cssSelector
			};

			UsedClasses.TryAdd(usedScssClass.CssSelector.FixedSelector, usedScssClass);
		}

		foreach (var cssSelector in watchedFile.ArbitraryCssMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedSelector))
				continue;

			var usedScssClass = new UsedScssClass
			{
				CssSelector = cssSelector
			};

			UsedClasses.TryAdd(usedScssClass.CssSelector.FixedSelector, usedScssClass);
		}

		await Task.CompletedTask;
	}
	
	#endregion
}

public class WatchedFile
{
	public string FilePath { get; set; } = string.Empty;
	public string Markup { get; set; } = string.Empty;
	public ConcurrentDictionary<string,CssSelector> CoreClassMatches { get; set; } = new ();
	public ConcurrentDictionary<string,CssSelector> ArbitraryCssMatches { get; set; } = new ();
}

public class WatchedScssFile
{
	public string FilePath { get; set; } = string.Empty;
	public string Scss { get; set; } = string.Empty;
	public string Css { get; set; } = string.Empty;
}