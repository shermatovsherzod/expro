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
    public class QuestionDetailsForSiteVM : DocumentListItemForSiteVM
    {
        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        //public List<CommentDetailsVM> Comments { get; set; }
        public List<QuestionAnswerDetailsVM> Answers { get; set; }
        

        public QuestionDetailsForSiteVM() { }

        public QuestionDetailsForSiteVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            Text = model.Text;
            Attachment = new AttachmentDetailsVM(model.Attachment);

            //Comments = new List<CommentDetailsVM>();
            //foreach (var item in model.DocumentComments)
            //{
            //    Comments.Add(new CommentDetailsVM(item.Comment));
            //}

            Answers = new List<QuestionAnswerDetailsVM>();
            foreach (var item in model.Answers)
            {
                Answers.Add(new QuestionAnswerDetailsVM(item));
            }
        }
    }
}
