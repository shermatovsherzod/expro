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
    public class QuestionFreeCreateVM : BaseVM
    {
        [Required]
        [StringLength(1024)]
        [Display(Name = "Название")]
        public string Title { get; set; }

        public QuestionFreeCreateVM() { }

        public QuestionFreeCreateVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
        }

        public virtual Question ToModel(Question model = null)
        {
            if (model == null)
            {
                model = new Question();
                model.PriceType = DocumentPriceTypesEnum.Free;
            }

            model.Title = this.Title;

            return model;
        }
    }

    public class QuestionFreeEditVM : QuestionFreeCreateVM
    {
        [Required]
        [Display(Name = "Направление")]
        public List<int> LawAreas { get; set; }

        //public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.file;

        [Display(Name = "Файл")]
        //[Remote("ValidateAttachment", "SampleDocument", ErrorMessage = "Обязательное поле", AdditionalFields = "DocumentContentType")]
        public int? AttachmentID { get; set; }

        public AttachmentDetailsVM Attachment { get; set; }

        [Required]
        [Display(Name = "Текст")]
        //[Remote("ValidateText", "SampleDocument", ErrorMessage = "Обязательное поле", AdditionalFields = "DocumentContentType")]
        public string Text { get; set; }

        //[Remote]
        public DocumentActionTypesEnum ActionType { get; set; } = DocumentActionTypesEnum.saveAsDraft;

        public int StatusID { get; set; }

        public QuestionFreeEditVM() { }

        public QuestionFreeEditVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

            LawAreas = model.QuestionLawAreas?
                .Select(m => m.LawAreaID)
                .ToList()
                ?? new List<int>();

            Text = model.Text;

            if (model.AttachmentID.HasValue)
            {
                AttachmentID = model.AttachmentID;
                Attachment = new AttachmentDetailsVM(model.Attachment);
            }

            StatusID = model.DocumentStatusID;
        }

        public override Question ToModel(Question model = null)
        {
            var mmodel = base.ToModel(model);
            mmodel.Text = this.Text;

            return mmodel;
        }
    }

    public class QuestionPaidCreateVM : QuestionFreeCreateVM
    {
        public QuestionPaidCreateVM() { }

        public QuestionPaidCreateVM(Question model)
            : base(model)
        {
        }

        public override Question ToModel(Question model = null)
        {
            var mmodel = base.ToModel(model);
            mmodel.PriceType = DocumentPriceTypesEnum.Paid;

            return mmodel;
        }
    }

    public class QuestionPaidEditVM : QuestionFreeEditVM
    {
        [Required]
        [Display(Name = "Цена")]
        public int? Price { get; set; }

        public QuestionPaidEditVM() { }

        public QuestionPaidEditVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

            Price = model.Price;
        }

        public override Question ToModel(Question model = null)
        {
            var mmodel = base.ToModel(model);
            mmodel.Price = this.Price;
            //model.PriceType = DocumentPriceTypesEnum.Paid;

            return mmodel;
        }
    }
}
