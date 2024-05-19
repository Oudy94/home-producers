using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class Admin : User
    {
		[Required(ErrorMessage = "Role is required")]
		public Role Role { get; set; }
    }
}
