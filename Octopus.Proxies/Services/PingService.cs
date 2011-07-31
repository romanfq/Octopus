using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OctopusServerLib.Messages;
using System.Runtime.Serialization;
using System.IO;
using Octopus.ServerLib;

namespace Octopus.Proxies.Services
{
    public class PingService : 
        ProxyBase<Ping, PingAck>, 
        IPingService
    {
        public PingService(IProxyConfiguration proxyConfiguration)
            : base(proxyConfiguration)
        {

        }
    }
}
