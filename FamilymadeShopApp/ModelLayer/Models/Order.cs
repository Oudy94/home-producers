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
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProduct> Products { get; set; }
        public decimal ShippingPrice { get; set; }
        public string ShippingAddress { get; set; }
		public string PaymentMethod { get; set; }

		public Order(int id, int customerId, OrderStatus status, DateTime date, List<OrderProduct> products, decimal shippingPrice, string shippingAddress, string paymentMethod)
		{
			Id = id;
			CustomerId = customerId;
			Status = status;
			Date = date;
			Products = products;
			ShippingPrice = shippingPrice;
			ShippingAddress = shippingAddress;
			PaymentMethod = paymentMethod;
		}

		public Order(int customerId, OrderStatus status, DateTime date, List<OrderProduct> products, decimal shippingPrice, string shippingAddress, string paymentMethod)
        {
			CustomerId = customerId;
			Status = status;
			Date = date;
			Products = products;
			ShippingPrice = shippingPrice;
			ShippingAddress = shippingAddress;
            PaymentMethod = paymentMethod;
		}

		public Order()
		{

		}
	}
}
