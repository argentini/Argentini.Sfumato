namespace Argentini.Sfumato.Entities.Library;

public static class LibraryValidFileExtensions
{
    public static HashSet<string> ValidFileExtensions { get; } = new(StringComparer.Ordinal)
    {
        // HTML
        "html",
        "pug",

        // Glimmer
        "gjs",
        "gts",

        // JS
        "astro",
        "cjs",
        "cts",
        "jade",
        "js",
        "jsx",
        "mjs",
        "mts",
        "svelte",
        "ts",
        "tsx",
        "vue",

        // Markdown
        "md",
        "mdx",

        // .NET
        "asp",
        "ascx",
        "aspx",
        "master",
        "ashx",
        "axd",
        "razor",
        "cshtml",
        "cs",
        "csx",
        "vb",
        "vbhtml",
        "fs",
        "fsx",

        // Handlebars
        "handlebars",
        "hbs",
        "mustache",

        // PHP
        "php",
        "twig",

        // Ruby
        "erb",
        "haml",
        "liquid",
        "rb",
        "rhtml",
        "slim",

        // Elixir / Phoenix
        "eex",
        "heex",

        // Nunjucks
        "njk",
        "nunjucks",

        // Python
        "py",
        "tpl",

        // Rust
        "rs",
    };
}