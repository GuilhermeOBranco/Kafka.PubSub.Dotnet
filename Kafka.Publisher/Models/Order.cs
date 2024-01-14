using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kafka.Publisher.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime ValidDate { get; set; }
// 
        public Order(Guid id, string productName, double price, DateTime validDate)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            ValidDate = validDate;
        }
    }
}