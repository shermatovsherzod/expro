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

        public DocumentPriceTypesEnum PriceType { get; set; }

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

            PriceType = model.PriceType;
            if (PriceType == DocumentPriceTypesEnum.Paid)
            {
                Price = model.Price.HasValue ?
                    model.Price.Value.ToString(AppData.Configuration.NumberViewStringFormat) : "0";
            }
            

            Status = new BaseDropdownableDetailsVM(model.DocumentStatus);
            Author = new AppUserVM(model.Creator);

            DateModified = DateTimeUtils.ConvertToString(model.DateModified);
        }
    }

    public class SampleDocumentDetailsForAdminVM : SampleDocumentListItemForAdminVM
    {
        [Display(Name = "Язык")]
        public BaseDropdownableDetailsVM Language { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

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

        public SampleDocumentDetailsForAdminVM() { }

        public SampleDocumentDetailsForAdminVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            Text = model.Text;
            Language = new BaseDropdownableDetailsVM(model.Language);

            LawAreas = model.DocumentLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList();

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
