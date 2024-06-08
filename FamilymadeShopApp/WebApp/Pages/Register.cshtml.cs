using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;

namespace WebApp.Pages
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

			UserManager = new UserManager(new UserRepository());

			Customer = new Customer
			{
				Name = Customer.Name,
				Email = Customer.Email,
				Password = Customer.Password,
			};

			UserManager.AddCustomer(Customer);

			return RedirectToPage("/Index");
		}

	}
}
