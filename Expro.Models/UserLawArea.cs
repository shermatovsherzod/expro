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
        public ApplicationUser User { get; set; }

        [ForeignKey("LawArea")]
        public int LawAreaID { get; set; }
        public LawArea LawArea { get; set; }
    }
}
