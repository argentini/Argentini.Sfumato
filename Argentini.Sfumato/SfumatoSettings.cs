using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CliWrap;
using Mapster;

namespace Argentini.Sfumato;

public sealed class SfumatoSettings
{
    #region JSON Properties

    public string CssOutputPath { get; set; } = string.Empty;

    public List<ProjectPath> ProjectPaths { get; set;  } = new();

    public string ThemeMode { get; set; } = "system";

    public Breakpoints? Breakpoints { get; set; } = new();
    public FontSizeViewportUnits? FontSizeViewportUnits { get; set; } = new();
    
    #endregion

    #region Runtime Properties

    public string SettingsFilePath { get; set; } = string.Empty;
    public string WorkingPath { get; set;  } = GetWorkingPath();
    public string SassCliPath { get; set; } = string.Empty;
    public string ScssPath { get; set; } = string.Empty;
    public static string CliErrorPrefix => "Sfumato => ";

    #endregion

    public async Task LoadAsync(string workingPath = "")
    {
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
            var jsonSettings = JsonSerializer.Deserialize<SfumatoSettings>(json, new JsonSerializerOptions
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
            
            CssOutputPath = Path.Combine(WorkingPath, jsonSettings.CssOutputPath.Replace('\\', '/').Replace('/', Path.DirectorySeparatorChar));
        
            if (Directory.Exists(CssOutputPath) == false)
            {
                Console.WriteLine($"{CliErrorPrefix}Could not find CSS output path: {CssOutputPath}");
                Environment.Exit(1);
            }
        
            ThemeMode = jsonSettings.ThemeMode switch
            {
                "system" => "system",
                "class" => "class",
                _ => "system"
            };

            SassCliPath = GetEmbeddedSassPath();
            ScssPath = GetEmbeddedScssPath();

            ProjectPaths.Clear();
        
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
                projectPath.Path = Path.Combine(WorkingPath, projectPath.Path.Replace('\\', '/').Replace('/', Path.DirectorySeparatorChar));

                if (string.IsNullOrEmpty(projectPath.FileSpec))
                    return;
            
                var tempFileSpec = projectPath.FileSpec.Replace("*", string.Empty).Replace(".", string.Empty);

                if (string.IsNullOrEmpty(tempFileSpec) == false)
                    projectPath.FileSpec = $"*.{tempFileSpec}";
            
                ProjectPaths.Add(projectPath);
            }

            jsonSettings.Breakpoints.Adapt(Breakpoints);
            jsonSettings.FontSizeViewportUnits.Adapt(FontSizeViewportUnits);
            
            #endregion
        }

        catch
        {
            Console.WriteLine($"{CliErrorPrefix}Invalid settings file at path {SettingsFilePath}");
            Environment.Exit(1);
        }
    }
    
    #region Helper Methods

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

public sealed class ProjectPath
{
    public string Path { get; set; } = string.Empty;
    public string FileSpec { get; set; } = "*.html";
    public bool Recurse { get; set; } = true;
}

public sealed class Breakpoints
{
    private int _zero;
    public int Zero
    {
        get => _zero;
        set
        {
            _zero = value switch
            {
                < 0 => 0,
                _ => value
            };
        }
    }
    private int _phab = 400;
    public int Phab
    {
        get => _phab;
        set
        {
            _phab = value switch
            {
                < 0 => 400,
                _ => value
            };
        }
    }
    private int _tabp = 540;
    public int Tabp
    {
        get => _tabp;
        set
        {
            _tabp = value switch
            {
                < 0 => 540,
                _ => value
            };
        }
    }
    private int _tabl = 800;
    public int Tabl
    {
        get => _tabl;
        set
        {
            _tabl = value switch
            {
                < 0 => 800,
                _ => value
            };
        }
    }
    private int _note = 1280;
    public int Note
    {
        get => _note;
        set
        {
            _note = value switch
            {
                < 0 => 1280,
                _ => value
            };
        }
    }
    private int _desk = 1440;
    public int Desk
    {
        get => _desk;
        set
        {
            _desk = value switch
            {
                < 0 => 1440,
                _ => value
            };
        }
    }
    private int _elas = 1600;
    public int Elas
    {
        get => _elas;
        set
        {
            _elas = value switch
            {
                < 0 => 1600,
                _ => value
            };
        }
    }
}

public sealed class FontSizeViewportUnits
{
    private double _zero = 4.35;
    public double Zero
    {
        get => _zero;
        set
        {
            _zero = value switch
            {
                < 0 => 4.35,
                _ => value
            };
        }
    }
    private double _phab = 4;
    public double Phab
    {
        get => _phab;
        set
        {
            _phab = value switch
            {
                < 0 => 4,
                _ => value
            };
        }
    }
    private double _tabp = 1.6;
    public double Tabp
    {
        get => _tabp;
        set
        {
            _tabp = value switch
            {
                < 0 => 1.6,
                _ => value
            };
        }
    }
    private double _tabl = 1;
    public double Tabl
    {
        get => _tabl;
        set
        {
            _tabl = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
    private double _note = 1;
    public double Note
    {
        get => _note;
        set
        {
            _note = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
    private double _desk = 1;
    public double Desk
    {
        get => _desk;
        set
        {
            _desk = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
    private double _elas = 1;
    public double Elas
    {
        get => _elas;
        set
        {
            _elas = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
}
