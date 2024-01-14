using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Kafka.Publisher.Serializer
{
    public class KafkaDeserializer<T> : IDeserializer<T> where T : class
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if(isNull)
                return null;
            
            try
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));
            }catch(JsonReaderException ex)
            {
                System.Console.WriteLine(Encoding.UTF8.GetString(data));
                return null;
            }
            
        }
    }
}