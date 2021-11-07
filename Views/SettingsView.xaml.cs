using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace DaysGoneModManager.Views
{
    public partial class SettingsView : BaseView
    {
        public IAppSettingsManager AppSettingsManager { get; private set; }
        public override ViewMenuData ViewMenuData { get; set; }

        public SettingsView(IAppSettingsManager appSettingsManager)
        {
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
                AppSettingsManager.ModPath = dialog.SelectedPath;
            }
        }
    }
}