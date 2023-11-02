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
            
            #endregion
            
            #region Merge settings
            
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

            jsonSettings.Theme?.MediaBreakpoints.Adapt(Theme?.MediaBreakpoints);
            jsonSettings.Theme?.FontSizeUnits.Adapt(Theme?.FontSizeUnits);
            
            #endregion
        }

        catch
        {
            await Console.Out.WriteLineAsync($"{SfumatoAppState.CliErrorPrefix}Invalid settings file at path {appState.SettingsFilePath}");
            Environment.Exit(1);
        }
    }
}
