namespace Argentini.Sfumato.Collections;

public sealed class ScssClassCollection
{
    #region Layout

    public ScssBaseClass AspectRatio { get; } = new()
    {
        SelectorPrefix = "aspect",
        PropertyName = "aspect-ratio",
        PrefixValueTypes = "ratio",        
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["auto"] = "auto",
            ["square"] = "1/1",
            ["video"] = "16/9",
            ["screen"] = "4/3"
        }
    };
    
    public ScssUtilityBaseClass Container { get; } = new()
    {
        Selector = "container",
        Template = """
                   width: 100%;

                   @include sf-media($from: phab) {
                      max-width: $phab-breakpoint;
                   }

                   @include sf-media($from: tabp) {
                      max-width: $tabp-breakpoint;
                   }

                   @include sf-media($from: tabl) {
                      max-width: $tabl-breakpoint;
                   }

                   @include sf-media($from: note) {
                      max-width: $note-breakpoint;
                   }

                   @include sf-media($from: desk) {
                      max-width: $desk-breakpoint;
                   }

                   @include sf-media($from: elas) {
                      max-width: $elas-breakpoint;
                   }
                   """
    };

    public ScssBaseClass Columns { get; } = new()
    {
        SelectorPrefix = "columns",
        PropertyName = "columns",
        PrefixValueTypes = "length,percentage,integer",        
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["auto"] = "auto",
            ["3xs"] = "16rem",
            ["2xs"] = "18rem",
            ["xs"] = "20rem",
            ["sm"] = "24rem",
            ["md"] = "28rem",
            ["lg"] = "32rem",
            ["xl"] = "36rem",
            ["2xl"] = "42rem",
            ["3xl"] = "48rem",
            ["4xl"] = "56rem",
            ["5xl"] = "64rem",
            ["6xl"] = "72rem",
            ["7xl"] = "80rem",
            ["1"] = "1",
            ["2"] = "2",
            ["3"] = "3",
            ["4"] = "4",
            ["5"] = "5",
            ["6"] = "6",
            ["7"] = "7",
            ["8"] = "8",
            ["9"] = "9",
            ["10"] = "10",
            ["11"] = "11",
            ["12"] = "12"
        }
    };
    
    public ScssBaseClass BreakAfter { get; } = new()
    {
        SelectorPrefix = "break-after",
        PropertyName = "break-after",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["all"] = "all",
            ["avoid-page"] = "avoid-page",
            ["page"] = "page",
            ["left"] = "left",
            ["right"] = "right",
            ["column"] = "column"
        }
    };

    public ScssBaseClass BreakBefore { get; } = new()
    {
        SelectorPrefix = "break-before",
        PropertyName = "break-before",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["all"] = "all",
            ["avoid-page"] = "avoid-page",
            ["page"] = "page",
            ["left"] = "left",
            ["right"] = "right",
            ["column"] = "column"
        }
    };

    public ScssBaseClass BreakInside { get; } = new()
    {
        SelectorPrefix = "break-inside",
        PropertyName = "break-inside",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["avoid-page"] = "avoid-page",
            ["avoid-column"] = "avoid-column"
        }
    };
    
    public ScssBaseClass BoxDecorationBreak { get; } = new()
    {
        SelectorPrefix = "box-decoration",
        PropertyName = "box-decoration-break",
        Options = new Dictionary<string, string>
        {
            ["clone"] = "clone",
            ["slice"] = "slice"
        }
    };

    public ScssBaseClass BoxSizing { get; } = new()
    {
        SelectorPrefix = "box",
        PropertyName = "box-sizing",
        Options = new Dictionary<string, string>
        {
            ["border"] = "border-box",
            ["content"] = "content-box"
        }
    };
    
    public ScssBaseClass Display { get; } = new()
    {
        PropertyName = "display",
        Options = new Dictionary<string, string>
        {
            ["block"] = "block",
            ["inline-block"] = "inline-block",
            ["inline"] = "inline",
            ["flex"] = "flex",
            ["inline-flex"] = "inline-flex",
            ["table"] = "table",
            ["inline-table"] = "inline-table",
            ["table-caption"] = "table-caption",
            ["table-cell"] = "table-cell",
            ["table-column"] = "table-column",
            ["table-column-group"] = "table-column-group",
            ["table-footer-group"] = "table-footer-group",
            ["table-header-group"] = "table-header-group",
            ["table-row-group"] = "table-row-group",
            ["table-row"] = "table-row",
            ["flow-root"] = "flow-root",
            ["grid"] = "grid",
            ["inline-grid"] = "inline-grid",
            ["contents"] = "contents",
            ["list-item"] = "list-item",
            ["hidden"] = "none"
        }
    };
    
    public ScssBaseClass Floats { get; } = new()
    {
        SelectorPrefix = "float",
        PropertyName = "float",
        Options = new Dictionary<string, string>
        {
            ["right"] = "right",
            ["left"] = "left",
            ["none"] = "none"
        }
    };

    public ScssBaseClass Clear { get; } = new()
    {
        SelectorPrefix = "clear",
        PropertyName = "clear",
        Options = new Dictionary<string, string>
        {
            ["right"] = "right",
            ["left"] = "left",
            ["both"] = "both",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass Isolation { get; } = new()
    {
        PropertyName = "isolation",
        Options = new Dictionary<string, string>
        {
            ["isolate"] = "isolate",
            ["isolation-auto"] = "auto"
        }
    };
    
    public ScssBaseClass ObjectFit { get; } = new()
    {
        SelectorPrefix = "object",
        PropertyName = "object-fit",
        Options = new Dictionary<string, string>
        {
            ["contain"] = "contain",
            ["cover"] = "cover",
            ["fill"] = "fill",
            ["none"] = "none",
            ["scale-down"] = "scale-down"
        }
    };

    public ScssBaseClass ObjectBottom { get; } = new()
    {
        SelectorPrefix = "object",
        PropertyName = "object-position",
        Options = new Dictionary<string, string>
        {
            ["bottom"] = "bottom",
            ["center"] = "center",
            ["left"] = "left",
            ["left-bottom"] = "left bottom",
            ["left-top"] = "left top",
            ["right"] = "right",
            ["right-bottom"] = "right bottom",
            ["right-top"] = "right top",
            ["top"] = "top"
        }
    };
    
    public ScssBaseClass Overflow { get; } = new()
    {
        SelectorPrefix = "overflow",
        PropertyName = "overflow",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass OverflowX { get; } = new()
    {
        SelectorPrefix = "overflow-x",
        PropertyName = "overflow-x",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass OverflowY { get; } = new()
    {
        SelectorPrefix = "overflow-y",
        PropertyName = "overflow-y",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass OverscrollBehavior { get; } = new()
    {
        SelectorPrefix = "overscroll",
        PropertyName = "overscroll-behavior",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass OverscrollBehaviorX { get; } = new()
    {
        SelectorPrefix = "overscroll-x",
        PropertyName = "overscroll-behavior-x",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass OverscrollBehaviorY { get; } = new()
    {
        SelectorPrefix = "overscroll-y",
        PropertyName = "overscroll-behavior-y",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass Position { get; } = new()
    {
        PropertyName = "position",
        Options = new Dictionary<string, string>
        {
            ["static"] = "static",
            ["fixed"] = "fixed",
            ["absolute"] = "absolute",
            ["relative"] = "relative",
            ["sticky"] = "sticky"
        }
    };

    public ScssMeasurementClass Top { get; } = new()
    {
        SelectorPrefix = "top",
        PropertyName = "top",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };

    public ScssMeasurementClass Right { get; } = new()
    {
        SelectorPrefix = "right",
        PropertyName = "right",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };

    public ScssMeasurementClass Bottom { get; } = new()
    {
        SelectorPrefix = "bottom",
        PropertyName = "bottom",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssMeasurementClass Left { get; } = new()
    {
        SelectorPrefix = "left",
        PropertyName = "left",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssMeasurementClass Inset { get; } = new()
    {
        SelectorPrefix = "inset",
        PropertyName = "inset",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssMeasurementClass InsetX { get; } = new()
    {
        SelectorPrefix = "inset-x",
        PropertyName = "left,right",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssMeasurementClass InsetY { get; } = new()
    {
        SelectorPrefix = "inset-y",
        PropertyName = "top,bottom",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssMeasurementClass Start { get; } = new()
    {
        SelectorPrefix = "start",
        PropertyName = "inset-inline-start",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssMeasurementClass End { get; } = new()
    {
        SelectorPrefix = "end",
        PropertyName = "inset-inline-end",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Visibility { get; } = new()
    {
        PropertyName = "visibility",
        Options = new Dictionary<string, string>
        {
            ["visible"] = "visible",
            ["invisible"] = "hidden",
            ["collapse"] = "collapse"
        }
    };
    
    public ScssBaseClass Zindex { get; } = new()
    {
        SelectorPrefix = "z",
        PropertyName = "z-index",
        PrefixValueTypes = "integer",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["auto"] = "auto",
            ["top"] = int.MaxValue.ToString(),
            ["bottom"] = int.MinValue.ToString(),
            ["0"] = "0",
            ["10"] = "10",
            ["20"] = "20",
            ["30"] = "30",
            ["40"] = "40",
            ["50"] = "50"
        }
    };
    
    #endregion
    
    #region Flexbox and Grid
    
    public ScssMeasurementClass FlexBasis { get; } = new()
    {
        SelectorPrefix = "basis",
        PropertyName = "flex-basis",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["0.5"] = "0.125rem",
            ["1.5"] = "0.375rem",
            ["2.5"] = "0.625rem",
            ["3.5"] = "0.875rem",
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
        }
    };

    
    
    
    
    
    #endregion
    
    #region Backgrounds
    
    public ScssBaseClass BgColor { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-color",
        PrefixValueTypes = "color",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["inherit"] = "inherit",
            ["transparent"] = "transparent",
            ["black"] = "rgb(0 0 0)",
            ["white"] = "rgb(255 255 255)",
            ["slate-50"] = "rgb(248 250 252)",
            ["slate-100"] = "rgb(241 245 249)",
            ["slate-200"] = "rgb(226 232 240)",
            ["slate-300"] = "rgb(203 213 225)",
            ["slate-400"] = "rgb(148 163 184)",
            ["slate-500"] = "rgb(100 116 139)",
            ["slate-600"] = "rgb(71 85 105)",
            ["slate-700"] = "rgb(51 65 85)",
            ["slate-800"] = "rgb(30 41 59)",
            ["slate-900"] = "rgb(15 23 42)",
            ["slate-950"] = "rgb(2 6 23)",
            ["gray-50"] = "rgb(249 250 251)",
            ["gray-100"] = "rgb(243 244 246)",
            ["gray-200"] = "rgb(229 231 235)",
            ["gray-300"] = "rgb(209 213 219)",
            ["gray-400"] = "rgb(156 163 175)",
            ["gray-500"] = "rgb(107 114 128)",
            ["gray-600"] = "rgb(75 85 99)",
            ["gray-700"] = "rgb(55 65 81)",
            ["gray-800"] = "rgb(31 41 55)",
            ["gray-900"] = "rgb(17 24 39)",
            ["gray-950"] = "rgb(3 7 18)",
            ["zinc-50"] = "rgb(250 250 250)",
            ["zinc-100"] = "rgb(244 244 245)",
            ["zinc-200"] = "rgb(228 228 231)",
            ["zinc-300"] = "rgb(212 212 216)",
            ["zinc-400"] = "rgb(161 161 170)",
            ["zinc-500"] = "rgb(113 113 122)",
            ["zinc-600"] = "rgb(82 82 91)",
            ["zinc-700"] = "rgb(63 63 70)",
            ["zinc-800"] = "rgb(39 39 42)",
            ["zinc-900"] = "rgb(24 24 27)",
            ["zinc-950"] = "rgb(9 9 11)",
            ["neutral-50"] = "rgb(250 250 250)",
            ["neutral-100"] = "rgb(245 245 245)",
            ["neutral-200"] = "rgb(229 229 229)",
            ["neutral-300"] = "rgb(212 212 212)",
            ["neutral-400"] = "rgb(163 163 163)",
            ["neutral-500"] = "rgb(115 115 115)",
            ["neutral-600"] = "rgb(82 82 82)",
            ["neutral-700"] = "rgb(64 64 64)",
            ["neutral-800"] = "rgb(38 38 38)",
            ["neutral-900"] = "rgb(23 23 23)",
            ["neutral-950"] = "rgb(10 10 10)",
            ["stone-50"] = "rgb(250 250 249)",
            ["stone-100"] = "rgb(245 245 244)",
            ["stone-200"] = "rgb(231 229 228)",
            ["stone-300"] = "rgb(214 211 209)",
            ["stone-400"] = "rgb(168 162 158)",
            ["stone-500"] = "rgb(120 113 108)",
            ["stone-600"] = "rgb(87 83 78)",
            ["stone-700"] = "rgb(68 64 60)",
            ["stone-800"] = "rgb(41 37 36)",
            ["stone-900"] = "rgb(28 25 23)",
            ["stone-950"] = "rgb(12 10 9)",
            ["red-50"] = "rgb(254 242 242)",
            ["red-100"] = "rgb(254 226 226)",
            ["red-200"] = "rgb(254 202 202)",
            ["red-300"] = "rgb(252 165 165)",
            ["red-400"] = "rgb(248 113 113)",
            ["red-500"] = "rgb(239 68 68)",
            ["red-600"] = "rgb(220 38 38)",
            ["red-700"] = "rgb(185 28 28)",
            ["red-800"] = "rgb(153 27 27)",
            ["red-900"] = "rgb(127 29 29)",
            ["red-950"] = "rgb(69 10 10)",
            ["orange-50"] = "rgb(255 247 237)",
            ["orange-100"] = "rgb(255 237 213)",
            ["orange-200"] = "rgb(254 215 170)",
            ["orange-300"] = "rgb(253 186 116)",
            ["orange-400"] = "rgb(251 146 60)",
            ["orange-500"] = "rgb(249 115 22)",
            ["orange-600"] = "rgb(234 88 12)",
            ["orange-700"] = "rgb(194 65 12)",
            ["orange-800"] = "rgb(154 52 18)",
            ["orange-900"] = "rgb(124 45 18)",
            ["orange-950"] = "rgb(67 20 7)",
            ["amber-50"] = "rgb(255 251 235)",
            ["amber-100"] = "rgb(254 243 199)",
            ["amber-200"] = "rgb(253 230 138)",
            ["amber-300"] = "rgb(252 211 77)",
            ["amber-400"] = "rgb(251 191 36)",
            ["amber-500"] = "rgb(245 158 11)",
            ["amber-600"] = "rgb(217 119 6)",
            ["amber-700"] = "rgb(180 83 9)",
            ["amber-800"] = "rgb(146 64 14)",
            ["amber-900"] = "rgb(120 53 15)",
            ["amber-950"] = "rgb(69 26 3)",
            ["yellow-50"] = "rgb(254 252 232)",
            ["yellow-100"] = "rgb(254 249 195)",
            ["yellow-200"] = "rgb(254 240 138)",
            ["yellow-300"] = "rgb(253 224 71)",
            ["yellow-400"] = "rgb(250 204 21)",
            ["yellow-500"] = "rgb(234 179 8)",
            ["yellow-600"] = "rgb(202 138 4)",
            ["yellow-700"] = "rgb(161 98 7)",
            ["yellow-800"] = "rgb(133 77 14)",
            ["yellow-900"] = "rgb(113 63 18)",
            ["yellow-950"] = "rgb(66 32 6)",
            ["lime-50"] = "rgb(247 254 231)",
            ["lime-100"] = "rgb(236 252 203)",
            ["lime-200"] = "rgb(217 249 157)",
            ["lime-300"] = "rgb(190 242 100)",
            ["lime-400"] = "rgb(163 230 53)",
            ["lime-500"] = "rgb(132 204 22)",
            ["lime-600"] = "rgb(101 163 13)",
            ["lime-700"] = "rgb(77 124 15)",
            ["lime-800"] = "rgb(63 98 18)",
            ["lime-900"] = "rgb(54 83 20)",
            ["lime-950"] = "rgb(26 46 5)",
            ["green-50"] = "rgb(240 253 244)",
            ["green-100"] = "rgb(220 252 231)",
            ["green-200"] = "rgb(187 247 208)",
            ["green-300"] = "rgb(134 239 172)",
            ["green-400"] = "rgb(74 222 128)",
            ["green-500"] = "rgb(34 197 94)",
            ["green-600"] = "rgb(22 163 74)",
            ["green-700"] = "rgb(21 128 61)",
            ["green-800"] = "rgb(22 101 52)",
            ["green-900"] = "rgb(20 83 45)",
            ["green-950"] = "rgb(5 46 22)",
            ["emerald-50"] = "rgb(236 253 245)",
            ["emerald-100"] = "rgb(209 250 229)",
            ["emerald-200"] = "rgb(167 243 208)",
            ["emerald-300"] = "rgb(110 231 183)",
            ["emerald-400"] = "rgb(52 211 153)",
            ["emerald-500"] = "rgb(16 185 129)",
            ["emerald-600"] = "rgb(5 150 105)",
            ["emerald-700"] = "rgb(4 120 87)",
            ["emerald-800"] = "rgb(6 95 70)",
            ["emerald-900"] = "rgb(6 78 59)",
            ["emerald-950"] = "rgb(2 44 34)",
            ["teal-50"] = "rgb(240 253 250)",
            ["teal-100"] = "rgb(204 251 241)",
            ["teal-200"] = "rgb(153 246 228)",
            ["teal-300"] = "rgb(94 234 212)",
            ["teal-400"] = "rgb(45 212 191)",
            ["teal-500"] = "rgb(20 184 166)",
            ["teal-600"] = "rgb(13 148 136)",
            ["teal-700"] = "rgb(15 118 110)",
            ["teal-800"] = "rgb(17 94 89)",
            ["teal-900"] = "rgb(19 78 74)",
            ["teal-950"] = "rgb(4 47 46)",
            ["cyan-50"] = "rgb(236 254 255)",
            ["cyan-100"] = "rgb(207 250 254)",
            ["cyan-200"] = "rgb(165 243 252)",
            ["cyan-300"] = "rgb(103 232 249)",
            ["cyan-400"] = "rgb(34 211 238)",
            ["cyan-500"] = "rgb(6 182 212)",
            ["cyan-600"] = "rgb(8 145 178)",
            ["cyan-700"] = "rgb(14 116 144)",
            ["cyan-800"] = "rgb(21 94 117)",
            ["cyan-900"] = "rgb(22 78 99)",
            ["cyan-950"] = "rgb(8 51 68)",
            ["sky-50"] = "rgb(240 249 255)",
            ["sky-100"] = "rgb(224 242 254)",
            ["sky-200"] = "rgb(186 230 253)",
            ["sky-300"] = "rgb(125 211 252)",
            ["sky-400"] = "rgb(56 189 248)",
            ["sky-500"] = "rgb(14 165 233)",
            ["sky-600"] = "rgb(2 132 199)",
            ["sky-700"] = "rgb(3 105 161)",
            ["sky-800"] = "rgb(7 89 133)",
            ["sky-900"] = "rgb(12 74 110)",
            ["sky-950"] = "rgb(8 47 73)",
            ["blue-50"] = "rgb(239 246 255)",
            ["blue-100"] = "rgb(219 234 254)",
            ["blue-200"] = "rgb(191 219 254)",
            ["blue-300"] = "rgb(147 197 253)",
            ["blue-400"] = "rgb(96 165 250)",
            ["blue-500"] = "rgb(59 130 246)",
            ["blue-600"] = "rgb(37 99 235)",
            ["blue-700"] = "rgb(29 78 216)",
            ["blue-800"] = "rgb(30 64 175)",
            ["blue-900"] = "rgb(30 58 138)",
            ["blue-950"] = "rgb(23 37 84)",
            ["indigo-50"] = "rgb(238 242 255)",
            ["indigo-100"] = "rgb(224 231 255)",
            ["indigo-200"] = "rgb(199 210 254)",
            ["indigo-300"] = "rgb(165 180 252)",
            ["indigo-400"] = "rgb(129 140 248)",
            ["indigo-500"] = "rgb(99 102 241)",
            ["indigo-600"] = "rgb(79 70 229)",
            ["indigo-700"] = "rgb(67 56 202)",
            ["indigo-800"] = "rgb(55 48 163)",
            ["indigo-900"] = "rgb(49 46 129)",
            ["indigo-950"] = "rgb(30 27 75)",
            ["violet-50"] = "rgb(245 243 255)",
            ["violet-100"] = "rgb(237 233 254)",
            ["violet-200"] = "rgb(221 214 254)",
            ["violet-300"] = "rgb(196 181 253)",
            ["violet-400"] = "rgb(167 139 250)",
            ["violet-500"] = "rgb(139 92 246)",
            ["violet-600"] = "rgb(124 58 237)",
            ["violet-700"] = "rgb(109 40 217)",
            ["violet-800"] = "rgb(91 33 182)",
            ["violet-900"] = "rgb(76 29 149)",
            ["violet-950"] = "rgb(46 16 101)",
            ["purple-50"] = "rgb(250 245 255)",
            ["purple-100"] = "rgb(243 232 255)",
            ["purple-200"] = "rgb(233 213 255)",
            ["purple-300"] = "rgb(216 180 254)",
            ["purple-400"] = "rgb(192 132 252)",
            ["purple-500"] = "rgb(168 85 247)",
            ["purple-600"] = "rgb(147 51 234)",
            ["purple-700"] = "rgb(126 34 206)",
            ["purple-800"] = "rgb(107 33 168)",
            ["purple-900"] = "rgb(88 28 135)",
            ["purple-950"] = "rgb(59 7 100)",
            ["fuchsia-50"] = "rgb(253 244 255)",
            ["fuchsia-100"] = "rgb(250 232 255)",
            ["fuchsia-200"] = "rgb(245 208 254)",
            ["fuchsia-300"] = "rgb(240 171 252)",
            ["fuchsia-400"] = "rgb(232 121 249)",
            ["fuchsia-500"] = "rgb(217 70 239)",
            ["fuchsia-600"] = "rgb(192 38 211)",
            ["fuchsia-700"] = "rgb(162 28 175)",
            ["fuchsia-800"] = "rgb(134 25 143)",
            ["fuchsia-900"] = "rgb(112 26 117)",
            ["fuchsia-950"] = "rgb(74 4 78)",
            ["pink-50"] = "rgb(253 242 248)",
            ["pink-100"] = "rgb(252 231 243)",
            ["pink-200"] = "rgb(251 207 232)",
            ["pink-300"] = "rgb(249 168 212)",
            ["pink-400"] = "rgb(244 114 182)",
            ["pink-500"] = "rgb(236 72 153)",
            ["pink-600"] = "rgb(219 39 119)",
            ["pink-700"] = "rgb(190 24 93)",
            ["pink-800"] = "rgb(157 23 77)",
            ["pink-900"] = "rgb(131 24 67)",
            ["pink-950"] = "rgb(80 7 36)",
            ["rose-50"] = "rgb(255 241 242)",
            ["rose-100"] = "rgb(255 228 230)",
            ["rose-200"] = "rgb(254 205 211)",
            ["rose-300"] = "rgb(253 164 175)",
            ["rose-400"] = "rgb(251 113 133)",
            ["rose-500"] = "rgb(244 63 94)",
            ["rose-600"] = "rgb(225 29 72)",
            ["rose-700"] = "rgb(190 18 60)",
            ["rose-800"] = "rgb(159 18 57)",
            ["rose-900"] = "rgb(136 19 55)",
            ["rose-950"] = "rgb(76 5 25)"
        }
    };
    
    #endregion

    #region Typography
    
    public TextSize TextSize { get; } = new();

    public ScssBaseClass TextColor { get; } = new()
    {
        SelectorPrefix = "text",
        PropertyName = "color",
        PrefixValueTypes = "color",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["inherit"] = "inherit",
            ["transparent"] = "transparent",
            ["black"] = "rgb(0 0 0)",
            ["white"] = "rgb(255 255 255)",
            ["slate-50"] = "rgb(248 250 252)",
            ["slate-100"] = "rgb(241 245 249)",
            ["slate-200"] = "rgb(226 232 240)",
            ["slate-300"] = "rgb(203 213 225)",
            ["slate-400"] = "rgb(148 163 184)",
            ["slate-500"] = "rgb(100 116 139)",
            ["slate-600"] = "rgb(71 85 105)",
            ["slate-700"] = "rgb(51 65 85)",
            ["slate-800"] = "rgb(30 41 59)",
            ["slate-900"] = "rgb(15 23 42)",
            ["slate-950"] = "rgb(2 6 23)",
            ["gray-50"] = "rgb(249 250 251)",
            ["gray-100"] = "rgb(243 244 246)",
            ["gray-200"] = "rgb(229 231 235)",
            ["gray-300"] = "rgb(209 213 219)",
            ["gray-400"] = "rgb(156 163 175)",
            ["gray-500"] = "rgb(107 114 128)",
            ["gray-600"] = "rgb(75 85 99)",
            ["gray-700"] = "rgb(55 65 81)",
            ["gray-800"] = "rgb(31 41 55)",
            ["gray-900"] = "rgb(17 24 39)",
            ["gray-950"] = "rgb(3 7 18)",
            ["zinc-50"] = "rgb(250 250 250)",
            ["zinc-100"] = "rgb(244 244 245)",
            ["zinc-200"] = "rgb(228 228 231)",
            ["zinc-300"] = "rgb(212 212 216)",
            ["zinc-400"] = "rgb(161 161 170)",
            ["zinc-500"] = "rgb(113 113 122)",
            ["zinc-600"] = "rgb(82 82 91)",
            ["zinc-700"] = "rgb(63 63 70)",
            ["zinc-800"] = "rgb(39 39 42)",
            ["zinc-900"] = "rgb(24 24 27)",
            ["zinc-950"] = "rgb(9 9 11)",
            ["neutral-50"] = "rgb(250 250 250)",
            ["neutral-100"] = "rgb(245 245 245)",
            ["neutral-200"] = "rgb(229 229 229)",
            ["neutral-300"] = "rgb(212 212 212)",
            ["neutral-400"] = "rgb(163 163 163)",
            ["neutral-500"] = "rgb(115 115 115)",
            ["neutral-600"] = "rgb(82 82 82)",
            ["neutral-700"] = "rgb(64 64 64)",
            ["neutral-800"] = "rgb(38 38 38)",
            ["neutral-900"] = "rgb(23 23 23)",
            ["neutral-950"] = "rgb(10 10 10)",
            ["stone-50"] = "rgb(250 250 249)",
            ["stone-100"] = "rgb(245 245 244)",
            ["stone-200"] = "rgb(231 229 228)",
            ["stone-300"] = "rgb(214 211 209)",
            ["stone-400"] = "rgb(168 162 158)",
            ["stone-500"] = "rgb(120 113 108)",
            ["stone-600"] = "rgb(87 83 78)",
            ["stone-700"] = "rgb(68 64 60)",
            ["stone-800"] = "rgb(41 37 36)",
            ["stone-900"] = "rgb(28 25 23)",
            ["stone-950"] = "rgb(12 10 9)",
            ["red-50"] = "rgb(254 242 242)",
            ["red-100"] = "rgb(254 226 226)",
            ["red-200"] = "rgb(254 202 202)",
            ["red-300"] = "rgb(252 165 165)",
            ["red-400"] = "rgb(248 113 113)",
            ["red-500"] = "rgb(239 68 68)",
            ["red-600"] = "rgb(220 38 38)",
            ["red-700"] = "rgb(185 28 28)",
            ["red-800"] = "rgb(153 27 27)",
            ["red-900"] = "rgb(127 29 29)",
            ["red-950"] = "rgb(69 10 10)",
            ["orange-50"] = "rgb(255 247 237)",
            ["orange-100"] = "rgb(255 237 213)",
            ["orange-200"] = "rgb(254 215 170)",
            ["orange-300"] = "rgb(253 186 116)",
            ["orange-400"] = "rgb(251 146 60)",
            ["orange-500"] = "rgb(249 115 22)",
            ["orange-600"] = "rgb(234 88 12)",
            ["orange-700"] = "rgb(194 65 12)",
            ["orange-800"] = "rgb(154 52 18)",
            ["orange-900"] = "rgb(124 45 18)",
            ["orange-950"] = "rgb(67 20 7)",
            ["amber-50"] = "rgb(255 251 235)",
            ["amber-100"] = "rgb(254 243 199)",
            ["amber-200"] = "rgb(253 230 138)",
            ["amber-300"] = "rgb(252 211 77)",
            ["amber-400"] = "rgb(251 191 36)",
            ["amber-500"] = "rgb(245 158 11)",
            ["amber-600"] = "rgb(217 119 6)",
            ["amber-700"] = "rgb(180 83 9)",
            ["amber-800"] = "rgb(146 64 14)",
            ["amber-900"] = "rgb(120 53 15)",
            ["amber-950"] = "rgb(69 26 3)",
            ["yellow-50"] = "rgb(254 252 232)",
            ["yellow-100"] = "rgb(254 249 195)",
            ["yellow-200"] = "rgb(254 240 138)",
            ["yellow-300"] = "rgb(253 224 71)",
            ["yellow-400"] = "rgb(250 204 21)",
            ["yellow-500"] = "rgb(234 179 8)",
            ["yellow-600"] = "rgb(202 138 4)",
            ["yellow-700"] = "rgb(161 98 7)",
            ["yellow-800"] = "rgb(133 77 14)",
            ["yellow-900"] = "rgb(113 63 18)",
            ["yellow-950"] = "rgb(66 32 6)",
            ["lime-50"] = "rgb(247 254 231)",
            ["lime-100"] = "rgb(236 252 203)",
            ["lime-200"] = "rgb(217 249 157)",
            ["lime-300"] = "rgb(190 242 100)",
            ["lime-400"] = "rgb(163 230 53)",
            ["lime-500"] = "rgb(132 204 22)",
            ["lime-600"] = "rgb(101 163 13)",
            ["lime-700"] = "rgb(77 124 15)",
            ["lime-800"] = "rgb(63 98 18)",
            ["lime-900"] = "rgb(54 83 20)",
            ["lime-950"] = "rgb(26 46 5)",
            ["green-50"] = "rgb(240 253 244)",
            ["green-100"] = "rgb(220 252 231)",
            ["green-200"] = "rgb(187 247 208)",
            ["green-300"] = "rgb(134 239 172)",
            ["green-400"] = "rgb(74 222 128)",
            ["green-500"] = "rgb(34 197 94)",
            ["green-600"] = "rgb(22 163 74)",
            ["green-700"] = "rgb(21 128 61)",
            ["green-800"] = "rgb(22 101 52)",
            ["green-900"] = "rgb(20 83 45)",
            ["green-950"] = "rgb(5 46 22)",
            ["emerald-50"] = "rgb(236 253 245)",
            ["emerald-100"] = "rgb(209 250 229)",
            ["emerald-200"] = "rgb(167 243 208)",
            ["emerald-300"] = "rgb(110 231 183)",
            ["emerald-400"] = "rgb(52 211 153)",
            ["emerald-500"] = "rgb(16 185 129)",
            ["emerald-600"] = "rgb(5 150 105)",
            ["emerald-700"] = "rgb(4 120 87)",
            ["emerald-800"] = "rgb(6 95 70)",
            ["emerald-900"] = "rgb(6 78 59)",
            ["emerald-950"] = "rgb(2 44 34)",
            ["teal-50"] = "rgb(240 253 250)",
            ["teal-100"] = "rgb(204 251 241)",
            ["teal-200"] = "rgb(153 246 228)",
            ["teal-300"] = "rgb(94 234 212)",
            ["teal-400"] = "rgb(45 212 191)",
            ["teal-500"] = "rgb(20 184 166)",
            ["teal-600"] = "rgb(13 148 136)",
            ["teal-700"] = "rgb(15 118 110)",
            ["teal-800"] = "rgb(17 94 89)",
            ["teal-900"] = "rgb(19 78 74)",
            ["teal-950"] = "rgb(4 47 46)",
            ["cyan-50"] = "rgb(236 254 255)",
            ["cyan-100"] = "rgb(207 250 254)",
            ["cyan-200"] = "rgb(165 243 252)",
            ["cyan-300"] = "rgb(103 232 249)",
            ["cyan-400"] = "rgb(34 211 238)",
            ["cyan-500"] = "rgb(6 182 212)",
            ["cyan-600"] = "rgb(8 145 178)",
            ["cyan-700"] = "rgb(14 116 144)",
            ["cyan-800"] = "rgb(21 94 117)",
            ["cyan-900"] = "rgb(22 78 99)",
            ["cyan-950"] = "rgb(8 51 68)",
            ["sky-50"] = "rgb(240 249 255)",
            ["sky-100"] = "rgb(224 242 254)",
            ["sky-200"] = "rgb(186 230 253)",
            ["sky-300"] = "rgb(125 211 252)",
            ["sky-400"] = "rgb(56 189 248)",
            ["sky-500"] = "rgb(14 165 233)",
            ["sky-600"] = "rgb(2 132 199)",
            ["sky-700"] = "rgb(3 105 161)",
            ["sky-800"] = "rgb(7 89 133)",
            ["sky-900"] = "rgb(12 74 110)",
            ["sky-950"] = "rgb(8 47 73)",
            ["blue-50"] = "rgb(239 246 255)",
            ["blue-100"] = "rgb(219 234 254)",
            ["blue-200"] = "rgb(191 219 254)",
            ["blue-300"] = "rgb(147 197 253)",
            ["blue-400"] = "rgb(96 165 250)",
            ["blue-500"] = "rgb(59 130 246)",
            ["blue-600"] = "rgb(37 99 235)",
            ["blue-700"] = "rgb(29 78 216)",
            ["blue-800"] = "rgb(30 64 175)",
            ["blue-900"] = "rgb(30 58 138)",
            ["blue-950"] = "rgb(23 37 84)",
            ["indigo-50"] = "rgb(238 242 255)",
            ["indigo-100"] = "rgb(224 231 255)",
            ["indigo-200"] = "rgb(199 210 254)",
            ["indigo-300"] = "rgb(165 180 252)",
            ["indigo-400"] = "rgb(129 140 248)",
            ["indigo-500"] = "rgb(99 102 241)",
            ["indigo-600"] = "rgb(79 70 229)",
            ["indigo-700"] = "rgb(67 56 202)",
            ["indigo-800"] = "rgb(55 48 163)",
            ["indigo-900"] = "rgb(49 46 129)",
            ["indigo-950"] = "rgb(30 27 75)",
            ["violet-50"] = "rgb(245 243 255)",
            ["violet-100"] = "rgb(237 233 254)",
            ["violet-200"] = "rgb(221 214 254)",
            ["violet-300"] = "rgb(196 181 253)",
            ["violet-400"] = "rgb(167 139 250)",
            ["violet-500"] = "rgb(139 92 246)",
            ["violet-600"] = "rgb(124 58 237)",
            ["violet-700"] = "rgb(109 40 217)",
            ["violet-800"] = "rgb(91 33 182)",
            ["violet-900"] = "rgb(76 29 149)",
            ["violet-950"] = "rgb(46 16 101)",
            ["purple-50"] = "rgb(250 245 255)",
            ["purple-100"] = "rgb(243 232 255)",
            ["purple-200"] = "rgb(233 213 255)",
            ["purple-300"] = "rgb(216 180 254)",
            ["purple-400"] = "rgb(192 132 252)",
            ["purple-500"] = "rgb(168 85 247)",
            ["purple-600"] = "rgb(147 51 234)",
            ["purple-700"] = "rgb(126 34 206)",
            ["purple-800"] = "rgb(107 33 168)",
            ["purple-900"] = "rgb(88 28 135)",
            ["purple-950"] = "rgb(59 7 100)",
            ["fuchsia-50"] = "rgb(253 244 255)",
            ["fuchsia-100"] = "rgb(250 232 255)",
            ["fuchsia-200"] = "rgb(245 208 254)",
            ["fuchsia-300"] = "rgb(240 171 252)",
            ["fuchsia-400"] = "rgb(232 121 249)",
            ["fuchsia-500"] = "rgb(217 70 239)",
            ["fuchsia-600"] = "rgb(192 38 211)",
            ["fuchsia-700"] = "rgb(162 28 175)",
            ["fuchsia-800"] = "rgb(134 25 143)",
            ["fuchsia-900"] = "rgb(112 26 117)",
            ["fuchsia-950"] = "rgb(74 4 78)",
            ["pink-50"] = "rgb(253 242 248)",
            ["pink-100"] = "rgb(252 231 243)",
            ["pink-200"] = "rgb(251 207 232)",
            ["pink-300"] = "rgb(249 168 212)",
            ["pink-400"] = "rgb(244 114 182)",
            ["pink-500"] = "rgb(236 72 153)",
            ["pink-600"] = "rgb(219 39 119)",
            ["pink-700"] = "rgb(190 24 93)",
            ["pink-800"] = "rgb(157 23 77)",
            ["pink-900"] = "rgb(131 24 67)",
            ["pink-950"] = "rgb(80 7 36)",
            ["rose-50"] = "rgb(255 241 242)",
            ["rose-100"] = "rgb(255 228 230)",
            ["rose-200"] = "rgb(254 205 211)",
            ["rose-300"] = "rgb(253 164 175)",
            ["rose-400"] = "rgb(251 113 133)",
            ["rose-500"] = "rgb(244 63 94)",
            ["rose-600"] = "rgb(225 29 72)",
            ["rose-700"] = "rgb(190 18 60)",
            ["rose-800"] = "rgb(159 18 57)",
            ["rose-900"] = "rgb(136 19 55)",
            ["rose-950"] = "rgb(76 5 25)"
        }
    };
    
    #endregion
    
    #region Class Helper Methods
    
    /// <summary>
    /// Get the total number of items in all SCSS class collections. 
    /// </summary>
    /// <returns></returns>
    public int GetClassCount()
    {
        var result = 0;

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null)
                continue;

            result += dictionaryReference.Count;
        }

        return result;
    }

    /// <summary>
    /// Get all ScssClass objects matching a given root class name (not user class name).
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public IEnumerable<ScssClass> GetAllByClassName(string className)
    {
        var result = new List<ScssClass>();
        var rootClassName = className;

        if (className.EndsWith(']') && className.Contains('['))
        {
            rootClassName = className[..className.IndexOf('[')];
        }
        
        rootClassName = rootClassName.Contains(':') ? rootClassName[(rootClassName.LastIndexOf(':') + 1)..] : rootClassName;

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null)
                continue;

            if (dictionaryReference.TryGetValue(rootClassName, out var scssClass))
            {
                result.Add(scssClass);
            }
        }

        return result;
    }
    
    /// <summary>
    /// Get the base (first) key of all collections; list has only unique prefixes (e.g. "bg-", "text-", etc.).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetClassPrefixesForRegex()
    {
        var result = new Dictionary<string,string>();
        var utilities = GetUtilityClassNamesForRegex().ToList();

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null || dictionaryReference.Count < 1)
                continue;

            var key = dictionaryReference.First().Key.Replace("-", "\\-");

            if (key.EndsWith("\\-", StringComparison.Ordinal) == false)
                continue;
            
            if (utilities.Contains(key) == false)
                result.TryAdd(key, string.Empty);
        }

        return result.Keys;
    }

    /// <summary>
    /// Get all base keys of all collections; list has only unique prefixes (e.g. "block", "inline", etc.).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetClassNonPrefixesForRegex()
    {
        var result = new Dictionary<string,string>();
        var utilities = GetUtilityClassNamesForRegex().ToList();

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null || dictionaryReference.Count < 1)
                continue;
            
            foreach (var item in dictionaryReference)
            {
                if (item.Key.EndsWith('-'))
                    continue;
                
                var key = item.Key.Replace("-", "\\-").Replace(".", "\\.");
                
                if (utilities.Contains(key) == false)
                    result.TryAdd(key, string.Empty);
            }
        }

        return result.Keys;
    }
    
    /// <summary>
    /// Gets a list of unique utility class names.
    /// Utility classes are of type ScssUtilityBaseClass.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetUtilityClassNamesForRegex()
    {
        var result = new Dictionary<string,string>();

        foreach (var property in typeof(ScssClassCollection).GetProperties())
        {
            if (property.PropertyType != typeof(ScssUtilityBaseClass))
                continue;
            
            var classesProperty = property.PropertyType.GetProperty("Classes");

            if (classesProperty == null || classesProperty.PropertyType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                continue;
            
            var propertyValue = property.GetValue(this);

            if (propertyValue is null)
                continue;
            
            var dictionaryReference = (IDictionary<string,ScssClass>?)classesProperty.GetValue(propertyValue);

            if (dictionaryReference is null || dictionaryReference.Count < 1)
                continue;

            foreach (var (key, value) in dictionaryReference)
            {
                if (value is { Value: "", ValueTypes: "" })
                    result.TryAdd(key.Replace("-", "\\-").Replace(".", "\\."), string.Empty);
            }
        }

        return result.Keys;
    }
    
    #endregion
}