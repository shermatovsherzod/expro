using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class CompanyLawArea
    {
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("LawArea")]
        public int LawAreaID { get; set; }
        public virtual LawArea LawArea { get; set; }
    }
}
