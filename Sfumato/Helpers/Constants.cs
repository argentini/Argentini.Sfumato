// ReSharper disable RedundantBoolCompare

namespace Sfumato.Helpers;

public static class Constants
{
    public static string CliErrorPrefix => "Sfumato => ";
    public static string WorkingPath => Directory.GetCurrentDirectory();

    public static readonly double NsPerTick = 1_000_000_000.0 / Stopwatch.Frequency;

    public static string LoadBrowserResetCss() => LoadEmbeddedText("Sfumato.css.browser-reset.css");

    public static string LoadDefaultsCss() => LoadEmbeddedText("Sfumato.css.defaults.css");

    public static string LoadFormsCss() => LoadEmbeddedText("Sfumato.css.forms.css");

    public static string LoadSfumatoExampleCss() => LoadEmbeddedText("Sfumato.css.sfumato-example.css");

    private static string LoadEmbeddedText(string resourceName)
    {
        var asm = typeof(Constants).Assembly;

        using var stream = asm.GetManifestResourceStream(resourceName) ?? throw new FileNotFoundException($"Embedded resource not found: {resourceName}");
        using var reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }    
}