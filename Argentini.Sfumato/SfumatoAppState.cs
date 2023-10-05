using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CliWrap;
using Mapster;
using Microsoft.Extensions.ObjectPool;

namespace Argentini.Sfumato;

public sealed class SfumatoAppState
{
    #region Properties

    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    public SfumatoJsonSettings Settings { get; set; } = new();
    public StringBuilder DiagnosticOutput { get; set; } = new();
    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public Dictionary<string, string> ScssFiles { get; } = new();
    public List<ScssClass> Classes { get; } = new();
    
    public static string CliErrorPrefix => "Sfumato => ";

    #endregion

    /// <summary>
    /// Initialize the app state. Loads settings JSON file from working path.
    /// Sets up runtime environment for the runner.
    /// </summary>
    /// <param name="workingPath">Leave empty to use app working directory or pass a path to override</param>
    /// <param name="diagnosticOutput">Used to collect diagnostic output</param>
    public async Task InitializeAsync(string workingPath, StringBuilder diagnosticOutput)
    {
	    var timer = new Stopwatch();

	    timer.Start();

	    DiagnosticOutput = diagnosticOutput;
	    
	    #region Find sfumato.json file

        if (string.IsNullOrEmpty(workingPath) == false)
            WorkingPath = workingPath;
        
        SettingsFilePath = Path.Combine(WorkingPath, "sfumato.json");

        if (File.Exists(SettingsFilePath) == false)
        {
            Console.WriteLine($"{CliErrorPrefix}Could not find settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        #endregion
        
        try
        {
            #region Load sfumato.json file

            var json = await File.ReadAllTextAsync(SettingsFilePath);
            var jsonSettings = JsonSerializer.Deserialize<SfumatoJsonSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IncludeFields = true
            });
		
            if (jsonSettings is null)
            {
                Console.WriteLine($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
                Environment.Exit(1);
            }
            
            #endregion
            
            #region Import settings
            
            Settings.CssOutputPath = Path.Combine(WorkingPath, jsonSettings.CssOutputPath.Replace('\\', '/').Replace('/', Path.DirectorySeparatorChar));
        
            if (Directory.Exists(Settings.CssOutputPath) == false)
            {
                Console.WriteLine($"{CliErrorPrefix}Could not find CSS output path: {Settings.CssOutputPath}");
                Environment.Exit(1);
            }
        
            Settings.ThemeMode = jsonSettings.ThemeMode switch
            {
                "system" => "system",
                "class" => "class",
                _ => "system"
            };

            SassCliPath = GetEmbeddedSassPath();
            ScssPath = GetEmbeddedScssPath();

            Settings.ProjectPaths.Clear();
        
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
                projectPath.Path = Path.Combine(WorkingPath, projectPath.Path.Replace('\\', '/').Replace('/', Path.DirectorySeparatorChar));

                if (string.IsNullOrEmpty(projectPath.FileSpec))
                    return;
            
                var tempFileSpec = projectPath.FileSpec.Replace("*", string.Empty).Replace(".", string.Empty);

                if (string.IsNullOrEmpty(tempFileSpec) == false)
                    projectPath.FileSpec = $"*.{tempFileSpec}";
            
                Settings.ProjectPaths.Add(projectPath);
            }

            jsonSettings.Breakpoints.Adapt(Settings.Breakpoints);
            jsonSettings.FontSizeViewportUnits.Adapt(Settings.FontSizeViewportUnits);
            
            #endregion
        }

        catch
        {
            Console.WriteLine($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }

        DiagnosticOutput.Append($"Initialized settings in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
        
        timer.Restart();
        
        await GatherAvailableClassesAsync();

        diagnosticOutput.Append($"Identified {Classes.Count:N0} available classes in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
    }
    
    #region Methods

    /// <summary>
    /// Identify the available class names from the embedded SCSS library.
    /// Also loads all SCSS file content into memory.
    /// </summary>
    /// <param name="config"></param>
    public async Task GatherAvailableClassesAsync()
    {
	    var dir = new DirectoryInfo(ScssPath);

	    ScssFiles.Clear();
	    Classes.Clear();
		
	    var files = dir.GetFiles();

	    foreach (var file in files.Where(f => f.Extension.Equals(".scss", StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.Name))
	    {
		    var scss = (await File.ReadAllTextAsync(file.FullName)).NormalizeLinebreaks();

		    if (scss.Length < 1)
			    continue;

		    ScssFiles.Add(file.FullName, scss);
			
		    var index = 0;

		    while (index > -1)
		    {
			    if (scss[index] != '.')
			    {
				    index = scss.IndexOf("\n.", index, StringComparison.Ordinal);

				    if (index > -1)
					    index++;
			    }

			    if (index < 0)
				    continue;
				
			    var end = scss.IndexOf(' ', index);

			    if (end < 0)
				    continue;
					
			    Classes.Add(new ScssClass
			    {
				    FilePath = file.FullName,
				    ClassName = scss.Substring(index + 1, end - index - 1)
			    });

			    index = end;
		    }
	    }
    }
    
    public static string GetWorkingPath()
    {
        var workingPath = Directory.GetCurrentDirectory();
        
#if DEBUG
        var index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

        if (index > -1)
        {
            workingPath = Path.Combine(workingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
        }

        else
        {
            index = workingPath.IndexOf(Path.DirectorySeparatorChar + "Argentini.Sfumato.Tests" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.InvariantCulture);

            if (index > -1)
                workingPath = Path.Combine(workingPath[..index], "Argentini.Sfumato.Tests", "SampleWebsite");
        }
#endif

        return workingPath;
    }

    public static string GetEmbeddedSassPath()
    {
        var osPlatform = Identify.GetOsPlatform();
        var processorArchitecture = Identify.GetProcessorArchitecture();
        var sassPath = string.Empty;
		var workingPath = Assembly.GetExecutingAssembly().Location;

		while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
		{
			workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];

#if DEBUG
			if (Directory.Exists(Path.Combine(workingPath, "sass")) == false)
				continue;

			var tempPath = workingPath; 
			
			workingPath = Path.Combine(tempPath, "sass");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "sass");
#endif
			
			if (osPlatform == OSPlatform.Windows)
			{
				if (processorArchitecture is Architecture.X64 or Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-windows-x64", "sass.bat");
			}
				
			else if (osPlatform == OSPlatform.OSX)
			{
				if (processorArchitecture == Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-macos-x64", "sass");
				else if (processorArchitecture == Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-macos-arm64", "sass");
			}
				
			else if (osPlatform == OSPlatform.Linux)
			{
				if (processorArchitecture == Architecture.X64)
					sassPath = Path.Combine(workingPath, "dart-sass-linux-x64", "sass");
				else if (processorArchitecture == Architecture.Arm64)
					sassPath = Path.Combine(workingPath, "dart-sass-linux-arm64", "sass");
			}

			break;
		}
		
		if (string.IsNullOrEmpty(sassPath) || File.Exists(sassPath) == false)
		{
			Console.WriteLine($"{CliErrorPrefix}Embedded Dart Sass cannot be found.");
			Environment.Exit(1);
		}
		
		var sb = new StringBuilder();
		var cmd = Cli.Wrap(sassPath)
			.WithArguments(arguments =>
			{
				arguments.Add("--version");
			})
			.WithStandardOutputPipe(PipeTarget.ToStringBuilder(sb))
			.WithStandardErrorPipe(PipeTarget.ToStringBuilder(sb));

		try
		{
			_ = cmd.ExecuteAsync().GetAwaiter().GetResult();
		}

		catch
		{
			Console.WriteLine($"{CliErrorPrefix}Dart Sass is embedded but cannot be found.");
			Environment.Exit(1);
		}
		
		return sassPath;
    }

    public static string GetEmbeddedScssPath()
    {
	    var workingPath = Assembly.GetExecutingAssembly().Location;

	    while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
	    {
		    workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];
            
#if DEBUG
		    if (Directory.Exists(Path.Combine(workingPath, "scss")) == false)
			    continue;

		    var tempPath = workingPath; 
			
		    workingPath = Path.Combine(tempPath, "scss");
#else
			if (Directory.Exists(Path.Combine(workingPath, "contentFiles")) == false)
				continue;
		
			var tempPath = workingPath; 

			workingPath = Path.Combine(tempPath, "contentFiles", "any", "any", "scss");
#endif
		    break;
		}

        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
        {
            Console.WriteLine($"{CliErrorPrefix}Embedded SCSS resources cannot be found.");
            Environment.Exit(1);
        }
        
		return workingPath;
	}
    
    #endregion
}
