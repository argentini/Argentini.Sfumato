using Argentini.Sfumato.ScssUtilityCollections;
using Argentini.Sfumato.ScssUtilityCollections.Accessibility;
using Argentini.Sfumato.ScssUtilityCollections.Backgrounds;
using Argentini.Sfumato.ScssUtilityCollections.Borders;
using Argentini.Sfumato.ScssUtilityCollections.Effects;
using Argentini.Sfumato.ScssUtilityCollections.Filters;
using Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

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
        ["black"] = "rgba(0,0,0,1.0)",
        ["white"] = "rgba(255,255,255,1.0)",
        ["slate"] = "rgba(203,213,225,1.0)",
        ["slate-50"] = "rgba(248,250,252,1.0)",
        ["slate-100"] = "rgba(241,245,249,1.0)",
        ["slate-200"] = "rgba(226,232,240,1.0)",
        ["slate-300"] = "rgba(203,213,225,1.0)",
        ["slate-400"] = "rgba(148,163,184,1.0)",
        ["slate-500"] = "rgba(100,116,139,1.0)",
        ["slate-600"] = "rgba(71,85,105,1.0)",
        ["slate-700"] = "rgba(51,65,85,1.0)",
        ["slate-800"] = "rgba(30,41,59,1.0)",
        ["slate-900"] = "rgba(15,23,42,1.0)",
        ["slate-950"] = "rgba(2,6,23,1.0)",
        ["gray"] = "rgba(209,213,219,1.0)",
        ["gray-50"] = "rgba(249,250,251,1.0)",
        ["gray-100"] = "rgba(243,244,246,1.0)",
        ["gray-200"] = "rgba(229,231,235,1.0)",
        ["gray-300"] = "rgba(209,213,219,1.0)",
        ["gray-400"] = "rgba(156,163,175,1.0)",
        ["gray-500"] = "rgba(107,114,128,1.0)",
        ["gray-600"] = "rgba(75,85,99,1.0)",
        ["gray-700"] = "rgba(55,65,81,1.0)",
        ["gray-800"] = "rgba(31,41,55,1.0)",
        ["gray-900"] = "rgba(17,24,39,1.0)",
        ["gray-950"] = "rgba(3,7,18,1.0)",
        ["zinc"] = "rgba(212,212,216,1.0)",
        ["zinc-50"] = "rgba(250,250,250,1.0)",
        ["zinc-100"] = "rgba(244,244,245,1.0)",
        ["zinc-200"] = "rgba(228,228,231,1.0)",
        ["zinc-300"] = "rgba(212,212,216,1.0)",
        ["zinc-400"] = "rgba(161,161,170,1.0)",
        ["zinc-500"] = "rgba(113,113,122,1.0)",
        ["zinc-600"] = "rgba(82,82,91,1.0)",
        ["zinc-700"] = "rgba(63,63,70,1.0)",
        ["zinc-800"] = "rgba(39,39,42,1.0)",
        ["zinc-900"] = "rgba(24,24,27,1.0)",
        ["zinc-950"] = "rgba(9,9,11,1.0)",
        ["neutral"] = "rgba(212,212,212,1.0)",
        ["neutral-50"] = "rgba(250,250,250,1.0)",
        ["neutral-100"] = "rgba(245,245,245,1.0)",
        ["neutral-200"] = "rgba(229,229,229,1.0)",
        ["neutral-300"] = "rgba(212,212,212,1.0)",
        ["neutral-400"] = "rgba(163,163,163,1.0)",
        ["neutral-500"] = "rgba(115,115,115,1.0)",
        ["neutral-600"] = "rgba(82,82,82,1.0)",
        ["neutral-700"] = "rgba(64,64,64,1.0)",
        ["neutral-800"] = "rgba(38,38,38,1.0)",
        ["neutral-900"] = "rgba(23,23,23,1.0)",
        ["neutral-950"] = "rgba(10,10,10,1.0)",
        ["stone"] = "rgba(214,211,209,1.0)",
        ["stone-50"] = "rgba(250,250,249,1.0)",
        ["stone-100"] = "rgba(245,245,244,1.0)",
        ["stone-200"] = "rgba(231,229,228,1.0)",
        ["stone-300"] = "rgba(214,211,209,1.0)",
        ["stone-400"] = "rgba(168,162,158,1.0)",
        ["stone-500"] = "rgba(120,113,108,1.0)",
        ["stone-600"] = "rgba(87,83,78,1.0)",
        ["stone-700"] = "rgba(68,64,60,1.0)",
        ["stone-800"] = "rgba(41,37,36,1.0)",
        ["stone-900"] = "rgba(28,25,23,1.0)",
        ["stone-950"] = "rgba(12,10,9,1.0)",
        ["red"] = "rgba(252,165,165,1.0)",
        ["red-50"] = "rgba(254,242,242,1.0)",
        ["red-100"] = "rgba(254,226,226,1.0)",
        ["red-200"] = "rgba(254,202,202,1.0)",
        ["red-300"] = "rgba(252,165,165,1.0)",
        ["red-400"] = "rgba(248,113,113,1.0)",
        ["red-500"] = "rgba(239,68,68,1.0)",
        ["red-600"] = "rgba(220,38,38,1.0)",
        ["red-700"] = "rgba(185,28,28,1.0)",
        ["red-800"] = "rgba(153,27,27,1.0)",
        ["red-900"] = "rgba(127,29,29,1.0)",
        ["red-950"] = "rgba(69,10,10,1.0)",
        ["orange"] = "rgba(253,186,116,1.0)",
        ["orange-50"] = "rgba(255,247,237,1.0)",
        ["orange-100"] = "rgba(255,237,213,1.0)",
        ["orange-200"] = "rgba(254,215,170,1.0)",
        ["orange-300"] = "rgba(253,186,116,1.0)",
        ["orange-400"] = "rgba(251,146,60,1.0)",
        ["orange-500"] = "rgba(249,115,22,1.0)",
        ["orange-600"] = "rgba(234,88,12,1.0)",
        ["orange-700"] = "rgba(194,65,12,1.0)",
        ["orange-800"] = "rgba(154,52,18,1.0)",
        ["orange-900"] = "rgba(124,45,18,1.0)",
        ["orange-950"] = "rgba(67,20,7,1.0)",
        ["amber"] = "rgba(252,211,77,1.0)",
        ["amber-50"] = "rgba(255,251,235,1.0)",
        ["amber-100"] = "rgba(254,243,199,1.0)",
        ["amber-200"] = "rgba(253,230,138,1.0)",
        ["amber-300"] = "rgba(252,211,77,1.0)",
        ["amber-400"] = "rgba(251,191,36,1.0)",
        ["amber-500"] = "rgba(245,158,11,1.0)",
        ["amber-600"] = "rgba(217,119,6,1.0)",
        ["amber-700"] = "rgba(180,83,9,1.0)",
        ["amber-800"] = "rgba(146,64,14,1.0)",
        ["amber-900"] = "rgba(120,53,15,1.0)",
        ["amber-950"] = "rgba(69,26,3,1.0)",
        ["yellow"] = "rgba(253,224,71,1.0)",
        ["yellow-50"] = "rgba(254,252,232,1.0)",
        ["yellow-100"] = "rgba(254,249,195,1.0)",
        ["yellow-200"] = "rgba(254,240,138,1.0)",
        ["yellow-300"] = "rgba(253,224,71,1.0)",
        ["yellow-400"] = "rgba(250,204,21,1.0)",
        ["yellow-500"] = "rgba(234,179,8,1.0)",
        ["yellow-600"] = "rgba(202,138,4,1.0)",
        ["yellow-700"] = "rgba(161,98,7,1.0)",
        ["yellow-800"] = "rgba(133,77,14,1.0)",
        ["yellow-900"] = "rgba(113,63,18,1.0)",
        ["yellow-950"] = "rgba(66,32,6,1.0)",
        ["lime"] = "rgba(190,242,100,1.0)",
        ["lime-50"] = "rgba(247,254,231,1.0)",
        ["lime-100"] = "rgba(236,252,203,1.0)",
        ["lime-200"] = "rgba(217,249,157,1.0)",
        ["lime-300"] = "rgba(190,242,100,1.0)",
        ["lime-400"] = "rgba(163,230,53,1.0)",
        ["lime-500"] = "rgba(132,204,22,1.0)",
        ["lime-600"] = "rgba(101,163,13,1.0)",
        ["lime-700"] = "rgba(77,124,15,1.0)",
        ["lime-800"] = "rgba(63,98,18,1.0)",
        ["lime-900"] = "rgba(54,83,20,1.0)",
        ["lime-950"] = "rgba(26,46,5,1.0)",
        ["green"] = "rgba(134,239,172,1.0)",
        ["green-50"] = "rgba(240,253,244,1.0)",
        ["green-100"] = "rgba(220,252,231,1.0)",
        ["green-200"] = "rgba(187,247,208,1.0)",
        ["green-300"] = "rgba(134,239,172,1.0)",
        ["green-400"] = "rgba(74,222,128,1.0)",
        ["green-500"] = "rgba(34,197,94,1.0)",
        ["green-600"] = "rgba(22,163,74,1.0)",
        ["green-700"] = "rgba(21,128,61,1.0)",
        ["green-800"] = "rgba(22,101,52,1.0)",
        ["green-900"] = "rgba(20,83,45,1.0)",
        ["green-950"] = "rgba(5,46,22,1.0)",
        ["emerald"] = "rgba(110,231,183,1.0)",
        ["emerald-50"] = "rgba(236,253,245,1.0)",
        ["emerald-100"] = "rgba(209,250,229,1.0)",
        ["emerald-200"] = "rgba(167,243,208,1.0)",
        ["emerald-300"] = "rgba(110,231,183,1.0)",
        ["emerald-400"] = "rgba(52,211,153,1.0)",
        ["emerald-500"] = "rgba(16,185,129,1.0)",
        ["emerald-600"] = "rgba(5,150,105,1.0)",
        ["emerald-700"] = "rgba(4,120,87,1.0)",
        ["emerald-800"] = "rgba(6,95,70,1.0)",
        ["emerald-900"] = "rgba(6,78,59,1.0)",
        ["emerald-950"] = "rgba(2,44,34,1.0)",
        ["teal"] = "rgba(94,234,212,1.0)",
        ["teal-50"] = "rgba(240,253,250,1.0)",
        ["teal-100"] = "rgba(204,251,241,1.0)",
        ["teal-200"] = "rgba(153,246,228,1.0)",
        ["teal-300"] = "rgba(94,234,212,1.0)",
        ["teal-400"] = "rgba(45,212,191,1.0)",
        ["teal-500"] = "rgba(20,184,166,1.0)",
        ["teal-600"] = "rgba(13,148,136,1.0)",
        ["teal-700"] = "rgba(15,118,110,1.0)",
        ["teal-800"] = "rgba(17,94,89,1.0)",
        ["teal-900"] = "rgba(19,78,74,1.0)",
        ["teal-950"] = "rgba(4,47,46,1.0)",
        ["cyan"] = "rgba(103,232,249,1.0)",
        ["cyan-50"] = "rgba(236,254,255,1.0)",
        ["cyan-100"] = "rgba(207,250,254,1.0)",
        ["cyan-200"] = "rgba(165,243,252,1.0)",
        ["cyan-300"] = "rgba(103,232,249,1.0)",
        ["cyan-400"] = "rgba(34,211,238,1.0)",
        ["cyan-500"] = "rgba(6,182,212,1.0)",
        ["cyan-600"] = "rgba(8,145,178,1.0)",
        ["cyan-700"] = "rgba(14,116,144,1.0)",
        ["cyan-800"] = "rgba(21,94,117,1.0)",
        ["cyan-900"] = "rgba(22,78,99,1.0)",
        ["cyan-950"] = "rgba(8,51,68,1.0)",
        ["sky"] = "rgba(125,211,252,1.0)",
        ["sky-50"] = "rgba(240,249,255,1.0)",
        ["sky-100"] = "rgba(224,242,254,1.0)",
        ["sky-200"] = "rgba(186,230,253,1.0)",
        ["sky-300"] = "rgba(125,211,252,1.0)",
        ["sky-400"] = "rgba(56,189,248,1.0)",
        ["sky-500"] = "rgba(14,165,233,1.0)",
        ["sky-600"] = "rgba(2,132,199,1.0)",
        ["sky-700"] = "rgba(3,105,161,1.0)",
        ["sky-800"] = "rgba(7,89,133,1.0)",
        ["sky-900"] = "rgba(12,74,110,1.0)",
        ["sky-950"] = "rgba(8,47,73,1.0)",
        ["blue"] = "rgba(147,197,253,1.0)",
        ["blue-50"] = "rgba(239,246,255,1.0)",
        ["blue-100"] = "rgba(219,234,254,1.0)",
        ["blue-200"] = "rgba(191,219,254,1.0)",
        ["blue-300"] = "rgba(147,197,253,1.0)",
        ["blue-400"] = "rgba(96,165,250,1.0)",
        ["blue-500"] = "rgba(59,130,246,1.0)",
        ["blue-600"] = "rgba(37,99,235,1.0)",
        ["blue-700"] = "rgba(29,78,216,1.0)",
        ["blue-800"] = "rgba(30,64,175,1.0)",
        ["blue-900"] = "rgba(30,58,138,1.0)",
        ["blue-950"] = "rgba(23,37,84,1.0)",
        ["indigo"] = "rgba(165,180,252,1.0)",
        ["indigo-50"] = "rgba(238,242,255,1.0)",
        ["indigo-100"] = "rgba(224,231,255,1.0)",
        ["indigo-200"] = "rgba(199,210,254,1.0)",
        ["indigo-300"] = "rgba(165,180,252,1.0)",
        ["indigo-400"] = "rgba(129,140,248,1.0)",
        ["indigo-500"] = "rgba(99,102,241,1.0)",
        ["indigo-600"] = "rgba(79,70,229,1.0)",
        ["indigo-700"] = "rgba(67,56,202,1.0)",
        ["indigo-800"] = "rgba(55,48,163,1.0)",
        ["indigo-900"] = "rgba(49,46,129,1.0)",
        ["indigo-950"] = "rgba(30,27,75,1.0)",
        ["violet"] = "rgba(196,181,253,1.0)",
        ["violet-50"] = "rgba(245,243,255,1.0)",
        ["violet-100"] = "rgba(237,233,254,1.0)",
        ["violet-200"] = "rgba(221,214,254,1.0)",
        ["violet-300"] = "rgba(196,181,253,1.0)",
        ["violet-400"] = "rgba(167,139,250,1.0)",
        ["violet-500"] = "rgba(139,92,246,1.0)",
        ["violet-600"] = "rgba(124,58,237,1.0)",
        ["violet-700"] = "rgba(109,40,217,1.0)",
        ["violet-800"] = "rgba(91,33,182,1.0)",
        ["violet-900"] = "rgba(76,29,149,1.0)",
        ["violet-950"] = "rgba(46,16,101,1.0)",
        ["purple"] = "rgba(216,180,254,1.0)",
        ["purple-50"] = "rgba(250,245,255,1.0)",
        ["purple-100"] = "rgba(243,232,255,1.0)",
        ["purple-200"] = "rgba(233,213,255,1.0)",
        ["purple-300"] = "rgba(216,180,254,1.0)",
        ["purple-400"] = "rgba(192,132,252,1.0)",
        ["purple-500"] = "rgba(168,85,247,1.0)",
        ["purple-600"] = "rgba(147,51,234,1.0)",
        ["purple-700"] = "rgba(126,34,206,1.0)",
        ["purple-800"] = "rgba(107,33,168,1.0)",
        ["purple-900"] = "rgba(88,28,135,1.0)",
        ["purple-950"] = "rgba(59,7,100,1.0)",
        ["fuchsia"] = "rgba(240,171,252,1.0)",
        ["fuchsia-50"] = "rgba(253,244,255,1.0)",
        ["fuchsia-100"] = "rgba(250,232,255,1.0)",
        ["fuchsia-200"] = "rgba(245,208,254,1.0)",
        ["fuchsia-300"] = "rgba(240,171,252,1.0)",
        ["fuchsia-400"] = "rgba(232,121,249,1.0)",
        ["fuchsia-500"] = "rgba(217,70,239,1.0)",
        ["fuchsia-600"] = "rgba(192,38,211,1.0)",
        ["fuchsia-700"] = "rgba(162,28,175,1.0)",
        ["fuchsia-800"] = "rgba(134,25,143,1.0)",
        ["fuchsia-900"] = "rgba(112,26,117,1.0)",
        ["fuchsia-950"] = "rgba(74,4,78,1.0)",
        ["pink"] = "rgba(249,168,212,1.0)",
        ["pink-50"] = "rgba(253,242,248,1.0)",
        ["pink-100"] = "rgba(252,231,243,1.0)",
        ["pink-200"] = "rgba(251,207,232,1.0)",
        ["pink-300"] = "rgba(249,168,212,1.0)",
        ["pink-400"] = "rgba(244,114,182,1.0)",
        ["pink-500"] = "rgba(236,72,153,1.0)",
        ["pink-600"] = "rgba(219,39,119,1.0)",
        ["pink-700"] = "rgba(190,24,93,1.0)",
        ["pink-800"] = "rgba(157,23,77,1.0)",
        ["pink-900"] = "rgba(131,24,67,1.0)",
        ["pink-950"] = "rgba(80,7,36,1.0)",
        ["rose"] = "rgba(253,164,175,1.0)",
        ["rose-50"] = "rgba(255,241,242,1.0)",
        ["rose-100"] = "rgba(255,228,230,1.0)",
        ["rose-200"] = "rgba(254,205,211,1.0)",
        ["rose-300"] = "rgba(253,164,175,1.0)",
        ["rose-400"] = "rgba(251,113,133,1.0)",
        ["rose-500"] = "rgba(244,63,94,1.0)",
        ["rose-600"] = "rgba(225,29,72,1.0)",
        ["rose-700"] = "rgba(190,18,60,1.0)",
        ["rose-800"] = "rgba(159,18,57,1.0)",
        ["rose-900"] = "rgba(136,19,55,1.0)",
        ["rose-950"] = "rgba(76,5,25,1.0)"
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

    public Dictionary<string, string> FilterSizeOptions { get; } = new()
    {
	    [""] = 8.PxToRem(),
	    ["none"] = "0",
	    ["sm"] = 4.PxToRem(),
	    ["md"] = 12.PxToRem(),
	    ["lg"] = 16.PxToRem(),
	    ["xl"] = 24.PxToRem(),
	    ["2xl"] = 40.PxToRem(),
	    ["3xl"] = 64.PxToRem(),
    };
    
    public Dictionary<string, string> LayoutRemUnitOptions { get; set; } = new();
    public Dictionary<string, string> LayoutWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> TypographyRemUnitOptions { get; set; } = new();
    public Dictionary<string, string> EffectsFiltersOneBasedPercentageOptions { get; set; } = new();
    public Dictionary<string, string> FlexboxAndGridWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> FlexboxAndGridNegativeWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> VerbatrimFractionOptions { get; set; } = new();
    public Dictionary<string, string> PercentageOptions { get; set; } = new();
    
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
    public ConcurrentDictionary<string,CssSelector> UsedClasses { get; } = new();
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
	    PercentageOptions.AddPercentageOptions(0, 100);
	    
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
        
        ScssUtilityClassGroupBase utilityClassGroup;

        #region Load Accessibility Classes
        
        utilityClassGroup = new SrOnly();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new NotSrOnly();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
		#endregion
		
        #region Load Background Classes
        
        utilityClassGroup = new Bg();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new From();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Via();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new To();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        #endregion
        
        #region Load Rounded Classes

        utilityClassGroup = new Rounded();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new RoundedB();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedBl();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new RoundedBr();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedE();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedEe();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedEs();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedL();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedR();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedS();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedSe();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedSs();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedT();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedTl();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RoundedTr();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        #endregion
        
        #region Load Border Classes

        utilityClassGroup = new Border();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new BorderB();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderE();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderL();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderR();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderS();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderT();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderX();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BorderY();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        #endregion
        
        #region Load Divide Classes

        utilityClassGroup = new Divide();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new DivideX();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new DivideY();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        #endregion
        
        #region Load Outline Classes
        
        utilityClassGroup = new Outline();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new OutlineOffset();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        #endregion

        #region Load Ring Classes
        
        utilityClassGroup = new Ring();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new RingInset();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RingOffset();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        #endregion

        #region Load Effects Classes
        
        utilityClassGroup = new Shadow();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Opacity();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new MixBlend();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BgBlend();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        #endregion
        
        #region Load Filter Classes
        
        utilityClassGroup = new Blur();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Brightness();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Contrast();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new DropShadow();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Grayscale();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new HueRotate();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Invert();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Saturate();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Sepia();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new BackdropBlur();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new BackdropBrightness();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BackdropContrast();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new BackdropGrayscale();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BackdropHueRotate();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BackdropInvert();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BackdropSaturate();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new BackdropSepia();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new BackdropOpacity();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        #endregion

        #region Load Flexbox And Grid Classes
        
        utilityClassGroup = new Basis();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Flex();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Grow();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Shrink();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Order();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new OrderNegative();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new GridCols();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new ColAuto();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new ColSpan();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new ColStart();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new ColEnd();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new GridRows();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RowAuto();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new RowSpan();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RowStart();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new RowEnd();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new GridFlow();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new AutoCols();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new AutoRows();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Gap();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new GapX();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new GapY();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Justify();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new JustifyItems();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new JustifySelf();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Content();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new Items();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new Self();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new PlaceContent();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);

        utilityClassGroup = new PlaceItems();
        UtilityClassCollection.TryAdd(utilityClassGroup.SelectorPrefix, utilityClassGroup);
        
        utilityClassGroup = new PlaceSelf();
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
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified no watched files");
		else
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified {WatchedFiles.Count:N0} watched file{(WatchedFiles.Count == 1 ? string.Empty : "s")} using {UsedClasses.Count(u => u.Value.IsInvalid == false):N0} classes in {totalTimer.FormatTimer()}");
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

		_ = cssSelector.ProcessSelector();
		_ = cssSelector.GetStyles();

		if (cssSelector.IsInvalid == false)
			collection.TryAdd(value, cssSelector);

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

			UsedClasses.TryAdd(cssSelector.FixedSelector, cssSelector);
		}

		foreach (var cssSelector in watchedFile.ArbitraryCssMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedSelector))
				continue;

			UsedClasses.TryAdd(cssSelector.FixedSelector, cssSelector);
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