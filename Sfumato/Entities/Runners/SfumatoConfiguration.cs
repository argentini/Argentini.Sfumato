// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Sfumato.Entities.Runners;

public class SfumatoConfiguration
{
    /// <summary>
    /// You must set the command line arguments.
    /// </summary>
    public string[]? Arguments { get; set; }

    /// <summary>
    /// When true will show CLI-specific output.
    /// </summary>
    public bool UsingCli { get; set; }
}