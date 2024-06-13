using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using BusinessLogicLayer.Interfaces;

namespace WebApp.Pages
{
    [Authorize]
	public class PersonalInformationModel : PageModel
	{
		private readonly IUserManager _userManager;
        private readonly IOrderManager _orderManager;

        public PersonalInformationModel(IUserManager userManager, IOrderManager orderManager)
        {
            _userManager = userManager;
            _orderManager = orderManager;
        }

        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
        public string PersonalPicture { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                try
                {
                    Customer = _userManager.GetCustomerById(userId);
                    Orders = _orderManager.GetOrdersByUserId(userId);
                    byte [] personalPicture = await _userManager.GetPersonalPictureAsync(userId);
                    if (personalPicture !=  null)
                    {
                        PersonalPicture = Convert.ToBase64String(personalPicture);
                    }
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
    }
}
