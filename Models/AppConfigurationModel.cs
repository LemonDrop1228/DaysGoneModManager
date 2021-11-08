using System;
using Newtonsoft.Json;

namespace DaysGoneModManager.Models
{
    public class AppConfigurationModel
    {
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

            [JsonProperty("OpenInSteam")]
            public bool OpenInSteam { get; set; }

            [JsonProperty("NexusApiKey")]
            public string NexusApiKey { get; set; }

            [JsonProperty("UseUISounds")]
            public bool UseUISounds { get; set; }

            [JsonProperty("CloseOnPlay")]
            public bool CloseOnPlay { get; set; }
        }

        public class Root
        {
            [JsonProperty("AppConfiguration")]
            public AppConfiguration AppConfiguration { get; set; }
        }
    }


}