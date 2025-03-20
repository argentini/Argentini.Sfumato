// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities;

public sealed class Library
{
    #region Utility Class Constants
    
    public Dictionary<string, string> Colors { get; set; } = new()
    {
        { "black", "#000" },
        { "white", "#fff" },
        { "red-50", "oklch(0.971 0.013 17.38)" },
        { "red-100", "oklch(0.936 0.032 17.717)" },
        { "red-200", "oklch(0.885 0.062 18.334)" },
        { "red-300", "oklch(0.808 0.114 19.571)" },
        { "red-400", "oklch(0.704 0.191 22.216)" },
        { "red-500", "oklch(0.637 0.237 25.331)" },
        { "red-600", "oklch(0.577 0.245 27.325)" },
        { "red-700", "oklch(0.505 0.213 27.518)" },
        { "red-800", "oklch(0.444 0.177 26.899)" },
        { "red-900", "oklch(0.396 0.141 25.723)" },
        { "red-950", "oklch(0.258 0.092 26.042)" },
        { "orange-50", "oklch(0.98 0.016 73.684)" },
        { "orange-100", "oklch(0.954 0.038 75.164)" },
        { "orange-200", "oklch(0.901 0.076 70.697)" },
        { "orange-300", "oklch(0.837 0.128 66.29)" },
        { "orange-400", "oklch(0.75 0.183 55.934)" },
        { "orange-500", "oklch(0.705 0.213 47.604)" },
        { "orange-600", "oklch(0.646 0.222 41.116)" },
        { "orange-700", "oklch(0.553 0.195 38.402)" },
        { "orange-800", "oklch(0.47 0.157 37.304)" },
        { "orange-900", "oklch(0.408 0.123 38.172)" },
        { "orange-950", "oklch(0.266 0.079 36.259)" },
        { "amber-50", "oklch(0.987 0.022 95.277)" },
        { "amber-100", "oklch(0.962 0.059 95.617)" },
        { "amber-200", "oklch(0.924 0.12 95.746)" },
        { "amber-300", "oklch(0.879 0.169 91.605)" },
        { "amber-400", "oklch(0.828 0.189 84.429)" },
        { "amber-500", "oklch(0.769 0.188 70.08)" },
        { "amber-600", "oklch(0.666 0.179 58.318)" },
        { "amber-700", "oklch(0.555 0.163 48.998)" },
        { "amber-800", "oklch(0.473 0.137 46.201)" },
        { "amber-900", "oklch(0.414 0.112 45.904)" },
        { "amber-950", "oklch(0.279 0.077 45.635)" },
        { "yellow-50", "oklch(0.987 0.026 102.212)" },
        { "yellow-100", "oklch(0.973 0.071 103.193)" },
        { "yellow-200", "oklch(0.945 0.129 101.54)" },
        { "yellow-300", "oklch(0.905 0.182 98.111)" },
        { "yellow-400", "oklch(0.852 0.199 91.936)" },
        { "yellow-500", "oklch(0.795 0.184 86.047)" },
        { "yellow-600", "oklch(0.681 0.162 75.834)" },
        { "yellow-700", "oklch(0.554 0.135 66.442)" },
        { "yellow-800", "oklch(0.476 0.114 61.907)" },
        { "yellow-900", "oklch(0.421 0.095 57.708)" },
        { "yellow-950", "oklch(0.286 0.066 53.813)" },
        { "lime-50", "oklch(0.986 0.031 120.757)" },
        { "lime-100", "oklch(0.967 0.067 122.328)" },
        { "lime-200", "oklch(0.938 0.127 124.321)" },
        { "lime-300", "oklch(0.897 0.196 126.665)" },
        { "lime-400", "oklch(0.841 0.238 128.85)" },
        { "lime-500", "oklch(0.768 0.233 130.85)" },
        { "lime-600", "oklch(0.648 0.2 131.684)" },
        { "lime-700", "oklch(0.532 0.157 131.589)" },
        { "lime-800", "oklch(0.453 0.124 130.933)" },
        { "lime-900", "oklch(0.405 0.101 131.063)" },
        { "lime-950", "oklch(0.274 0.072 132.109)" },
        { "green-50", "oklch(0.982 0.018 155.826)" },
        { "green-100", "oklch(0.962 0.044 156.743)" },
        { "green-200", "oklch(0.925 0.084 155.995)" },
        { "green-300", "oklch(0.871 0.15 154.449)" },
        { "green-400", "oklch(0.792 0.209 151.711)" },
        { "green-500", "oklch(0.723 0.219 149.579)" },
        { "green-600", "oklch(0.627 0.194 149.214)" },
        { "green-700", "oklch(0.527 0.154 150.069)" },
        { "green-800", "oklch(0.448 0.119 151.328)" },
        { "green-900", "oklch(0.393 0.095 152.535)" },
        { "green-950", "oklch(0.266 0.065 152.934)" },
        { "emerald-50", "oklch(0.979 0.021 166.113)" },
        { "emerald-100", "oklch(0.95 0.052 163.051)" },
        { "emerald-200", "oklch(0.905 0.093 164.15)" },
        { "emerald-300", "oklch(0.845 0.143 164.978)" },
        { "emerald-400", "oklch(0.765 0.177 163.223)" },
        { "emerald-500", "oklch(0.696 0.17 162.48)" },
        { "emerald-600", "oklch(0.596 0.145 163.225)" },
        { "emerald-700", "oklch(0.508 0.118 165.612)" },
        { "emerald-800", "oklch(0.432 0.095 166.913)" },
        { "emerald-900", "oklch(0.378 0.077 168.94)" },
        { "emerald-950", "oklch(0.262 0.051 172.552)" },
        { "teal-50", "oklch(0.984 0.014 180.72)" },
        { "teal-100", "oklch(0.953 0.051 180.801)" },
        { "teal-200", "oklch(0.91 0.096 180.426)" },
        { "teal-300", "oklch(0.855 0.138 181.071)" },
        { "teal-400", "oklch(0.777 0.152 181.912)" },
        { "teal-500", "oklch(0.704 0.14 182.503)" },
        { "teal-600", "oklch(0.6 0.118 184.704)" },
        { "teal-700", "oklch(0.511 0.096 186.391)" },
        { "teal-800", "oklch(0.437 0.078 188.216)" },
        { "teal-900", "oklch(0.386 0.063 188.416)" },
        { "teal-950", "oklch(0.277 0.046 192.524)" },
        { "cyan-50", "oklch(0.984 0.019 200.873)" },
        { "cyan-100", "oklch(0.956 0.045 203.388)" },
        { "cyan-200", "oklch(0.917 0.08 205.041)" },
        { "cyan-300", "oklch(0.865 0.127 207.078)" },
        { "cyan-400", "oklch(0.789 0.154 211.53)" },
        { "cyan-500", "oklch(0.715 0.143 215.221)" },
        { "cyan-600", "oklch(0.609 0.126 221.723)" },
        { "cyan-700", "oklch(0.52 0.105 223.128)" },
        { "cyan-800", "oklch(0.45 0.085 224.283)" },
        { "cyan-900", "oklch(0.398 0.07 227.392)" },
        { "cyan-950", "oklch(0.302 0.056 229.695)" },
        { "sky-50", "oklch(0.977 0.013 236.62)" },
        { "sky-100", "oklch(0.951 0.026 236.824)" },
        { "sky-200", "oklch(0.901 0.058 230.902)" },
        { "sky-300", "oklch(0.828 0.111 230.318)" },
        { "sky-400", "oklch(0.746 0.16 232.661)" },
        { "sky-500", "oklch(0.685 0.169 237.323)" },
        { "sky-600", "oklch(0.588 0.158 241.966)" },
        { "sky-700", "oklch(0.5 0.134 242.749)" },
        { "sky-800", "oklch(0.443 0.11 240.79)" },
        { "sky-900", "oklch(0.391 0.09 240.876)" },
        { "sky-950", "oklch(0.293 0.066 243.157)" },
        { "blue-50", "oklch(0.97 0.014 254.604)" },
        { "blue-100", "oklch(0.932 0.032 255.585)" },
        { "blue-200", "oklch(0.882 0.059 254.128)" },
        { "blue-300", "oklch(0.809 0.105 251.813)" },
        { "blue-400", "oklch(0.707 0.165 254.624)" },
        { "blue-500", "oklch(0.623 0.214 259.815)" },
        { "blue-600", "oklch(0.546 0.245 262.881)" },
        { "blue-700", "oklch(0.488 0.243 264.376)" },
        { "blue-800", "oklch(0.424 0.199 265.638)" },
        { "blue-900", "oklch(0.379 0.146 265.522)" },
        { "blue-950", "oklch(0.282 0.091 267.935)" },
        { "indigo-50", "oklch(0.962 0.018 272.314)" },
        { "indigo-100", "oklch(0.93 0.034 272.788)" },
        { "indigo-200", "oklch(0.87 0.065 274.039)" },
        { "indigo-300", "oklch(0.785 0.115 274.713)" },
        { "indigo-400", "oklch(0.673 0.182 276.935)" },
        { "indigo-500", "oklch(0.585 0.233 277.117)" },
        { "indigo-600", "oklch(0.511 0.262 276.966)" },
        { "indigo-700", "oklch(0.457 0.24 277.023)" },
        { "indigo-800", "oklch(0.398 0.195 277.366)" },
        { "indigo-900", "oklch(0.359 0.144 278.697)" },
        { "indigo-950", "oklch(0.257 0.09 281.288)" },
        { "violet-50", "oklch(0.969 0.016 293.756)" },
        { "violet-100", "oklch(0.943 0.029 294.588)" },
        { "violet-200", "oklch(0.894 0.057 293.283)" },
        { "violet-300", "oklch(0.811 0.111 293.571)" },
        { "violet-400", "oklch(0.702 0.183 293.541)" },
        { "violet-500", "oklch(0.606 0.25 292.717)" },
        { "violet-600", "oklch(0.541 0.281 293.009)" },
        { "violet-700", "oklch(0.491 0.27 292.581)" },
        { "violet-800", "oklch(0.432 0.232 292.759)" },
        { "violet-900", "oklch(0.38 0.189 293.745)" },
        { "violet-950", "oklch(0.283 0.141 291.089)" },
        { "purple-50", "oklch(0.977 0.014 308.299)" },
        { "purple-100", "oklch(0.946 0.033 307.174)" },
        { "purple-200", "oklch(0.902 0.063 306.703)" },
        { "purple-300", "oklch(0.827 0.119 306.383)" },
        { "purple-400", "oklch(0.714 0.203 305.504)" },
        { "purple-500", "oklch(0.627 0.265 303.9)" },
        { "purple-600", "oklch(0.558 0.288 302.321)" },
        { "purple-700", "oklch(0.496 0.265 301.924)" },
        { "purple-800", "oklch(0.438 0.218 303.724)" },
        { "purple-900", "oklch(0.381 0.176 304.987)" },
        { "purple-950", "oklch(0.291 0.149 302.717)" },
        { "fuchsia-50", "oklch(0.977 0.017 320.058)" },
        { "fuchsia-100", "oklch(0.952 0.037 318.852)" },
        { "fuchsia-200", "oklch(0.903 0.076 319.62)" },
        { "fuchsia-300", "oklch(0.833 0.145 321.434)" },
        { "fuchsia-400", "oklch(0.74 0.238 322.16)" },
        { "fuchsia-500", "oklch(0.667 0.295 322.15)" },
        { "fuchsia-600", "oklch(0.591 0.293 322.896)" },
        { "fuchsia-700", "oklch(0.518 0.253 323.949)" },
        { "fuchsia-800", "oklch(0.452 0.211 324.591)" },
        { "fuchsia-900", "oklch(0.401 0.17 325.612)" },
        { "fuchsia-950", "oklch(0.293 0.136 325.661)" },
        { "pink-50", "oklch(0.971 0.014 343.198)" },
        { "pink-100", "oklch(0.948 0.028 342.258)" },
        { "pink-200", "oklch(0.899 0.061 343.231)" },
        { "pink-300", "oklch(0.823 0.12 346.018)" },
        { "pink-400", "oklch(0.718 0.202 349.761)" },
        { "pink-500", "oklch(0.656 0.241 354.308)" },
        { "pink-600", "oklch(0.592 0.249 0.584)" },
        { "pink-700", "oklch(0.525 0.223 3.958)" },
        { "pink-800", "oklch(0.459 0.187 3.815)" },
        { "pink-900", "oklch(0.408 0.153 2.432)" },
        { "pink-950", "oklch(0.284 0.109 3.907)" },
        { "rose-50", "oklch(0.969 0.015 12.422)" },
        { "rose-100", "oklch(0.941 0.03 12.58)" },
        { "rose-200", "oklch(0.892 0.058 10.001)" },
        { "rose-300", "oklch(0.81 0.117 11.638)" },
        { "rose-400", "oklch(0.712 0.194 13.428)" },
        { "rose-500", "oklch(0.645 0.246 16.439)" },
        { "rose-600", "oklch(0.586 0.253 17.585)" },
        { "rose-700", "oklch(0.514 0.222 16.935)" },
        { "rose-800", "oklch(0.455 0.188 13.697)" },
        { "rose-900", "oklch(0.41 0.159 10.272)" },
        { "rose-950", "oklch(0.271 0.105 12.094)" },
        { "slate-50", "oklch(0.984 0.003 247.858)" },
        { "slate-100", "oklch(0.968 0.007 247.896)" },
        { "slate-200", "oklch(0.929 0.013 255.508)" },
        { "slate-300", "oklch(0.869 0.022 252.894)" },
        { "slate-400", "oklch(0.704 0.04 256.788)" },
        { "slate-500", "oklch(0.554 0.046 257.417)" },
        { "slate-600", "oklch(0.446 0.043 257.281)" },
        { "slate-700", "oklch(0.372 0.044 257.287)" },
        { "slate-800", "oklch(0.279 0.041 260.031)" },
        { "slate-900", "oklch(0.208 0.042 265.755)" },
        { "slate-950", "oklch(0.129 0.042 264.695)" },
        { "gray-50", "oklch(0.985 0.002 247.839)" },
        { "gray-100", "oklch(0.967 0.003 264.542)" },
        { "gray-200", "oklch(0.928 0.006 264.531)" },
        { "gray-300", "oklch(0.872 0.01 258.338)" },
        { "gray-400", "oklch(0.707 0.022 261.325)" },
        { "gray-500", "oklch(0.551 0.027 264.364)" },
        { "gray-600", "oklch(0.446 0.03 256.802)" },
        { "gray-700", "oklch(0.373 0.034 259.733)" },
        { "gray-800", "oklch(0.278 0.033 256.848)" },
        { "gray-900", "oklch(0.21 0.034 264.665)" },
        { "gray-950", "oklch(0.13 0.028 261.692)" },
        { "zinc-50", "oklch(0.985 0 0)" },
        { "zinc-100", "oklch(0.967 0.001 286.375)" },
        { "zinc-200", "oklch(0.92 0.004 286.32)" },
        { "zinc-300", "oklch(0.871 0.006 286.286)" },
        { "zinc-400", "oklch(0.705 0.015 286.067)" },
        { "zinc-500", "oklch(0.552 0.016 285.938)" },
        { "zinc-600", "oklch(0.442 0.017 285.786)" },
        { "zinc-700", "oklch(0.37 0.013 285.805)" },
        { "zinc-800", "oklch(0.274 0.006 286.033)" },
        { "zinc-900", "oklch(0.21 0.006 285.885)" },
        { "zinc-950", "oklch(0.141 0.005 285.823)" },
        { "neutral-50", "oklch(0.985 0 0)" },
        { "neutral-100", "oklch(0.97 0 0)" },
        { "neutral-200", "oklch(0.922 0 0)" },
        { "neutral-300", "oklch(0.87 0 0)" },
        { "neutral-400", "oklch(0.708 0 0)" },
        { "neutral-500", "oklch(0.556 0 0)" },
        { "neutral-600", "oklch(0.439 0 0)" },
        { "neutral-700", "oklch(0.371 0 0)" },
        { "neutral-800", "oklch(0.269 0 0)" },
        { "neutral-900", "oklch(0.205 0 0)" },
        { "neutral-950", "oklch(0.145 0 0)" },
        { "stone-50", "oklch(0.985 0.001 106.423)" },
        { "stone-100", "oklch(0.97 0.001 106.424)" },
        { "stone-200", "oklch(0.923 0.003 48.717)" },
        { "stone-300", "oklch(0.869 0.005 56.366)" },
        { "stone-400", "oklch(0.709 0.01 56.259)" },
        { "stone-500", "oklch(0.553 0.013 58.071)" },
        { "stone-600", "oklch(0.444 0.011 73.639)" },
        { "stone-700", "oklch(0.374 0.01 67.558)" },
        { "stone-800", "oklch(0.268 0.007 34.298)" },
        { "stone-900", "oklch(0.216 0.006 56.043)" },
        { "stone-950", "oklch(0.147 0.004 49.25)" }
    };

    private HashSet<string> ValidSafariCssPropertyNames { get; } = 
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

    private HashSet<string> ValidChromeCssPropertyNames { get; } =
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

    #endregion
    
    #region Runtime Properties
    
    public int FileAccessRetryMs => 5000;
    public int MaxConsoleWidth => GetMaxConsoleWidth();
	private static int GetMaxConsoleWidth()
    {
        try
        {
            return Console.WindowWidth > 120 ? 120 : Console.WindowWidth - 1;
        }
        catch
        {
            return 78;
        }
    }

    #endregion
    
    #region Scanner Collections
    
    public HashSet<string> CssPropertyNamesWithColons { get; set; } = [];
    public HashSet<string> ScannerClassNamePrefixes { get; set; } = [];

    #endregion
    
    public Library()
    {
        foreach (var propertyName in ValidSafariCssPropertyNames)
            CssPropertyNamesWithColons.Add($"{propertyName}:");
        
        foreach (var propertyName in ValidChromeCssPropertyNames)
            CssPropertyNamesWithColons.Add($"{propertyName}:");
        
        ScannerClassNamePrefixes.UnionWith(StaticClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
        ScannerClassNamePrefixes.UnionWith(NumberClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
        ScannerClassNamePrefixes.UnionWith(LengthClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
        ScannerClassNamePrefixes.UnionWith(FractionClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
        ScannerClassNamePrefixes.UnionWith(ColorClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
        ScannerClassNamePrefixes.UnionWith(DurationClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
        ScannerClassNamePrefixes.UnionWith(AngleClasses.Keys.Where(key => key.EndsWith('(') == false && key.EndsWith('[') == false));
    }
    
    #region Utility Class Definitions

    public Dictionary<string, ClassDefinition> StaticClasses { get; set; } = new()
    {
        {
            "antialiased", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           -webkit-font-smoothing: antialiased;
                           -moz-osx-font-smoothing: grayscale;
                           """
            }
        },
        {
            "block", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           display: block;
                           """
            }
        },
        {
            "flex", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           display: flex;
                           """
            }
        },
        {
            "subpixel-antialiased", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           -webkit-font-smoothing: auto;
                           -moz-osx-font-smoothing: auto;
                           """
            }
        },
        {
            "leading-none", new ClassDefinition
            {
                IsSimpleUtility = true,
                SelectorSort = 1,
                Template = """
                           line-height: 1;
                           """
            }
        },
        {
            "text-xs", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-xs);
                           line-height: var(--text-xs--line-height);
                           """,
                ModifierTemplate = """
                           font-size: var(--text-xs);
                           line-height: calc(var(--spacing) * {1});
                           """
            }
        },
        {
            "text-sm", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-sm);
                           line-height: var(--text-sm--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-sm);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-base", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-base);
                           line-height: var(--text-base--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-base);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-lg", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-lg);
                           line-height: var(--text-lg--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-lg);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-xl);
                           line-height: var(--text-xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-2xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-2xl);
                           line-height: var(--text-2xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-2xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-3xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-3xl);
                           line-height: var(--text-3xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-3xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-4xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-4xl);
                           line-height: var(--text-4xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-4xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-5xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-5xl);
                           line-height: var(--text-5xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-5xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-6xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-6xl);
                           line-height: var(--text-6xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-6xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-7xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-7xl);
                           line-height: var(--text-7xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-7xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-8xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-8xl);
                           line-height: var(--text-8xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-8xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-9xl", new ClassDefinition
            {
                IsSimpleUtility = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var(--text-9xl);
                           line-height: var(--text-9xl--line-height);
                           """,
                ModifierTemplate = """
                                   font-size: var(--text-9xl);
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-left", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           text-align: left;
                           """
            }
        },
        {
            "text-center", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           text-align: center;
                           """
            }
        },
        {
            "text-right", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           text-align: right;
                           """
            }
        },
        {
            "text-justify", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           text-align: justify;
                           """
            }
        },
        {
            "text-start", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           text-align: start;
                           """
            }
        },
        {
            "text-end", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """
                           text-align: end;
                           """
            }
        },
        {
            "top-auto", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """top: auto;"""
            }
        },
        {
            "top-px", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """top: 1px;"""
            }
        },
        {
            "-top-px", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """top: -1px;"""
            }
        },
        {
            "top-full", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """top: {0};""",
                Value = """100%"""
            }
        },
        {
            "-top-full", new ClassDefinition
            {
                IsSimpleUtility = true,
                Template = """top: {0};""",
                Value = """-100%"""
            }
        },
    };

    public Dictionary<string, ClassDefinition> NumberClasses { get; set; } = new()
    {
        {
            "top-", new ClassDefinition
            {
                UsesNumber = true,
                Template = """top: calc(var(--spacing) * {0});"""
            }
        },
        {
            "-top-", new ClassDefinition
            {
                UsesLength = true,
                Template = """top: calc(var(--spacing) * -{0});"""
            }
        },
        {
            "leading-", new ClassDefinition
            {
                UsesNumber = true,
                SelectorSort = 1,
                Template = """line-height: calc(var(--spacing) * {0});"""
            }
        },
    };

    public Dictionary<string, ClassDefinition> LengthClasses { get; set; } = new()
    {
        {
            "leading-[", new ClassDefinition
            {
                UsesLength = true,
                Template = """
                           line-height: {0};
                           """
            }
        },
        {
            "leading-(", new ClassDefinition
            {
                UsesLength = true,
                Template = """
                           line-height: var({0});
                           """
            }
        },
        {
            "text-[", new ClassDefinition
            {
                UsesLength = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: {0};
                           line-height: calc(var(--spacing) * {1});
                           """,
                ModifierTemplate = """
                                   font-size: {0};
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "text-(", new ClassDefinition
            {
                UsesLength = true,
                UsesSlashModifier = true,
                Template = """
                           font-size: var({0});
                           line-height: calc(var(--spacing) * {1});
                           """,
                ModifierTemplate = """
                                   font-size: var({0});
                                   line-height: calc(var(--spacing) * {1});
                                   """
            }
        },
        {
            "top-[", new ClassDefinition
            {
                UsesLength = true,
                Template = """top: {0};"""
            }
        },
        {
            "top-(", new ClassDefinition
            {
                UsesLength = true,
                Template = """top: var({0});"""
            }
        },
        {
            "-top-(", new ClassDefinition
            {
                UsesLength = true,
                Template = """top: calc(var({0}) * -1);"""
            }
        },
    };

    public Dictionary<string, ClassDefinition> FractionClasses { get; set; } = new()
    {
        {
            "top-", new ClassDefinition
            {
                UsesFraction = true,
                Template = """top: calc({0} * 100%);"""
            }
        },
        {
            "-top-", new ClassDefinition
            {
                UsesFraction = true,
                Template = """top: calc({0} * -100%);"""
            }
        },
    };

    public Dictionary<string, ClassDefinition> ColorClasses { get; set; } = new()
    {
        {
            "bg-", new ClassDefinition
            {
                UsesColor = true,
                UsesSlashModifier = true,
                Template = """
                           background-color: {0};
                           """,
                ModifierTemplate = """
                                   background-color: {0};
                                   """
            }
        },
        {
            "text-", new ClassDefinition
            {
                UsesColor = true,
                UsesSlashModifier = true,
                Template = """
                           color: {0};
                           """,
                ModifierTemplate = """
                                   color: {0};
                                   """
            }
        },
    };

    public Dictionary<string, ClassDefinition> DurationClasses { get; set; } = new()
    {
    };

    public Dictionary<string, ClassDefinition> AngleClasses { get; set; } = new()
    {
    };
    
    #endregion
}