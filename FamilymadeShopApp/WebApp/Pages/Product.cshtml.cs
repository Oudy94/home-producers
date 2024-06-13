using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ModelLayer.Models;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductManager _productManager;
        private readonly ICartManager _cartManager;

        public Product Product { get; set; }

        public ProductModel(IProductManager productManager, ICartManager cartManager)
        {
            _productManager = productManager;
            _cartManager = cartManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Product = _productManager.GetProductById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["MessageError"] = "Failed to load the product. Please try again later.";
                return RedirectToPage("/Index");
            }

            return Page();
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
                        return await UpdateCart(cartProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in UpdateCart: {ex.Message}");
                TempData["MessageError"] = "Failed to update cart. Please try again later.";
                return new BadRequestResult();
            }

            return BadRequest("Invalid request");
        }

        private async Task<IActionResult> UpdateCart(CartProduct cartProduct)
        {
            try
            {
                List<CartProduct> cartItems = HttpContext.Request.Cookies.ContainsKey("CartItems")
                    ? JsonConvert.DeserializeObject<List<CartProduct>>(HttpContext.Request.Cookies["CartItems"])
                    : new List<CartProduct>();


                if (User.Identity.IsAuthenticated)
                {
                    var userIdClaim = User.FindFirst("id");
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                    {
                        try
                        {
                            _cartManager.AddProductToCart(userId, cartProduct.ProductId, cartProduct.Quantity);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception occurred in UpdateCart: {ex.Message}");
                            TempData["MessageError"] = "Failed to update cart. Please try again later.";
                            return new BadRequestResult();
                        }
                    }
                }

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

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in UpdateCart: {ex.Message}");
                TempData["MessageError"] = "Failed to update cart. Please try again later.";
                return new BadRequestResult();
            }
        }
    }
}
