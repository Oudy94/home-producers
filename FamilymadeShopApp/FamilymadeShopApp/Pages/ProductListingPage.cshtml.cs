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

            this.ProductManager.Add(1, "Golden Honey Wheat Bread", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam metus orci, malesuada nec sapien quis,", CategoryEnum.BakeryGoods, 10.50, 14, new List<string>(), 16, 222.0);
            this.ProductManager.Add(2, "Cinnamon Swirl Coffee Cake", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam metus orci, malesuada nec sapien quis,", CategoryEnum.BakeryGoods, 12.50, 5, new List<string>(), 25, 115.0);
            this.ProductManager.Add(3, "Chocolate Chip Cookie Delight", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam metus orci, malesuada nec sapien quis,", CategoryEnum.BakeryGoods, 8.50, 7, new List<string>(), 15, 444.0);
        }
    }
}
