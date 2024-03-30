using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Order
    {
        public int Id { get; }
        public Customer Customer { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime Date { get; }
        public List<OrderProduct> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAdress { get; set; }

        public Order(int id, Customer customer, OrderStatusEnum status, List<OrderProduct> products, decimal totalPrice, string shippingAdress)
        {
            Id = id;
            Customer = customer;
            Status = status;
            Date = DateTime.Now;
            Products = products;
            TotalPrice = totalPrice;
            ShippingAdress = shippingAdress;
        }
    }
}
