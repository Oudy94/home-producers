using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Models;

namespace FamilymadeShopApp.Pages
{
    public class RegisterModel : PageModel
    {
        public UserManager UserManager { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public void OnGet()
        {

        }

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			UserManager = new UserManager();

			Customer = new Customer
			{
				Name = Customer.Name,
				Email = Customer.Email,
				Password = Customer.Password,
			};

			UserManager.Add(Customer);

			return RedirectToPage("/Index");
		}

	}
}
