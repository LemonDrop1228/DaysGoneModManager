using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AnyClone;
using DaysGoneModManager.CustomClasses;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using MaterialDesignThemes.Wpf;
using static DaysGoneModManager.Helpers.AppConstants;

namespace DaysGoneModManager.Views
{
    public partial class GameView : BaseView
    {
        private readonly ISteamService _steamService;
        private readonly IAppSettingsManager _settingsManager;
        public override ViewMenuData ViewMenuData { get; set; }
        public object PlayerModel { get; set; }
        public ObservableCollection<SteamAction> SteamActions { get; set; }

        public ICommand RunSteamAction => new RelayCommand(o =>
        {
            (o as SteamAction).Action.Invoke();
        }, o => true);


        public GameView(ISteamService steamService, 
            IAppSettingsManager settingsManager)
        {
            _steamService = steamService;
            _settingsManager = settingsManager;

            ViewMenuData = new ViewMenuData
            {
                ViewIndex = 0,
                ViewLabel = "GAMES",
                ViewIcon = PackIconKind.Gamepad,
                ViewType = AppConstants.ViewTypes.Game
            };

            _steamService.PlayerDataLoaded += (sender, args) =>
            {
                PlayerModel = _steamService.GetPlayerData();
            };

            DataContext = this;
            SetupSteamActions();
            InitializeComponent();
        }

        private void SetupSteamActions()
        {
            var actionList = new ObservableCollection<SteamAction>();
            var protocolArray = (BrowserProtocols[])Enum.GetValues(typeof(BrowserProtocols));
            var actionArray = protocolArray.Select(v => new SteamAction(v.ToSpacedString(), 
                    () => { _steamService.RunSteamProtocol(v); }
                ));
            actionList.AddRange<SteamAction>(actionArray.Clone());
            protocolArray = null;
            actionArray = null;

            SteamActions = actionList;
        }

        private void LoadProfile(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _steamService.RunSteamProtocol(AppConstants.BrowserProtocols.Profile);
        }

        private void LaunchGame(object sender, System.Windows.RoutedEventArgs e)
        {
            _steamService.LaunchGame();
        }
    }
}