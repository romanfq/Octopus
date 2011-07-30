using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Octopus.ServerLib
{
    public static class Wire
    {
        public static byte[] Serialize<T>(T obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] data)
        {
            using (Stream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }
    }
}
