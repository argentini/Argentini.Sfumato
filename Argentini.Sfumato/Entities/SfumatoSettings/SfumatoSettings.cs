using Mapster;

namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class SfumatoSettings
{
    public List<ProjectPath> ProjectPaths { get; set;  } = new();
    public string DarkMode { get; set; } = "media";
    public bool UseAutoTheme { get; set; }

    public Theme? Theme { get; set; } = new();

    public async Task LoadJsonSettingsAsync(SfumatoAppState appState)
    {
        #region Find sfumato.json file

        if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
            appState.WorkingPath = appState.WorkingPathOverride;
        
        appState.SettingsFilePath = Path.Combine(appState.WorkingPath, "sfumato.json");

        if (File.Exists(appState.SettingsFilePath) == false)
        {
            await Console.Out.WriteLineAsync($"Could not find sfumato.json settings file at path {appState.WorkingPath}");
            await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
            Environment.Exit(1);
        }

        #endregion

        try
        {
            #region Load sfumato.json file

            var json = await File.ReadAllTextAsync(appState.SettingsFilePath);
            var jsonSettings = JsonSerializer.Deserialize<SfumatoSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IncludeFields = false
            });
		
            if (jsonSettings is null)
            {
                await Console.Out.WriteLineAsync($"{SfumatoAppState.CliErrorPrefix}Invalid settings file at path {appState.SettingsFilePath}");
                Environment.Exit(1);
            }
            
            #region Dark Mode
            
            DarkMode = jsonSettings.DarkMode switch
            {
                "media" => "media",
                "system" => "media",
                "class" => "class",
                _ => "media"
            };

            UseAutoTheme = jsonSettings.UseAutoTheme;
            
            #endregion
            
            #region Project Paths
            
            ProjectPaths.Clear();
        
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
	            if (string.IsNullOrEmpty(projectPath.FileSpec))
		            continue;
	            
                projectPath.Path = Path.Combine(appState.WorkingPath, projectPath.Path.SetNativePathSeparators());
                
                if (projectPath.FileSpec.Contains('.') && projectPath.FileSpec.StartsWith('*') == false)
                {
	                projectPath.IsFilePath = true;
                    ProjectPaths.Add(projectPath);

	                continue;
                }
                
                var tempFileSpec = projectPath.FileSpec.Replace("*", string.Empty).Replace(".", string.Empty);

                if (string.IsNullOrEmpty(tempFileSpec) == false)
                    projectPath.FileSpec = $"*.{tempFileSpec}";
            
                ProjectPaths.Add(projectPath);
            }

            ProjectPaths.Add(new ProjectPath
            {
	            Path = appState.WorkingPath,
	            FileSpec = "*.scss",
	            Recurse = true
            });

            #endregion

            jsonSettings.Theme?.MediaBreakpoints.Adapt(Theme?.MediaBreakpoints);
            jsonSettings.Theme?.FontSizeUnits.Adapt(Theme?.FontSizeUnits);
            jsonSettings.Theme?.Colors.Adapt(Theme?.Colors);
            
            #endregion
            
            #region Merge Settings

            if (Theme is null)
                return;
            
            if (Theme.Colors is not null)
            {
                foreach (var color in Theme.Colors)
                    appState.ColorOptions.TryAddUpdate(color);
                
                
                
                
                
            }
            
            #endregion
        }

        catch
        {
            await Console.Out.WriteLineAsync($"{SfumatoAppState.CliErrorPrefix}Invalid settings file at path {appState.SettingsFilePath}");
            Environment.Exit(1);
        }
    }
}
