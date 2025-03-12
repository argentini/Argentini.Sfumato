using Argentini.Sfumato.Extensions;
using Mapster;
using YamlDotNet.Serialization;

namespace Argentini.Sfumato.Entities.Yaml;

public sealed class Settings
{
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Project> Projects { get; set;  } = [];

    public async Task LoadSettingsAsync(AppState appState)
    {
        #region Find sfumato.yml file

        if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
            appState.WorkingPath = appState.WorkingPathOverride;
        
        appState.SettingsFilePath = Path.Combine(appState.WorkingPath, "sfumato.yml");

        if (File.Exists(appState.SettingsFilePath) == false)
        {
            await Console.Out.WriteLineAsync($"Could not find sfumato.yml settings file at path {appState.WorkingPath}");
            await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
            Environment.Exit(1);
        }

        #endregion

        try
        {
            #region Load sfumato.yml file

            var yaml = await Storage.ReadAllTextWithRetriesAsync(appState.SettingsFilePath, AppState.FileAccessRetryMs);
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            var yamlSettings = deserializer.Deserialize<Settings>(yaml);

            yamlSettings.Adapt(this);

            foreach(var project in Projects)
            {
                #region Validate dark mode
                
                project.DarkMode = project.DarkMode.ToLower() switch
                {
                    "media" => "media",
                    "system" => "media",
                    "class" => "class",
                    _ => "media"
                };

                #endregion
                
                #region Validate project paths

                var invalidExtensions = new[] { "css", "map" };

                if (project.ProjectPaths.Count == 0)
                {
                    project.ProjectPaths.Add(new ProjectPath());
                }
                
                foreach (var projectPath in project.ProjectPaths.ToList())
                {
                    projectPath.Path = Path.Combine(appState.WorkingPath, projectPath.Path.SetNativePathSeparators());

                    if (projectPath.ExtensionsList.Count > 0)
                        projectPath.Extensions = string.Join(',', projectPath.ExtensionsList);

                    if (projectPath.IgnoreFoldersList.Count > 0)
                        projectPath.IgnoreFolders = string.Join(',', projectPath.IgnoreFoldersList);

                    if (string.IsNullOrEmpty(projectPath.Extensions) || invalidExtensions.Contains(projectPath.Extensions))
                        project.ProjectPaths.Remove(projectPath);
                }

                #endregion
            }
                
            #endregion
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"{AppState.CliErrorPrefix}Invalid settings in file: {appState.SettingsFilePath}");
            await Console.Out.WriteLineAsync(e.Message);
            Environment.Exit(1);
        }
    }
}
