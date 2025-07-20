namespace Argentini.Sfumato.Tests;

public class ExportTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public async Task ExportJsonAsync()
    {
        AppRunner = new AppRunner(StringBuilderPool, "../../../SampleCss/export.css");

        await AppRunner.LoadCssFileAsync();
        
        #region Utility Class Definitions
        
        var json = AppRunner.Library.ExportUtilityClassDefinitions(AppRunner);
        
        Assert.True(json.Length > 10);

        var path = Path.GetFullPath(Path.Combine("../../../utility-classes.json"));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"Utility class JSON written to {path}");

        #endregion

        #region Color Library
        
        json = AppRunner.Library.ExportColorDefinitions(AppRunner);
        
        path = Path.GetFullPath(Path.Combine("../../../colors.json"));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"Color definitions JSON written to {path}");
        
        #endregion
        
        #region CSS Custom Properties
        
        json = AppRunner.Library.ExportCssCustomProperties(AppRunner);
        
        path = Path.GetFullPath(Path.Combine("../../../css-custom-properties.json"));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"CSS Custom Properties JSON written to {path}");
        
        #endregion
        
        #region Variants
        
        json = AppRunner.Library.ExportVariants();
        
        path = Path.GetFullPath(Path.Combine("../../../variants.json"));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"Variants JSON written to {path}");
        
        #endregion
    }
}
