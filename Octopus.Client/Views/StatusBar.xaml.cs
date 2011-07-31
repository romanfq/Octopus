using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using Octopus.Client.Configuration.Autofac;

namespace Octopus.Client.Views
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// </summary>
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();

            OctopusClient.App app = (OctopusClient.App) Application.Current;
            DataContext = app.Container.Resolve<ViewModel.StatusBarViewModel>();
        }
    }
}
