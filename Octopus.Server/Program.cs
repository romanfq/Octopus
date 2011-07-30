using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OctopusServerLib.Messages;
using System.IO;
using Octopus.ServerLib;

namespace OctopusServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContractSerializer serializer;

            InitSerializers();

            using (ZMQ.Context context = new ZMQ.Context(1))
            {
                using (ZMQ.Socket socket = context.Socket(ZMQ.SocketType.REP))
                {
                    socket.Bind("tcp://*:5555");
                    Console.WriteLine("Running server on port 5555");

                    while (true)
                    {
                        byte[] clientMessage = socket.Recv();
                        Console.WriteLine("Received {0} bytes", clientMessage.Length);

                        var ping = Wire.Deserialize<Ping>(clientMessage);
                        Console.WriteLine("From client: {0}", ping.ClientId);

                        PingAck ack = new PingAck { TimeStamp = DateTime.UtcNow };
                        socket.Send(Wire.Serialize(ack));
                    }
                }
            }
        }

        private static void InitSerializers()
        {
            serializers = new[] 
            { 
                typeof(Ping), 
                typeof(PingAck) 
            }.ToDictionary(type => type, type => new DataContractSerializer(type));
           
        }

        static Dictionary<Type, DataContractSerializer> serializers;

        

    }
}
