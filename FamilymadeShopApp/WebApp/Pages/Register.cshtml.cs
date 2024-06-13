using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using SharedLayer.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Interfaces;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserManager _userManager;

        [BindProperty]
        public RegisterCredential Credential { get; set; }

        public RegisterModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Customer customer = new Customer
                {
                    Name = Credential.Name,
                    Email = Credential.Email,
                    Password = Credential.Password,
                };

                _userManager.AddCustomer(customer);

                TempData["MessageSuccess"] = "Account created successfully";
                return RedirectToPage("/Index");
            }
            catch (ExistingEmailException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while adding the customer. Please try again later.");
                return Page();
            }
        }
    }

    public class RegisterCredential
    {
        [Required(ErrorMessage = "is required")]
        [StringLength(24, ErrorMessage = "must be between {2} and {1} characters", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

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
