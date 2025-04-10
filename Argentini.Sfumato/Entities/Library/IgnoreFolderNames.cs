namespace Argentini.Sfumato.Entities.Library;

public static class LibraryIgnoreFolderNames
{
    public static HashSet<string> IgnoreFolderNames { get; } = new(StringComparer.Ordinal)
    {
        ".git",
        "node_modules",
        "bin",
        "obj",
        "publish",
        "temp",
    };
}