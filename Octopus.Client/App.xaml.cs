using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Autofac;
using Octopus.Client.Configuration.Autofac;

namespace OctopusClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Autofac.IContainer _container;

        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
           var containerBuilder = new ContainerBuilder();

           // register components modules
           containerBuilder.RegisterModule(new ProxiesModule());
           containerBuilder.RegisterModule(new ViewModelModule());

           _container = containerBuilder.Build();
        }

        public Autofac.IContainer Container
        {
            get
            {
                return _container;
            }
        }
    }
}
