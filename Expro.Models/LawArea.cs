using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class LawArea : BaseModelDropdownable
    {
        public int? ParentID { get; set; }
        public virtual LawArea Parent { get; set; }

        [InverseProperty("Parent")]
        public virtual ICollection<LawArea> Children { get; set; }

        public virtual ICollection<UserLawArea> UserLawAreas { get; set; }
        public virtual ICollection<DocumentLawArea> DocumentLawAreas { get; set; }
        public virtual ICollection<CompanyLawArea> CompanyLawAreas { get; set; }
    }
}
