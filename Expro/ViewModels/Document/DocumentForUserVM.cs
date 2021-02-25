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
    public class DocumentListItemForUserVM : BaseVM
    {
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        //[Display(Name = "Статус")]
        //public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Дата публикации")]
        public string DatePublished { get; set; }

        public DocumentPriceTypesEnum PriceType { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Display(Name = "Цена")]
        public string PriceStr { get; set; }

        public DocumentListItemForUserVM() { }

        public DocumentListItemForUserVM(Document model)
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
                Price = model.Price.HasValue ? model.Price.Value : 0;
                PriceStr = model.Price.HasValue ?
                    model.Price.Value.ToString(AppData.Configuration.NumberViewStringFormat) : "0";
            }

            //Status = new BaseDropdownableDetailsVM(model.DocumentStatus);
            Author = new AppUserVM(model.Creator);
            LawAreas = model.DocumentLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList();

            DatePublished = DateTimeUtils.ConvertToString(model.DatePublished);
        }
    }

    public class DocumentDetailsForUserVM : DocumentListItemForUserVM
    {
        [Display(Name = "Язык")]
        public BaseDropdownableDetailsVM Language { get; set; }

        //[Display(Name = "Направление")]
        //public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.file;

        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        public DocumentDetailsForUserVM() { }

        public DocumentDetailsForUserVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            Language = new BaseDropdownableDetailsVM(model.Language);

            ContentType = model.ContentType;
            Attachment = new AttachmentDetailsVM(model.Attachment);
            if (!string.IsNullOrWhiteSpace(model.Text))
                Text = model.Text;
        }
    }
}
