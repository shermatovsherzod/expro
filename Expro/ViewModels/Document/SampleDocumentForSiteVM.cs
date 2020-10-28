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
    public class SampleDocumentListItemForSiteVM : BaseVM
    {
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public AppUserVM Author { get; set; }

        [Display(Name = "Цена")]
        public string Price { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        public DocumentContentTypesEnum ContentType { get; set; } = DocumentContentTypesEnum.file;

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Дата публикации")]
        public string DatePublished { get; set; }

        public SampleDocumentListItemForSiteVM() { }

        public SampleDocumentListItemForSiteVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            if (model.Title.Length <= 100)
                Title = model.Title;
            else
                Title = model.Title.Substring(0, 100) + "...";

            LawAreas = model.DocumentLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList();

            ContentType = model.ContentType;

            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                if (model.Text.Length <= 200)
                    Text = model.Text;
                else
                    Text = model.Text.Substring(0, 200) + "...";
            }

            Price = model.Price.HasValue ?
                model.Price.Value.ToString(AppData.Configuration.NumberViewStringFormat) : null;

            Author = new AppUserVM(model.Creator);
            DatePublished = DateTimeUtils.ConvertToString(model.DateApproved);
        }
    }

    public class SampleDocumentDetailsForSiteVM : SampleDocumentListItemForSiteVM
    {
        [Display(Name = "Язык")]
        public BaseDropdownableDetailsVM Language { get; set; }

        [Display(Name = "Файл")]
        public AttachmentDetailsVM Attachment { get; set; }

        public SampleDocumentDetailsForSiteVM() { }

        public SampleDocumentDetailsForSiteVM(Document model)
            : base(model)
        {
            if (model == null)
                return;

            Title = model.Title;
            Language = new BaseDropdownableDetailsVM(model.Language);
            Attachment = new AttachmentDetailsVM(model.Attachment);
            if (!string.IsNullOrWhiteSpace(model.Text))
                Text = model.Text;
        }
    }
}
