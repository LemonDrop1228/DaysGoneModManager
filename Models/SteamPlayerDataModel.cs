using System;
using System.Collections.ObjectModel;
using System.Linq;
using DaysGoneModManager.Helpers;
using Steam.Models;
using Steam.Models.SteamCommunity;
using Steam.Models.SteamPlayer;

namespace DaysGoneModManager.Models
{
    public class SteamPlayerDataModel
    {
        public PlayerSummaryModel PlayerData { get; set; }
        public Uri PlayerAvatar { get => new Uri(PlayerData.AvatarFullUrl, UriKind.Absolute);}
        public string AchievementCount { get => $"{Achievements.Count(a => a.Achieved > 0)}/{Achievements.Count}"; }
        public ObservableCollection<PlayerAchievementModel> AchievementsSorted { get => Achievements.OrderByDescending(a => a.Achieved).ToObservableCollection();}
        public ObservableCollection<PlayerAchievementModel> Achievements { get; set; }
        public ObservableCollection<NewsItemModel> NewsItems { get; set; }
        public TimeSpan PlayTime { get; set; }
    }
}