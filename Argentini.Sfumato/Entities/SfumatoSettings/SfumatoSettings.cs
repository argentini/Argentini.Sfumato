using Mapster;
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

            jsonSettings.Adapt(this);
            
            #region Dark Mode
            
            DarkMode = DarkMode.ToLower() switch
            {
                "media" => "media",
                "system" => "media",
                "class" => "class",
                _ => "media"
            };

            UseAutoTheme = jsonSettings.UseAutoTheme;
            
            #endregion
            
            #region Project Paths

            var invalidExtensions = new[] { "css", "map", "scss" };

            if (jsonSettings.ProjectPaths.Count == 0)
            {
                jsonSettings.ProjectPaths.Add(new ProjectPath());
            }
            
            foreach (var projectPath in ProjectPaths.ToList())
            {
                projectPath.Path = Path.Combine(appState.WorkingPath, projectPath.Path.SetNativePathSeparators());

                if (projectPath.ExtensionsList.Count > 0)
                    projectPath.Extensions = string.Join(',', projectPath.ExtensionsList);

                if (string.IsNullOrEmpty(projectPath.Extensions) || invalidExtensions.Contains(projectPath.Extensions))
                    ProjectPaths.Remove(projectPath);
            }

            ProjectPaths.Insert(0, new ProjectPath
            {
                Path = appState.WorkingPath,
                Extensions = "scss",
                Recurse = true
            });

            #endregion

            #endregion
            
            #region Merge Settings
            
            foreach (var color in Theme.Color)
                appState.ColorOptions.TryAddUpdate(color);
                
            foreach (var animation in Theme.Animation)
                appState.AnimateStaticUtilities.TryAddUpdate(animation);

            foreach (var ratio in Theme.AspectRatio)
                appState.AspectStaticUtilities.TryAddUpdate(ratio, "aspect-ratio: ");

            foreach (var bgImage in Theme.BackgroundImage)
                appState.BgStaticUtilities.TryAddUpdate(bgImage, "background-image: ");
            
            foreach (var bgPosition in Theme.BackgroundPosition)
                appState.BgStaticUtilities.TryAddUpdate(bgPosition, "background-position: ");
            
            foreach (var bgSize in Theme.BackgroundSize)
                appState.BgStaticUtilities.TryAddUpdate(bgSize, "background-size: ");
            
            
            
            
            
            
            


            #endregion
        }

        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"{SfumatoAppState.CliErrorPrefix}Invalid settings in file: {appState.SettingsFilePath}");
            await Console.Out.WriteLineAsync(e.Message);
            Environment.Exit(1);
        }
    }
}
