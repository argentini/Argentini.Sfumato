using HtmlAgilityPack;

namespace Argentini.Sfumato;

public sealed class SfumatoAppState
{
	#region Theme Options

	#region Shared Options

	public IEnumerable<CssMediaQuery> MediaQueryPrefixes { get; } = new []
	{
		new CssMediaQuery
		{
			PrefixOrder = 1,
			Priority = int.MaxValue,
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
			Priority = 1024,
			Prefix = "motion-safe",
			PrefixType = "animation",
			Statement = "@media (prefers-reduced-motion: no-preference) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 6,
			Priority = 2048,
			Prefix = "motion-reduced",
			PrefixType = "animation",
			Statement = "@media (prefers-reduced-motion: reduce) {"
		},
        new CssMediaQuery
        {
            PrefixOrder = 7,
            Priority = 4096,
            Prefix = "supports-backdrop-blur",
            PrefixType = "features",
            Statement = "@supports ((-webkit-backdrop-filter:blur(0)) or (backdrop-filter:blur(0))) or (-webkit-backdrop-filter:blur(0)) {"
        },
		new CssMediaQuery
		{
			PrefixOrder = 8,
			Priority = 4,
			Prefix = "sm",
			PrefixType = "breakpoint",
			Statement = "@media (min-width: #{$sm-breakpoint}) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 9,
			Priority = 8,
			Prefix = "md",
			PrefixType = "breakpoint",
			Statement = "@media (min-width: #{$md-breakpoint}) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 10,
			Priority = 16,
			Prefix = "lg",
			PrefixType = "breakpoint",
			Statement = "@media (min-width: #{$lg-breakpoint}) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 11,
			Priority = 32,
			Prefix = "xl",
			PrefixType = "breakpoint",
			Statement = "@media (min-width: #{$xl-breakpoint}) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 12,
			Priority = 64,
			Prefix = "xxl",
			PrefixType = "breakpoint",
			Statement = "@media (min-width: #{$xxl-breakpoint}) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 13,
			Priority = 128,
			Prefix = "mobi",
			PrefixType = "breakpoint",
			Statement = "@media screen and (max-aspect-ratio: 9.999999999999/16) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 14,
			Priority = 256,
			Prefix = "tabp",
			PrefixType = "breakpoint",
			Statement = "@media screen and (min-aspect-ratio: 10/16) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 15,
			Priority = 512,
			Prefix = "tabl",
			PrefixType = "breakpoint",
			Statement = "@media screen and (min-aspect-ratio: 1/1) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 16,
			Priority = 1024,
			Prefix = "desk",
			PrefixType = "breakpoint",
			Statement = "@media screen and (min-aspect-ratio: 3/2) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 17,
			Priority = 2048,
			Prefix = "wide",
			PrefixType = "breakpoint",
			Statement = "@media screen and (min-aspect-ratio: 16/9) {"
		},
		new CssMediaQuery
		{
			PrefixOrder = 18,
			Priority = 4096,
			Prefix = "vast",
			PrefixType = "breakpoint",
			Statement = "@media screen and (min-aspect-ratio: 21/9) {"
		},
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
    
    public List<string> FractionDividendOptions { get; } =
    [
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12"
    ];
	
    public Dictionary<string,string> TextSizeOptions { get; } = new()
    {
        ["none"] = "0",
	    ["xs"] = "clamp((0.75rem * 0.875), (4.35vw * 0.75), 0.75rem)",
	    ["sm"] = "clamp((0.875rem * 0.875), (4.35vw * 0.875), 0.875rem)",
	    ["base"] = "clamp((1rem * 0.875), (4.35vw * 1), 1rem)",
	    ["lg"] = "clamp((1.125rem * 0.875), (4.35vw * 1.125), 1.125rem)",
	    ["xl"] = "clamp((1.25rem * 0.875), (4.35vw * 1.25), 1.25rem)",
	    ["2xl"] = "clamp((1.5rem * 0.875), (4.35vw * 1.5), 1.5rem)",
	    ["3xl"] = "clamp((1.875rem * 0.875), (4.35vw * 1.875), 1.875rem)",
	    ["4xl"] = "clamp((2.25rem * 0.875), (4.35vw * 2.25), 2.25rem)",
	    ["5xl"] = "clamp((3rem * 0.875), (4.35vw * 3), 3rem)",
	    ["6xl"] = "clamp((3.75rem * 0.875), (4.35vw * 3.75), 3.75rem)",
	    ["7xl"] = "clamp((4.5rem * 0.875), (4.35vw * 4.5), 4.5rem)",
	    ["8xl"] = "clamp((6rem * 0.875), (4.35vw * 6), 6rem)",
	    ["9xl"] = "clamp((8rem * 0.875), (4.35vw * 8), 8rem)"
    };

    public Dictionary<string,string> TextSizeLeadingOptions { get; } = new()
    {
        ["none"] = "0",
	    ["xs"] = "clamp((1rem * 0.875), (4.35vw * 1), 1rem)",
	    ["sm"] = "clamp((1.25rem * 0.875), (4.35vw * 1.25), 1.25rem)",
	    ["base"] = "clamp((1.5rem * 0.875), (4.35vw * 1.5), 1.5rem)",
	    ["lg"] = "clamp((1.75rem * 0.875), (4.35vw * 1.75), 1.75rem)",
	    ["xl"] = "clamp((1.75rem * 0.875), (4.35vw * 1.75), 1.75rem)",
	    ["2xl"] = "clamp((2rem * 0.875), (4.35vw * 2), 2rem)",
	    ["3xl"] = "clamp((2.25rem * 0.875), (4.35vw * 2.25), 2.25rem)",
	    ["4xl"] = "clamp((2.5rem * 0.875), (4.35vw * 2.5), 2.5rem)",
	    ["5xl"] = "1.1",
	    ["6xl"] = "1.1",
	    ["7xl"] = "1.1",
	    ["8xl"] = "1.1",
	    ["9xl"] = "1.1"
    };
    
    public Dictionary<string,string> LeadingOptions { get; } = new()
    {
        ["0"] = "0",
        ["1"] = "1",
	    ["3"] = "clamp((0.75rem * 0.875), (4.35vw * 0.75), 0.75rem)",
	    ["4"] = "clamp((1rem * 0.875), (4.35vw * 1), 1rem)",
	    ["5"] = "clamp((1.25rem * 0.875), (4.35vw * 1.25), 1.25rem)",
	    ["6"] = "clamp((1.5rem * 0.875), (4.35vw * 1.5), 1.5rem)",
	    ["7"] = "clamp((1.75rem * 0.875), (4.35vw * 1.75), 1.75rem)",
	    ["8"] = "clamp((2rem * 0.875), (4.35vw * 2), 2rem)",
	    ["9"] = "clamp((2.25rem * 0.875), (4.35vw * 2.25), 2.25rem)",
	    ["10"] = "clamp((2.5rem * 0.875), (4.35vw * 2.5), 2.5rem)",
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
        ["1"] = "1px",
	    ["2"] = 2.PxToRem(),
	    ["4"] = 4.PxToRem(),
	    ["8"] = 8.PxToRem()
    };
   
    public Dictionary<string, string> DivideWidthOptions { get; } = new()
    {
	    [""] = "1px",
        ["0"] = "0px",
        ["1"] = "1px",
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
    public Dictionary<string, string> FlexRemUnitOptions { get; set; } = new();
    public Dictionary<string, string> LayoutWholeNumberOptions { get; set; } = new();

    public Dictionary<string, string> EffectsFiltersOneBasedPercentageOptions { get; set; } = new();
    public Dictionary<string, string> FlexboxAndGridWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> FlexboxAndGridNegativeWholeNumberOptions { get; set; } = new();
    public Dictionary<string, string> PercentageOptions { get; set; } = new();
    
    #endregion

    #region Static Class Options

    #region Backgrounds

    public Dictionary<string, string> BgStaticUtilities { get; } = new()
    {
        ["repeat"] = "background-repeat: repeat;",
        ["no-repeat"] = "background-repeat: no-repeat;",
        ["repeat-x"] = "background-repeat: repeat-x;",
        ["repeat-y"] = "background-repeat: repeat-y;",
        ["repeat-round"] = "background-repeat: round;",
        ["repeat-space"] = "background-repeat: space;",
        ["origin-border"] = "background-origin: border-box;",
        ["origin-padding"] = "background-origin: padding-box;",
        ["origin-content"] = "background-origin: content-box;",
        ["fixed"] = "background-attachment: fixed;",
        ["local"] = "background-attachment: local;",
        ["scroll"] = "background-attachment: scroll;",
        ["border"] = "background-clip: border-box;",
        ["padding"] = "background-clip: padding-box;",
        ["content"] = "background-clip: content-box;",
        ["text"] = "background-clip: text;",
        ["bottom"] = "background-position: bottom;",
        ["center"] = "background-position: center;",
        ["left"] = "background-position: left;",
        ["left-bottom"] = "background-position: left bottom;",
        ["left-top"] = "background-position: left top;",
        ["right"] = "background-position: right;",
        ["right-bottom"] = "background-position: right bottom;",
        ["right-top"] = "background-position: right top;",
        ["top"] = "background-position: top;",
        ["auto"] = "background-size: auto;",
        ["cover"] = "background-size: cover;",
        ["contain"] = "background-size: contain;",
        ["none"] = "background-image: none;",
        ["gradient-to-t"] = "background-image: linear-gradient(to top, var(--sf-gradient-stops));",
        ["gradient-to-tr"] = "background-image: linear-gradient(to top right, var(--sf-gradient-stops));",
        ["gradient-to-r"] = "background-image: linear-gradient(to right, var(--sf-gradient-stops));",
        ["gradient-to-br"] = "background-image: linear-gradient(to bottom right, var(--sf-gradient-stops));",
        ["gradient-to-b"] = "background-image: linear-gradient(to bottom, var(--sf-gradient-stops));",
        ["gradient-to-bl"] = "background-image: linear-gradient(to bottom left, var(--sf-gradient-stops));",
        ["gradient-to-l"] = "background-image: linear-gradient(to left, var(--sf-gradient-stops));",
        ["gradient-to-tl"] = "background-image: linear-gradient(to top left, var(--sf-gradient-stops));"
    };

    public Dictionary<string, string> FromStaticUtilities { get; } = new();

    public Dictionary<string, string> ToStaticUtilities { get; } = new();
    public Dictionary<string, string> ViaStaticUtilities { get; } = new();
    
    #endregion
    
    #region Borders

    public Dictionary<string, string> BorderStaticUtilities { get; } = new()
    {
	    ["solid"] = "border-style: solid;",
	    ["dashed"] = "border-style: dashed;",
	    ["dotted"] = "border-style: dotted;",
	    ["double"] = "border-style: double;",
	    ["hidden"] = "border-style: hidden;",
	    ["none"] = "border-style: none;"
    }; 

    public Dictionary<string, string> DivideStaticUtilities { get; } = new()
    {
	    ["solid"] = """
	                & > * + * {
	                    border-style: solid;
	                }
	                """,
	    ["dashed"] = """
	                 & > * + * {
	                     border-style: dashed;
	                 }
	                 """,
	    ["dotted"] = """
	                 & > * + * {
	                     border-style: dotted;
	                 }
	                 """,
	    ["double"] = """
	                 & > * + * {
	                     border-style: double;
	                 }
	                 """,
	    ["none"] = """
	               & > * + * {
	                   border-style: none;
	               }
	               """
    }; 
    
    public Dictionary<string, string> DivideXStaticUtilities { get; } = new()
    {
	    ["reverse"] = "--sf-divide-x-reverse: 1;"
    }; 

    public Dictionary<string, string> DivideYStaticUtilities { get; } = new()
    {
	    ["reverse"] = "--sf-divide-y-reverse: 1;"
    }; 
    
    public Dictionary<string, string> OutlineStaticUtilities { get; } = new()
    {
	    [""] = "outline-style: solid;",
	    ["dashed"] = "outline-style: dashed;",
	    ["dotted"] = "outline-style: dotted;",
	    ["double"] = "outline-style: double;",
	    ["none"] = "outline-style: none;"
    }; 

    public Dictionary<string, string> RingInsetStaticUtilities { get; } = new()
    {
	    [""] = "--sf-ring-inset: inset;",
    }; 

    #endregion
    
    #region Effects
    
    public Dictionary<string, string> ShadowStaticUtilities { get; } = new()
    {
	    [""] = $"box-shadow: 0 1px {3.PxToRem()} 0 rgb(0 0 0 / 0.1), 0 1px {2.PxToRem()} -1px rgb(0 0 0 / 0.1);",
	    ["xs"] = "box-shadow: 0 0 0 1px rgb(0 0 0 / 0.05);",
	    ["sm"] = $"box-shadow: 0 1px {2.PxToRem()} 0 rgb(0 0 0 / 0.05);",
	    ["md"] = $"box-shadow: 0 {4.PxToRem()} {6.PxToRem()} -1px rgb(0 0 0 / 0.1), 0 {2.PxToRem()} {4.PxToRem()} -{2.PxToRem()} rgb(0 0 0 / 0.1);",
	    ["lg"] = $"box-shadow: 0 {10.PxToRem()} {15.PxToRem()} -{3.PxToRem()} rgb(0 0 0 / 0.1), 0 {4.PxToRem()} {6.PxToRem()} -{4.PxToRem()} rgb(0 0 0 / 0.1);",
	    ["xl"] = $"box-shadow: 0 {20.PxToRem()} {25.PxToRem()} -{5.PxToRem()} rgb(0 0 0 / 0.1), 0 {8.PxToRem()} {10.PxToRem()} -{6.PxToRem()} rgb(0 0 0 / 0.1);",
	    ["2xl"] = $"box-shadow: 0 {25.PxToRem()} {50.PxToRem()} -{12.PxToRem()} rgb(0 0 0 / 0.25);",
	    ["inner"] = $"box-shadow: inset 0 {2.PxToRem()} {4.PxToRem()} 0 rgb(0 0 0 / 0.05);",
	    ["none"] = "box-shadow: 0 0 #0000;"
    }; 

    #endregion
    
    #region Filters
    
    public Dictionary<string, string> BackdropGrayscaleStaticUtilities { get; } = new()
    {
	    [""] = "--sf-backdrop-grayscale: grayscale(100%);",
	    ["0"] = "--sf-backdrop-grayscale: grayscale(0);"
    }; 
    
    public Dictionary<string, string> BackdropHueRotateStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-backdrop-hue-rotate: hue-rotate(0deg);",
	    ["15"] = "--sf-backdrop-hue-rotate: hue-rotate(15deg);",
	    ["30"] = "--sf-backdrop-hue-rotate: hue-rotate(30deg);",
	    ["60"] = "--sf-backdrop-hue-rotate: hue-rotate(60deg);",
	    ["90"] = "--sf-backdrop-hue-rotate: hue-rotate(90deg);",
	    ["180"] = "--sf-backdrop-hue-rotate: hue-rotate(180deg);"
    }; 
    
    public Dictionary<string, string> BackdropInvertStaticUtilities { get; } = new()
    {
	    [""] = "--sf-backdrop-invert: invert(100%);",
	    ["0"] = "--sf-backdrop-invert: invert(0);"
    }; 

    public Dictionary<string, string> BackdropSepiaStaticUtilities { get; } = new()
    {
	    [""] = "--sf-backdrop-sepia: sepia(100%);",
	    ["0"] = "--sf-backdrop-sepia: sepia(0);"
    }; 

    public Dictionary<string, string> DropShadowStaticUtilities { get; } = new()
    {
	    [""] = $"--sf-drop-shadow: drop-shadow(0 1px {2.PxToRem()} rgb(0 0 0 / 0.1)) drop-shadow(0 1px 1px rgb(0 0 0 / 0.06));",
	    ["sm"] = "--sf-drop-shadow: drop-shadow(0 1px 1px rgb(0 0 0 / 0.05));",
	    ["md"] = $"--sf-drop-shadow: drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.07)) drop-shadow(0 {2.PxToRem()} {2.PxToRem()} rgb(0 0 0 / 0.06));",
	    ["lg"] = $"--sf-drop-shadow: drop-shadow(0 {10.PxToRem()} {8.PxToRem()} rgb(0 0 0 / 0.04)) drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.1));",
	    ["xl"] = $"--sf-drop-shadow: drop-shadow(0 {20.PxToRem()} {13.PxToRem()} rgb(0 0 0 / 0.03)) drop-shadow(0 {8.PxToRem()} {5.PxToRem()} rgb(0 0 0 / 0.08));",
	    ["2xl"] = $"--sf-drop-shadow: drop-shadow(0 {25.PxToRem()} {25.PxToRem()} rgb(0 0 0 / 0.15));",
	    ["none"] = "--sf-drop-shadow: drop-shadow(0 0 #0000);"
    }; 
    
    public Dictionary<string, string> GrayscaleStaticUtilities { get; } = new()
    {
	    [""] = "--sf-grayscale: grayscale(100%);",
	    ["0"] = "--sf-grayscale: grayscale(0);"
    }; 

    public Dictionary<string, string> HueRotateStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-hue-rotate: hue-rotate(0deg);",
	    ["15"] = "--sf-hue-rotate: hue-rotate(15deg);",
	    ["30"] = "--sf-hue-rotate: hue-rotate(30deg);",
	    ["60"] = "--sf-hue-rotate: hue-rotate(60deg);",
	    ["90"] = "--sf-hue-rotate: hue-rotate(90deg);",
	    ["180"] = "--sf-hue-rotate: hue-rotate(180deg);"
    }; 
    
    public Dictionary<string, string> InvertStaticUtilities { get; } = new()
    {
	    [""] = "--sf-invert: invert(100%);",
	    ["0"] = "--sf-invrt: invert(0);"
    }; 
    
    public Dictionary<string, string> SepiaStaticUtilities { get; } = new()
    {
	    [""] = "--sf-sepia: sepia(100%);",
	    ["0"] = "--sf-sepia: sepia(0);"
    }; 
    
    #endregion
    
    #region Flexbox and Grid
    
    public Dictionary<string, string> AutoColsStaticUtilities { get; } = new()
    {
	    ["auto"] = "grid-auto-columns: auto;",
	    ["min"] = "grid-auto-columns: min-content;",
	    ["max"] = "grid-auto-columns: max-content;",
	    ["fr"] = "grid-auto-columns: minmax(0, 1fr);"
    }; 

    public Dictionary<string, string> AutoRowsStaticUtilities { get; } = new()
    {
	    ["auto"] = "grid-auto-rows: auto;",
	    ["min"] = "grid-auto-rows: min-content;",
	    ["max"] = "grid-auto-rows: max-content;",
	    ["fr"] = "grid-auto-rows: minmax(0, 1fr);"
    }; 

    public Dictionary<string, string> BasisStaticUtilities { get; } = new()
    {
	    ["full"] = "flex-basis: 100%;"
    };

    public Dictionary<string, string> ColStaticUtilities { get; } = new()
    {
        ["auto"] = "grid-column: auto;",

        ["span-1"] = "grid-column: span 1 / span 1;",
        ["span-2"] = "grid-column: span 2 / span 2;",
        ["span-3"] = "grid-column: span 3 / span 3;",
        ["span-4"] = "grid-column: span 4 / span 4;",
        ["span-5"] = "grid-column: span 5 / span 5;",
        ["span-6"] = "grid-column: span 6 / span 6;",
        ["span-7"] = "grid-column: span 7 / span 7;",
        ["span-8"] = "grid-column: span 8 / span 8;",
        ["span-9"] = "grid-column: span 9 / span 9;",
        ["span-10"] = "grid-column: span 10 / span 10;",
        ["span-11"] = "grid-column: span 11 / span 11;",
        ["span-12"] = "grid-column: span 12 / span 12;",
        ["span-full"] = "grid-column: 1 / -1;",
        
        ["start-1"] = "grid-column-start: 1;",
        ["start-2"] = "grid-column-start: 2;",
        ["start-3"] = "grid-column-start: 3;",
        ["start-4"] = "grid-column-start: 4;",
        ["start-5"] = "grid-column-start: 5;",
        ["start-6"] = "grid-column-start: 6;",
        ["start-7"] = "grid-column-start: 7;",
        ["start-8"] = "grid-column-start: 8;",
        ["start-9"] = "grid-column-start: 9;",
        ["start-10"] = "grid-column-start: 10;",
        ["start-11"] = "grid-column-start: 11;",
        ["start-12"] = "grid-column-start: 12;",
        ["start-13"] = "grid-column-start: 13;",
        ["start-auto"] = "grid-column-start: auto;",

        ["end-1"] = "grid-column-end: 1;",
        ["end-2"] = "grid-column-end: 2;",
        ["end-3"] = "grid-column-end: 3;",
        ["end-4"] = "grid-column-end: 4;",
        ["end-5"] = "grid-column-end: 5;",
        ["end-6"] = "grid-column-end: 6;",
        ["end-7"] = "grid-column-end: 7;",
        ["end-8"] = "grid-column-end: 8;",
        ["end-9"] = "grid-column-end: 9;",
        ["end-10"] = "grid-column-end: 10;",
        ["end-11"] = "grid-column-end: 11;",
        ["end-12"] = "grid-column-end: 12;",
        ["end-13"] = "grid-column-end: 13;",
        ["end-auto"] = "grid-column-end: auto;",
    };
    
    public Dictionary<string, string> ContentAroundStaticUtilities { get; } = new()
    {
	    [""] = "align-content: space-around;",
    }; 

    public Dictionary<string, string> ContentBaselineStaticUtilities { get; } = new()
    {
	    [""] = "align-content: baseline;",
    }; 

    public Dictionary<string, string> ContentBetweenStaticUtilities { get; } = new()
    {
	    [""] = "align-content: space-between;",
    }; 

    public Dictionary<string, string> ContentCenterStaticUtilities { get; } = new()
    {
	    [""] = "align-content: center;",
    }; 

    public Dictionary<string, string> ContentEndStaticUtilities { get; } = new()
    {
	    [""] = "align-content: flex-end;",
    }; 

    public Dictionary<string, string> ContentEvenlyStaticUtilities { get; } = new()
    {
	    [""] = "align-content: space-evenly;",
    }; 

    public Dictionary<string, string> ContentNormalStaticUtilities { get; } = new()
    {
	    [""] = "align-content: normal;",
    }; 

    public Dictionary<string, string> ContentStartStaticUtilities { get; } = new()
    {
	    [""] = "align-content: flex-start;",
    }; 

    public Dictionary<string, string> ContentStretchStaticUtilities { get; } = new()
    {
	    [""] = "align-content: stretch;"
    }; 

    public Dictionary<string, string> FlexStaticUtilities { get; } = new()
    {
	    [""] = "display: flex;",

	    ["row"] = "flex-direction: row;",
	    ["row-reverse"] = "flex-direction: row-reverse;",
	    ["col"] = "flex-direction: column;",
	    ["col-reverse"] = "flex-direction: column-reverse;",
        
	    ["wrap"] = "flex-wrap: wrap;",
	    ["wrap-reverse"] = "flex-wrap: wrap-reverse;",
	    ["nowrap"] = "flex-wrap: nowrap;",
        
	    ["1"] = "flex: 1 1 0%;",
	    ["auto"] = "flex: 1 1 auto;",
	    ["initial"] = "flex: 0 1 auto;",
	    ["none"] = "flex: none;"
    }; 

    public Dictionary<string, string> FlexGrowStaticUtilities { get; } = new()
    {
        [""] = "flex-grow: 1;",
        ["0"] = "flex-grow: 0;",
    }; 

    public Dictionary<string, string> FlexShrinkStaticUtilities { get; } = new()
    {
        [""] = "flex-shrink: 1;",
        ["0"] = "flex-shrink: 0;"
    }; 
    
    public Dictionary<string, string> GapStaticUtilities { get; } = new()
    {
	    ["0"] = "gap: auto;",
	    ["px"] = "gap: min-content;",
    }; 

    public Dictionary<string, string> GapXStaticUtilities { get; } = new()
    {
	    ["0"] = "column-gap: auto;",
	    ["px"] = "column-gap: min-content;",
    }; 

    public Dictionary<string, string> GapYStaticUtilities { get; } = new()
    {
	    ["0"] = "row-gap: auto;",
	    ["px"] = "row-gap: min-content;",
    }; 

    public Dictionary<string, string> GridColsStaticUtilities { get; } = new()
    {
	    ["none"] = "grid-template-columns: none;",
    }; 

    public Dictionary<string, string> GridFlowStaticUtilities { get; } = new()
    {
	    ["row"] = "grid-auto-flow: row;",
	    ["col"] = "grid-auto-flow: column;",
	    ["dense"] = "grid-auto-flow: dense;",
	    ["row-dense"] = "grid-auto-flow: row dense;",
	    ["col-dense"] = "grid-auto-flow: column dense;"
    }; 

    public Dictionary<string, string> GridRowsStaticUtilities { get; } = new()
    {
	    ["none"] = "grid-template-rows: none;",
    }; 

    public Dictionary<string, string> ItemsStaticUtilities { get; } = new()
    {
	    ["start"] = "align-items: flex-start;",
	    ["end"] = "align-items: flex-end;",
	    ["center"] = "align-items: center;",
	    ["baseline"] = "align-items: baseline;",
	    ["stretch"] = "align-items: stretch;"
    }; 

    public Dictionary<string, string> JustifyStaticUtilities { get; } = new()
    {
	    ["normal"] = "justify-content: normal;",
	    ["start"] = "justify-content: flex-start;",
	    ["end"] = "justify-content: flex-end;",
	    ["center"] = "justify-content: center;",
	    ["between"] = "justify-content: space-between;",
	    ["around"] = "justify-content: space-around;",
	    ["evenly"] = "justify-content: space-evenly;",
	    ["stretch"] = "justify-content: stretch;"
    }; 

    public Dictionary<string, string> JustifyItemsStaticUtilities { get; } = new()
    {
	    ["start"] = "justify-items: start;",
	    ["end"] = "justify-items: end;",
	    ["center"] = "justify-items: center;",
	    ["stretch"] = "justify-items: stretch;"
    }; 

    public Dictionary<string, string> JustifySelfStaticUtilities { get; } = new()
    {
	    ["auto"] = "justify-self: auto;",
	    ["start"] = "justify-self: start;",
	    ["end"] = "justify-self: end;",
	    ["center"] = "justify-self: center;",
	    ["stretch"] = "justify-self: stretch;"
    }; 

    public Dictionary<string, string> OrderStaticUtilities { get; } = new()
    {
	    ["first"] = $"order: {int.MinValue.ToString()};",
	    ["last"] = $"order: {int.MaxValue.ToString()};",
	    ["none"] = "order: 0;"
    }; 

    public Dictionary<string, string> PlaceContentStaticUtilities { get; } = new()
    {
	    ["start"] = "place-content: start;",
	    ["end"] = "place-content: end;",
	    ["center"] = "place-content: center;",
	    ["between"] = "place-content: space-between;",
	    ["around"] = "place-content: space-around;",
	    ["evenly"] = "place-content: space-evenly;",
	    ["baseline"] = "place-content: baseline;",
	    ["stretch"] = "place-content: stretch;"
    }; 

    public Dictionary<string, string> PlaceItemsStaticUtilities { get; } = new()
    {
	    ["start"] = "place-items: start;",
	    ["end"] = "place-items: end;",
	    ["center"] = "place-items: center;",
	    ["baseline"] = "place-items: baseline;",
	    ["stretch"] = "place-items: stretch;"
    }; 

    public Dictionary<string, string> PlaceSelfStaticUtilities { get; } = new()
    {
	    ["auto"] = "place-self: auto;",
	    ["start"] = "place-self: start;",
	    ["end"] = "place-self: end;",
	    ["center"] = "place-self: center;",
	    ["stretch"] = "place-self: stretch;"
    }; 

    public Dictionary<string, string> RowAutoStaticUtilities { get; } = new()
    {
	    [""] = "grid-row: auto;",
    }; 

    public Dictionary<string, string> RowEndStaticUtilities { get; } = new()
    {
	    ["auto"] = "grid-row-end: auto;",
    }; 

    public Dictionary<string, string> RowSpanStaticUtilities { get; } = new()
    {
	    ["full"] = "grid-row: 1 / -1;",
    }; 

    public Dictionary<string, string> RowStartStaticUtilities { get; } = new()
    {
	    ["auto"] = "grid-row-start: auto;",
    };

    public Dictionary<string, string> SelfStaticUtilities { get; } = new()
    {
	    ["auto"] = "align-self: auto;",
	    ["start"] = "align-self: flex-start;",
	    ["end"] = "align-self: flex-end;",
	    ["center"] = "align-self: center;",
	    ["baseline"] = "align-self: baseline;",
	    ["stretch"] = "align-self: stretch;"
    }; 
    
    #endregion

    #region Interactivity
    
    public Dictionary<string, string> AppearanceStaticUtilities { get; } = new()
    {
	    ["none"] = "appearance: none;",
    };

    public Dictionary<string, string> CursorStaticUtilities { get; } = new()
    {
	    ["alias"] = "cursor: alias;",
	    ["all-scroll"] = "cursor: all-scroll;",
	    ["auto"] = "cursor: auto;",
	    ["cell"] = "cursor: cell;",
	    ["context-menu"] = "cursor: context-menu;",
	    ["col-resize"] = "cursor: col-resize;",
	    ["copy"] = "cursor: copy;",
	    ["crosshair"] = "cursor: crosshair;",
	    ["default"] = "cursor: default;",
	    ["e-resize"] = "cursor: e-resize;",
	    ["ew-resize"] = "cursor: ew-resize;",
	    ["grab"] = "cursor: grab;",
	    ["grabbing"] = "cursor: grabbing;",
	    ["help"] = "cursor: help;",
	    ["move"] = "cursor: move;",
	    ["n-resize"] = "cursor: n-resize;",
	    ["ne-resize"] = "cursor: ne-resize;",
	    ["nesw-resize"] = "cursor: nesw-resize;",
	    ["ns-resize"] = "cursor: ns-resize;",
	    ["nw-resize"] = "cursor: nw-resize;",
	    ["nwse-resize"] = "cursor: nwse-resize;",
	    ["no-drop"] = "cursor: no-drop;",
	    ["none"] = "cursor: none;",
	    ["not-allowed"] = "cursor: not-allowed;",
	    ["pointer"] = "cursor: pointer;",
	    ["progress"] = "cursor: progress;",
	    ["row-resize"] = "cursor: row-resize;",
	    ["s-resize"] = "cursor: s-resize;",
	    ["se-resize"] = "cursor: se-resize;",
	    ["sw-resize"] = "cursor: sw-resize;",
	    ["text"] = "cursor: text;",
	    ["vertical-text"] = "cursor: vertical-text;",
	    ["w-resize"] = "cursor: w-resize;",
	    ["wait"] = "cursor: wait;",
	    ["zoom-in"] = "cursor: zoom-in;",
	    ["zoom-out"] = "cursor: zoom-out;"
    };

    public Dictionary<string, string> PointerEventsStaticUtilities { get; } = new()
    {
	    ["none"] = "pointer-events: none;",
	    ["auto"] = "pointer-events: auto;"
    }; 

    public Dictionary<string, string> ResizeStaticUtilities { get; } = new()
    {
	    [""] = "resize: both;",
	    ["none"] = "resize: none;",
	    ["y"] = "resize: vertical;",
	    ["x"] = "resize: horizontal;"
    }; 

    public Dictionary<string, string> ScrollStaticUtilities { get; } = new()
    {
	    ["auto"] = "scroll-behavior: auto;",
	    ["smooth"] = "scroll-behavior: smooth;",
    };

    public Dictionary<string, string> ScrollMStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin: 0;",
	    ["px"] = "scroll-margin: 1px;",
    }; 

    public Dictionary<string, string> ScrollMbStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin-bottom: 0;",
	    ["px"] = "scroll-margin-bottom: 1px;",
    }; 

    public Dictionary<string, string> ScrollMeStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin-inline-end: 0;",
	    ["px"] = "scroll-margin-inline-end: 1px;",
    }; 

    public Dictionary<string, string> ScrollMlStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin-left: 0;",
	    ["px"] = "scroll-margin-left: 1px;",
    }; 

    public Dictionary<string, string> ScrollMrStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin-right: 0;",
	    ["px"] = "scroll-margin-right: 1px;",
    }; 

    public Dictionary<string, string> ScrollMsStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin-inline-start: 0;",
	    ["px"] = "scroll-margin-inline-start: 1px;",
    }; 

    public Dictionary<string, string> ScrollMtStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-margin-top: 0;",
	    ["px"] = "scroll-margin-top: 1px;",
    }; 

    public Dictionary<string, string> ScrollMxStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            scroll-margin-left: 0;
	            scroll-margin-right: 0;
	            """,
	    ["px"] = """
	             scroll-margin-left: 1px;
	             scroll-margin-right: 1px;
	             """,
    };

    public Dictionary<string, string> ScrollMyStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            scroll-margin-top: 0;
	            scroll-margin-bottom: 0;
	            """,
	    ["px"] = """
	             scroll-margin-top: 1px;
	             scroll-margin-bottom: 1px;
	             """,
    };

    public Dictionary<string, string> ScrollPStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding: 0;",
	    ["px"] = "scroll-padding: 1px;",
    }; 

    public Dictionary<string, string> ScrollPbStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding-bottom: 0;",
	    ["px"] = "scroll-padding-bottom: 1px;",
    }; 

    public Dictionary<string, string> ScrollPeStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding-inline-end: 0;",
	    ["px"] = "scroll-padding-inline-end: 1px;",
    }; 

    public Dictionary<string, string> ScrollPlStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding-left: 0;",
	    ["px"] = "scroll-padding-left: 1px;",
    }; 

    public Dictionary<string, string> ScrollPrStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding-right: 0;",
	    ["px"] = "scroll-padding-right: 1px;",
    };

    public Dictionary<string, string> ScrollPsStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding-inline-start: 0;",
	    ["px"] = "scroll-padding-inline-start: 1px;",
    }; 

    public Dictionary<string, string> ScrollPtStaticUtilities { get; } = new()
    {
	    ["0"] = "scroll-padding-top: 0;",
	    ["px"] = "scroll-padding-top: 1px;",
    }; 

    public Dictionary<string, string> ScrollPxStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            scroll-padding-left: 0;
	            scroll-padding-right: 0;
	            """,
	    ["px"] = """
	             scroll-padding-left: 1px;
	             scroll-padding-right: 1px;
	             """,
    }; 

    public Dictionary<string, string> ScrollPyStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            scroll-padding-top: 0;
	            scroll-padding-bottom: 0;
	            """,
	    ["px"] = """
	             scroll-padding-top: 1px;
	             scroll-padding-bottom: 1px;
	             """,
    }; 

    public Dictionary<string, string> SelectStaticUtilities { get; } = new()
    {
	    ["none"] = "user-select: none;",
	    ["text"] = "user-select: text;",
	    ["all"] = "user-select: all;",
	    ["auto"] = "user-select: auto;"
    }; 

    public Dictionary<string, string> SnapStaticUtilities { get; } = new()
    {
	    ["start"] = "scroll-snap-align: start;",
	    ["end"] = "scroll-snap-align: end;",
	    ["center"] = "scroll-snap-align: center;",
	    ["align-none"] = "scroll-snap-align: none;",
	    ["normal"] = "scroll-snap-stop: normal;",
	    ["always"] = "scroll-snap-stop: always;",
	    ["none"] = "scroll-snap-type: none;",
	    ["x"] = "scroll-snap-type: x var(--sf-scroll-snap-strictness);",
	    ["y"] = "scroll-snap-type: y var(--sf-scroll-snap-strictness);",
	    ["both"] = "scroll-snap-type: both var(--sf-scroll-snap-strictness);",
	    ["mandatory"] = "--sf-scroll-snap-strictness: mandatory;",
	    ["proximity"] = "--sf-scroll-snap-strictness: proximity;"
    }; 

    public Dictionary<string, string> TouchStaticUtilities { get; } = new()
    {
	    ["auto"] = "touch-action: auto;",
	    ["none"] = "touch-action: none;",
	    ["pan-x"] = "touch-action: pan-x;",
	    ["pan-left"] = "touch-action: pan-left;",
	    ["pan-right"] = "touch-action: pan-right;",
	    ["pan-y"] = "touch-action: pan-y;",
	    ["pan-up"] = "touch-action: pan-up;",
	    ["pan-down"] = "touch-action: pan-down;",
	    ["pinch-zoom"] = "touch-action: pinch-zoom;",
	    ["manipulation"] = "touch-action: manipulation;"
    }; 

    public Dictionary<string, string> WillChangeStaticUtilities { get; } = new()
    {
	    ["auto"] = "will-change: auto;",
	    ["scroll"] = "will-change: scroll-position;",
	    ["contents"] = "will-change: contents;",
	    ["transform"] = "will-change: transform;"
    }; 

    #endregion
    
    #region Layout
    
    public Dictionary<string, string> AbsoluteStaticUtilities { get; } = new()
    {
	    [""] = "position: absolute;",
    }; 

    public Dictionary<string, string> AspectStaticUtilities { get; } = new()
    {
	    ["auto"] = "aspect-ratio: auto;",
	    ["square"] = "aspect-ratio: 1/1;",
	    ["video"] = "aspect-ratio: 16/9;",
	    ["screen"] = "aspect-ratio: 4/3;"
    }; 

    public Dictionary<string, string> BottomStaticUtilities { get; } = new()
    {
	    ["0"] = "bottom: 0px;",
	    ["px"] = "bottom: 1px;",
	    ["auto"] = "bottom: auto;",
	    ["full"] = "100%"
    };

    public Dictionary<string, string> BoxStaticUtilities { get; } = new()
    {
	    ["border"] = "box-sizing: border-box;",
	    ["content"] = "box-sizing: content-box;",
    }; 

    public Dictionary<string, string> BoxDecorationStaticUtilities { get; } = new()
    {
	    ["clone"] = "box-decoration-break: clone;",
	    ["slice"] = "box-decoration-break: slice;",
    }; 

    public Dictionary<string, string> BreakAfterStaticUtilities { get; } = new()
    {
	    ["auto"] = "break-after: auto;",
	    ["avoid"] = "break-after: avoid;",
	    ["all"] = "break-after: all;",
	    ["avoid-page"] = "break-after: avoid-page;",
	    ["page"] = "break-after: page;",
	    ["left"] = "break-after: left;",
	    ["right"] = "break-after: right;",
	    ["column"] = "break-after: column;"
    }; 

    public Dictionary<string, string> BreakBeforeStaticUtilities { get; } = new()
    {
	    ["auto"] = "break-before: auto;",
	    ["avoid"] = "break-before: avoid;",
	    ["all"] = "break-before: all;",
	    ["avoid-page"] = "break-before: avoid-page;",
	    ["page"] = "break-before: page;",
	    ["left"] = "break-before: left;",
	    ["right"] = "break-before: right;",
	    ["column"] = "break-before: column;"
    }; 

    public Dictionary<string, string> BreakInsideStaticUtilities { get; } = new()
    {
	    ["auto"] = "break-inside: auto;",
	    ["avoid"] = "break-inside: avoid;",
	    ["avoid-page"] = "break-inside: avoid-page;",
	    ["avoid-column"] = "break-inside: column;"
    }; 

    public Dictionary<string, string> ClearStaticUtilities { get; } = new()
    {
	    ["right"] = "clear: right;",
	    ["left"] = "clear: left;",
	    ["both"] = "clear: both;",
	    ["none"] = "clear: none;",
    }; 

    public Dictionary<string, string> CollapseStaticUtilities { get; } = new()
    {
	    [""] = "visibility: collapse;",
    }; 

    public Dictionary<string, string> ColumnsStaticUtilities { get; } = new()
    {
	    ["auto"] = "columns: auto;",
	    ["3xs"] = "columns: 16rem;",
	    ["2xs"] = "columns: 18rem;",
	    ["xs"] = "columns: 20rem;",
	    ["sm"] = "columns: 24rem;",
	    ["md"] = "columns: 28rem;",
	    ["lg"] = "columns: 32rem;",
	    ["xl"] = "columns: 36rem;",
	    ["2xl"] = "columns: 42rem;",
	    ["3xl"] = "columns: 48rem;",
	    ["4xl"] = "columns: 56rem;",
	    ["5xl"] = "columns: 64rem;",
	    ["6xl"] = "columns: 72rem;",
	    ["7xl"] = "columns: 80rem;"
    }; 

    public Dictionary<string, string> ContainerStaticUtilities { get; } = new()
    {
	    [""] = """
	           width: 100%;
	           margin-left: auto;
	           margin-right: auto;

	           @include sf-media($from: $sm-breakpoint) {
	              max-width: $sm-breakpoint;
	           }

	           @include sf-media($from: $md-breakpoint) {
	              max-width: $md-breakpoint;
	           }

	           @include sf-media($from: $lg-breakpoint) {
	              max-width: $lg-breakpoint;
	           }

	           @include sf-media($from: $xl-breakpoint) {
	              max-width: $xl-breakpoint;
	           }

	           @include sf-media($from: $xxl-breakpoint) {
	              max-width: $xxl-breakpoint;
	           }
	           """,
    }; 

    public Dictionary<string, string> ElasticStaticUtilities { get; } = new()
    {
        [""] = """
               width: 100%;
               margin-left: auto;
               margin-right: auto;
               
               @include sf-media($from: $sm-breakpoint) {
                   max-width: calc(#{$md-breakpoint} - 1px);
               }
               
               @include sf-media($from: $md-breakpoint) {
                   max-width: calc(#{$lg-breakpoint} - 1px);
               }
               
               @include sf-media($from: $lg-breakpoint) {
                   max-width: calc(#{$xl-breakpoint} - 1px);
               }
               
               @include sf-media($from: $xl-breakpoint) {
                   max-width: calc(#{$xxl-breakpoint} - 1px);
               }
               
               @include sf-media($from: $xxl-breakpoint) {
                   max-width: $xxl-breakpoint;
               }
               """,
    }; 

    public Dictionary<string, string> DisplayBlockStaticUtilities { get; } = new()
    {
	    [""] = "display: block;"
    }; 

    public Dictionary<string, string> DisplayContentsStaticUtilities { get; } = new()
    {
	    [""] = "display: contents;"
    }; 

    public Dictionary<string, string> DisplayFlowRootStaticUtilities { get; } = new()
    {
	    [""] = "display: flow-root;"
    }; 

    public Dictionary<string, string> DisplayGridStaticUtilities { get; } = new()
    {
	    [""] = "display: grid;"
    }; 

    public Dictionary<string, string> DisplayHiddenStaticUtilities { get; } = new()
    {
	    [""] = "display: none;"
    }; 

    public Dictionary<string, string> DisplayInlineStaticUtilities { get; } = new()
    {
	    [""] = "display: inline;"
    }; 

    public Dictionary<string, string> DisplayInlineBlockStaticUtilities { get; } = new()
    {
	    [""] = "display: inline-block;"
    }; 

    public Dictionary<string, string> DisplayInlineFlexStaticUtilities { get; } = new()
    {
	    [""] = "display: inline-flex;"
    }; 

    public Dictionary<string, string> DisplayInlineGridStaticUtilities { get; } = new()
    {
	    [""] = "display: inline-grid;"
    }; 

    public Dictionary<string, string> DisplayInlineTableStaticUtilities { get; } = new()
    {
	    [""] = "display: inline-table;"
    }; 

    public Dictionary<string, string> DisplayListItemStaticUtilities { get; } = new()
    {
	    [""] = "display: list-item;"
    };

    public Dictionary<string, string> DisplayTableStaticUtilities { get; } = new()
    {
	    [""] = "display: table;"
    };

    public Dictionary<string, string> DisplayTableCaptionStaticUtilities { get; } = new()
    {
	    [""] = "display: table-caption;"
    };

    public Dictionary<string, string> DisplayTableCellStaticUtilities { get; } = new()
    {
	    [""] = "display: table-cell;"
    }; 

    public Dictionary<string, string> DisplayTableColumnStaticUtilities { get; } = new()
    {
	    [""] = "display: table-column;"
    }; 

    public Dictionary<string, string> DisplayTableColumnGroupStaticUtilities { get; } = new()
    {
	    [""] = "display: table-column-group;"
    };

    public Dictionary<string, string> DisplayTableFooterGroupStaticUtilities { get; } = new()
    {
	    [""] = "display: table-footer-group;"
    }; 

    public Dictionary<string, string> DisplayTableHeaderGroupStaticUtilities { get; } = new()
    {
	    [""] = "display: table-header-group;"
    }; 

    public Dictionary<string, string> DisplayTableRowStaticUtilities { get; } = new()
    {
	    [""] = "display: table-row;"
    }; 

    public Dictionary<string, string> DisplayTableRowGroupStaticUtilities { get; } = new()
    {
	    [""] = "display: table-row-group;"
    }; 

    public Dictionary<string, string> EndStaticUtilities { get; } = new()
    {
	    ["0"] = "inset-inline-end: 0px;",
	    ["px"] = "inset-inline-end: 1px;",
	    ["auto"] = "inset-inline-end: auto;",
	    ["full"] = "inset-inline-end: 100%;",
    };

    public Dictionary<string, string> FixedStaticUtilities { get; } = new()
    {
	    [""] = "position: fixed;",
    };

    public Dictionary<string, string> FloatStaticUtilities { get; } = new()
    {
	    ["right"] = "float: right;",
	    ["left"] = "float: left;",
	    ["none"] = "float: none;",
    }; 

    public Dictionary<string, string> InsetStaticUtilities { get; } = new()
    {
	    ["0"] = "inset: 0px;",
	    ["px"] = "inset: 1px;",
	    ["auto"] = "inset: auto;",
	    ["full"] = "inset: 100%;",
    }; 

    public Dictionary<string, string> InsetXStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            left: 0px;
	            right: 0px;
	            """,
	    ["px"] = """
	             left: 1px;
	             right: 1px;
	             """,
	    ["auto"] = """
	               left: auto;
	               right: auto;
	               """,
	    ["full"] = """
	               left: 100%;
	               right: 100%;
	               """,
    };

    public Dictionary<string, string> InsetYStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            top: 0px;
	            bottom: 0px;
	            """,
	    ["px"] = """
	             top: 1px;
	             bottom: 1px;
	             """,
	    ["auto"] = """
	               top: auto;
	               bottom: auto;
	               """,
	    ["full"] = """
	               top: 100%;
	               bottom: 100%;
	               """,
    };

    public Dictionary<string, string> InvisibleStaticUtilities { get; } = new()
    {
	    [""] = "visibility: hidden;",
    };

    public Dictionary<string, string> IsolateStaticUtilities { get; } = new()
    {
	    [""] = "isolation: isolate;",
    };

    public Dictionary<string, string> IsolationAutoStaticUtilities { get; } = new()
    {
	    [""] = "isolation: auto;",
    };

    public Dictionary<string, string> LeftStaticUtilities { get; } = new()
    {
	    ["0"] = "left: 0px;",
	    ["px"] = "left: 1px;",
	    ["auto"] = "left: auto;",
	    ["full"] = "left: 100%;",
    };

    public Dictionary<string, string> ObjectStaticUtilities { get; } = new()
    {
	    ["contain"] = "object-fit: contain;",
	    ["cover"] = "object-fit: cover;",
	    ["fill"] = "object-fit: fill;",
	    ["none"] = "object-fit: none;",
	    ["scale-down"] = "object-fit: scale-down;",
	    ["bottom"] = "object-position: bottom;",
	    ["center"] = "object-position: center;",
	    ["left"] = "object-position: left;",
	    ["left-bottom"] = "object-position: left bottom;",
	    ["left-top"] = "object-position: left top;",
	    ["right"] = "object-position: right;",
	    ["right-bottom"] = "object-position: right bottom;",
	    ["right-top"] = "object-position: right top;",
	    ["top"] = "object-position: top;"
    };

    public Dictionary<string, string> OverflowStaticUtilities { get; } = new()
    {
	    ["auto"] = "overflow: auto;",
	    ["hidden"] = "overflow: hidden;",
	    ["clip"] = "overflow: clip;",
	    ["visible"] = "overflow: visible;",
	    ["scroll"] = "overflow: scroll;"
    };

    public Dictionary<string, string> OverflowXStaticUtilities { get; } = new()
    {
	    ["auto"] = "overflow-x: auto;",
	    ["hidden"] = "overflow-x: hidden;",
	    ["clip"] = "overflow-x: clip;",
	    ["visible"] = "overflow-x: visible;",
	    ["scroll"] = "overflow-x: scroll;"
    }; 

    public Dictionary<string, string> OverflowYStaticUtilities { get; } = new()
    {
	    ["auto"] = "overflow-y: auto;",
	    ["hidden"] = "overflow-y: hidden;",
	    ["clip"] = "overflow-y: clip;",
	    ["visible"] = "overflow-y: visible;",
	    ["scroll"] = "overflow-y: scroll;"
    };

    public Dictionary<string, string> OverscrollStaticUtilities { get; } = new()
    {
	    ["auto"] = "overscroll-behavior: auto;",
	    ["contain"] = "overscroll-behavior: contain;",
	    ["none"] = "overscroll-behavior: none;",
    }; 

    public Dictionary<string, string> OverscrollXStaticUtilities { get; } = new()
    {
	    ["auto"] = "overscroll-behavior-x: auto;",
	    ["contain"] = "overscroll-behavior-x: contain;",
	    ["none"] = "overscroll-behavior-x: none;",
    }; 

    public Dictionary<string, string> OverscrollYStaticUtilities { get; } = new()
    {
	    ["auto"] = "overscroll-behavior-y: auto;",
	    ["contain"] = "overscroll-behavior-y: contain;",
	    ["none"] = "overscroll-behavior-y: none;",
    }; 

    public Dictionary<string, string> RelativeStaticUtilities { get; } = new()
    {
	    [""] = "position: relative;",
    }; 

    public Dictionary<string, string> RightStaticUtilities { get; } = new()
    {
	    ["0"] = "right: 0px;",
	    ["px"] = "right: 1px;",
	    ["auto"] = "right: auto;",
	    ["full"] = "right: 100%;",
    };

    public Dictionary<string, string> StartStaticUtilities { get; } = new()
    {
	    ["0"] = "inset-inline-start: 0px;",
	    ["px"] = "inset-inline-start: 1px;",
	    ["auto"] = "inset-inline-start: auto;",
	    ["full"] = "inset-inline-start: 100%;",
    }; 

    public Dictionary<string, string> StaticStaticUtilities { get; } = new()
    {
	    [""] = "position: static;",
    }; 

    public Dictionary<string, string> StickyStaticUtilities { get; } = new()
    {
	    [""] = "position: sticky;",
    }; 

    public Dictionary<string, string> TopStaticUtilities { get; } = new()
    {
	    ["0"] = "top: 0px;",
	    ["px"] = "top: 1px;",
	    ["auto"] = "top: auto;",
	    ["full"] = "top: 100%;",
    }; 

    public Dictionary<string, string> VisibleStaticUtilities { get; } = new()
    {
	    [""] = "visibility: visible;",
    }; 

    public Dictionary<string, string> ZStaticUtilities { get; } = new()
    {
	    ["auto"] = "z-index: auto;",
	    ["top"] = $"z-index: {int.MaxValue.ToString()};",
	    ["bottom"] = $"z-index: {int.MinValue.ToString()};",
	    ["0"] = "z-index: 0;",
	    ["10"] = "z-index: 10;",
	    ["20"] = "z-index: 20;",
	    ["30"] = "z-index: 30;",
	    ["40"] = "z-index: 40;",
	    ["50"] = "z-index: 50;"
    }; 
    
    #endregion
    
    #region Sizing
    
    public Dictionary<string, string> HStaticUtilities { get; } = new()
    {
	    ["0"] = "height: 0px;",
	    ["px"] = "height: 1px;",
	    ["auto"] = "height: auto;",
	    ["screen"] = "height: 100vh;",
	    ["min"] = "height: min-content;",
	    ["max"] = "height: max-content;",
	    ["fit"] = "height: fit-content;",
	    ["full"] = "height: 100%;",
        ["svh"] = "height: 100svh;",
        ["lvh"] = "height: 100lvh;",
        ["dvh"] = "height: 100dvh;",
    }; 

    public Dictionary<string, string> MaxHStaticUtilities { get; } = new()
    {
	    ["0"] = "max-height: 0px;",
	    ["px"] = "max-height: 1px;",
	    ["none"] = "max-height: none;",
	    ["full"] = "max-height: 100%;",
	    ["screen"] = "max-height: 100vh;",
	    ["min"] = "max-height: min-content;",
	    ["max"] = "max-height: max-content;",
	    ["fit"] = "max-height: fit-content;",
        ["svh"] = "max-height: 100svh;",
        ["lvh"] = "max-height: 100lvh;",
        ["dvh"] = "max-height: 100dvh;",
    }; 

    public Dictionary<string, string> MaxWStaticUtilities { get; } = new()
    {
	    ["0"] = "max-width: 0px;",
	    ["none"] = "max-width: none;",
	    ["xs"] = "max-width: 20rem;",
	    ["sm"] = "max-width: 24rem;",
	    ["md"] = "max-width: 28rem;",
	    ["lg"] = "max-width: 32rem;",
	    ["xl"] = "max-width: 36rem;",
	    ["2xl"] = "max-width: 42rem;",
	    ["3xl"] = "max-width: 48rem;",
	    ["4xl"] = "max-width: 56rem;",
	    ["5xl"] = "max-width: 64rem;",
	    ["6xl"] = "max-width: 72rem;",
	    ["7xl"] = "max-width: 80rem;",
	    ["full"] = "max-width: 100%;",
	    ["min"] = "max-width: min-content;",
	    ["max"] = "max-width: max-content;",
	    ["fit"] = "max-width: fit-content;",
	    ["prose"] = "max-width: 65ch;",
	    ["screen-sm"] = "max-width: #{$sm-breakpoint};",
	    ["screen-md"] = "max-width: #{$md-breakpoint};",
	    ["screen-lg"] = "max-width: #{$lg-breakpoint};",
	    ["screen-xl"] = "max-width: #{$xl-breakpoint};",
	    ["screen-xxl"] = "max-width: #{$xxl-breakpoint};"
    }; 

    public Dictionary<string, string> MinHStaticUtilities { get; } = new()
    {
        ["auto"] = "min-height: auto;",
	    ["0"] = "min-height: 0px;",
	    ["full"] = "min-height: 100%;",
	    ["screen"] = "min-height: 100vh;",
	    ["min"] = "min-height: min-content;",
	    ["max"] = "min-height: max-content;",
	    ["fit"] = "min-height: fit-content;",
        ["svh"] = "min-height: 100svh;",
        ["lvh"] = "min-height: 100lvh;",
        ["dvh"] = "min-height: 100dvh;",
    }; 

    public Dictionary<string, string> MinWStaticUtilities { get; } = new()
    {
        ["auto"] = "min-width: auto;",
	    ["0"] = "min-width: 0px;",
	    ["px"] = "min-width: 1px;",
	    ["screen"] = "min-width: 100vw;",
        ["full"] = "min-width: 100%;",
	    ["min"] = "min-width: min-content;",
	    ["max"] = "min-width: max-content;",
	    ["fit"] = "min-width: fit-content;"
    }; 

    public Dictionary<string, string> WStaticUtilities { get; } = new()
    {
	    ["0"] = "width: 0px;",
	    ["px"] = "width: 1px;",
	    ["auto"] = "width: auto;",
	    ["screen"] = "width: 100vw;",
	    ["min"] = "width: min-content;",
	    ["max"] = "width: max-content;",
	    ["fit"] = "width: fit-content;",
	    ["full"] = "width: 100%;",
        ["svw"] = "width: 100svw;",
        ["lvw"] = "width: 100lvw;",
        ["dvw"] = "width: 100dvw;",
    }; 
    
    public Dictionary<string, string> SizeStaticUtilities { get; } = new()
    {
        ["0"] = "width: 0px; height: 0px;",
        ["px"] = "width: 1px; height: 1px;",
        ["full"] = "width: 100%; height: 100%;",
        ["min"] = "width: min-content; height: min-content;",
        ["max"] = "width: max-content; height: max-content;",
        ["fit"] = "width: fit-content; height: fit-content;"
    }; 
    
    #endregion
    
    #region Spacing
    
    public Dictionary<string, string> MStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin: auto;",
	    ["0"] = "margin: 0px;",
	    ["px"] = "margin: 1px;",
    }; 

    public Dictionary<string, string> MbStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin-bottom: auto;",
	    ["0"] = "margin-bottom: 0px;",
	    ["px"] = "margin-bottom: 1px;",
    }; 

    public Dictionary<string, string> MeStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin-inline-end: auto;",
	    ["0"] = "margin-inline-end: 0px;",
	    ["px"] = "margin-inline-end: 1px;",
    }; 

    public Dictionary<string, string> MlStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin-left: auto;",
	    ["0"] = "margin-left: 0px;",
	    ["px"] = "margin-left: 1px;",
    }; 

    public Dictionary<string, string> MrStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin-right: auto;",
	    ["0"] = "margin-right: 0px;",
	    ["px"] = "margin-right: 1px;",
    }; 

    public Dictionary<string, string> MsStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin-inline-start: auto;",
	    ["0"] = "margin-inline-start: 0px;",
	    ["px"] = "margin-inline-start: 1px;",
    }; 

    public Dictionary<string, string> MtStaticUtilities { get; } = new()
    {
	    ["auto"] = "margin-top: auto;",
	    ["0"] = "margin-top: 0px;",
	    ["px"] = "margin-top: 1px;",
    }; 

    public Dictionary<string, string> MxStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            margin-left: 0px;
	            margin-right: 0px;
	            """,
	    ["px"] = """
	             margin-left: 1px;
	             margin-right: 1px;
	             """,
	    ["auto"] = """
	               margin-left: auto;
	               margin-right: auto;
	               """,
    }; 

    public Dictionary<string, string> MyStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            margin-top: 0px;
	            margin-bottom: 0px;
	            """,
	    ["px"] = """
	             margin-top: 1px;
	             margin-bottom: 1px;
	             """,
	    ["auto"] = """
	               margin-top: auto;
	               margin-bottom: auto;
	               """,
    }; 

    public Dictionary<string, string> PStaticUtilities { get; } = new()
    {
	    ["0"] = "padding: 0px;",
	    ["px"] = "padding: 1px;",
    }; 

    public Dictionary<string, string> PbStaticUtilities { get; } = new()
    {
	    ["0"] = "padding-bottom: 0px;",
	    ["px"] = "padding-bottom: 1px;",
    }; 

    public Dictionary<string, string> PeStaticUtilities { get; } = new()
    {
	    ["0"] = "padding-inline-end: 0px;",
	    ["px"] = "padding-inline-end: 1px;",
    }; 

    public Dictionary<string, string> PlStaticUtilities { get; } = new()
    {
	    ["0"] = "padding-left: 0px;",
	    ["px"] = "padding-left: 1px;",
    };

    public Dictionary<string, string> PrStaticUtilities { get; } = new()
    {
	    ["0"] = "padding-right: 0px;",
	    ["px"] = "padding-right: 1px;",
    }; 

    public Dictionary<string, string> PsStaticUtilities { get; } = new()
    {
	    ["0"] = "padding-inline-start: 0px;",
	    ["px"] = "padding-inline-start: 1px;",
    }; 

    public Dictionary<string, string> PtStaticUtilities { get; } = new()
    {
	    ["0"] = "padding-top: 0px;",
	    ["px"] = "padding-top: 1px;",
    }; 

    public Dictionary<string, string> PxStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            padding-left: 0px;
	            padding-right: 0px;
	            """,
	    ["px"] = """
	             padding-left: 1px;
	             padding-right: 1px;
	             """,
    }; 

    public Dictionary<string, string> PyStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            padding-top: 0px;
	            padding-bottom: 0px;
	            """,
	    ["px"] = """
	             padding-top: 1px;
	             padding-bottom: 1px;
	             """,
    }; 

    public Dictionary<string, string> SpaceXStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            & > * + * {
	                margin-left: 0px;
	            }
	            """,
	    ["px"] = """
	             & > * + * {
	                 margin-left: 1px;
	             }
	             """,
    }; 

    public Dictionary<string, string> SpaceYStaticUtilities { get; } = new()
    {
	    ["0"] = """
	            & > * + * {
	                margin-top: 0px;
	            }
	            """,
	    ["px"] = """
	             & > * + * {
	                 margin-top: 1px;
	             }
	             """,
    }; 
    
    #endregion
    
    #region SVG
    
    public Dictionary<string, string> FillStaticUtilities { get; } = new()
    {
	    ["none"] = "fill: none;",
    }; 

    public Dictionary<string, string> StrokeStaticUtilities { get; } = new()
    {
	    ["none"] = "stroke: none;",
	    ["0"] = "stroke-width: 0;",
	    ["1"] = "stroke-width: 1;",
	    ["2"] = "stroke-width: 2;",
    }; 

    #endregion
    
    #region Tables

    public Dictionary<string, string> BorderCollapseStaticUtilities { get; } = new()
    {
	    [""] = "border-collapse: collapse;"
    }; 

    public Dictionary<string, string> BorderSeparateStaticUtilities { get; } = new()
    {
	    [""] = "border-collapse: separate;"
    }; 

    public Dictionary<string, string> BorderSpacingStaticUtilities { get; } = new()
    {
	    ["0"] = "border-spacing: 0px; --sf-border-spacing-x: 0px; --sf-border-spacing-y: 0px;",
	    ["px"] = "border-spacing: 1px; --sf-border-spacing-x: 1px; --sf-border-spacing-y: 1px;",
    };

    public Dictionary<string, string> BorderSpacingXStaticUtilities { get; } = new()
    {
	    ["0"] = "border-spacing: 0px var(--sf-border-spacing-y); --sf-border-spacing-x: 0px;",
	    ["px"] = "border-spacing: 1px var(--sf-border-spacing-y); --sf-border-spacing-x: 1px;",
    };

    public Dictionary<string, string> BorderSpacingYStaticUtilities { get; } = new()
    {
	    ["0"] = "border-spacing: var(--sf-border-spacing-x) 0px; --sf-border-spacing-y: 0px;",
	    ["px"] = "border-spacing: var(--sf-border-spacing-x) 1px; --sf-border-spacing-y: 1px;",
    }; 

    public Dictionary<string, string> CaptionStaticUtilities { get; } = new()
    {
	    ["top"] = "caption-side: top;",
	    ["bottom"] = "caption-side: bottom;",
    }; 

    public Dictionary<string, string> TableAutoStaticUtilities { get; } = new()
    {
	    [""] = "table-layout: auto;",
    }; 

    public Dictionary<string, string> TabledFixedStaticUtilities { get; } = new()
    {
	    [""] = "table-layout: fixed;",
    }; 

    #endregion
    
    #region Transforms
    
    public Dictionary<string, string> AnimateStaticUtilities { get; } = new()
    {
	    ["none"] = "animation: none;",
	    ["spin"] = """
	               animation: spin 1s linear infinite;

	               @keyframes spin {
	                  from {
	                      transform: rotate(0deg);
	                   }
	                   to {
	                      transform: rotate(360deg);
	                   }
	               }
	               """,
	    ["ping"] = """
	               animation: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite;

	               @keyframes ping {
	                   75%, 100% {
	                       transform: scale(2);
	                       opacity: 0;
	                   }
	               }
	               """,
	    ["pulse"] = """
	                animation: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite;

	                @keyframes pulse {
	                    0%, 100% {
	                        opacity: 1;
	                    }
	                    50% {
	                        opacity: .5;
	                    }
	                }
	                """,
	    ["bounce"] = """
	                 animation: bounce 1s infinite;

	                 @keyframes bounce {
	                     0%, 100% {
	                         transform: translateY(-25%);
	                         animation-timing-function: cubic-bezier(0.8, 0, 1, 1);
	                     }
	                     50% {
	                         transform: translateY(0);
	                         animation-timing-function: cubic-bezier(0, 0, 0.2, 1);
	                     }
	                 }
	                 """,
    };
    
    public Dictionary<string, string> DelayStaticUtilities { get; } = new()
    {
	    ["0"] = "transition-delay: 0s;",
	    ["75"] = "transition-delay: 75ms;",
	    ["100"] = "transition-delay: 100ms;",
	    ["150"] = "transition-delay: 150ms;",
	    ["200"] = "transition-delay: 200ms;",
	    ["300"] = "transition-delay: 300ms;",
	    ["500"] = "transition-delay: 500ms;",
	    ["700"] = "transition-delay: 700ms;",
	    ["1000"] = "transition-delay: 1000ms;"
    }; 

    public Dictionary<string, string> DurationStaticUtilities { get; } = new()
    {
	    ["0"] = "transition-duration: 0s;",
	    ["75"] = "transition-duration: 75ms;",
	    ["100"] = "transition-duration: 100ms;",
	    ["150"] = "transition-duration: 150ms;",
	    ["200"] = "transition-duration: 200ms;",
	    ["300"] = "transition-duration: 300ms;",
	    ["500"] = "transition-duration: 500ms;",
	    ["700"] = "transition-duration: 700ms;",
	    ["1000"] = "transition-duration: 1000ms;"
    }; 

    public Dictionary<string, string> EaseStaticUtilities { get; } = new()
    {
	    ["linear"] = "transition-timing-function: linear;",
	    ["in"] = "transition-timing-function: cubic-bezier(0.4, 0, 1, 1);",
	    ["out"] = "transition-timing-function: cubic-bezier(0, 0, 0.2, 1);",
	    ["in-out"] = "transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);",
    }; 

    public Dictionary<string, string> TransitionStaticUtilities { get; } = new()
    {
	    [""] = """
	           transition-property: color, background-color, border-color, text-decoration-color, fill, stroke, opacity, box-shadow, transform, filter, backdrop-filter;
	           transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	           transition-duration: 150ms;
	           """,
	    ["none"] = """
	               transition-property: none;
	               transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	               transition-duration: 150ms;
	               """,
	    ["all"] = """
	              transition-property: all;
	              transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	              transition-duration: 150ms;
	              """,
	    ["colors"] = """
	                 transition-property: color, background-color, border-color, text-decoration-color, fill, stroke;
	                 transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	                 transition-duration: 150ms;
	                 """,
	    ["opacity"] = """
	                  transition-property: opacity;
	                  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	                  transition-duration: 150ms;
	                  """,
	    ["shadow"] = """
	                 transition-property: box-shadow;
	                 transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	                 transition-duration: 150ms;
	                 """,
	    ["transform"] = """
	                    transition-property: transform;
	                    transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
	                    transition-duration: 150ms;
	                    """
    }; 
    
    #endregion
    
    #region Transitions and Animations
    
    public Dictionary<string, string> OriginStaticUtilities { get; } = new()
    {
	    ["center"] = "transform-origin: center;",
	    ["top"] = "transform-origin: top;",
	    ["top-right"] = "transform-origin: top right;",
	    ["right"] = "transform-origin: right;",
	    ["bottom-right"] = "transform-origin: bottom right;",
	    ["bottom"] = "transform-origin: bottom;",
	    ["bottom-left"] = "transform-origin: bottom left;",
	    ["left"] = "transform-origin: left;",
	    ["top-left"] = "transform-origin: top left;"
    }; 

    public Dictionary<string, string> RotateStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-rotate: 0deg;",
	    ["1"] = "--sf-rotate: 1deg;",
	    ["2"] = "--sf-rotate: 2deg;",
	    ["3"] = "--sf-rotate: 3deg;",
	    ["6"] = "--sf-rotate: 6deg;",
	    ["12"] = "--sf-rotate: 12deg;",
	    ["45"] = "--sf-rotate: 45deg;",
	    ["90"] = "--sf-rotate: 90deg;",
	    ["180"] = "--sf-rotate: 180deg;"
    }; 

    public Dictionary<string, string> ScaleStaticUtilities { get; } = new()
    {
	    ["0"] = "transform: scale(0);",
	    ["50"] = "transform: scale(0.5);",
	    ["75"] = "transform: scale(0.75);",
	    ["90"] = "transform: scale(0.90);",
	    ["95"] = "transform: scale(0.95);",
	    ["100"] = "transform: scale(1.0);",
	    ["105"] = "transform: scale(1.05);",
	    ["110"] = "transform: scale(1.1);",
	    ["125"] = "transform: scale(1.25);",
	    ["150"] = "transform: scale(1.5);"
    }; 

    public Dictionary<string, string> ScaleXStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-scale-x: 0;",
	    ["50"] = "--sf-scale-x: 0.5;",
	    ["75"] = "--sf-scale-x: 0.75;",
	    ["90"] = "--sf-scale-x: 0.90;",
	    ["95"] = "--sf-scale-x: 0.95;",
	    ["100"] = "--sf-scale-x: 1.0;",
	    ["105"] = "--sf-scale-x: 1.05;",
	    ["110"] = "--sf-scale-x: 1.1;",
	    ["125"] = "--sf-scale-x: 1.25;",
	    ["150"] = "--sf-scale-x: 1.5;"
    }; 

    public Dictionary<string, string> ScaleYStaticUtilities { get; } = new()
    {
        ["0"] = "--sf-scale-y: 0;",
        ["50"] = "--sf-scale-y: 0.5;",
        ["75"] = "--sf-scale-y: 0.75;",
        ["90"] = "--sf-scale-y: 0.90;",
        ["95"] = "--sf-scale-y: 0.95;",
        ["100"] = "--sf-scale-y: 1.0;",
        ["105"] = "--sf-scale-y: 1.05;",
        ["110"] = "--sf-scale-y: 1.1;",
        ["125"] = "--sf-scale-y: 1.25;",
        ["150"] = "--sf-scale-y: 1.5;"
    };

    public Dictionary<string, string> SkewXStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-skew-x: 0deg;",
	    ["1"] = "--sf-skew-x: 1deg;",
	    ["2"] = "--sf-skew-x: 2deg;",
	    ["3"] = "--sf-skew-x: 3deg;",
	    ["6"] = "--sf-skew-x: 6deg;",
	    ["12"] = "--sf-skew-x: 12deg;",
	    ["45"] = "--sf-skew-x: 45deg;",
	    ["90"] = "--sf-skew-x: 90deg;",
	    ["180"] = "--sf-skew-x: 180deg;"
    };

    public Dictionary<string, string> SkewYStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-skew-y: 0deg;",
	    ["1"] = "--sf-skew-y: 1deg;",
	    ["2"] = "--sf-skew-y: 2deg;",
	    ["3"] = "--sf-skew-y: 3deg;",
	    ["6"] = "--sf-skew-y: 6deg;",
	    ["12"] = "--sf-skew-y: 12deg;",
	    ["45"] = "--sf-skew-y: 45deg;",
	    ["90"] = "--sf-skew-y: 90deg;",
	    ["180"] = "--sf-skew-y: 180deg;"
    };

    public Dictionary<string, string> TranslateStaticUtilities { get; } = new()
    {
        ["0"] = "transform: translate(0px,0px);",
        ["px"] = "transform: translate(1px,1px);",
        ["full"] = "transform: translate(100%,100%);",
        ["center"] = "transform: translate(-50%,-50%);"
    }; 

    public Dictionary<string, string> TranslateXStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-translate-x: 0px;",
	    ["px"] = "--sf-translate-x: 1px;",
	    ["full"] = "--sf-translate-x: 100%;",
        ["center"] = "--sf-translate-x: -50%;",
    }; 

    public Dictionary<string, string> TranslateYStaticUtilities { get; } = new()
    {
	    ["0"] = "--sf-translate-y: 0px;",
	    ["px"] = "--sf-translate-y: 1px;",
	    ["full"] = "--sf-translate-y: 100%;",
        ["center"] = "--sf-translate-y: -50%;"
    }; 
    
    #endregion
    
    #region Typography
    
    public Dictionary<string, string> AlignStaticUtilities { get; } = new()
    {
	    ["baseline"] = "vertical-align: baseline;",
	    ["top"] = "vertical-align: top;",
	    ["middle"] = "vertical-align: middle;",
	    ["bottom"] = "vertical-align: bottom;",
	    ["text-top"] = "vertical-align: text-top;",
	    ["text-bottom"] = "vertical-align: text-bottom;",
	    ["sub"] = "vertical-align: sub;",
	    ["super"] = "vertical-align: super;"
    }; 

    public Dictionary<string, string> AntialiasedStaticUtilities { get; } = new()
    {
	    [""] = """
	           -webkit-font-smoothing: antialiased;
	           -moz-osx-font-smoothing: grayscale;
	           """,
    }; 

    public Dictionary<string, string> BreakStaticUtilities { get; } = new()
    {
	    ["all"] = "word-break: break-all;",
	    ["keep"] = "word-break: keep-all;",
    }; 

    public Dictionary<string, string> BreakNormalStaticUtilities { get; } = new()
    {
	    [""] = """
	           overflow-wrap: normal;
	           word-break: normal;
	           """,
    }; 

    public Dictionary<string, string> BreakWordsStaticUtilities { get; } = new()
    {
	    [""] = "overflow-wrap: break-word;",
    };

    public Dictionary<string, string> BreakAnywhereStaticUtilities { get; } = new()
    {
        [""] = "overflow-wrap: anywhere;",
    };

    public Dictionary<string, string> CapitalizeStaticUtilities { get; } = new()
    {
	    [""] = "text-transform: capitalize;",
    }; 

    public Dictionary<string, string> ContentStaticUtilities { get; } = new()
    {
	    ["none"] = "content: none;",
    }; 

    public Dictionary<string, string> DecorationStaticUtilities { get; } = new()
    {
	    ["solid"] = "text-decoration-style: solid;",
	    ["double"] = "text-decoration-style: double;",
	    ["dotted"] = "text-decoration-style: dotted;",
	    ["dashed"] = "text-decoration-style: dashed;",
	    ["wavy"] = "text-decoration-style: wavy;",
	    ["auto"] = "text-decoration-thickness: auto;",
	    ["from-font"] = "text-decoration-thickness: from-font;",
	    ["0"] = "text-decoration-thickness: 0px;",
	    ["1"] = "text-decoration-thickness: 1px;",
	    ["2"] = $"text-decoration-thickness: {2.PxToRem()};",
	    ["4"] = $"text-decoration-thickness: {4.PxToRem()};",
	    ["8"] = $"text-decoration-thickness: {8.PxToRem()};"
    }; 

    public Dictionary<string, string> DiagonalFractionsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: diagonal-fractions;",
    }; 

    public Dictionary<string, string> FontStaticUtilities { get; } = new()
    {
	    ["sans"] = "font-family: ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\";",
	    ["serif"] = "font-family: ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif;",
	    ["mono"] = "font-family: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace;",
        
	    ["thin"] = "font-weight: 100;",
	    ["extralight"] = "font-weight: 200;",
	    ["light"] = "font-weight: 300;",
	    ["normal"] = "font-weight: 400;",
	    ["medium"] = "font-weight: 500;",
	    ["semibold"] = "font-weight: 600;",
	    ["bold"] = "font-weight: 700;",
	    ["extrabold"] = "font-weight: 800;",
	    ["black"] = "font-weight: 900;"
    }; 

    public Dictionary<string, string> HyphensStaticUtilities { get; } = new()
    {
	    ["none"] = "hyphens: none;",
	    ["manual"] = "hyphens: manual;",
	    ["auto"] = "hyphens: auto;",
    }; 

    public Dictionary<string, string> IndentStaticUtilities { get; } = new()
    {
	    ["0"] = "text-indent: 0px;",
	    ["px"] = "text-indent: 1px;",
    }; 

    public Dictionary<string, string> ItalicStaticUtilities { get; } = new()
    {
	    [""] = "font-style: italic;",
    }; 

    public Dictionary<string, string> LineClampStaticUtilities { get; } = new()
    {
	    ["none"] = """
	               overflow: visible;
	               display: block;
	               -webkit-box-orient: horizontal;
	               -webkit-line-clamp: none;
	               """,
	    ["1"] = """
	            -webkit-line-clamp: 1;
	            overflow: hidden;
	            display: -webkit-box;
	            -webkit-box-orient: vertical;
	            """,
	    ["2"] = """
	            -webkit-line-clamp: 2;
	            overflow: hidden;
	            display: -webkit-box;
	            -webkit-box-orient: vertical;
	            """,
	    ["3"] = """
	            -webkit-line-clamp: 3;
	            overflow: hidden;
	            display: -webkit-box;
	            -webkit-box-orient: vertical;
	            """,
	    ["4"] = """
	            -webkit-line-clamp: 4;
	            overflow: hidden;
	            display: -webkit-box;
	            -webkit-box-orient: vertical;
	            """,
	    ["5"] = """
	            -webkit-line-clamp: 5;
	            overflow: hidden;
	            display: -webkit-box;
	            -webkit-box-orient: vertical;
	            """,
    }; 

    public Dictionary<string, string> LineThroughStaticUtilities { get; } = new()
    {
	    [""] = "text-decoration-line: line-through;",
    }; 

    public Dictionary<string, string> LiningNumsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: lining-nums;",
    }; 

    public Dictionary<string, string> ListStaticUtilities { get; } = new()
    {
	    ["inside"] = "list-style-position: inside;",
	    ["outside"] = "list-style-position: outside;",
        
	    ["none"] = "list-style-type: none;",
	    ["disc"] = "list-style-type: disc;",
	    ["decimal"] = "list-style-type: decimal;",
        
    }; 

    public Dictionary<string, string> ListImageStaticUtilities { get; } = new()
    {
	    ["none"] = "list-style-image: none;",
    }; 

    public Dictionary<string, string> LowercaseStaticUtilities { get; } = new()
    {
	    [""] = "text-transform: lowercase;",
    }; 

    public Dictionary<string, string> NotItalicStaticUtilities { get; } = new()
    {
	    [""] = "font-style: normal;",
    };

    public Dictionary<string, string> NormalCaseStaticUtilities { get; } = new()
    {
	    [""] = "text-transform: none;",
    }; 

    public Dictionary<string, string> NormalNumsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: normal;",
    }; 

    public Dictionary<string, string> NoUnderlineStaticUtilities { get; } = new()
    {
	    [""] = "text-decoration-line: none;",
    }; 

    public Dictionary<string, string> OldStyleNumsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: oldstyle-nums;",
    }; 

    public Dictionary<string, string> OrdinalStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: ordinal;",
    }; 

    public Dictionary<string, string> OverlineStaticUtilities { get; } = new()
    {
	    [""] = "text-decoration-line: overline;",
    }; 

    public Dictionary<string, string> ProportionalNumsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: proportional-nums;",
    }; 

    public Dictionary<string, string> SlashedZeroStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: slashed-zero;",
    }; 

    public Dictionary<string, string> StackedFractionsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: stacked-fractions;",
    }; 

    public Dictionary<string, string> SubpixelAntialiasedStaticUtilities { get; } = new()
    {
	    [""] = """
	           -webkit-font-smoothing: auto;
	           -moz-osx-font-smoothing: auto;
	           """,
    }; 

    public Dictionary<string, string> TabularNumsStaticUtilities { get; } = new()
    {
	    [""] = "font-variant-numeric: tabular-nums;",
    }; 

    public Dictionary<string, string> TextAlignStaticUtilities { get; } = new()
    {
	    ["left"] = "text-align: left;",
	    ["center"] = "text-align: center;",
	    ["right"] = "text-align: right;",
	    ["justify"] = "text-align: justify;",
	    ["start"] = "text-align: start;",
	    ["end"] = "text-align: end;"
    }; 

    public Dictionary<string, string> TextWrapStaticUtilities { get; } = new()
    {
        ["wrap"] = "text-wrap: wrap;",
        ["nowrap"] = "text-wrap: nowrap;",
        ["balance"] = "text-wrap: balance;",
        ["pretty"] = "text-wrap: pretty;"
    }; 

    public Dictionary<string, string> TextClipStaticUtilities { get; } = new()
    {
	    [""] = "text-overflow: clip;",
    }; 

    public Dictionary<string, string> TextEllipsisStaticUtilities { get; } = new()
    {
	    [""] = "text-overflow: ellipsis;",
    }; 

    public Dictionary<string, string> TrackingStaticUtilities { get; } = new()
    {
	    ["tighter"] = "letter-spacing: -0.05em;",
	    ["tight"] = "letter-spacing: -0.025em;",
	    ["normal"] = "letter-spacing: 0em;",
	    ["wide"] = "letter-spacing: 0.025em;",
	    ["wider"] = "letter-spacing: 0.05em;",
	    ["widest"] = "letter-spacing: 0.1em;"
    }; 

    public Dictionary<string, string> TruncateStaticUtilities { get; } = new()
    {
	    [""] = """
	           overflow: hidden;
	           text-overflow: ellipsis;
	           white-space: nowrap;
	           """,
    }; 

    public Dictionary<string, string> UnderlineStaticUtilities { get; } = new()
    {
	    [""] = "text-decoration-line: underline;",
    }; 

    public Dictionary<string, string> UnderlineOffsetStaticUtilities { get; } = new()
    {
	    ["auto"] = "text-underline-offset: auto;",
	    ["0"] = "text-underline-offset: 0px;",
	    ["1"] = "text-underline-offset: 1px;",
	    ["2"] = $"text-underline-offset: {2.PxToRem()};",
	    ["4"] = $"text-underline-offset: {4.PxToRem()};",
	    ["8"] = $"text-underline-offset: {8.PxToRem()};"
    }; 

    public Dictionary<string, string> UppercaseStaticUtilities { get; } = new()
    {
	    [""] = "text-transform: uppercase;",
    };

    public Dictionary<string, string> WhitespaceStaticUtilities { get; } = new()
    {
	    ["normal"] = "white-space: normal;",
	    ["nowrap"] = "white-space: nowrap;",
	    ["no-wrap"] = "white-space: nowrap;",
	    ["pre"] = "white-space: pre;",
	    ["pre-line"] = "white-space: pre-line;",
	    ["pre-wrap"] = "white-space: pre-wrap;",
	    ["break-spaces"] = "white-space: break-spaces;"
    };

    #endregion
    
    #endregion
    
    #endregion
    
    #region Constants
    
    public IEnumerable<string> ClassMatchEndingExclusions { get; } = new[]
    {
	    "/",
	    ".css",
	    ".htm",
	    ".html",
	    ".js",
	    ".json",
    };
    
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

    public List<string> ValidCssPropertyNames { get; } = [];

    public IEnumerable<string> ValidSafariCssPropertyNames { get; } = 
    [
        "-apple-pay-button-style",
        "-apple-pay-button-type",
        "-epub-caption-side",
        "-epub-hyphens",
        "-epub-text-combine",
        "-epub-text-emphasis",
        "-epub-text-emphasis-color",
        "-epub-text-emphasis-style",
        "-epub-text-orientation",
        "-epub-text-transform",
        "-epub-word-break",
        "-epub-writing-mode",
        "-webkit-align-content",
        "-webkit-align-items",
        "-webkit-align-self",
        "-webkit-alt",
        "-webkit-animation",
        "-webkit-animation-delay",
        "-webkit-animation-direction",
        "-webkit-animation-duration",
        "-webkit-animation-fill-mode",
        "-webkit-animation-iteration-count",
        "-webkit-animation-name",
        "-webkit-animation-play-state",
        "-webkit-animation-timing-function",
        "-webkit-appearance",
        "-webkit-backdrop-filter",
        "-webkit-backface-visibility",
        "-webkit-background-clip",
        "-webkit-background-origin",
        "-webkit-background-size",
        "-webkit-border-after",
        "-webkit-border-after-color",
        "-webkit-border-after-style",
        "-webkit-border-after-width",
        "-webkit-border-before",
        "-webkit-border-before-color",
        "-webkit-border-before-style",
        "-webkit-border-before-width",
        "-webkit-border-bottom-left-radius",
        "-webkit-border-bottom-right-radius",
        "-webkit-border-end",
        "-webkit-border-end-color",
        "-webkit-border-end-style",
        "-webkit-border-end-width",
        "-webkit-border-horizontal-spacing",
        "-webkit-border-image",
        "-webkit-border-radius",
        "-webkit-border-start",
        "-webkit-border-start-color",
        "-webkit-border-start-style",
        "-webkit-border-start-width",
        "-webkit-border-top-left-radius",
        "-webkit-border-top-right-radius",
        "-webkit-border-vertical-spacing",
        "-webkit-box-align",
        "-webkit-box-decoration-break",
        "-webkit-box-direction",
        "-webkit-box-flex",
        "-webkit-box-flex-group",
        "-webkit-box-lines",
        "-webkit-box-ordinal-group",
        "-webkit-box-orient",
        "-webkit-box-pack",
        "-webkit-box-reflect",
        "-webkit-box-shadow",
        "-webkit-box-sizing",
        "-webkit-clip-path",
        "-webkit-column-axis",
        "-webkit-column-break-after",
        "-webkit-column-break-before",
        "-webkit-column-break-inside",
        "-webkit-column-count",
        "-webkit-column-fill",
        "-webkit-column-gap",
        "-webkit-column-progression",
        "-webkit-column-rule",
        "-webkit-column-rule-color",
        "-webkit-column-rule-style",
        "-webkit-column-rule-width",
        "-webkit-column-span",
        "-webkit-column-width",
        "-webkit-columns",
        "-webkit-cursor-visibility",
        "-webkit-filter",
        "-webkit-flex",
        "-webkit-flex-basis",
        "-webkit-flex-direction",
        "-webkit-flex-flow",
        "-webkit-flex-grow",
        "-webkit-flex-shrink",
        "-webkit-flex-wrap",
        "-webkit-font-kerning",
        "-webkit-font-smoothing",
        "-webkit-hyphenate-character",
        "-webkit-hyphenate-limit-after",
        "-webkit-hyphenate-limit-before",
        "-webkit-hyphenate-limit-lines",
        "-webkit-hyphens",
        "-webkit-initial-letter",
        "-webkit-justify-content",
        "-webkit-justify-items",
        "-webkit-line-align",
        "-webkit-line-box-contain",
        "-webkit-line-break",
        "-webkit-line-clamp",
        "-webkit-line-grid",
        "-webkit-line-snap",
        "-webkit-locale",
        "-webkit-logical-height",
        "-webkit-logical-width",
        "-webkit-margin-after",
        "-webkit-margin-before",
        "-webkit-margin-end",
        "-webkit-margin-start",
        "-webkit-mask",
        "-webkit-mask-box-image",
        "-webkit-mask-box-image-outset",
        "-webkit-mask-box-image-repeat",
        "-webkit-mask-box-image-slice",
        "-webkit-mask-box-image-source",
        "-webkit-mask-box-image-width",
        "-webkit-mask-clip",
        "-webkit-mask-composite",
        "-webkit-mask-image",
        "-webkit-mask-origin",
        "-webkit-mask-position",
        "-webkit-mask-position-x",
        "-webkit-mask-position-y",
        "-webkit-mask-repeat",
        "-webkit-mask-size",
        "-webkit-mask-source-type",
        "-webkit-max-logical-height",
        "-webkit-max-logical-width",
        "-webkit-min-logical-height",
        "-webkit-min-logical-width",
        "-webkit-nbsp-mode",
        "-webkit-opacity",
        "-webkit-order",
        "-webkit-padding-after",
        "-webkit-padding-before",
        "-webkit-padding-end",
        "-webkit-padding-start",
        "-webkit-perspective",
        "-webkit-perspective-origin",
        "-webkit-perspective-origin-x",
        "-webkit-perspective-origin-y",
        "-webkit-print-color-adjust",
        "-webkit-rtl-ordering",
        "-webkit-ruby-position",
        "-webkit-shape-image-threshold",
        "-webkit-shape-margin",
        "-webkit-shape-outside",
        "-webkit-text-combine",
        "-webkit-text-decoration",
        "-webkit-text-decoration-color",
        "-webkit-text-decoration-line",
        "-webkit-text-decoration-skip",
        "-webkit-text-decoration-style",
        "-webkit-text-decorations-in-effect",
        "-webkit-text-emphasis",
        "-webkit-text-emphasis-color",
        "-webkit-text-emphasis-position",
        "-webkit-text-emphasis-style",
        "-webkit-text-fill-color",
        "-webkit-text-orientation",
        "-webkit-text-security",
        "-webkit-text-stroke",
        "-webkit-text-stroke-color",
        "-webkit-text-stroke-width",
        "-webkit-text-underline-position",
        "-webkit-text-zoom",
        "-webkit-transform",
        "-webkit-transform-origin",
        "-webkit-transform-origin-x",
        "-webkit-transform-origin-y",
        "-webkit-transform-origin-z",
        "-webkit-transform-style",
        "-webkit-transition",
        "-webkit-transition-delay",
        "-webkit-transition-duration",
        "-webkit-transition-property",
        "-webkit-transition-timing-function",
        "-webkit-user-drag",
        "-webkit-user-modify",
        "-webkit-user-select",
        "-webkit-writing-mode",
        "accent-color",
        "align-content",
        "align-items",
        "align-self",
        "alignment-baseline",
        "all",
        "alt",
        "animation",
        "animation-composition",
        "animation-delay",
        "animation-direction",
        "animation-duration",
        "animation-fill-mode",
        "animation-iteration-count",
        "animation-name",
        "animation-play-state",
        "animation-timing-function",
        "appearance",
        "aspect-ratio",
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
        "baseline-shift",
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
        "border-end-end-radius",
        "border-end-start-radius",
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
        "border-start-end-radius",
        "border-start-start-radius",
        "border-style",
        "border-top",
        "border-top-color",
        "border-top-left-radius",
        "border-top-right-radius",
        "border-top-style",
        "border-top-width",
        "border-width",
        "bottom",
        "box-shadow",
        "box-sizing",
        "break-after",
        "break-before",
        "break-inside",
        "buffered-rendering",
        "caption-side",
        "caret-color",
        "clear",
        "clip",
        "clip-path",
        "clip-rule",
        "color",
        "color-adjust",
        "color-interpolation",
        "color-interpolation-filters",
        "color-scheme",
        "column-count",
        "column-fill",
        "column-gap",
        "column-rule",
        "column-rule-color",
        "column-rule-style",
        "column-rule-width",
        "column-span",
        "column-width",
        "columns",
        "contain",
        "contain-intrinsic-block-size",
        "contain-intrinsic-height",
        "contain-intrinsic-inline-size",
        "contain-intrinsic-size",
        "contain-intrinsic-width",
        "container",
        "container-name",
        "container-type",
        "content",
        "counter-increment",
        "counter-reset",
        "counter-set",
        "cursor",
        "cx",
        "cy",
        "direction",
        "display",
        "dominant-baseline",
        "empty-cells",
        "fill",
        "fill-opacity",
        "fill-rule",
        "filter",
        "flex",
        "flex-basis",
        "flex-direction",
        "flex-flow",
        "flex-grow",
        "flex-shrink",
        "flex-wrap",
        "float",
        "flood-color",
        "flood-opacity",
        "font",
        "font-family",
        "font-feature-settings",
        "font-kerning",
        "font-optical-sizing",
        "font-palette",
        "font-size",
        "font-size-adjust",
        "font-stretch",
        "font-style",
        "font-synthesis",
        "font-synthesis-small-caps",
        "font-synthesis-style",
        "font-synthesis-weight",
        "font-variant",
        "font-variant-alternates",
        "font-variant-caps",
        "font-variant-east-asian",
        "font-variant-ligatures",
        "font-variant-numeric",
        "font-variant-position",
        "font-variation-settings",
        "font-weight",
        "gap",
        "glyph-orientation-horizontal",
        "glyph-orientation-vertical",
        "grid",
        "grid-area",
        "grid-auto-columns",
        "grid-auto-flow",
        "grid-auto-rows",
        "grid-column",
        "grid-column-end",
        "grid-column-gap",
        "grid-column-start",
        "grid-gap",
        "grid-row",
        "grid-row-end",
        "grid-row-gap",
        "grid-row-start",
        "grid-template",
        "grid-template-areas",
        "grid-template-columns",
        "grid-template-rows",
        "hanging-punctuation",
        "height",
        "hyphenate-character",
        "hyphens",
        "image-orientation",
        "image-rendering",
        "inline-size",
        "inset",
        "inset-block",
        "inset-block-end",
        "inset-block-start",
        "inset-inline",
        "inset-inline-end",
        "inset-inline-start",
        "isolation",
        "justify-content",
        "justify-items",
        "justify-self",
        "kerning",
        "left",
        "letter-spacing",
        "lighting-color",
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
        "margin-trim",
        "marker",
        "marker-end",
        "marker-mid",
        "marker-start",
        "mask",
        "mask-border",
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
        "math-style",
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
        "offset",
        "offset-anchor",
        "offset-distance",
        "offset-path",
        "offset-position",
        "offset-rotate",
        "opacity",
        "order",
        "orphans",
        "outline",
        "outline-color",
        "outline-offset",
        "outline-style",
        "outline-width",
        "overflow",
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
        "page",
        "page-break-after",
        "page-break-before",
        "page-break-inside",
        "paint-order",
        "perspective",
        "perspective-origin",
        "perspective-origin-x",
        "perspective-origin-y",
        "place-content",
        "place-items",
        "place-self",
        "pointer-events",
        "position",
        "print-color-adjust",
        "quotes",
        "r",
        "resize",
        "right",
        "rotate",
        "row-gap",
        "rx",
        "ry",
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
        "shape-rendering",
        "size",
        "speak-as",
        "stop-color",
        "stop-opacity",
        "stroke",
        "stroke-color",
        "stroke-dasharray",
        "stroke-dashoffset",
        "stroke-linecap",
        "stroke-linejoin",
        "stroke-miterlimit",
        "stroke-opacity",
        "stroke-width",
        "supported-color-schemes",
        "tab-size",
        "table-layout",
        "text-align",
        "text-align-last",
        "text-anchor",
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
        "transform-origin-x",
        "transform-origin-y",
        "transform-origin-z",
        "transform-style",
        "transition",
        "transition-delay",
        "transition-duration",
        "transition-property",
        "transition-timing-function",
        "translate",
        "unicode-bidi",
        "vector-effect",
        "vertical-align",
        "visibility",
        "widows",
        "width",
        "will-change",
        "word-break",
        "word-spacing",
        "word-wrap",
        "writing-mode",
        "x",
        "y",
        "z-index",
        "zoom"
    ];
    
	public IEnumerable<string> ValidChromeCssPropertyNames { get; } =
	[
        "accent-color",
        "additive-symbols",
        "align-content",
        "align-items",
        "align-self",
        "alignment-baseline",
        "all",
        "animation",
        "animation-composition",
        "animation-delay",
        "animation-direction",
        "animation-duration",
        "animation-fill-mode",
        "animation-iteration-count",
        "animation-name",
        "animation-play-state",
        "animation-range",
        "animation-range-end",
        "animation-range-start",
        "animation-timeline",
        "animation-timing-function",
        "app-region",
        "appearance",
        "ascent-override",
        "aspect-ratio",
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
        "base-palette",
        "baseline-shift",
        "baseline-source",
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
        "border-end-end-radius",
        "border-end-start-radius",
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
        "border-start-end-radius",
        "border-start-start-radius",
        "border-style",
        "border-top",
        "border-top-color",
        "border-top-left-radius",
        "border-top-right-radius",
        "border-top-style",
        "border-top-width",
        "border-width",
        "bottom",
        "box-shadow",
        "box-sizing",
        "break-after",
        "break-before",
        "break-inside",
        "buffered-rendering",
        "caption-side",
        "caret-color",
        "clear",
        "clip",
        "clip-path",
        "clip-rule",
        "color",
        "color-interpolation",
        "color-interpolation-filters",
        "color-rendering",
        "color-scheme",
        "column-count",
        "column-fill",
        "column-gap",
        "column-rule",
        "column-rule-color",
        "column-rule-style",
        "column-rule-width",
        "column-span",
        "column-width",
        "columns",
        "contain",
        "contain-intrinsic-block-size",
        "contain-intrinsic-height",
        "contain-intrinsic-inline-size",
        "contain-intrinsic-size",
        "contain-intrinsic-width",
        "container",
        "container-name",
        "container-type",
        "content",
        "content-visibility",
        "counter-increment",
        "counter-reset",
        "counter-set",
        "cursor",
        "cx",
        "cy",
        "d",
        "descent-override",
        "direction",
        "display",
        "dominant-baseline",
        "empty-cells",
        "fallback",
        "fill",
        "fill-opacity",
        "fill-rule",
        "filter",
        "flex",
        "flex-basis",
        "flex-direction",
        "flex-flow",
        "flex-grow",
        "flex-shrink",
        "flex-wrap",
        "float",
        "flood-color",
        "flood-opacity",
        "font",
        "font-display",
        "font-family",
        "font-feature-settings",
        "font-kerning",
        "font-optical-sizing",
        "font-palette",
        "font-size",
        "font-stretch",
        "font-style",
        "font-synthesis",
        "font-synthesis-small-caps",
        "font-synthesis-style",
        "font-synthesis-weight",
        "font-variant",
        "font-variant-alternates",
        "font-variant-caps",
        "font-variant-east-asian",
        "font-variant-ligatures",
        "font-variant-numeric",
        "font-variant-position",
        "font-variation-settings",
        "font-weight",
        "forced-color-adjust",
        "gap",
        "grid",
        "grid-area",
        "grid-auto-columns",
        "grid-auto-flow",
        "grid-auto-rows",
        "grid-column",
        "grid-column-end",
        "grid-column-gap",
        "grid-column-start",
        "grid-gap",
        "grid-row",
        "grid-row-end",
        "grid-row-gap",
        "grid-row-start",
        "grid-template",
        "grid-template-areas",
        "grid-template-columns",
        "grid-template-rows",
        "height",
        "hyphenate-character",
        "hyphenate-limit-chars",
        "hyphens",
        "image-orientation",
        "image-rendering",
        "inherits",
        "initial-letter",
        "initial-value",
        "inline-size",
        "inset",
        "inset-block",
        "inset-block-end",
        "inset-block-start",
        "inset-inline",
        "inset-inline-end",
        "inset-inline-start",
        "isolation",
        "justify-content",
        "justify-items",
        "justify-self",
        "left",
        "letter-spacing",
        "lighting-color",
        "line-break",
        "line-gap-override",
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
        "marker",
        "marker-end",
        "marker-mid",
        "marker-start",
        "mask",
        "mask-clip",
        "mask-composite",
        "mask-image",
        "mask-mode",
        "mask-origin",
        "mask-position",
        "mask-repeat",
        "mask-size",
        "mask-type",
        "math-depth",
        "math-shift",
        "math-style",
        "max-block-size",
        "max-height",
        "max-inline-size",
        "max-width",
        "min-block-size",
        "min-height",
        "min-inline-size",
        "min-width",
        "mix-blend-mode",
        "negative",
        "object-fit",
        "object-position",
        "object-view-box",
        "offset",
        "offset-anchor",
        "offset-distance",
        "offset-path",
        "offset-position",
        "offset-rotate",
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
        "overflow-clip-margin",
        "overflow-wrap",
        "overflow-x",
        "overflow-y",
        "overlay",
        "override-colors",
        "overscroll-behavior",
        "overscroll-behavior-block",
        "overscroll-behavior-inline",
        "overscroll-behavior-x",
        "overscroll-behavior-y",
        "pad",
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
        "page",
        "page-break-after",
        "page-break-before",
        "page-break-inside",
        "page-orientation",
        "paint-order",
        "perspective",
        "perspective-origin",
        "place-content",
        "place-items",
        "place-self",
        "pointer-events",
        "position",
        "prefix",
        "quotes",
        "r",
        "range",
        "resize",
        "right",
        "rotate",
        "row-gap",
        "ruby-position",
        "rx",
        "ry",
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
        "scroll-snap-stop",
        "scroll-snap-type",
        "scroll-timeline",
        "scroll-timeline-axis",
        "scroll-timeline-name",
        "scrollbar-color",
        "scrollbar-gutter",
        "scrollbar-width",
        "shape-image-threshold",
        "shape-margin",
        "shape-outside",
        "shape-rendering",
        "size",
        "size-adjust",
        "speak",
        "speak-as",
        "src",
        "stop-color",
        "stop-opacity",
        "stroke",
        "stroke-dasharray",
        "stroke-dashoffset",
        "stroke-linecap",
        "stroke-linejoin",
        "stroke-miterlimit",
        "stroke-opacity",
        "stroke-width",
        "suffix",
        "symbols",
        "syntax",
        "system",
        "tab-size",
        "table-layout",
        "text-align",
        "text-align-last",
        "text-anchor",
        "text-combine-upright",
        "text-decoration",
        "text-decoration-color",
        "text-decoration-line",
        "text-decoration-skip-ink",
        "text-decoration-style",
        "text-decoration-thickness",
        "text-emphasis",
        "text-emphasis-color",
        "text-emphasis-position",
        "text-emphasis-style",
        "text-indent",
        "text-orientation",
        "text-overflow",
        "text-rendering",
        "text-shadow",
        "text-size-adjust",
        "text-transform",
        "text-underline-offset",
        "text-underline-position",
        "text-wrap",
        "timeline-scope",
        "top",
        "touch-action",
        "transform",
        "transform-box",
        "transform-origin",
        "transform-style",
        "transition",
        "transition-behavior",
        "transition-delay",
        "transition-duration",
        "transition-property",
        "transition-timing-function",
        "translate",
        "unicode-bidi",
        "unicode-range",
        "user-select",
        "vector-effect",
        "vertical-align",
        "view-timeline",
        "view-timeline-axis",
        "view-timeline-inset",
        "view-timeline-name",
        "view-transition-name",
        "visibility",
        "webkit-align-content",
        "webkit-align-items",
        "webkit-align-self",
        "webkit-animation",
        "webkit-animation-delay",
        "webkit-animation-direction",
        "webkit-animation-duration",
        "webkit-animation-fill-mode",
        "webkit-animation-iteration-count",
        "webkit-animation-name",
        "webkit-animation-play-state",
        "webkit-animation-timing-function",
        "webkit-app-region",
        "webkit-appearance",
        "webkit-backface-visibility",
        "webkit-background-clip",
        "webkit-background-origin",
        "webkit-background-size",
        "webkit-border-after",
        "webkit-border-after-color",
        "webkit-border-after-style",
        "webkit-border-after-width",
        "webkit-border-before",
        "webkit-border-before-color",
        "webkit-border-before-style",
        "webkit-border-before-width",
        "webkit-border-bottom-left-radius",
        "webkit-border-bottom-right-radius",
        "webkit-border-end",
        "webkit-border-end-color",
        "webkit-border-end-style",
        "webkit-border-end-width",
        "webkit-border-horizontal-spacing",
        "webkit-border-image",
        "webkit-border-radius",
        "webkit-border-start",
        "webkit-border-start-color",
        "webkit-border-start-style",
        "webkit-border-start-width",
        "webkit-border-top-left-radius",
        "webkit-border-top-right-radius",
        "webkit-border-vertical-spacing",
        "webkit-box-align",
        "webkit-box-decoration-break",
        "webkit-box-direction",
        "webkit-box-flex",
        "webkit-box-ordinal-group",
        "webkit-box-orient",
        "webkit-box-pack",
        "webkit-box-reflect",
        "webkit-box-shadow",
        "webkit-box-sizing",
        "webkit-clip-path",
        "webkit-column-break-after",
        "webkit-column-break-before",
        "webkit-column-break-inside",
        "webkit-column-count",
        "webkit-column-gap",
        "webkit-column-rule",
        "webkit-column-rule-color",
        "webkit-column-rule-style",
        "webkit-column-rule-width",
        "webkit-column-span",
        "webkit-column-width",
        "webkit-columns",
        "webkit-filter",
        "webkit-flex",
        "webkit-flex-basis",
        "webkit-flex-direction",
        "webkit-flex-flow",
        "webkit-flex-grow",
        "webkit-flex-shrink",
        "webkit-flex-wrap",
        "webkit-font-feature-settings",
        "webkit-font-smoothing",
        "webkit-hyphenate-character",
        "webkit-justify-content",
        "webkit-line-break",
        "webkit-line-clamp",
        "webkit-locale",
        "webkit-logical-height",
        "webkit-logical-width",
        "webkit-margin-after",
        "webkit-margin-before",
        "webkit-margin-end",
        "webkit-margin-start",
        "webkit-mask",
        "webkit-mask-box-image",
        "webkit-mask-box-image-outset",
        "webkit-mask-box-image-repeat",
        "webkit-mask-box-image-slice",
        "webkit-mask-box-image-source",
        "webkit-mask-box-image-width",
        "webkit-mask-clip",
        "webkit-mask-composite",
        "webkit-mask-image",
        "webkit-mask-origin",
        "webkit-mask-position",
        "webkit-mask-position-x",
        "webkit-mask-position-y",
        "webkit-mask-repeat",
        "webkit-mask-size",
        "webkit-max-logical-height",
        "webkit-max-logical-width",
        "webkit-min-logical-height",
        "webkit-min-logical-width",
        "webkit-opacity",
        "webkit-order",
        "webkit-padding-after",
        "webkit-padding-before",
        "webkit-padding-end",
        "webkit-padding-start",
        "webkit-perspective",
        "webkit-perspective-origin",
        "webkit-perspective-origin-x",
        "webkit-perspective-origin-y",
        "webkit-print-color-adjust",
        "webkit-rtl-ordering",
        "webkit-ruby-position",
        "webkit-shape-image-threshold",
        "webkit-shape-margin",
        "webkit-shape-outside",
        "webkit-tap-highlight-color",
        "webkit-text-combine",
        "webkit-text-decorations-in-effect",
        "webkit-text-emphasis",
        "webkit-text-emphasis-color",
        "webkit-text-emphasis-position",
        "webkit-text-emphasis-style",
        "webkit-text-fill-color",
        "webkit-text-orientation",
        "webkit-text-security",
        "webkit-text-size-adjust",
        "webkit-text-stroke",
        "webkit-text-stroke-color",
        "webkit-text-stroke-width",
        "webkit-transform",
        "webkit-transform-origin",
        "webkit-transform-origin-x",
        "webkit-transform-origin-y",
        "webkit-transform-origin-z",
        "webkit-transform-style",
        "webkit-transition",
        "webkit-transition-delay",
        "webkit-transition-duration",
        "webkit-transition-property",
        "webkit-transition-timing-function",
        "webkit-user-drag",
        "webkit-user-modify",
        "webkit-user-select",
        "webkit-writing-mode",
        "white-space",
        "white-space-collapse",
        "widows",
        "width",
        "will-change",
        "word-break",
        "word-spacing",
        "word-wrap",
        "writing-mode",
        "x",
        "y",
        "z-index",
        "zoom"
    ];
	
	public Dictionary<string,string> PseudoclassPrefixes { get; } = new ()
	{
		{ "hover", ":hover"},
		{ "focus", ":focus" },
		{ "focus-within", ":focus-within" },
		{ "focus-visible", ":focus-visible" },
		{ "active", ":active" },
		{ "visited", ":visited" },
		{ "target", ":target" },
		{ "first", ":first-child" },
		{ "last", ":last-child" },
		{ "only", ":only-child" },
		{ "odd", ":nth-child(odd)" },
		{ "even", ":nth-child(even)" },
		{ "first-of-type", ":first-of-type" },
		{ "last-of-type", ":last-of-type" },
		{ "only-of-type", ":only-of-type" },
		{ "empty", ":empty" },
		{ "disabled", ":disabled" },
		{ "enabled", ":enabled" },
		{ "checked", ":checked" },
		{ "indeterminate", ":indeterminate" },
		{ "default", ":default" },
		{ "required", ":required" },
		{ "valid", ":valid" },
		{ "invalid", ":invalid" },
		{ "in-range", ":in-range" },
		{ "out-of-range", ":out-of-range" },
		{ "placeholder-shown", ":placeholder-shown" },
		{ "autofill", ":autofill" },
		{ "read-only", ":read-only" },
		{ "before", "::before" },
		{ "after", "::after" },
		{ "first-letter", "::first-letter" },
		{ "first-line", "::first-line" },
		{ "marker", "::marker" },
		{ "selection", "::selection" },
		{ "file", "::file-selector-button" },
		{ "backdrop", "::backdrop" },
		{ "placeholder", "::placeholder" }
	};
	
	#endregion
	
    #region Regex Properties
    
    public Regex ArbitraryCssRegex { get; }
    public Regex CoreClassRegex { get; }
    public Regex SfumatoScssRegex { get; }
    public Regex SfumatoScssApplyRegex { get; }
    public Regex PeerVariantRegex { get; } = new (@"(peer\-([a-z\-]{1,25}([/]([a-z\-]{0,25})){0,1}?:))", RegexOptions.Compiled);

    #endregion
    
    #region Run Mode Properties

    public bool Minify { get; set; }
    public bool WatchMode { get; set; }
    public bool VersionMode { get; set; }
    public bool InitMode { get; set; }
    public bool HelpMode { get; set; }
    public bool DiagnosticMode { get; set; }
	
    #endregion
    
    #region Collection Properties

    public List<string> CliArguments { get; } = new();
    public ConcurrentDictionary<string,WatchedFile> WatchedFiles { get; } = new();
    public ConcurrentDictionary<string,WatchedScssFile> WatchedScssFiles { get; } = new();
    public ConcurrentDictionary<string,CssSelector> UsedClasses { get; } = new();
    public Dictionary<string,ScssUtilityClassGroupBase> UtilityClassCollection { get; } = new();
    public List<string> HtmlTagClasses { get; } = new();
    
    #endregion
    
    #region App State Properties

    public static string CliErrorPrefix => "Sfumato => ";
    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public SfumatoSettings Settings { get; set; } = new();
    public ConcurrentDictionary<string,string> DiagnosticOutput { get; set; } = new();
    public string WorkingPathOverride { get; set; } = string.Empty;
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public string YamlPath { get; set; } = string.Empty;
    public List<string> AllVariants { get; } = new();
    public StringBuilder ScssBaseInjectable { get; } = new();
    public StringBuilder ScssSharedInjectable { get; } = new();
    
    #endregion

    public SfumatoAppState()
    {
	    AllVariants.Clear();
	    AllVariants.AddRange(MediaQueryPrefixes.Select(p => p.Prefix));
	    AllVariants.AddRange(PseudoclassPrefixes.Select(p => p.Key));

        ValidCssPropertyNames.Clear();
        ValidCssPropertyNames.AddRange(ValidChromeCssPropertyNames);

        foreach (var propName in ValidSafariCssPropertyNames)
        {
            if (ValidCssPropertyNames.Contains(propName))
                continue;
            
            ValidCssPropertyNames.Add(propName);
        }
        
	    #region Regular Expressions
	    
	    const string arbitraryCssExpression = """
(?<=[\s"'`])
([a-z]{1,25}(\-[a-z]{0,25})?:){0,5}
([\!]?\[[a-z\-]{1,50}\:[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,250}\])
(?=[\s"'`]|[\\"])
""";
	    
	    ArbitraryCssRegex = new Regex(arbitraryCssExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    const string coreClassExpression = """
(?<=[\s"'`])
((peer\-([a-z\-]{1,25}([/]([a-z\-]{0,25})){0,1}?:))|([a-z]{1,25}([\-a-z]{0,25})[a-z]{1,25}?:)){0,5}
(
	([\!\-]{0,2}[a-z]{1,25}(\-[a-z0-9\.%]{0,25}){0,5})
	(
		(/[a-z0-9\-\.]{1,250})|([/]?\[[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,250}\])?
	)
)
(?=[\s"'`]|[\\"])
""";
	    
	    CoreClassRegex = new Regex(coreClassExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);

	    const string sfumatoScssRegexExpression = """
(?<=^|[\s])
(@sfumato[\s]{1,})
(
	([\!\-]?[a-z]{1,25}(\-[a-z0-9\.%]{0,25}){0,5})
	(
		(/[a-z0-9\-\.]{1,250})|([/]?\[[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,250}\])?
	)
	(([\s]{1,})|([\s]{0,};))
){1,}
(?=[\s])
""";
	    
	    SfumatoScssRegex = new Regex(sfumatoScssRegexExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
	    const string sfumatoScssApplyRegexExpression = """
(?<=^|[\s])
(@apply[\s]{1,})
(
	([\!\-]?[a-z]{1,25}(\-[a-z0-9\.%]{0,25}){0,5})
	(
		(/[a-z0-9\-\.]{1,250})|([/]?\[[a-zA-Z0-9%',\!/\-\._\:\(\)\\\*\#\$\^\?\+\{\}]{1,250}\])?
	)
	(([\s]{1,})|([\s]{0,};))
){1,}
(?=[\s])
""";
	    
	    SfumatoScssApplyRegex = new Regex(sfumatoScssApplyRegexExpression.CleanUpIndentedRegex(), RegexOptions.Compiled);
	    
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
	    
	    WatchedFiles.Clear();
	    WatchedScssFiles.Clear();
	    DiagnosticOutput.Clear();
	    
	    await ProcessCliArgumentsAsync(args);
	    
	    timer.Start();
	    
	    #region Establish Theme Dictionaries

	    LayoutRemUnitOptions.Clear();
	    LayoutRemUnitOptions.AddNumberedRemUnitOptions(0.5m, 96m);

	    FlexRemUnitOptions.Clear();
	    FlexRemUnitOptions.AddNumberedRemUnitOptions(0.5m, 96m);

	    LayoutWholeNumberOptions.Clear();
	    LayoutWholeNumberOptions.AddWholeNumberOptions(1, 24);
	    
	    EffectsFiltersOneBasedPercentageOptions.Clear();
	    EffectsFiltersOneBasedPercentageOptions.AddOneBasedPercentageOptions(0m, 200m);

	    FlexboxAndGridWholeNumberOptions.Clear();
	    FlexboxAndGridWholeNumberOptions.AddWholeNumberOptions(1, 25);

	    FlexboxAndGridNegativeWholeNumberOptions.Clear();
	    FlexboxAndGridNegativeWholeNumberOptions.AddWholeNumberOptions(1, 25, true);

	    PercentageOptions.Clear();
	    PercentageOptions.AddPercentageOptions(0, 100);
	    
	    #endregion
	    
	    if (VersionMode == false && HelpMode == false && InitMode == false)
		    await Settings.LoadJsonSettingsAsync(this);
	    
	    #region Load Utility Classes

	    timer.Restart();

	    UtilityClassCollection.Clear();
        
	    var orderedDictionary = new ConcurrentDictionary<string,ScssUtilityClassGroupBase>();
	    var tasks = new List<Task>();
		
	    foreach (var scssUtilityClassGroup in typeof(ScssUtilityClassGroupBase).GetInheritedTypes().OrderBy(o => o.Name).ToList())
		    tasks.Add(AddUtilityClassToCollectionAsync(scssUtilityClassGroup, orderedDictionary));

	    await Task.WhenAll(tasks);

	    foreach (var item in orderedDictionary.OrderByDescending(c => c.Key))
		    UtilityClassCollection.Add(item.Key, item.Value);		
    
	    if (DiagnosticMode)
		    DiagnosticOutput.TryAdd("init1", $"{Strings.TriangleRight} Loaded {UtilityClassCollection.Count:N0} utility classes in {timer.FormatTimer()}{Environment.NewLine}");
	    
	    #endregion
	    
	    #region Find Embedded Resources (Sass, SCSS)
	    
	    SassCliPath = await GetEmbeddedSassPathAsync();
	    ScssPath = await GetEmbeddedScssPathAsync();
	    YamlPath = await GetEmbeddedYamlPathAsync();
	    
	    #endregion

	    if (VersionMode || HelpMode || InitMode)
		    return;

        if (DiagnosticMode)
			DiagnosticOutput.TryAdd("init001", $"{Strings.TriangleRight} Processed settings in {timer.FormatTimer()}{Environment.NewLine}");

        ScssBaseInjectable.Clear();
        ScssBaseInjectable.Append($"/* SFUMATO BROWSER RESET */{Environment.NewLine}{Environment.NewLine}");
        ScssBaseInjectable.Append(await SfumatoScss.GetBaseScssAsync(this, DiagnosticOutput));

        ScssSharedInjectable.Clear();
        ScssSharedInjectable.Append(await SfumatoScss.GetSharedScssAsync(this, DiagnosticOutput));
    }

    private async Task AddUtilityClassToCollectionAsync(Type scssUtilityClassGroup, ConcurrentDictionary<string,ScssUtilityClassGroupBase> dictionary)
    {
	    if (Activator.CreateInstance(scssUtilityClassGroup) is not ScssUtilityClassGroupBase utilityClassGroup) 
		    throw new Exception($"Could not instantiate ScssUtilityClassGroupBase object for {scssUtilityClassGroup.Name}");

	    await utilityClassGroup.InitializeAsync(this);
		
	    foreach (var selector in utilityClassGroup.SelectorIndex)
		    if (dictionary.TryAdd(selector, utilityClassGroup) == false)
			    throw new Exception($"Could not add utility class group {selector}");
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
			if (CliArguments[0] != "help" && CliArguments[0] != "version" && CliArguments[0] != "build" && CliArguments[0] != "watch" && CliArguments[0] != "init")
			{
				await Console.Out.WriteLineAsync("Invalid command specified; must be: help, init, version, build, or watch");
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
		
			if (CliArguments[0] == "init")
			{
				InitMode = true;
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
							    path.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
								continue;

							if (path.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
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

        //workingPath = "/Users/magic/Developer/monitoring-analytics-web/UmbracoCms";
        //workingPath = "/Users/magic/Developer/FileBucket/FileBucket";
        
#endif
        
        return workingPath;
    }

    public async Task<string> GetEmbeddedSassVersionAsync()
    {
        var sassPath = await GetEmbeddedSassPathAsync();
        var sb = StringBuilderPool.Get();

        try
        {
            var cmd = Cli.Wrap(sassPath)
                .WithArguments(arguments => { arguments.Add("--version"); })
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

            _ = cmd.ExecuteAsync().GetAwaiter().GetResult();

            return sb.ToString().Trim();
        }

        catch
        {
            await Console.Out.WriteLineAsync("Dart Sass is embedded but cannot be found.");
            Environment.Exit(1);
        }

        finally
        {
            StringBuilderPool.Return(sb);
        }

        return string.Empty;
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

        try
        {
		    var cmd = Cli.Wrap(sassPath)
			    .WithArguments(arguments =>
			    {
				    arguments.Add("--version");
			    })
			    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
			    .WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

                _ = cmd.ExecuteAsync().GetAwaiter().GetResult();
        }

        catch
        {
            await Console.Out.WriteLineAsync($"{CliErrorPrefix}Dart Sass is embedded but cannot be found.");
            Environment.Exit(1);
        }

        finally
        {
            StringBuilderPool.Return(sb);
        }
        
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

    public static async Task<string> GetEmbeddedYamlPathAsync()
    {
	    var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];
            
#if DEBUG
		    if (Directory.Exists(Path.Combine(workingPath, "yaml")) == false)
			    continue;

		    var tempPath = workingPath; 
			
		    workingPath = Path.Combine(tempPath, "yaml");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "yaml");
#endif
		    break;
	    }

	    // ReSharper disable once InvertIf
	    if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
	    {
		    await Console.Out.WriteLineAsync($"{CliErrorPrefix}Embedded YAML resources cannot be found.");
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
		var totalTimer = new Stopwatch();

		totalTimer.Start();

		if (Settings.ProjectPaths.Count == 0)
		{
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} No project paths specified");
			return;
		}
		
		var tasks = new List<Task>();

		// Gather files lists
		
		foreach (var projectPath in Settings.ProjectPaths)
			tasks.Add(RecurseProjectPathAsync(projectPath.Path, projectPath.ExtensionsList, projectPath.Recurse));

		await Task.WhenAll(tasks);
		
		tasks.Clear();
		
		// Add matches to files lists

		foreach (var watchedFile in WatchedFiles)
			tasks.Add(ProcessFileMatchesAsync(watchedFile.Value));
		
		await Task.WhenAll(tasks);

		// Generate used classes list

		await ExamineWatchedFilesForUsedClassesAsync();

		if (WatchedFiles.IsEmpty)
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified no watched files");
		else
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Identified {UsedClasses.Count(u => u.Value.IsInvalid == false):N0} utilities used by {WatchedFiles.Count:N0} project file{(WatchedFiles.Count == 1 ? string.Empty : "s")} in {totalTimer.FormatTimer()}");
	}
	
	/// <summary>
	/// Recurse a project path to collect all matching files.
	/// </summary>
	/// <param name="sourcePath"></param>
	/// <param name="extensionsList"></param>
	/// <param name="recurse"></param>
	public async Task RecurseProjectPathAsync(string? sourcePath, List<string> extensionsList, bool recurse = false)
	{
		if (string.IsNullOrEmpty(sourcePath) || sourcePath.IsEmpty() || extensionsList.Count == 0)
			return;

		FileInfo[] files = null!;
		DirectoryInfo[] dirs = null!;
		
		var dir = new DirectoryInfo(sourcePath);

		if (dir.Exists == false)
		{
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} WARNING: Source directory could not be found: {sourcePath}");
            return;
        }

		dirs = dir.GetDirectories();
		files = dir.GetFiles();

		var tasks = new List<Task>();
		
		foreach (var projectFile in files)
		{
			var extension = ProjectPath.GetMatchingFileExtension(projectFile.Name, extensionsList);
			
			if (extension != string.Empty)
				tasks.Add(AddProjectFileToCollectionAsync(projectFile, extension));
		}

		await Task.WhenAll(tasks);

		if (recurse == false)
			return;
		
		foreach (var subDir in dirs.OrderBy(d => d.Name))
			await RecurseProjectPathAsync(subDir.FullName, extensionsList, recurse);
	}

	/// <summary>
	/// Read a FileInfo object and add it to the appropriate collection.
	/// </summary>
	/// <param name="projectFile"></param>
	/// <param name="extension"></param>
	public async Task AddProjectFileToCollectionAsync(FileInfo projectFile, string extension)
	{
		if (projectFile.Name.Equals("sfumato.yml", StringComparison.OrdinalIgnoreCase))
			return;

		if (extension == "scss" && projectFile.Name.StartsWith("_", StringComparison.OrdinalIgnoreCase))
			return;
		
		var markup = await File.ReadAllTextAsync(projectFile.FullName);

		if (extension == "scss")
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
		var matches = CoreClassRegex.Matches(watchedFile.Markup).DistinctBy(m => m.Value).ToList();

		if (matches.Count > 0)
		{
			FilterCoreClassMatches(matches);
			
			foreach (var match in matches)
				tasks.Add(AddCssSelectorToCollection(watchedFile.CoreClassMatches, match.Value));
		}

		await Task.WhenAll(tasks);

		tasks.Clear();
		
		matches = ArbitraryCssRegex.Matches(watchedFile.Markup).DistinctBy(m => m.Value).ToList();

		if (matches.Count > 0)
		{
			FilterArbitraryCssMatches(matches);

			foreach (var match in matches)
			{
				tasks.Add(AddCssSelectorToCollection(watchedFile.ArbitraryCssMatches, match.Value, true));
			}
		}
		
		await Task.WhenAll(tasks);
	}

	/// <summary>
	/// Create a new CssSelector and add to a collection.
	/// 
	/// </summary>
	/// <param name="collection"></param>
	/// <param name="value"></param>
	/// <param name="isArbitraryCss"></param>
	public async Task AddCssSelectorToCollection(ConcurrentDictionary<string,CssSelector> collection, string value, bool isArbitraryCss = false)
	{
		if (collection.ContainsKey(value))
			return;

		var cssSelector = new CssSelector(this, value, isArbitraryCss);

		await cssSelector.ProcessSelectorAsync();

		if (cssSelector.IsInvalid)
			return;

		cssSelector.GetStyles();
		collection.TryAdd(value, cssSelector);
	}
	
	/// <summary>
	/// Verifuy a string of colon-separated variants.
	/// </summary>
	/// <param name="variantsList"></param>
	/// <returns></returns>
	public bool VariantsAreValid(string variantsList)
	{
		var variants = variantsList.TrimEnd(':').Split(':');

        foreach (var variant in variants)
        {
            if (variant.StartsWith("peer-"))
                return CssSelector.TryHasPeerVariant(this, $"{variant}:", out _, out _);

            if (AllVariants.Contains(variant) == false)
                return false;
        }

		return true;
	}
	
	/// <summary>
	/// Remove invalid utility class matches from a list of regex matches.
	/// </summary>
	/// <param name="matches"></param>
	public void FilterCoreClassMatches(List<Match> matches)
	{
		foreach (var match in matches.ToList())
		{
			if (IsValidCoreClassSelector(match.Value) == false)
				matches.Remove(match);
		}
	}

	/// <summary>
	/// Determine if a selector is a valid core class.
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public bool IsValidCoreClassSelector(string selector)
	{
		var invalidEnding = false;

		foreach (var exclusion in ClassMatchEndingExclusions)
			if (selector.EndsWith(exclusion))
			{
				invalidEnding = true;
				break;
			}

		if (invalidEnding)
			return false;
			
		var variants = string.Empty;
		var indexOfBracket = selector.IndexOf('[');

		if (indexOfBracket > -1)
			selector = selector[..indexOfBracket].TrimEnd('-').TrimEnd('/');
			
		var indexOfColon = selector.LastIndexOf(':');

		if (indexOfColon > 1 && indexOfColon < selector.Length - 1)
		{
			variants = selector[..(indexOfColon + 1)];
			selector = selector[(indexOfColon + 1)..];

			if (VariantsAreValid(variants) == false)
				return false;
		}
        
        selector = selector.TrimStart('!');
        selector = selector.TrimStart('-');
        
		var indexOfSlash = selector.LastIndexOf('/');

        if (selector.IndexOf("peer-", StringComparison.Ordinal) > -1)
        {
            var matches = PeerVariantRegex.Matches(selector);

            if (matches.Count == 1)
            {
                var peerSlashIndex = matches[0].Value.IndexOf('/') + selector.IndexOf(matches[0].Value, StringComparison.Ordinal);

                if (peerSlashIndex == indexOfSlash)
                    indexOfSlash = -1;
            }
        }
        
		if (indexOfSlash > -1)
			selector = selector[..indexOfSlash];

		if (UtilityClassCollection.ContainsKey(selector))
			return true;
			
		return false;
	}
	
	/// <summary>
	/// Remove invalid arbitrary CSS matches from a list of regex matches.
	/// </summary>
	/// <param name="matches"></param>
	public void FilterArbitraryCssMatches(List<Match> matches)
	{
		foreach (var match in matches.ToList())
		{
			var value = match.Value;
			var variants = string.Empty;
			var indexOfBracket = value.IndexOf('[');

			if (indexOfBracket == -1)
			{
				matches.Remove(match);
				continue;
			}
			
			if (indexOfBracket > 1)
			{
				variants = value[..indexOfBracket];
				value = value[indexOfBracket..];

				if (VariantsAreValid(variants) == false)
				{
					matches.Remove(match);
					continue;
				}
			}

			value = value.TrimStart('!').TrimStart('[').TrimEnd(']');
			
			var indexOfColon = value.IndexOf(':');

			if (indexOfColon == -1)
			{
				matches.Remove(match);
				continue;
			}

			var segments = value.Split(':');
			
			if (ValidCssPropertyNames.Contains(segments[0]))
				continue;

			matches.Remove(match);
		}
	}
	
	/// <summary>
	/// Examine all watched files for used classes.
	/// Generates the UsedClasses collection.
	/// </summary>
	public async Task ExamineWatchedFilesForUsedClassesAsync()
	{
		UsedClasses.Clear();
        HtmlTagClasses.Clear();

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
        // Classes on the root tag need special treatment when using theme modes
        // So keep track of all root tag classes for dark mode so their selectors
        // can be modified later.
        if (watchedFile.Markup.Contains("<html "))
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(watchedFile.Markup);

            var htmlTag = htmlDoc.DocumentNode.SelectSingleNode("//html");

            if (htmlTag != null)
            {
                var classes = htmlTag.GetAttributeValue("class", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var className in classes)
                {
                    if (className.StartsWith("dark:") == false || className.Equals("html.theme-dark") || className.Equals("html.theme-light") || className.Equals("html.theme-auto"))
                        continue;

                    if (HtmlTagClasses.Contains(className))
                        continue;

                    HtmlTagClasses.Add(className);
                }
            }
        }
        
		foreach (var cssSelector in watchedFile.CoreClassMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedSelector))
				continue;
            
			if (cssSelector.ScssUtilityClassGroup is null)
				continue;

			var newCssSelector = new CssSelector(this, cssSelector.Selector, cssSelector.IsArbitraryCss);
			await newCssSelector.ProcessSelectorAsync();
            
			if (newCssSelector.IsInvalid)
				continue;
			
			newCssSelector.GetStyles();
			UsedClasses.TryAdd(cssSelector.FixedSelector, newCssSelector);
		}

		foreach (var cssSelector in watchedFile.ArbitraryCssMatches.Values)
		{
			if (UsedClasses.ContainsKey(cssSelector.FixedSelector))
				continue;

			var newCssSelector = new CssSelector(this, cssSelector.Selector, cssSelector.IsArbitraryCss);
			await newCssSelector.ProcessSelectorAsync();

			if (newCssSelector.IsInvalid)
				continue;
			
			newCssSelector.GetStyles();
			UsedClasses.TryAdd(cssSelector.FixedSelector, newCssSelector);
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