using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class QuestionSetAsPaidFormVM : BaseVM
    {
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources.ResourceTexts), ErrorMessageResourceName = "ErrorMustBePositive")]
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceTexts), ErrorMessageResourceName = "ErrorRequiredField")]
        [Display(Name = "FeeInSoums", ResourceType = typeof(Resources.ResourceTexts))]
        public int Price { get; set; }
    }
}
