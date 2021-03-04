using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Expro.Services
{
    public class BaseDropdownableService<T> : BaseCRUDService<T>
        where T : BaseModelDropdownable
    {
        private IBaseDropdownableRepository<T> _repositoryDropdownable;

        public BaseDropdownableService(
            IBaseDropdownableRepository<T> repository,
            Data.Infrastructure.IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;

            _repositoryDropdownable = repository;
        }

        public IQueryable<T> GetWithLocalizedNameAsIQueryable()
        {
            return _repositoryDropdownable.GetWithLocalizedNameAsIQueryable();
        }


        public virtual List<SelectListItem> GetAsSelectList(int[] selected = null, bool includeOther = false)//false berasiz i vsyo
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

            var items = GetWithLocalizedNameAsIQueryable()
                .ToList();

            var result = new List<SelectListItem>();
            foreach (var item in items)
            {
                var selectListItem = new SelectListItem();
                selectListItem.Value = item.ID.ToString();

                if (item.NameShort != null)
                {
                    if (cultureInfo.Name == "fr") //uz
                        selectListItem.Text = item.NameShort.TextUz;
                    else
                        selectListItem.Text = item.NameShort.TextRu;
                }
                if (string.IsNullOrWhiteSpace(selectListItem.Text))
                    selectListItem.Text = item.Name;

                selectListItem.Selected = (selected != null && selected.Contains(item.ID));

                result.Add(selectListItem);
            }

            //var result = GetWithLocalizedNameAsIQueryable()
            //    .ToList()
            //    .Select(item => new SelectListItem()
            //    {
            //        Value = item.ID.ToString(),
            //        Text = item.Name.ToString(),
            //        Selected = (selected != null && selected.Contains(item.ID))
            //    }).ToList();

            if (includeOther)
            {
                result.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Другое",
                    Selected = false
                });
            }

            return result;
        }

        public virtual List<SelectListItem> GetAsSelectListOne(int? selected = null, bool includeOther = false)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

            var items = GetWithLocalizedNameAsIQueryable()
                .ToList();

            var result = new List<SelectListItem>();
            foreach (var item in items)
            {
                var selectListItem = new SelectListItem();
                selectListItem.Value = item.ID.ToString();

                if (item.NameShort != null)
                {
                    if (cultureInfo.Name == "fr") //uz
                        selectListItem.Text = item.NameShort.TextUz;
                    else
                        selectListItem.Text = item.NameShort.TextRu;
                }
                if (string.IsNullOrWhiteSpace(selectListItem.Text))
                    selectListItem.Text = item.Name;

                selectListItem.Selected = (selected != null && selected == item.ID);

                result.Add(selectListItem);
            }

            //var result = GetWithLocalizedNameAsIQueryable().Select(item => new SelectListItem()
            //{
            //    Value = item.ID.ToString(),
            //    Text = item.Name.ToString(),
            //    Selected = (selected != null && selected == item.ID)
            //}).ToList();

            if (includeOther)
            {
                result.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Другое",
                    Selected = false
                });
            }

            return result;
        }
    }
}
