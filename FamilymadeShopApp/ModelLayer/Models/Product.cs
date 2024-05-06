using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLayer.Enums;

namespace ModelLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<string> Images { get; set; }
        public int SalesCount { get; set; }

        public Product()
        {

        }

        public Product(int id, string name, string description, Category category, decimal price, int stock, List<string> images, int salesCount)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Price = price;
            this.Stock = stock;
            this.Images = images;
            this.SalesCount = salesCount;
        }
    }
}
