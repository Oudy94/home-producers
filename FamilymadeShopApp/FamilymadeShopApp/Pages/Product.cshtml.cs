using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedLibrary.Enums;
using SharedLibrary.Models;
using Microsoft.AspNetCore.Antiforgery;

namespace FamilymadeShopApp.Pages
{
    public class ProductModel : PageModel
    {
        public ProductManager ProductManager { get; set; }
        public Product Product { get; set; }

        public void OnGet(int id)
        {
            this.ProductManager = new ProductManager();
            Product = ProductManager.Get(id);
        }

        public async Task<IActionResult> OnPostAddToCartAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    CartProduct cartProduct = JsonConvert.DeserializeObject<CartProduct>(requestBody);

                    if (cartProduct != null)
                    {
                        UpdateCart(cartProduct);
                    }

                    return new JsonResult(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to deserialize JSON data: " + ex.Message);
            }
        }

        private void UpdateCart(CartProduct cartProduct)
        {
            List<CartProduct> cartItems = HttpContext.Request.Cookies.ContainsKey("CartItems")
                ? JsonConvert.DeserializeObject<List<CartProduct>>(HttpContext.Request.Cookies["CartItems"])
                : new List<CartProduct>();

            CartProduct existingCartItem = cartItems.FirstOrDefault(item => item.ProductId == cartProduct.ProductId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartProduct.Quantity;
            }
            else
            {
                cartItems.Add(cartProduct);
            }

            HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(cartItems));
        } 
    }
}
