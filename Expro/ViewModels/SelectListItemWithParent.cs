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
    }
}
