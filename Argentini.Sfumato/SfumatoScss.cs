using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;

namespace Argentini.Sfumato;

public sealed class SfumatoScss
{
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
		
		sb.Append((await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "includes", "_core.scss"))).Trim() + '\n');

		var mediaQueriesScss = (await File.ReadAllTextAsync(Path.Combine(appState.ScssPath, "includes", "_media-queries.scss"))).Trim() + '\n';

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
}