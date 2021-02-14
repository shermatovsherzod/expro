using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class ExpertTopInfoVM : ExpertListInfoVM
    {
        public ExpertTopInfoVM()
        {

        }
        
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ExpertTopInfoVM(ApplicationUser model)
            : base (model)
        {
            if (model == null)
                return;

            if (!string.IsNullOrWhiteSpace(model.AboutMe))
            {
                if (model.AboutMe.Length <= 100)
                    AboutMe = model.AboutMe;
                else
                    AboutMe = model.AboutMe.Substring(0, 100) + "...";
            }
        }
    }
}
