using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Octopus.Proxies.Services;
using System.Windows.Threading;
using OctopusServerLib.Messages;
using System.Threading.Tasks;

namespace Octopus.Client.ViewModel
{
    class StatusBarViewModel : AbstractViewModel
    {
        private IPingService _pingService;
        private DispatcherTimer _dispatcherTimer; 

        public StatusBarViewModel(
            IPingService pingService)
        {
            _pingService = pingService;

          
        }

        private string _statusText;
        public string StatusText 
        {
            get 
            {
                return _statusText;
            }
            set
            {
                if (value != _statusText)
                {
                    var oldValue = _statusText;
                    _statusText = value;
                    RaisePropertyChanged<string>("StatusText", oldValue, value, true);
                }
            }
        }

        private ConnectionStatus _connectionStatus;
        public ConnectionStatus ConnectionStatus 
        {
            get { return _connectionStatus; }
            set
            {
                if (value != _connectionStatus)
                {
                    var oldValue = _connectionStatus;
                    _connectionStatus = value;
                    RaisePropertyChanged<ConnectionStatus>("ConnectionStatus", oldValue, value, true);
                }
            }
        }

        public override void NotifyLoaded()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
            _dispatcherTimer.Tick += (s, a) => { Ping(); };

            _pingService.Connect();
            _dispatcherTimer.Start();
        }

        /// <summary>
        /// This method executes periodically in the Dispatcher Thread, so no need
        /// to do UI marshalling
        /// </summary>
        private void Ping()
        {
            StatusText = "Pinging server...";
            ConnectionStatus = ConnectionStatus.Idle;
            _pingService
                 .Request(new Ping { ClientId = Guid.NewGuid() })
                 .ContinueWith(pingAckTask => 
                 {
                     var ack = pingAckTask.Result;
                     ConnectionStatus = ack == null ? ConnectionStatus.Disconnected : ConnectionStatus.Connected;
                     StatusText = string.Format("Server is {0}", ConnectionStatus.ToString().ToLower());
                 },
                 TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    enum ConnectionStatus
    {
        Disconnected,
        Connected,
        Idle
    }
}
