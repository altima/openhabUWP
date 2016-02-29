using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace openhabUWP.Services
{
    public interface IDialogService
    {
        Task ShowContentDialog(ContentDialog dialog);
    }

    public class DialogService : IDialogService
    {
        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        public async Task ShowContentDialog(ContentDialog dialog)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                await dialog.ShowAsync();
            }
            catch
            {

            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
