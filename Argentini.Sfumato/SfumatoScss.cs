namespace Argentini.Sfumato;

public static class SfumatoScss
{
	#region Theme Constants

	public static IEnumerable<CssMediaQuery> MediaQueryPrefixes => new[]
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
	
    public static Dictionary<string,string> Colors { get; } = new()
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
    
    public static Dictionary<string, string> Fractions { get; } = new()
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
	
    #endregion
    
    #region CSS Constants
    
	public static IEnumerable<string> ArbitraryValueTypes => new[]
	{
		"string", "url", "custom-ident", "dashed-ident",
		"integer", "number", "percentage", "ratio", "flex",
		"length", "angle", "time", "frequency", "resolution",
		"color"
	};

	public static IEnumerable<string> CssUnits => new[]
	{
		// Order here matters as truncating values like 'em' also work on values ending with 'rem'

		"rem", "vmin", "vmax",
		"cm", "in", "mm", "pc", "pt", "px",
		"ch", "em", "ex", "vw", "vh"
	};

	public static IEnumerable<string> CssAngleUnits => new[]
	{
		// Order here matters as truncating values like 'rad' also work on values ending with 'grad'

		"grad", "turn", "deg", "rad"
	};

	public static IEnumerable<string> CssTimeUnits => new[]
	{
		// Order here matters as truncating values like 's' also work on values ending with 'ms'
		
		"ms", "s"
	};

	public static IEnumerable<string> CssFrequencyUnits => new[]
	{
		// Order here matters as truncating values like 'Hz' also work on values ending with 'kHz'
		
		"kHz", "Hz"
	};

	public static IEnumerable<string> CssResolutionUnits => new[]
	{
		// Order here matters as truncating values like 'x' also work on values ending with 'dppx'
		
		"dpcm", "dppx", "dpi", "x"
	};
	
	public static IEnumerable<string> CssNamedColors => new[]
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
	
	public static IEnumerable<string> CssPropertyNames => new[]
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
	
	public static Dictionary<string, string> PseudoclassPrefixes => new ()
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
	
	/// <summary>
	/// Get all Sfumato core SCSS include files (e.g. mixins) and return as a single string.
	/// Used as a prefix for the global CSS file (sfumato.css).
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="diagnosticOutput"></param>
	/// <returns></returns>
	public static async Task<string> GetCoreScssAsync(SfumatoAppState appState, ConcurrentDictionary<string,string> diagnosticOutput)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var sb = appState.StringBuilderPool.Get();
		
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_core.scss"))).Trim() + '\n');
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_browser-reset.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_media-queries.scss"))).Trim() + '\n';

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{appState.Settings.Breakpoints?.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{appState.Settings.Breakpoints?.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{appState.Settings.Breakpoints?.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{appState.Settings.Breakpoints?.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{appState.Settings.Breakpoints?.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{appState.Settings.Breakpoints?.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{appState.Settings.Breakpoints?.Elas}px");
		
		sb.Append(mediaQueriesScss);

		var initScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_initialize.scss"))).Trim() + '\n';
		
		initScss = initScss.Replace("#{zero-font-size}", $"{appState.Settings.FontSizeUnits?.Zero}");
		initScss = initScss.Replace("#{phab-font-size}", $"{appState.Settings.FontSizeUnits?.Phab}");
		initScss = initScss.Replace("#{tabp-font-size}", $"{appState.Settings.FontSizeUnits?.Tabp}");
		initScss = initScss.Replace("#{tabl-font-size}", $"{appState.Settings.FontSizeUnits?.Tabl}");
		initScss = initScss.Replace("#{note-font-size}", $"{appState.Settings.FontSizeUnits?.Note}");
		initScss = initScss.Replace("#{desk-font-size}", $"{appState.Settings.FontSizeUnits?.Desk}");

		if (appState.Settings.FontSizeUnits?.Elas.EndsWith("vw", StringComparison.Ordinal) ?? false)
		{
			initScss = initScss.Replace("#{elas-font-size}", $"calc(#{{$elas-breakpoint}} * (#{{sf-strip-unit({appState.Settings.FontSizeUnits?.Elas})}} / 100))");
		}

		else
		{
			initScss = initScss.Replace("#{elas-font-size}", $"{appState.Settings.FontSizeUnits?.Elas}");
		}
		
		sb.Append(initScss);

		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_forms.scss"))).Trim() + '\n');
		
		diagnosticOutput.TryAdd("init2", $"Prepared SCSS Core for output injection in {timer.FormatTimer()}{Environment.NewLine}");

		var result = sb.ToString();
		
		appState.StringBuilderPool.Return(sb);

		return result;
	}

	/// <summary>
	/// Get all Sfumato core SCSS include files (e.g. mixins) and return as a single string.
	/// Used as a prefix for transpile in-place project SCSS files.
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="diagnosticOutput"></param>
	/// <returns></returns>
	public static async Task<string> GetSharedScssAsync(SfumatoAppState appState, ConcurrentDictionary<string,string> diagnosticOutput)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var sb = appState.StringBuilderPool.Get();
		
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_core.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_media-queries.scss"))).Trim() + '\n';

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{appState.Settings.Breakpoints?.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{appState.Settings.Breakpoints?.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{appState.Settings.Breakpoints?.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{appState.Settings.Breakpoints?.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{appState.Settings.Breakpoints?.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{appState.Settings.Breakpoints?.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{appState.Settings.Breakpoints?.Elas}px");
		
		sb.Append(mediaQueriesScss);

		diagnosticOutput.TryAdd("init3", $"Prepared shared SCSS for output injection in {timer.FormatTimer()}{Environment.NewLine}");

		var result = sb.ToString();
		
		appState.StringBuilderPool.Return(sb);

		return result;
	}
	
	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="rawScss"></param>
	/// <param name="appState"></param>
	/// <returns>Generated CSS file</returns>
	public static async Task<string> TranspileScss(string filePath, string rawScss, SfumatoAppState appState)
	{
		var sb = appState.StringBuilderPool.Get();

		try
		{
			var arguments = new List<string>();
			var cssOutputPath = filePath.TrimEnd(".scss") + ".css"; 
			var cssMapOutputPath = cssOutputPath + ".map"; 
			
			if (File.Exists(cssMapOutputPath))
				File.Delete(cssMapOutputPath);

			if (appState.ReleaseMode == false)
			{
				arguments.Add("--style=expanded");
				arguments.Add("--embed-sources");
			}

			else
			{
				arguments.Add("--style=compressed");
				arguments.Add("--no-source-map");
	            
			}

			arguments.Add("--stdin");
			arguments.Add(cssOutputPath);
			
			var scss = appState.StringBuilderPool.Get();
			var matches = appState.SfumatoScssIncludesRegex.Matches(rawScss);
			var startIndex = 0;

			foreach (Match match in matches)
			{
				if (match.Index + match.Value.Length > startIndex)
					startIndex = match.Index + match.Value.Length;

				if (match.Value.Trim().EndsWith("core;"))
				{
					scss.Append(appState.ScssSharedInjectable);
				}
			}
			
			scss.Append(rawScss[startIndex..]);
			
			var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap(appState.SassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

			await cmd.ExecuteAsync();
			
			appState.StringBuilderPool.Return(sb);
			appState.StringBuilderPool.Return(scss);

			return await File.ReadAllTextAsync(cssOutputPath);
		}

		catch (Exception e)
		{
			sb.AppendLine($"{Strings.TriangleRight} ERROR: {e.Message.Trim()}");
			sb.AppendLine(string.Empty);
			sb.AppendLine(e.StackTrace?.Trim());
			sb.AppendLine(string.Empty);

			await Console.Out.WriteLineAsync(sb.ToString());

			appState.StringBuilderPool.Return(sb);

			Environment.Exit(1);

			return string.Empty;
		}
	}
	
    /// <summary>
    /// Get the value type of the user class value (e.g. "length:...", "color:...", etc.)
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public static string GetUserClassValueType(this string valueSegment)
    {
	    if (string.IsNullOrEmpty(valueSegment))
		    return string.Empty;
	    
	    if (valueSegment.StartsWith('[') == false)
		    return string.Empty;

	    var value = valueSegment.TrimStart('[').TrimEnd(']');
	    
	    if (value.Contains(':'))
	    {
		    var segments = value.Split(':', StringSplitOptions.RemoveEmptyEntries);

		    if (segments.Length > 1)
		    {
			    if (ArbitraryValueTypes.Contains(segments[0]))
				    return segments[0];
		    }
	    }

	    // Determine value type based on value (e.g. text-[red])
	    
	    if (value.EndsWith('%') && double.TryParse(value.TrimEnd('%'), out _))
		    return "percentage";
	    
	    if (value.Contains('.') == false && int.TryParse(value, out _))
		    return "integer";

	    if (double.TryParse(value, out _))
		    return "number";
	    
	    #region length
	    
	    var unitless = string.Empty;

	    foreach (var unit in CssUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "length";

	    #endregion
	    
	    if (value.EndsWith("fr") && double.TryParse(value.TrimEnd("fr"), out _))
		    return "flex";

	    if (value.IsValidWebHexColor() || value.StartsWith("rgb(") || value.StartsWith("rgba(") || CssNamedColors.Contains(value))
		    return "color";

	    if (value.StartsWith('\'') && value.EndsWith('\''))
		    return "string";
	    
	    if (value.StartsWith("url(", StringComparison.Ordinal) && value.EndsWith(')') || (value.StartsWith('/') && Uri.TryCreate(value, UriKind.Relative, out _)) || (Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
		    return "url";
	    
	    #region angle

	    unitless = string.Empty;

	    foreach (var unit in CssAngleUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "angle";

	    #endregion

	    #region time

	    unitless = string.Empty;

	    foreach (var unit in CssTimeUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "time";

	    #endregion

	    #region frequency

	    unitless = string.Empty;

	    foreach (var unit in CssFrequencyUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "frequency";

	    #endregion

	    #region resolution

	    unitless = string.Empty;

	    foreach (var unit in CssResolutionUnits)
	    {
		    unitless = value.TrimEnd(unit) ?? string.Empty;
		    
		    if (value.Length != unitless.Length)
			    break;
	    }

	    if (double.TryParse(unitless, out _))
		    return "resolution";

	    #endregion
	    
	    #region ratio
	    
	    if (value.Length < 3 || value.IndexOf('/') < 1 || value.IndexOf('/') == value.Length - 1)
		    return string.Empty;
	    
	    var values = value.Replace('_', ' ').Split('/', StringSplitOptions.RemoveEmptyEntries);

	    if (values.Length != 2)
		    return string.Empty;

	    if (double.TryParse(values[0], out _) && double.TryParse(values[1], out _))
		    return "ratio";

	    #endregion
	    
	    return string.Empty;
    }
}

public class CssMediaQuery
{
	public int PrefixOrder { get; set; }
	public int Priority { get; set; }
	public string Prefix { get; set; } = string.Empty;
	public string PrefixType { get; set; } = string.Empty;
	public string Statement { get; set; } = string.Empty;
}