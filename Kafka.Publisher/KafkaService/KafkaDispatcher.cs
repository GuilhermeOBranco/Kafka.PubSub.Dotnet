using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.Publisher.Serializer;
using Newtonsoft;
namespace Kafka.Publisher.KafkaService
{
    public class KafkaDispatcher<T> where T : class
    {
        private ProducerConfig configuration = new ProducerConfig { BootstrapServers = "localhost:9091" };

        public async Task Dispatch(T value)
        {
            using (var p = new ProducerBuilder<string, T>(configuration).SetValueSerializer(new KafkaSerialization<T>()).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("new_test", new Message<string, T> { Key = Guid.NewGuid().ToString(), Value = value });

                    Console.WriteLine($"{DateTime.Now} - Delivered -KEY: {dr.Key} Value: '{Newtonsoft.Json.JsonConvert.SerializeObject(dr.Value)}' - Partition {dr.Partition} - OffSet: {dr.Offset} Timestamp: {dr.Timestamp}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}