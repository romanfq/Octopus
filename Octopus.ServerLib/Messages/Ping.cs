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
        [DataMember]
        public Guid ClientId { get; set; }
    }

    [DataContract]
    public class PingAck
    {
        [DataMember]
        public DateTime TimeStamp { get; set; }
    }
}
