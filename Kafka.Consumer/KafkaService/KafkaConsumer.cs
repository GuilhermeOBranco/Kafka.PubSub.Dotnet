using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.Publisher.Serializer;
using Newtonsoft.Json;

namespace Kafka.Consumer.KafkaService
{
    public class KafkaConsumer<T> where T : class
    {
        private string GroupId { get; set; }
        private string Topic { get; set; }
        ConsumerConfig config;

        public KafkaConsumer(string groupId, string topic)
        {
            GroupId = groupId;
            Topic = topic;

            config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9091",
                GroupId = GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
        }

        public async Task Consume()
        {
            using (var consumer = new ConsumerBuilder<string, T>(config).SetValueDeserializer(new KafkaDeserializer<T>()).Build())
            {
                consumer.Subscribe("new_test");
                while (true)
                {
                    var response = consumer.Consume();
                    Console.WriteLine("[{0}] Key: {1}. Value: {2}", DateTime.Now, response.Key, JsonConvert.SerializeObject(response.Value));
                    consumer.Commit(response);
                }
            }
        }
    }
}