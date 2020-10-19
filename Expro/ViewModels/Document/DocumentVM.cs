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
    public class SampleDocumentFreeFormVM : BaseVM
    {
        [Required]
        [StringLength(1024)]
        [Display(Name = "Название")]
        public string Title { get; set; }

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

        public SampleDocumentFreeFormVM()
        { 
        }

        public SampleDocumentFreeFormVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            LanguageID = model.LanguageID;
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

        public Document ToModel(Document model = null)
        {
            if (model == null)
                model = new Document();

            model.Title = this.Title;
            model.LanguageID = this.LanguageID;
            //model.DocumentLawAreas
            model.Text = null;
            model.AttachmentID = null;
            if (DocumentContentType == DocumentContentTypesEnum.file)
                model.AttachmentID = this.AttachmentID;
            else
                model.Text = this.Text;

            return model;
        }
    }
}
