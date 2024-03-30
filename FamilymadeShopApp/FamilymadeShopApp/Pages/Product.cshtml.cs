using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Enums;
using SharedLibrary.Models;

namespace FamilymadeShopApp.Pages
{
    public class ProductModel : PageModel
    {
        public ProductManager ProductManager { get; set; }

        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            this.ProductManager = new ProductManager();
            Product = ProductManager.Get(id);
        }
    }
}
