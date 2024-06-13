using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;

namespace WebApp.Pages
{
    using BusinessLogicLayer.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductListingPageModel : PageModel
    {
        private readonly IProductManager _productManager;

        public ProductListingPageModel(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int ProductsPerPage { get; } = 20;

        public async Task<IActionResult> OnGetAsync(string search = "", int pageIndex = 1)
        {
            try
            {
                SearchTerm = search;
                PageIndex = pageIndex;

                int totalProducts = await _productManager.GetProductsCountAsync(search, null);
                TotalPages = (int)Math.Ceiling((double)totalProducts / ProductsPerPage);
                Products = await _productManager.GetAllProductsAsync(pageIndex, ProductsPerPage, search, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["MessageError"] = "Failed to load products. Please try again later.";
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }


}
