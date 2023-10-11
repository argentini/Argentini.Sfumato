namespace Argentini.Sfumato;

public static class SfumatoScss
{
	#region Constants

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
	
	public static Dictionary<string, string> MediaQueryPrefixes => new ()
	{
		{ "dark", "@media (prefers-color-scheme: dark) {" },
		{ "portrait", "@media (orientation: portrait) {" },
		{ "landscape", "@media (orientation: landscape) {" },
		{ "print", "@media print {" },
		{ "zero", "@include sf-media($from: zero) {" },
		{ "phab", "@include sf-media($from: phab) {" },
		{ "tabp", "@include sf-media($from: tabp) {" },
		{ "tabl", "@include sf-media($from: tabl) {" },
		{ "note", "@include sf-media($from: note) {" },
		{ "desk", "@include sf-media($from: desk) {" },
		{ "elas", "@include sf-media($from: elas) {" }
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
	public static async Task<string> GetCoreScssAsync(SfumatoAppState appState, StringBuilder diagnosticOutput)
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
		
		initScss = initScss.Replace("#{zero-vw}", $"{appState.Settings.FontSizeViewportUnits?.Zero}vw");
		initScss = initScss.Replace("#{phab-vw}", $"{appState.Settings.FontSizeViewportUnits?.Phab}vw");
		initScss = initScss.Replace("#{tabp-vw}", $"{appState.Settings.FontSizeViewportUnits?.Tabp}vw");
		initScss = initScss.Replace("#{tabl-vw}", $"{appState.Settings.FontSizeViewportUnits?.Tabl}vw");
		initScss = initScss.Replace("#{note-vw}", $"{appState.Settings.FontSizeViewportUnits?.Note}vw");
		initScss = initScss.Replace("#{desk-vw}", $"{appState.Settings.FontSizeViewportUnits?.Desk}vw");
		initScss = initScss.Replace("#{elas-vw}", $"{appState.Settings.FontSizeViewportUnits?.Elas}vw");
		
		sb.Append(initScss);

		diagnosticOutput.Append($"Prepared SCSS Core for output injection in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

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
	public static async Task<string> GetSharedScssAsync(SfumatoAppState appState, StringBuilder diagnosticOutput)
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

		diagnosticOutput.Append($"Prepared shared SCSS for output injection in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		var result = sb.ToString();
		
		appState.StringBuilderPool.Return(sb);

		return result;
	}
	
	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// </summary>
	/// <param name="scss"></param>
	/// <param name="appState"></param>
	/// <param name="releaseMode"></param>
	/// <returns>Byte length of generated CSS file</returns>
	public static async Task<long> TranspileScss(StringBuilder scss, SfumatoAppState appState)
	{
		var sb = appState.StringBuilderPool.Get();

		try
		{
			var arguments = new List<string>();

			if (File.Exists(Path.Combine(appState.Settings.CssOutputPath, "sfumato.css.map")))
				File.Delete(Path.Combine(appState.Settings.CssOutputPath, "sfumato.css.map"));

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
			arguments.Add(Path.Combine(appState.Settings.CssOutputPath, "sfumato.css"));
			
			var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap(appState.SassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

			await cmd.ExecuteAsync();

			var fileInfo = new FileInfo(Path.Combine(appState.Settings.CssOutputPath, "sfumato.css"));
			
			appState.StringBuilderPool.Return(sb);
			
			return fileInfo.Length;
		}

		catch (Exception e)
		{
			sb.AppendLine($"=> ERROR: {e.Message.Trim()}");
			sb.AppendLine(string.Empty);
			sb.AppendLine(e.StackTrace?.Trim());
			sb.AppendLine(string.Empty);

			Console.WriteLine(sb.ToString());

			appState.StringBuilderPool.Return(sb);

			Environment.Exit(1);
		}

		return -1;
	}
	
    /// <summary>
    /// Transpile a single SCSS file into CSS, in-place.
    /// </summary>
    /// <param name="scssFilePath">File system path to the scss input file (e.g. "/scss/application.scss")</param>
    /// <param name="appState">When true compacts the generated CSS</param>
    /// <returns>Byte length of generated CSS file</returns>
    public static async Task<long> TranspileSingleScss(string scssFilePath, SfumatoAppState appState)
    {
	    var scss = appState.StringBuilderPool.Get();
	    var sb = appState.StringBuilderPool.Get();

	    scss.Append(appState.ScssSharedInjectable);
	    
		if (string.IsNullOrEmpty(scssFilePath) || scssFilePath.EndsWith(".scss", StringComparison.OrdinalIgnoreCase) == false)
		{
			Console.WriteLine($"=> ERROR: invalid SCSS file path: {scssFilePath}");
			return -1;
        }

        var outputPath = string.Concat(scssFilePath.AsSpan(0, scssFilePath.Length - 4), "css");

        try
        {
            var arguments = new List<string>();

            if (File.Exists(Path.GetFullPath(outputPath) + ".map"))
                File.Delete(Path.GetFullPath(outputPath) + ".map");

            arguments.Add($"--style={(appState.ReleaseMode ? "compressed" : "expanded")}");
            arguments.Add("--no-source-map");
            arguments.Add("--stdin");
            arguments.Add($"{outputPath}");

            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            while (cancellationToken.IsCancellationRequested == false)
            {
                try
                {
	                await using (var fs = new FileStream(scssFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    
	                using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
	                    scss.Append(await sr.ReadToEndAsync(cancellationToken.Token));
                    }

                    cancellationToken.Cancel();
                }

                catch
                {
                    await Task.Delay(10, cancellationToken.Token);
                }
            }

            var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap("sass")
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

            var fileInfo = new FileInfo(outputPath);
		
            return fileInfo.Length;
        }

        catch (Exception ex)
        {
            sb.AppendLine($"=> ERROR: {ex.Message.Trim()}");
            sb.AppendLine(string.Empty);
            sb.AppendLine(ex.StackTrace?.Trim());
            sb.AppendLine(string.Empty);
            
            Console.WriteLine(sb.ToString());
            
            appState.StringBuilderPool.Return(sb);
            appState.StringBuilderPool.Return(scss);

            return -1;
        }
    }
    
    /// <summary>
    /// Escape a CSS class name to be used in a CSS selector.
    /// </summary>
    /// <param name="className"></param>
    /// <param name="appState"></param>
    /// <returns></returns>
    public static string EscapeCssClassName(this string className, ObjectPool<StringBuilder> pool)
    {
	    if (string.IsNullOrEmpty(className))
		    return className;

	    var sb = pool.Get();

	    for (var i = 0; i < className.Length; i++)
	    {
		    var c = className[i];
            
		    if ((i == 0 && char.IsDigit(c)) || (!char.IsLetterOrDigit(c) && c != '-' && c != '_'))
			    sb.Append($"\\{c}");
		    else
			    sb.Append(c);
	    }

	    var result = sb.ToString();
		
	    pool.Return(sb);

	    return result;
    }
    
    /// <summary>
    /// Get the value type of the user class name (e.g. "length:...", "color:...", etc.)
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public static string GetUserClassValueType(this string className)
    {
	    if (className.EndsWith(']') == false || className.Contains('[') == false)
		    return string.Empty;

	    var segments = className[className.LastIndexOf('[')..].Trim().TrimStart('[').TrimEnd(']').Split(':', StringSplitOptions.RemoveEmptyEntries);
	    var value = segments[0];

	    // Passed a value type prefix (e.g. text-[color:red])

	    if (segments.Length > 1)
	    {
		    if (ArbitraryValueTypes.Contains(segments[0]))
			    return segments[0];
		    
		    return string.Empty;
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
	    
	    if (value.StartsWith("url(", StringComparison.Ordinal) && value.EndsWith(')') || (Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
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
	    
	    var values = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

	    if (values.Length != 2)
		    return string.Empty;

	    if (double.TryParse(values[0], out _) && double.TryParse(values[1], out _))
		    return "ratio";

	    #endregion
	    
	    return string.Empty;
    }
    
    /// <summary>
    /// Get the value of the user class name (e.g. "length:value", "color:value", etc.)
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public static string GetUserClassValue(this string className)
    {
	    if (className.EndsWith(']') == false || className.Contains('[') == false)
		    return string.Empty;

	    var result = string.Empty;
	    var segments = className[className.IndexOf('[')..].TrimStart('[').TrimEnd(']').Split(':', StringSplitOptions.RemoveEmptyEntries);
	    
	    if (segments.Length == 2)
		    result = segments[1];
	    else
		    result = segments[0];

	    if (result.StartsWith("--"))
		    result = $"var({result})";

	    return result;
    }
}