﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class ResetPasswordModel
    {
        

       [Required]
       [Display(Name = "Temp Password")]
       [DataType(DataType.Password)]
       public string TempPassword { get; set; }

       [Required]
       [Display(Name = "New Password")]
       [DataType(DataType.Password)]
       public string Password { get; set; }

       [Required]
       [Display(Name = "Confirm Password")]
       [DataType(DataType.Password)]
       [Compare("Password", ErrorMessage = "New password and confirmation does not match.")]
       public string ConfirmPassword { get; set; }

       public string ReturnToken { get; set; }
    }
}