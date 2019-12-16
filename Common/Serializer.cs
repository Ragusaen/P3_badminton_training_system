using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
    public class Serializer
    {
        public T Deserialize<T>(byte[] data) where T : class
        {
            var stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Position = 0;
            
            var serializer = new XmlSerializer(typeof(T));
            T result;
            try
            {
                result = serializer.Deserialize(stream) as T;
            }
            catch (System.Xml.XmlException)
            {
                Debug.WriteLine($"Attempted to deserialize to type {typeof(T).FullName}: {Encoding.UTF8.GetString(data)}");
                throw;
            }

            return result;
        }

        public byte[] Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stream = new MemoryStream();
            serializer.Serialize(stream, obj);

            return stream.ToArray();
        }
    }
}
