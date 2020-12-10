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
    public class QuestionListItemForUserVM : DocumentListItemForExpertVM
    {
        public bool IsCompleted { get; set; }
        public bool FeeIsDistributed { get; set; }

        public QuestionListItemForUserVM() { }

        public QuestionListItemForUserVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;

            IsCompleted = model.QuestionIsCompleted ?? false;
            FeeIsDistributed = model.QuestionFeeIsDistributed ?? false;
        }
    }
}
