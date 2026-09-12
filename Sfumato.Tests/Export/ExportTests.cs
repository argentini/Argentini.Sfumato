namespace Sfumato.Tests.Export;

public class ExportTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public async Task ExportJsonAsync()
    {
        AppRunner = new AppRunner(StringBuilderPool, ExportCssFilePath);

        Assert.True(File.Exists(ExportCssFilePath));
        
        await AppRunner.LoadCssFileAsync();
        
        #region Utility Class Definitions
        
        var json = AppRunner.ExportUtilityClassDefinitions();
        
        Assert.True(json.Length > 10);

        var path = Path.GetFullPath(Path.Combine("../../../", UmbracoHelpExportNames.UtilityClasses));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"Utility class JSON written to {path}");

        #endregion

        #region Color Library
        
        json = AppRunner.ExportColorDefinitions();
        
        path = Path.GetFullPath(Path.Combine("../../../", UmbracoHelpExportNames.Colors));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"Color definitions JSON written to {path}");
        
        #endregion
        
        #region CSS Custom Properties
        
        json = AppRunner.ExportCssCustomProperties();
        
        path = Path.GetFullPath(Path.Combine("../../../", UmbracoHelpExportNames.CssCustomProperties));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"CSS Custom Properties JSON written to {path}");
        
        #endregion
        
        #region Variants
        
        json = AppRunner.ExportVariants();
        
        path = Path.GetFullPath(Path.Combine("../../../", UmbracoHelpExportNames.Variants));
        
        await File.WriteAllTextAsync(path, json);

        TestOutputHelper?.WriteLine($"Variants JSON written to {path}");
        
        #endregion
    }
}
