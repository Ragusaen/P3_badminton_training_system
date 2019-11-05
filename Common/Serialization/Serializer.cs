using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace application.SystemInterface.Requests.Serialization
{
    public class Serializer
    {
        public T Deserialize<T>(byte[] data) where T : class
        {
            var stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Position = 0;

            var serializer = new DataContractSerializer(typeof(T));
            T result = null;
            try
            {
                result = serializer.ReadObject(stream) as T;
            } catch (System.Xml.XmlException)
            {
                Debug.WriteLine($"Attempted to deserialize to type {typeof(T).FullName}: {Encoding.UTF8.GetString(data)}");
                throw;
            }

            return result;
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
