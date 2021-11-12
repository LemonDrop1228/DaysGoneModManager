using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PropertyChanged;

namespace DaysGoneModManager
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindowFull : Window
    {
        public PackIcon MaxRestoreIcon {get => this.WindowState == WindowState.Maximized
            ? new(){Kind = PackIconKind.WindowRestore, Width = 18, Height = 18}
            : new(){Kind = PackIconKind.WindowMaximize, Width = 18, Height = 18};}

        public MainWindowFull()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void TitleCard_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}