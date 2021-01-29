using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class ExpertEducation : BaseModelAuthorable
    {
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string University { get; set; }

        [StringLength(256)]
        public string Faculty { get; set; }
        public int GraduationYear { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
