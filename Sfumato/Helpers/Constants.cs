// ReSharper disable RedundantBoolCompare

namespace Sfumato.Helpers;

public static class Constants
{
    public static string CliErrorPrefix => "Sfumato => ";
    public static string EmbeddedCssPath => GetEmbeddedCssPath();
    public static string WorkingPath => Directory.GetCurrentDirectory();

    public static readonly double NsPerTick = 1_000_000_000.0 / Stopwatch.Frequency;

    private static string GetEmbeddedCssPath()
    {
        var workingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        while (workingPath.LastIndexOf(Path.DirectorySeparatorChar) > -1)
        {
            workingPath = workingPath[..workingPath.LastIndexOf(Path.DirectorySeparatorChar)];

            if (Directory.Exists(Path.Combine(workingPath, "contentFiles")))
            {
                workingPath = Path.Combine(workingPath, "contentFiles", "any", "any", "css");
                break;
            }		    
			
            if (Directory.Exists(Path.Combine(workingPath, "css")))
            {
                workingPath = Path.Combine(workingPath, "css");
                break;
            }		    
        }

        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(workingPath) || Directory.Exists(workingPath) == false)
        {
            $"{CliErrorPrefix}Embedded CSS resources cannot be found.".WriteToOutput();
            Environment.Exit(1);
        }
        
        return workingPath;
    }
}