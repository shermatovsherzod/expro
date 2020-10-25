using Expro.Common;
using Expro.Models;
using Expro.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class SampleDocumentFreeCreateVM : BaseVM
    {
        [Required]
        [StringLength(1024)]
        [Display(Name = "Название")]
        public string Title { get; set; }

        public SampleDocumentFreeCreateVM()
        {
        }

        public SampleDocumentFreeCreateVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
        }

        public virtual Document ToModel(Document model = null)
        {
            if (model == null)
                model = new Document();

            model.Title = this.Title;

            return model;
        }
    }

    public class SampleDocumentFreeEditVM : SampleDocumentFreeCreateVM
    {
        [Display(Name = "Язык")]
        public int LanguageID { get; set; }

        [Required]
        [Display(Name = "Направление")]
        public List<int> LawAreas { get; set; }

        public DocumentContentTypesEnum DocumentContentType { get; set; } = DocumentContentTypesEnum.file;

        [Display(Name = "Файл")]
        //[Remote("ValidateAttachment", "SampleDocument", ErrorMessage = "Обязательное поле", AdditionalFields = "DocumentContentType")]
        public int? AttachmentID { get; set; }

        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Текст")]
        //[Remote("ValidateText", "SampleDocument", ErrorMessage = "Обязательное поле", AdditionalFields = "DocumentContentType")]
        public string Text { get; set; }

        //[Remote]
        public DocumentActionTypesEnum ActionType { get; set; } = DocumentActionTypesEnum.saveAsDraft;

        public SampleDocumentFreeEditVM()
        { 
        }

        public SampleDocumentFreeEditVM(Document model)
            : base(model)
        {
            if (model == null)
                return;
            
            LanguageID = model.LanguageID ?? 0;
            LawAreas = model.DocumentLawAreas?
                .Select(m => m.LawAreaID)
                .ToList() 
                ?? new List<int>();

            if (model.AttachmentID.HasValue)
            {
                DocumentContentType = DocumentContentTypesEnum.file;
                AttachmentID = model.AttachmentID;
                Attachment = new AttachmentDetailsVM(model.Attachment);
            }
            else
            {
                DocumentContentType = DocumentContentTypesEnum.text;
                Text = model.Text;
            }
        }

        public override Document ToModel(Document model = null)
        {
            var mmodel = base.ToModel(model);
            //if (model == null)
            //    model = new Document();

            //model.Title = this.Title;
            model.LanguageID = this.LanguageID;
            //model.DocumentLawAreas
            model.ContentType = this.DocumentContentType;
            //model.AttachmentID = this.AttachmentID;
            model.Text = this.Text;

            return model;
        }
    }
}
