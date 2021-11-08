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
        string NexusApiKey { get; set; }
        bool UseUISounds { get; set; }
        bool HealthCheck { get; }
        bool CloseOnPlay { get; set; }

        event EventHandler SettingsUpdated;

        string GetSettingsJson();
    }


    [AddINotifyPropertyChangedInterface]
    public class AppSettingsManager : IAppSettingsManager
    {
        public event EventHandler SettingsUpdated;

        public bool HealthCheck { get; private set; }

        private bool PerformHealthCheck() => SteamId != 0 && GameId != 0 && !ModPath.NullOrEmpty() && !GamePath.NullOrEmpty() && !NexusApiKey.NullOrEmpty();

        public AppSettingsManager(string appSettings)
        {
            if (!appSettings.NullOrEmpty())
            {
                AppConfigurationModel.Root configRoot = JsonConvert.DeserializeObject<AppConfigurationModel.Root>(appSettings);
                InititializeSettings(configRoot);
            }
            else
            {
                InititializeSettings();
            }

            HealthCheck = PerformHealthCheck();
        }

        private AppConfigurationModel.Root LocalAppConfiguration { get; set; }

        // Initialize Settings
        private void InititializeSettings(AppConfigurationModel.Root config = null)
        {
            LocalAppConfiguration = config ?? new()
            {
                AppConfiguration = new()
                {
                    GamePath = "",
                    SteamId = 0,
                    LaunchCommands = "",
                    GameId = 1259420,
                    ModPath = "",
                    OpenInSteam = true,
                    NexusApiKey = "",
                    UseUISounds = true,
                    CloseOnPlay = false
                }
            };
        }

        public string GetSettingsJson() => JsonConvert.SerializeObject(LocalAppConfiguration, Formatting.Indented);

        public string GamePath
        {
            get { return LocalAppConfiguration.AppConfiguration.GamePath; }
            set
            {
                LocalAppConfiguration.AppConfiguration.GamePath = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string ModPath
        {
            get { return LocalAppConfiguration.AppConfiguration.ModPath; }
            set { LocalAppConfiguration.AppConfiguration.ModPath = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);

            }
        }

        public string LaunchParameters
        {
            get { return LocalAppConfiguration.AppConfiguration.LaunchCommands; }
            set
            {
                LocalAppConfiguration.AppConfiguration.LaunchCommands = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string NexusApiKey
        {
            get { return LocalAppConfiguration.AppConfiguration.NexusApiKey; }
            set
            {
                LocalAppConfiguration.AppConfiguration.NexusApiKey = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public ulong SteamId
        {
            get { return LocalAppConfiguration.AppConfiguration.SteamId; }
            set
            {
                LocalAppConfiguration.AppConfiguration.SteamId = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public uint GameId
        {
            get { return LocalAppConfiguration.AppConfiguration.GameId; }
            set
            {
                LocalAppConfiguration.AppConfiguration.GameId = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool OpenInSteam
        {
            get { return LocalAppConfiguration.AppConfiguration.OpenInSteam; }
            set
            {
                LocalAppConfiguration.AppConfiguration.OpenInSteam = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool UseUISounds
        {
            get { return LocalAppConfiguration.AppConfiguration.UseUISounds; }
            set
            {
                LocalAppConfiguration.AppConfiguration.UseUISounds = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CloseOnPlay
        {
            get { return LocalAppConfiguration.AppConfiguration.CloseOnPlay; }
            set
            {
                LocalAppConfiguration.AppConfiguration.CloseOnPlay = value;
                HealthCheck = PerformHealthCheck();
                SettingsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}