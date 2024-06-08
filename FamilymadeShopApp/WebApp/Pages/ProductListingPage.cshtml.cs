using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;

namespace WebApp.Pages
{
    public class ProductListingPageModel : PageModel
    {
        public ProductManager ProductManager { get; set; }
        public string SearchTerm { get; set; }
        public int PageIndex { get; set; }

        public void OnGet(string search, int pageIndex = 1)
        {
            this.ProductManager = new ProductManager(new ProductRepository());
            SearchTerm = search;
            PageIndex = pageIndex;
        }
    }
}
