using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppEcom.Shared
{
    public class UserRegistration
    {
        [Required,EmailAddress]
        public string Email { get; set; }=string.Empty;
        [Required,StringLength(100),MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password",ErrorMessage ="Password Didn't Match")]
        public string ConfirmPassword { get; set; }=string.Empty;
    }
}
