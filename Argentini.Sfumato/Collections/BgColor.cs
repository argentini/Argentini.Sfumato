namespace Argentini.Sfumato.Collections;

public sealed class BgColor
{
    // ReSharper disable once UnusedMember.Global
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["bg-"] = new ScssClass
        {
            Value = "",
            ValueTypes = "color",
            Template = "background-color: {value};"
        },
        ["bg-inherit"] = new ScssClass
        {
            Value = "inherit",
            Template = "background-color: {value};"
        },
        ["bg-transparent"] = new ScssClass
        {
            Value = "transparent",
            Template = "background-color: {value};"
        },
        ["bg-black"] = new ScssClass
        {
            Value = "rgb(0 0 0)",
            Template = "background-color: {value};"
        },
        ["bg-white"] = new ScssClass
        {
            Value = "rgb(255 255 255)",
            Template = "background-color: {value};"
        },
        ["bg-slate-50"] = new ScssClass
        {
            Value = "rgb(248 250 252)",
            Template = "background-color: {value};"
        },
        ["bg-slate-100"] = new ScssClass
        {
            Value = "rgb(241 245 249)",
            Template = "background-color: {value};"
        },
        ["bg-slate-200"] = new ScssClass
        {
            Value = "rgb(226 232 240)",
            Template = "background-color: {value};"
        },
        ["bg-slate-300"] = new ScssClass
        {
            Value = "rgb(203 213 225)",
            Template = "background-color: {value};"
        },
        ["bg-slate-400"] = new ScssClass
        {
            Value = "rgb(148 163 184)",
            Template = "background-color: {value};"
        },
        ["bg-slate-500"] = new ScssClass
        {
            Value = "rgb(100 116 139)",
            Template = "background-color: {value};"
        },
        ["bg-slate-600"] = new ScssClass
        {
            Value = "rgb(71 85 105)",
            Template = "background-color: {value};"
        },
        ["bg-slate-700"] = new ScssClass
        {
            Value = "rgb(51 65 85)",
            Template = "background-color: {value};"
        },
        ["bg-slate-800"] = new ScssClass
        {
            Value = "rgb(30 41 59)",
            Template = "background-color: {value};"
        },
        ["bg-slate-900"] = new ScssClass
        {
            Value = "rgb(15 23 42)",
            Template = "background-color: {value};"
        },
        ["bg-slate-950"] = new ScssClass
        {
            Value = "rgb(2 6 23)",
            Template = "background-color: {value};"
        },
        ["bg-gray-50"] = new ScssClass
        {
            Value = "rgb(249 250 251)",
            Template = "background-color: {value};"
        },
        ["bg-gray-100"] = new ScssClass
        {
            Value = "rgb(243 244 246)",
            Template = "background-color: {value};"
        },
        ["bg-gray-200"] = new ScssClass
        {
            Value = "rgb(229 231 235)",
            Template = "background-color: {value};"
        },
        ["bg-gray-300"] = new ScssClass
        {
            Value = "rgb(209 213 219)",
            Template = "background-color: {value};"
        },
        ["bg-gray-400"] = new ScssClass
        {
            Value = "rgb(156 163 175)",
            Template = "background-color: {value};"
        },
        ["bg-gray-500"] = new ScssClass
        {
            Value = "rgb(107 114 128)",
            Template = "background-color: {value};"
        },
        ["bg-gray-600"] = new ScssClass
        {
            Value = "rgb(75 85 99)",
            Template = "background-color: {value};"
        },
        ["bg-gray-700"] = new ScssClass
        {
            Value = "rgb(55 65 81)",
            Template = "background-color: {value};"
        },
        ["bg-gray-800"] = new ScssClass
        {
            Value = "rgb(31 41 55)",
            Template = "background-color: {value};"
        },
        ["bg-gray-900"] = new ScssClass
        {
            Value = "rgb(17 24 39)",
            Template = "background-color: {value};"
        },
        ["bg-gray-950"] = new ScssClass
        {
            Value = "rgb(3 7 18)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-50"] = new ScssClass
        {
            Value = "rgb(250 250 250)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-100"] = new ScssClass
        {
            Value = "rgb(244 244 245)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-200"] = new ScssClass
        {
            Value = "rgb(228 228 231)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-300"] = new ScssClass
        {
            Value = "rgb(212 212 216)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-400"] = new ScssClass
        {
            Value = "rgb(161 161 170)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-500"] = new ScssClass
        {
            Value = "rgb(113 113 122)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-600"] = new ScssClass
        {
            Value = "rgb(82 82 91)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-700"] = new ScssClass
        {
            Value = "rgb(63 63 70)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-800"] = new ScssClass
        {
            Value = "rgb(39 39 42)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-900"] = new ScssClass
        {
            Value = "rgb(24 24 27)",
            Template = "background-color: {value};"
        },
        ["bg-zinc-950"] = new ScssClass
        {
            Value = "rgb(9 9 11)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-50"] = new ScssClass
        {
            Value = "rgb(250 250 250)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-100"] = new ScssClass
        {
            Value = "rgb(245 245 245)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-200"] = new ScssClass
        {
            Value = "rgb(229 229 229)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-300"] = new ScssClass
        {
            Value = "rgb(212 212 212)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-400"] = new ScssClass
        {
            Value = "rgb(163 163 163)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-500"] = new ScssClass
        {
            Value = "rgb(115 115 115)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-600"] = new ScssClass
        {
            Value = "rgb(82 82 82)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-700"] = new ScssClass
        {
            Value = "rgb(64 64 64)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-800"] = new ScssClass
        {
            Value = "rgb(38 38 38)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-900"] = new ScssClass
        {
            Value = "rgb(23 23 23)",
            Template = "background-color: {value};"
        },
        ["bg-neutral-950"] = new ScssClass
        {
            Value = "rgb(10 10 10)",
            Template = "background-color: {value};"
        },
        ["bg-stone-50"] = new ScssClass
        {
            Value = "rgb(250 250 249)",
            Template = "background-color: {value};"
        },
        ["bg-stone-100"] = new ScssClass
        {
            Value = "rgb(245 245 244)",
            Template = "background-color: {value};"
        },
        ["bg-stone-200"] = new ScssClass
        {
            Value = "rgb(231 229 228)",
            Template = "background-color: {value};"
        },
        ["bg-stone-300"] = new ScssClass
        {
            Value = "rgb(214 211 209)",
            Template = "background-color: {value};"
        },
        ["bg-stone-400"] = new ScssClass
        {
            Value = "rgb(168 162 158)",
            Template = "background-color: {value};"
        },
        ["bg-stone-500"] = new ScssClass
        {
            Value = "rgb(120 113 108)",
            Template = "background-color: {value};"
        },
        ["bg-stone-600"] = new ScssClass
        {
            Value = "rgb(87 83 78)",
            Template = "background-color: {value};"
        },
        ["bg-stone-700"] = new ScssClass
        {
            Value = "rgb(68 64 60)",
            Template = "background-color: {value};"
        },
        ["bg-stone-800"] = new ScssClass
        {
            Value = "rgb(41 37 36)",
            Template = "background-color: {value};"
        },
        ["bg-stone-900"] = new ScssClass
        {
            Value = "rgb(28 25 23)",
            Template = "background-color: {value};"
        },
        ["bg-stone-950"] = new ScssClass
        {
            Value = "rgb(12 10 9)",
            Template = "background-color: {value};"
        },
        ["bg-red-50"] = new ScssClass
        {
            Value = "rgb(254 242 242)",
            Template = "background-color: {value};"
        },
        ["bg-red-100"] = new ScssClass
        {
            Value = "rgb(254 226 226)",
            Template = "background-color: {value};"
        },
        ["bg-red-200"] = new ScssClass
        {
            Value = "rgb(254 202 202)",
            Template = "background-color: {value};"
        },
        ["bg-red-300"] = new ScssClass
        {
            Value = "rgb(252 165 165)",
            Template = "background-color: {value};"
        },
        ["bg-red-400"] = new ScssClass
        {
            Value = "rgb(248 113 113)",
            Template = "background-color: {value};"
        },
        ["bg-red-500"] = new ScssClass
        {
            Value = "rgb(239 68 68)",
            Template = "background-color: {value};"
        },
        ["bg-red-600"] = new ScssClass
        {
            Value = "rgb(220 38 38)",
            Template = "background-color: {value};"
        },
        ["bg-red-700"] = new ScssClass
        {
            Value = "rgb(185 28 28)",
            Template = "background-color: {value};"
        },
        ["bg-red-800"] = new ScssClass
        {
            Value = "rgb(153 27 27)",
            Template = "background-color: {value};"
        },
        ["bg-red-900"] = new ScssClass
        {
            Value = "rgb(127 29 29)",
            Template = "background-color: {value};"
        },
        ["bg-red-950"] = new ScssClass
        {
            Value = "rgb(69 10 10)",
            Template = "background-color: {value};"
        },
        ["bg-orange-50"] = new ScssClass
        {
            Value = "rgb(255 247 237)",
            Template = "background-color: {value};"
        },
        ["bg-orange-100"] = new ScssClass
        {
            Value = "rgb(255 237 213)",
            Template = "background-color: {value};"
        },
        ["bg-orange-200"] = new ScssClass
        {
            Value = "rgb(254 215 170)",
            Template = "background-color: {value};"
        },
        ["bg-orange-300"] = new ScssClass
        {
            Value = "rgb(253 186 116)",
            Template = "background-color: {value};"
        },
        ["bg-orange-400"] = new ScssClass
        {
            Value = "rgb(251 146 60)",
            Template = "background-color: {value};"
        },
        ["bg-orange-500"] = new ScssClass
        {
            Value = "rgb(249 115 22)",
            Template = "background-color: {value};"
        },
        ["bg-orange-600"] = new ScssClass
        {
            Value = "rgb(234 88 12)",
            Template = "background-color: {value};"
        },
        ["bg-orange-700"] = new ScssClass
        {
            Value = "rgb(194 65 12)",
            Template = "background-color: {value};"
        },
        ["bg-orange-800"] = new ScssClass
        {
            Value = "rgb(154 52 18)",
            Template = "background-color: {value};"
        },
        ["bg-orange-900"] = new ScssClass
        {
            Value = "rgb(124 45 18)",
            Template = "background-color: {value};"
        },
        ["bg-orange-950"] = new ScssClass
        {
            Value = "rgb(67 20 7)",
            Template = "background-color: {value};"
        },
        ["bg-amber-50"] = new ScssClass
        {
            Value = "rgb(255 251 235)",
            Template = "background-color: {value};"
        },
        ["bg-amber-100"] = new ScssClass
        {
            Value = "rgb(254 243 199)",
            Template = "background-color: {value};"
        },
        ["bg-amber-200"] = new ScssClass
        {
            Value = "rgb(253 230 138)",
            Template = "background-color: {value};"
        },
        ["bg-amber-300"] = new ScssClass
        {
            Value = "rgb(252 211 77)",
            Template = "background-color: {value};"
        },
        ["bg-amber-400"] = new ScssClass
        {
            Value = "rgb(251 191 36)",
            Template = "background-color: {value};"
        },
        ["bg-amber-500"] = new ScssClass
        {
            Value = "rgb(245 158 11)",
            Template = "background-color: {value};"
        },
        ["bg-amber-600"] = new ScssClass
        {
            Value = "rgb(217 119 6)",
            Template = "background-color: {value};"
        },
        ["bg-amber-700"] = new ScssClass
        {
            Value = "rgb(180 83 9)",
            Template = "background-color: {value};"
        },
        ["bg-amber-800"] = new ScssClass
        {
            Value = "rgb(146 64 14)",
            Template = "background-color: {value};"
        },
        ["bg-amber-900"] = new ScssClass
        {
            Value = "rgb(120 53 15)",
            Template = "background-color: {value};"
        },
        ["bg-amber-950"] = new ScssClass
        {
            Value = "rgb(69 26 3)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-50"] = new ScssClass
        {
            Value = "rgb(254 252 232)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-100"] = new ScssClass
        {
            Value = "rgb(254 249 195)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-200"] = new ScssClass
        {
            Value = "rgb(254 240 138)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-300"] = new ScssClass
        {
            Value = "rgb(253 224 71)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-400"] = new ScssClass
        {
            Value = "rgb(250 204 21)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-500"] = new ScssClass
        {
            Value = "rgb(234 179 8)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-600"] = new ScssClass
        {
            Value = "rgb(202 138 4)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-700"] = new ScssClass
        {
            Value = "rgb(161 98 7)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-800"] = new ScssClass
        {
            Value = "rgb(133 77 14)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-900"] = new ScssClass
        {
            Value = "rgb(113 63 18)",
            Template = "background-color: {value};"
        },
        ["bg-yellow-950"] = new ScssClass
        {
            Value = "rgb(66 32 6)",
            Template = "background-color: {value};"
        },
        ["bg-lime-50"] = new ScssClass
        {
            Value = "rgb(247 254 231)",
            Template = "background-color: {value};"
        },
        ["bg-lime-100"] = new ScssClass
        {
            Value = "rgb(236 252 203)",
            Template = "background-color: {value};"
        },
        ["bg-lime-200"] = new ScssClass
        {
            Value = "rgb(217 249 157)",
            Template = "background-color: {value};"
        },
        ["bg-lime-300"] = new ScssClass
        {
            Value = "rgb(190 242 100)",
            Template = "background-color: {value};"
        },
        ["bg-lime-400"] = new ScssClass
        {
            Value = "rgb(163 230 53)",
            Template = "background-color: {value};"
        },
        ["bg-lime-500"] = new ScssClass
        {
            Value = "rgb(132 204 22)",
            Template = "background-color: {value};"
        },
        ["bg-lime-600"] = new ScssClass
        {
            Value = "rgb(101 163 13)",
            Template = "background-color: {value};"
        },
        ["bg-lime-700"] = new ScssClass
        {
            Value = "rgb(77 124 15)",
            Template = "background-color: {value};"
        },
        ["bg-lime-800"] = new ScssClass
        {
            Value = "rgb(63 98 18)",
            Template = "background-color: {value};"
        },
        ["bg-lime-900"] = new ScssClass
        {
            Value = "rgb(54 83 20)",
            Template = "background-color: {value};"
        },
        ["bg-lime-950"] = new ScssClass
        {
            Value = "rgb(26 46 5)",
            Template = "background-color: {value};"
        },
        ["bg-green-50"] = new ScssClass
        {
            Value = "rgb(240 253 244)",
            Template = "background-color: {value};"
        },
        ["bg-green-100"] = new ScssClass
        {
            Value = "rgb(220 252 231)",
            Template = "background-color: {value};"
        },
        ["bg-green-200"] = new ScssClass
        {
            Value = "rgb(187 247 208)",
            Template = "background-color: {value};"
        },
        ["bg-green-300"] = new ScssClass
        {
            Value = "rgb(134 239 172)",
            Template = "background-color: {value};"
        },
        ["bg-green-400"] = new ScssClass
        {
            Value = "rgb(74 222 128)",
            Template = "background-color: {value};"
        },
        ["bg-green-500"] = new ScssClass
        {
            Value = "rgb(34 197 94)",
            Template = "background-color: {value};"
        },
        ["bg-green-600"] = new ScssClass
        {
            Value = "rgb(22 163 74)",
            Template = "background-color: {value};"
        },
        ["bg-green-700"] = new ScssClass
        {
            Value = "rgb(21 128 61)",
            Template = "background-color: {value};"
        },
        ["bg-green-800"] = new ScssClass
        {
            Value = "rgb(22 101 52)",
            Template = "background-color: {value};"
        },
        ["bg-green-900"] = new ScssClass
        {
            Value = "rgb(20 83 45)",
            Template = "background-color: {value};"
        },
        ["bg-green-950"] = new ScssClass
        {
            Value = "rgb(5 46 22)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-50"] = new ScssClass
        {
            Value = "rgb(236 253 245)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-100"] = new ScssClass
        {
            Value = "rgb(209 250 229)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-200"] = new ScssClass
        {
            Value = "rgb(167 243 208)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-300"] = new ScssClass
        {
            Value = "rgb(110 231 183)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-400"] = new ScssClass
        {
            Value = "rgb(52 211 153)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-500"] = new ScssClass
        {
            Value = "rgb(16 185 129)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-600"] = new ScssClass
        {
            Value = "rgb(5 150 105)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-700"] = new ScssClass
        {
            Value = "rgb(4 120 87)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-800"] = new ScssClass
        {
            Value = "rgb(6 95 70)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-900"] = new ScssClass
        {
            Value = "rgb(6 78 59)",
            Template = "background-color: {value};"
        },
        ["bg-emerald-950"] = new ScssClass
        {
            Value = "rgb(2 44 34)",
            Template = "background-color: {value};"
        },
        ["bg-teal-50"] = new ScssClass
        {
            Value = "rgb(240 253 250)",
            Template = "background-color: {value};"
        },
        ["bg-teal-100"] = new ScssClass
        {
            Value = "rgb(204 251 241)",
            Template = "background-color: {value};"
        },
        ["bg-teal-200"] = new ScssClass
        {
            Value = "rgb(153 246 228)",
            Template = "background-color: {value};"
        },
        ["bg-teal-300"] = new ScssClass
        {
            Value = "rgb(94 234 212)",
            Template = "background-color: {value};"
        },
        ["bg-teal-400"] = new ScssClass
        {
            Value = "rgb(45 212 191)",
            Template = "background-color: {value};"
        },
        ["bg-teal-500"] = new ScssClass
        {
            Value = "rgb(20 184 166)",
            Template = "background-color: {value};"
        },
        ["bg-teal-600"] = new ScssClass
        {
            Value = "rgb(13 148 136)",
            Template = "background-color: {value};"
        },
        ["bg-teal-700"] = new ScssClass
        {
            Value = "rgb(15 118 110)",
            Template = "background-color: {value};"
        },
        ["bg-teal-800"] = new ScssClass
        {
            Value = "rgb(17 94 89)",
            Template = "background-color: {value};"
        },
        ["bg-teal-900"] = new ScssClass
        {
            Value = "rgb(19 78 74)",
            Template = "background-color: {value};"
        },
        ["bg-teal-950"] = new ScssClass
        {
            Value = "rgb(4 47 46)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-50"] = new ScssClass
        {
            Value = "rgb(236 254 255)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-100"] = new ScssClass
        {
            Value = "rgb(207 250 254)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-200"] = new ScssClass
        {
            Value = "rgb(165 243 252)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-300"] = new ScssClass
        {
            Value = "rgb(103 232 249)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-400"] = new ScssClass
        {
            Value = "rgb(34 211 238)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-500"] = new ScssClass
        {
            Value = "rgb(6 182 212)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-600"] = new ScssClass
        {
            Value = "rgb(8 145 178)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-700"] = new ScssClass
        {
            Value = "rgb(14 116 144)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-800"] = new ScssClass
        {
            Value = "rgb(21 94 117)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-900"] = new ScssClass
        {
            Value = "rgb(22 78 99)",
            Template = "background-color: {value};"
        },
        ["bg-cyan-950"] = new ScssClass
        {
            Value = "rgb(8 51 68)",
            Template = "background-color: {value};"
        },
        ["bg-sky-50"] = new ScssClass
        {
            Value = "rgb(240 249 255)",
            Template = "background-color: {value};"
        },
        ["bg-sky-100"] = new ScssClass
        {
            Value = "rgb(224 242 254)",
            Template = "background-color: {value};"
        },
        ["bg-sky-200"] = new ScssClass
        {
            Value = "rgb(186 230 253)",
            Template = "background-color: {value};"
        },
        ["bg-sky-300"] = new ScssClass
        {
            Value = "rgb(125 211 252)",
            Template = "background-color: {value};"
        },
        ["bg-sky-400"] = new ScssClass
        {
            Value = "rgb(56 189 248)",
            Template = "background-color: {value};"
        },
        ["bg-sky-500"] = new ScssClass
        {
            Value = "rgb(14 165 233)",
            Template = "background-color: {value};"
        },
        ["bg-sky-600"] = new ScssClass
        {
            Value = "rgb(2 132 199)",
            Template = "background-color: {value};"
        },
        ["bg-sky-700"] = new ScssClass
        {
            Value = "rgb(3 105 161)",
            Template = "background-color: {value};"
        },
        ["bg-sky-800"] = new ScssClass
        {
            Value = "rgb(7 89 133)",
            Template = "background-color: {value};"
        },
        ["bg-sky-900"] = new ScssClass
        {
            Value = "rgb(12 74 110)",
            Template = "background-color: {value};"
        },
        ["bg-sky-950"] = new ScssClass
        {
            Value = "rgb(8 47 73)",
            Template = "background-color: {value};"
        },
        ["bg-blue-50"] = new ScssClass
        {
            Value = "rgb(239 246 255)",
            Template = "background-color: {value};"
        },
        ["bg-blue-100"] = new ScssClass
        {
            Value = "rgb(219 234 254)",
            Template = "background-color: {value};"
        },
        ["bg-blue-200"] = new ScssClass
        {
            Value = "rgb(191 219 254)",
            Template = "background-color: {value};"
        },
        ["bg-blue-300"] = new ScssClass
        {
            Value = "rgb(147 197 253)",
            Template = "background-color: {value};"
        },
        ["bg-blue-400"] = new ScssClass
        {
            Value = "rgb(96 165 250)",
            Template = "background-color: {value};"
        },
        ["bg-blue-500"] = new ScssClass
        {
            Value = "rgb(59 130 246)",
            Template = "background-color: {value};"
        },
        ["bg-blue-600"] = new ScssClass
        {
            Value = "rgb(37 99 235)",
            Template = "background-color: {value};"
        },
        ["bg-blue-700"] = new ScssClass
        {
            Value = "rgb(29 78 216)",
            Template = "background-color: {value};"
        },
        ["bg-blue-800"] = new ScssClass
        {
            Value = "rgb(30 64 175)",
            Template = "background-color: {value};"
        },
        ["bg-blue-900"] = new ScssClass
        {
            Value = "rgb(30 58 138)",
            Template = "background-color: {value};"
        },
        ["bg-blue-950"] = new ScssClass
        {
            Value = "rgb(23 37 84)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-50"] = new ScssClass
        {
            Value = "rgb(238 242 255)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-100"] = new ScssClass
        {
            Value = "rgb(224 231 255)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-200"] = new ScssClass
        {
            Value = "rgb(199 210 254)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-300"] = new ScssClass
        {
            Value = "rgb(165 180 252)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-400"] = new ScssClass
        {
            Value = "rgb(129 140 248)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-500"] = new ScssClass
        {
            Value = "rgb(99 102 241)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-600"] = new ScssClass
        {
            Value = "rgb(79 70 229)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-700"] = new ScssClass
        {
            Value = "rgb(67 56 202)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-800"] = new ScssClass
        {
            Value = "rgb(55 48 163)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-900"] = new ScssClass
        {
            Value = "rgb(49 46 129)",
            Template = "background-color: {value};"
        },
        ["bg-indigo-950"] = new ScssClass
        {
            Value = "rgb(30 27 75)",
            Template = "background-color: {value};"
        },
        ["bg-violet-50"] = new ScssClass
        {
            Value = "rgb(245 243 255)",
            Template = "background-color: {value};"
        },
        ["bg-violet-100"] = new ScssClass
        {
            Value = "rgb(237 233 254)",
            Template = "background-color: {value};"
        },
        ["bg-violet-200"] = new ScssClass
        {
            Value = "rgb(221 214 254)",
            Template = "background-color: {value};"
        },
        ["bg-violet-300"] = new ScssClass
        {
            Value = "rgb(196 181 253)",
            Template = "background-color: {value};"
        },
        ["bg-violet-400"] = new ScssClass
        {
            Value = "rgb(167 139 250)",
            Template = "background-color: {value};"
        },
        ["bg-violet-500"] = new ScssClass
        {
            Value = "rgb(139 92 246)",
            Template = "background-color: {value};"
        },
        ["bg-violet-600"] = new ScssClass
        {
            Value = "rgb(124 58 237)",
            Template = "background-color: {value};"
        },
        ["bg-violet-700"] = new ScssClass
        {
            Value = "rgb(109 40 217)",
            Template = "background-color: {value};"
        },
        ["bg-violet-800"] = new ScssClass
        {
            Value = "rgb(91 33 182)",
            Template = "background-color: {value};"
        },
        ["bg-violet-900"] = new ScssClass
        {
            Value = "rgb(76 29 149)",
            Template = "background-color: {value};"
        },
        ["bg-violet-950"] = new ScssClass
        {
            Value = "rgb(46 16 101)",
            Template = "background-color: {value};"
        },
        ["bg-purple-50"] = new ScssClass
        {
            Value = "rgb(250 245 255)",
            Template = "background-color: {value};"
        },
        ["bg-purple-100"] = new ScssClass
        {
            Value = "rgb(243 232 255)",
            Template = "background-color: {value};"
        },
        ["bg-purple-200"] = new ScssClass
        {
            Value = "rgb(233 213 255)",
            Template = "background-color: {value};"
        },
        ["bg-purple-300"] = new ScssClass
        {
            Value = "rgb(216 180 254)",
            Template = "background-color: {value};"
        },
        ["bg-purple-400"] = new ScssClass
        {
            Value = "rgb(192 132 252)",
            Template = "background-color: {value};"
        },
        ["bg-purple-500"] = new ScssClass
        {
            Value = "rgb(168 85 247)",
            Template = "background-color: {value};"
        },
        ["bg-purple-600"] = new ScssClass
        {
            Value = "rgb(147 51 234)",
            Template = "background-color: {value};"
        },
        ["bg-purple-700"] = new ScssClass
        {
            Value = "rgb(126 34 206)",
            Template = "background-color: {value};"
        },
        ["bg-purple-800"] = new ScssClass
        {
            Value = "rgb(107 33 168)",
            Template = "background-color: {value};"
        },
        ["bg-purple-900"] = new ScssClass
        {
            Value = "rgb(88 28 135)",
            Template = "background-color: {value};"
        },
        ["bg-purple-950"] = new ScssClass
        {
            Value = "rgb(59 7 100)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-50"] = new ScssClass
        {
            Value = "rgb(253 244 255)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-100"] = new ScssClass
        {
            Value = "rgb(250 232 255)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-200"] = new ScssClass
        {
            Value = "rgb(245 208 254)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-300"] = new ScssClass
        {
            Value = "rgb(240 171 252)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-400"] = new ScssClass
        {
            Value = "rgb(232 121 249)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-500"] = new ScssClass
        {
            Value = "rgb(217 70 239)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-600"] = new ScssClass
        {
            Value = "rgb(192 38 211)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-700"] = new ScssClass
        {
            Value = "rgb(162 28 175)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-800"] = new ScssClass
        {
            Value = "rgb(134 25 143)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-900"] = new ScssClass
        {
            Value = "rgb(112 26 117)",
            Template = "background-color: {value};"
        },
        ["bg-fuchsia-950"] = new ScssClass
        {
            Value = "rgb(74 4 78)",
            Template = "background-color: {value};"
        },
        ["bg-pink-50"] = new ScssClass
        {
            Value = "rgb(253 242 248)",
            Template = "background-color: {value};"
        },
        ["bg-pink-100"] = new ScssClass
        {
            Value = "rgb(252 231 243)",
            Template = "background-color: {value};"
        },
        ["bg-pink-200"] = new ScssClass
        {
            Value = "rgb(251 207 232)",
            Template = "background-color: {value};"
        },
        ["bg-pink-300"] = new ScssClass
        {
            Value = "rgb(249 168 212)",
            Template = "background-color: {value};"
        },
        ["bg-pink-400"] = new ScssClass
        {
            Value = "rgb(244 114 182)",
            Template = "background-color: {value};"
        },
        ["bg-pink-500"] = new ScssClass
        {
            Value = "rgb(236 72 153)",
            Template = "background-color: {value};"
        },
        ["bg-pink-600"] = new ScssClass
        {
            Value = "rgb(219 39 119)",
            Template = "background-color: {value};"
        },
        ["bg-pink-700"] = new ScssClass
        {
            Value = "rgb(190 24 93)",
            Template = "background-color: {value};"
        },
        ["bg-pink-800"] = new ScssClass
        {
            Value = "rgb(157 23 77)",
            Template = "background-color: {value};"
        },
        ["bg-pink-900"] = new ScssClass
        {
            Value = "rgb(131 24 67)",
            Template = "background-color: {value};"
        },
        ["bg-pink-950"] = new ScssClass
        {
            Value = "rgb(80 7 36)",
            Template = "background-color: {value};"
        },
        ["bg-rose-50"] = new ScssClass
        {
            Value = "rgb(255 241 242)",
            Template = "background-color: {value};"
        },
        ["bg-rose-100"] = new ScssClass
        {
            Value = "rgb(255 228 230)",
            Template = "background-color: {value};"
        },
        ["bg-rose-200"] = new ScssClass
        {
            Value = "rgb(254 205 211)",
            Template = "background-color: {value};"
        },
        ["bg-rose-300"] = new ScssClass
        {
            Value = "rgb(253 164 175)",
            Template = "background-color: {value};"
        },
        ["bg-rose-400"] = new ScssClass
        {
            Value = "rgb(251 113 133)",
            Template = "background-color: {value};"
        },
        ["bg-rose-500"] = new ScssClass
        {
            Value = "rgb(244 63 94)",
            Template = "background-color: {value};"
        },
        ["bg-rose-600"] = new ScssClass
        {
            Value = "rgb(225 29 72)",
            Template = "background-color: {value};"
        },
        ["bg-rose-700"] = new ScssClass
        {
            Value = "rgb(190 18 60)",
            Template = "background-color: {value};"
        },
        ["bg-rose-800"] = new ScssClass
        {
            Value = "rgb(159 18 57)",
            Template = "background-color: {value};"
        },
        ["bg-rose-900"] = new ScssClass
        {
            Value = "rgb(136 19 55)",
            Template = "background-color: {value};"
        },
        ["bg-rose-950"] = new ScssClass
        {
            Value = "rgb(76 5 25)",
            Template = "background-color: {value};"
        }
    };
}