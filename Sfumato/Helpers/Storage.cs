// ReSharper disable RedundantBoolCompare
namespace Sfumato.Helpers;

public static class Storage
{
    public static async Task<string> ReadAllTextWithRetriesAsync(string filePath, int retryForMs = 3000, CancellationToken cancellationToken = default)
    {
        using var timeoutCts = new CancellationTokenSource(TimeSpan.FromMilliseconds(retryForMs));
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

        while (linkedCts.IsCancellationRequested == false)
        {
            try
            {
                return await File.ReadAllTextAsync(filePath, linkedCts.Token);
            }
            catch (IOException)
            {
                try
                {
                    await Task.Delay(50, linkedCts.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch
            {
                break;
            }
        }

        return string.Empty;
    }
}