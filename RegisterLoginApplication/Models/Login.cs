using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

/// <summary>
/// Demonstration about Login Page: Property Creation/Declaration 
/// With Required Error Message
/// </summary>

namespace RegisterLoginApplication.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter EmailId")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
