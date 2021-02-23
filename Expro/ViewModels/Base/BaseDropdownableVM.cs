using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expro.Models;

namespace Expro.ViewModels
{
    public class BaseDropdownableDetailsVM : BaseVM
    {
        public string Name { get; set; } = "";

        public BaseDropdownableDetailsVM() { }

        public BaseDropdownableDetailsVM(BaseModelDropdownable model)
            : base(model)
        {
            if (model == null)
                return;

            ID = model.ID;

            if (model.NameShort != null)
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                if (cultureInfo.Name == "fr") //uz
                    Name = model.NameShort.TextUz;
                else
                    Name = model.NameShort.TextRu;
            }

            if (string.IsNullOrWhiteSpace(Name))
                Name = model.Name;
        }
    }
}
