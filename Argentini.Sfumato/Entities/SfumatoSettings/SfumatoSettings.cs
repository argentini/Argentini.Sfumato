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
            
            #region Shared Values
            
            foreach (var item in Theme.BorderWidth ?? new Dictionary<string, string>())
                appState.BorderWidthOptions.TryAddUpdate(item);

            foreach (var item in Theme.BorderRadius ?? new Dictionary<string, string>())
                appState.RoundedOptions.TryAddUpdate(item);
            
            foreach (var item in Theme.Color ?? new Dictionary<string, string>())
                appState.ColorOptions.TryAddUpdate(item);

            foreach (var item in Theme.FilterSize ?? new Dictionary<string, string>())
                appState.FilterSizeOptions.TryAddUpdate(item);
            
            #endregion
            
            #region Backgrounds
            
            foreach (var item in Theme.BackgroundImage ?? new Dictionary<string, string>())
                appState.BgStaticUtilities.TryAddUpdate(item, "background-image: {value};");
            
            foreach (var item in Theme.BackgroundPosition ?? new Dictionary<string, string>())
                appState.BgStaticUtilities.TryAddUpdate(item, "background-position: {value};");
            
            foreach (var item in Theme.BackgroundSize ?? new Dictionary<string, string>())
                appState.BgStaticUtilities.TryAddUpdate(item, "background-size: {value};");

            foreach (var item in Theme.FromPosition ?? new Dictionary<string, string>())
                appState.FromStaticUtilities.TryAddUpdate(item, """
                                                                --sf-gradient-from: {value} var(--sf-gradient-from-position);
                                                                --sf-gradient-to: transparent var(--sf-gradient-to-position);
                                                                --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                                                                """);
            
            foreach (var item in Theme.ToPosition ?? new Dictionary<string, string>())
                appState.ToStaticUtilities.TryAddUpdate(item, "--sf-gradient-to: {value} var(--sf-gradient-to-position);");

            foreach (var item in Theme.ViaPosition ?? new Dictionary<string, string>())
                appState.ViaStaticUtilities.TryAddUpdate(item, """
                                                               --sf-gradient-to: transparent var(--sf-gradient-to-position);
                                                               --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
                                                               """);

            #endregion
            
            #region Effects
            
            foreach (var item in Theme.BoxShadow ?? new Dictionary<string, string>())
                appState.ShadowStaticUtilities.TryAddUpdate(item, "box-shadow: {value};");
            
            #endregion

            #region Filters
            
            foreach (var item in Theme.BackdropGrayscale ?? new Dictionary<string, string>())
                appState.BackdropGrayscaleStaticUtilities.TryAddUpdate(item, "backdrop-filter: grayscale({value});");

            foreach (var item in Theme.BackdropHueRotate ?? new Dictionary<string, string>())
                appState.BackdropHueRotateStaticUtilities.TryAddUpdate(item, "backdrop-filter: hue-rotate({value});");

            foreach (var item in Theme.BackdropInvert ?? new Dictionary<string, string>())
                appState.BackdropInvertStaticUtilities.TryAddUpdate(item, "backdrop-filter: invert({value});");

            foreach (var item in Theme.BackdropSepia ?? new Dictionary<string, string>())
                appState.BackdropSepiaStaticUtilities.TryAddUpdate(item, "backdrop-filter: sepia({value});");
            
            foreach (var item in Theme.Grayscale ?? new Dictionary<string, string>())
                appState.GrayscaleStaticUtilities.TryAddUpdate(item, "filter: grayscale({value});");

            foreach (var item in Theme.HueRotate ?? new Dictionary<string, string>())
                appState.HueRotateStaticUtilities.TryAddUpdate(item, "filter: hue-rotate({value});");

            foreach (var item in Theme.Invert ?? new Dictionary<string, string>())
                appState.InvertStaticUtilities.TryAddUpdate(item, "filter: invert({value});");

            foreach (var item in Theme.Sepia ?? new Dictionary<string, string>())
                appState.SepiaStaticUtilities.TryAddUpdate(item, "filter: sepia({value});");
            
            #endregion

            #region Grid

            foreach (var item in Theme.GridAutoCols ?? new Dictionary<string, string>())
                appState.AutoColsStaticUtilities.TryAddUpdate(item, "grid-auto-columns: {value};");

            foreach (var item in Theme.GridAutoRows ?? new Dictionary<string, string>())
                appState.AutoRowsStaticUtilities.TryAddUpdate(item, "grid-auto-rows: {value};");
            
            foreach (var item in Theme.GridColEnd ?? new Dictionary<string, string>())
                appState.ColEndStaticUtilities.TryAddUpdate(item, "grid-column-end: {value};");

            foreach (var item in Theme.GridColSpan ?? new Dictionary<string, string>())
                appState.ColSpanStaticUtilities.TryAddUpdate(item, "grid-column: {value};");

            foreach (var item in Theme.GridColStart ?? new Dictionary<string, string>())
                appState.ColStartStaticUtilities.TryAddUpdate(item, "grid-column-start: {value};");
            
            foreach (var item in Theme.Gap ?? new Dictionary<string, string>())
            {
                appState.GapStaticUtilities.TryAddUpdate(item, "gap: {value};");
                appState.GapXStaticUtilities.TryAddUpdate(item, "column-gap: {value};");
                appState.GapYStaticUtilities.TryAddUpdate(item, "row-gap: {value};");
            }

            foreach (var item in Theme.GridCols ?? new Dictionary<string, string>())
                appState.GridColsStaticUtilities.TryAddUpdate(item, "grid-template-columns: {value};");

            foreach (var item in Theme.GridRows ?? new Dictionary<string, string>())
                appState.GridRowsStaticUtilities.TryAddUpdate(item, "grid-template-rows: {value};");

            foreach (var item in Theme.Order ?? new Dictionary<string, string>())
                appState.OrderStaticUtilities.TryAddUpdate(item, "order: {value};");

            foreach (var item in Theme.GridRowEnd ?? new Dictionary<string, string>())
                appState.RowEndStaticUtilities.TryAddUpdate(item, "grid-row-end: {value};");

            foreach (var item in Theme.GridRowSpan ?? new Dictionary<string, string>())
                appState.RowSpanStaticUtilities.TryAddUpdate(item, "grid-row: {value};");

            foreach (var item in Theme.GridRowStart ?? new Dictionary<string, string>())
                appState.RowStartStaticUtilities.TryAddUpdate(item, "grid-row-start: {value};");

            #endregion

            #region Flexbox

            foreach (var item in Theme.FlexBasis ?? new Dictionary<string, string>())
                appState.BasisStaticUtilities.TryAddUpdate(item, "flex-basis: {value};");

            foreach (var item in Theme.Flex ?? new Dictionary<string, string>())
                appState.FlexStaticUtilities.TryAddUpdate(item, "flex: {value};");

            foreach (var item in Theme.FlexGrow ?? new Dictionary<string, string>())
                appState.FlexStaticUtilities.TryAddUpdate(item, "flex-grow: {value};");

            foreach (var item in Theme.FlexShrink ?? new Dictionary<string, string>())
                appState.FlexStaticUtilities.TryAddUpdate(item, "flex-shrink: {value};");
            
            #endregion
            
            #region Layout
            
            foreach (var item in Theme.AspectRatio ?? new Dictionary<string, string>())
                appState.AspectStaticUtilities.TryAddUpdate(item, "aspect-ratio: {value};");
            
            #endregion
            
            #region Transforms
            
            foreach (var item in Theme.Animate ?? new Dictionary<string, string>())
                appState.AnimateStaticUtilities.TryAddUpdate(item);

            #endregion
            
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
