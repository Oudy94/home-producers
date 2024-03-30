using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class OrderProduct
    {
        public int Id { get; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderProduct(int id, Order order, Product product, int quantity, decimal price)
        {
            Id = id;
            Order = order;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
