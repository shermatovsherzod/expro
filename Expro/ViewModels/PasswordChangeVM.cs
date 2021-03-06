﻿using Expro.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class PasswordChangeVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "lblPassword", ResourceType = typeof(ResourceTexts))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "lblNewPassword", ResourceType = typeof(ResourceTexts))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "lblConfirmPassword", ResourceType = typeof(ResourceTexts))]
        [Compare("NewPassword", ErrorMessage = "Введенные пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

    }
}
