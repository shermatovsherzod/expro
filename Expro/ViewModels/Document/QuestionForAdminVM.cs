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
    public class QuestionListItemForAdminVM : QuestionListItemForUserVM
    {
        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        public QuestionListItemForAdminVM() { }

        public QuestionListItemForAdminVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Author = new AppUserVM(model.Creator);
        }
    }
}
