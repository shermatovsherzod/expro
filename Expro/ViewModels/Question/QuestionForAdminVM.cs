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

        public QuestionListItemForAdminVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

            Author = new AppUserVM(model.Creator);
        }
    }

    public class QuestionDetailsForAdminVM : QuestionListItemForAdminVM
    {
        [Display(Name = "Язык")]
        public BaseDropdownableDetailsVM Language { get; set; }

        public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.file;

        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        //[Display(Name = "Дата публикации")]
        //public string DatePublished { get; set; }

        [Display(Name = "Дата отправки администратору")]
        public string DateSubmittedForApproval { get; set; }

        [Display(Name = "Дата ответа администратора")]
        public string DateAdminResponsed { get; set; }

        public QuestionDetailsForAdminVM() { }

        public QuestionDetailsForAdminVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;

            Attachment = new AttachmentDetailsVM(model.Attachment);
            if (!string.IsNullOrWhiteSpace(model.Text))
                Text = model.Text;

            //DatePublished = DateTimeUtils.ConvertToString(model.DateApproved);
            DateSubmittedForApproval = DateTimeUtils.ConvertToString(model.DateSubmittedForApproval);

            if (model.DocumentStatusID == (int)DocumentStatusesEnum.Approved
                && model.DateApproved.HasValue)
            {
                DateAdminResponsed = DateTimeUtils.ConvertToString(model.DateApproved);
            }
            else if (model.DocumentStatusID == (int)DocumentStatusesEnum.Rejected
                && model.DateRejected.HasValue)
            {
                DateAdminResponsed = DateTimeUtils.ConvertToString(model.DateRejected);
            }
        }
    }
}
