﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SIP.SurveyMaker.WPFUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider serviceProvider;
        public static IConfiguration Configuration;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
            Configuration = configSettings;
            services.AddSingleton<MaintainSurvey>();
        }

        private void OnStartUp(object sender, StartupEventArgs e)
        {
            var MaintainSurvey = serviceProvider.GetService<MaintainSurvey>();
            MaintainSurvey.Show();
        }
    }
}
