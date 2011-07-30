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
    public class PingService : IPingService
    {
        public Task<PingAck> Request(Ping input)
        {
            return Task.Factory.StartNew<PingAck>(() =>
            {
                using (var context = new ZMQ.Context(1))
                {
                    using (var requester = context.Socket(ZMQ.SocketType.REQ))
                    {
                        requester.Connect("tcp://localhost:5555");
                        byte[] request = Wire.Serialize(input);
                        requester.Send(request);

                        byte[] reply = requester.Recv();
                        PingAck ack = Wire.Deserialize<PingAck>(reply);
                        return ack;
                    }
                }
            });
        }
    }
}
