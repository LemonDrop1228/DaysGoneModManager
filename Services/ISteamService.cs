using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AnyClone;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Models;
using PropertyChanged;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using static DaysGoneModManager.Helpers.AppConstants;

namespace DaysGoneModManager.Services
{
    public interface ISteamService
    {
        Task LoadPlayerData();
        void InitializeService(IAppSettingsManager appSettings, INotificationService notificationService);
        SteamPlayerDataModel GetPlayerData();
        event EventHandler PlayerDataLoaded;
        void LaunchGame();
        void RunSteamProtocol(BrowserProtocols type);
    }

    [AddINotifyPropertyChangedInterface]
    public class SteamService : ISteamService
    {
        private IAppSettingsManager _appSettings;
        private INotificationService _notificationService;

        public event EventHandler PlayerDataLoaded;
        SteamBrowserProtocols SteamBrowserProtocolValues { get; set; }
        private ulong SteamId { get; set; }
        private uint GameId { get; set; }
        SteamPlayerDataModel PlayerData { get; set; }

        public async Task LoadPlayerData()
        {
            if (SteamId == 0 || GameId == 0)
            {
                ShowSteamErrors();
                return;
            }

            try
            {
                // If you clone this repository and/or want to build this app yourself from source you need to provide a Steam API Key and replace <REDACTED> with that key...
                var webInterfaceFactory = new SteamWebInterfaceFactory("<REDACTED>");

                var steamUserInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
                var steamNewsInterface = webInterfaceFactory.CreateSteamWebInterface<SteamNews>(new HttpClient());
                var steamUserStatsInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUserStats>(new HttpClient());
                var steamPlayerInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>();

                var playerSummaryResponse = await steamUserInterface.GetPlayerSummaryAsync(SteamId);
                var newsSummaryResponse = await steamNewsInterface.GetNewsForAppAsync(GameId);
                var achievementsSummaryResponse = await steamUserStatsInterface.GetPlayerAchievementsAsync(steamId:SteamId, appId:GameId);
                var ownedGames = await steamPlayerInterface.GetOwnedGamesAsync(SteamId, includeAppInfo: true);
                var DG = ownedGames.Data.OwnedGames.FirstOrDefault(g => g.AppId == GameId);

                PlayerData = new()
                {
                    PlayerData = playerSummaryResponse.Data.Clone(),
                    Achievements = achievementsSummaryResponse.Data.Achievements.Clone().ToObservableCollection(),
                    NewsItems = newsSummaryResponse.Data.NewsItems.Clone().ToObservableCollection(),
                    PlayTime = DG.PlaytimeForever.Clone()
                };

                playerSummaryResponse = null;
                achievementsSummaryResponse = null;
                newsSummaryResponse = null;
                DG = null;
                steamPlayerInterface = null;
                steamUserStatsInterface = null;
                steamNewsInterface = null;
                steamUserInterface = null;

                webInterfaceFactory = null;

                PlayerDataLoaded(this, null);

                _notificationService.SetNotificationContent("Steam Data", "Successfully retrieved steam data.");
                _notificationService.NotifyInfo();

            }
            catch (Exception ex)
            {
                _notificationService.SetNotificationContent("Steam Error", "Unable to load Steam data.");
                _notificationService.NotifyFailure();
            }
        }

        private async Task ShowSteamErrors()
        {
            if (SteamId == 0)
            {
                _notificationService.SetNotificationContent("Steam ID Issue", "Go to settings and update your SteamID to your profile ID.");
                _notificationService.NotifyWarning();
            }
            if (GameId == 0)
            {
                _notificationService.SetNotificationContent("Steam ID Issue", "Go to settings and update your SteamID to your profile ID.");
                _notificationService.NotifyWarning();
            }

        }

        public SteamPlayerDataModel GetPlayerData() => PlayerData;

        public void InitializeService(IAppSettingsManager appSettings, INotificationService notificationService)
        {
            _appSettings = appSettings;
            _notificationService = notificationService;

            _appSettings.SettingsUpdated += AppSettingsOnSettingsUpdated;
            SteamId = _appSettings.SteamId;
            GameId = _appSettings.GameId;
            SteamBrowserProtocolValues = new();
        }

        private void AppSettingsOnSettingsUpdated(object sender, EventArgs e)
        {
            SteamId = _appSettings.SteamId;
            GameId = _appSettings.GameId;

            if(PlayerData == null)
                LoadPlayerData();
        }

        public void LaunchGame() => RunProtocol(
            SteamBrowserProtocolValues.GameLaunchAddress.Uri,
            GameId.ToString(), _appSettings.LaunchParameters);

        public void RunSteamProtocol(BrowserProtocols type)
        {
            var OpenAddess = _appSettings.OpenInSteam
                ? SteamBrowserProtocolValues.OpenInSteamAddress.Uri
                : SteamBrowserProtocolValues.OpenInPopupAddress.Uri;

            switch (@type)
            {
                case BrowserProtocols.Profile:
                    RunProtocol(SteamBrowserProtocolValues.ProfileAddress.Uri);
                    break;
                case BrowserProtocols.GameDetailsPage:
                    RunProtocol(SteamBrowserProtocolValues.GameDetailsLibraryAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.PublisherPage:
                    RunProtocol(SteamBrowserProtocolValues.OpenPublisherPageAddress.Uri);
                    break;
                case BrowserProtocols.CloseSteam:
                    RunProtocol(SteamBrowserProtocolValues.CloseSteam.Uri);
                    break;
                case BrowserProtocols.GameHub:
                    RunProtocol(SteamBrowserProtocolValues.GameHubAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.StorePage:
                    RunProtocol(SteamBrowserProtocolValues.StorePageAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.VerifyGame:
                    RunProtocol(SteamBrowserProtocolValues.VerifyGameFilesAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.GameNews:
                    RunProtocol(SteamBrowserProtocolValues.GameNewsAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.BackupGame:
                    RunProtocol(SteamBrowserProtocolValues.BackUpAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.CheckSysReqs:
                    RunProtocol(SteamBrowserProtocolValues.CheckSysReqsAddress.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.DefragGame:
                    RunProtocol(SteamBrowserProtocolValues.DefragGame.Uri, GameId.ToString());
                    break;
                case BrowserProtocols.OpenGameLibrary:
                    RunProtocol(OpenAddess, SteamLibrary);
                    break;
                case BrowserProtocols.OpenDownloads:
                    RunProtocol(OpenAddess, SteamDownloads);
                    break;
                case BrowserProtocols.OpenScreenshots:
                    RunProtocol(OpenAddess, SteamScreenshots);
                    break;
                case BrowserProtocols.OpenSettings:
                    RunProtocol(OpenAddess, SteamSettings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), @type, null);
            }
        }

        private void RunProtocol(string command, string param = null, string args = null)
        {
            var execCommand = param is null ? command : command.Format(param, args);
            Process.Start(execCommand);
        }

        public class SteamBrowserProtocols
        {
            public BrowserProtocol ProfileAddress = new() { Uri = "steam://url/SteamIDMyProfile", ProtocalType = BrowserProtocalTypes.Static};
            public BrowserProtocol OpenPublisherPageAddress = new() {Uri = "steam://publisher/playstationstudios", ProtocalType = BrowserProtocalTypes.Static};
            public BrowserProtocol CloseSteam = new() { Uri = "steam://ExitSteam", ProtocalType = BrowserProtocalTypes.Static};
            public BrowserProtocol GameDetailsLibraryAddress = new() { Uri = "steam://nav/games/details/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol GameHubAddress = new() { Uri = "steam://url/GameHub/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol StorePageAddress = new() { Uri = "steam://url/StoreAppPage/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol VerifyGameFilesAddress = new() { Uri = "steam://validate/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol GameNewsAddress = new() { Uri = "steam://appnews/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol BackUpAddress = new() { Uri = "steam://backup/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol CheckSysReqsAddress = new() { Uri = "steam://checksysreqs/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol DefragGame = new() {Uri = "steam://defrag/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol OpenInPopupAddress = new() {Uri = "steam://open/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol OpenInSteamAddress = new() {Uri = "steam://nav/{0}", ProtocalType = BrowserProtocalTypes.Formatted};
            public BrowserProtocol GameLaunchAddress = new() { Uri = "steam://run/{0}//{1}/", ProtocalType = BrowserProtocalTypes.Special};
            public BrowserProtocol GameLaunchSafeAddress = new() {Uri = "steam://runsafe/{0}", ProtocalType = BrowserProtocalTypes.Special};
        }

        public class BrowserProtocol
        {
            public string Uri { get; set; }
            public BrowserProtocalTypes ProtocalType { get; set; }
        }
    }
}