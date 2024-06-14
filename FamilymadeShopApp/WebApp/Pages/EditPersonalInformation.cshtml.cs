using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApp.Pages
{
    [Authorize]
    public class EditPersonalInformationModel : PageModel
    {
        private readonly IUserManager _userManager;
        private readonly IOrderManager _orderManager;

        public EditPersonalInformationModel(IUserManager userManager, IOrderManager orderManager)
        {
            _userManager = userManager;
            _orderManager = orderManager;
        }

        public Customer Customer { get; set; }

        [BindProperty]
        public CustomerPersonalInformation CustomerInfo { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                try
                {
                    Customer = _userManager.GetCustomerById(userId);
                    CustomerInfo = new CustomerPersonalInformation(Customer.Name, Customer.Email);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["MessageError"] = "Failed to load personal information. Please try again later.";
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(CustomerInfo.Password))
            {
                ModelState.Remove("CustomerInfo.Password");
            }

            if (CustomerInfo.PersonalPic == null)
            {
                ModelState.Remove("CustomerInfo.PersonalPic");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                try
                {
                    Customer customer = new Customer { Id = userId, Name = CustomerInfo .Name, Email = CustomerInfo.Email, Password = CustomerInfo.Password };
                    _userManager.UpdateCustomerAsync(customer);
                    if (CustomerInfo.PersonalPic != null)
                    {
                        await _userManager.AddPersonalPictureAsync(userId, CustomerInfo.PersonalPic);
                    }

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, customer.Name),
                        new Claim("id", customer.Id.ToString()),
                        new Claim("name", customer.Name)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity)).Wait();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["MessageError"] = "Failed to load personal information. Please try again later.";
                    return RedirectToPage("/Index");
                }
            }

            TempData["MessageSuccess"] = "Personal information updated successfully.";
            return RedirectToPage("/Index");
        }
    }

    public class CustomerPersonalInformation
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(24, ErrorMessage = "Name must be between {2} and {1} characters", MinimumLength = 4)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(24, ErrorMessage = "Password must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        public IFormFile PersonalPic { get; set; }

        //public Address ShippingAddress { get; set; }

        public CustomerPersonalInformation() { }

        public CustomerPersonalInformation(string name, string email)
        {
            Name = name;
            Email = email;
            Password = "";
            //ShippingAddress = shippingAddress;
        }
    }
}
