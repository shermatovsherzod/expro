﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;

namespace Expro.ViewModels
{
    public class QuestionAnswerCreateVM
    {
        public int QuestionID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ResourceTexts), ErrorMessageResourceName = "ErrorRequiredField")]
        [Display(Name = "AnswerText", ResourceType = typeof(Resources.ResourceTexts))]
        public string Text { get; set; }

        public QuestionAnswer ToModel()
        {
            QuestionAnswer answer = new QuestionAnswer()
            {
                Text = this.Text,
                QuestionID = this.QuestionID
            };

            return answer;
        }
    }

    public class QuestionAnswerDetailsVM : BaseVM
    {
        public string Text { get; set; }

        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        [Display(Name = "Дата")]
        public string DatePublished { get; set; }

        public int PositiveLikesCount { get; set; }
        public int NegativeLikesCount { get; set; }

        public List<CommentDetailsVM> Comments { get; set; }

        public int? PaidFee { get; set; }
        public string PaidFeeStr { get; set; }

        public bool? ViewerUserHasLikeOrDislike { get; set; }

        public QuestionAnswerDetailsVM() { }

        public QuestionAnswerDetailsVM(QuestionAnswer model, string viewerUserID = null)
            : base(model)
        {
            if (model == null)
                return;

            Text = model.Text;
            Attachment = new AttachmentDetailsVM(model.Attachment);
            Author = new AppUserVM(model.Creator);
            DatePublished = DateTimeUtils.ConvertToString(model.DateCreated);
            PositiveLikesCount = model.QuestionAnswerLikes.Where(m => m.Like.IsPositive).Count();
            NegativeLikesCount = model.QuestionAnswerLikes.Count - PositiveLikesCount;

            Comments = new List<CommentDetailsVM>();
            foreach (var item in model.Comments)
            {
                Comments.Add(new CommentDetailsVM(item.Comment));
            }

            PaidFee = model.PaidFee;
            if (PaidFee.HasValue)
                PaidFeeStr = model.PaidFee.Value.ToString(AppData.Configuration.NumberViewStringFormat);

            ViewerUserHasLikeOrDislike = model.QuestionAnswerLikes
                .Where(m => m.Like.CreatedBy == viewerUserID)
                .FirstOrDefault()?.Like.IsPositive;
        }
    }

    public class QuestionFeeDistributionVM
    {
        public int QuestionID { get; set; }
        public List<DistributedQuestionAnswerVM> Answers { get; set; }
    }

    public class DistributedQuestionAnswerVM
    {
        public int AnswerID { get; set; }
        public int Percentage { get; set; }
    }
}
