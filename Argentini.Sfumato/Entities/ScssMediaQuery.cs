namespace Argentini.Sfumato.Entities;

public sealed class ScssMediaQuery
{
    public SfumatoAppState AppState { get; }
    public string Prefix { get; } // Current prefix (e.g. 'md')
    public string PrefixPath { get; } // Partial variant path up to this point (e.g. 'dark:md:')
    public string Selector { get; } // Selector with opening brace (e.g. '@media (...) {')
    public List<ScssMediaQuery> MediaQueries { get; } = [];
    public List<ScssClass> ScssClasses { get; } = [];
    public int Depth { get; } // Zero-based indentation depth

    /// <summary>
    /// Create an SCSS graph media query node.
    /// </summary>
    /// <param name="appState"></param>
    /// <param name="prefixPath">Partial variant path up to this point (e.g. 'dark:md:')</param>
    public ScssMediaQuery(SfumatoAppState appState, string mediaQueryPrefixPath, int depth = -2)
    {
        var mediaQueryPrefixPaths = mediaQueryPrefixPath.Split(':', StringSplitOptions.RemoveEmptyEntries); 
        
        AppState = appState;
        PrefixPath = mediaQueryPrefixPath.TrimEnd(':') + ':';
        Prefix = mediaQueryPrefixPaths.Length > 0 ? mediaQueryPrefixPaths.Last() : string.Empty;
        Selector = AppState.MediaQueryPrefixes.FirstOrDefault(m => m.Prefix == Prefix)?.Statement ?? string.Empty;
        Depth = depth;

        if (depth == -2)
            Depth = mediaQueryPrefixPaths.Length - 1 < 0 ? 0 : mediaQueryPrefixPaths.Length - 1;
    }
    
    /// <summary>
    /// Get the SCSS markup for this SCSS graph node.
    /// </summary>
    /// <returns></returns>
    public string GetScssMarkup()
    {
        var scss = AppState.StringBuilderPool.Get();

        try
        {
            if (Prefix == "dark" && AppState.Settings.DarkMode.InvariantEquals("class"))
            {
                #region Handle root tag classes as "special case"

                foreach (var scssClass in ScssClasses)
                {
                    foreach (var selector in scssClass.Selectors.ToList())
                    {
                        if (AppState.HtmlTagClasses.Contains(selector.TrimStart('.').Replace("\\", string.Empty)))
                        {
                            scssClass.Selectors.Add($"html{selector}");
                        }
                    }
                }

                #endregion

                scss.Append($"html.theme-dark {{{Environment.NewLine}");

                foreach (var scssClass in ScssClasses)
                {
                    scss.Append(scssClass.GetScssMarkup());
                }

                foreach (var mediaQuery in MediaQueries)
                {
                    scss.Append(mediaQuery.GetScssMarkup());
                }

                scss.Append($"}}{Environment.NewLine}");

                if (AppState.Settings.UseAutoTheme == false)
                    return scss.ToString();

                scss.Append($"html.theme-auto {{ {AppState.MediaQueryPrefixes.First(p => p.Prefix == "dark").Statement}{Environment.NewLine}");

                foreach (var scssClass in ScssClasses)
                {
                    scss.Append(scssClass.GetScssMarkup());
                }

                foreach (var mediaQuery in MediaQueries)
                {
                    scss.Append(mediaQuery.GetScssMarkup());
                }

                scss.Append($"}} }}{Environment.NewLine}");
            }

            else
            {
                if (Depth > -1)
                    scss.Append(Selector.Indent(Depth * 4) + Environment.NewLine);

                foreach (var scssClass in ScssClasses)
                {
                    scss.Append(scssClass.GetScssMarkup());
                }

                foreach (var mediaQuery in MediaQueries)
                {
                    scss.Append(mediaQuery.GetScssMarkup());
                }

                if (Depth > -1)
                    scss.Append($"}}{Environment.NewLine}".Indent(Depth * 4));
            }

            return scss.ToString();
        }

        catch
        {
            return string.Empty;
        }

        finally
        {
            AppState.StringBuilderPool.Return(scss);
        }
    }
}
