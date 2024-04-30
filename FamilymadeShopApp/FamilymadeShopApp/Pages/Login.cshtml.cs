using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FamilymadeShopApp.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Credential Credential { get; set; }

        //public User User { get; set; }
        //public Login Login { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Login login = new Login();
            Customer customer = login.AuthenticateCustomer(this.Credential.Email, this.Credential.Password);

            if (customer == null)
            {
                return Page();
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, customer.Name));
            claims.Add(new Claim("id", customer.Id.ToString()));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Index");
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
