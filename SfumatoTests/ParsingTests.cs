using System;
using System.Linq;
using System.Threading.Tasks;
using Sfumato;
using Xunit.Abstractions;

namespace SfumatoTests;

public class ParsingTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ParsingTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ParsingTest()
    {
        _testOutputHelper.WriteLine("ParsingTest");
        
        var runner = new SfumatoRunner(Array.Empty<string>());

        await runner.InitializeAsync();
        await runner.GatherUsedClassesAsync();

        var scssClass = runner.UsedClasses.FirstOrDefault(c => c.ClassName == "first:dark:hover:bg-fuchsia-600");

        Assert.NotNull(scssClass);
        
        var result = await runner.GenerateScssClassAsync(scssClass);
        
        Assert.Equal(@".first\:dark\:hover\:bg-fuchsia-600 {
    &:first-child {
        @media (prefers-color-scheme: dark) {
            &:hover {
                background-color: rgb(192 38 211);
            }
        }
    }
}
", result);
        
        result = await runner.GenerateScssClassAsync(scssClass, "first:dark:");
        
        Assert.Equal(@".first\:dark\:hover\:bg-fuchsia-600 {
    &:hover {
        background-color: rgb(192 38 211);
    }
}
", result);
        
        scssClass = runner.UsedClasses.FirstOrDefault(c => c.ClassName == "bg-fuchsia-50");

        Assert.NotNull(scssClass);
        
        result = await runner.GenerateScssClassAsync(scssClass);
        
        Assert.Equal(".bg-fuchsia-50 { background-color: rgb(253 244 255); }", result);
    }
}
