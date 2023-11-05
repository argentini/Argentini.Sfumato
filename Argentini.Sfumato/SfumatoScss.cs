namespace Argentini.Sfumato;

public static class SfumatoScss
{
	#region Core Shared SCSS

	/// <summary>
	/// Get all Sfumato base SCSS include files (e.g. browser reset, static element styles) and return as a single string.
	/// </summary>
	/// <param name="appState"></param>
	/// <param name="diagnosticOutput"></param>
	/// <returns></returns>
	public static async Task<string> GetBaseScssAsync(SfumatoAppState appState, ConcurrentDictionary<string,string> diagnosticOutput)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var sb = appState.StringBuilderPool.Get();
		
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_core.scss"))).Trim() + '\n');
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_browser-reset.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_media-queries.scss"))).Trim() + '\n';

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Elas}px");
		
		sb.Append(mediaQueriesScss);

		var initScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_initialize.scss"))).Trim() + '\n';
		
		initScss = initScss.Replace("#{zero-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Zero}");
		initScss = initScss.Replace("#{phab-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Phab}");
		initScss = initScss.Replace("#{tabp-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Tabp}");
		initScss = initScss.Replace("#{tabl-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Tabl}");
		initScss = initScss.Replace("#{note-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Note}");
		initScss = initScss.Replace("#{desk-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Desk}");

		if (appState.Settings.Theme?.FontSizeUnits?.Elas.EndsWith("vw", StringComparison.Ordinal) ?? false)
		{
			initScss = initScss.Replace("#{elas-font-size}", $"calc(#{{$elas-breakpoint}} * (#{{sf-strip-unit({appState.Settings.Theme?.FontSizeUnits?.Elas})}} / 100))");
		}

		else
		{
			initScss = initScss.Replace("#{elas-font-size}", $"{appState.Settings.Theme?.FontSizeUnits?.Elas}");
		}
		
		sb.Append(initScss);

		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "_forms.scss"))).Trim() + '\n');

		diagnosticOutput.TryAdd("init2", $"{Strings.TriangleRight} Prepared SCSS base for output injection in {timer.FormatTimer()}{Environment.NewLine}");

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

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{appState.Settings.Theme?.MediaBreakpoints?.Elas}px");
		
		sb.Append(mediaQueriesScss);

		diagnosticOutput.TryAdd("init3", $"{Strings.TriangleRight} Prepared shared SCSS for output injection in {timer.FormatTimer()}{Environment.NewLine}");

		var result = sb.ToString();
		
		appState.StringBuilderPool.Return(sb);

		return result;
	}
	
	#endregion
	
	#region SCSS Transpiling
	
	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// </summary>
	/// <param name="filePath"></param>
	/// <param name="rawScss"></param>
	/// <param name="runner"></param>
	/// <param name="showOutput"></param>
	/// <returns>Generated CSS file</returns>
	public static async Task<string> TranspileScssAsync(string filePath, string rawScss, SfumatoRunner runner, bool showOutput = true)
	{
		var sb = runner.AppState.StringBuilderPool.Get();
		var scss = runner.AppState.StringBuilderPool.Get();

		try
		{
			if (string.IsNullOrEmpty(rawScss))
				rawScss = await File.ReadAllTextAsync(filePath);

			if (string.IsNullOrEmpty(rawScss))
				return string.Empty;
			
			var arguments = new List<string>();
			var cssOutputPath = filePath.TrimEnd(".scss") + ".css"; 
			var cssMapOutputPath = cssOutputPath + ".map";
			var includesBase = false;
			var includesUtilities = false;
			var includesShared = false;
			var timer = new Stopwatch();

			timer.Start();

			if (File.Exists(cssMapOutputPath))
				File.Delete(cssMapOutputPath);

			if (runner.AppState.Minify == false)
			{
				arguments.Add("--style=expanded");
				arguments.Add("--embed-sources");
			}

			else
			{
				arguments.Add("--style=compressed");
				arguments.Add("--no-source-map");
			}

			if (filePath.Contains(Path.DirectorySeparatorChar))
				arguments.Add($"--load-path={filePath[..filePath.LastIndexOf(Path.DirectorySeparatorChar)]}");
			else
				arguments.Add($"--load-path={runner.AppState.WorkingPath}");
			
			arguments.Add("--stdin");
			arguments.Add(cssOutputPath);
			
			#region Process @sfumato directives
			
			var matches = runner.AppState.SfumatoScssRegex.Matches(rawScss);
			var startIndex = 0;

			while (matches.Count > 0)
			{
				var match = matches[0];
				
				if (match.Index + match.Value.Length > startIndex)
					startIndex = match.Index + match.Value.Length;

				var matchValue = match.Value.CompactCss().TrimEnd(';');
				
				if (matchValue.EndsWith(" shared"))
				{
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, runner.AppState.ScssSharedInjectable.ToString());
					includesShared = true;
				}

				else if (matchValue.EndsWith(" base"))
				{
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, runner.AppState.ScssBaseInjectable.ToString());
					includesBase = true;
				}
				
				else if (matchValue.EndsWith(" utilities"))
				{
					var preamble = $"{Environment.NewLine}{Environment.NewLine}/* SFUMATO UTILITY CLASSES */{Environment.NewLine}{Environment.NewLine}";

					var utilitiesScss = await runner.GenerateUtilityScssAsync();
					
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, preamble + utilitiesScss);

					includesUtilities = true;
				}
				
				matches = runner.AppState.SfumatoScssRegex.Matches(rawScss);
			}
			
			#endregion
			
			#region Process @apply directives
			
			matches = runner.AppState.SfumatoScssApplyRegex.Matches(rawScss);
			startIndex = 0;

			while (matches.Count > 0)
			{
				var match = matches[0];
				
				if (match.Index + match.Value.Length > startIndex)
					startIndex = match.Index + match.Value.Length;

				var matchValue = match.Value.Trim().TrimEnd(';').CompactCss().TrimStart("@apply ");

				var classes = (matchValue?.Split(' ') ?? Array.Empty<string>()).ToList();

				foreach (var selector in classes.ToList())
				{
					if (runner.AppState.IsValidCoreClassSelector(selector) == false)
						classes.Remove(selector);
				}

				if (classes.Count == 0)
				{
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
				}
				
				else
				{
					var styles = runner.AppState.StringBuilderPool.Get();

					foreach (var selector in classes)
					{
						var newCssSelector = new CssSelector(runner.AppState, selector);

						await newCssSelector.ProcessSelectorAsync();

						if (newCssSelector.IsInvalid)
							continue;

						styles.Append(newCssSelector.GetStyles());
					}
					
					rawScss = rawScss.Remove(match.Index, match.Value.Length);
					rawScss = rawScss.Insert(match.Index, styles.ToString());
					
					runner.AppState.StringBuilderPool.Return(styles);
				}
				
				matches = runner.AppState.SfumatoScssApplyRegex.Matches(rawScss);
			}

			#endregion
			
			scss.Append(rawScss);
			
			var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap(runner.AppState.SassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

			await cmd.ExecuteAsync();
			
			runner.AppState.StringBuilderPool.Return(sb);
			runner.AppState.StringBuilderPool.Return(scss);

			var css = await File.ReadAllTextAsync(cssOutputPath);

			if (showOutput == false)
				return css;
			
			var details = runner.AppState.StringBuilderPool.Get();
				
			if (includesBase)
				details.Append(", +base");
				
			if (includesUtilities)
				details.Append($", +{runner.AppState.UsedClasses.Count(u => u.Value.IsInvalid == false):N0} utilities");

			if (includesShared)
				details.Append(", +shared");

			if (runner.AppState.Minify)
				details.Append(", minified");
				
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} Generated {SfumatoRunner.ShortenPathForOutput(filePath.TrimEnd(".scss", StringComparison.OrdinalIgnoreCase) + ".css", runner.AppState)} ({css.Length.FormatBytes()}{details}) in {timer.FormatTimer()}");

			runner.AppState.StringBuilderPool.Return(details);

			return css;
		}

		catch
		{
			var error = sb.ToString();

			if (error.IndexOf($"Command:{Environment.NewLine}", StringComparison.OrdinalIgnoreCase) > -1)
			{
				error = error[..error.IndexOf($"Command:{Environment.NewLine}", StringComparison.OrdinalIgnoreCase)].Trim();
			}
			
			await Console.Out.WriteLineAsync($"{Strings.TriangleRight} {SfumatoRunner.ShortenPathForOutput(filePath, runner.AppState)} => {error}");

			runner.AppState.StringBuilderPool.Return(sb);
			runner.AppState.StringBuilderPool.Return(scss);

			return string.Empty;
		}
	}
	
	#endregion
}

public class CssMediaQuery
{
	public int PrefixOrder { get; set; }
	public int Priority { get; set; }
	public string Prefix { get; set; } = string.Empty;
	public string PrefixType { get; set; } = string.Empty;
	public string Statement { get; set; } = string.Empty;
}