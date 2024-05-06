using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        public int CustomerId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProduct> Products { get; set; }
        public decimal ShippingPrice { get; set; }
        public string ShippingAddress { get; set; }

        public Order(int id, int customerId, OrderStatusEnum status, DateTime date, List<OrderProduct> products, decimal shippingPrice, string shippingAddress)
        {
            Id = id;
            CustomerId = customerId;
            Status = status;
            Date = date;
            Products = products;
            ShippingPrice = shippingPrice;
            ShippingAddress = shippingAddress;
        }

        public Order(int customerId, OrderStatusEnum status, DateTime date, List<OrderProduct> products, decimal shippingPrice, string shippingAddress)
        {
            CustomerId = customerId;
            Status = status;
            Date = date;
            Products = products;
            ShippingPrice = shippingPrice;
            ShippingAddress = shippingAddress;
        }

        public Order(OrderStatusEnum status, DateTime date, List<OrderProduct> products, decimal shippingPrice, string shippingAddress)
        {
            Status = status;
            Date = date;
            Products = products;
            ShippingPrice = shippingPrice;
            ShippingAddress = shippingAddress;
        }
    }
}
