using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;

namespace Expro.ViewModels
{
    public class BaseVM
    {
        public int ID { get; set; }

        public BaseVM() { }

        public BaseVM(BaseModel model)
        {
            if (model == null)
                return;

            ID = model.ID;
        }
    }
}
