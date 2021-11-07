using System;
using DaysGoneModManager.Helpers;
using Newtonsoft.Json;
using DaysGoneModManager.Models;
using PropertyChanged;

namespace DaysGoneModManager.Services
{
    public interface IAppSettingsManager
    {
        string GamePath { get; set; }
        string ModPath { get; set; }
        string LaunchParameters { get; set; }
        ulong SteamId { get; set; }
        uint GameId { get; set; }
        bool OpenInSteam { get; set; }

        event EventHandler SettingsUpdated;

        string GetSettingsJson();
    }


    [AddINotifyPropertyChangedInterface]
    public class AppSettingsManager : IAppSettingsManager
    {
        public event EventHandler SettingsUpdated;

        public AppSettingsManager(string appSettings)
        {
            if (!appSettings.NullOrEmpty())
            {
                AppConfigurationModel.Root configRoot = JsonConvert.DeserializeObject<AppConfigurationModel.Root>(appSettings);
                InititializeSettings(configRoot.AppConfiguration);
            }
            else
            {
                InititializeSettings();
            }
        }

        private AppConfigurationModel.AppConfiguration LocalAppConfiguration { get; set; }

        // Initialize Settings
        private void InititializeSettings(AppConfigurationModel.AppConfiguration config = null)
        {
            LocalAppConfiguration = config ?? new();
        }

        public string GetSettingsJson() => JsonConvert.SerializeObject(LocalAppConfiguration, Formatting.Indented);

        public string GamePath
        {
            get { return LocalAppConfiguration.GamePath; }
            set
            {
                LocalAppConfiguration.GamePath = value;
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string ModPath
        {
            get { return LocalAppConfiguration.ModPath; }
            set { LocalAppConfiguration.ModPath = value;
                SettingsUpdated?.Invoke(this, EventArgs.Empty);

            }
        }

        public string LaunchParameters
        {
            get { return LocalAppConfiguration.LaunchCommands; }
            set
            {
                LocalAppConfiguration.LaunchCommands = value;
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public ulong SteamId
        {
            get { return LocalAppConfiguration.SteamId; }
            set
            {
                LocalAppConfiguration.SteamId = value;
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public uint GameId
        {
            get { return LocalAppConfiguration.GameId; }
            set
            {
                LocalAppConfiguration.GameId = value;
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool OpenInSteam
        {
            get { return LocalAppConfiguration.OpenInSteam; }
            set
            {
                LocalAppConfiguration.OpenInSteam = value;
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}