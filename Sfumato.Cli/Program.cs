// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Threading;
// ReSharper disable RedundantBoolCompare

namespace Sfumato.Cli;

internal class Program
{
	private static async Task Main(string[] args)
	{
		Service.Configuration.Arguments = args;
		Service.Configuration.UsingCli = true;

#if DEBUG
		//Service.Configuration.Arguments = args;
		//Service.Configuration.Arguments = ["watch", @"c:\code\Fynydd-Website-2024\UmbracoCms\wwwroot\stylesheets\source.css"];
		//Service.Configuration.Arguments = ["watch", "/Users/magic/Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"];
		//Service.Configuration.Arguments = ["watch", "/Users/magic/Developer/Sfumato-Web/UmbracoCms/wwwroot/stylesheets/source.css"];
		//Service.Configuration.Arguments = ["watch", "/Users/magic/Developer/Tolnedra2/UmbracoCms/wwwroot/stylesheets/source.css"];
		//Service.Configuration.Arguments = ["watch", "/Users/magic/Developer/Coursabi/Coursabi.Apps/Coursabi.Apps.Client/Coursabi.Apps.Client/wwwroot/css/source.css"];
		//Service.Configuration.Arguments = ["watch", "/Users/magic/Developer/Woordle/Woordle.Shared/wwwroot/css/source.css"];
#endif

		var cts = new CancellationTokenSource();

		if (Service.Configuration.Arguments?.Length > 0 && Service.Configuration.Arguments[0] == "watch")
		{
			if (Console.IsInputRedirected)
			{
				// Check if the escape key is sent to the stdin stream.
				// If it is, cancel the token source and exit the loop.
				// This will allow interactive quit when input is redirected.
				_ = Task.Run(async () =>
				{
					while (cts.IsCancellationRequested == false)
					{
						try
						{
							await Task.Delay(25, cts.Token);
						}
						catch (TaskCanceledException)
						{
							break;
						}

						if (Console.In.Peek() == -1)
							continue;

						if ((char)Console.In.Read() != Convert.ToChar(ConsoleKey.Escape))
							continue;

						await cts.CancelAsync();

						break;
					}
				}, cts.Token);
			}
			else
			{
				_ = Task.Run(async () =>
				{
					while (cts.IsCancellationRequested == false)
					{
						if (Console.KeyAvailable == false)
							continue;

						var keyPress = Console.ReadKey(intercept: true);

						if (keyPress.Key != ConsoleKey.Escape)
							continue;

						await cts.CancelAsync();

						break;
					}
				}, cts.Token);
			}
		}
		
		var result = await Service.RunAsync(cts);

		Environment.Exit(result ? 0 : 1);
	}
}
