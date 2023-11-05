using YamlDotNet.Serialization;

namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class SfumatoSettings
{
    public List<ProjectPath> ProjectPaths { get; set;  } = new();
    public string DarkMode { get; set; } = "media";
    public bool UseAutoTheme { get; set; }

    public Theme Theme { get; set; } = new();

    public async Task LoadJsonSettingsAsync(SfumatoAppState appState)
    {
        #region Find sfumato.yaml file

        if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
            appState.WorkingPath = appState.WorkingPathOverride;
        
        appState.SettingsFilePath = Path.Combine(appState.WorkingPath, "sfumato.yaml");

        if (File.Exists(appState.SettingsFilePath) == false)
        {
            await Console.Out.WriteLineAsync($"Could not find sfumato.yaml settings file at path {appState.WorkingPath}");
            await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
            Environment.Exit(1);
        }

        #endregion

        try
        {
            #region Load sfumato.yaml file

            var json = await File.ReadAllTextAsync(appState.SettingsFilePath);
            var deserializer = new DeserializerBuilder().Build();
            var jsonSettings = deserializer.Deserialize<SfumatoSettings>(json);
		
            #region Dark Mode
            
            DarkMode = jsonSettings.DarkMode.ToLower() switch
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
        
            ProjectPaths.Add(new ProjectPath
            {
                Path = appState.WorkingPath,
                Extension = "scss",
                Recurse = true
            });
            
            foreach (var projectPath in jsonSettings.ProjectPaths)
            {
                projectPath.Path = Path.Combine(appState.WorkingPath, projectPath.Path.SetNativePathSeparators());
                projectPath.Extension = projectPath.Extension.TrimStart('.').ToLower();

                if (string.IsNullOrEmpty(projectPath.Extension) || projectPath.Extension == "scss")
                    continue;
                
                ProjectPaths.Add(projectPath);
            }
            
            #endregion

            Theme.MediaBreakpoints = jsonSettings.Theme.MediaBreakpoints;
            Theme.FontSizeUnits = jsonSettings.Theme.FontSizeUnits;
            Theme.Colors = jsonSettings.Theme.Colors;

            #endregion
            
            #region Merge Settings

            foreach (var color in Theme.Colors)
                appState.ColorOptions.TryAddUpdate(color);
                
                
                
                
            
            #endregion
        }

        catch
        {
            await Console.Out.WriteLineAsync($"{SfumatoAppState.CliErrorPrefix}Invalid settings in file: {appState.SettingsFilePath}");
            Environment.Exit(1);
        }
    }
}
