using Confluent.Kafka;
using Kafka.Publisher.KafkaService;
using Kafka.Publisher.Models;

KafkaDispatcher<Order> orderDispatcher = new KafkaDispatcher<Order>();

while (true)
{
    Console.WriteLine("Enter the product name:");
    string productName = Console.ReadLine();

    Console.WriteLine("Enter the price (only values):");
    double price = double.Parse(Console.ReadLine());

    Order order = new Order(Guid.NewGuid(), productName, price, DateTime.Now.AddDays(30));

    await orderDispatcher.Dispatch(order);

}