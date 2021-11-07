using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using MaterialDesignThemes.Wpf;

namespace DaysGoneModManager.Views
{
    public partial class SettingsView : BaseView
    {
        public override ViewMenuData ViewMenuData { get; set; }

        public SettingsView()
        {
            ViewMenuData = new ViewMenuData
            {
                ViewIndex = 4,
                ViewLabel = "SETTINGS",
                ViewIcon = PackIconKind.Gamepad,
                ViewType = AppConstants.ViewTypes.Game
            };
            InitializeComponent();
        }
    }
}