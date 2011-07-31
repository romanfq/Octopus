using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.ServerLib;

namespace Octopus.Proxies
{
    public abstract class ProxyBase<T,U> : IProxy<T,U>, IDisposable
    {
        private IProxyConfiguration _proxyConfiguration;
        private ZMQ.Context _zmqContext;
        private ZMQ.Socket _zmqSocket;
        private bool _isConnected;

        protected ProxyBase(IProxyConfiguration proxyConfiguration)
        {
            _proxyConfiguration = proxyConfiguration;
        }

        public void Connect()
        {
            if (_isConnected)
            {
                return;
            }

            if (_zmqContext == null)
            {
                _zmqContext = new ZMQ.Context(1);
            }

            if (_zmqSocket == null)
            {
                _zmqSocket = _zmqContext.Socket(ZMQ.SocketType.REQ);
            }

            _zmqSocket.Connect(_proxyConfiguration.ServerAddress);
            _isConnected = true;
        }
        public void Disconnect()
        {
            if (!_isConnected)
            {
                return;
            }

            _zmqSocket.Dispose();
            _isConnected = false;
        }
        public virtual Task<U> Request(T input)
        {
            return Task.Factory.StartNew<U>(() =>
            {
                if (!_isConnected)
                {
                    throw new Exceptions.ProxyException("Call Connect before calling Request");
                }

                _zmqSocket.Send(Wire.Serialize(input));
                return Wire.Deserialize<U>(_zmqSocket.Recv());
            });

        }
        public void Dispose()
        {
            Disconnect();

            if(_zmqContext != null)
            {
                _zmqContext.Dispose();    
            }
        }
    }
}
