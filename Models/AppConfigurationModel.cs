using System;
using Newtonsoft.Json;

namespace DaysGoneModManager.Models
{
    public class AppConfigurationModel
    {
        public class AppConfiguration
        {
            [JsonProperty("GamePath")]
            public string GamePath { get; set; } = String.Empty;

            [JsonProperty("SteamId")]
            public ulong SteamId { get; set; } = 0;

            [JsonProperty("LaunchCommands")]
            public string LaunchCommands { get; set; } = String.Empty;

            [JsonProperty("GameId")]
            public uint GameId { get; set; } = 1259420;

            [JsonProperty("ModPath")]
            public string ModPath { get; set; } = String.Empty;

            [JsonProperty("OpenInSteam")]
            public bool OpenInSteam { get; set; } = true;
        }

        public class Root
        {
            [JsonProperty("AppConfiguration")]
            public AppConfiguration AppConfiguration { get; set; } = new();
        }
    }


}