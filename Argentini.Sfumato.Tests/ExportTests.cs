using Argentini.Sfumato.Entities.Library;

namespace Argentini.Sfumato.Tests;

public class ExportTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task ExportJsonAsync()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css");

        await appRunner.LoadCssFileAsync();
        
        var json = appRunner.Library.ExportDefinitions(appRunner);
        
        Assert.True(json.Length > 10);

        var path = Path.GetFullPath(Path.Combine("../../../sfumato-export.json"));
        
        await File.WriteAllTextAsync(path, json);

        testOutputHelper.WriteLine($"JSON written to {path}");
    }
}
