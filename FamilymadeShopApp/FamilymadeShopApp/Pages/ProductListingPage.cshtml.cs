using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Models;
using SharedLibrary.Enums;

namespace FamilymadeShopApp.Pages
{
    public class ProductListingPageModel : PageModel
    {
        public ProductManager ProductManager { get; set; }
        public string SearchTerm { get; set; }
        public int PageIndex { get; set; }

        public void OnGet(string search, int pageIndex = 1)
        {
            this.ProductManager = new ProductManager();
            SearchTerm = search;
            PageIndex = pageIndex;
        }
    }
}
