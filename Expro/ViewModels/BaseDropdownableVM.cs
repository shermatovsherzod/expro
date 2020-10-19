using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;

namespace Expro.ViewModels
{
    public class BaseDropdownableDetailsVM : BaseVM
    {
        public string Name { get; set; }

        public BaseDropdownableDetailsVM() { }

        public BaseDropdownableDetailsVM(BaseModelDropdownable model)
            : base(model)
        {
            if (model == null)
                return;

            Name = model.Name;
        }
    }
}
