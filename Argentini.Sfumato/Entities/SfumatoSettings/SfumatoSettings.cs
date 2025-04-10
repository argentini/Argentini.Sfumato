using Mapster;
using YamlDotNet.Serialization;

namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class SfumatoSettings
{
    public List<ProjectPath> ProjectPaths { get; set;  } = [];
    public string DarkMode { get; set; } = "media";
    public bool UseAutoTheme { get; set; }

    public Theme Theme { get; set; } = new();

    public async Task LoadSettingsAsync(SfumatoAppState appState)
    {
        #region Find yml file

        if (string.IsNullOrEmpty(appState.WorkingPathOverride) == false)
            appState.WorkingPath = appState.WorkingPathOverride;
        
        appState.SettingsFilePath = Path.Combine(appState.WorkingPath, appState.FileNameOverride);

        if (File.Exists(appState.SettingsFilePath) == false)
        {
            await Console.Out.WriteLineAsync($"Could not find ${appState.FileNameOverride} settings file at path {appState.WorkingPath}");
            await Console.Out.WriteLineAsync("Use command `sfumato help` for assistance");
            Environment.Exit(1);
        }

        #endregion

        try
        {
            #region Load sfumato.yml file

            var yaml = await Storage.ReadAllTextWithRetriesAsync(appState.SettingsFilePath, SfumatoAppState.FileAccessRetryMs);
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            var yamlSettings = deserializer.Deserialize<SfumatoSettings>(yaml);

            yamlSettings.Adapt(this);
            
            #region Dark Mode
            
            DarkMode = DarkMode.ToLower() switch
            {
                "media" => "media",
                "system" => "media",
                "class" => "class",
                _ => "media"
            };

            UseAutoTheme = yamlSettings.UseAutoTheme;
            
            #endregion
            
            #region Project Paths

            var invalidExtensions = new[] { "css", "map" };

            if (yamlSettings.ProjectPaths.Count == 0)
            {
                yamlSettings.ProjectPaths.Add(new ProjectPath());
            }
            
            foreach (var projectPath in ProjectPaths.ToList())
            {
                projectPath.Path = Path.Combine(appState.WorkingPath, projectPath.Path.SetNativePathSeparators());

                if (projectPath.ExtensionsList.Count > 0)
                    projectPath.Extensions = string.Join(',', projectPath.ExtensionsList);

                if (projectPath.IgnoreFoldersList.Count > 0)
                    projectPath.IgnoreFolders = string.Join(',', projectPath.IgnoreFoldersList);

                if (string.IsNullOrEmpty(projectPath.Extensions) || invalidExtensions.Contains(projectPath.Extensions))
                    ProjectPaths.Remove(projectPath);
            }

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
            
            foreach (var item in Theme.TextSize ?? new Dictionary<string, string>())
                appState.TextSizeOptions.TryAddUpdate(item);

            foreach (var item in Theme.Leading ?? new Dictionary<string, string>())
                appState.LeadingOptions.TryAddUpdate(item);

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
            
            foreach (var item in Theme.GridCol ?? new Dictionary<string, string>())
                appState.ColStaticUtilities.TryAddUpdate(item, "grid-column: {value};");

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

            #region Interactivity
            
            foreach (var item in Theme.Cursor ?? new Dictionary<string, string>())
                appState.CursorStaticUtilities.TryAddUpdate(item, "cursor: {value};");

            foreach (var item in Theme.PointerEvents ?? new Dictionary<string, string>())
                appState.PointerEventsStaticUtilities.TryAddUpdate(item, "pointer-events: {value};");

            foreach (var item in Theme.ScrollMargin ?? new Dictionary<string, string>())
            {
                appState.ScrollMStaticUtilities.TryAddUpdate(item, "scroll-margin: {value};");
                appState.ScrollMbStaticUtilities.TryAddUpdate(item, "scroll-margin-bottom: {value};");
                appState.ScrollMeStaticUtilities.TryAddUpdate(item, "scroll-margin-inline-end: {value};");
                appState.ScrollMlStaticUtilities.TryAddUpdate(item, "scroll-margin-left: {value};");
                appState.ScrollMrStaticUtilities.TryAddUpdate(item, "scroll-margin-right: {value};");
                appState.ScrollMsStaticUtilities.TryAddUpdate(item, "scroll-margin-inline-start: {value};");
                appState.ScrollMtStaticUtilities.TryAddUpdate(item, "scroll-margin-top: {value};");
                appState.ScrollMxStaticUtilities.TryAddUpdate(item, """
                                                                    scroll-margin-left: {value};
                                                                    scroll-margin-right: {value};
                                                                    """);
                appState.ScrollMyStaticUtilities.TryAddUpdate(item, """
                                                                    scroll-margin-top: {value};
                                                                    scroll-margin-bottom: {value};
                                                                    """);
            }

            foreach (var item in Theme.ScrollPadding ?? new Dictionary<string, string>())
            {
                appState.ScrollPStaticUtilities.TryAddUpdate(item, "scroll-padding: {value};");
                appState.ScrollPbStaticUtilities.TryAddUpdate(item, "scroll-padding-bottom: {value};");
                appState.ScrollPeStaticUtilities.TryAddUpdate(item, "scroll-padding-inline-end: {value};");
                appState.ScrollPlStaticUtilities.TryAddUpdate(item, "scroll-padding-left: {value};");
                appState.ScrollPrStaticUtilities.TryAddUpdate(item, "scroll-padding-right: {value};");
                appState.ScrollPsStaticUtilities.TryAddUpdate(item, "scroll-padding-inline-start: {value};");
                appState.ScrollPtStaticUtilities.TryAddUpdate(item, "scroll-padding-top: {value};");
                appState.ScrollPxStaticUtilities.TryAddUpdate(item, """
                                                                    scroll-padding-left: {value};
                                                                    scroll-padding-right: {value};
                                                                    """);
                appState.ScrollPyStaticUtilities.TryAddUpdate(item, """
                                                                    scroll-padding-top: {value};
                                                                    scroll-padding-bottom: {value};
                                                                    """);
            }

            foreach (var item in Theme.UserSelect ?? new Dictionary<string, string>())
                appState.SelectStaticUtilities.TryAddUpdate(item, "user-select: {value};");

            #endregion

            #region Layout
            
            foreach (var item in Theme.AspectRatio ?? new Dictionary<string, string>())
                appState.AspectStaticUtilities.TryAddUpdate(item, "aspect-ratio: {value};");
            
            foreach (var item in Theme.Bottom ?? new Dictionary<string, string>())
                appState.BottomStaticUtilities.TryAddUpdate(item, "bottom: {value};");

            foreach (var item in Theme.Box ?? new Dictionary<string, string>())
                appState.BoxStaticUtilities.TryAddUpdate(item, "box-sizing: {value};");

            foreach (var item in Theme.BoxDecoration ?? new Dictionary<string, string>())
                appState.BoxDecorationStaticUtilities.TryAddUpdate(item, "box-decoration-break: {value};");

            foreach (var item in Theme.BreakAfter ?? new Dictionary<string, string>())
                appState.BreakAfterStaticUtilities.TryAddUpdate(item, "break-after: {value};");

            foreach (var item in Theme.BreakBefore ?? new Dictionary<string, string>())
                appState.BreakBeforeStaticUtilities.TryAddUpdate(item, "break-before: {value};");

            foreach (var item in Theme.BreakInside ?? new Dictionary<string, string>())
                appState.BreakInsideStaticUtilities.TryAddUpdate(item, "break-inside: {value};");

            foreach (var item in Theme.Clear ?? new Dictionary<string, string>())
                appState.ClearStaticUtilities.TryAddUpdate(item, "clear: {value};");

            foreach (var item in Theme.Columns ?? new Dictionary<string, string>())
                appState.ColumnsStaticUtilities.TryAddUpdate(item, "columns: {value};");

            foreach (var item in Theme.Container ?? new Dictionary<string, string>())
                appState.ContainerStaticUtilities.TryAddUpdate(item, "{value}");

            foreach (var item in Theme.Elastic ?? new Dictionary<string, string>())
                appState.ElasticStaticUtilities.TryAddUpdate(item, "{value}");
            
            foreach (var item in Theme.InsetEnd ?? new Dictionary<string, string>())
                appState.EndStaticUtilities.TryAddUpdate(item, "inset-inline-end: {value};");

            foreach (var item in Theme.InsetStart ?? new Dictionary<string, string>())
                appState.StartStaticUtilities.TryAddUpdate(item, "inset-inline-start: {value};");

            foreach (var item in Theme.Inset ?? new Dictionary<string, string>())
                appState.InsetStaticUtilities.TryAddUpdate(item, "inset: {value};");

            foreach (var item in Theme.InsetX ?? new Dictionary<string, string>())
                appState.InsetXStaticUtilities.TryAddUpdate(item, """
                                                                  left: {value};
                                                                  right: {value};
                                                                  """);

            foreach (var item in Theme.InsetY ?? new Dictionary<string, string>())
                appState.InsetYStaticUtilities.TryAddUpdate(item, """
                                                                  top: {value};
                                                                  bottom: {value};
                                                                  """);

            foreach (var item in Theme.Isolate ?? new Dictionary<string, string>())
                appState.IsolateStaticUtilities.TryAddUpdate(item, "isolation: {value};");

            foreach (var item in Theme.Left ?? new Dictionary<string, string>())
                appState.LeftStaticUtilities.TryAddUpdate(item, "left: {value};");

            foreach (var item in Theme.Right ?? new Dictionary<string, string>())
                appState.RightStaticUtilities.TryAddUpdate(item, "right: {value};");

            foreach (var item in Theme.Top ?? new Dictionary<string, string>())
                appState.TopStaticUtilities.TryAddUpdate(item, "top: {value};");

            foreach (var item in Theme.ZIndex ?? new Dictionary<string, string>())
                appState.ZStaticUtilities.TryAddUpdate(item, "z-index: {value};");

            #endregion

            #region Sizing
            
            foreach (var item in Theme.Height ?? new Dictionary<string, string>())
                appState.HStaticUtilities.TryAddUpdate(item, "height: {value};");

            foreach (var item in Theme.MinHeight ?? new Dictionary<string, string>())
                appState.MinHStaticUtilities.TryAddUpdate(item, "min-height: {value};");

            foreach (var item in Theme.MaxHeight ?? new Dictionary<string, string>())
                appState.MaxHStaticUtilities.TryAddUpdate(item, "max-height: {value};");

            foreach (var item in Theme.Width ?? new Dictionary<string, string>())
                appState.WStaticUtilities.TryAddUpdate(item, "width: {value};");

            foreach (var item in Theme.MinWidth ?? new Dictionary<string, string>())
                appState.MinWStaticUtilities.TryAddUpdate(item, "min-width: {value};");

            foreach (var item in Theme.MaxWidth ?? new Dictionary<string, string>())
                appState.MaxWStaticUtilities.TryAddUpdate(item, "max-width: {value};");

            foreach (var item in Theme.Size ?? new Dictionary<string, string>())
                appState.SizeStaticUtilities.TryAddUpdate(item, "width: {value}; height: {value};");

            #endregion

            #region Spacing
            
            foreach (var item in Theme.Margin ?? new Dictionary<string, string>())
            {
                appState.MStaticUtilities.TryAddUpdate(item, "margin: {value};");
                appState.MbStaticUtilities.TryAddUpdate(item, "margin-bottom: {value};");
                appState.MeStaticUtilities.TryAddUpdate(item, "margin-inline-end: {value};");
                appState.MlStaticUtilities.TryAddUpdate(item, "margin-left: {value};");
                appState.MrStaticUtilities.TryAddUpdate(item, "margin-right: {value};");
                appState.MsStaticUtilities.TryAddUpdate(item, "margin-inline-start: {value};");
                appState.MtStaticUtilities.TryAddUpdate(item, "margin-top: {value};");
                appState.MxStaticUtilities.TryAddUpdate(item, """
                                                                    margin-left: {value};
                                                                    margin-right: {value};
                                                                    """);
                appState.MyStaticUtilities.TryAddUpdate(item, """
                                                                    margin-top: {value};
                                                                    margin-bottom: {value};
                                                                    """);
            }
            
            foreach (var item in Theme.Padding ?? new Dictionary<string, string>())
            {
                appState.PStaticUtilities.TryAddUpdate(item, "padding: {value};");
                appState.PbStaticUtilities.TryAddUpdate(item, "padding-bottom: {value};");
                appState.PeStaticUtilities.TryAddUpdate(item, "padding-inline-end: {value};");
                appState.PlStaticUtilities.TryAddUpdate(item, "padding-left: {value};");
                appState.PrStaticUtilities.TryAddUpdate(item, "padding-right: {value};");
                appState.PsStaticUtilities.TryAddUpdate(item, "padding-inline-start: {value};");
                appState.PtStaticUtilities.TryAddUpdate(item, "padding-top: {value};");
                appState.PxStaticUtilities.TryAddUpdate(item, """
                                                              padding-left: {value};
                                                              padding-right: {value};
                                                              """);
                appState.PyStaticUtilities.TryAddUpdate(item, """
                                                              padding-top: {value};
                                                              padding-bottom: {value};
                                                              """);
            }

            foreach (var item in Theme.SpaceX ?? new Dictionary<string, string>())
                appState.SpaceXStaticUtilities.TryAddUpdate(item, """
                                                                  & > * + * {
                                                                      margin-left: {value};
                                                                  }
                                                                  """);

            foreach (var item in Theme.SpaceY ?? new Dictionary<string, string>())
                appState.SpaceYStaticUtilities.TryAddUpdate(item, """
                                                                  & > * + * {
                                                                      margin-top: {value};
                                                                  }
                                                                  """);

            #endregion

            #region SVG
            
            foreach (var item in Theme.SvgStroke ?? new Dictionary<string, string>())
                appState.StrokeStaticUtilities.TryAddUpdate(item, "stroke: {value};");

            #endregion

            #region Tables
            
            foreach (var item in Theme.BorderSpacing ?? new Dictionary<string, string>())
                appState.BorderSpacingStaticUtilities.TryAddUpdate(item, "border-spacing: {value}; --sf-border-spacing-x: {value}; --sf-border-spacing-y: {value};");

            foreach (var item in Theme.BorderSpacingX ?? new Dictionary<string, string>())
                appState.BorderSpacingXStaticUtilities.TryAddUpdate(item, "border-spacing: {value} var(--sf-border-spacing-y); --sf-border-spacing-x: {value};");

            foreach (var item in Theme.BorderSpacingY ?? new Dictionary<string, string>())
                appState.BorderSpacingYStaticUtilities.TryAddUpdate(item, "border-spacing: var(--sf-border-spacing-x) {value}; --sf-border-spacing-y: {value};");

            foreach (var item in Theme.CaptionSide ?? new Dictionary<string, string>())
                appState.CaptionStaticUtilities.TryAddUpdate(item, "caption-side: {value};");

            #endregion

            #region Transforms
            
            foreach (var item in Theme.Animate ?? new Dictionary<string, string>())
                appState.AnimateStaticUtilities.TryAddUpdate(item);

            foreach (var item in Theme.Transition ?? new Dictionary<string, string>())
                appState.TransitionStaticUtilities.TryAddUpdate(item);

            foreach (var item in Theme.TransitionDelay ?? new Dictionary<string, string>())
                appState.DelayStaticUtilities.TryAddUpdate(item, "transition-delay: {value};");

            foreach (var item in Theme.TransitionDuration ?? new Dictionary<string, string>())
                appState.DurationStaticUtilities.TryAddUpdate(item, "transition-duration: {value};");

            foreach (var item in Theme.TransitionTiming ?? new Dictionary<string, string>())
                appState.EaseStaticUtilities.TryAddUpdate(item, "transition-timing: {value};");
            
            #endregion

            #region Transitions And Animations
            
            foreach (var item in Theme.Origin ?? new Dictionary<string, string>())
                appState.OriginStaticUtilities.TryAddUpdate(item, "transform-origin: {value};");

            foreach (var item in Theme.Rotate ?? new Dictionary<string, string>())
                appState.RotateStaticUtilities.TryAddUpdate(item, "transform: rotate({value});");

            foreach (var item in Theme.Scale ?? new Dictionary<string, string>())
                appState.ScaleStaticUtilities.TryAddUpdate(item, "transform: scale({value});");

            foreach (var item in Theme.ScaleX ?? new Dictionary<string, string>())
                appState.ScaleXStaticUtilities.TryAddUpdate(item, "transform: scaleX({value});");

            foreach (var item in Theme.ScaleY ?? new Dictionary<string, string>())
                appState.ScaleYStaticUtilities.TryAddUpdate(item, "transform: scaleY({value});");

            foreach (var item in Theme.SkewX ?? new Dictionary<string, string>())
                appState.SkewXStaticUtilities.TryAddUpdate(item, "transform: skewX({value});");

            foreach (var item in Theme.SkewY ?? new Dictionary<string, string>())
                appState.SkewYStaticUtilities.TryAddUpdate(item, "transform: skewY({value});");

            foreach (var item in Theme.TranslateX ?? new Dictionary<string, string>())
                appState.TranslateXStaticUtilities.TryAddUpdate(item, "transform: translateX({value});");

            foreach (var item in Theme.TranslateY ?? new Dictionary<string, string>())
                appState.TranslateYStaticUtilities.TryAddUpdate(item, "transform: translateY({value});");

            #endregion

            #region Typography
            
            foreach (var item in Theme.DecorationThickness ?? new Dictionary<string, string>())
                appState.DecorationStaticUtilities.TryAddUpdate(item, "text-decoration-thickness: {value};");

            foreach (var item in Theme.FontFamily ?? new Dictionary<string, string>())
                appState.FontStaticUtilities.TryAddUpdate(item, "font-family: {value};");
            
            foreach (var item in Theme.FontWeight ?? new Dictionary<string, string>())
                appState.FontStaticUtilities.TryAddUpdate(item, "font-weight: {value};");
            
            foreach (var item in Theme.LineClamp ?? new Dictionary<string, string>())
                appState.LineClampStaticUtilities.TryAddUpdate(item, """
                                                                     -webkit-line-clamp: {value};
                                                                     overflow: hidden;
                                                                     display: -webkit-box;
                                                                     -webkit-box-orient: vertical;
                                                                     """);
            
            foreach (var item in Theme.ListStylePosition ?? new Dictionary<string, string>())
                appState.ListStaticUtilities.TryAddUpdate(item, "list-style-position: {value};");
            
            foreach (var item in Theme.ListStyleType ?? new Dictionary<string, string>())
                appState.ListStaticUtilities.TryAddUpdate(item, "list-style-type: {value};");
            
            foreach (var item in Theme.ListStyleImage ?? new Dictionary<string, string>())
                appState.ListImageStaticUtilities.TryAddUpdate(item, "list-style-image: {value};");

            foreach (var item in Theme.TextSize ?? new Dictionary<string, string>())
                appState.TextAlignStaticUtilities.TryAddUpdate(item, "font-size: {value};");

            foreach (var item in Theme.TextAlign ?? new Dictionary<string, string>())
                appState.TextAlignStaticUtilities.TryAddUpdate(item, "text-align: {value};");
            
            foreach (var item in Theme.Tracking ?? new Dictionary<string, string>())
                appState.TrackingStaticUtilities.TryAddUpdate(item, "letter-spacing: {value};");

            foreach (var item in Theme.UnderlineOffset ?? new Dictionary<string, string>())
                appState.UnderlineOffsetStaticUtilities.TryAddUpdate(item, "text-underline-offset: {value};");

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
