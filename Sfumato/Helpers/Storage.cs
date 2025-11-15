namespace Sfumato.Helpers;

public static class Storage
{
    public static async Task<string> ReadAllTextWithRetriesAsync(string filePath, int retryForMs = 3000, CancellationToken cancellationToken = default)
    {
        var fileContent = string.Empty;

        if (retryForMs < 1)
        {
            try
            {
                return await File.ReadAllTextAsync(filePath, cancellationToken);
            }
            catch
            {
                return string.Empty;
            }
        }
        
        var timerCancellationToken = new CancellationTokenSource(TimeSpan.FromMilliseconds(retryForMs));

        do
        {
            try
            {
                // ReSharper disable once PossiblyMistakenUseOfCancellationToken
                fileContent = await File.ReadAllTextAsync(filePath, cancellationToken);
                await timerCancellationToken.CancelAsync();
            }
            catch (IOException)
            {
                await Task.Delay(50, timerCancellationToken.Token);
            }
            catch
            {
                await timerCancellationToken.CancelAsync();
            }
            
        } while (timerCancellationToken.IsCancellationRequested == false && cancellationToken.IsCancellationRequested == false);

        return fileContent;
    }
}