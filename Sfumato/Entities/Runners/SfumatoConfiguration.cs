using Microsoft.Extensions.Logging;

namespace Sfumato.Entities.Runners;

public class SfumatoConfiguration
{
    /// <summary>
    /// Optionally set a logger to redirect output.
    /// This should be set when using the Sfumato package in your project. 
    /// </summary>
    public ILogger? Logger { get; set; }

    /// <summary>
    /// You must set the command line arguments.
    /// </summary>
    public string[]? Arguments { get; set; }
}