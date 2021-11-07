using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using DaysGoneModManager.Views;
using MaterialDesignThemes.Wpf;
using PropertyChanged;

namespace DaysGoneModManager
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        private readonly IStartupService _startupService;
        private readonly IControllerService _controllerService;
        private readonly ISteamService _steamService;
        public bool LoadingComplete { get; set; }
        System.Media.SoundPlayer soundPlayer { get; set; } = new System.Media.SoundPlayer();
        public IEnumerable<ViewBucket> ViewCollection { get => _controllerService.GetViews().Select(v => new ViewBucket(v));}
        public IBaseView ActiveView { get => _controllerService.CurrentView;}

        public ICommand ChangeView => new RelayCommand(o =>
        {
            _controllerService.SetView((o as ViewBucket).ViewRef);
            HostCard.GetBindingExpression(Card.ContentProperty).UpdateTarget();
            PlayPageTurnSound();
        }, o => true);

        public MainWindow(IStartupService startupService,
            IControllerService controllerService,
            ISteamService steamService)
        {
            _startupService = startupService;
            _controllerService = controllerService;
            _steamService = steamService;

            DataContext = this;
            InitializeComponent();
            _startupService.StartupCompleted += _startupService_StartupCompleted;
        }

        private void _startupService_StartupCompleted(object sender, EventArgs e)
        {
            LoadingComplete = true;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _startupService.InitializeStartup(_steamService);
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1 && e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseAppClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
            soundPlayer.Stop();
            soundPlayer.Dispose();
        }

        private void MinimizeAppClicked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void PlayPageTurnSound()
        {
            soundPlayer.SoundLocation = "Sounds/PAPER- LARGE-MVMNT.wav";
            soundPlayer.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e) {
                soundPlayer.Play();
            };
            soundPlayer.LoadAsync();
        }
    }
}
