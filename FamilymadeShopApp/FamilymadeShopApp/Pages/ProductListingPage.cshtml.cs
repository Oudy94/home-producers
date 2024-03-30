using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Models;
using SharedLibrary.Enums;

namespace FamilymadeShopApp.Pages
{
    public class ProductListingPageModel : PageModel
    {
        [BindProperty]
        public ProductManager ProductManager { get; set; }

        public void OnGet()
        {
            this.ProductManager = new ProductManager();
        }
    }
}
