using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Enums;

namespace SharedLibrary.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryEnum Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Images { get; set; }
        public int SalesCount { get; set; }
        public double Revenue { get; set; }

        public Product(int id, string name, string description, CategoryEnum category, double price, int quantity, List<string> images, int salesCount, double revenue)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Price = price;
            this.Quantity = quantity;
            this.Images = images;
            this.SalesCount = salesCount;
            this.Revenue = revenue;
        }
    }
}
