using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class ProductManager
    {
        public Dictionary<int, Product> ProductDict { get; set; }

        public ProductManager()
        {
            this.ProductDict = new Dictionary<int, Product>();
        }

        public void Add(int id, string name, string description, CategoryEnum category, double price, int quantity, List<string> images, int salesCount, double revenue)
        {
            try
            {
                Product product = new Product(id, name, description, category, price, quantity, images, salesCount, revenue);
                ProductDict.Add(id, product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
