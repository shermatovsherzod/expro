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
    public class QuestionListItemForUserVM : BaseVM
    {
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Статус")]
        public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        [Display(Name = "Дата изменения")]
        public string DateModified { get; set; }

        public DocumentPriceTypesEnum PriceType { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Display(Name = "Цена")]
        public string PriceStr { get; set; }

        public bool IsCompleted { get; set; }
        public bool FeeIsDistributed { get; set; }

        public QuestionListItemForUserVM() { }

        public QuestionListItemForUserVM(Question model)
            : base(model)
        {
            if (model == null)
                return;

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

            LawAreas = new List<BaseDropdownableDetailsVM>();
            if (model.LawAreaParent != null)
                LawAreas.Add(new BaseDropdownableDetailsVM(model.LawAreaParent));
            LawAreas.AddRange(model.QuestionLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList());

            Status = new BaseDropdownableDetailsVM(model.DocumentStatus);
            DateModified = DateTimeUtils.ConvertToString(model.DateModified);

            //Title = model.Title;

            IsCompleted = model.QuestionIsCompleted ?? false;
            FeeIsDistributed = model.QuestionFeeIsDistributed ?? false;
        }
    }
}
