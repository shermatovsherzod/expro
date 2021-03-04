using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class SelectListItemWithParent : SelectListItem
    {
        public string ParentValue { get; set; }

        public SelectListItemWithParent() { }

        public SelectListItemWithParent(LawArea lawArea)
        {
            if (lawArea == null)
                return;

            BaseDropdownableDetailsVM lawAreaVM = new BaseDropdownableDetailsVM(lawArea);
            Value = lawAreaVM.ID.ToString();
            Text = lawAreaVM.Name;
            ParentValue = lawArea.ParentID.HasValue ? lawArea.ParentID.Value.ToString() : "";
        }
    }
}
