using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class WorkExperience : BaseModelAuthorable
    {
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [StringLength(256)]
        public string Position { get; set; }

        [StringLength(100)]
        public string WorkPeriodFrom { get; set; }

        [StringLength(100)]
        public string WorkPeriodTo { get; set; }

        //public string UserID { get; set; }
        //public virtual ApplicationUser User { get; set; }
    }
}
