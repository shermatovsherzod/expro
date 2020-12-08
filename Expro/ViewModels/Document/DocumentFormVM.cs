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
    public class DocumentFreeCreateVM : BaseVM
    {
        [Required]
        [StringLength(1024)]
        [Display(Name = "Название")]
        public string Title { get; set; }

        public DocumentFreeCreateVM() { }

        public DocumentFreeCreateVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
        }

        public virtual Document ToModel(Document model = null)
        {
            if (model == null)
            {
                model = new Document();
                model.PriceType = DocumentPriceTypesEnum.Free;
            }

            model.Title = this.Title;

            return model;
        }
    }

    public class DocumentFreeEditVM : DocumentFreeCreateVM
    {
        [Display(Name = "Язык")]
        public int LanguageID { get; set; }

        [Required]
        [Display(Name = "Направление")]
        public List<int> LawAreas { get; set; }

        public DocumentContentTypesEnum ContentType { get; set; }// = DocumentContentTypesEnum.file;

        [Display(Name = "Файл")]
        //[Remote("ValidateAttachment", "SampleDocument", ErrorMessage = "Обязательное поле", AdditionalFields = "DocumentContentType")]
        public int? AttachmentID { get; set; }

        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Текст")]
        //[Remote("ValidateText", "SampleDocument", ErrorMessage = "Обязательное поле", AdditionalFields = "DocumentContentType")]
        public string Text { get; set; }

        //[Remote]
        public DocumentActionTypesEnum ActionType { get; set; } = DocumentActionTypesEnum.saveAsDraft;

        public int StatusID { get; set; }

        public DocumentFreeEditVM() { }

        public DocumentFreeEditVM(Document model)
            : base(model)
        {
            if (model == null)
                return;
            
            LanguageID = model.LanguageID ?? 0;
            LawAreas = model.DocumentLawAreas?
                .Select(m => m.LawAreaID)
                .ToList() 
                ?? new List<int>();

            ContentType = model.ContentType;
            Text = model.Text;
            if (model.AttachmentID.HasValue)
            {   
                AttachmentID = model.AttachmentID;
                Attachment = new AttachmentDetailsVM(model.Attachment);
            }

            StatusID = model.DocumentStatusID;
        }

        public override Document ToModel(Document model = null)
        {
            var mmodel = base.ToModel(model);
            //if (model == null)
            //    model = new Document();

            //model.Title = this.Title;
            mmodel.LanguageID = this.LanguageID;
            //model.DocumentLawAreas
            mmodel.ContentType = this.ContentType;
            //model.AttachmentID = this.AttachmentID;
            mmodel.Text = this.Text;

            return mmodel;
        }
    }

    public class DocumentPaidCreateVM : DocumentFreeCreateVM
    {
        public DocumentPaidCreateVM() { }

        public DocumentPaidCreateVM(Document model)
            : base(model)
        {
        }

        public override Document ToModel(Document model = null)
        {
            var mmodel = base.ToModel(model);
            mmodel.PriceType = DocumentPriceTypesEnum.Paid;

            return mmodel;
        }
    }

    public class DocumentPaidEditVM : DocumentFreeEditVM
    {
        [Required]
        [Display(Name = "Цена")]
        public int? Price { get; set; }

        public DocumentPaidEditVM() { }

        public DocumentPaidEditVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Price = model.Price;
        }

        public override Document ToModel(Document model = null)
        {
            var mmodel = base.ToModel(model);
            mmodel.Price = this.Price;
            //model.PriceType = DocumentPriceTypesEnum.Paid;

            return mmodel;
        }
    }
}
