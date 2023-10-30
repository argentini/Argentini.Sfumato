namespace Argentini.Sfumato;

public static class SfumatoScss
{
	#region Core Shared SCSS
	
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
		
		diagnosticOutput.TryAdd("init2", $"{Strings.TriangleRight} Prepared SCSS Core for output injection in {timer.FormatTimer()}{Environment.NewLine}");

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

			if (appState.Minify == false)
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