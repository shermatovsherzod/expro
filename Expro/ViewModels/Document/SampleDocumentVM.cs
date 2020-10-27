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
    public class SampleDocumentFreeCreateVM : BaseVM
    {
        [Required]
        [StringLength(1024)]
        [Display(Name = "Название")]
        public string Title { get; set; }

        public SampleDocumentFreeCreateVM() { }

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

        public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.file;

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

        public SampleDocumentFreeEditVM() { }

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
                ContentType = DocumentContentTypesEnum.file;
                AttachmentID = model.AttachmentID;
                Attachment = new AttachmentDetailsVM(model.Attachment);
            }
            else
            {
                ContentType = DocumentContentTypesEnum.text;
                Text = model.Text;
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

    public class SampleDocumentListItemForAdminVM : BaseVM
    {
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        [Display(Name = "Статус")]
        public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Дата изменения")]
        public string DateModified { get; set; }

        [Display(Name = "Цена")]
        public string Price { get; set; }

        public SampleDocumentListItemForAdminVM() { }

        public SampleDocumentListItemForAdminVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            if (model.Title.Length <= 100)
                Title = model.Title;
            else
                Title = model.Title.Substring(0, 100) + "...";

            Price = model.Price.HasValue ?
                model.Price.Value.ToString(AppData.Configuration.NumberViewStringFormat) : null;

            Status = new BaseDropdownableDetailsVM(model.DocumentStatus);
            Author = new AppUserVM(model.Creator);

            DateModified = DateTimeUtils.ConvertToString(model.DateModified);
        }
    }

    public class SampleDocumentListItemForPersonalCabVM : SampleDocumentListItemForAdminVM
    {
        //[Display(Name = "Название")]
        //public string Title { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.file;

        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        //[Display(Name = "Цена")]
        //public string Price { get; set; }

        //[Display(Name = "Автор")]
        //public AppUserVM Author { get; set; }

        //[Display(Name = "Статус")]
        //public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Дата публикации")]
        public string DatePublished { get; set; }

        public SampleDocumentListItemForPersonalCabVM() { }

        public SampleDocumentListItemForPersonalCabVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            //Title = model.Title;

            LawAreas = model.DocumentLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList();

            ContentType = model.ContentType;
            Attachment = new AttachmentDetailsVM(model.Attachment);

            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                if (model.Text.Length <= 200)
                    Text = model.Text;
                else
                    Text = model.Text.Substring(0, 200) + "...";
            }
            
            //Price = model.Price.HasValue ?
            //    model.Price.Value.ToString(AppData.Configuration.NumberViewStringFormat) : null;

            //Status = new BaseDropdownableDetailsVM(model.DocumentStatus);
            //Author = new AppUserVM(model.Creator);

            DatePublished = DateTimeUtils.ConvertToString(model.DateApproved);
        }
    }

    public class SampleDocumentDetailsVM : SampleDocumentListItemForPersonalCabVM
    {
        [Display(Name = "Язык")]
        public BaseDropdownableDetailsVM Language { get; set; }

        [Display(Name = "Дата отправки администратору")]
        public string DateSubmittedForApproval { get; set; }

        [Display(Name = "Дата ответа администратора")]
        public string DateAdminResponsed { get; set; }

        public SampleDocumentDetailsVM() { }

        public SampleDocumentDetailsVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            Text = model.Text;
            Language = new BaseDropdownableDetailsVM(model.Language);

            if (!string.IsNullOrWhiteSpace(model.Text))
                Text = model.Text;

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

    public class SampleDocumentPaidCreateVM : SampleDocumentFreeCreateVM
    {
        public SampleDocumentPaidCreateVM() { }

        public SampleDocumentPaidCreateVM(Document model)
            : base(model)
        {
        }

        public override Document ToModel(Document model = null)
        {
            var mmodel = base.ToModel(model);
            return mmodel;
        }
    }

    public class SampleDocumentPaidEditVM : SampleDocumentFreeEditVM
    {
        [Required]
        [Display(Name = "Цена")]
        public int? Price { get; set; }

        public SampleDocumentPaidEditVM() { }

        public SampleDocumentPaidEditVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Price = model.Price;
        }

        public override Document ToModel(Document model = null)
        {
            var mmodel = base.ToModel(model);;
            mmodel.Price = this.Price;

            return mmodel;
        }
    }
}
