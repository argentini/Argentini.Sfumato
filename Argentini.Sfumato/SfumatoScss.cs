using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;

namespace Argentini.Sfumato;

public sealed class SfumatoScss
{
	public static async Task<string> GetCoreScssAsync(SfumatoAppState appState, StringBuilder diagnosticOutput)
	{
		var timer = new Stopwatch();

		timer.Start();
		
		var sb = new StringBuilder();
		
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "includes", "_core.scss"))).Trim() + '\n');
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "includes", "_browser-reset.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "includes", "_media-queries.scss"))).Trim() + '\n';

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{appState.Settings.Breakpoints?.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{appState.Settings.Breakpoints?.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{appState.Settings.Breakpoints?.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{appState.Settings.Breakpoints?.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{appState.Settings.Breakpoints?.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{appState.Settings.Breakpoints?.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{appState.Settings.Breakpoints?.Elas}px");
		
		sb.Append(mediaQueriesScss);

		var initScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "includes", "_initialize.scss"))).Trim() + '\n';
		
		initScss = initScss.Replace("#{zero-vw}", $"{appState.Settings.FontSizeViewportUnits?.Zero}vw");
		initScss = initScss.Replace("#{phab-vw}", $"{appState.Settings.FontSizeViewportUnits?.Phab}vw");
		initScss = initScss.Replace("#{tabp-vw}", $"{appState.Settings.FontSizeViewportUnits?.Tabp}vw");
		initScss = initScss.Replace("#{tabl-vw}", $"{appState.Settings.FontSizeViewportUnits?.Tabl}vw");
		initScss = initScss.Replace("#{note-vw}", $"{appState.Settings.FontSizeViewportUnits?.Note}vw");
		initScss = initScss.Replace("#{desk-vw}", $"{appState.Settings.FontSizeViewportUnits?.Desk}vw");
		initScss = initScss.Replace("#{elas-vw}", $"{appState.Settings.FontSizeViewportUnits?.Elas}vw");
		
		sb.Append(initScss);
		
		diagnosticOutput.Append($"Loaded core SCSS libraries in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
		
		return sb.ToString();
	}

	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// Calls "sass" CLI to transpile.
	/// Returns the byte size of the written file.
	/// </summary>
	/// <param name="scss"></param>
	/// <param name="appState"></param>
	/// <param name="releaseMode"></param>
	public static async Task<long> TranspileScss(StringBuilder scss, SfumatoAppState appState, bool releaseMode = false)
	{
		var sb = appState.StringBuilderPool.Get();

		try
		{
			var arguments = new List<string>();

			if (File.Exists(Path.Combine(appState.Settings.CssOutputPath, "sfumato.css.map")))
				File.Delete(Path.Combine(appState.Settings.CssOutputPath, "sfumato.css.map"));

			if (releaseMode == false)
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
			
			return fileInfo.Length;
		}

		catch (Exception e)
		{
			sb.AppendLine($" ERROR: {e.Message.Trim()}");
			sb.AppendLine(string.Empty);
			sb.AppendLine(e.StackTrace?.Trim());
			sb.AppendLine(string.Empty);

			Console.WriteLine(sb.ToString());
			
			Environment.Exit(1);
		}
		
		appState.StringBuilderPool.Return(sb);

		return 0;
	}
}