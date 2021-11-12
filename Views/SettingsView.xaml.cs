using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using static DaysGoneModManager.Helpers.AppConstants;

namespace DaysGoneModManager.Views
{
    public partial class SettingsView : BaseView
    {
        private readonly INotificationService _notificationService;
        public IAppSettingsManager AppSettingsManager { get; set; }
        public override ViewMenuData ViewMenuData { get; set; }

        public SettingsView(IAppSettingsManager appSettingsManager,
            INotificationService notificationService)
        {
            _notificationService = notificationService;
            AppSettingsManager = appSettingsManager;


            ViewMenuData = new ViewMenuData
            {
                ViewIndex = 4,
                ViewLabel = "SETTINGS",
                ViewIcon = PackIconKind.Gamepad,
                ViewType = AppConstants.ViewTypes.Game
            };

            DataContext = this;
            InitializeComponent();
        }

        private void ModPathButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                if(Directory.Exists(dialog.SelectedPath) && VerifyModPath(dialog.SelectedPath))
                    AppSettingsManager.ModPath = dialog.SelectedPath;
                else
                {
                    _notificationService.SetNotificationContent("Setup Issues", "Invalid mod path.");
                    _notificationService.NotifyWarning();
                }
            }
        }

        private bool VerifyModPath(string path)
        {
            return path == GameAppDataPaksPath;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
        }

        private void GamePathButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                if (Directory.Exists(dialog.SelectedPath) && VerifyGamePath(dialog.SelectedPath))
                    AppSettingsManager.GamePath = dialog.SelectedPath;
                else
                {
                    _notificationService.SetNotificationContent("Setup Issues", "Invalid game path.");
                    _notificationService.NotifyWarning();
                }
            }
        }

        private bool VerifyGamePath(string path)
        {
            var testPath = path + GameExePathPart;
            return File.Exists(testPath);
        }

        private void GenerateModFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(GameAppDataPath))
            {
                _notificationService.SetNotificationContent("Setup Error", "It appears you have not yet installed and ran the game. Please do so before attempting to mod it.");
                _notificationService.NotifyFailure();
                return;
            }

            if (Directory.Exists(GameAppDataPaksPath))
            {
                _notificationService.SetNotificationContent("Setup Error", "It appears there is already a mod folder in the games app data folder.");
                _notificationService.NotifyFailure();
                return;
            }

            try
            {
                Directory.CreateDirectory(GameAppDataPaksPath);
                AppSettingsManager.ModPath = GameAppDataPaksPath;
            }
            catch (Exception exception)
            {
                _notificationService.SetNotificationContent("Setup Error", exception.Message);
                _notificationService.NotifyFailure();
            }
        }
    }
}