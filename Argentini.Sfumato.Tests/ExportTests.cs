namespace Argentini.Sfumato.Tests;

public class ExportTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task ExportJsonAsync()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/export.css");

        await appRunner.LoadCssFileAsync();
        
        #region Utility Class Definitions
        
        var json = appRunner.Library.ExportUtilityClassDefinitions(appRunner);
        
        Assert.True(json.Length > 10);

        var path = Path.GetFullPath(Path.Combine("../../../utility-classes.json"));
        
        await File.WriteAllTextAsync(path, json);

        testOutputHelper.WriteLine($"Utility class JSON written to {path}");

        #endregion

        #region Color Library
        
        json = appRunner.Library.ExportColorDefinitions(appRunner);
        
        path = Path.GetFullPath(Path.Combine("../../../colors.json"));
        
        await File.WriteAllTextAsync(path, json);

        testOutputHelper.WriteLine($"Color definitions JSON written to {path}");
        
        #endregion
        
        #region CSS Custom Properties
        
        json = appRunner.Library.ExportCssCustomProperties(appRunner);
        
        path = Path.GetFullPath(Path.Combine("../../../css-custom-properties.json"));
        
        await File.WriteAllTextAsync(path, json);

        testOutputHelper.WriteLine($"CSS Custom Properties JSON written to {path}");
        
        #endregion
        
        #region Variants
        
        json = appRunner.Library.ExportVariants(appRunner);
        
        path = Path.GetFullPath(Path.Combine("../../../variants.json"));
        
        await File.WriteAllTextAsync(path, json);

        testOutputHelper.WriteLine($"Variants JSON written to {path}");
        
        #endregion
    }
}
