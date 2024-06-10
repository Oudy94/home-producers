using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DataAccessLayer.DataAccess;
using Newtonsoft.Json;
using DataAccessLayer.Interfaces;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Credential Credential { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserManager userManager = new UserManager(new UserRepository());
            Customer customer = userManager.GetCustomerByCredentials(this.Credential.Email, this.Credential.Password);

            if (customer == null)
            {
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

        private void MergeCartWithDatabase(int customerId)
        {
            List<CartProduct> cartItemsFromCookies = HttpContext.Request.Cookies.ContainsKey("CartItems")
                ? JsonConvert.DeserializeObject<List<CartProduct>>(HttpContext.Request.Cookies["CartItems"])
                : new List<CartProduct>();

            CartManager cartManager = new CartManager(new CartRepository());
            Cart cartFromDB = cartManager.GetCartByCustomerId(customerId);

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

            cartManager.AddCart(cartFromDB);
            Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(cartFromDB.CartProducts));
        }
    }

    public class Credential
    {
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "format is invalid")]
        [Display(Name = "Email-address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "is required")]
        [DataType(DataType.Password)]
        [StringLength(24, ErrorMessage = "must be between {2} and {1} characters", MinimumLength = 2)]
        public string Password { get; set; }
    }
}
