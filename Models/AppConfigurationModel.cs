using System;
using System.ComponentModel;
using Newtonsoft.Json;
using PropertyChanged;

namespace DaysGoneModManager.Models
{
    public class AppConfigurationModel
    {
        [AddINotifyPropertyChangedInterface]
        public class AppConfiguration
        {
            [JsonProperty("GamePath")]
            public string GamePath { get; set; }

            [JsonProperty("SteamId")]
            public ulong SteamId { get; set; }

            [JsonProperty("LaunchCommands")]
            public string LaunchCommands { get; set; }

            [JsonProperty("GameId")]
            public uint GameId { get; set; }

            [JsonProperty("ModPath")]
            public string ModPath { get; set; }

            [JsonProperty("OpenInSteam"), DefaultValue(true)]
            public bool OpenInSteam { get; set; }

            [JsonProperty("NexusApiKey")]
            public string NexusApiKey { get; set; }

            [JsonProperty("UseUISounds"), DefaultValue(true)]
            public bool UseUISounds { get; set; }

            [JsonProperty("CloseOnPlay"), DefaultValue(true)]
            public bool CloseOnPlay { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class Root
        {
            [JsonProperty("AppConfiguration")]
            public AppConfiguration AppConfiguration { get; set; }
        }
    }


}