using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Newtonsoft.Json;
using BusinessLogicLayer.Interfaces;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserManager _userManager;
        private readonly ICartManager _cartManager;

        public LoginModel(IUserManager userManager, ICartManager cartManager)
        {
            _userManager = userManager;
            _cartManager = cartManager;
        }

        [BindProperty]
        public LoginCredential Credential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer customer;
            try
            {
                customer = _userManager.GetCustomerByCredentials(Credential.Email, Credential.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["MessageError"] = "Failed to login. Please try again later.";
                return RedirectToPage("/Index");
            }

            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "No user found with these credentials.");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim("id", customer.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity)).Wait();

            MergeCartWithDatabase(customer.Id);

            string returnUrl = Request.Query["ReturnUrl"];
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return LocalRedirect("/Index");
            }
        }

        private async Task<IActionResult> MergeCartWithDatabase(int customerId)
        {
            List<CartProduct> cartItemsFromCookies = HttpContext.Request.Cookies.ContainsKey("CartItems")
                ? JsonConvert.DeserializeObject<List<CartProduct>>(HttpContext.Request.Cookies["CartItems"])
                : new List<CartProduct>();

            Cart cartFromDB;
            try
            {
                cartFromDB = _cartManager.GetCartByCustomerId(customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["MessageError"] = "Failed to get cart from database. Please try again later.";
                return RedirectToPage("/Index");
            }

            if (cartFromDB == null)
            {
                cartFromDB = new Cart
                {
                    Customer = new Customer { Id = customerId },
                    CartProducts = new List<CartProduct>()
                };
            }

            foreach (var cartProductFromCookies in cartItemsFromCookies)
            {
                var existingProduct = cartFromDB.CartProducts.FirstOrDefault(p => p.ProductId == cartProductFromCookies.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += cartProductFromCookies.Quantity;
                }
                else
                {
                    cartFromDB.CartProducts.Add(cartProductFromCookies);
                }
            }

            try
            {
                _cartManager.AddCart(cartFromDB);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["MessageError"] = "Failed to add cart to database. Please try again later.";
                return RedirectToPage("/Index");
            }

            Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(cartFromDB.CartProducts));

            return new OkResult();
        }
    }

    public class LoginCredential
    {
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "format is invalid")]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "is required")]
        [DataType(DataType.Password)]
        [StringLength(24, ErrorMessage = "must be between {2} and {1} characters", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
