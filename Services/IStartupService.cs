using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaysGoneModManager.Services
{
    public interface IStartupService
    {
        event EventHandler StartupCompleted;
        Task InitializeStartup(ISteamService steamService);
    }

    public class StartupService : IStartupService
    {
        public event EventHandler StartupCompleted;
        public async Task InitializeStartup(ISteamService steamService)
        {
            await steamService.LoadPlayerData();
            await Task.Run(async () => {
                while (steamService.GetType() is null)
                {
                    Thread.Sleep(500);
                }
                StartupCompleted(this, null);
            });
        }
    }
}
