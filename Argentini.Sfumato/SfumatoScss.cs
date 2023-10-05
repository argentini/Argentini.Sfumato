using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;

namespace Argentini.Sfumato;

public sealed class SfumatoScss
{
	public static async Task<string> GetCoreScssAsync(SfumatoSettings settings)
	{
		var sb = new StringBuilder();
		
		sb.Append((await File.ReadAllTextAsync(Path.Combine(settings.ScssPath, "includes", "_core.scss"))).Trim() + '\n');
		sb.Append((await File.ReadAllTextAsync(Path.Combine(settings.ScssPath, "includes", "_browser-reset.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(settings.ScssPath, "includes", "_media-queries.scss"))).Trim() + '\n';

		mediaQueriesScss = mediaQueriesScss.Replace("#{zero-bp}", $"{settings.Breakpoints?.Zero}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{phab-bp}", $"{settings.Breakpoints?.Phab}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabp-bp}", $"{settings.Breakpoints?.Tabp}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{tabl-bp}", $"{settings.Breakpoints?.Tabl}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{note-bp}", $"{settings.Breakpoints?.Note}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{desk-bp}", $"{settings.Breakpoints?.Desk}px");
		mediaQueriesScss = mediaQueriesScss.Replace("#{elas-bp}", $"{settings.Breakpoints?.Elas}px");
		
		sb.Append(mediaQueriesScss);

		var initScss = (await File.ReadAllTextAsync(Path.Combine(settings.ScssPath, "includes", "_initialize.scss"))).Trim() + '\n';
		
		initScss = initScss.Replace("#{zero-vw}", $"{settings.FontSizeViewportUnits?.Zero}vw");
		initScss = initScss.Replace("#{phab-vw}", $"{settings.FontSizeViewportUnits?.Phab}vw");
		initScss = initScss.Replace("#{tabp-vw}", $"{settings.FontSizeViewportUnits?.Tabp}vw");
		initScss = initScss.Replace("#{tabl-vw}", $"{settings.FontSizeViewportUnits?.Tabl}vw");
		initScss = initScss.Replace("#{note-vw}", $"{settings.FontSizeViewportUnits?.Note}vw");
		initScss = initScss.Replace("#{desk-vw}", $"{settings.FontSizeViewportUnits?.Desk}vw");
		initScss = initScss.Replace("#{elas-vw}", $"{settings.FontSizeViewportUnits?.Elas}vw");
		
		sb.Append(initScss);
		
		return sb.ToString();
	}

	/// <summary>
	/// Transpile SCSS markup into CSS.
	/// Calls "sass" CLI to transpile.
	/// Returns the byte size of the written file.
	/// </summary>
	/// <param name="scss"></param>
	/// <param name="settings"></param>
	/// <param name="releaseMode"></param>
	public static async Task<long> TranspileScss(StringBuilder scss, SfumatoSettings settings, bool releaseMode = false)
	{
		var sb = settings.StringBuilderPool.Get();

		try
		{
			var arguments = new List<string>();

			if (File.Exists(Path.Combine(settings.CssOutputPath, "sfumato.css.map")))
				File.Delete(Path.Combine(settings.CssOutputPath, "sfumato.css.map"));

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
			arguments.Add(Path.Combine(settings.CssOutputPath, "sfumato.css"));
			
			var cmd = PipeSource.FromString(scss.ToString()) | Cli.Wrap(settings.SassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

			await cmd.ExecuteAsync();

			var fileInfo = new FileInfo(Path.Combine(settings.CssOutputPath, "sfumato.css"));
			
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
		
		settings.StringBuilderPool.Return(sb);

		return 0;
	}
}