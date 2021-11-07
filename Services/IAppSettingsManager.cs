using Newtonsoft.Json;
using DaysGoneModManager.Models;

namespace DaysGoneModManager.Services
{
    public interface IAppSettingsManager
    {
        string GetGamePath();
        string GetLaunchCommands();
        string GetModPath();
        uint GetGameId();
        ulong GetSteamId();
        bool GetOpenInSteam();
    }


    public class AppSettingsManager : IAppSettingsManager
    {
        public AppSettingsManager(string appSettings)
        {
            AppConfigurationModel.Root configRoot = JsonConvert.DeserializeObject<AppConfigurationModel.Root>(appSettings);
            InititializeSettings(configRoot.AppConfiguration);
        }

        private AppConfigurationModel.AppConfiguration LocalAppConfiguration { get; set; }

        private void InititializeSettings(AppConfigurationModel.AppConfiguration config)
        {
            LocalAppConfiguration = config;
        }

        public string GetGamePath() => LocalAppConfiguration.GamePath;
        public string GetLaunchCommands() => LocalAppConfiguration.LaunchCommands;
        public string GetModPath() => LocalAppConfiguration.ModPath;
        public uint GetGameId() => LocalAppConfiguration.GameId;
        public ulong GetSteamId() => LocalAppConfiguration.SteamId;
        public bool GetOpenInSteam() => LocalAppConfiguration.OpenInSteam;
    }
}