using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DaysGoneModManager.Helpers;
using DaysGoneModManager.Services;
using DaysGoneModManager.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DaysGoneModManager
{

    public partial class App : Application
    {
        private readonly IHost host;
        private const string configPath = "AppConfiguration.json";

        public App()
        {
            host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var appConfigData = string.Empty;
                    try
                    {
                        if(File.Exists(configPath))
                            appConfigData = File.ReadAllText(configPath);

                    }
                    catch (Exception e)
                    {
                        throw;
                    }

                    services.AddSingleton<IAppSettingsManager>(provider =>
                    {
                        return new AppSettingsManager(appConfigData);
                    });

                    services.AddSingleton<IStartupService>(provider => new StartupService());
                    services.AddSingleton<INotificationService>(provider => new NotificationService());
                    services.AddSingleton<IControllerService>(provider => new ControllerService());
                    services.AddSingleton<ISteamService>(provider => new SteamService());

                    Assembly.GetEntryAssembly().GetTypesAssignableFrom<IBaseView, BaseView>().ForEach((t) =>
                    {
                        services.AddSingleton(typeof(IBaseView), t);
                    });

                    services.AddSingleton<MainWindow>();
                }).Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            host.Services.GetService<ISteamService>().InitializeService(
                host.Services.GetService<IAppSettingsManager>(), host.Services.GetService<INotificationService>()
            );

            host.Services.GetService<IControllerService>().InitializeViews(
                host.Services.GetServices<IBaseView>()
            );

            host.Services.GetService<IControllerService>().StartAtHome();

            host.Services.GetService<IAppSettingsManager>().SettingsUpdated += (s, e) =>
            {
                System.IO.File.WriteAllText( configPath, host.Services.GetService<IAppSettingsManager>().GetSettingsJson());
            };

            var mainWindow = host.Services.GetService<MainWindow>();

            mainWindow.Closed += (s, eArgs) => {
                ShutItDown();
            };

            mainWindow.Show();
        }

        private void ShutItDown()
        {
            using (host)
            {
                host.StopAsync();
            }

            Current.Shutdown();
        }
    }
}
