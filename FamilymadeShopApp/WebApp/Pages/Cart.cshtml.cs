using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;

namespace WebApp.Pages
{
    public class CartModel : PageModel
    {
        public ProductManager ProductManager { get; set; }
        public List<CartProduct> CartItems { get; set; }

        public void OnGet()
        {
            if (Request.Cookies.ContainsKey("CartItems"))
            {
                CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

                ProductManager = new ProductManager();

                foreach (CartProduct cartItem in CartItems)
                {
                    Product product = ProductManager.Get(cartItem.ProductId);

                    cartItem.Name = product.Name;
                    cartItem.Price = product.Price;
                    cartItem.ImageUrl = product.Images[0];
                }
                HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(CartItems));
            }
            else
            {
                CartItems = new List<CartProduct>();
            }
        }

        public async Task<IActionResult> OnPostChangeQuantityAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    CartProduct cartProduct = JsonConvert.DeserializeObject<CartProduct>(requestBody);

                    if (cartProduct != null)
                    {
                        List<CartProduct> cartItems = HttpContext.Request.Cookies.ContainsKey("CartItems")
                            ? JsonConvert.DeserializeObject<List<CartProduct>>(HttpContext.Request.Cookies["CartItems"])
                            : new List<CartProduct>();

                        CartProduct existingCartItem = cartItems.FirstOrDefault(item => item.ProductId == cartProduct.ProductId);
                        if (existingCartItem != null)
                        {
                            existingCartItem.Quantity = cartProduct.Quantity;

                            HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(cartItems));

                            return new JsonResult(new { success = true, cartItems = cartItems });
                        }
                    }

                    return new JsonResult(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to deserialize JSON data: " + ex.Message);
            }
        }

        public async Task<IActionResult> OnPostRemoveProductAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    CartProduct cartProduct = JsonConvert.DeserializeObject<CartProduct>(requestBody);

                    if (cartProduct != null)
                    {
                        List<CartProduct> cartItems = HttpContext.Request.Cookies.ContainsKey("CartItems")
                            ? JsonConvert.DeserializeObject<List<CartProduct>>(HttpContext.Request.Cookies["CartItems"])
                            : new List<CartProduct>();

                        CartProduct existingCartItem = cartItems.FirstOrDefault(item => item.ProductId == cartProduct.ProductId);
                        if (existingCartItem != null)
                        {
                            cartItems.Remove(existingCartItem);

                            HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(cartItems));
                            return new JsonResult(new { success = true, cartItems = cartItems });
                        }
                    }

                    return new JsonResult(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to deserialize JSON data: " + ex.Message);
            }
        }

    }
}
