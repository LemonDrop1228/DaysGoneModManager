using DaysGoneModManager.Services;
using Steam.Models.SteamPlayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DaysGoneModManager.Views
{
    public partial class AchievementsView : BaseView
    {
        private readonly ISteamService _steamService;
        private readonly IAppSettingsManager _settingsManager;

        public override ViewMenuData ViewMenuData { get; set; }
        public ObservableCollection<PlayerAchievementModel> PlayerAchievements { get; set; }
        public bool HealthCheckPass { get; set; }

        public AchievementsView(ISteamService steamService, IAppSettingsManager settingsManager)
        {
            _steamService = steamService;
            _settingsManager = settingsManager;

            SetHealthCheck();

            _steamService.PlayerDataLoaded += _steamService_PlayerDataLoaded;
            _settingsManager.SettingsUpdated += (sender, args) => { SetHealthCheck();};

            ViewMenuData = new() { ViewIndex = 1, ViewLabel = "UNLOCKS" };
            DataContext = this;
            InitializeComponent();
        }

        private void SetHealthCheck()
        {
            HealthCheckPass = _settingsManager.HealthCheck;
        }

        private void _steamService_PlayerDataLoaded(object sender, EventArgs e)
        {
            PlayerAchievements = _steamService.GetPlayerData().AchievementsSorted;
        }
    }
}
