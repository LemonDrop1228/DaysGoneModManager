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
        private readonly INotificationService _notificationService;
        private readonly IAppSettingsManager _appSettingsManager;

        public bool LoadingComplete { get; set; }
        System.Media.SoundPlayer soundPlayer { get; set; } = new System.Media.SoundPlayer();
        public IEnumerable<ViewBucket> ViewCollection { get => _controllerService.GetViews().Select(v => new ViewBucket(v));}
        public IBaseView ActiveView { get => _controllerService.CurrentView;}
        public bool HealthCheckPass {get => _appSettingsManager.HealthCheck;}


        public ICommand ChangeView => new RelayCommand(o =>
        {
            _controllerService.SetView((o as ViewBucket).ViewRef);
        }, o => true);

        public MainWindow(IStartupService startupService,
            IControllerService controllerService,
            ISteamService steamService,
            INotificationService notificationService,
            IAppSettingsManager appSettingsManager)
        {
            _startupService = startupService;
            _controllerService = controllerService;
            _steamService = steamService;
            _notificationService = notificationService;
            _appSettingsManager = appSettingsManager;

            _startupService.StartupCompleted += _startupService_StartupCompleted;
            _controllerService.ViewChanged += ControllerServiceOnViewChanged;

            DataContext = this;
            InitializeComponent();
            if (!HealthCheckPass)
                _controllerService.SetView((IBaseView)_controllerService.GetLastView());
        }

        private void ControllerServiceOnViewChanged(object sender, EventArgs e)
        {
            HostCard.GetBindingExpression(Card.ContentProperty).UpdateTarget();
            if(_appSettingsManager.UseUISounds)
                PlayPageTurnSound();
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

        private void NotificaitonOkClicked(object sender, RoutedEventArgs e)
        {
            DHost.IsOpen = false;
        }
    }
}
