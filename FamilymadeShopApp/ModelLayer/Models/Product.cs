using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLayer.Enums;

namespace ModelLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(24, ErrorMessage = "Name must be between {2} and {1} characters", MinimumLength = 3)]
		public string Name { get; set; }
        public string Description { get; set; }
		[Required(ErrorMessage = "Category is required")]
		public Category Category { get; set; }
		[Required(ErrorMessage = "Price is required")]
		public decimal Price { get; set; }
		[Required(ErrorMessage = "Stock is required")]
		public int Stock { get; set; }
        public string Image { get; set; }
		[Required(ErrorMessage = "Sales count is required")]
		public int SalesCount { get; set; }

        public Product()
        {

        }

        public Product(int id, string name, string description, Category category, decimal price, int stock, string image, int salesCount)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Price = price;
            this.Stock = stock;
            this.Image = image;
            this.SalesCount = salesCount;
        }
    }
}
