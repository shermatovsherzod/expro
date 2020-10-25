using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class UserLawArea
    {
        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("LawArea")]
        public int LawAreaID { get; set; }
        public virtual LawArea LawArea { get; set; }
    }
}
