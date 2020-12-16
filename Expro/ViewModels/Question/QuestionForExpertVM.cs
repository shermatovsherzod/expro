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
    public class QuestionListItemForExpertVM : QuestionListItemForAdminVM
    {

        public QuestionListItemForExpertVM() { }

        public QuestionListItemForExpertVM(Question model)
            : base(model)
        {
            if (model == null)
                return;
        }
    }
}
