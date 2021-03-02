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
    public class QuestionListItemForSiteVM : BaseVM
    {
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        public DocumentPriceTypesEnum PriceType { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Display(Name = "Цена")]
        public string PriceStr { get; set; }

        [Display(Name = "Дата публикации")]
        public string DatePublished { get; set; }

        public bool IsCompleted { get; set; }
        public bool FeeIsDistributed { get; set; }

        public QuestionListItemForSiteVM() { }

        public QuestionListItemForSiteVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

            if (model.Title.Length <= 100)
                Title = model.Title;
            else
                Title = model.Title.Substring(0, 100) + "...";

            LawAreas = new List<BaseDropdownableDetailsVM>();
            if (model.LawAreaParent != null)
                LawAreas.Add(new BaseDropdownableDetailsVM(model.LawAreaParent));
            LawAreas.AddRange(model.QuestionLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList());

            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                if (model.Text.Length <= 200)
                    Text = model.Text;
                else
                    Text = model.Text.Substring(0, 200) + "...";
            }

            PriceType = model.PriceType;
            if (PriceType == DocumentPriceTypesEnum.Paid)
            {
                Price = model.Price.HasValue ? model.Price.Value : 0;
                PriceStr = model.Price.HasValue ?
                    model.Price.Value.ToString(AppData.Configuration.NumberViewStringFormat) : "0";
            }

            Author = new AppUserVM(model.Creator);
            DatePublished = DateTimeUtils.ConvertToString(model.DatePublished);

            IsCompleted = model.QuestionIsCompleted ?? false;
            FeeIsDistributed = model.QuestionFeeIsDistributed ?? false;
        }
    }

    public class QuestionDetailsForSiteVM : QuestionListItemForSiteVM
    {
        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        public List<CommentDetailsVM> Comments { get; set; }
        public List<QuestionAnswerDetailsVM> Answers { get; set; }

        public bool IsCompleted { get; set; }
        public bool FeeIsDistributed { get; set; }

        public QuestionDetailsForSiteVM() { }

        public QuestionDetailsForSiteVM(Question model, string viewerUserID = null)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            Text = model.Text;
            Attachment = new AttachmentDetailsVM(model.Attachment);

            Answers = new List<QuestionAnswerDetailsVM>();
            foreach (var item in model.Answers)
            {
                Answers.Add(new QuestionAnswerDetailsVM(item, viewerUserID));
            }

            IsCompleted = model.QuestionIsCompleted ?? false;
            FeeIsDistributed = model.QuestionFeeIsDistributed ?? false;
        }
    }
}
