using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace dat_q_ngo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Your name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
         ErrorMessage = "Name is in valid.")]
        public string Name {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^([a-zA-Z0-9!@#$%^&*()~_+=-]{8,15})$", 
         ErrorMessage = "Password is invalid.")]
        public string Password {get; set;}

    }
}
