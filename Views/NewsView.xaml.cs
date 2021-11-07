using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using MaterialDesignThemes.Wpf;
using Steam.Models;
using Steam.Models.SteamPlayer;

namespace DaysGoneModManager.Views
{
    public partial class NewsView : BaseView
    {
        private readonly ISteamService _steamService;

        public override ViewMenuData ViewMenuData { get; set; }
        public ObservableCollection<NewsItemModel> GameNews { get; set; }

        public NewsView(ISteamService steamService)
        {
            _steamService = steamService;
            _steamService.PlayerDataLoaded += _steamService_PlayerDataLoaded;

            ViewMenuData = new ViewMenuData
            {
                ViewIndex = 2,
                ViewLabel = "NEWS",
                ViewIcon = PackIconKind.Gamepad,
                ViewType = AppConstants.ViewTypes.Game
            };
            DataContext = this;
            InitializeComponent();
        }

        private void _steamService_PlayerDataLoaded(object sender, System.EventArgs e)
        {
            GameNews = _steamService.GetPlayerData().NewsItems;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }
    }
}