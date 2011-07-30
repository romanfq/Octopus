using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OctopusServerLib.Messages;
using System.IO;

namespace OctopusServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContractSerializer serializer;
            MemoryStream ms;

            using (ZMQ.Context context = new ZMQ.Context(1))
            {
                using (ZMQ.Socket socket = context.Socket(ZMQ.SocketType.REP))
                {
                    socket.Bind("tcp://*.5555");
                    Console.WriteLine("Running server on port 5555");

                    while (true)
                    {
                        byte[] clientMessage = socket.Recv();
                        Console.WriteLine("Received {0} bytes", clientMessage.Length);

                        ms = new MemoryStream(clientMessage);
                        serializers.TryGetValue(typeof(Ping), out serializer);
                        Ping pingMessage = (Ping) serializer.ReadObject(ms);
                        Console.WriteLine("from client...'{0}'", pingMessage.ClientId);

                        PingAck ack = new PingAck();
                        serializers.TryGetValue(typeof(PingAck), out serializer);
                        ms = new MemoryStream();
                        serializer.WriteObject(ms, ack);
                        socket.Send(ms.GetBuffer());
                    }
                }
            }
        }

        static Dictionary<Type, DataContractSerializer> serializers = new Dictionary<Type, DataContractSerializer>();
    }
}
