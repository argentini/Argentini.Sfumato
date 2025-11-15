// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ClassNeverInstantiated.Global

namespace Sfumato.Cli;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var sfumato = new SfumatoService();
		
		await sfumato.InitializeAsync(args);
	}
}
