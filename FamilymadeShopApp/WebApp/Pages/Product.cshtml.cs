using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;

namespace WebApp.Pages
{
    public class ProductModel : PageModel
    {
        public ProductManager ProductManager { get; set; }
        public CartManager CartManager { get; set; }
        public Product Product { get; set; }

        public void OnGet(int id)
        {
            this.ProductManager = new ProductManager(new ProductRepository());
            Product = ProductManager.GetProductById(id);
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

            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null)
                {
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        CartManager = new CartManager(new CartRepository());
                        CartManager.AddProductToCart(userId, cartProduct.ProductId, cartProduct.Quantity);
                    }
                }
            }
        } 
    }
}
