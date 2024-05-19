using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public abstract class User
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(24, ErrorMessage = "Name must be between {2} and {1} characters", MinimumLength = 4)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is invalid")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
        [StringLength(24, ErrorMessage = "Password must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
	}
}
