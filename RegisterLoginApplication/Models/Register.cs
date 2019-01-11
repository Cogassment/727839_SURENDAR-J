using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.Web.Mvc.CompareAttribute;

/// <summary>
/// Demonstration about Registration Page: Property Creation/Declaration 
/// With Required Error Message and Regular Expression Pattern
/// </summary>

namespace RegisterLoginApplication.Models
{
    public class Register
    {
        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please Enter Only Alphabets.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please Enter Only Alphabets.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        //[RegularExpression(@"^[a-z0-9](\.?[a-z0-9]){5,}@g(oogle)?mail\.com$", ErrorMessage = "Please Enter Correct Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile number is Required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please Enter Only Alphabets.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}$", ErrorMessage = "Please Enter Valid Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}


