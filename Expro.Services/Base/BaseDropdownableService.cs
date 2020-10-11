using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Services
{
    public class BaseDropdownableService<T> : BaseCRUDService<T>
        where T : BaseModelDropdownable
    {
        public BaseDropdownableService(
            IBaseCRUDRepository<T> repository,
            Data.Infrastructure.IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public List<SelectListItem> GetAsSelectList(int[] selected = null, bool includeOther = false)
        {
            var result = GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name.ToString(),
                Selected = (selected != null && selected.Contains(item.ID))
            }).ToList();

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

        public List<SelectListItem> GetAsSelectListOne(int? selected = null, bool includeOther = false)
        {
            var result = GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name.ToString(),
                Selected = (selected != null && selected == item.ID)
            }).ToList();

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
