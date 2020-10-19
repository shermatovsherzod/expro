using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Models
{
    public class LawArea : BaseModelDropdownable
    {
        public ICollection<UserLawArea> UserLawAreas { get; set; }
        public ICollection<DocumentLawArea> DocumentLawAreas { get; set; }
    }
}
