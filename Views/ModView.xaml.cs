using System.Windows.Controls;
using DaysGoneModManager.Helpers;
using MaterialDesignThemes.Wpf;

namespace DaysGoneModManager.Views
{
    public partial class ModView : BaseView
    {
        public override ViewMenuData ViewMenuData { get; set; }

        public ModView()
        {
            ViewMenuData = new ViewMenuData
            {
                ViewIndex = 3,
                ViewLabel = "MODS",
                ViewIcon = PackIconKind.Gamepad,
                ViewType = AppConstants.ViewTypes.Game
            };
            InitializeComponent();
        }
    }
}