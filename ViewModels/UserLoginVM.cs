using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DDWAssignment.ViewModels //where projectName is YOUR project name
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class UserRegistrationVM
    {
        [Remote("doesUserNameExist", "User", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter a Password")]
        [StringLength(7, ErrorMessage = "Please choose a password with a mimimum length of 10 characters", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Remote("doesEmailExist", "User", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different Email.")]
        [Required(ErrorMessage = "Please Enter an Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter a valid Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }

    public class UserEditVM
    {
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter a Password")]
        [StringLength(7, ErrorMessage = "Please choose a password with a mimimum length of 10 characters", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter an Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter a valid Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }


}
