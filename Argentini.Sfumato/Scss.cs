using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;

namespace Argentini.Sfumato;

/// <summary>
/// Various helper methods for using Sass and SCSS files.
/// </summary>
public static class Scss
{
    /// <summary>
    /// Transpile a SCSS file into CSS.
    /// Calls "sass" CLI to transpile; install Dart Sass to use this method.
    /// </summary>
    /// <param name="sassCliPath">File system path to the dart sass executable</param>
    /// <param name="scssInputPath">File system path to the scss input file (e.g. "/scss/application.scss")</param>
    /// <param name="outputPath">File system path to the CSS output file (e.g. "/css/application.css")</param>
    /// <param name="debugMode">Set to true for verbose output and map files, false for compact output with no map files</param>
    /// <returns>True if successful, false if an error occurred</returns>
    public static async Task<string> TranspileScss(string sassCliPath, string scssInputPath, string outputPath, bool debugMode = true)
	{
		var sb = new StringBuilder();

		try
		{
			var arguments = new List<string>();

            if (File.Exists(Path.GetFullPath(outputPath) + ".map"))
                File.Delete(Path.GetFullPath(outputPath) + ".map");

            if (debugMode)
            {
	            arguments.Add("--style=expanded");
	            arguments.Add("--embed-sources");
            }

            else
            {
	            arguments.Add("--style=compressed");
	            arguments.Add("--no-source-map");
	            
            }
            
            arguments.Add($"{scssInputPath}");
            arguments.Add($"{outputPath}");

			await Cli.Wrap(sassCliPath)
				.WithArguments(args =>
				{
					foreach (var arg in arguments)
						args.Add(arg);

				})
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb))
				.ExecuteAsync();
			
            sb.AppendLine(" DONE");
        }

		catch (Exception e)
		{
			sb.AppendLine($" ERROR: {e.Message.Trim()}");
			sb.AppendLine(string.Empty);
			sb.AppendLine(e.StackTrace?.Trim());
			sb.AppendLine(string.Empty);

			Console.WriteLine(sb.ToString());
			throw;
		}

        return sb.ToString();
	}

    /// <summary>
    /// Transpile a single SCSS file into CSS, in-place.
    /// Calls "sass" CLI to transpile; install Dart Sass to use this method.
    /// </summary>
    /// <param name="sassCliPath">File system path to the dart sass executable</param>
    /// <param name="scssFilePath">File system path to the scss input file (e.g. "/scss/application.scss")</param>
    /// <param name="compressed">When true compacts the generated CSS</param>
    /// <param name="sfumatoRootPath">Sfumato root path</param>
    /// <param name="pathPrefix">This part of the path is removed from the logged output</param>
    /// <param name="injectScss">Inject this SCSS code at the top of the file prior to transpiling</param>
    /// <returns>True if successful, false if an error occurred</returns>
    public static async Task<string> TranspileSingleScss(string sassCliPath, string scssFilePath, bool compressed = false, string sfumatoRootPath = "", string pathPrefix = "", string injectScss = "")
    {
        var sb = new StringBuilder();

		if (string.IsNullOrEmpty(scssFilePath) || scssFilePath.ToLower().EndsWith(".scss") == false)
		{
			sb.AppendLine($"ERROR: invalid SCSS file path: {scssFilePath}");
            return await Task.FromResult(sb.ToString());
        }

        var outputPath = string.Concat(scssFilePath.AsSpan(0, scssFilePath.Length - 4), "css");

        try
        {
            var arguments = new List<string>();

            if (File.Exists(Path.GetFullPath(outputPath) + ".map"))
                File.Delete(Path.GetFullPath(outputPath) + ".map");

            arguments.Add($"--style={(compressed ? "compressed" : "expanded")}");
            arguments.Add("--no-source-map");
            arguments.Add($"--load-path={Path.Combine(sfumatoRootPath, "scss")}");
            arguments.Add("--stdin");
            arguments.Add($"{outputPath}");

            var scssContent = string.Empty;
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            while (cancellationToken.IsCancellationRequested == false)
            {
                try
                {
	                await using (var fs = new FileStream(scssFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    
	                using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        scssContent = await sr.ReadToEndAsync(cancellationToken.Token);
                    }

                    cancellationToken.Cancel();
                }

                catch
                {
                    await Task.Delay(10, cancellationToken.Token);
                }
            }

            if (injectScss.HasValue())
                scssContent = injectScss.Trim() + "\r\n\r\n" + scssContent;
            
            sb.Append($"{scssFilePath.TrimStart(pathPrefix).TrimStart(Path.DirectorySeparatorChar.ToString()).TrimStart(Path.AltDirectorySeparatorChar.ToString())}...");
            
            var cmd = PipeSource.FromString(scssContent) | Cli.Wrap(sassCliPath)
                .WithArguments(args =>
                {
	                foreach (var arg in arguments)
		                args.Add(arg);

                })
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

                await cmd.ExecuteAsync();

            sb.AppendLine(" DONE");
        }

        catch (Exception ex)
        {
            sb.AppendLine($"ERROR: {ex.Message.Trim()}");
            sb.AppendLine(string.Empty);
            sb.AppendLine(ex.StackTrace?.Trim());
            sb.AppendLine(string.Empty);
            
            Console.WriteLine(sb.ToString());
        }

        return sb.ToString();
    }
}
