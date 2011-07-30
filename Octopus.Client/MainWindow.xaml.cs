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
using Octopus.Proxies.Services;
using OctopusServerLib.Messages;
using System.Threading.Tasks;

namespace OctopusClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime requestTime = DateTime.UtcNow ;
            Func<Ping> getPingMessage = () =>
            {
                requestTime = DateTime.UtcNow;
                return new Ping { ClientId = Guid.NewGuid() };
            };

            IPingService pingService = new PingService();
            pingService
                .Request(getPingMessage())
                .ContinueWith(pingAckTask => 
                {
                    PingAck ack = pingAckTask.Result;
                    PingReplyTextBlock.Text = ack == null 
                        ? "Nothing received" 
                        : string.Format("Hi from server! (round trip: {0})", (DateTime.UtcNow - requestTime));
                }, 
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
