namespace Argentini.Sfumato.Collections;

public sealed class TextColor
{
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["text-"] = new ScssClass
        {
            Value = "",
            ValueType = "color",
            Template = "color: {value};"
        },
        ["text-inherit"] = new ScssClass
        {
            Value = "inherit",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-transparent"] = new ScssClass
        {
            Value = "transparent",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-black"] = new ScssClass
        {
            Value = "rgb(0 0 0)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-white"] = new ScssClass
        {
            Value = "rgb(255 255 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-50"] = new ScssClass
        {
            Value = "rgb(248 250 252)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-100"] = new ScssClass
        {
            Value = "rgb(241 245 249)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-200"] = new ScssClass
        {
            Value = "rgb(226 232 240)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-300"] = new ScssClass
        {
            Value = "rgb(203 213 225)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-400"] = new ScssClass
        {
            Value = "rgb(148 163 184)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-500"] = new ScssClass
        {
            Value = "rgb(100 116 139)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-600"] = new ScssClass
        {
            Value = "rgb(71 85 105)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-700"] = new ScssClass
        {
            Value = "rgb(51 65 85)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-800"] = new ScssClass
        {
            Value = "rgb(30 41 59)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-900"] = new ScssClass
        {
            Value = "rgb(15 23 42)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-slate-950"] = new ScssClass
        {
            Value = "rgb(2 6 23)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-50"] = new ScssClass
        {
            Value = "rgb(249 250 251)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-100"] = new ScssClass
        {
            Value = "rgb(243 244 246)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-200"] = new ScssClass
        {
            Value = "rgb(229 231 235)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-300"] = new ScssClass
        {
            Value = "rgb(209 213 219)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-400"] = new ScssClass
        {
            Value = "rgb(156 163 175)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-500"] = new ScssClass
        {
            Value = "rgb(107 114 128)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-600"] = new ScssClass
        {
            Value = "rgb(75 85 99)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-700"] = new ScssClass
        {
            Value = "rgb(55 65 81)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-800"] = new ScssClass
        {
            Value = "rgb(31 41 55)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-900"] = new ScssClass
        {
            Value = "rgb(17 24 39)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-gray-950"] = new ScssClass
        {
            Value = "rgb(3 7 18)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-50"] = new ScssClass
        {
            Value = "rgb(250 250 250)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-100"] = new ScssClass
        {
            Value = "rgb(244 244 245)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-200"] = new ScssClass
        {
            Value = "rgb(228 228 231)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-300"] = new ScssClass
        {
            Value = "rgb(212 212 216)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-400"] = new ScssClass
        {
            Value = "rgb(161 161 170)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-500"] = new ScssClass
        {
            Value = "rgb(113 113 122)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-600"] = new ScssClass
        {
            Value = "rgb(82 82 91)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-700"] = new ScssClass
        {
            Value = "rgb(63 63 70)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-800"] = new ScssClass
        {
            Value = "rgb(39 39 42)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-900"] = new ScssClass
        {
            Value = "rgb(24 24 27)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-zinc-950"] = new ScssClass
        {
            Value = "rgb(9 9 11)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-50"] = new ScssClass
        {
            Value = "rgb(250 250 250)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-100"] = new ScssClass
        {
            Value = "rgb(245 245 245)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-200"] = new ScssClass
        {
            Value = "rgb(229 229 229)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-300"] = new ScssClass
        {
            Value = "rgb(212 212 212)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-400"] = new ScssClass
        {
            Value = "rgb(163 163 163)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-500"] = new ScssClass
        {
            Value = "rgb(115 115 115)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-600"] = new ScssClass
        {
            Value = "rgb(82 82 82)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-700"] = new ScssClass
        {
            Value = "rgb(64 64 64)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-800"] = new ScssClass
        {
            Value = "rgb(38 38 38)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-900"] = new ScssClass
        {
            Value = "rgb(23 23 23)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-neutral-950"] = new ScssClass
        {
            Value = "rgb(10 10 10)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-50"] = new ScssClass
        {
            Value = "rgb(250 250 249)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-100"] = new ScssClass
        {
            Value = "rgb(245 245 244)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-200"] = new ScssClass
        {
            Value = "rgb(231 229 228)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-300"] = new ScssClass
        {
            Value = "rgb(214 211 209)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-400"] = new ScssClass
        {
            Value = "rgb(168 162 158)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-500"] = new ScssClass
        {
            Value = "rgb(120 113 108)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-600"] = new ScssClass
        {
            Value = "rgb(87 83 78)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-700"] = new ScssClass
        {
            Value = "rgb(68 64 60)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-800"] = new ScssClass
        {
            Value = "rgb(41 37 36)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-900"] = new ScssClass
        {
            Value = "rgb(28 25 23)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-stone-950"] = new ScssClass
        {
            Value = "rgb(12 10 9)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-50"] = new ScssClass
        {
            Value = "rgb(254 242 242)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-100"] = new ScssClass
        {
            Value = "rgb(254 226 226)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-200"] = new ScssClass
        {
            Value = "rgb(254 202 202)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-300"] = new ScssClass
        {
            Value = "rgb(252 165 165)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-400"] = new ScssClass
        {
            Value = "rgb(248 113 113)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-500"] = new ScssClass
        {
            Value = "rgb(239 68 68)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-600"] = new ScssClass
        {
            Value = "rgb(220 38 38)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-700"] = new ScssClass
        {
            Value = "rgb(185 28 28)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-800"] = new ScssClass
        {
            Value = "rgb(153 27 27)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-900"] = new ScssClass
        {
            Value = "rgb(127 29 29)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-red-950"] = new ScssClass
        {
            Value = "rgb(69 10 10)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-50"] = new ScssClass
        {
            Value = "rgb(255 247 237)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-100"] = new ScssClass
        {
            Value = "rgb(255 237 213)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-200"] = new ScssClass
        {
            Value = "rgb(254 215 170)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-300"] = new ScssClass
        {
            Value = "rgb(253 186 116)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-400"] = new ScssClass
        {
            Value = "rgb(251 146 60)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-500"] = new ScssClass
        {
            Value = "rgb(249 115 22)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-600"] = new ScssClass
        {
            Value = "rgb(234 88 12)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-700"] = new ScssClass
        {
            Value = "rgb(194 65 12)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-800"] = new ScssClass
        {
            Value = "rgb(154 52 18)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-900"] = new ScssClass
        {
            Value = "rgb(124 45 18)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-orange-950"] = new ScssClass
        {
            Value = "rgb(67 20 7)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-50"] = new ScssClass
        {
            Value = "rgb(255 251 235)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-100"] = new ScssClass
        {
            Value = "rgb(254 243 199)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-200"] = new ScssClass
        {
            Value = "rgb(253 230 138)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-300"] = new ScssClass
        {
            Value = "rgb(252 211 77)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-400"] = new ScssClass
        {
            Value = "rgb(251 191 36)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-500"] = new ScssClass
        {
            Value = "rgb(245 158 11)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-600"] = new ScssClass
        {
            Value = "rgb(217 119 6)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-700"] = new ScssClass
        {
            Value = "rgb(180 83 9)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-800"] = new ScssClass
        {
            Value = "rgb(146 64 14)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-900"] = new ScssClass
        {
            Value = "rgb(120 53 15)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-amber-950"] = new ScssClass
        {
            Value = "rgb(69 26 3)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-50"] = new ScssClass
        {
            Value = "rgb(254 252 232)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-100"] = new ScssClass
        {
            Value = "rgb(254 249 195)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-200"] = new ScssClass
        {
            Value = "rgb(254 240 138)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-300"] = new ScssClass
        {
            Value = "rgb(253 224 71)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-400"] = new ScssClass
        {
            Value = "rgb(250 204 21)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-500"] = new ScssClass
        {
            Value = "rgb(234 179 8)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-600"] = new ScssClass
        {
            Value = "rgb(202 138 4)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-700"] = new ScssClass
        {
            Value = "rgb(161 98 7)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-800"] = new ScssClass
        {
            Value = "rgb(133 77 14)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-900"] = new ScssClass
        {
            Value = "rgb(113 63 18)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-yellow-950"] = new ScssClass
        {
            Value = "rgb(66 32 6)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-50"] = new ScssClass
        {
            Value = "rgb(247 254 231)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-100"] = new ScssClass
        {
            Value = "rgb(236 252 203)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-200"] = new ScssClass
        {
            Value = "rgb(217 249 157)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-300"] = new ScssClass
        {
            Value = "rgb(190 242 100)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-400"] = new ScssClass
        {
            Value = "rgb(163 230 53)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-500"] = new ScssClass
        {
            Value = "rgb(132 204 22)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-600"] = new ScssClass
        {
            Value = "rgb(101 163 13)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-700"] = new ScssClass
        {
            Value = "rgb(77 124 15)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-800"] = new ScssClass
        {
            Value = "rgb(63 98 18)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-900"] = new ScssClass
        {
            Value = "rgb(54 83 20)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-lime-950"] = new ScssClass
        {
            Value = "rgb(26 46 5)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-50"] = new ScssClass
        {
            Value = "rgb(240 253 244)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-100"] = new ScssClass
        {
            Value = "rgb(220 252 231)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-200"] = new ScssClass
        {
            Value = "rgb(187 247 208)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-300"] = new ScssClass
        {
            Value = "rgb(134 239 172)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-400"] = new ScssClass
        {
            Value = "rgb(74 222 128)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-500"] = new ScssClass
        {
            Value = "rgb(34 197 94)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-600"] = new ScssClass
        {
            Value = "rgb(22 163 74)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-700"] = new ScssClass
        {
            Value = "rgb(21 128 61)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-800"] = new ScssClass
        {
            Value = "rgb(22 101 52)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-900"] = new ScssClass
        {
            Value = "rgb(20 83 45)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-green-950"] = new ScssClass
        {
            Value = "rgb(5 46 22)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-50"] = new ScssClass
        {
            Value = "rgb(236 253 245)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-100"] = new ScssClass
        {
            Value = "rgb(209 250 229)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-200"] = new ScssClass
        {
            Value = "rgb(167 243 208)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-300"] = new ScssClass
        {
            Value = "rgb(110 231 183)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-400"] = new ScssClass
        {
            Value = "rgb(52 211 153)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-500"] = new ScssClass
        {
            Value = "rgb(16 185 129)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-600"] = new ScssClass
        {
            Value = "rgb(5 150 105)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-700"] = new ScssClass
        {
            Value = "rgb(4 120 87)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-800"] = new ScssClass
        {
            Value = "rgb(6 95 70)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-900"] = new ScssClass
        {
            Value = "rgb(6 78 59)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-emerald-950"] = new ScssClass
        {
            Value = "rgb(2 44 34)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-50"] = new ScssClass
        {
            Value = "rgb(240 253 250)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-100"] = new ScssClass
        {
            Value = "rgb(204 251 241)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-200"] = new ScssClass
        {
            Value = "rgb(153 246 228)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-300"] = new ScssClass
        {
            Value = "rgb(94 234 212)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-400"] = new ScssClass
        {
            Value = "rgb(45 212 191)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-500"] = new ScssClass
        {
            Value = "rgb(20 184 166)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-600"] = new ScssClass
        {
            Value = "rgb(13 148 136)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-700"] = new ScssClass
        {
            Value = "rgb(15 118 110)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-800"] = new ScssClass
        {
            Value = "rgb(17 94 89)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-900"] = new ScssClass
        {
            Value = "rgb(19 78 74)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-teal-950"] = new ScssClass
        {
            Value = "rgb(4 47 46)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-50"] = new ScssClass
        {
            Value = "rgb(236 254 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-100"] = new ScssClass
        {
            Value = "rgb(207 250 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-200"] = new ScssClass
        {
            Value = "rgb(165 243 252)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-300"] = new ScssClass
        {
            Value = "rgb(103 232 249)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-400"] = new ScssClass
        {
            Value = "rgb(34 211 238)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-500"] = new ScssClass
        {
            Value = "rgb(6 182 212)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-600"] = new ScssClass
        {
            Value = "rgb(8 145 178)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-700"] = new ScssClass
        {
            Value = "rgb(14 116 144)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-800"] = new ScssClass
        {
            Value = "rgb(21 94 117)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-900"] = new ScssClass
        {
            Value = "rgb(22 78 99)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-cyan-950"] = new ScssClass
        {
            Value = "rgb(8 51 68)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-50"] = new ScssClass
        {
            Value = "rgb(240 249 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-100"] = new ScssClass
        {
            Value = "rgb(224 242 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-200"] = new ScssClass
        {
            Value = "rgb(186 230 253)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-300"] = new ScssClass
        {
            Value = "rgb(125 211 252)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-400"] = new ScssClass
        {
            Value = "rgb(56 189 248)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-500"] = new ScssClass
        {
            Value = "rgb(14 165 233)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-600"] = new ScssClass
        {
            Value = "rgb(2 132 199)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-700"] = new ScssClass
        {
            Value = "rgb(3 105 161)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-800"] = new ScssClass
        {
            Value = "rgb(7 89 133)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-900"] = new ScssClass
        {
            Value = "rgb(12 74 110)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-sky-950"] = new ScssClass
        {
            Value = "rgb(8 47 73)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-50"] = new ScssClass
        {
            Value = "rgb(239 246 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-100"] = new ScssClass
        {
            Value = "rgb(219 234 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-200"] = new ScssClass
        {
            Value = "rgb(191 219 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-300"] = new ScssClass
        {
            Value = "rgb(147 197 253)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-400"] = new ScssClass
        {
            Value = "rgb(96 165 250)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-500"] = new ScssClass
        {
            Value = "rgb(59 130 246)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-600"] = new ScssClass
        {
            Value = "rgb(37 99 235)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-700"] = new ScssClass
        {
            Value = "rgb(29 78 216)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-800"] = new ScssClass
        {
            Value = "rgb(30 64 175)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-900"] = new ScssClass
        {
            Value = "rgb(30 58 138)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-blue-950"] = new ScssClass
        {
            Value = "rgb(23 37 84)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-50"] = new ScssClass
        {
            Value = "rgb(238 242 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-100"] = new ScssClass
        {
            Value = "rgb(224 231 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-200"] = new ScssClass
        {
            Value = "rgb(199 210 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-300"] = new ScssClass
        {
            Value = "rgb(165 180 252)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-400"] = new ScssClass
        {
            Value = "rgb(129 140 248)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-500"] = new ScssClass
        {
            Value = "rgb(99 102 241)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-600"] = new ScssClass
        {
            Value = "rgb(79 70 229)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-700"] = new ScssClass
        {
            Value = "rgb(67 56 202)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-800"] = new ScssClass
        {
            Value = "rgb(55 48 163)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-900"] = new ScssClass
        {
            Value = "rgb(49 46 129)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-indigo-950"] = new ScssClass
        {
            Value = "rgb(30 27 75)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-50"] = new ScssClass
        {
            Value = "rgb(245 243 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-100"] = new ScssClass
        {
            Value = "rgb(237 233 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-200"] = new ScssClass
        {
            Value = "rgb(221 214 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-300"] = new ScssClass
        {
            Value = "rgb(196 181 253)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-400"] = new ScssClass
        {
            Value = "rgb(167 139 250)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-500"] = new ScssClass
        {
            Value = "rgb(139 92 246)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-600"] = new ScssClass
        {
            Value = "rgb(124 58 237)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-700"] = new ScssClass
        {
            Value = "rgb(109 40 217)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-800"] = new ScssClass
        {
            Value = "rgb(91 33 182)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-900"] = new ScssClass
        {
            Value = "rgb(76 29 149)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-violet-950"] = new ScssClass
        {
            Value = "rgb(46 16 101)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-50"] = new ScssClass
        {
            Value = "rgb(250 245 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-100"] = new ScssClass
        {
            Value = "rgb(243 232 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-200"] = new ScssClass
        {
            Value = "rgb(233 213 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-300"] = new ScssClass
        {
            Value = "rgb(216 180 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-400"] = new ScssClass
        {
            Value = "rgb(192 132 252)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-500"] = new ScssClass
        {
            Value = "rgb(168 85 247)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-600"] = new ScssClass
        {
            Value = "rgb(147 51 234)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-700"] = new ScssClass
        {
            Value = "rgb(126 34 206)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-800"] = new ScssClass
        {
            Value = "rgb(107 33 168)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-900"] = new ScssClass
        {
            Value = "rgb(88 28 135)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-purple-950"] = new ScssClass
        {
            Value = "rgb(59 7 100)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-50"] = new ScssClass
        {
            Value = "rgb(253 244 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-100"] = new ScssClass
        {
            Value = "rgb(250 232 255)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-200"] = new ScssClass
        {
            Value = "rgb(245 208 254)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-300"] = new ScssClass
        {
            Value = "rgb(240 171 252)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-400"] = new ScssClass
        {
            Value = "rgb(232 121 249)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-500"] = new ScssClass
        {
            Value = "rgb(217 70 239)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-600"] = new ScssClass
        {
            Value = "rgb(192 38 211)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-700"] = new ScssClass
        {
            Value = "rgb(162 28 175)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-800"] = new ScssClass
        {
            Value = "rgb(134 25 143)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-900"] = new ScssClass
        {
            Value = "rgb(112 26 117)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-fuchsia-950"] = new ScssClass
        {
            Value = "rgb(74 4 78)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-50"] = new ScssClass
        {
            Value = "rgb(253 242 248)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-100"] = new ScssClass
        {
            Value = "rgb(252 231 243)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-200"] = new ScssClass
        {
            Value = "rgb(251 207 232)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-300"] = new ScssClass
        {
            Value = "rgb(249 168 212)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-400"] = new ScssClass
        {
            Value = "rgb(244 114 182)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-500"] = new ScssClass
        {
            Value = "rgb(236 72 153)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-600"] = new ScssClass
        {
            Value = "rgb(219 39 119)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-700"] = new ScssClass
        {
            Value = "rgb(190 24 93)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-800"] = new ScssClass
        {
            Value = "rgb(157 23 77)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-900"] = new ScssClass
        {
            Value = "rgb(131 24 67)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-pink-950"] = new ScssClass
        {
            Value = "rgb(80 7 36)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-50"] = new ScssClass
        {
            Value = "rgb(255 241 242)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-100"] = new ScssClass
        {
            Value = "rgb(255 228 230)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-200"] = new ScssClass
        {
            Value = "rgb(254 205 211)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-300"] = new ScssClass
        {
            Value = "rgb(253 164 175)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-400"] = new ScssClass
        {
            Value = "rgb(251 113 133)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-500"] = new ScssClass
        {
            Value = "rgb(244 63 94)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-600"] = new ScssClass
        {
            Value = "rgb(225 29 72)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-700"] = new ScssClass
        {
            Value = "rgb(190 18 60)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-800"] = new ScssClass
        {
            Value = "rgb(159 18 57)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-900"] = new ScssClass
        {
            Value = "rgb(136 19 55)",
            ValueType = "color",
            Template = "background-color: {value};"
        },
        ["text-rose-950"] = new ScssClass
        {
            Value = "rgb(76 5 25)",
            ValueType = "color",
            Template = "background-color: {value};"
        }
    };
}