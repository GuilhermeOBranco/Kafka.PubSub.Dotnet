
using Confluent.Kafka;
using Kafka.Consumer.KafkaService;
using Kafka.Consumer.Models;

KafkaConsumer<Order> kafkaConsumer = new(Guid.NewGuid().ToString(),"new_test");


await kafkaConsumer.Consume();


