using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using System.ComponentModel.DataAnnotations;

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
        public IFormFile PersonalPic { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                try
                {
                    Customer = _userManager.GetCustomerById(userId);

                    CustomerInfo = new CustomerPersonalInformation(Customer.Name, Customer.Email, new Address());

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

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(CustomerInfo.Password))
            {
                ModelState.Remove("CustomerInfo.Password");
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
                    _userManager.AddPersonalPictureAsync(userId, PersonalPic);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["MessageError"] = "Failed to load personal information. Please try again later.";
                    return RedirectToPage("/Index");
                }
            }

            return RedirectToPage("/PersonalInformation");
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

        public Address ShippingAddress { get; set; }

        public CustomerPersonalInformation() { }

        public CustomerPersonalInformation(string name, string email, Address shippingAddress)
        {
            Name = name;
            Email = email;
            Password = "";
            ShippingAddress = shippingAddress;
        }
    }
}
