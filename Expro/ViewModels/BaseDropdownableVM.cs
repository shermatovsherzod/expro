using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;

namespace Expro.ViewModels
{
    public class BaseDropdownableDetailsVM
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public BaseDropdownableDetailsVM() { }

        public BaseDropdownableDetailsVM(BaseModelDropdownable model)
        {
            if (model == null)
                return;

            ID = model.ID;
            Name = model.Name;
        }
    }
}
