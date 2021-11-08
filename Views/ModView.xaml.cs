using System.Windows;
using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using MaterialDesignThemes.Wpf;

namespace DaysGoneModManager.Views
{
    public partial class ModView : BaseView
    {
        private readonly INexusModsService _modsService;
        private readonly IAppSettingsManager _settingsManager;
        public override ViewMenuData ViewMenuData { get; set; }

        public bool HealthCheckPass { get; set; }

        public ModView(INexusModsService modsService, IAppSettingsManager settingsManager)
        {
            _modsService = modsService;
            _settingsManager = settingsManager;

            SetHealthCheck();
            _settingsManager.SettingsUpdated += (sender, args) => { SetHealthCheck();};

            ViewMenuData = new ViewMenuData
            {
                ViewIndex = 3,
                ViewLabel = "MODS",
                ViewIcon = PackIconKind.Gamepad,
                ViewType = AppConstants.ViewTypes.Game
            };

            DataContext = this;
            InitializeComponent();
        }

        private void SetHealthCheck()
        {
            HealthCheckPass = _settingsManager.HealthCheck;
        }

        private async void ModView_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _modsService.TestClients();
        }
    }
}