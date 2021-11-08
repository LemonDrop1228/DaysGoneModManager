using System.Threading;
using System.Threading.Tasks;
using DaysGoneModManager.Helpers;
using NexusModsNET;
using NexusModsNET.DataModels;
using NexusModsNET.Inquirers;

namespace DaysGoneModManager.Services
{
    public interface INexusModsService
    {
        void InitializeService(IAppSettingsManager appSettingsManager);
        Task TestClients();
    }

    public class NexusModsService : INexusModsService
    {
        private IAppSettingsManager _appSettingsManager;
        string ApiKey { get => _appSettingsManager.NexusApiKey;}
        IInquirerFactory InquirerFactory { get; set; }
        IGamesInquirer GameClient { get; set; }
        IModsInquirer ModClient { get; set; }
        IInfosInquirer InfoClient { get; set; }

        public void InitializeService(IAppSettingsManager appSettingsManager)
        {
            _appSettingsManager = appSettingsManager;
            if (!ApiKey.NullOrEmpty())
                InitializeClient();
        }

        private void InitializeClient()
        {
            InquirerFactory = NexusModsFactory.New(ApiKey);
            GameClient = InquirerFactory.CreateGamesInquirer();
            ModClient = InquirerFactory.CreateModsInquirer();
            InfoClient = InquirerFactory.CreateInfosInquirer();
        }

        public async Task TestClients()
        {
            //NexusGame res = await GameClient.GetGameAsync("daysgone", new CancellationToken());
            // var res = ModClient.
        }

    }
}