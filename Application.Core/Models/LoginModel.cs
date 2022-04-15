using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="This is email-type field")]
        public string Email { get; set; }

       
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
    }
}
