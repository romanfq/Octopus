using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OctopusServerLib.Messages;
using System.Threading.Tasks;

namespace Octopus.Proxies.Services
{
    public interface IPingService : IProxy<Ping, PingAck>
    {
    }
}
