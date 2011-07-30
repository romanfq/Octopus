using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OctopusServerLib.Messages
{
    [DataContract]
    public class Ping
    {
        public Guid ClientId { get; set; }
    }

    [DataContract]
    public class PingAck
    {
    }
}
