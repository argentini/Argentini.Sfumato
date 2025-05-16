using Argentini.Sfumato.Entities.Library;

namespace Argentini.Sfumato.Tests;

public class ExportTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ExportJson()
    {
        var library = new Library();
        var json = library.ExportDefinitions();
        
        Assert.True(json.Length > 10);

        var path = Path.GetFullPath(Path.Combine("../../../sfumato-export.json"));
        
        File.WriteAllText(path, json);

        testOutputHelper.WriteLine($"JSON written to {path}");
    }
}
