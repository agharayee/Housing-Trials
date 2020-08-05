using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EstateApplication.Web.Models
{
    public class RegisterModel
    {
        [DisplayName("Full Name")]
        [Required]
        public  string FullName { get; set; }

        [DisplayName("Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Comfirm Password")]
        [Compare(nameof (Password))]
        public string ComfirmPassword { get; set; }

    }
}
