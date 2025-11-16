// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ClassNeverInstantiated.Global

namespace Sfumato.Cli;

internal class Program
{
	private static async Task Main(string[] args)
	{
		SfumatoService.Configuration?.Arguments = args;

#if DEBUG
		//SfumatoService.Configuration?.Arguments = args;
		//SfumatoService.Configuration?.Arguments = ["watch", @"c:\code\Fynydd-Website-2024\UmbracoCms\wwwroot\stylesheets\source.css"];
		SfumatoService.Configuration?.Arguments = ["watch", "/Users/magic/Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"];
		//SfumatoService.Configuration?.Arguments = ["watch", "/Users/magic/Developer/Sfumato-Web/UmbracoCms/wwwroot/stylesheets/source.css"];
		//SfumatoService.Configuration?.Arguments = ["watch", "/Users/magic/Developer/Tolnedra2/UmbracoCms/wwwroot/stylesheets/source.css"];
		//SfumatoService.Configuration?.Arguments = ["watch", "/Users/magic/Developer/Coursabi/Coursabi.Apps/Coursabi.Apps.Client/Coursabi.Apps.Client/wwwroot/css/source.css"];
		//SfumatoService.Configuration?.Arguments = ["watch", "/Users/magic/Developer/Woordle/Woordle.Shared/wwwroot/css/source.css"];
#endif
		
		await SfumatoService.InitializeCliAsync();
	}
}
