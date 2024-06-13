using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ModelLayer.Models;
using BusinessLogicLayer.Interfaces;

namespace WebApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IProductManager _productManager;
        private readonly ICartManager _cartManager;

        public List<CartProduct> CartItems { get; set; }

        public CartModel(IProductManager productManager, ICartManager cartManager)
        {
            _productManager = productManager;
            _cartManager = cartManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey("CartItems"))
            {
                CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

                foreach (CartProduct cartItem in CartItems)
                {
                    try
                    {
                        Product product = _productManager.GetProductById(cartItem.ProductId);
                        cartItem.Name = product.Name;
                        cartItem.Price = product.Price;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        TempData["MessageError"] = "Failed to load the cart. Please try again later.";
                        return RedirectToPage("/Index");
                    }
                }

                HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(CartItems));
            }
            else
            {
                CartItems = new List<CartProduct>();
            }

            return Page();
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
                            if (User.Identity.IsAuthenticated)
                            {
                                var userIdClaim = User.FindFirst("id");
                                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                                {
                                    try
                                    {
                                        _cartManager.UpdateProductQuantityInCart(userId, cartProduct.ProductId, cartProduct.Quantity);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        TempData["MessageError"] = "Failed to add product to cart. Please try again later.";
                                        return new BadRequestResult();
                                    }
                                }
                            }

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
                            if (User.Identity.IsAuthenticated)
                            {
                                var userIdClaim = User.FindFirst("id");
                                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                                {
                                    try 
                                    {
                                        _cartManager.RemoveCartByCustomerId(userId);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        TempData["MessageError"] = "Failed to remove product from cart. Please try again later.";
                                        return new BadRequestResult();
                                    }
                                }
                            }

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
