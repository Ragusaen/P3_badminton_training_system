using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller.Requests.Serialization
{
    class Serializer
    {
        public T Deserialize<T>(byte[] data) where T : class
        {
            var stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Position = 0;

            var serializer = new DataContractSerializer(typeof(T));
            return serializer.ReadObject(stream) as T;
        }

        public byte[] Serialize<T>(T obj)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, obj);

            return stream.ToArray();
        }
    }
}
