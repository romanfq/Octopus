using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.Proxies
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        public ProxyConfiguration(string address)
        {
            ServerAddress = address;
        }

        public string ServerAddress
        {
            get;
            private set;
        }
    }
}
