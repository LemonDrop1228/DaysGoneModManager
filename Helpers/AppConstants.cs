using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaysGoneModManager.Helpers
{
    public static class AppConstants
    {
        public enum ViewTypes
        {
            Game,
            News,
            Mods,
            Settings
        }

        public enum BrowserProtocalTypes
        {
            Static,
            Formatted,
            Special
        }

        public enum BrowserProtocols
        {
            Profile,
            GameDetailsPage,
            PublisherPage,
            CloseSteam,
            GameHub,
            StorePage,
            VerifyGame,
            GameNews,
            BackupGame,
            CheckSysReqs,
            DefragGame,
            OpenGameLibrary,
            OpenDownloads,
            OpenScreenshots,
            OpenSettings
        }

        #region Steam Constants

        public static readonly string SteamLibrary = "games";
        public static readonly string SteamDownloads = "downloads";
        public static readonly string SteamScreenshots = "screenshots";
        public static readonly string SteamSettings = "settings";

        #endregion

        #region Mod Constants

        public static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string GameAppDataPath = $"{AppDataPath}\\BendGame\\";
        public static readonly string GameAppDataPaksPath = $"{GameAppDataPath}\\Paks";
        public static readonly string SFPaksPathPart = "\\BendGame\\Content\\sfpaks\\startuppackages.pak";
        public static readonly string GameExePathPart = "\\BendGame\\Binaries\\Win64\\DaysGone.exe";

        #endregion
    }
}
